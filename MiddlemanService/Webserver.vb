Imports System
Imports System.Net
Imports System.Text
Imports System.IO

Public Class Webserver
    Const listenerPrefix As String = "http://*:6510/"
    Dim listener As HttpListener
    Dim middleman As Middleman

    Public Sub New(ByRef _caller As Middleman)
        middleman = _caller
    End Sub

    Public Sub startServer()
        listener = New HttpListener
        listener.Prefixes.Add(listenerPrefix)
        listener.Start()
        listener.BeginGetContext(AddressOf requestWait, Nothing)
    End Sub

    Public Sub stopServer()
        listener.Stop()
    End Sub

    Private Sub requestWait(ByVal ar As IAsyncResult)
        If Not Listener.IsListening Then
            Return
        End If
        Dim c = Listener.EndGetContext(ar)
        Listener.BeginGetContext(AddressOf requestWait, Nothing)
        Dim url = (c.Request.RawUrl)

        Dim ms As MemoryStream = handleCommands(url)

        ms.Position = 0
        Dim sr As New StreamReader(ms)
        Dim formattedResponse As String = sr.ReadToEnd()


        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(formattedResponse)
        Dim response = c.Response
        response.ContentLength64 = buffer.Length
        Dim output As System.IO.Stream = response.OutputStream
        output.Write(buffer, 0, buffer.Length)
    End Sub

    Private Function handleCommands(ByVal url As String) As MemoryStream
        Dim formattedResponse As New MemoryStream
        If url.Contains("GETLOGINRTF") Then
            formattedResponse = middleman.getLoginRTF
        End If

            Return formattedResponse
    End Function

End Class
