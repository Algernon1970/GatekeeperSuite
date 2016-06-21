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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GatekeeperdbDataSet = New Global.PrinterChooser.gatekeeperdbDataSet()
        Me.PrinterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PrinterTableAdapter = New gatekeeperdbDataSetTableAdapters.printerTableAdapter()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ConnectionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectableDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.GatekeeperdbDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrinterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GatekeeperdbDataSet
        '
        Me.GatekeeperdbDataSet.DataSetName = "gatekeeperdbDataSet"
        Me.GatekeeperdbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PrinterBindingSource
        '
        Me.PrinterBindingSource.DataMember = "printer"
        Me.PrinterBindingSource.DataSource = Me.GatekeeperdbDataSet
        '
        'PrinterTableAdapter
        '
        Me.PrinterTableAdapter.ClearBeforeFill = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ColumnHeadersVisible = False
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.ConnectionDataGridViewTextBoxColumn, Me.SelectableDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.PrinterBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(284, 262)
        Me.DataGridView1.TabIndex = 0
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
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
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
        'PrinterChooser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "PrinterChooser"
        Me.Text = "PrinterChooser"
        CType(Me.GatekeeperdbDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrinterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PrinterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConnectionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectableDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GatekeeperdbDataSet As Global.PrinterChooser.gatekeeperdbDataSet
    Friend WithEvents PrinterTableAdapter As gatekeeperdbDataSetTableAdapters.printerTableAdapter
End Class
