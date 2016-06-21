Public Class PrinterChooser

    Public Sub preselect(ByVal pname As String)
        If PrinterListBox.Items.Contains(pname) Then
            PrinterListBox.SetItemChecked(PrinterListBox.Items.IndexOf(pname), True)
        End If
    End Sub

    Public Sub preselect(ByRef plist As List(Of printerInfo))
        For Each printer As printerInfo In plist
            If PrinterListBox.Items.Contains(printer.name) Then
                PrinterListBox.SetItemChecked(PrinterListBox.Items.IndexOf(printer.name), True)
            End If
        Next
    End Sub

    Public Function getSelectedPrinters() As List(Of String)
        Dim plist As New List(Of String)
        For Each item In PrinterListBox.CheckedItems
            plist.Add(item.ToString)
        Next
        Return plist
    End Function

    Public Sub displayPrinters(ByVal plist As List(Of printerInfo))
        For Each printer As printerInfo In plist
            If printer.isSelectable Then
                PrinterListBox.Items.Add(printer.name)
            End If
        Next
    End Sub

End Class

Public Structure printerInfo
    Dim name As String
    Dim connection As String
    Dim isSelectable As Boolean
End Structure