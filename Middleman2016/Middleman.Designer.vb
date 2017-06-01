Imports System.ServiceProcess

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Middleman
    Inherits System.ServiceProcess.ServiceBase

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

    ' The main entry point for the process
    <MTAThread()> _
    <System.Diagnostics.DebuggerNonUserCode()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New Middleman}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GatekeeperdbDataSet1 = New Middleman2016.gatekeeperdbDataSet()
        Me.InfotableTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.infotableTableAdapter()
        Me.UsertableTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.usertableTableAdapter()
        Me.UserlogintableTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.userlogintableTableAdapter()
        Me.PrinterTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.printerTableAdapter()
        Me.ComputeruserTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.computeruserTableAdapter()
        Me.ComputertableTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.computertableTableAdapter()
        Me.ComputerprinterTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.computerprinterTableAdapter()
        Me.GatekeeperSuiteEvents = New System.Diagnostics.EventLog()
        Me.DebugtableTableAdapter1 = New Middleman2016.gatekeeperdbDataSetTableAdapters.debugtableTableAdapter()
        CType(Me.GatekeeperdbDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GatekeeperSuiteEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'GatekeeperdbDataSet1
        '
        Me.GatekeeperdbDataSet1.DataSetName = "gatekeeperdbDataSet"
        Me.GatekeeperdbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'InfotableTableAdapter1
        '
        Me.InfotableTableAdapter1.ClearBeforeFill = True
        '
        'UsertableTableAdapter1
        '
        Me.UsertableTableAdapter1.ClearBeforeFill = True
        '
        'UserlogintableTableAdapter1
        '
        Me.UserlogintableTableAdapter1.ClearBeforeFill = True
        '
        'PrinterTableAdapter1
        '
        Me.PrinterTableAdapter1.ClearBeforeFill = True
        '
        'ComputeruserTableAdapter1
        '
        Me.ComputeruserTableAdapter1.ClearBeforeFill = True
        '
        'ComputertableTableAdapter1
        '
        Me.ComputertableTableAdapter1.ClearBeforeFill = True
        '
        'ComputerprinterTableAdapter1
        '
        Me.ComputerprinterTableAdapter1.ClearBeforeFill = True
        '
        'GatekeeperSuiteEvents
        '
        Me.GatekeeperSuiteEvents.Log = "GatekeeperSuite"
        Me.GatekeeperSuiteEvents.Source = "Middleman"
        '
        'DebugtableTableAdapter1
        '
        Me.DebugtableTableAdapter1.ClearBeforeFill = True
        '
        'Middleman
        '
        Me.ServiceName = "MiddlemanService"
        CType(Me.GatekeeperdbDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GatekeeperSuiteEvents, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents GatekeeperdbDataSet1 As Middleman2016.gatekeeperdbDataSet
    Friend WithEvents InfotableTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.infotableTableAdapter
    Friend WithEvents UsertableTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.usertableTableAdapter
    Friend WithEvents UserlogintableTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.userlogintableTableAdapter
    Friend WithEvents PrinterTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.printerTableAdapter
    Friend WithEvents ComputeruserTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.computeruserTableAdapter
    Friend WithEvents ComputertableTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.computertableTableAdapter
    Friend WithEvents ComputerprinterTableAdapter1 As Middleman2016.gatekeeperdbDataSetTableAdapters.computerprinterTableAdapter
    Friend WithEvents GatekeeperSuiteEvents As EventLog
    Friend WithEvents DebugtableTableAdapter1 As gatekeeperdbDataSetTableAdapters.debugtableTableAdapter
End Class
