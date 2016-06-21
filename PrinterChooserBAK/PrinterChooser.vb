Public Class PrinterChooser

    Private Sub PrinterChooser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GatekeeperdbDataSet.printer' table. You can move, or remove it, as needed.
        Me.PrinterTableAdapter.Fillby(Me.GatekeeperdbDataSet.printer)

    End Sub
End Class