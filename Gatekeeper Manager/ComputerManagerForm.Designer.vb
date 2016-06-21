<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ComputerManagerForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GatekeeperdbDataSet1 = New Gatekeeper_Manager.gatekeeperdbDataSet()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.NodeContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CompList = New System.Windows.Forms.ListBox()
        Me.AddPrinterToDBButton = New System.Windows.Forms.Button()
        CType(Me.GatekeeperdbDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NodeContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'GatekeeperdbDataSet1
        '
        Me.GatekeeperdbDataSet1.DataSetName = "gatekeeperdbDataSet"
        Me.GatekeeperdbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TreeView1
        '
        Me.TreeView1.ContextMenuStrip = Me.NodeContextMenu
        Me.TreeView1.Location = New System.Drawing.Point(12, 12)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(310, 373)
        Me.TreeView1.TabIndex = 0
        '
        'NodeContextMenu
        '
        Me.NodeContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.NodeContextMenu.Name = "NodeContextMenu"
        Me.NodeContextMenu.Size = New System.Drawing.Size(87, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(86, 22)
        Me.ToolStripMenuItem1.Text = "...."
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(734, 362)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(114, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "PopulateComputers"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CompList
        '
        Me.CompList.FormattingEnabled = True
        Me.CompList.Location = New System.Drawing.Point(328, 12)
        Me.CompList.Name = "CompList"
        Me.CompList.Size = New System.Drawing.Size(349, 381)
        Me.CompList.TabIndex = 2
        '
        'AddPrinterToDBButton
        '
        Me.AddPrinterToDBButton.Location = New System.Drawing.Point(739, 333)
        Me.AddPrinterToDBButton.Name = "AddPrinterToDBButton"
        Me.AddPrinterToDBButton.Size = New System.Drawing.Size(75, 23)
        Me.AddPrinterToDBButton.TabIndex = 3
        Me.AddPrinterToDBButton.Text = "Add Printer"
        Me.AddPrinterToDBButton.UseVisualStyleBackColor = True
        '
        'ComputerManagerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 397)
        Me.Controls.Add(Me.AddPrinterToDBButton)
        Me.Controls.Add(Me.CompList)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "ComputerManagerForm"
        Me.Text = "Gatekeeper Computer Manager"
        CType(Me.GatekeeperdbDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NodeContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GatekeeperdbDataSet1 As gatekeeperdbDataSet
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents NodeContextMenu As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Button1 As Button
    Friend WithEvents CompList As ListBox
    Friend WithEvents AddPrinterToDBButton As Button
End Class
