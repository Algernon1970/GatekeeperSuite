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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.CancelBut = New System.Windows.Forms.Button()
        Me.AcceptBut = New System.Windows.Forms.Button()
        Me.PrinterListBox = New System.Windows.Forms.CheckedListBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.PrinterListBox, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.28493!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.715064!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(234, 462)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.CancelBut, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.AcceptBut, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 433)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(228, 26)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'CancelBut
        '
        Me.CancelBut.Dock = System.Windows.Forms.DockStyle.Right
        Me.CancelBut.Location = New System.Drawing.Point(36, 3)
        Me.CancelBut.Name = "CancelBut"
        Me.CancelBut.Size = New System.Drawing.Size(75, 20)
        Me.CancelBut.TabIndex = 0
        Me.CancelBut.Text = "Cancel"
        Me.CancelBut.UseVisualStyleBackColor = True
        '
        'AcceptBut
        '
        Me.AcceptBut.Dock = System.Windows.Forms.DockStyle.Left
        Me.AcceptBut.Location = New System.Drawing.Point(117, 3)
        Me.AcceptBut.Name = "AcceptBut"
        Me.AcceptBut.Size = New System.Drawing.Size(75, 20)
        Me.AcceptBut.TabIndex = 1
        Me.AcceptBut.Text = "Accept"
        Me.AcceptBut.UseVisualStyleBackColor = True
        '
        'PrinterListBox
        '
        Me.PrinterListBox.CheckOnClick = True
        Me.PrinterListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrinterListBox.FormattingEnabled = True
        Me.PrinterListBox.Location = New System.Drawing.Point(3, 3)
        Me.PrinterListBox.Name = "PrinterListBox"
        Me.PrinterListBox.Size = New System.Drawing.Size(228, 424)
        Me.PrinterListBox.TabIndex = 1
        '
        'PrinterChooser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 462)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "PrinterChooser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Add Printers"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents CancelBut As Button
    Friend WithEvents AcceptBut As Button
    Friend WithEvents PrinterListBox As CheckedListBox
End Class
