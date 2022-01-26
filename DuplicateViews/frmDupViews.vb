
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices

Partial Public Class FrmDupViews

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

        'set number of duplicates control box

        For i As Integer = 1 To 100
            Me.cmbNumDupes.Items.Add(i)
        Next

        'set selected number of dupes
        Me.cmbNumDupes.SelectedIndex = 0

        'set selected duplicate option
        Me.rbDup.Checked = True

        'get all structural views in current model file
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

        Next

        'sort views alpahbetically
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
            Me.lbxViews.Items.Add(tmpStructView.StructuralViewName)
        Next


        SendMessage(Me.SearchBox.Handle, &H1501, 0, "Search here")

    End Sub

    Public Function GetSelectedViews(curdoc As Document) As List(Of View)
        Dim viewList As New List(Of View)
        Dim collectors As New ClsCollectors
        Dim i As Integer

        For i = 0 To Me.lbxViews.SelectedItems.Count - 1
            Dim SelectedViewName As String = Me.lbxViews.SelectedItems(i).ToString

            'get all views
            Dim vpList As List(Of View) = collectors.GetAllStructuralViews(curdoc)

            For Each curView As View In vpList
                Dim tmpView As View = curView

                If tmpView.Name = SelectedViewName Then
                    viewList.Add(curView)
                End If

            Next

        Next

        Return viewList
    End Function

    Public Function GetNumDupes() As Integer
        'return selected number of duplicates
        Return CInt(Me.cmbNumDupes.SelectedItem.ToString)
    End Function

    Public Function GetDuplicateType() As String
        'return duplicate type value
        If Me.rbDupDetailing.Checked = True Then
            Return "detailing"
        ElseIf Me.rbDupDependent.Checked = True Then
            Return "dependent"
        Else
            Return "duplicate"
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

    Private Sub frmDupViews_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class

