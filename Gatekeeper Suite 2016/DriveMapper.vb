Imports System.DirectoryServices
Imports System.DirectoryServices.AccountManagement
Imports System.Environment
Imports System.IO
Imports AshbyTools
Imports AS365Cookie

Public Class DriveMapper
    Dim commandStore As New Dictionary(Of String, String)
    Public usersCTXString As String = "OU=AS Users, OU=Ashby School, DC=as, DC=Internal"
    Dim myMaps As String = ""
    Dim appdata As String = GetFolderPath(SpecialFolder.LocalApplicationData)
    Dim progdata As String = "C:\program files"
    Dim gatekeeperevents As EventLog

    Public Sub New(ByRef log As EventLog)
        CustomLogger.writeEntry(appdata & "\log.txt", "Starting DriveMapper")
    End Sub

    Private Sub setupCommandStore()
        Dim filename As String = progdata & "\Ashby School\ASOneDriveMapper.ini"
        If File.Exists(filename) Then
            commandStore.Clear()

            Using sr As New StreamReader(filename)
                Dim line As String = "."
                Dim lines() As String
                While Not IsNothing(line)
                    line = sr.ReadLine
                    If Not IsNothing(line) Then
                        lines = Split(line, "£", 2)
                        commandStore.Add(lines(0), lines(1))
                    End If
                End While
            End Using
        End If
    End Sub
    Public Function mapdrives() As Boolean
        setupCommandStore()
        If amOnDomain() Then
            storeGroup()
            buildMyMaps()
            startCookieInternal()
            Return False
        Else
            Threading.Thread.Sleep(2000)
            buildMyMaps()
            startCookieExternal()
            Return True
        End If
    End Function

    Private Sub storeGroup()
        If Not Directory.Exists(appdata & "\Ashby School") Then
            Directory.CreateDirectory(appdata & "\Ashby School")
        End If
        Dim filename As String = appdata & "\Ashby School\ASOneDriveMapperUserGrps.xml"
        Dim userCTX As PrincipalContext = ADTools.getConnection("as.internal", Settings.usersCTXString)
        Dim user As UserPrincipal = ADTools.getUserPrincipalbyUsername(userCTX, Environment.UserName)
        Dim groups = user.GetGroups
        Dim groupnames As New List(Of String)
        For Each grp In groups
            groupnames.Add(grp.Name)
        Next
        Serializer.writeObject(groupnames, filename)
    End Sub

    Private Sub buildMyMaps()
        Dim filename As String = appdata & "\Ashby School\ASOneDriveMapperUserGrps.xml"
        If File.Exists(filename) Then
            Dim groupnames As New List(Of String)
            groupnames = CType(readObject(groupnames, filename), List(Of String))
            For Each grp As String In groupnames
                If commandStore.ContainsKey(grp) Then
                    If Not myMaps.Contains(commandStore(grp)) Then
                        If myMaps.StartsWith("-m") Then
                            myMaps = String.Format("{0},{1}", myMaps, commandStore(grp))
                        Else
                            myMaps = "-m " & commandStore(grp)
                        End If
                    End If
                End If
            Next
        Else
            ' Oh dear, I dont have a mapping file.  FIX ME NOW!
        End If

        Return
    End Sub

    Private Function amOnDomain() As Boolean
        Dim test As DirectoryEntry = Nothing
        Try
            test = New DirectoryEntry("LDAP://RootDSE")
            'Wont throw exception till we check test object
            If test.Name.Equals("RootDSE") Then

            End If
        Catch ex As Exception
            test = Nothing
        End Try

        If IsNothing(test) Then
            CustomLogger.writeEntry(appdata & "\log.txt", "Not On Domain")
            Return False
        Else
            CustomLogger.writeEntry(appdata & "\log.txt", "On Domain")
            Return True
        End If
    End Function

#Region "COOKIES"
    Private Sub startCookieInternal()
        Dim Arguments = String.Format("-s https://ashbyschool-my.sharepoint.com -mount z: -homedir {0}", myMaps)
        Call New Program().getCookie365(Arguments.Split(" "))
        'CustomLogger.writeEntry(appdata & "\log.txt", "Cookie Internal - Appdata Dir " & Environment.GetFolderPath(SpecialFolder.ApplicationData))
    End Sub

    Private Sub startCookieExternal()
        Dim user As String = Environment.UserName.ToLower
        Dim password As String = getPassword()
        If password.Equals("No Password Given") Then
            Return
        End If
        Dim p As New ProcessStartInfo
        Dim username As String = user & "@ashbyschool.org.uk"
        p.FileName = "C:\program files\ashby school\ascookie365.exe"
        p.WindowStyle = ProcessWindowStyle.Hidden
        p.Arguments = String.Format("-s https://ashbyschool-my.sharepoint.com -u {0} -p {1} -mount z: -homedir {2}", username, password, myMaps)
        'CustomLogger.writeEntry(appdata & "\log.txt", "Cookie External- Appdata Dir " & Environment.GetFolderPath(SpecialFolder.ApplicationData))
        'CustomLogger.writeEntry(appdata & "\log.txt", String.Format("External Cookie {0}", p.Arguments))
        Process.Start(p)
    End Sub


    'Private Sub startCookieExternal()
    '    CustomLogger.writeEntry(appdata & "\log.txt", "Cookie External - Appdata Dir " & Environment.GetFolderPath(SpecialFolder.ApplicationData))
    '    Dim user As String = Environment.UserName.ToLower
    '    Dim password As String = getPassword()
    '    If password.Equals("No Password Given") Then
    '        CustomLogger.writeEntry(appdata & "\log.txt", "External Cookie - No Password Given")
    '        Return
    '    End If
    '    Dim Arguments = String.Format("-s https://ashbyschool-my.sharepoint.com -u {0} -p {1} -mount z: -homedir {2}", user, password, myMaps)
    '    Call New Program().getCookie365(Arguments.Split(" "))
    '    CustomLogger.writeEntry(appdata & "\log.txt", String.Format("External Cookie -s https://ashbyschool-my.sharepoint.com -u {0} -p {1} -mount z: -homedir {2}", user, password, myMaps))
    'End Sub
#End Region

#Region "Password"
    Private Function getPassword() As String
        Dim password As String = readPW()
        If password.Equals("NOFILE") Then
            password = passwordRequest()
            storePW(password)
        End If
        Return password
    End Function
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

    Private Function readPW() As String
        Dim wrapper As New Simple3Des("A$h3y $ch00l")
        Dim fl As String = Environment.GetFolderPath(SpecialFolder.ApplicationData) & "\onedrivepw.dat"
        Dim pw As String = ""
        If File.Exists(fl) Then
            Using infile As New StreamReader(fl)
                pw = infile.ReadLine()
            End Using
            pw = wrapper.DecryptData(pw)
        Else
            pw = "NOFILE"
        End If
        Return pw
    End Function

    Private Function passwordRequest() As String
        Dim pwForm As New PasswordRequestForm()
        pwForm.ShowDialog()
        Return pwForm.passwordBox.Text
    End Function
#End Region


End Class
