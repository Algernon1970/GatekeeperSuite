Public Class PrintSelectorShaped

    Dim pList As New List(Of printerInfo)

    Public Sub displayPrinters(ByRef plist As List(Of printerInfo))
        For Each printerObj As printerInfo In plist
            If printerObj.isSelectable Then
                addObj(printerObj)
            End If

        Next
    End Sub

    Private Sub MakeDefaultButton_Click(sender As Object, e As EventArgs) Handles MakeDefaultButton.Click
        If PrinterListBox.SelectedItem.isDefault = True Then
            PrinterListBox.SelectedItem.isDefault = False
        Else
            For Each pitem As printerInfo In PrinterListBox.Items
                pitem.isDefault = False
            Next
            PrinterListBox.SelectedItem.isDefault = True
        End If
        PrinterListBox.SetItemChecked(PrinterListBox.SelectedIndex, True)
        PrinterListBox.Refresh()
    End Sub

    Public Sub addObj(ByVal printObj As printerInfo)
        pList.Add(printObj)
        PrinterListBox.Items.Add(printObj, printObj.isSelected)
    End Sub

    Private Sub PrinterListBox_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PrinterListBox.DoubleClick
        If PrinterListBox.SelectedItem.isdefault = True Then
            PrinterListBox.SelectedItem.isdefault = False
        Else
            For Each pitem As printerInfo In PrinterListBox.Items
                pitem.isDefault = False
            Next
            PrinterListBox.SelectedItem.isDefault = True
        End If
        PrinterListBox.SetItemChecked(PrinterListBox.SelectedIndex, True)
        PrinterListBox.Refresh()
    End Sub

    Public Function getSelectedPrinters() As List(Of printerInfo)
        Dim cpList As New List(Of printerInfo)
        For Each pitem As printerInfo In PrinterListBox.Items
            If PrinterListBox.GetItemChecked(PrinterListBox.Items.IndexOf(pitem)) Then
                pitem.isSelected = True
            Else
                pitem.isSelected = False
            End If
            cpList.Add(pitem)
        Next
        Return cpList
    End Function
    Private Sub PrinterListBox_MouseMove(sender As Object, e As MouseEventArgs) Handles PrinterListBox.MouseMove
        PrinterListBox.SelectedIndex = PrinterListBox.IndexFromPoint(e.X, e.Y)
    End Sub

    'Private Sub PrinterListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles PrinterListBox.ItemCheck
    '    If PrinterListBox.SelectedItem.isdefault = True Then
    '        PrinterListBox.SelectedItem.isdefault = False
    '    Else
    '        For Each pitem As printerInfo In PrinterListBox.Items
    '            pitem.isDefault = False
    '        Next
    '        PrinterListBox.SelectedItem.isDefault = True
    '    End If
    'End Sub
End Class