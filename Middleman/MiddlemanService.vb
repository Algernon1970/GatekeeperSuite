Imports System.ServiceProcess
Imports System.IO
Imports System.Text

Public Class MiddlemanService
    Dim webServer As New Webserver(Me)

    Protected Overrides Sub OnStart(ByVal args() As String)
        webServer.startServer()
    End Sub

    Protected Overrides Sub OnStop()
        webServer.stopServer()
    End Sub

#Region "Callbacks"
    Public Function getLoginRTF() As MemoryStream
        Dim foundrows() As gatekeeperdbDataSet.infotableRow
        foundrows = GatekeeperdbDataSet1.Tables("InfoTable").Select("type like 'Login'")
        For Each row As DataRow In foundrows
            Dim ms As New MemoryStream()
            ms.Write(row.Field(Of Byte())("info"), 0, row.Field(Of Byte())("info").Length)
            ms.Seek(0, SeekOrigin.Begin)
            Return (ms)
        Next
        Return New MemoryStream(Encoding.UTF8.GetBytes("Not Found"))
    End Function
#End Region

End Class
