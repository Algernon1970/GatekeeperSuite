Public Class AddPrinterForm
    Public pc As New printerConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        pc.friendlyName = friendlyNameBox.Text
        pc.connectionString = connectionStringBox.Text
        pc.selectable = SelectableCheck.CheckState

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class

Public Class printerConnection
    Public friendlyName As String
    Public connectionString As String
    Public selectable As Boolean
End Class