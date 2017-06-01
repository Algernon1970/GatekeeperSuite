Imports System.ComponentModel
Imports AshbyTools
Imports System
Imports System.IO

Public Class Watchdog
    Dim watcher As New BackgroundWorker
    Public running As Boolean = False
    Public Event watchdogevent(ByVal message As String)
    Dim mapped As Boolean = False
    Dim gatekeeperevents As EventLog
    Dim notifier As NotifyIcon
    Dim attempted As Boolean = False
    Dim flashing As Boolean = False
    Dim flasher As New BackgroundWorker

    Public Sub New(ByVal eventstuff As EventLog)
        gatekeeperevents = eventstuff
        AddHandler watcher.DoWork, AddressOf watcher_dowork
        AddHandler flasher.DoWork, AddressOf flasher_dowork
        running = True
    End Sub

    Public Sub startwatcher(ByRef icon As NotifyIcon)
        notifier = icon
        watcher.RunWorkerAsync()
    End Sub

    Public Sub stopwatcher()
        running = False
        watcher.CancelAsync()
    End Sub

    Private Sub watcher_dowork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim foundz As Boolean = False
        Application.UseWaitCursor = False
        running = True
        RaiseEvent watchdogevent("Started Watching")
        ' While Not mapped
        While running
            foundz = False
            Application.UseWaitCursor = False
            Threading.Thread.Sleep(10000)
            Try
                Dim di() As DriveInfo = DriveInfo.GetDrives
                For Each d As DriveInfo In di
                    If d.Name.ToUpper.StartsWith("Z:") Then
                        foundz = True
                        If d.IsReady Then
                            mapped = True
                            notifier.Icon = My.Resources.greencloud
                            flashing = False
                            attempted = False
                        Else
                            notifier.Icon = My.Resources.redcloud
                            If attempted And Not flashing Then
                                startFlashing()
                            End If
                            mapped = False
                        End If
                    End If
                Next
                If Not foundz Then
                    mapped = False
                    notifier.Icon = My.Resources.redcloud
                    If attempted And Not flashing Then
                        startFlashing()
                    End If
                End If

                If Not mapped Then
                    mapZDrive()
                End If
            Catch ex As Exception
                RaiseEvent watchdogevent(ex.Message)
            End Try

        End While
        RaiseEvent watchdogevent("Finished Watchdog")
    End Sub

    Private Sub startFlashing()
        flasher.RunWorkerAsync()
    End Sub

    Private Sub stopFlashing()
        flashing = False
        flasher.CancelAsync()
    End Sub

    Private Sub flasher_dowork()
        Application.UseWaitCursor = False
        flashing = True
        While flashing
            notifier.Icon = My.Resources.whitecloud
            Threading.Thread.Sleep(1000)
            notifier.Icon = My.Resources.redcloud
            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Private Sub mapZDrive()
        Try
            Dim drivemapper As New DriveMapper(gatekeeperevents)
            attempted = drivemapper.mapdrives()
            RaiseEvent watchdogevent("mapZDrive Ran")
        Catch ex As Exception
            RaiseEvent watchdogevent("mapZDrive: " & ex.Message)
        End Try

    End Sub

End Class
