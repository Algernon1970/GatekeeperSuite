<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrinterChooser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PrinterListBox = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout()
        '
        'PrinterListBox
        '
        Me.PrinterListBox.CheckOnClick = True
        Me.PrinterListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrinterListBox.FormattingEnabled = True
        Me.PrinterListBox.Location = New System.Drawing.Point(0, 0)
        Me.PrinterListBox.Name = "PrinterListBox"
        Me.PrinterListBox.Size = New System.Drawing.Size(149, 458)
        Me.PrinterListBox.TabIndex = 1
        '
        'PrinterChooser
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(149, 458)
        Me.ControlBox = False
        Me.Controls.Add(Me.PrinterListBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "PrinterChooser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Add Printers"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PrinterListBox As CheckedListBox
End Class
