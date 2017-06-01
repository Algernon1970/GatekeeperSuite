Public Class Form1
    Dim lf As New ListForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        lf.addObj(New printObj(1, "test1"))
        lf.addObj(New printObj(2, "test2"))
        lf.addObj(New printObj(3, "test3"))
        lf.addObj(New printObj(4, "test4"))
        lf.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim plist As List(Of printObj) = lf.getCheckedPrinters
        For Each printer As printObj In plist
            If printer.flag Then
                MsgBox(String.Format("Got a checked/flaged one {0}:{1}", printer.number, printer.name))
            End If
        Next
    End Sub
End Class