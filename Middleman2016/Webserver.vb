Imports System
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.ComponentModel

Public Class Webserver
    Const listenerPrefix As String = "http://*:6510/"
    Dim listener As HttpListener
    Dim middleman As Middleman
    Dim watchdog As New BackgroundWorker

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
        If cmd.Equals("CHECKONLINE") Then
            formattedResponse = middleman.checkOnline
        ElseIf cmd.Equals("CHECKPRIV") Then
            formattedResponse = middleman.amIPriv
        ElseIf cmd.Equals("GETLOGINRTF") Then
            formattedResponse = middleman.getLoginRTF
        ElseIf cmd.Equals("GETPRIVFILE") Then
            formattedResponse = middleman.getPrivFile()
        ElseIf cmd.Equals("USER") Then
            middleman.setUser(raw.QueryString("username"))
        ElseIf cmd.Equals("FORCELOGOUT") Then
            middleman.forcelogout()
        ElseIf cmd.Equals("WHOAMI") Then
            formattedResponse = middleman.getMyComputer()
        ElseIf cmd.Equals("GETMYPRINTERS") Then
            formattedResponse = middleman.getMyPrinters
        ElseIf cmd.Equals("SETSID") Then
            middleman.setSid(raw.QueryString("sid"))
        ElseIf cmd.Equals("REG") Then
            formattedResponse = middleman.registry(url, raw)
        ElseIf cmd.Equals("PROCESSPRIV") Then
            formattedResponse = middleman.processPriv()
        ElseIf cmd.Equals("GETVERSION") Then
            formattedResponse = middleman.getversion()
        ElseIf cmd.Equals("RECORDLOGIN") Then
            formattedResponse = middleman.recordLogin()
        ElseIf cmd.Equals("GETUSERID") Then
            formattedResponse = middleman.getUserID()
        ElseIf cmd.Equals("LATCHGATEKEEPER") Then
            formattedResponse = middleman.latchGateKeeper()
        ElseIf cmd.Equals("UNLATCHGATEKEEPER") Then
            formattedResponse = middleman.unlatchGateKeeper()
        ElseIf cmd.Equals("RUN") Then
            formattedResponse = middleman.launchExternal(raw.QueryString("cmdline"))
        ElseIf cmd.Equals("LOCK") Then
            formattedResponse = middleman.lockWorkstation()
        ElseIf cmd.Equals("MSG") Then
            formattedResponse = middleman.messageUser(raw.QueryString("message"))
        ElseIf cmd.Equals("MAPMYPRINTERS") Then
            formattedResponse = middleman.mapMyPrinters()
        ElseIf cmd.Equals("CHECKMOBILE") Then
            formattedResponse = middleman.checkMobile()
        ElseIf cmd.Equals("GETALLPRINTERS") Then
            formattedResponse = middleman.getAllPrinters
            'ElseIf cmd.Equals("MAPTHESEPRINTERS") Then
            '    formattedResponse = middleman.mapThesePrinters(raw.QueryString("plist"))
        ElseIf cmd.Equals("SETAPPDATA") Then
            formattedResponse = middleman.setAppData()
        ElseIf cmd.Equals("GETUSERPRINTERS") Then
            formattedResponse = middleman.getUserPrinters()
        ElseIf cmd.Equals("MTREDIRECT") Then
            formattedResponse = middleman.mtRedirect(True)
        ElseIf cmd.Equals("NOMTREDIRECT") Then
            formattedResponse = middleman.mtRedirect(False)
        ElseIf cmd.Equals("SETUSERPRINTERS") Then
            formattedResponse = middleman.setUserPrinters(raw.QueryString("plist"))
        End If

        Return formattedResponse
    End Function

End Class