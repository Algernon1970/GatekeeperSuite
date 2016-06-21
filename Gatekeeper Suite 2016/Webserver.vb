Imports System
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.ComponentModel

Public Class Webserver
    Const listenerPrefix As String = "http://*:6502/"
    Dim listener As HttpListener
    Dim watchdog As New BackgroundWorker


    Public Sub New()

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
        If Not listener.IsListening Then
            listener.BeginGetContext(AddressOf requestWait, Nothing)
        End If
        Dim formattedResponse As String = ""
        Dim c = listener.EndGetContext(ar)
        listener.BeginGetContext(AddressOf requestWait, Nothing)
        Dim url = (c.Request.RawUrl)
        'If Not c.Request.Headers("x-ashbyauth") Is Nothing Then

        Dim ms As MemoryStream = handleCommands(url, c.Request)

        ms.Position = 0
        Dim sr As New StreamReader(ms)
        formattedResponse = sr.ReadToEnd()
        'Else
        '    formattedResponse = "Remote Access Not Allowed"
        'End If

        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(formattedResponse)
        Dim response = c.Response
        response.ContentLength64 = buffer.Length
        Dim output As System.IO.Stream = response.OutputStream
        output.Write(buffer, 0, buffer.Length)
        output.Flush()
        output.Close()
    End Sub

    Private Function handleCommands(ByVal url As String, ByRef raw As HttpListenerRequest) As MemoryStream
        Dim formattedResponse As New MemoryStream
        Dim cmd As String = raw.QueryString("command")

        Return formattedResponse
    End Function



End Class

