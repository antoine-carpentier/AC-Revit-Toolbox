'
' Created by SharpDevelop.
' User: antoine
' Date: 3/27/2015
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

Partial Public Class frmAlignLegends

    Private Searchable As New List(Of Object)()

    Public Structure structVP
        'structure for viewport information
        Public vpViewName As String
        Public vpSheetNum As String
        Public vport As Viewport
        Public vpDisplay As String
    End Structure

    Public Sub New(curDoc As Document)

        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        '
        ' TODO : Add constructor code after InitializeComponents
        '		
        'loop through viewports and add to list of structures
        Dim vpList As New List(Of structVP)
        Dim collectors As New ClsCollectors

        For Each curVP As Viewport In collectors.GetAllViewports(curDoc)
            Dim tmpVP As structVP
            Dim tmpView As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)
            Dim tmpSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)

            tmpVP.vport = curVP
            tmpVP.vpViewName = tmpView.Name
            tmpVP.vpSheetNum = tmpSheet.SheetNumber
            tmpVP.vpDisplay = tmpVP.vpSheetNum & ", " & tmpVP.vpViewName

            'add to list

            If tmpView.ViewType = ViewType.Legend Then
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

        'preselect primary and alignment
        Me.cmbPrimary.SelectedIndex = 0
        Me.cmbAlignType.SelectedItem = "Top Left"

        SendMessage(Me.SearchBox.Handle, &H1501, 0, "Search here")

    End Sub


    Public Function getAlignmentType() As String
        Return Me.cmbAlignType.SelectedItem.ToString

    End Function

    Public Function getPrimaryView(curDoc As Document) As Viewport
        Dim SelectedviewName As String = Me.cmbPrimary.SelectedItem.ToString


        'get all viewports
        Dim collectors As New ClsCollectors
        Dim vpList As List(Of Viewport) = collectors.GetAllViewports(curDoc)


        For Each curVP As Viewport In vpList
            Dim tmpSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)
            Dim tmpView As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)
            Dim tmpVp As structVP

            tmpVp.vport = curVP
            tmpVp.vpViewName = tmpView.Name
            tmpVp.vpSheetNum = tmpSheet.SheetNumber
            tmpVp.vpDisplay = tmpVp.vpSheetNum & ", " & tmpVp.vpViewName

            If SelectedviewName = tmpVp.vpDisplay Then
                Debug.Print("found match!")
                Return curVP
            End If
        Next

        Return Nothing
    End Function

    Public Function getViewList(curDoc As Document) As List(Of Viewport)
        Dim collectors As New ClsCollectors
        Dim viewList As New List(Of Viewport)
        Dim i As Integer

        For i = 0 To Me.lbxViews.SelectedItems.Count - 1
            Dim curItem As String = Me.lbxViews.SelectedItems(i).ToString


            'get all viewports
            Dim vpList As List(Of Viewport) = collectors.GetAllViewports(curDoc)

            For Each curVP As Viewport In vpList

                Dim tmpSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)
                Dim tmpView As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)
                Dim tmpVp As structVP

                tmpVp.vport = curVP
                tmpVp.vpViewName = tmpView.Name
                tmpVp.vpSheetNum = tmpSheet.SheetNumber
                tmpVp.vpDisplay = tmpVp.vpSheetNum & ", " & tmpVp.vpViewName

                If curItem = tmpVp.vpDisplay And tmpView.ViewType = ViewType.Legend Then
                    'add to list
                    Debug.Print("found match")
                    viewList.Add(curVP)
                End If
            Next

        Next

        Return viewList

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

    Private Sub frmAlignLegends_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class
