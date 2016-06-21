<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GatekeeperForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GatekeeperForm))
        Me.BaseLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.BrandingPanel = New System.Windows.Forms.Panel()
        Me.LoginInfoBox = New System.Windows.Forms.RichTextBox()
        Me.ButtonHolder = New System.Windows.Forms.Panel()
        Me.AcceptedButton = New System.Windows.Forms.Button()
        Me.DeclineButton = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.OnlineStatusBall = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BaseLayout.SuspendLayout()
        Me.ButtonHolder.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BaseLayout
        '
        Me.BaseLayout.ColumnCount = 1
        Me.BaseLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.BaseLayout.Controls.Add(Me.BrandingPanel, 0, 0)
        Me.BaseLayout.Controls.Add(Me.LoginInfoBox, 0, 1)
        Me.BaseLayout.Controls.Add(Me.ButtonHolder, 0, 2)
        Me.BaseLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BaseLayout.Location = New System.Drawing.Point(0, 0)
        Me.BaseLayout.Name = "BaseLayout"
        Me.BaseLayout.RowCount = 3
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.BaseLayout.Size = New System.Drawing.Size(680, 458)
        Me.BaseLayout.TabIndex = 0
        '
        'BrandingPanel
        '
        Me.BrandingPanel.BackgroundImage = CType(resources.GetObject("BrandingPanel.BackgroundImage"), System.Drawing.Image)
        Me.BrandingPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BrandingPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BrandingPanel.Location = New System.Drawing.Point(3, 3)
        Me.BrandingPanel.Name = "BrandingPanel"
        Me.BrandingPanel.Size = New System.Drawing.Size(674, 100)
        Me.BrandingPanel.TabIndex = 0
        '
        'LoginInfoBox
        '
        Me.LoginInfoBox.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.LoginInfoBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoginInfoBox.Location = New System.Drawing.Point(3, 109)
        Me.LoginInfoBox.Name = "LoginInfoBox"
        Me.LoginInfoBox.ReadOnly = True
        Me.LoginInfoBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.LoginInfoBox.ShortcutsEnabled = False
        Me.LoginInfoBox.Size = New System.Drawing.Size(674, 246)
        Me.LoginInfoBox.TabIndex = 1
        Me.LoginInfoBox.Text = "Loading...."
        '
        'ButtonHolder
        '
        Me.ButtonHolder.Controls.Add(Me.AcceptedButton)
        Me.ButtonHolder.Controls.Add(Me.DeclineButton)
        Me.ButtonHolder.Controls.Add(Me.StatusStrip1)
        Me.ButtonHolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonHolder.Location = New System.Drawing.Point(3, 361)
        Me.ButtonHolder.Name = "ButtonHolder"
        Me.ButtonHolder.Size = New System.Drawing.Size(674, 94)
        Me.ButtonHolder.TabIndex = 2
        '
        'AcceptedButton
        '
        Me.AcceptedButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.AcceptedButton.Location = New System.Drawing.Point(524, 0)
        Me.AcceptedButton.Name = "AcceptedButton"
        Me.AcceptedButton.Size = New System.Drawing.Size(150, 72)
        Me.AcceptedButton.TabIndex = 2
        Me.AcceptedButton.Text = "Accept"
        Me.AcceptedButton.UseVisualStyleBackColor = True
        '
        'DeclineButton
        '
        Me.DeclineButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.DeclineButton.Location = New System.Drawing.Point(0, 0)
        Me.DeclineButton.Name = "DeclineButton"
        Me.DeclineButton.Size = New System.Drawing.Size(150, 72)
        Me.DeclineButton.TabIndex = 1
        Me.DeclineButton.Text = "Decline"
        Me.DeclineButton.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlineStatusBall})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 72)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(674, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'OnlineStatusBall
        '
        Me.OnlineStatusBall.Image = Global.Gatekeeper_Suite_2016.My.Resources.Resources.amberball
        Me.OnlineStatusBall.Name = "OnlineStatusBall"
        Me.OnlineStatusBall.Size = New System.Drawing.Size(16, 17)
        '
        'GatekeeperForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(680, 458)
        Me.ControlBox = False
        Me.Controls.Add(Me.BaseLayout)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "GatekeeperForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Gatekeeper 2016"
        Me.TopMost = True
        Me.BaseLayout.ResumeLayout(False)
        Me.ButtonHolder.ResumeLayout(False)
        Me.ButtonHolder.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BaseLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BrandingPanel As System.Windows.Forms.Panel
    Friend WithEvents LoginInfoBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ButtonHolder As System.Windows.Forms.Panel
    Friend WithEvents AcceptedButton As System.Windows.Forms.Button
    Friend WithEvents DeclineButton As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents OnlineStatusBall As System.Windows.Forms.ToolStripStatusLabel
End Class
