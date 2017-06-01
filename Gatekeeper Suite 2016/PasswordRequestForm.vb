Public Class PasswordRequestForm
    Private Sub PasswordRequestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As Integer = 0
        Dim y As Integer = 0
        x = MousePosition.X - Me.Width
        y = Screen.GetBounds(MousePosition).Height - (Me.Height + 60)
        Me.Location = New Point(x, y)
        Me.Refresh()
    End Sub
End Class