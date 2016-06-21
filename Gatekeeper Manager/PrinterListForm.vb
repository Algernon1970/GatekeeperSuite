Public Class PrinterListForm
    Private Sub PrinterListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GatekeeperdbDataSet.printer' table. You can move, or remove it, as needed.
        Me.PrinterTableAdapter.Fill(Me.GatekeeperdbDataSet.printer)

    End Sub

    Private Sub CommitButton_Click(sender As Object, e As EventArgs) Handles CommitButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelBut.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class