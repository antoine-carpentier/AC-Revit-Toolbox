
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

Public Partial Class frmAutomateRevisions
    Public Structure StructRev 'structure for Revision information
        Public RevName As String
        Public RevNumber As Integer
        Public Rev As Revision
        Public RevDisplay As String
    End Structure

    Private Searchable As New List(Of Object)()

    Public Structure StructSh 'structure for Sheet information
		Public ShName As String
		Public ShNumber As String
        Public Sh As ViewSheet
        Public ShDisplay As String
    End Structure
	
	Public Sub New(curDoc As Document)
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()

		'loop through revisions and add to list of structures
		Dim RevList As New List(Of StructRev)
		Dim collectors As New clsCollectors
		
		For Each curRev As Revision In collectors.GetAllRevisions(curDoc)
			Dim tmpStructRev As StructRev
			Dim tmpRev As Revision = curRev
			
			tmpStructRev.Rev = curRev
            tmpStructRev.RevName = curRev.Description
            tmpStructRev.RevNumber = curRev.SequenceNumber
            tmpStructRev.RevDisplay = curRev.SequenceNumber.ToString & " - " & curRev.Description

            'add to list
            RevList.Add(tmpStructRev)
		Next
		
		'loop through sheets and add to list of structures
		Dim ShList As New List(Of StructSh)
		
		For Each curSh As ViewSheet In collectors.getAllSheets(curDoc)
			Dim tmpStructSh As StructSh
			Dim tmpSh As ViewSheet = curSh
			
			tmpStructSh.Sh = curSh
			tmpStructsh.ShName = curSh.Name
			tmpStructSh.ShNumber= curSh.SheetNumber
            tmpStructSh.ShDisplay = curSh.SheetNumber & " - " & curSh.Name
            'add to list
            ShList.Add(tmpStructSh)
		Next
		
		'order list by sheet #
		ShList = ShList.OrderBy(Function(x) x.ShNumber).ToList()
			
		'output to form
		For Each tmpStructRev As StructRev In RevList
            Me.cmbPrimary.Items.Add(tmpStructRev.RevDisplay)

        Next
		
		For Each tmpStructsh As Structsh In ShList
            Me.lbxViews.Items.Add(tmpStructsh.ShDisplay)

        Next

        'preselect primary
        Me.cmbPrimary.SelectedIndex = 0

        SendMessage(Me.SearchBox.Handle, &H1501, 0, "Search here")

    End Sub
	
	Public Function OnOrOff(curdoc As Document) As Boolean
		If Me.radioButton1.Checked = True Then
			Return True 
			Else return False
		End If
		
	End Function
	
	Public Function AllSheetSelected(curdoc As document) As Boolean
		If Me.checkBox3.Checked = True Then
			Return True
			Else return False
		End If
		
	End Function

    Public Function GetPrimaryBox(curDoc As Document) As Revision
        Dim SelectedRev As String = Me.cmbPrimary.SelectedItem.ToString


        'get all revisisons
        Dim collectors As New ClsCollectors
        Dim RevList As List(Of Revision) = collectors.GetAllRevisions(curDoc)

        Dim tmpStructRev As StructRev

        For Each curRev As Revision In RevList

            tmpStructRev.Rev = curRev
            tmpStructRev.RevName = curRev.Description
            tmpStructRev.RevNumber = curRev.SequenceNumber
            tmpStructRev.RevDisplay = curRev.SequenceNumber.ToString & " - " & curRev.Description

            If SelectedRev = tmpStructRev.RevDisplay Then
                Return tmpStructRev.Rev
            End If

        Next

        Return Nothing
    End Function

    Public Function GetSheetList(curDoc As Document) As List(Of ViewSheet)
        Dim collectors As New ClsCollectors
        Dim SheetList As New List(Of ViewSheet)
        Dim i As Integer

        For i = 0 To Me.lbxViews.SelectedItems.Count - 1
            Dim SelectedSheetName As String = Me.lbxViews.SelectedItems(i).ToString


            'get all views
            Dim vpList As List(Of ViewSheet) = collectors.GetAllSheets(curDoc)
            Dim tmpStructSh As StructSh

            For Each curSh As ViewSheet In vpList

                tmpStructSh.Sh = curSh
                tmpStructSh.ShName = curSh.Name
                tmpStructSh.ShNumber = curSh.SheetNumber
                tmpStructSh.ShDisplay = curSh.SheetNumber & " - " & curSh.Name

                If SelectedSheetName = tmpStructSh.ShDisplay Then
                    SheetList.Add(tmpStructSh.Sh)
                End If

            Next

        Next

        Return SheetList

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

    Private Sub frmAutoRev_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class
