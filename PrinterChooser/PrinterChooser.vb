Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing

Public Class PrinterChooser
    Dim usersTA As New gatekeeperdbDataSetTableAdapters.usertableTableAdapter
    Dim printersTA As New gatekeeperdbDataSetTableAdapters.printerTableAdapter
    Dim gatekeeperdbDataSet1 As New gatekeeperdbDataSet

    Declare Function AddPrinterConnection Lib "winspool.drv" Alias "AddPrinterConnectionA" (ByVal pName As String) As Long
    Declare Function DeletePrinterConnection Lib "winspool.drv" Alias "DeletePrinterConnectionA" (ByVal pName As String) As Long
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Declare Function GetDefaultPrinter Lib "winspool.drv" Alias "GetDefaultPrinterA" (ByVal pszBuffer() As String, ByVal pcchBuffer As Integer) As Boolean



    Private Sub PrinterChooser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim args As String() = Environment.GetCommandLineArgs
        If args.Count > 1 Then
            Dim location As New Point
            Dim bits As String() = args(1).Substring(1).Split(",")
            Dim myX As Integer = Integer.Parse(bits(0))
            Dim myY As Integer = Integer.Parse(bits(1))

            Me.Location = New Point(myX, myY)
            Me.Refresh()
        End If

        printersTA.Fill(gatekeeperdbDataSet1.Tables("printer"))
        For Each printerRow As gatekeeperdbDataSet.printerRow In gatekeeperdbDataSet1.Tables("printer").Rows
            If printerRow.Selectable = 1 Then
                PrinterListBox.Items.Add(printerRow.Name)
            End If
        Next
        Me.Refresh()
    End Sub

    Private Sub CancelBut_Click(sender As Object, e As EventArgs) Handles CancelBut.Click
        Me.Close()
    End Sub

    Private Sub AcceptBut_Click(sender As Object, e As EventArgs) Handles AcceptBut.Click
        Dim printerNames As CheckedListBox.CheckedItemCollection = PrinterListBox.CheckedItems
        Dim pinfo As New loclprinterInfo
        For Each printerName In printerNames
            Dim connection As String = printersTA.GetConnectionByName(printerName.ToString)
            pinfo.name = printerName.ToString
            pinfo.connection = connection
            pinfo.isDefault = False
            addPrinter(pinfo)
        Next
        Me.Close()
    End Sub

#Region "printers"
    Public Sub addPrinter(ByRef printer As loclprinterInfo)
        AddPrinterConnection(printer.connection)
        If printer.isDefault Then
            SetDefaultPrinter(printer.connection)
        End If
    End Sub

    Public Sub delPrinter(ByRef printer As loclprinterInfo)
        DeletePrinterConnection(printer.connection)
    End Sub

    Public Sub deleteAllNetworkPrinters()
        For Each printerConnection As String In PrinterSettings.InstalledPrinters
            If printerConnection.StartsWith("\\") Then
                DeletePrinterConnection(printerConnection)
            End If
        Next
    End Sub

    Public Sub setLocalDefault()
        For Each printerConnection As String In PrinterSettings.InstalledPrinters
            If Not printerConnection.StartsWith("\\") Then
                SetDefaultPrinter(printerConnection)
            End If
        Next
    End Sub
#End Region
End Class

Public Structure loclprinterInfo
    Dim name As String
    Dim connection As String
    Dim isDefault As Boolean
End Structure