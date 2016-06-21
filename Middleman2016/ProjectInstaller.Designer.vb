<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.EventLogInstaller1 = New System.Diagnostics.EventLogInstaller()
        '
        'EventLogInstaller1
        '
        Me.EventLogInstaller1.CategoryCount = 0
        Me.EventLogInstaller1.CategoryResourceFile = Nothing
        Me.EventLogInstaller1.Log = "GatekeeperSuite"
        Me.EventLogInstaller1.MessageResourceFile = Nothing
        Me.EventLogInstaller1.ParameterResourceFile = Nothing
        Me.EventLogInstaller1.Source = "Middleman"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.EventLogInstaller1})

    End Sub

    Public WithEvents EventLogInstaller1 As EventLogInstaller
End Class
