<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintChooser
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PrinterChooserView = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ConnectionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectableDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrinterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GatekeeperdbDataSet = New Gatekeeper_Suite_2016.gatekeeperdbDataSet()
        Me.PrinterTableAdapter1 = New Gatekeeper_Suite_2016.gatekeeperdbDataSetTableAdapters.printerTableAdapter()
        CType(Me.PrinterChooserView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrinterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GatekeeperdbDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PrinterChooserView
        '
        Me.PrinterChooserView.AllowUserToAddRows = False
        Me.PrinterChooserView.AllowUserToDeleteRows = False
        Me.PrinterChooserView.AllowUserToResizeColumns = False
        Me.PrinterChooserView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PrinterChooserView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.PrinterChooserView.AutoGenerateColumns = False
        Me.PrinterChooserView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.PrinterChooserView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PrinterChooserView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.ConnectionDataGridViewTextBoxColumn, Me.SelectableDataGridViewTextBoxColumn})
        Me.PrinterChooserView.DataSource = Me.PrinterBindingSource
        Me.PrinterChooserView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrinterChooserView.Location = New System.Drawing.Point(0, 0)
        Me.PrinterChooserView.Name = "PrinterChooserView"
        Me.PrinterChooserView.ReadOnly = True
        Me.PrinterChooserView.Size = New System.Drawing.Size(284, 262)
        Me.PrinterChooserView.TabIndex = 0
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Visible = False
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Printer"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ConnectionDataGridViewTextBoxColumn
        '
        Me.ConnectionDataGridViewTextBoxColumn.DataPropertyName = "Connection"
        Me.ConnectionDataGridViewTextBoxColumn.HeaderText = "Connection"
        Me.ConnectionDataGridViewTextBoxColumn.Name = "ConnectionDataGridViewTextBoxColumn"
        Me.ConnectionDataGridViewTextBoxColumn.ReadOnly = True
        Me.ConnectionDataGridViewTextBoxColumn.Visible = False
        '
        'SelectableDataGridViewTextBoxColumn
        '
        Me.SelectableDataGridViewTextBoxColumn.DataPropertyName = "Selectable"
        Me.SelectableDataGridViewTextBoxColumn.HeaderText = "Selectable"
        Me.SelectableDataGridViewTextBoxColumn.Name = "SelectableDataGridViewTextBoxColumn"
        Me.SelectableDataGridViewTextBoxColumn.ReadOnly = True
        Me.SelectableDataGridViewTextBoxColumn.Visible = False
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
        'PrinterTableAdapter1
        '
        Me.PrinterTableAdapter1.ClearBeforeFill = True
        '
        'PrintChooser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.PrinterChooserView)
        Me.Name = "PrintChooser"
        Me.Text = "PrintChooser"
        CType(Me.PrinterChooserView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrinterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GatekeeperdbDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PrinterTableAdapter1 As Gatekeeper_Suite_2016.gatekeeperdbDataSetTableAdapters.printerTableAdapter
    Friend WithEvents PrinterChooserView As System.Windows.Forms.DataGridView
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConnectionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectableDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrinterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GatekeeperdbDataSet As Gatekeeper_Suite_2016.gatekeeperdbDataSet
End Class
