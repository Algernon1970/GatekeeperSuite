﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("UserUtilities.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to GPUPDATE.
        '''</summary>
        Friend ReadOnly Property GPUPDATE() As String
            Get
                Return ResourceManager.GetString("GPUPDATE", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to qHhzqsFNwn.
        '''</summary>
        Friend ReadOnly Property LOCK() As String
            Get
                Return ResourceManager.GetString("LOCK", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to hw4uLegSH7.
        '''</summary>
        Friend ReadOnly Property MSG() As String
            Get
                Return ResourceManager.GetString("MSG", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to 6AUpuPB4dP.
        '''</summary>
        Friend ReadOnly Property PRINTER() As String
            Get
                Return ResourceManager.GetString("PRINTER", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to q5gfuChcVs.
        '''</summary>
        Friend ReadOnly Property PRINTERDEFAULT() As String
            Get
                Return ResourceManager.GetString("PRINTERDEFAULT", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
