'
' Created by SharpDevelop.
' User: acarpentier
' Date: 12/21/2018
' Time: 11:30 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Option Strict Off
Option Explicit On

'''
<Global.System.CLSCompliantAttribute(false)>  _
Partial Public NotInheritable Class CropViews
    Inherits Autodesk.Revit.UI.Macros.DocumentEntryPoint
    
    Public Event Startup As System.EventHandler
    
    Public Event Shutdown As System.EventHandler
    
    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>  _
    Private Sub OnStartup()
        Globals.ThisDocument1 = Me
    End Sub
    
    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>  _
    Protected Overrides Sub FinishInitialization()
        MyBase.FinishInitialization
        Me.OnStartup
        If (Not (Me.StartupEvent) Is Nothing) Then
            RaiseEvent Startup(Me, System.EventArgs.Empty)
        End If
    End Sub
    
    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>  _
    Protected Overrides Sub OnShutdown()
        If (Not (Me.ShutdownEvent) Is Nothing) Then
            RaiseEvent Shutdown(Me, System.EventArgs.Empty)
        End If
        MyBase.OnShutdown
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>  _
    Protected Overrides ReadOnly Property PrimaryCookie() As String
        Get
            Return "ThisDocument1"
        End Get
    End Property
End Class

'''
Partial Public NotInheritable Class Globals

    Private Shared _ThisDocument1 As CropViews

    Friend Shared Property ThisDocument1() As CropViews
        Get
            Return _ThisDocument1
        End Get
        Set
            If (_ThisDocument1 Is Nothing) Then
                _ThisDocument1 = Value
            Else
                Throw New System.NotSupportedException
            End If
        End Set
    End Property
End Class
