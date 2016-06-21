<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrinterListForm
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.CommitButton = New System.Windows.Forms.Button()
        Me.CancelBut = New System.Windows.Forms.Button()
        Me.PrinterList = New System.Windows.Forms.ListBox()
        Me.PrinterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GatekeeperdbDataSet = New Gatekeeper_Manager.gatekeeperdbDataSet()
        Me.PrinterTableAdapter = New Gatekeeper_Manager.gatekeeperdbDataSetTableAdapters.printerTableAdapter()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.PrinterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GatekeeperdbDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.PrinterList, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(487, 426)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.CommitButton, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CancelBut, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(407, 371)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(77, 52)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'CommitButton
        '
        Me.CommitButton.Location = New System.Drawing.Point(3, 3)
        Me.CommitButton.Name = "CommitButton"
        Me.CommitButton.Size = New System.Drawing.Size(75, 20)
        Me.CommitButton.TabIndex = 0
        Me.CommitButton.Text = "Commit"
        Me.CommitButton.UseVisualStyleBackColor = True
        '
        'CancelBut
        '
        Me.CancelBut.Location = New System.Drawing.Point(3, 29)
        Me.CancelBut.Name = "CancelBut"
        Me.CancelBut.Size = New System.Drawing.Size(75, 20)
        Me.CancelBut.TabIndex = 1
        Me.CancelBut.Text = "Cancel"
        Me.CancelBut.UseVisualStyleBackColor = True
        '
        'PrinterList
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.PrinterList, 2)
        Me.PrinterList.DataSource = Me.PrinterBindingSource
        Me.PrinterList.DisplayMember = "Name"
        Me.PrinterList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrinterList.FormattingEnabled = True
        Me.PrinterList.Location = New System.Drawing.Point(3, 3)
        Me.PrinterList.Name = "PrinterList"
        Me.PrinterList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.PrinterList.Size = New System.Drawing.Size(481, 362)
        Me.PrinterList.TabIndex = 1
        '
        'PrinterBindingSource
        '
        Me.PrinterBindingSource.DataMember = "printer"
        Me.PrinterBindingSource.DataSource = Me.GatekeeperdbDataSet
        '
        'GatekeeperdbDataSet
        '
        Me.GatekeeperdbDataSet.DataSetName = "gatekeeperdbDataSet"
        Me.GatekeeperdbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PrinterTableAdapter
        '
        Me.PrinterTableAdapter.ClearBeforeFill = True
        '
        'PrinterListForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 426)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "PrinterListForm"
        Me.Text = "PrinterListForm"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.PrinterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GatekeeperdbDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents CommitButton As Button
    Friend WithEvents CancelBut As Button
    Friend WithEvents PrinterList As ListBox
    Friend WithEvents GatekeeperdbDataSet As gatekeeperdbDataSet
    Friend WithEvents PrinterBindingSource As BindingSource
    Friend WithEvents PrinterTableAdapter As gatekeeperdbDataSetTableAdapters.printerTableAdapter
End Class
