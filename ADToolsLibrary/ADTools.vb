Imports System.DirectoryServices.AccountManagement

Public Class UserDetails
    Property Username As String
    Property Surname As String
    Property DisplayName As String
    Property Password As String
    Property Description As String
    Property ScriptPath As String
    Property GivenName As String
    Property MiddleName As String
    Property _DistinguishedName As String
    Property EmailAddress As String
    Property _Guid As String
    Property HomeDirectory As String
    Property ProfilePath As String
    Property HomeDrive As String
    Property _LastLogin As String
    Property _SID As String
    Property Enabled As String
    Property UserCannotChangePassword As String
    Property PasswordNeverExpires As String
    Property _LastPasswordSet As String
    Property Groups As List(Of String)
End Class

Public Enum containerType
    dn
    ou
    cn
End Enum

Public Class UserTree
    Property type As containerType
    Property name As String
    Property parent As UserTree
    Property children As List(Of UserTree)
    Property userList As List(Of UserPrincipalEx)
End Class

<DirectoryRdnPrefix("CN")>
<DirectoryObjectClass("Person")>
Public Class UserPrincipalEx
    Inherits UserPrincipal

    Public Sub New(context As PrincipalContext)
        MyBase.New(context)
    End Sub

    ' Implement the constructor with initialization parameters.    
    Public Sub New(context As PrincipalContext, samAccountName As String, password As String, enabled As Boolean)
        MyBase.New(context, samAccountName, password, enabled)
    End Sub

    <DirectoryProperty("profilePath")>
    Public Property ProfilePath() As String
        Get
            If ExtensionGet("profilePath").Length <> 1 Then
                Return String.Empty
            End If

            Return DirectCast(ExtensionGet("profilePath")(0), String)
        End Get
        Set(value As String)
            ExtensionSet("profilePath", value)
        End Set
    End Property

    ' Implement the overloaded search method FindByIdentity.
    Public Shared Shadows Function FindByIdentity(context As PrincipalContext, identityValue As String) As UserPrincipalEx
        Try
            Return DirectCast(FindByIdentityWithType(context, GetType(UserPrincipalEx), identityValue), UserPrincipalEx)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ' Implement the overloaded search method FindByIdentity. 
    Public Shared Shadows Function FindByIdentity(context As PrincipalContext, identityType As IdentityType, identityValue As String) As UserPrincipalEx
        Try
            Return DirectCast(FindByIdentityWithType(context, GetType(UserPrincipalEx), identityType, identityValue), UserPrincipalEx)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
End Class

Public Class ADTools
    Public Shared Function getConnection(ByVal domain As String, ByVal OU As String, ByVal user As String, ByVal pass As String) As PrincipalContext
        Return New PrincipalContext(ContextType.Domain, My.Resources.Domain & domain, OU, ContextOptions.SimpleBind, user, pass)
    End Function

    Public Shared Function getConnection(ByVal domain As String, ByVal OU As String) As PrincipalContext
        Return New PrincipalContext(ContextType.Domain, domain, OU, ContextOptions.Negotiate)
    End Function

    Public Shared Function getUserTree(ctx As PrincipalContext) As UserTree
        Dim uTree As New UserTree
        uTree.name = "DC=internal"
        uTree.type = containerType.dn

        Dim uList As List(Of UserPrincipalEx) = getAllUsers(ctx)
        For Each user As UserPrincipalEx In uList
            addToTree(user, uTree)
        Next
        Return uTree
    End Function

    Public Shared Sub addToTree(ByVal user As UserPrincipalEx, ByVal uTree As UserTree)
        'get treeNode from user's DistingushedName
        Dim locations As String() = reverseArray(user.DistinguishedName.Split(","))
        Dim treeNode As UserTree = getReleventNode(locations, uTree)
        If treeNode.userList Is Nothing Then
            treeNode.userList = New List(Of UserPrincipalEx)
        End If
        treeNode.userList.Add(user)

    End Sub

    Public Shared Function getReleventNode(ByVal locations As String(), ByVal cNode As UserTree) As UserTree
        Dim userNode As UserTree = cNode
        For i As Integer = 2 To locations.Count - 2
            userNode = findMatchingNode(locations(i), userNode)
        Next
        Return userNode
    End Function

    Private Shared Function findMatchingNode(ByVal locationName As String, cNode As UserTree) As UserTree
        If cNode.children IsNot Nothing Then
            For Each node As UserTree In cNode.children
                If node.name.Equals(locationName) Then
                    Return node
                End If
            Next
        Else
            cNode.children = New List(Of UserTree)
        End If

        Dim newNode As UserTree = New UserTree
        newNode.name = locationName
        cNode.children.Add(newNode)
        Return newNode
    End Function

    Public Shared Function userExists(ByRef ctx As PrincipalContext, ByRef user As UserDetails) As Boolean
        Dim usr As UserPrincipalEx = UserPrincipalEx.FindByIdentity(ctx, user.Username)
        If usr IsNot Nothing Then
            usr.Dispose()
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function createUser(ByRef ctx As PrincipalContext, ByRef user As UserDetails) As Boolean
        'User already exist?
        If Not userExists(ctx, user) Then
            Dim lusr = New UserPrincipal(ctx)
            lusr.SamAccountName = user.Username
            lusr.DisplayName = user.DisplayName
            lusr.GivenName = user.GivenName
            lusr.Surname = user.Surname
            Try
                lusr.Save(ctx)
                lusr.Dispose()
                modify(ctx, user)
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If

    End Function

    Private Shared Sub modify(ByVal ctx As PrincipalContext, ByVal user As UserDetails)
        Dim usr As UserPrincipalEx = UserPrincipalEx.FindByIdentity(ctx, user.Username)
        If usr Is Nothing Then
            Throw New Exception("User Doesn't Exist")
        End If
        If user.Enabled.ToLower.Equals("true") Then
            usr.Enabled = True
        Else
            usr.Enabled = False
        End If
        If user.PasswordNeverExpires.ToLower.Equals("true") Then
            usr.PasswordNeverExpires = True
        Else
            usr.PasswordNeverExpires = False
        End If
        If user.UserCannotChangePassword.ToLower.Equals("true") Then
            usr.UserCannotChangePassword = True
        Else
            usr.UserCannotChangePassword = False
        End If


        usr.ProfilePath = user.ProfilePath
        usr.HomeDrive = user.HomeDrive
        usr.HomeDirectory = user.HomeDirectory
        usr.SetPassword(user.Password)
        usr.Description = user.Description
        usr.MiddleName = user.MiddleName

        usr.UserPrincipalName = user.Username & "@ashbyschool.org.uk"
        usr.Save()
        usr.Dispose()
    End Sub

    Public Shared Sub addToGroups(ByVal ctx As PrincipalContext, ByVal user As UserDetails)
        ' add User to group
        If user.Groups.Count > 0 Then
            For Each group As String In user.Groups
                addUserToGroup(ctx, user.Username, group)
            Next
        End If
    End Sub

    Public Shared Function getUserPrincipalexbyUsername(ByVal ctx As PrincipalContext, userName As String) As UserPrincipalEx
        Try
            Dim usr As UserPrincipalEx = UserPrincipalEx.FindByIdentity(ctx, userName)
            Return usr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function getUserPrincipalbyUsername(ByVal ctx As PrincipalContext, userName As String) As UserPrincipal
        Try
            Dim usr As UserPrincipal = UserPrincipal.FindByIdentity(ctx, userName)
            Return usr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function getUserPrincipalsByFullName(ByVal ctx As PrincipalContext, fullname As String) As List(Of UserPrincipal)
        Dim userlist As New List(Of UserPrincipal)
        Dim searchusr As UserPrincipal = New UserPrincipal(ctx)
        searchusr.GivenName = fullname.Split(" ")(0)
        searchusr.Surname = fullname.Split(" ")(1)
        Dim searcher As PrincipalSearcher = New PrincipalSearcher(searchusr)
        For Each user As UserPrincipal In searcher.FindAll()
            userlist.Add(user)
        Next
        Return userlist
    End Function

    Public Shared Function getGroupPrincipalbyName(ByVal ctx As PrincipalContext, group As String) As GroupPrincipal
        Try
            Dim usr As GroupPrincipal = GroupPrincipal.FindByIdentity(ctx, group)
            Return usr
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function getAllUsers(ByVal domain As String, ByVal OU As String) As List(Of UserPrincipalEx)
        Dim ctx As PrincipalContext = getConnection(domain, OU)
        Return getAllUsers(ctx)
    End Function

    Public Shared Function getAllUsers(ByRef ctx As PrincipalContext) As List(Of UserPrincipalEx)
        Dim returnList As New List(Of UserPrincipalEx)
        Dim usr As UserPrincipalEx = New UserPrincipalEx(ctx)
        Dim ps As PrincipalSearcher = New PrincipalSearcher(usr)
        For Each user As UserPrincipalEx In ps.FindAll()
            returnList.Add(user)
        Next
        Return returnList
    End Function

    Public Shared Function getUsersExBySearch(ByRef ctx As PrincipalContext, ByVal user As UserDetails) As List(Of UserPrincipalEx)
        Dim lusr = New UserPrincipal(ctx)
        If Not user.Username.Equals("") Then
            lusr.SamAccountName = user.Username
        End If
        If Not user.DisplayName.Equals("") Then
            lusr.DisplayName = user.DisplayName
        End If
        If Not user.GivenName.Equals("") Then
            lusr.GivenName = user.GivenName
        End If
        If Not user.Surname.Equals("") Then
            lusr.Surname = user.Surname
        End If
        Dim returnList As New List(Of UserPrincipalEx)
        Dim usr As UserPrincipalEx = New UserPrincipalEx(ctx)
        Dim ps As PrincipalSearcher = New PrincipalSearcher(lusr)
        For Each person As UserPrincipalEx In ps.FindAll()
            returnList.Add(person)
        Next
        Return returnList
    End Function

    Private Shared Function reverseArray(ByVal inp() As String) As String()
        Dim outArray(inp.Count) As String
        For i As Integer = 0 To inp.Count - 1
            outArray(inp.Count - i) = inp(i)
        Next
        Return outArray
    End Function

    ''' <summary>
    ''' Convert a path in the form as/internal/user into cn=user,dc=internal,dc=as
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getADPath(ByVal path As String) As String
        Dim backPath As String() = reverseArray(path.Split(","))
        Dim returnString As String = String.Join(",", backPath)
        Return returnString.Substring(1)
    End Function

    Public Shared Sub addUserToGroup(ByVal ctx As PrincipalContext, user As String, ByVal group As String)
        Try
            Dim usr As UserPrincipal = getUserPrincipalbyUsername(ctx, user)
            Dim grp As GroupPrincipal = getGroupPrincipalbyName(ctx, group)
            If Not grp.Members.Contains(ctx, IdentityType.SamAccountName, usr.SamAccountName) Then
                grp.Members.Add(usr)
            End If
            grp.Save()
        Catch ex As Exception
            'no groups to add
        End Try
    End Sub

    Public Shared Function ADLookupByName(ByVal tlctx As PrincipalContext, name As String) As String
        Dim userlist As List(Of UserPrincipal) = getUserPrincipalsByFullName(tlctx, name)
        If userlist.Count > 0 Then
            If userlist.Count > 1 Then
                MsgBox(String.Format("{0} added as {1}. {2} Others exist.", name, userlist(0).SamAccountName, userlist.Count))
            End If
            Return userlist(0).SamAccountName
        End If
        Return "**** " & name & " ****"
    End Function

    Public Shared Function ADLookupByUserName(ByVal tlctx As PrincipalContext, name As String) As String
        Dim user As UserPrincipal = getUserPrincipalbyUsername(tlctx, name)
        If user Is Nothing Then
            Return "****" & name & " ****"
        Else
            Return user.SamAccountName
        End If

    End Function
End Class

