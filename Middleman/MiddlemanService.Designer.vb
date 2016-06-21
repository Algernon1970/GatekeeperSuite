Imports System.ServiceProcess

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MiddlemanService
    Inherits ServiceBase

    'UserService overrides dispose to clean up the component list.
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

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GatekeeperdbDataSet1 = New Gatekeeper_Suite_2016.gatekeeperdbDataSet()
        CType(Me.GatekeeperdbDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'GatekeeperdbDataSet1
        '
        Me.GatekeeperdbDataSet1.DataSetName = "gatekeeperdbDataSet"
        Me.GatekeeperdbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'MiddlemanService
        '
        Me.ServiceName = "Service1"
        CType(Me.GatekeeperdbDataSet1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents GatekeeperdbDataSet1 As Gatekeeper_Suite_2016.gatekeeperdbDataSet

End Class
