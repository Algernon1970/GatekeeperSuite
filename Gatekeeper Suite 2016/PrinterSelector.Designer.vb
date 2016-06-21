<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrinterSelector
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PrinterListBox = New System.Windows.Forms.CheckedListBox()
        Me.MakeDefaultButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PrinterListBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.MakeDefaultButton, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(153, 462)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'PrinterListBox
        '
        Me.PrinterListBox.CheckOnClick = True
        Me.PrinterListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrinterListBox.FormattingEnabled = True
        Me.PrinterListBox.Location = New System.Drawing.Point(3, 3)
        Me.PrinterListBox.Name = "PrinterListBox"
        Me.PrinterListBox.Size = New System.Drawing.Size(147, 421)
        Me.PrinterListBox.Sorted = True
        Me.PrinterListBox.TabIndex = 0
        '
        'MakeDefaultButton
        '
        Me.MakeDefaultButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.MakeDefaultButton.Location = New System.Drawing.Point(62, 430)
        Me.MakeDefaultButton.Name = "MakeDefaultButton"
        Me.MakeDefaultButton.Size = New System.Drawing.Size(88, 29)
        Me.MakeDefaultButton.TabIndex = 1
        Me.MakeDefaultButton.Text = "Make Default"
        Me.MakeDefaultButton.UseVisualStyleBackColor = True
        '
        'PrinterSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(153, 462)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PrinterSelector"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PrinterSelector"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents PrinterListBox As CheckedListBox
    Friend WithEvents MakeDefaultButton As Button
End Class
