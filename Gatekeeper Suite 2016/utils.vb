Imports System
Imports System.Security.Principal
Imports System.Drawing.Printing
Imports System.DirectoryServices

Module Utils
    'GATEKEEPER VERSION

    Declare Function AddPrinterConnection Lib "winspool.drv" Alias "AddPrinterConnectionA" (ByVal pName As String) As Long
    Declare Function DeletePrinterConnection Lib "winspool.drv" Alias "DeletePrinterConnectionA" (ByVal pName As String) As Long
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Declare Function GetDefaultPrinter Lib "winspool.drv" Alias "GetDefaultPrinterA" (ByVal pszBuffer() As String, ByVal pcchBuffer As Integer) As Boolean

    ''' <summary>
    ''' Gets the user sid.
    ''' </summary>
    ''' <param name="strUsername">The STR username.</param>
    ''' <returns>a string representing the SID of the user</returns>
    Public Function GetUserSid(ByVal strUsername As String) As String
        Try
            Dim id As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent()
            Dim sid As String = id.User.ToString
            Return sid
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    Public Function IsInGroup(PCName As String, groupName As String) As Boolean
        Dim vUsuario As New NTAccount(PCName & "$")
        Dim sid As SecurityIdentifier = vUsuario.Translate(GetType(SecurityIdentifier))

        Using vRootDSE As New DirectoryEntry("LDAP://rootDSE")
            Using vSearcher As New DirectorySearcher(New DirectoryEntry("LDAP://" + CStr(vRootDSE.Properties("defaultNamingContext")(0))), "(objectSID=" & sid.ToString() & ")", New String() {"memberOf"}, SearchScope.Subtree)
                Dim src As SearchResultCollection = vSearcher.FindAll()

                Dim memberOf As ResultPropertyValueCollection = src(0).Properties("memberOf")
                For i As Integer = 0 To memberOf.Count - 1
                    'Debug.Print(memberOf(i).ToString())
                    If memberOf(i).ToString().Contains("=" & groupName & ",") Then
                        Return True
                    End If
                Next

            End Using

        End Using

        Return False
    End Function

#Region "printers"
    Public Sub addPrinter(ByRef printer As printerInfo)
        AddPrinterConnection(printer.connection)
        'If printer.isDefault Then
        '    SetDefaultPrinter(printer.connection)
        'End If
    End Sub

    Public Sub delPrinter(ByRef printer As printerInfo)
        DeletePrinterConnection(printer.connection)
    End Sub

    Public Sub deleteAllNetworkPrinters()
        For Each printerConnection As String In PrinterSettings.InstalledPrinters
            If printerConnection.StartsWith("\\") Then
                DeletePrinterConnection(printerConnection)
            End If
        Next
    End Sub

    Public Sub setLocalDefault()
        For Each printerConnection As String In PrinterSettings.InstalledPrinters
            If Not printerConnection.StartsWith("\\") Then
                SetDefaultPrinter(printerConnection)
            End If
        Next
    End Sub
#End Region

    Public Function plistToString(ByVal plist As List(Of printerInfo)) As String
        Dim paramString As String
        Dim def As String
        If plist.Count > 0 Then
            'If plist.ElementAt(0).isDefault Then
            '    paramString = "&plist=^" & plist.ElementAt(0).name
            'Else
            '    paramString = "&plist=$" & plist.ElementAt(0).name
            'End If
            paramString = "&plist="
            For Each printer In plist
                If Not paramString.Contains(printer.name) Then
                    If printer.isDefault Then
                        def = "^"
                    Else
                        def = "$"
                    End If
                    If printer.isSelected Then
                        If paramString.EndsWith("=") Then
                            paramString = String.Format("{0}{1}{2}", paramString, def, printer.name)
                        Else
                            paramString = String.Format("{0},{1}{2}", paramString, def, printer.name)
                        End If
                    End If
                End If
            Next
            Return paramString
        End If
        Return ""

    End Function

End Module