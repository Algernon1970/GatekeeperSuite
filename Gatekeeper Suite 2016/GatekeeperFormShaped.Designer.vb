﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GatekeeperFormShaped
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GatekeeperFormShaped))
        Me.myAcceptButton = New System.Windows.Forms.PictureBox()
        Me.DeclineButton = New System.Windows.Forms.PictureBox()
        Me.Outbox = New System.Windows.Forms.RichTextBox()
        Me.OnlineStatusBall = New System.Windows.Forms.PictureBox()
        Me.versionLabel = New System.Windows.Forms.Label()
        Me.GatekeeperEvents = New System.Diagnostics.EventLog()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        CType(Me.myAcceptButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DeclineButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OnlineStatusBall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GatekeeperEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'myAcceptButton
        '
        Me.myAcceptButton.BackColor = System.Drawing.Color.Transparent
        Me.myAcceptButton.Location = New System.Drawing.Point(544, 451)
        Me.myAcceptButton.Name = "myAcceptButton"
        Me.myAcceptButton.Size = New System.Drawing.Size(121, 128)
        Me.myAcceptButton.TabIndex = 0
        Me.myAcceptButton.TabStop = False
        '
        'DeclineButton
        '
        Me.DeclineButton.BackColor = System.Drawing.Color.Transparent
        Me.DeclineButton.Location = New System.Drawing.Point(29, 451)
        Me.DeclineButton.Name = "DeclineButton"
        Me.DeclineButton.Size = New System.Drawing.Size(117, 128)
        Me.DeclineButton.TabIndex = 1
        Me.DeclineButton.TabStop = False
        '
        'Outbox
        '
        Me.Outbox.BackColor = System.Drawing.Color.LightGray
        Me.Outbox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Outbox.Cursor = System.Windows.Forms.Cursors.Default
        Me.Outbox.Location = New System.Drawing.Point(143, 159)
        Me.Outbox.Name = "Outbox"
        Me.Outbox.ReadOnly = True
        Me.Outbox.Size = New System.Drawing.Size(405, 303)
        Me.Outbox.TabIndex = 2
        Me.Outbox.Text = ""
        '
        'OnlineStatusBall
        '
        Me.OnlineStatusBall.BackColor = System.Drawing.Color.Transparent
        Me.OnlineStatusBall.Location = New System.Drawing.Point(511, 556)
        Me.OnlineStatusBall.Name = "OnlineStatusBall"
        Me.OnlineStatusBall.Size = New System.Drawing.Size(27, 23)
        Me.OnlineStatusBall.TabIndex = 3
        Me.OnlineStatusBall.TabStop = False
        '
        'versionLabel
        '
        Me.versionLabel.AutoSize = True
        Me.versionLabel.Location = New System.Drawing.Point(152, 566)
        Me.versionLabel.Name = "versionLabel"
        Me.versionLabel.Size = New System.Drawing.Size(0, 13)
        Me.versionLabel.TabIndex = 4
        '
        'GatekeeperEvents
        '
        Me.GatekeeperEvents.Log = "Gatekeepersuite"
        Me.GatekeeperEvents.Source = "Middleman"
        Me.GatekeeperEvents.SynchronizingObject = Me
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipText = "Mapped Drives"
        Me.NotifyIcon1.BalloonTipTitle = "Drive Mapper"
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "One Drive Mapper"
        Me.NotifyIcon1.Visible = True
        '
        'GatekeeperFormShaped
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Gatekeeper_Suite_2016.My.Resources.Resources.LoginBoxFlat
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(668, 603)
        Me.ControlBox = False
        Me.Controls.Add(Me.versionLabel)
        Me.Controls.Add(Me.OnlineStatusBall)
        Me.Controls.Add(Me.Outbox)
        Me.Controls.Add(Me.DeclineButton)
        Me.Controls.Add(Me.myAcceptButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GatekeeperFormShaped"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "GatekeeperFormShaped"
        Me.TransparencyKey = System.Drawing.Color.White
        CType(Me.myAcceptButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DeclineButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OnlineStatusBall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GatekeeperEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents myAcceptButton As PictureBox
    Friend WithEvents DeclineButton As PictureBox
    Friend WithEvents Outbox As RichTextBox
    Friend WithEvents OnlineStatusBall As PictureBox
    Friend WithEvents versionLabel As Label
    Public WithEvents GatekeeperEvents As EventLog
    Friend WithEvents NotifyIcon1 As NotifyIcon
End Class
