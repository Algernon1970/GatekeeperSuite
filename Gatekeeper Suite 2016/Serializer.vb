Imports System.IO
Imports System.Xml.Serialization

Module Serializer
    Public Sub writeObject(ByVal obj As Object, ByVal path As String)
        Try
            Dim objWriter As New StreamWriter(path)
            Dim myserializer As New XmlSerializer(obj.GetType)
            myserializer.Serialize(objWriter, obj)
            objWriter.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function readObject(ByVal obj As Object, ByVal path As String) As Object
        Dim objReader As New StreamReader(path)
        Dim serializer As New XmlSerializer(obj.GetType)
        Dim omyObj As Object = serializer.Deserialize(objReader)
        objReader.Close()
        Return omyObj
    End Function

    Public Sub writeFile(ByRef str As String, ByRef fp As String)
        Using sw As StreamWriter = File.AppendText(fp)
            sw.WriteLine(str)
        End Using
    End Sub


End Module
