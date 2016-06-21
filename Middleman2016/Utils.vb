Imports System
Imports System.DirectoryServices
Imports System.Security.Principal
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.IO

Module Utils
    'MIDDLEMAN VERSION

    Declare Function AddPrinterConnection Lib "winspool.drv" Alias "AddPrinterConnectionA" (ByVal pName As String) As Long
    Declare Function DeletePrinterConnection Lib "winspool.drv" Alias "DeletePrinterConnectionA" (ByVal pName As String) As Long
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Declare Function GetDefaultPrinter Lib "winspool.drv" Alias "GetDefaultPrinterA" (ByVal pszBuffer() As String, ByVal pcchBuffer As Integer) As Boolean

    Private Const SPI_SETDESKWALLPAPER As Integer = &H14
    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDWININICHANGE As Integer = &H2
    Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer


    Dim debugTable As New gatekeeperdbDataSetTableAdapters.debugtableTableAdapter

    ''' <summary>
    ''' Sets the users wallpaper to the specified file
    ''' </summary>
    ''' <param name="imageLocation">filepath/name of the image</param>
    ''' <remarks></remarks>
    Public Sub setWallpaper(ByVal imageLocation As String)
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imageLocation, SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)
    End Sub

    ''' <summary>
    ''' Forces the current user to logout.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub logout()
        'Force Logout
        Dim t As Single
        Dim objWMIService, objComputer As Object

        'Now get some privileges
        objWMIService = GetObject("Winmgmts:{impersonationLevel=impersonate,(Debug,Shutdown)}")
        For Each objComputer In objWMIService.InstancesOf("Win32_OperatingSystem")
            t = objComputer.Win32Shutdown(0, 0)
        Next
    End Sub

    ''' <summary>
    ''' Add the username to the administrators local group on the specified machine
    ''' </summary>
    ''' <param name="PCNAME">the machine name (my.computer.name)</param>
    ''' <param name="username">user to add to local admins</param>
    ''' <remarks></remarks>
    Public Sub localAdmin(ByVal PCNAME As String, ByVal username As String)
        If Not isLocalAdmin(PCNAME, username) Then

            Try
                Dim LCL As New DirectoryEntry("WinNT://" & PCNAME & ",computer")
                Dim DOM As New DirectoryEntry("WinNT://" & "as")
                Dim DOMUSR As DirectoryEntry = DOM.Children.Find(username, "user")
                Dim LCLGRP As DirectoryEntry = LCL.Children.Find("administrators", "group")
                LCLGRP.Invoke("Add", New Object() {DOMUSR.Path.ToString})
                LCLGRP.CommitChanges()
                LCLGRP.Close()
            Catch ex As Exception
                ' debugTable.Insert(Now, ex.Message, "localAdmin", 0)
            End Try
        End If
    End Sub

    Public Sub unlocalAdmin(ByVal PCNAME As String, username As String)
        If isLocalAdmin(PCNAME, username) Then

            Try
                Dim LCL As New DirectoryEntry("WinNT://" & PCNAME & ",computer")
                Dim DOM As New DirectoryEntry("WinNT://" & "as")
                Dim DOMUSR As DirectoryEntry = DOM.Children.Find(username, "user")
                Dim LCLGRP As DirectoryEntry = LCL.Children.Find("administrators", "group")
                LCLGRP.Invoke("Remove", New Object() {DOMUSR.Path.ToString})
                LCLGRP.CommitChanges()
                LCLGRP.Close()
            Catch ex As Exception
                ' debugTable.Insert(Now, ex.Message, "localAdmin", 0)
                Dim test As Integer = 5
            End Try
        End If
    End Sub


    Public Function isLocalAdmin(ByVal PCNAME As String, username As String)
        Try
            Dim LCL As New DirectoryEntry("WinNT://" & PCNAME & ",computer")
            Dim DOM As New DirectoryEntry("WinNT://" & "as")
            Dim DOMUSR As DirectoryEntry = DOM.Children.Find(username, "user")
            Dim LCLGRP As DirectoryEntry = LCL.Children.Find("administrators", "group")
            Dim members As Object = LCLGRP.Invoke("Members", Nothing)
            For Each member As Object In CType(members, IEnumerable)
                Dim currentMember As New DirectoryEntry(member)
                If currentMember.Name.ToLower.Equals(username.ToLower) Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            ' debugTable.Insert(Now, ex.Message, "isLocalAdmin", 0)
            Return False
        End Try
    End Function

#Region "printers"
    Public Sub addPrinter(ByRef printer As printerInfo)
        AddPrinterConnection(printer.connection)
        If printer.isDefault Then
            SetDefaultPrinter(printer.connection)
        End If
    End Sub

    Public Sub delPrinter(ByRef printer As printerInfo)
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

    Public Function msToString(ByRef ms As MemoryStream) As String
        ms.Position = 0
        Dim sr As New StreamReader(ms)
        Return sr.ReadToEnd
    End Function

    Public Function simpleEncrypt(ByVal ba As Byte()) As Byte()
        Dim keyS As String = "A$hby"
        Dim key As Byte() = System.Text.Encoding.ASCII.GetBytes(keyS)
        Dim lb As New List(Of Byte)
        Dim n As Integer = 0
        For Each b As Byte In ba
            lb.Add(b Xor key(n))
            n = n + 1
            If n > keyS.Length - 1 Then n = 0

        Next
        Return lb.ToArray
    End Function

End Module

Public Class printerInfo
    Public name As String
    Public number As Integer
    Public connection As String
    Public isDefault As Boolean
    Public isSelectable As Boolean
    Public isSelected As Boolean

    Public Overrides Function toString() As String
        If isDefault Then
            Return name & " (Default)"
        End If
        Return name
    End Function

    Public Sub New(ByVal id As Integer, named As String)
        name = named
        number = id
    End Sub

    Public Sub New()
        name = "dummy"
        number = -1
    End Sub
End Class
