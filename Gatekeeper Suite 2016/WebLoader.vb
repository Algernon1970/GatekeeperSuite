Imports System.IO
Imports System.Net
Imports System.Text

Module WebLoader
    Dim browser As New WebClient

    Public Function loadAsMS(ByVal url As String) As MemoryStream
        browser.Headers.Add("x-ashbyauth", "ASHBY")
        Dim response() As Byte = browser.DownloadData(url)
        Return New MemoryStream(response)
    End Function

    Public Function loadAsString(ByVal url As String) As String
        browser.Headers.Add("x-ashbyauth", "ASHBY")
        Return browser.DownloadString(url)
    End Function

    Public Function msToString(ByRef ms As MemoryStream) As String
        ms.Position = 0
        Dim sr As New StreamReader(ms)
        Return sr.ReadToEnd
    End Function

    Public Function stringtoMS(ByRef str As String) As MemoryStream
        Return New MemoryStream(Encoding.UTF8.GetBytes(str))
    End Function

End Module