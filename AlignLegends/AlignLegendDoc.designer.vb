'
' Created by SharpDevelop.
' User: antoine
' Date: 3/27/2015
' Time: 2:51 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Option Strict Off
Option Explicit On

'''
<Global.System.CLSCompliantAttribute(False)>
Partial Public NotInheritable Class ThisLegendDocument
    Inherits Autodesk.Revit.UI.Macros.DocumentEntryPoint

    Public Event Startup As System.EventHandler

    Public Event Shutdown As System.EventHandler

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>
    Private Sub OnStartup()
        Globals.ThisLegDoc = Me
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>
    Protected Overrides Sub FinishInitialization()
        MyBase.FinishInitialization
        Me.OnStartup()

        If (Not (Me.StartupEvent) Is Nothing) Then
            RaiseEvent Startup(Me, System.EventArgs.Empty)
        End If
    End Sub

    '''
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>
    Protected Overrides Sub OnShutdown()
        If (Not (Me.ShutdownEvent) Is Nothing) Then
            RaiseEvent Shutdown(Me, System.EventArgs.Empty)
        End If
        MyBase.OnShutdown
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)>
    Protected Overrides ReadOnly Property PrimaryCookie() As String
        Get
            Return "ThisDocument"
        End Get
    End Property
End Class

'''
Partial Public NotInheritable Class Globals

    Private Shared _ThisLegDoc As ThisLegendDocument

    Friend Shared Property ThisLegDoc() As ThisLegendDocument
        Get
            Return _ThisLegDoc
        End Get
        Set
            If (_ThisLegDoc Is Nothing) Then
                _ThisLegDoc = Value
            Else
                Throw New System.NotSupportedException
            End If
        End Set
    End Property
End Class
