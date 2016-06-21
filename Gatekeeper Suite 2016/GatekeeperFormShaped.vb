Imports System.Net.WebClient
Imports System.Net
Imports System.IO
Imports System.Xml.Serialization
Imports System.Drawing.Printing

Public Class GatekeeperFormShaped
    Dim dummy As String
    Dim ms As MemoryStream
    Dim pc As New PrintSelectorShaped
    Dim onlineFlag As Boolean = False

    Private Sub GatekeeperForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mainLoad()
    End Sub

    Private Sub mainLoad()
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
            OnlineStatusBall.Image = My.Resources.amberball
            printerChooser()
            processOnline()
            OnlineStatusBall.Image = My.Resources.greenball
        Else
            onlineFlag = False
            OnlineStatusBall.Image = My.Resources.amberball
            processOffline()
        End If
        processRemaining()
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
        OnlineStatusBall.Image = My.Resources.greenball
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

    Private Sub AcceptedButton_Click(sender As Object, e As EventArgs) Handles AcceptButton.Click
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
        ' ms = WebLoader.loadAsMS(My.Resources.RECORDLOGIN & Environment.UserName)
        Me.Close()
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

#End Region
End Class