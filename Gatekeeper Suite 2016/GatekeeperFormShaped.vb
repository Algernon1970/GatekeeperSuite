Imports System.Net.WebClient
Imports System.Net
Imports System.IO
Imports System.Xml.Serialization
Imports System.Drawing.Printing
Imports System.Environment

Public Class GatekeeperFormShaped
    Dim dummy As String
    Dim ms As MemoryStream
    Dim pc As New PrintSelectorShaped
    Dim onlineFlag As Boolean = False
    Dim wd As New Watchdog(GatekeeperEvents)

    Private Sub GatekeeperForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler wd.watchdogevent, AddressOf watchdogeventhandler
        wd.startwatcher(Me.NotifyIcon1)
        NotifyIcon1.Visible = False
        Dim version As String = regread("HKEY_LOCAL_MACHINE\SOFTWARE\asGateKeeper", "version")
        NotifyIcon1.Text = "One Drive Mapper v" & version
        mainLoad()
    End Sub

    Private Sub watchdogeventhandler(ByVal message As String)
        Try
            GatekeeperEvents.WriteEntry(message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub mainLoad()
        OnlineStatusBall.Image = My.Resources.redball
        Dim screenWidth = My.Computer.Screen.Bounds.Width
        Dim screenHeight = My.Computer.Screen.Bounds.Height
        Dim myWidth = Me.Width
        Dim myHeight = Me.Height

        Dim mypos As New Point((screenWidth / 2) - (myWidth / 2), (screenHeight / 2) - (myHeight / 2))
        Me.Location = mypos
        WebLoader.loadAsMS(My.Resources.SETSID & Utils.GetUserSid(My.User.Name))

        ms = WebLoader.loadAsMS(My.Resources.CHECKONLINE)
        If msToString(ms).Equals("Online") Then
            onlineFlag = True
            WebLoader.loadAsMS(My.Resources.SETUSER & My.User.Name)
            printerChooser()
            processOnline()
            processRemaining()
            OnlineStatusBall.Image = My.Resources.greenball
        Else
            onlineFlag = False
            processOffline()
            processRemaining()
            OnlineStatusBall.Image = My.Resources.amberball
        End If

    End Sub

    Private Sub printerChooser()
        Dim screenWidth = My.Computer.Screen.Bounds.Width
        Dim screenHeight = My.Computer.Screen.Bounds.Height
        Dim myWidth = Me.Width + pc.Width
        Dim myHeight = Me.Height

        Dim mypos As New Point((screenWidth / 2) - (myWidth / 2), (screenHeight / 2) - (myHeight / 2))
        Dim pcpos As New Point(mypos.X + Me.Width, mypos.Y)
        ' remove old printers, not local
        Utils.deleteAllNetworkPrinters()

        ' get list of selectable printers
        ms = WebLoader.loadAsMS(My.Resources.GETALLPRINTERS)
        Dim plist As List(Of printerInfo) = msToPrinterList(ms)
        pc.displayPrinters(plist)

        ms = WebLoader.loadAsMS(My.Resources.CHECKMOBILE)
        If msToString(ms).ToLower.Equals("mobile") Then
            Me.Location = mypos
            pc.Location = pcpos
            pc.Validate()
            pc.Refresh()
            pc.Show()
        End If
    End Sub

    Private Function msToPrinterList(ByRef ms As MemoryStream) As List(Of printerInfo)
        ms.Seek(0, SeekOrigin.Begin)
        Dim plist As New List(Of printerInfo)
        Dim serializer As New XmlSerializer(plist.GetType)
        plist = DirectCast(serializer.Deserialize(ms), List(Of printerInfo))
        Return plist
    End Function

    ''' <summary>
    ''' Items to process that dont mind if online or offline
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub processRemaining()

        ms = WebLoader.loadAsMS(My.Resources.PROCESSPRIV)

        'load priv file and process
        Dim priv As String
        ms = WebLoader.loadAsMS(My.Resources.CHECKPRIV)
        priv = msToString(ms)
        If msToString(ms).Equals("PRIV") Then
            OnlineStatusBall.Text = OnlineStatusBall.Text & " Priv User"
            Me.Refresh()
            doGetPrivFile()
        End If
    End Sub

    Private Sub processMT()
        If IsInGroup(My.Computer.Name, "AS MusicTech Computers") Then
            ms = loadAsMS(My.Resources.MTREDIRECT)
        Else
            ms = loadAsMS(My.Resources.NOMTREDIRECT)
        End If
    End Sub

    ''' <summary>
    ''' Perform login actions with access to the Online Database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub processOnline()
        doLoginRTF()
        Me.Refresh()
        doSetAppData()

    End Sub

    Private Sub doSetAppData()
        ms = WebLoader.loadAsMS(My.Resources.SETAPPDATA)
        OnlineStatusBall.Text = OnlineStatusBall.Text & " " & msToString(ms)
    End Sub

    ''' <summary>
    ''' Perform login actions assuming no access to Database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub processOffline()
        Utils.deleteAllNetworkPrinters()
        Outbox.LoadFile(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & My.Resources.DefaultTextFilename, RichTextBoxStreamType.RichText)
    End Sub

    Private Sub DeclineButton_Click(sender As Object, e As EventArgs) Handles DeclineButton.Click
        DeclineClicked()
    End Sub

    Private Shared Sub DeclineClicked()
        logout()
        Application.Exit()
    End Sub

    Public Shared Sub logout()
        'Force Logout
        Dim t As Single
        Dim objWMIService, objComputer As Object

        'Now get some privileges
        objWMIService = GetObject("Winmgmts:{impersonationLevel=impersonate,(Debug,Shutdown)}")
        For Each objComputer In objWMIService.InstancesOf("Win32_OperatingSystem")
            t = objComputer.Win32Shutdown(0, 0)
        Next
    End Sub

#Region "Actions"
    Private Sub doLoginRTF()
        Me.Visible = True
        ms = WebLoader.loadAsMS(My.Resources.GETLOGINRTF)
        ms.Position = 0
        Try
            Outbox.LoadFile(ms, RichTextBoxStreamType.RichText)
            Outbox.SelectionStart = 0
            Outbox.SelectionLength = 1
            Outbox.ScrollToCaret()
            Outbox.SelectionLength = 0
        Catch ex As Exception
            Outbox.Text = msToString(ms)
        End Try
        ms.Position = 0
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & My.Resources.DefaultTextFilename
        Dim objWriter As New System.IO.StreamWriter(path, False)
        ms.WriteTo(objWriter.BaseStream)
        objWriter.Flush()
        objWriter.Close()
    End Sub

    Private Sub doGetPrivFile()
        ms = WebLoader.loadAsMS(My.Resources.GETPRIVFILE)
        'handle return from middleman  (ok = fine, notfound = privfile not in database, offlinemode = no access to database)
    End Sub

    Private Sub AcceptedButton_Click(sender As Object, e As EventArgs) Handles myAcceptButton.Click
        AcceptClicked()
    End Sub

    Private Sub AcceptClicked()
        If onlineFlag Then
            Dim plist As List(Of printerInfo) = pc.getSelectedPrinters()
            ms = WebLoader.loadAsMS(My.Resources.SETUSERPRINTERS & plistToString(plist))
            pc.Visible = False
            'sendMapPrinters(plist)
            ms = WebLoader.loadAsMS(My.Resources.MAPMYPRINTERS)

        End If

        ms = WebLoader.loadAsMS(My.Resources.UNLATCHGATEKEEPER)
        Threading.Thread.Sleep(1000)
        Me.Visible = False
        Process.Start("C:\windows\explorer.exe")
        Threading.Thread.Sleep(5000)
        ms = WebLoader.loadAsMS(My.Resources.LATCHGATEKEEPER)
        Threading.Thread.Sleep(5000)
        ms = WebLoader.loadAsMS(My.Resources.RECORDLOGIN)
        Me.Visible = False

        NotifyIcon1.Visible = True
    End Sub

    Private Sub sendMapPrinters(ByRef plist As List(Of String))
        Dim paramString As String
        If plist.Count > 0 Then
            paramString = "&plist=" & plist.ElementAt(0)
            For Each printer In plist
                If Not paramString.Contains(printer) Then
                    paramString = String.Format("{0},{1}", paramString, printer)
                End If
            Next
            ms = WebLoader.loadAsMS(My.Resources.MAPTHESEPRINTERS & paramString)
            ' MsgBox(msToString(ms))
        End If
    End Sub

    Private Sub OnlineStatusBall_Click(sender As Object, e As EventArgs) Handles OnlineStatusBall.Click
        Dim version As String = regread("HKEY_LOCAL_MACHINE\SOFTWARE\asGateKeeper", "version")
        Dim ms As MemoryStream = WebLoader.loadAsMS("http://localhost:6510/?command=GETIPA")
        Dim ipa As String = msToString(ms)
        versionLabel.Text = String.Format("{0}({1})", version, ipa)
    End Sub

    Public Function regread(ByVal key As String, valuename As String) As String
        Dim value
        Try
            value = My.Computer.Registry.GetValue(key, valuename, Nothing)
        Catch ex As Exception
            value = ex.Message
        End Try
        If value Is Nothing Then
            value = "1.x"
        End If

        Return value
    End Function

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Dim pwf As New PasswordRequestForm
        Dim res As DialogResult = pwf.ShowDialog
        pwf.Visible = False
        storePW(pwf.passwordBox.Text)
    End Sub

    Public Sub storePW(ByVal pw As String)
        Dim wrapper As New Simple3Des("A$h3y $ch00l")
        pw = wrapper.EncryptData(pw)
        Dim fl As String = Environment.GetFolderPath(SpecialFolder.ApplicationData) & "\onedrivepw.dat"
        If File.Exists(fl) Then
            File.Delete(fl)
        End If
        Using outfile As New StreamWriter(fl)
            outfile.WriteLine(pw)
        End Using

    End Sub

    Private Sub Outbox_KeyDown(sender As Object, e As KeyEventArgs) Handles Outbox.KeyDown
        If e.KeyCode.Equals(Keys.A) Then
            AcceptClicked()
        ElseIf e.KeyCode.Equals(Keys.D) Then
            DeclineClicked()
        End If
    End Sub
#End Region

End Class