Imports System.ComponentModel
Imports System.Runtime.InteropServices
Public Class UserUtilities
    <DllImport("user32.dll", SetLastError:=True)> Private Shared Function LockWorkStation() As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    'Dim eSource As String = "UserUtilities"
    'Dim eLog As String = "GatekeeperSuite"
    'Dim eSourceData As EventSourceCreationData = New EventSourceCreationData(eSource, eLog)

    Private Sub UserUtilities_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If Not EventLog.SourceExists(eSource, ".") Then
        '    EventLog.CreateEventSource(eSourceData)
        'End If
        '  Dim eventlog1 As New EventLog("GatekeeperSuite", ".", "UserUtilities")
        ' Utils.setLog(eventlog1)
        For Each arg As String In My.Application.CommandLineArgs
            If arg.Equals(My.Resources.LOCK) Then
                LockWorkStation()
            ElseIf arg.StartsWith(My.Resources.MSG) Then
                Dim userbox As New MessageForm
                userbox.UserMessageBox.Text = arg.Substring(10)
                userbox.ShowDialog()
            ElseIf arg.StartsWith(My.Resources.GPUPDATE) Then
                gpupdate()
            ElseIf arg.StartsWith(My.Resources.PRINTER) Then
                Dim pinfo As New printerInfo
                Dim parg() As String = arg.Substring(10).Split(",")
                pinfo.name = parg(0)
                pinfo.connection = parg(1)
                If parg(2).Equals(My.Resources.PRINTERDEFAULT) Then
                    pinfo.isDefault = True
                Else
                    pinfo.isDefault = False
                End If
                'EventLog1.WriteEntry(String.Format("Printer {0}({1})", pinfo.name, pinfo.isDefault))
                addPrinter(pinfo)

            End If
        Next
        Application.Exit()
    End Sub

    Private Sub gpupdate()
        Try
            Dim refresher As Process = New Process()
            refresher.StartInfo.Arguments = "/target:user /wait:-1"
            refresher.StartInfo.FileName = "c:\windows\system32\gpupdate.exe"
            refresher.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            refresher.Start()
            refresher.WaitForExit()
        Catch ex As Exception

        End Try
    End Sub
End Class