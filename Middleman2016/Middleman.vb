Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Net
Imports System.Runtime.InteropServices
Imports murrayju.ProcessExtensions
Imports ADToolsLibrary
Imports System.DirectoryServices.AccountManagement
Imports System.Configuration
Imports System.ComponentModel

Public Class Middleman
    Dim ws As New Webserver(Me)
    Dim user As New user
    Dim online As Boolean
    Dim computerID As Integer = 0
    Dim adTools As New ADTools()
    Dim localAppData As Boolean = False

    Dim getUserIDOverflow As Boolean = False
    Dim countstop As Integer = 0

    Dim debugTable As New gatekeeperdbDataSetTableAdapters.debugtableTableAdapter

    Private Sub protectSettings()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        config.ConnectionStrings.SectionInformation.ProtectSection(Nothing)
        ' We must save the changes to the configuration file.
        config.Save(ConfigurationSaveMode.Full, True)
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        GatekeeperSuiteEvents.WriteEntry("Middleman Start")
        protectSettings()
        user.sid = "0"
        user.userName = "noone"
        user.priv = False
        online = False
        ' recordVersion()
        latchGateKeeper()
        checkOnline()
        ws.startServer()
    End Sub

    Private Sub recordVersion()
        Dim op As New regop
        op.keyname = "HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\"
        op.valuename = "Version"
        op.value = "3.0"
        op.valuetype = "reg_sz"
        op.hive = reghive.machine
        doRegAdd(op)
    End Sub

    Protected Overrides Sub OnStop()
        ws.stopServer()
    End Sub

    Public Function checkOnline() As MemoryStream
        Try
            Dim myId As Integer = GetComputerID()

            InfotableTableAdapter1.Fill(GatekeeperdbDataSet1.Tables("InfoTable"))
            ComputertableTableAdapter1.FillBy(GatekeeperdbDataSet1.Tables("computertable"), My.Computer.Name)
            online = True
            cleanPrivs()
            Return New MemoryStream(Encoding.UTF8.GetBytes("Online"))
        Catch ex As Exception
            GatekeeperSuiteEvents.WriteEntry("CheckOnline : " & ex.Message, EventLogEntryType.Warning)
            online = False
            Return New MemoryStream(Encoding.UTF8.GetBytes("Offline"))
        End Try
    End Function

    Private Function checkGroup(ByVal grpName As String) As Boolean
        Using ctx As PrincipalContext = ADTools.getConnection("as.internal", "OU=Security Groups,OU=AS Groups,OU=Ashby School,DC=as,DC=internal")
            Using gtx As GroupPrincipal = ADTools.getGroupPrincipalbyName(ctx, grpName)
                For Each member In gtx.GetMembers
                    Dim name As String = member.Name
                    If name.Equals(My.Computer.Name) Then
                        Return True
                    End If
                Next
            End Using
        End Using
        Return False
    End Function

    Public Function setAppData() As MemoryStream
        Try
            If checkGroup("AS MusicTech Computers") Then
                localAppData = True
            End If
        Catch ex As Exception

        End Try
        Dim appLocation As String
        Dim ret As MemoryStream
        If localAppData Then
            If IO.Directory.Exists("D:\") Then
                appLocation = "D:\" & user.userName & "\appData"
                ret = New MemoryStream(Encoding.UTF8.GetBytes("AppData D"))
            Else
                appLocation = "C:\Program Files\Ashby School\" & user.userName & "\appData"
                ret = New MemoryStream(Encoding.UTF8.GetBytes("AppData C"))
            End If
            If Not IO.Directory.Exists(appLocation) Then
                IO.Directory.CreateDirectory(appLocation)
            End If
        Else
            appLocation = "N:\My Settings\Application Data"
            ret = New MemoryStream(Encoding.UTF8.GetBytes("AppData N"))
        End If
        Dim op As New regop
        op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", user.sid)
        op.valuename = "appdata"
        op.valuetype = "reg_sz"
        op.value = appLocation
        doRegAdd(op)
        op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", user.sid)
        doRegAdd(op)
        op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\Volatile Environment", user.sid)
        doRegAdd(op)
        op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\Environment", user.sid)
        doRegAdd(op)

        Return ret
    End Function

    Public Function mtRedirect(redirect As Boolean) As MemoryStream
        Dim op As New regop
        op.hive = reghive.user
        Dim sid As String = getSid()
        If redirect Then
            Dim appLocation As String
            If IO.Directory.Exists("D:\") Then
                appLocation = "D:"
            Else
                appLocation = "C:\Program Files\Ashby School"
            End If

            If Not IO.Directory.Exists(String.Format("{0}\{1}\AppData", appLocation, getUsername)) Then
                IO.Directory.CreateDirectory(String.Format("{0}\{1}\AppData", appLocation, getUsername))
            End If

            op.valuetype = "reg_sz"
            op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders\", sid)
            op.valuename = "AppData"
            op.value = String.Format("{0}\{1}\appdata", appLocation, getUsername)
            doRegAdd(op)

            op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders\", sid)
            doRegAdd(op)

            op.keyname = String.Format("HKEY_USERS\{0}\Volatile Environment\", sid)
            doRegAdd(op)

            op.keyname = String.Format("HKEY_USERS\{0}\Environment\", sid)
            doRegAdd(op)

            op.keyname = String.Format("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}")
            op.valuetype = "reg_dword"
            op.valuename = "NoBackgroundPolicy"
            op.value = "0x00000001"
            doRegAdd(op)
            op.valuename = "NoGPOListChanges"
            doRegAdd(op)
        Else
            Dim value = My.Computer.Registry.GetValue(String.Format("HKEY_USERS\{0}\Environment\", sid), "AppData", Nothing)
            If value.ToString.StartsWith("N:\") Then
                Dim appLocation As String = "N:\My Settings\Application Data"
                op.valuetype = "reg_sz"
                op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders\", sid)
                op.valuename = "AppData"
                op.value = String.Format("{0}\{1}\appdata", appLocation, getUsername)
                doRegAdd(op)

                op.keyname = String.Format("HKEY_USERS\{0}\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders\", sid)
                doRegAdd(op)

                op.keyname = String.Format("HKEY_USERS\{0}\Volatile Environment\", sid)
                doRegAdd(op)

                op.keyname = String.Format("HKEY_USERS\{0}\Environment\", sid)
                doRegAdd(op)

            End If
        End If

        Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
    End Function

    Public Function recordLogin() As MemoryStream
        If online Then
            getUserIDOverflow = False
            Dim computerID As Integer = GetComputerID()
            Dim userID As Integer = getUserID(Environment.UserName)
            storeLogin(computerID, userID)
            Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
        Else
            Return New MemoryStream(Encoding.UTF8.GetBytes("OFFLINE"))
        End If
    End Function

    Private Sub storeLogin(ByVal computer As Integer, ByVal user As Integer)
        If online Then
            Try
                Dim loginRecorder As New gatekeeperdbDataSetTableAdapters.userlogintableTableAdapter
                loginRecorder.Insert(computer, user, Now, Nothing)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Function getUserID(ByVal name As String) As Integer
        If online Then
            Dim users As New gatekeeperdbDataSetTableAdapters.usertableTableAdapter
            Try
                Dim userID As Integer = users.GetUserByName(name)
                Return userID
            Catch ex As Exception
                If getUserIDOverflow Then Return -1
                getUserIDOverflow = True
                If ex.Message.StartsWith("Nullable") Then
                    users.Insert(name)
                    Return getUserID(name)
                End If
            End Try
        End If

        Return -1
    End Function

    ''' <summary>
    ''' Get/store computer in DB
    ''' </summary>
    ''' <returns>Key to Computertable</returns>
    Private Function GetComputerID() As Integer
        If online Then
            Try
                Dim drs() As DataRow = GatekeeperdbDataSet1.Tables("computertable").Select(String.Format("Name like '{0}'", My.Computer.Name))
                If drs.Count = 0 Then
                    ComputertableTableAdapter1.Insert(My.Computer.Name)
                    ComputertableTableAdapter1.FillBy(GatekeeperdbDataSet1.Tables("computertable"), My.Computer.Name)
                End If
                Dim jar() As DataRow = GatekeeperdbDataSet1.Tables("computertable").Select(String.Format("Name like '{0}'", My.Computer.Name))
                Return jar(0).Field(Of Integer)("Id")
            Catch ex As Exception
                Return -1
            End Try
        End If

        Return -1
    End Function

    Public Function getversion() As MemoryStream
        Dim line As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\", "Version", Nothing)
        If IsNothing(line) Then
            Return New MemoryStream(Encoding.UTF8.GetBytes("V0"))
        End If
        Return New MemoryStream(Encoding.UTF8.GetBytes(line))
    End Function

    Public Sub forcelogout()
        Utils.logout()
    End Sub

    ''' <summary>
    ''' Load the privFile from the database and store it on the local machine for later processing
    ''' </summary>
    ''' <returns>Status</returns>
    ''' <remarks></remarks>
    Public Function getPrivFile() As MemoryStream
        Try
            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Ashby School"
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            Dim location As String = path & My.Resources.LocalPrivFile
            If online Then
                If privFileUpdateNeeded() Then
                    Dim result As Byte() = InfotableTableAdapter1.GetPrivFile
                    Dim ms As New MemoryStream()
                    ms.Write(result, 0, result.Length)
                    ms.Seek(0, SeekOrigin.Begin)
                    ms.Position = 0
                    Dim objWriter As New System.IO.StreamWriter(location, False)
                    ms.WriteTo(objWriter.BaseStream)
                    objWriter.Flush()
                    objWriter.Close()

                End If
                Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
            End If
            Return New MemoryStream(Encoding.UTF8.GetBytes("OfflineMode"))
        Catch ex As Exception
            Return New MemoryStream(Encoding.UTF8.GetBytes("OfflineMode"))
        End Try

    End Function

    Private Function privFileUpdateNeeded() As Boolean
        If online Then
            Try
                Dim updateDate As DateTime = InfotableTableAdapter1.GetPrivUpdate
                Dim localDate As DateTime
                Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Ashby School"
                Dim infoReader As FileInfo = My.Computer.FileSystem.GetFileInfo(path & My.Resources.LocalPrivFile)
                localDate = infoReader.LastWriteTime
                If updateDate.CompareTo(localDate) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Private Sub cleanPrivs()
        If online Then
            Dim changed As Boolean = False
            Try
                Dim privTableUsers As New List(Of String)
                Dim privUserTableAdapter = New gatekeeperdbDataSetTableAdapters.privUserTableAdapter
                Dim privuserdatatable = privUserTableAdapter.GetData(My.Computer.Name)
                If privuserdatatable.Rows.Count > 0 Then
                    For Each userrow As DataRow In privuserdatatable.Rows
                        privTableUsers.Add(userrow.Field(Of String)("Username"))
                    Next
                End If

                Dim privusers As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\", "PrivUsers", Nothing).tolower
                Dim puserslist As String() = privusers.Split(",")
                For Each puser In puserslist
                    If Not privTableUsers.Contains(puser) Then

                        unlocalAdmin(My.Computer.Name, puser)
                        privusers = privusers.Replace(puser.ToLower, "")
                        privusers = privusers.Replace(",,", ",")
                        changed = True
                    End If

                Next
                If changed Then
                    My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\", "PrivUsers", privusers)
                End If

            Catch ex As Exception
                changed = False
            End Try
        End If
    End Sub

    Public Function launchExternal(ByVal cmdline As String) As MemoryStream
        Dim cmd() As String
        If cmdline.Contains(",") Then
            cmd = cmdline.Split(",", 2, StringSplitOptions.RemoveEmptyEntries)
            GatekeeperSuiteEvents.WriteEntry(String.Format("Call External {0}, {1}", cmd(0), cmd(1)))
            ProcessExtensions.StartProcessAsCurrentUser(cmd(0), cmd(1))
        Else
            GatekeeperSuiteEvents.WriteEntry(String.Format("Call External {0}", cmdline))
            ProcessExtensions.StartProcessAsCurrentUser(cmdline)
        End If
        Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
    End Function

    Public Function lockWorkstation() As MemoryStream
        ProcessExtensions.StartProcessAsCurrentUser("C:\program files\ashby school\gatekeeper2016\userutilities.exe", "Utilities " & My.Resources.LOCK, "C:\program files\ashby school\gatekeeper2016", True)
        Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
    End Function

    Public Function gpupdate() As MemoryStream
        ProcessExtensions.StartProcessAsCurrentUser("C:\program files\ashby school\gatekeeper2016\userutilities.exe", "Utilities " & My.Resources.GPUPDATE, "C:\program files\ashby school\gatekeeper2016", True)
        Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
    End Function

    Public Function messageUser(ByVal message As String)
        ProcessExtensions.StartProcessAsCurrentUser("C:\program files\ashby school\gatekeeper2016\userutilities.exe", "Utilities " & My.Resources.MSG & message)
        Return New MemoryStream(Encoding.UTF8.GetBytes("OK"))
    End Function

    Public Function getLoginRTF() As MemoryStream
        If online Then
            Try
                Dim foundrows() As gatekeeperdbDataSet.infotableRow
                foundrows = GatekeeperdbDataSet1.Tables("InfoTable").Select("type like 'Login'")
                For Each row As DataRow In foundrows
                    Dim ms As New MemoryStream()
                    ms.Write(row.Field(Of Byte())("info"), 0, row.Field(Of Byte())("info").Length)
                    ms.Seek(0, SeekOrigin.Begin)
                    Return (ms)
                Next
                Return New MemoryStream(Encoding.UTF8.GetBytes("NotFound"))
            Catch ex As Exception
                Return New MemoryStream(Encoding.UTF8.GetBytes("OfflineMode"))
            End Try
        End If
        Return New MemoryStream(Encoding.UTF8.GetBytes("OfflineMode"))
    End Function

    Public Function getMyComputer() As MemoryStream
        Dim str As String = ""
        If online Then
            Try
                For Each myRow As gatekeeperdbDataSet.computertableRow In GatekeeperdbDataSet1.Tables("computertable").Rows
                    computerID = myRow.Field(Of Integer)("ID")
                    str = String.Format("{2} :: ID = {0}{1}", computerID, vbCrLf, str)
                Next
            Catch ex As Exception
                str = "offline"
            End Try
            Dim ms As MemoryStream

            ms = New MemoryStream(Encoding.UTF8.GetBytes(str))
            Return ms
        End If
        Return New MemoryStream(Encoding.UTF8.GetBytes("offline"))
    End Function

    ''' <summary>
    ''' Store username, and get the users ID from the DB
    ''' If user not found, store a new entry on the DB and get the ID
    '''
    '''
    ''' </summary>
    ''' <param name="uname"></param>
    ''' <remarks></remarks>
    Public Sub setUser(ByVal uname As String)

        Dim luser As String = uname.ToLower
        user.userName = luser.Split("\")(1)
        If online Then
            Try
                UsertableTableAdapter1.ClearBeforeFill = True
                Dim userTable As DataTable = UsertableTableAdapter1.GetDataBy1(user.userName)
                If userTable.Rows.Count = 0 Then
                    UsertableTableAdapter1.AddUserQuery(user.userName)
                    If countstop > 2 Then Return
                    countstop = countstop + 1
                    setUser(uname)
                    Return
                End If
                user.id = userTable.Rows(0).Field(Of Integer)("ID")
            Catch ex As Exception

            End Try
        End If

    End Sub

    Public Function getUsername() As String
        Return user.userName
    End Function

    Public Function getUserID() As MemoryStream
        Dim line As String = String.Format("User {0} = {1}", user.userName, user.id)
        Return New MemoryStream(Encoding.UTF8.GetBytes(line))
    End Function

    Public Sub setSid(ByVal sid As String)
        user.sid = sid
    End Sub
    Public Function getSid() As String
        Return user.sid
    End Function

    Public Sub setPriv(ByVal priv As Boolean)
        user.priv = priv
    End Sub
    Public Function getPriv() As Boolean
        Return user.priv
    End Function

    Public Function amIPriv() As MemoryStream
        Dim ms As New MemoryStream
        storePrivs()

        Dim privusers As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\", "PrivUsers", Nothing)
        If IsNothing(privusers) Then Return New MemoryStream(Encoding.UTF8.GetBytes("NOTPRIV"))
        If privusers.ToLower.Contains(user.userName.ToLower) Then
            flagPriv(True)
            gpoBackgroundProcessing(False)
            redirectDesktop("%userprofile%\desktop")
            user.priv = True
            ms = New MemoryStream(Encoding.UTF8.GetBytes("PRIV"))
        Else
            user.priv = False
            ms = New MemoryStream(Encoding.UTF8.GetBytes("NOTPRIV"))
            If isPrivFlag() Then
                gpoBackgroundProcessing(True)
                redirectDesktop("c:\Users\Shared Desktop\")
                gpupdate()
                flagPriv(False)
            End If
        End If

        Return ms
    End Function

    ''' <summary>
    ''' Check to see if priv list updated (if we can see the DB online), then find out of we are priv, and if so, process the privs file.
    ''' </summary>
    ''' <returns>string indicating priv/nonpriv status</returns>
    ''' <remarks>Needs cleaning up do we need to check priv again?
    ''' Did we already call amIPriv?</remarks>
    Public Function processPriv() As MemoryStream
        Dim ms As New MemoryStream
        storePrivs()

        Dim privusers As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\", "PrivUsers", Nothing)
        If IsNothing(privusers) Then Return New MemoryStream(Encoding.UTF8.GetBytes("NOTPRIV"))
        If privusers.ToLower.Contains(user.userName.ToLower) Then
            user.priv = True
            ms = New MemoryStream(Encoding.UTF8.GetBytes("PRIV"))
            loadPrivFile()
            localAdmin(".", user.userName.ToLower)
        Else
            user.priv = False
            ms = New MemoryStream(Encoding.UTF8.GetBytes("NOTPRIV"))
        End If

        Return ms
    End Function

    Public Function checkMobile() As MemoryStream
        Dim ms As MemoryStream
        Dim check As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\", "DeviceType", Nothing)
        If IsNothing(check) Then Return New MemoryStream(Encoding.UTF8.GetBytes("Static"))

        If check.ToLower.Contains("static") Then
            ms = New MemoryStream(Encoding.UTF8.GetBytes("Static"))
        Else
            ms = New MemoryStream(Encoding.UTF8.GetBytes("Mobile"))
        End If

        Return ms
    End Function

    ''' <summary>
    ''' If online, grab a list of priv users from the DB and store in the registry for offline use
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub storePrivs()
        If online Then
            Try
                Dim users As String = ""
                Dim privUserTableAdapter = New gatekeeperdbDataSetTableAdapters.privUserTableAdapter
                Dim privuserdatatable = privUserTableAdapter.GetData(My.Computer.Name)
                If privuserdatatable.Rows.Count > 0 Then
                    For Each userrow As DataRow In privuserdatatable.Rows
                        users = users & userrow.Field(Of String)("Username") & ","
                    Next
                    If users.EndsWith(",") Then
                        users = users.TrimEnd(",")
                    End If
                    Dim op As New regop
                    op.keyname = "HKEY_LOCAL_MACHINE\SOFTWARE\asGatekeeper\"
                    op.valuename = "PrivUsers"
                    op.value = users.ToLower
                    op.valuetype = "reg_sz"
                    doRegAdd(op)
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    ''' <summary>
    ''' process the priv file. Load from local file then edit registry based on entries.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadPrivFile()
        Dim regops As New List(Of regop)
        Dim op As New regop
        Dim line As String

        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Ashby School"
        ' Load local priv file into list of operations
        If File.Exists(path & My.Resources.LocalPrivFile) Then
            Dim fr As New StreamReader(path & My.Resources.LocalPrivFile)
            Do While fr.Peek() <> -1
                line = fr.ReadLine()
                ' line = System.Text.Encoding.ASCII.GetString(simpleEncrypt(System.Text.Encoding.ASCII.GetBytes(line)))
                If line.StartsWith("KeyName") Then
                    op = New regop
                    op.hive = reghive.user
                    op.keyname = line.Split(":", 2, StringSplitOptions.None)(1)
                ElseIf line.StartsWith("ValueName") Then
                    op.valuename = line.Split(":", 2, StringSplitOptions.None)(1)
                ElseIf line.StartsWith("ValueType") Then
                    op.valuetype = line.Split(":", 2, StringSplitOptions.None)(1)
                ElseIf line.StartsWith("Value") Then
                    op.value = line.Split(":", 2, StringSplitOptions.None)(1)
                    regops.Add(op)
                End If
            Loop
            fr.Close()
            fr.Dispose()

            enactPrivs(regops)

        End If
    End Sub

    Private Sub enactPrivs(ByRef ops As List(Of regop))
        For Each op In ops
            If op.hive = reghive.machine Then
                op.keyname = "HKEY_LOCAL_MACHINE\" & op.keyname
            ElseIf op.hive = reghive.user Then
                op.keyname = "HKEY_USERS\" & user.sid & "\" & op.keyname
            End If
            If op.valuename.StartsWith("**del.") Then
                doregdel(op)
            ElseIf op.valuename.StartsWith("**delvals.") Then
                doregdelall(op)
            Else
                doRegAdd(op)
            End If
        Next
    End Sub

    Private Sub flagPriv(ByVal flag As Boolean)
        Dim op As New regop
        op.keyname = "HKEY_USERS\" & user.sid & "\Software\Ashby"
        op.valuename = "PrivUser"
        op.valuetype = "Reg_Sz"
        If flag Then
            op.value = "True"
        Else
            op.value = "False"
        End If
        doRegAdd(op)
    End Sub

    Private Sub redirectDesktop(ByVal loc As String)
        Dim op As New regop
        op.keyname = "HKEY_USERS\" & user.sid & "\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders"
        op.valuename = "Desktop"
        op.valuetype = "Reg_Sz"
        op.value = loc
        doRegAdd(op)
    End Sub

    Private Sub gpoBackgroundProcessing(ByVal proc As Boolean)
        Dim op As regop
        op.keyname = "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}"
        op.valuename = "NoBackgroundPolicy"
        op.valuetype = "Reg_DWORD"
        If proc Then
            op.value = "0x00000000"
        Else
            op.value = "0x00000001"
        End If
        doRegAdd(op)
    End Sub

    Private Function isPrivFlag() As Boolean
        Dim value As String = regread("HKEY_USERS\" & user.sid & "\Software\Ashby", "PrivUser")
        If value.ToLower.Equals("true") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function setWallpaper(ByRef paper As String) As MemoryStream
        Utils.setWallpaper(paper)
        Return New MemoryStream(Encoding.UTF8.GetBytes(paper))
    End Function

    Public Function latchGateKeeper() As MemoryStream
        Dim ms As New MemoryStream
        ms = New MemoryStream(Encoding.UTF8.GetBytes("OK"))
        Dim op As New regop
        op.keyname = "HKEY_LOCAL_MACHINE\Software\microsoft\windows nt\currentversion\winlogon"
        op.valuename = "Shell"
        op.valuetype = "REG_SZ"
        op.value = "C:\Program Files\Ashby School\Gatekeeper2016\Gatekeeper Suite 2016.exe"
        doRegAdd(op)
        Return ms
    End Function

    Public Function unlatchGateKeeper() As MemoryStream
        Dim ms As New MemoryStream
        ms = New MemoryStream(Encoding.UTF8.GetBytes("OK"))
        Dim op As New regop
        op.keyname = "HKEY_LOCAL_MACHINE\Software\microsoft\windows nt\currentversion\winlogon"
        op.valuename = "Shell"
        op.valuetype = "REG_SZ"
        op.value = "explorer.exe"
        doRegAdd(op)
        Return ms
    End Function

#Region "Registry"
    Public Function registry(ByRef url As String, ByRef req As HttpListenerRequest) As MemoryStream
        Dim ms As New MemoryStream(Encoding.UTF8.GetBytes("ok"))
        If req.QueryString("regcommand").Equals("ADD") Then
            regadd(req)
        ElseIf req.QueryString("regcommand").Equals("DEL") Then
            regdel(req)
        ElseIf req.QueryString("regcommand").Equals("READ") Then
            ms = New MemoryStream(Encoding.UTF8.GetBytes(regread(req)))
        End If
        Return ms
    End Function

    ''' <summary>
    ''' Perform regadd from browser/client request
    ''' </summary>
    ''' <param name="req"></param>
    ''' <remarks></remarks>
    Public Sub regadd(ByRef req As HttpListenerRequest)
        Dim op As New regop

        op.keyname = "HKEY_USERS\" & user.sid & "\" & req.QueryString("key")
        op.valuename = req.QueryString("valuename")
        op.valuetype = req.QueryString("valuetype")
        op.value = req.QueryString("value")
        doRegAdd(op)
    End Sub

    ''' <summary>
    ''' Perform regadd from regop data
    ''' </summary>
    ''' <param name="op"></param>
    ''' <remarks></remarks>
    Private Sub doRegAdd(ByRef op As regop)
        If op.valuetype.ToLower.Equals("reg_sz") Then
            My.Computer.Registry.SetValue(op.keyname, op.valuename, op.value)
        ElseIf op.valuetype.ToLower.Equals("reg_dword") Then
            Dim value As String = op.value.Split("x")(1)
            Dim dword As Integer = Convert.ToInt32(value, 16)
            My.Computer.Registry.SetValue(op.keyname, op.valuename, dword, Microsoft.Win32.RegistryValueKind.DWord)
        End If
    End Sub

    Public Sub regdel(ByRef req As HttpListenerRequest)
        Throw New Exception("Not Implemented")
    End Sub

    Public Sub doregdel(ByRef op As regop)
        Dim valuename As String = op.valuename.Split(".")(1)
        Dim key = My.Computer.Registry.Users.OpenSubKey(op.keyname.Split("\", 2, StringSplitOptions.None)(1), True)
        If key Is Nothing Then Return
        Try
            key.DeleteValue(valuename)
            key.Flush()
            key.Close()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub doregdelall(ByRef op As regop)
        Try
            My.Computer.Registry.Users.DeleteSubKey(op.keyname)
        Catch ex As Exception

        End Try

    End Sub

    Public Function regread(ByRef req As HttpListenerRequest) As String
        Dim key As String = req.QueryString("key")
        Dim valuename As String = req.QueryString("valuename")
        Return regread(key, valuename)

    End Function

    Public Function regread(ByVal key As String, valuename As String) As String
        Dim value
        Try
            value = My.Computer.Registry.GetValue(key, valuename, Nothing)
        Catch ex As Exception
            value = ex.Message
        End Try
        If value Is Nothing Then
            value = "Value does not exist"
        End If

        Return value
    End Function
#End Region

#Region "Printers"
    Public Function getUserPrinters() As MemoryStream
        Try
            Dim userPrintersReg As String = String.Format("HKEY_USERS\{0}\Software\Ashby\", user.sid)
            Dim valuename As String = "SelectedPrinters"
            Dim plist As String = regread(userPrintersReg, valuename)
            GatekeeperSuiteEvents.WriteEntry("getUserPrinters : " & plist)
            Return New MemoryStream(Encoding.UTF8.GetBytes(plist))
        Catch ex As Exception
            Return New MemoryStream(Encoding.UTF8.GetBytes("novalue"))
        End Try

    End Function

    Public Function setUserPrinters(ByVal plist As String) As MemoryStream
        Dim newop As New regop()
        newop.valuetype = "reg_sz"
        newop.valuename = "SelectedPrinters"
        newop.keyname = String.Format("HKEY_USERS\{0}\Software\Ashby\", user.sid)
        newop.value = plist
        doRegAdd(newop)
        GatekeeperSuiteEvents.WriteEntry("setUserPrinters : " & plist)
        Return New MemoryStream(Encoding.UTF8.GetBytes(String.Format("Stored {0} in {1}", plist, newop.keyname)))
    End Function

    ''' <summary>
    ''' Get a list of printers assigned just to this station, and return it as a list of objects to the web-client  DEPRICATED
    ''' </summary>
    ''' <returns>list of printerinfo objects</returns>
    Public Function getMyPrinters() As MemoryStream
        Try
            Dim ms As New MemoryStream
            Dim myPrintersTableAdapter1 As New gatekeeperdbDataSetTableAdapters.myPrintersTableAdapter()
            myPrintersTableAdapter1.Fill(GatekeeperdbDataSet1.Tables("myPrinters"), My.Computer.Name)
            Dim lpinfo As New printerInfo
            Dim plist As New List(Of printerInfo)
            Dim serializer As New XmlSerializer(plist.GetType)

            For Each row As gatekeeperdbDataSet.myPrintersRow In GatekeeperdbDataSet1.Tables("myPrinters").Rows
                lpinfo.name = row.printerName
                lpinfo.connection = row.Connection
                lpinfo.isSelectable = True
                lpinfo.isDefault = row.isDefault
                plist.Add(lpinfo)
            Next
            GatekeeperSuiteEvents.WriteEntry("getMyPrinters got " & plist.Count)
            serializer.Serialize(ms, plist)
            Return ms
        Catch ex As Exception
            GatekeeperSuiteEvents.WriteEntry("ex.message", EventLogEntryType.Error)
            Return Nothing
        End Try
    End Function

    'Public Function mapThesePrinters(ByVal plist As String) As MemoryStream
    '    Dim def As String = "jkfhsjh"
    '    Dim sel As Char
    '    Dim listbits As String() = plist.Split(",")
    '    Dim pinfo As New printerInfo
    '    GatekeeperSuiteEvents.WriteEntry("plist = " & plist, EventLogEntryType.Warning)
    '    Try
    '        For Each printername As String In listbits
    '            If printername.StartsWith("$") Then
    '                def = "dkjfhgj"
    '                pinfo.isDefault = False
    '            Else
    '                def = My.Resources.PRINTERDEFAULT
    '                pinfo.isDefault = True
    '            End If
    '            printername = printername.Substring(1)

    '            sel = printername(printername.Length - 1)
    '            printername = printername.Remove(printername.Length - 1)

    '            If sel.Equals("^"c) Then
    '                pinfo.name = printername
    '                pinfo.connection = PrinterTableAdapter1.getPrinterConnectionByName(printername)
    '                GatekeeperSuiteEvents.WriteEntry(String.Format("mapThesePrinters + : {0}, {1}", pinfo.name, pinfo.connection))
    '                ProcessExtensions.StartProcessAsCurrentUser("C:\program files\ashby school\gatekeeper2016\userutilities.exe",
    '                                                        String.Format("utilites {4}{3}{0}{3},{1},{2}", pinfo.name, pinfo.connection, def, ControlChars.Quote, My.Resources.PRINTER))
    '                recordPrinter(pinfo, printerop.add)
    '            Else
    '                GatekeeperSuiteEvents.WriteEntry(String.Format("mapThesePrinters - : {0}, {1}", pinfo.name, pinfo.connection))
    '                recordPrinter(pinfo, printerop.remove)
    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try

    '    Return New MemoryStream(Encoding.UTF8.GetBytes("paramstring = " & plist))
    'End Function

    'Private Sub recordPrinter(ByRef pinfo As printerInfo, op As printerop)
    '    Dim cid As Integer = GetComputerID()
    '    Dim pid As Integer = PrinterTableAdapter1.getPrinterIDByName(pinfo.name)
    '    Dim def As Byte = 0

    '    If pinfo.isDefault Then def = 1

    '    If op = printerop.add Then
    '        Dim count As Integer = ComputerprinterTableAdapter1.exists(cid, pid)
    '        If count = 0 Then
    '            ComputerprinterTableAdapter1.Insert(cid, pid, def)
    '        End If
    '    End If

    '    If op = printerop.remove Then
    '        Dim cpid As Integer = ComputerprinterTableAdapter1.GetIDbyDetails(cid, pid)
    '        If cpid > 0 Then
    '            ComputerprinterTableAdapter1.Delete(cpid, cid, pid, 1)
    '            ComputerprinterTableAdapter1.Delete(cpid, cid, pid, 0)

    '        End If
    '    End If
    '    ComputerprinterTableAdapter1.Update(GatekeeperdbDataSet1.Tables("computerprinter"))
    'End Sub

    Private Function isPrinterDefault(ByRef printerString As String, printer As String) As Boolean
        Dim userPrintersList As String() = printerString.Split(",")
        For Each printerDef As String In userPrintersList
            If printerDef.Contains(printer) Then
                If printerDef.StartsWith("^") Then
                    Return True
                Else
                    Return False
                End If

            End If
        Next
        Return False
    End Function

    Public Function getAllPrinters() As MemoryStream
        Try
            Dim ms As New MemoryStream
            Dim userPrinters As String = msToString(getUserPrinters)

            Dim PrintersTableAdapter1 As New gatekeeperdbDataSetTableAdapters.printerTableAdapter()
            PrintersTableAdapter1.Fill(GatekeeperdbDataSet1.Tables("Printer"))

            Dim lpinfo As printerInfo
            Dim plist As New List(Of printerInfo)
            Dim serializer As New XmlSerializer(plist.GetType)

            For Each row As gatekeeperdbDataSet.printerRow In GatekeeperdbDataSet1.Tables("Printer").Rows
                'query to see if printer is in myprinters, if so, set selected.

                lpinfo = New printerInfo
                If userPrinters.Contains(row.Name) Then
                    lpinfo.isSelected = True
                    If isPrinterDefault(userPrinters, row.Name) Then
                        lpinfo.isDefault = True
                    Else
                        lpinfo.isDefault = False
                    End If
                Else
                    lpinfo.isSelected = False
                    lpinfo.isDefault = False
                End If

                lpinfo.name = row.Name
                lpinfo.connection = row.Connection
                If row.Selectable = 1 Then
                    lpinfo.isSelectable = True
                Else
                    lpinfo.isSelectable = False
                End If
                plist.Add(lpinfo)
            Next

            serializer.Serialize(ms, plist)
            Return ms
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function mapMyPrinters() As MemoryStream
        GatekeeperSuiteEvents.WriteEntry("mapMyPrinters : ")
        Try
            Dim ms As New MemoryStream
            Dim pinfo As New printerInfo
            Dim myPrintersTableAdapter1 As New gatekeeperdbDataSetTableAdapters.myPrintersTableAdapter()
            myPrintersTableAdapter1.Fill(GatekeeperdbDataSet1.Tables("myPrinters"), My.Computer.Name)
            For Each prow As DataRow In GatekeeperdbDataSet1.Tables("myPrinters").Rows
                pinfo.connection = prow.Field(Of String)("Connection")
                pinfo.name = prow.Field(Of String)("PrinterName")
                Dim def As String = If(prow.Field(Of Boolean)("isDefault"), My.Resources.PRINTERDEFAULT, "fj67T3xH")
                GatekeeperSuiteEvents.WriteEntry(String.Format("mapMyPrinters : {0}({1})", pinfo.connection, def))
                ProcessExtensions.StartProcessAsCurrentUser("C:\program files\ashby school\gatekeeper2016\userutilities.exe",
                                                            String.Format("utilites {4}{3}{0}{3},{1},{2}", pinfo.name, pinfo.connection, def, ControlChars.Quote, My.Resources.PRINTER))
            Next
            mapUserPrinters()
            Return New MemoryStream(Encoding.UTF8.GetBytes("Mapped " & GatekeeperdbDataSet1.Tables("myPrinters").Rows.Count))
        Catch ex As Exception
            GatekeeperSuiteEvents.WriteEntry("mapMyPrinters : " & ex.Message)
            mapUserPrinters()
            Return New MemoryStream(Encoding.UTF8.GetBytes("Mapped None"))
        End Try

    End Function

    Private Sub mapUserPrinters()
        Dim pname As String
        Dim pinfo As New printerInfo
        Dim def As String = "fj66T3xX"
        Dim plistString As String = msToString(getUserPrinters())
        If plistString.ToLower.Equals("novalue") Then
            Return
        End If
        Dim plist As String() = plistString.Split(",")
        For Each pstring As String In plist
            Try
                If pstring.Length > 1 Then
                    pname = pstring.Substring(1)
                    pinfo.name = pname
                    pinfo.connection = PrinterTableAdapter1.getPrinterConnectionByName(pname)

                    If pstring.StartsWith("^") Then
                        def = My.Resources.PRINTERDEFAULT
                    Else
                        def = "fj67T3xH"
                    End If
                    GatekeeperSuiteEvents.WriteEntry(String.Format("MapUserPrinters: {0} ({1})", pinfo.connection, def), EventLogEntryType.Information)
                    ProcessExtensions.StartProcessAsCurrentUser("C:\program files\ashby school\gatekeeper2016\userutilities.exe",
                                                           String.Format("utilites {4}{3}{0}{3},{1},{2}", pinfo.name, pinfo.connection, def, ControlChars.Quote, My.Resources.PRINTER))
                End If

            Catch ex As Exception
                GatekeeperSuiteEvents.WriteEntry(String.Format("MapUserPrinters: {0}", ex.Message), EventLogEntryType.Error)
            End Try
        Next

    End Sub
#End Region
End Class

Public Structure user
    Dim userName As String
    Dim sid As String
    Dim priv As Boolean
    Dim id As Integer
End Structure

Public Structure regop
    Dim keyname As String
    Dim valuename As String
    Dim valuetype As String
    Dim value As String
    Dim hive As reghive
End Structure

Public Enum reghive
    user
    machine
End Enum

Public Enum printerop
    add
    remove
End Enum