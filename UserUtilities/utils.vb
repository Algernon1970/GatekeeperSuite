Imports System
Imports System.Diagnostics
Imports System.Security.Principal
Imports System.Drawing.Printing

Module Utils
    'GATEKEEPER VERSION
    Dim eventlog1 As EventLog
    Declare Function AddPrinterConnection Lib "winspool.drv" Alias "AddPrinterConnectionA" (ByVal pName As String) As Integer
    Declare Function DeletePrinterConnection Lib "winspool.drv" Alias "DeletePrinterConnectionA" (ByVal pName As String) As Long
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Declare Function GetDefaultPrinter Lib "winspool.drv" Alias "GetDefaultPrinterA" (ByVal pszBuffer() As String, ByVal pcchBuffer As Integer) As Boolean

    Dim retrycounter As Integer = 5
    ''' <summary>
    ''' Gets the user sid.
    ''' </summary>
    ''' <param name="strUsername">The STR username.</param>
    ''' <returns>a string representing the SID of the user</returns>
    Public Function GetUserSid(ByVal strUsername As String) As String
        Try
            Dim id As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim sid As String = id.User.ToString
            Return sid
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    'Public Sub setLog(log As EventLog)
    '    eventlog1 = log
    'End Sub

#Region "printers"
    Public Sub addPrinter(ByRef printer As printerInfo)
        Dim ret As Integer = 0
        ret = AddPrinterConnection(printer.connection)
        ' If eventlog1 IsNot Nothing Then
        If ret = 0 Then
                Threading.Thread.Sleep(10000)
                'eventlog1.WriteEntry(String.Format("Couldnt map printer {0} error {1}", printer.connection, ret))
                retrycounter = retrycounter - 1
                If retrycounter > 0 Then
                    addPrinter(printer)
                Else
                    MsgBox("Failed to map printer " & printer.connection)
                    ' eventlog1.WriteEntry(String.Format("Mapped printer {0} final error {1}", printer.connection, ret))
                End If
            Else
                ' eventlog1.WriteEntry(String.Format("Mapped printer {0} error {1}", printer.connection, ret))
                retrycounter = 5
            End If
        '  End If

        If printer.isDefault Then
            SetDefaultPrinter(printer.connection)
        End If

    End Sub

    Public Sub delPrinter(ByRef printer As printerInfo)
        DeletePrinterConnection(printer.connection)
        'If eventlog1 IsNot Nothing Then
        '    eventlog1.WriteEntry(String.Format("Deleteing printer {0}", printer.connection))
        'End If

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

End Module

Public Structure printerInfo
    Dim name As String
    Dim connection As String
    Dim isDefault As Boolean
End Structure

