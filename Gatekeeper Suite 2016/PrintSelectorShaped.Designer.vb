<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintSelectorShaped
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
        Me.MakeDefaultButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PrinterListBox
        '
        Me.PrinterListBox.BackColor = System.Drawing.Color.LightGray
        Me.PrinterListBox.FormattingEnabled = True
        Me.PrinterListBox.Location = New System.Drawing.Point(68, 119)
        Me.PrinterListBox.Name = "PrinterListBox"
        Me.PrinterListBox.Size = New System.Drawing.Size(201, 334)
        Me.PrinterListBox.TabIndex = 0
        '
        'MakeDefaultButton
        '
        Me.MakeDefaultButton.Location = New System.Drawing.Point(275, 406)
        Me.MakeDefaultButton.Name = "MakeDefaultButton"
        Me.MakeDefaultButton.Size = New System.Drawing.Size(75, 23)
        Me.MakeDefaultButton.TabIndex = 1
        Me.MakeDefaultButton.Text = "Default"
        Me.MakeDefaultButton.UseVisualStyleBackColor = True
        '
        'PrintSelectorShaped
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Gatekeeper_Suite_2016.My.Resources.Resources.PrintboxWhiteCopy
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(373, 603)
        Me.ControlBox = False
        Me.Controls.Add(Me.MakeDefaultButton)
        Me.Controls.Add(Me.PrinterListBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PrintSelectorShaped"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PrintSelectorShaped"
        Me.TransparencyKey = System.Drawing.Color.White
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PrinterListBox As CheckedListBox
    Friend WithEvents MakeDefaultButton As Button
End Class
