'
' Created by SharpDevelop.
' User: antoine
' Date: 12/17/2015
' Time: 1:04 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices

Public Partial Class frmRenameViews
	
	Private Searchable As New List(Of Object)()
	
	
	Public Sub New(curDoc As Document)
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		
		
		'get all views in current model file
		
		Dim collector As New clsCollectors
		Dim viewList As list(Of View)
		viewList = collector.GetAllStructuralViews(curDoc)
		
		'create list of view names for combobox
		Dim viewNameList As New List(Of String)
		
		For Each curView As View In viewList
			viewNameList.Add(curView.Name)
		Next

        'sort list 
        viewNameList.Sort(New AlphanumComparator())

        'add view names to combo box
        For Each curViewName As String In viewNameList
			lbxViews.Items.Add(curViewName)
		Next
		
		SendMessage(Me.SearchBox.Handle, &H1501, 0, "Search here")
		
	End Sub
	
	
	
	 Public Function GetViewList(curDoc As Document) As List(Of View)
        Dim collectors As New clsCollectors
        Dim viewList As New List(Of View)
        Dim i As Integer

        For i = 0 To Me.lbxViews.SelectedItems.Count - 1
            Dim SelectedViewName As String = Me.lbxViews.SelectedItems(i).ToString

            'get all views
            Dim vpList As List(Of View) = collectors.GetAllStructuralViews(curDoc)

            For Each curView As View In vpList

                If curView.Name = SelectedViewName Then
                    viewList.Add(curView)
                End If

            Next

        Next

        Return viewList

	 End Function
	 
	 Public Function GetAddedBeginning As String
	 	Return AddBeginningBox.Text
	 End Function
	 
	  Public Function GetAddedEnd As String
	 	Return AddEndBox.Text
	  End Function
	  
	   Public Function GetDeleted As String
	 	Return DeleteBox.Text
	   End Function
	   
	   Public Function GetReplaced As String
	 	Return ReplaceBox.Text
	   End Function
	    
	   Public Function Replacing As String
	 	Return ByBox.Text
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

    Private Sub frmRenameViews_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class