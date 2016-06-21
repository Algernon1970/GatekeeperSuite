Public Class PrintChooser


    Private Sub PrintChooser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrinterTableAdapter1.FillBySelectable(GatekeeperdbDataSet.Tables("printer"))
    End Sub
End Class