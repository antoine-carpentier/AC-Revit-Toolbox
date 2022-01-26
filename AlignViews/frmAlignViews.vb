'
'
' Created by SharpDevelop.
' User: antoine
' Date: 3/27/2019
' Time: 2:58 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

Public Partial Class frmAlignViews
    Public Structure structVP
        'structure for viewport information
        Public vpViewName As String
        Public vpSheetNum As String
        Public vport As Viewport
        Public vpDisplay As String
    End Structure

    Private Searchable As New List(Of Object)()

    Public Sub New(curDoc As Document)
		
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'		
		'loop through viewports and add to list of structures
		Dim vpList As New List(Of structVP)
		Dim collectors As New ClsCollectors

        For Each curVP As Viewport In collectors.getAllViewports(curDoc)

            Dim tmpView As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)
			Dim tmpSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)
            Dim tmpVP As structVP

            tmpVP.vport = curVP
			tmpVP.vpViewName = tmpView.Name
            tmpVP.vpSheetNum = tmpSheet.SheetNumber
            tmpVP.vpDisplay = tmpVP.vpSheetNum & ", " & tmpVP.vpViewName

            'add to list
            If tmpView.ViewType =ViewType.EngineeringPlan then
				vpList.Add(tmpVP)
			End If

		Next
		
		'order list by sheet #
		vpList = vpList.OrderBy(Function(x) x.vpSheetNum).ToList()
		
		
		
		'output to form
		For Each tmpVP As structVP In vpList
            Me.cmbPrimary.Items.Add(tmpVP.vpDisplay)
            Me.lbxViews.Items.Add(tmpVP.vpDisplay)
        Next

        'preselect primary
        Me.cmbPrimary.SelectedIndex = 0

        SendMessage(Me.SearchBox.Handle, &H1501, 0, "Search here")

    End Sub


    Public Function getPrimaryViewPort(curDoc As Document) As Viewport

        Dim viewName As String = Me.cmbPrimary.SelectedItem.ToString
        Dim tmpVP As structVP


        'get all viewports
        Dim collectors As New ClsCollectors
        Dim vpList As List(Of Viewport) = collectors.GetAllViewports(curDoc)

        For Each curVP As Viewport In vpList
            Dim tmpSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)
            Dim tmpView As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)

            tmpVP.vport = curVP
            tmpVP.vpViewName = tmpView.Name
            tmpVP.vpSheetNum = tmpSheet.SheetNumber
            tmpVP.vpDisplay = tmpVP.vpSheetNum & ", " & tmpVP.vpViewName

            If viewName = tmpVP.vpDisplay Then
                Debug.Print("found match")
                Return curVP

            End If
        Next

        Return Nothing
    End Function

    Public Function getViewList(curDoc As Document) As List(Of viewport)
		Dim collectors As New clsCollectors
		Dim viewList As New List(Of Viewport)
        Dim i As Integer
        Dim tmpVP As structVP

        For i = 0 To Me.lbxViews.SelectedItems.Count - 1
            Dim curItem As String = Me.lbxViews.SelectedItems(i).ToString

            'get all viewports
            Dim vpList As List(Of Viewport) = collectors.getAllViewports(curDoc)
			
			For Each curVP As Viewport In vpList
                Dim tmpSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)
                Dim tmpView As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)

                tmpVP.vport = curVP
                tmpVP.vpViewName = tmpView.Name
                tmpVP.vpSheetNum = tmpSheet.SheetNumber
                tmpVP.vpDisplay = tmpVP.vpSheetNum & ", " & tmpVP.vpViewName

                If curItem = tmpVP.vpDisplay And tmpView.ViewType = ViewType.EngineeringPlan Then
                    'add to list
                    viewList.Add(curVP)
                End If
            Next
			
		Next
		
		Return viewList
		
	End Function

    Public Function CopyCropCheckBox() As Boolean

        If CropCheckBox.Checked = True Then
            Return True
        Else Return False
        End If

    End Function

    Private Sub List()
        For i = 0 To Me.lbxViews.Items.Count - 1
            Dim ViewName As String = Me.lbxViews.Items(i).ToString
            'add to list
            Searchable.Add(ViewName)
        Next
    End Sub

    Private Sub Refreshlist()
        lbxViews.Items.Clear()
        lbxViews.Items.AddRange(Searchable.Where(Function(Searchable) Searchable.ToString().ToLower.Contains(SearchBox.Text.ToLower)).ToArray())
    End Sub


    Private Sub frmAlignViews_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        List()
        Refreshlist()
    End Sub


    Private Sub SearchBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchBox.TextChanged
        Refreshlist()
    End Sub

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function

    Private Sub frmAlignViews_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class
