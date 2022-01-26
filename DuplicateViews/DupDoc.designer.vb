'
' Created by SharpDevelop.
' User: antoine
' Date: 10/2/2015
' Time: 10:35 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Option Strict Off
Option Explicit On

'''
<Global.System.CLSCompliantAttribute(false)>  _
Partial Public NotInheritable Class DupDoc
    Inherits Autodesk.Revit.UI.Macros.DocumentEntryPoint
    
    Public Event Startup As System.EventHandler
    
    Public Event Shutdown As System.EventHandler
    
    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>  _
    Private Sub OnStartup()
        Globals.ThisDocument = Me
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
            Return "ThisDocument"
        End Get
    End Property
End Class

'''
Partial Public NotInheritable Class Globals

    Private Shared _ThisDoc As DupDoc

    Friend Shared Property ThisDocument() As DupDoc
        Get
            Return _ThisDoc
        End Get
        Set
            If (_ThisDoc Is Nothing) Then
                _ThisDoc = Value
            Else
                Throw New System.NotSupportedException
            End If
        End Set
    End Property
End Class
