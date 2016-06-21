Public Class ListForm

    Dim pList As New List(Of printObj)

    Public Sub addObj(ByVal printObj As printObj)
        pList.Add(printObj)
        CheckedListBox1.Items.Add(printObj)
    End Sub

    Public Function getCheckedPrinters() As List(Of printObj)
        Dim cpList As New List(Of printObj)
        For Each pitem As printObj In CheckedListBox1.CheckedItems
            cpList.Add(pitem)
        Next
        Return cpList
    End Function

    Private Sub CheckedListBox1_DoubleClick(sender As Object, e As EventArgs) Handles CheckedListBox1.DoubleClick
        If CheckedListBox1.SelectedItem.flag = True Then
            CheckedListBox1.SelectedItem.flag = False
        Else
            For Each pitem As printObj In CheckedListBox1.Items
                pitem.flag = False
            Next
            CheckedListBox1.SelectedItem.flag = True
        End If
        CheckedListBox1.SetItemChecked(CheckedListBox1.SelectedIndex, True)
        CheckedListBox1.Refresh()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckedListBox1.SelectedItem.flag = True Then
            CheckedListBox1.SelectedItem.flag = False
        Else
            For Each pitem As printObj In CheckedListBox1.Items
                pitem.flag = False
            Next
            CheckedListBox1.SelectedItem.flag = True
        End If
        CheckedListBox1.SetItemChecked(CheckedListBox1.SelectedIndex, True)
        CheckedListBox1.Refresh()
    End Sub
End Class

Public Class printObj
    Public name As String
    Public number As Integer
    Public flag As Boolean

    Public Overrides Function toString() As String
        If flag Then
            Return name & " (Default)"
        End If
        Return name
    End Function

    Public Sub New(ByVal id As Integer, named As String)
        name = named
        number = id
    End Sub
End Class