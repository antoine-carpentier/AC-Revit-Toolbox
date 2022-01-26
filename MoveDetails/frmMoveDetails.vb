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

Public Partial Class frmMoveDetails
	Public Structure structSheet
		'structure for viewport information
		Public SheetName As String
		Public SheetNum As String
		Public Sheet As ViewSheet
		Public ShDisplay As String
	End Structure
	
	Public Sub New(curDoc As Document)
		
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'		
		
		Dim ShList As New List(Of structSheet)
		Dim collectors As New ClsCollectors

        For Each CurSh As ViewSheet In collectors.GetAllSheets(curDoc)
            Dim tmpSh As structSheet

            tmpSh.SheetName = CurSh.Name
            tmpSh.SheetNum = CurSh.SheetNumber
            tmpSh.ShDisplay = tmpSh.SheetNum & " - " & tmpSh.SheetName
            tmpSh.Sheet = CurSh

            'add to list
            ShList.Add(tmpSh)
        Next

        'order list by sheet #
        ShList = ShList.OrderBy(Function(x) x.SheetNum).ToList()
		
		
		
		'output to form
		For Each tmpSh As structSheet In ShList
			Me.lbxViews.Items.Add(tmpSh.ShDisplay)
		Next

	End Sub
	
	
	Public Function getOneSheet(curDoc As Document) As ViewSheet
		Dim collectors As New clsCollectors
		Dim viewList As New List(Of Viewport)

			Dim curItem As String = Me.lbxViews.SelectedItem.ToString



        'get all viewports
        Dim ShList As List(Of ViewSheet) = collectors.GetAllSheets(curDoc)

        For Each curSh As ViewSheet In ShList

            Dim tmpSh As structSheet

            tmpSh.SheetName = curSh.Name
            tmpSh.SheetNum = curSh.SheetNumber
            tmpSh.ShDisplay = tmpSh.SheetNum & " - " & tmpSh.SheetName
            tmpSh.Sheet = curSh

            If curItem = tmpSh.ShDisplay Then

                Debug.Print("found match")
                Return tmpSh.Sheet
            End If
        Next


        Return Nothing
		
	End Function
	
	
End Class
