Imports System.DirectoryServices
Imports System.Drawing.Printing

Public Class ComputerManagerForm
    Dim computerTableAdapter As New gatekeeperdbDataSetTableAdapters.computertableTableAdapter
    Dim computerUserTableAdapter As New gatekeeperdbDataSetTableAdapters.computeruserTableAdapter
    Dim userTableAdapter As New gatekeeperdbDataSetTableAdapters.usertableTableAdapter
    Dim printerTableAdapter As New gatekeeperdbDataSetTableAdapters.printerTableAdapter
    Dim computerPrinterTableAdapter As New gatekeeperdbDataSetTableAdapters.computerprinterTableAdapter

    Dim computerTree As New TreeNode("Computers")
    Dim computerTreeCollection As TreeNodeCollection
    Dim str As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        computerTableAdapter.Fill(GatekeeperdbDataSet1.Tables("computertable"))
        computerUserTableAdapter.Fill(GatekeeperdbDataSet1.Tables("computeruser"))
        userTableAdapter.Fill(GatekeeperdbDataSet1.Tables("usertable"))
        printerTableAdapter.Fill(GatekeeperdbDataSet1.Tables("printer"))
        setupTreeView()
        For Each printer In PrinterSettings.InstalledPrinters
            'MsgBox(String.Format("Printer - {0}", printer))
            CompList.Items.Add(printer)
        Next
    End Sub

#Region "Database Handleing"

    Private Sub setupTreeView()
        Dim computerName As String
        Dim locationName As String
        Dim computerID As Integer
        Dim lastNode As TreeNode
        TreeView1.Nodes.Clear()

        For Each row As gatekeeperdbDataSet.computertableRow In GatekeeperdbDataSet1.computertable.Rows
            computerName = row.Field(Of String)("name")
            locationName = computerName.Split("-")(0)
            computerID = row.Field(Of Integer)("ID")
            lastNode = addtoTree(locationName.ToUpper, computerName.ToLower)

        Next
        TreeView1.Nodes.Add(computerTree)
    End Sub

    Private Function addtoTree(ByVal locationName, computerName) As TreeNode
        Dim cNode As TreeNode
        cNode = findTreeNode(locationName)

        If IsNothing(cNode) Then
            addItem(computerTree, locationName)
            addtoTree(locationName, computerName)
        Else
            Dim newNode As TreeNode = addItem(cNode, computerName)
            Dim pUsers As TreeNode = newNode.Nodes.Add("Priv Users")
            Dim cPrinters As TreeNode = newNode.Nodes.Add("Printers")
            For Each drow As DataRow In getPrivUsers(computerName)
                pUsers.Nodes.Add(drow.Field(Of String)("userName"))
            Next
            For Each drow As DataRow In getPrinters(computerName)
                cPrinters.Nodes.Add(drow.Field(Of String)("printerName"))
            Next
        End If
        Return cNode
    End Function

    Private Function findTreeNode(ByVal item) As TreeNode
        For Each cNode As TreeNode In computerTree.Nodes
            If cNode.Text.Equals(item) Then
                Return cNode
            End If
        Next
        Return Nothing
    End Function

    Private Function addItem(ByRef cNode As TreeNode, ByVal name As String) As TreeNode
        Dim aNode As New TreeNode(name)
        cNode.Nodes.Add(aNode)
        Return aNode
    End Function
    Private Function getPrivUsers(ByVal computerName As String) As DataRowCollection
        Dim privAdapter As New gatekeeperdbDataSetTableAdapters.privUserTableAdapter()
        Dim privTable As DataTable = privAdapter.GetData(computerName)
        Return privTable.Rows
    End Function

    Private Function getPrinters(ByVal computername As String) As DataRowCollection
        Dim printerAdapter As New gatekeeperdbDataSetTableAdapters.myPrintersTableAdapter
        Dim printTable As DataTable = printerAdapter.GetData(computername)
        Return printTable.Rows
    End Function
#End Region

#Region "Handle Treeview Context Menu"
    Private Sub TreeView1_MouseUp(sender As Object, e As MouseEventArgs) Handles TreeView1.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim p As Point = New Point(e.X, e.Y)
            Dim node As TreeNode = TreeView1.GetNodeAt(p)
            If Not node Is Nothing Then
                TreeView1.SelectedNode = node
            End If
        End If
        Dim cNode As TreeNode = TreeView1.SelectedNode
        Dim pNode As TreeNode
        If cNode.Text.Equals("Printers") Then
            pNode = cNode.Parent
            ToolStripMenuItem1.Text = "Add Printer to " & pNode.Text
        ElseIf cNode.Text.Equals("Priv Users")
            pNode = cNode.Parent
            ToolStripMenuItem1.Text = "Add Priv User to " & pNode.Text
        Else
            ToolStripMenuItem1.Text = "."

        End If
    End Sub

    Private Sub NodeContextMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NodeContextMenu.Opening
        Dim cNode As TreeNode = TreeView1.SelectedNode
        Dim pNode As TreeNode
        If cNode.Text.Equals("Printers") Then
            pNode = cNode.Parent
            ToolStripMenuItem1.Text = "Add Printer to " & pNode.Text
        ElseIf cNode.Text.Equals("Priv Users")
            pNode = cNode.Parent
            ToolStripMenuItem1.Text = "Add Priv User to " & pNode.Text
        Else
            ToolStripMenuItem1.Text = "."
            e.Cancel = True
        End If
    End Sub

    Private Sub NodeContextMenu_MouseDown(sender As Object, e As MouseEventArgs) Handles TreeView1.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim p As Point = New Point(e.X, e.Y)
            Dim node As TreeNode = TreeView1.GetNodeAt(p)
            If Not node Is Nothing Then
                TreeView1.SelectedNode = node
            End If
        End If
        Dim cNode As TreeNode = TreeView1.SelectedNode
        Dim pNode As TreeNode
        If cNode.Text.Equals("Printers") Then
            pNode = cNode.Parent
            ToolStripMenuItem1.Text = "Add Printer to " & pNode.Text
        ElseIf cNode.Text.Equals("Priv Users")
            pNode = cNode.Parent
            ToolStripMenuItem1.Text = "Add Priv User to " & pNode.Text
        Else
            ToolStripMenuItem1.Text = "."
        End If
    End Sub

    Private Sub NodeContextMenu_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles NodeContextMenu.ItemClicked
        Dim item As String = e.ClickedItem.Text
        Dim location As String = TreeView1.SelectedNode.Parent.Text
        If item.Contains("Priv") Then
            addPriv(location)
        ElseIf item.Contains("Printer") Then
            addPrinter(location)
        End If
    End Sub
#End Region

    Private Function getIDByName(ByVal computerName As String) As Integer
        For Each drow In GatekeeperdbDataSet1.Tables("computertable").Rows
            If drow.field(Of String)("Name").Equals(computerName) Then
                Return drow.field(Of Integer)("ID")
            End If
        Next
        Return -1
    End Function

    Private Sub savePriv(ByVal location As String, ByVal user As String)
        Dim pid As Integer = -1
        Dim cid As Integer = -1

        Try
            pid = userTableAdapter.getUserIDByName(user)
        Catch ex As Exception
            If ex.Message.StartsWith("Nullable") Then
                pid = createUser(user)
            Else
                Throw ex
            End If
        End Try

        Try
            cid = computerTableAdapter.getIDByName(location)
        Catch ex As Exception
            Throw ex
        End Try
        If pid > 0 And cid > 0 Then
            computerUserTableAdapter.Insert(Nothing, cid, pid)
            setupTreeView()
            Me.Refresh()
        End If
    End Sub

    Private Function createUser(ByVal name As String) As Integer
        Dim res = MsgBox("User not found.  Do you want to add this user anyway?", MsgBoxStyle.YesNo, "Create new user?")
        If res = MsgBoxResult.Yes Then
            userTableAdapter.Insert(name)
            Return userTableAdapter.getUserIDByName(name)
        Else
            Return -99
        End If
    End Function

    Private Sub addPriv(ByVal location As String)
        Dim pUser As String = requestString()
        If Not pUser.Equals("<none>") Then
            savePriv(location, pUser)
        End If
    End Sub

    Private Sub addPrinter(ByVal location As String)
        Dim locationID = computerTableAdapter.getIDByName(location)
        Dim ps As New PrinterListForm
        Dim id As Integer
        Dim printer As String
        Dim connection As String
        Dim res As DialogResult = ps.ShowDialog()
        If res = DialogResult.OK Then
            Dim plist As ListBox.SelectedObjectCollection = ps.PrinterList.SelectedItems
            For Each p As DataRowView In plist
                id = p.Row.Field(Of Integer)("ID")
                printer = p.Row.Field(Of String)("Name")
                connection = p.Row.Field(Of String)("Connection")
                MsgBox(String.Format("{0} - {1} - {2} added to {3}", id, printer, connection, locationID))
                computerPrinterTableAdapter.Insert(locationID, id, 0)
            Next
        End If
    End Sub

    Private Function requestString() As String
        Dim strReq As New stringDialog
        Dim p As Point = Cursor.Position
        strReq.Location = p
        Dim result As DialogResult = strReq.ShowDialog()
        If result = DialogResult.OK Then
            Return strReq.stringTextBox.Text
        Else
            Return "<none>"
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ad As DirectoryEntry
        Dim ads As New DirectorySearcher()
        Try
            ad = New DirectoryEntry("LDAP://OU=AS Workstations,OU=Ashby School,DC=AS,DC=Internal")
            ads.Filter = ("(objectClass=computer)")

            Dim res As SearchResult
            For Each res In ads.FindAll()
                If res.GetDirectoryEntry.Path.Contains("AS Workstations") Then
                    CompList.Items.Add(res.GetDirectoryEntry.Name.ToString.Substring(3))
                    computerTableAdapter.Insert(res.GetDirectoryEntry.Name.ToString.Substring(3))
                End If

            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddPrinterToDBButton_Click(sender As Object, e As EventArgs) Handles AddPrinterToDBButton.Click
        Dim ap As New AddPrinterForm
        Dim res As DialogResult = ap.ShowDialog()
        Dim sel As Byte = 0
        If ap.pc.selectable Then
            sel = 1
        End If
        If res = DialogResult.OK Then
            printerTableAdapter.Insert(ap.pc.friendlyName, ap.pc.connectionString, sel)
        End If
    End Sub
End Class