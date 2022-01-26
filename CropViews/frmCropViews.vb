
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

Partial Public Class frmCropViews
    Public Structure StructView 'structure for Structural view information
        Public StructuralViewName As String
        Public v As View
    End Structure

    Private Searchable As New List(Of Object)()

    Public Sub New(curDoc As Document)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        '
        ' TODO : Add constructor code after InitializeComponents
        '		
        'loop through viewports and add to list of structures
        Dim viewListTmp As New List(Of StructView)
        Dim viewList As New List(Of StructView)
        Dim ViewListName As New List(Of String)
        Dim collectors As New ClsCollectors

        For Each curView As View In collectors.GetAllStructuralViews(curDoc)

            Dim tmpView As View = curView
            Dim tmpStructView As StructView

            tmpStructView.v = curView
            tmpStructView.StructuralViewName = curView.Name

            'add to list
            viewListTmp.Add(tmpStructView)
            ViewListName.Add(tmpStructView.StructuralViewName)


            'viewList = viewList.OrderBy(Function(x) x.StructuralViewName).ToList()
        Next

        'sort views alpahbetically
        'ViewListName.Sort(New NaturalComparer)
        ViewListName.Sort(New AlphanumComparator())

        For Each VName As String In ViewListName
            For Each tmpStructView As StructView In viewListTmp
                If tmpStructView.StructuralViewName = VName Then
                    viewList.Add(tmpStructView)
                End If
            Next
        Next


        'output to form
        For Each tmpStructView As StructView In viewList
            Me.cmbPrimary.Items.Add(tmpStructView.StructuralViewName)
            Me.lbxViews.Items.Add(tmpStructView.StructuralViewName)
        Next

        'preselect primary
        Me.cmbPrimary.SelectedIndex = 0

        SendMessage(Me.SearchBox.Handle, &H1501, 0, "Search here")

    End Sub



    Public Function GetPrimaryView(curDoc As Document) As View
        Dim viewName As String = Me.cmbPrimary.SelectedItem.ToString

        'get all views
        Dim collectors As New ClsCollectors
        Dim vpList As List(Of View) = collectors.GetAllStructuralViews(curDoc)

        For Each curView As View In vpList

            Dim tmpView As View = curView

            If tmpView.Name = viewName Then
                Return curView
            End If

        Next

        Return Nothing
    End Function

    Public Function GetViewList(curDoc As Document) As List(Of View)
        Dim collectors As New ClsCollectors
        Dim viewList As New List(Of View)
        Dim i As Integer

        For i = 0 To Me.lbxViews.SelectedItems.Count - 1
            Dim SelectedViewName As String = Me.lbxViews.SelectedItems(i).ToString

            'get all views
            Dim vpList As List(Of View) = collectors.GetAllStructuralViews(curDoc)

            For Each curView As View In vpList
                Dim tmpView As View = curView

                If tmpView.Name = SelectedViewName Then
                    viewList.Add(curView)
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

    Private Sub frmCropViews_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class
