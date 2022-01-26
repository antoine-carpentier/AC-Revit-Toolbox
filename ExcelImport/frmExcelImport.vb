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
Imports InteropExcel = Microsoft.Office.Interop.Excel
Imports System.Drawing


Partial Public Class frmExcelImport

	Public CellList As New List(Of CellClass)

	Public Sub AddCell(IsMerged As Boolean, Content As String, Alignment As Integer, Width As Double, Height As Double, LocationX As Double, LocationY As Double, LeftBorder As Boolean, LeftBorderWidth As Integer, RightBorder As Boolean, RightBorderWidth As Integer, TopBorder As Boolean, TopBorderWidth As Integer, BottomBorder As Boolean, BottomBorderWidth As Integer)
		CellList.Add(New CellClass(IsMerged, Content, Alignment, Width, Height, LocationX, LocationY, LeftBorder, LeftBorderWidth, RightBorder, RightBorderWidth, TopBorder, TopBorderWidth, BottomBorder, BottomBorderWidth))
	End Sub

	Public Sub New(curDoc As Document)

		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()

	End Sub

	Public Sub DrawTable(curdoc As Document)

		Dim StartPoint As New XYZ(0, 0, 0)
		Dim EndPoint As New XYZ(0, 0, 0)
		Dim CurBorder As Line = Nothing
		Dim LineList As New List(Of String)

		Dim CustomScaleFactor As Double = 80 '85.333 'From excel unit system (in pixel) to view unit system (in ft)


		Dim collector As New ClsCollectors
		Dim TextTypeList As List(Of TextNoteType) = collector.getAllTextTypes(curdoc)

		Dim TextOptions As New TextNoteOptions
		TextOptions.TypeId = TextTypeList(0).Id

		'Dim minWidth As Double = TextNote.GetMinimumAllowedWidth(curdoc, TextOptions.TypeId)
		'Dim maxWidth As Double = TextNote.GetMaximumAllowedWidth(curdoc, TextOptions.TypeId)

		Dim PreviousMerge As Boolean = False
		Dim CellIndex As Integer = 0
		Dim MergeIndex As Integer = 1
		Dim MaxMerge As Integer = 1

		Dim XtextOffset As Double = 0
		Dim YTextOffset As Double = 0
		Dim TextWidth As Double = 0

		Dim LocString As String

		For Each cell As CellClass In CellList

			TextWidth = cell.Width

			If cell.IsMerged = True Then
				If MergeIndex = 1 Then
					CellIndex = CellList.IndexOf(cell)
					Do Until CellList(CellIndex + MergeIndex).IsMerged = False Or String.IsNullOrEmpty(CellList(CellIndex + MergeIndex).Content) = False
						TextWidth += CellList(CellIndex + MergeIndex).Width
						MergeIndex += 1
					Loop
					MaxMerge = MergeIndex
				Else
					MergeIndex -= 1
				End If
			End If

			If cell.Alignment = -4131 Then
				TextOptions.HorizontalAlignment = HorizontalTextAlignment.Left
				XtextOffset = -TextWidth / 2 + 3.5
			ElseIf cell.Alignment = -4152 Then
				TextOptions.HorizontalAlignment = HorizontalTextAlignment.Right
				XtextOffset = TextWidth / 2 - 3.5
			Else
				TextOptions.HorizontalAlignment = HorizontalTextAlignment.Center
				XtextOffset = 0
			End If



			If String.IsNullOrEmpty(cell.Content) Then
			Else
				Dim TextHeight As Double = MeasureContentHeight(cell.Content, TextWidth * 0.94 / (CustomScaleFactor * curdoc.ActiveView.Scale) * 12) / 72 * curdoc.ActiveView.Scale
				YTextOffset = (cell.Height / 2 * 12 / CustomScaleFactor - TextHeight / 2) * CustomScaleFactor / 12 'Calculate the offset to center the text in the cell
				StartPoint = New XYZ(cell.LocationX + TextWidth / 2 + XtextOffset, -cell.LocationY - YTextOffset, 0) / CustomScaleFactor
				Dim CurNote As TextNote = TextNote.Create(curdoc, curdoc.ActiveView.Id, StartPoint, TextWidth * 0.95 / (CustomScaleFactor * curdoc.ActiveView.Scale), cell.Content.ToUpper, TextOptions)
			End If

			'left border
			If cell.IsMerged = True And MergeIndex < MaxMerge Then
			Else
				If cell.LeftBorder = True Then
					StartPoint = New XYZ(cell.LocationX, -cell.LocationY, 0) / CustomScaleFactor
					EndPoint = New XYZ(cell.LocationX, -cell.LocationY - cell.Height, 0) / CustomScaleFactor
					LocString = StartPoint.X.ToString & ";" & StartPoint.Y.ToString & "/" & EndPoint.X.ToString & ";" & EndPoint.Y.ToString

					If LineList.Contains(LocString) Then
					Else
						CurBorder = Line.CreateBound(StartPoint, EndPoint)
						curdoc.Create.NewDetailCurve(curdoc.ActiveView, CurBorder)
						LineList.Add(LocString)
					End If
				End If
			End If

			'right border
			If cell.IsMerged = True And MergeIndex > 1 Then
			Else
				If cell.RightBorder = True Then
					StartPoint = New XYZ(cell.LocationX + cell.Width, -cell.LocationY, 0) / CustomScaleFactor
					EndPoint = New XYZ(cell.LocationX + cell.Width, -cell.LocationY - cell.Height, 0) / CustomScaleFactor
					LocString = StartPoint.X.ToString & ";" & StartPoint.Y.ToString & "/" & EndPoint.X.ToString & ";" & EndPoint.Y.ToString

					If LineList.Contains(LocString) Then
					Else
						CurBorder = Line.CreateBound(StartPoint, EndPoint)
						curdoc.Create.NewDetailCurve(curdoc.ActiveView, CurBorder)
						LineList.Add(LocString)
					End If
				End If
			End If

			'top border

			If cell.TopBorder = True Then
				StartPoint = New XYZ(cell.LocationX, -cell.LocationY, 0) / CustomScaleFactor
				EndPoint = New XYZ(cell.LocationX + cell.Width, -cell.LocationY, 0) / CustomScaleFactor
				LocString = StartPoint.X.ToString & ";" & StartPoint.Y.ToString & "/" & EndPoint.X.ToString & ";" & EndPoint.Y.ToString

				If LineList.Contains(LocString) Then
				Else
					CurBorder = Line.CreateBound(StartPoint, EndPoint)
					curdoc.Create.NewDetailCurve(curdoc.ActiveView, CurBorder)
					LineList.Add(LocString)
				End If
			End If

			'bottom border

			If cell.BottomBorder = True Then
				StartPoint = New XYZ(cell.LocationX, -cell.LocationY - cell.Height, 0) / CustomScaleFactor
				EndPoint = New XYZ(cell.LocationX + cell.Width, -cell.LocationY - cell.Height, 0) / CustomScaleFactor
				LocString = StartPoint.X.ToString & ";" & StartPoint.Y.ToString & "/" & EndPoint.X.ToString & ";" & EndPoint.Y.ToString

				If LineList.Contains(LocString) Then
				Else
					CurBorder = Line.CreateBound(StartPoint, EndPoint)
					curdoc.Create.NewDetailCurve(curdoc.ActiveView, CurBorder)
					LineList.Add(LocString)
				End If
			End If

		Next

	End Sub

	Private Function MeasureContentHeight(content As String, width As Double) As Double

		Dim graphics As Graphics = graphics.FromHwnd(IntPtr.Zero)

		'One inch is 72 font points, so 5/64" is 5/64*72 points
		Dim FontSize As Single = 5 / 64 * 72

		Dim WidthPoint As Integer = Convert.ToInt32(width) * 72 ' convert inches to points
		Dim CurFont As Font = New Font("Arial", FontSize)
		Dim size As SizeF = graphics.MeasureString(content.ToUpper, CurFont, WidthPoint)

		Return size.Height

	End Function


	Public Sub OpenExcel(ByVal FileName As String, ByVal SheetIndex As Integer)

		If IO.File.Exists(FileName) Then
			Dim Proceed As Boolean = False
			Dim xlApp As InteropExcel.Application = Nothing
			Dim xlWorkBooks As InteropExcel.Workbooks = Nothing
			Dim xlWorkBook As InteropExcel.Workbook = Nothing
			Dim xlWorkSheet As InteropExcel.Worksheet = Nothing
			Dim xlRow As InteropExcel.Range = Nothing
			Dim xlWorkSheets As InteropExcel.Sheets = Nothing
			Dim xlCell As InteropExcel.Range = Nothing
			Dim CellLocationX As Double = 0
			Dim CellLocationY As Double = 0
			xlApp = New InteropExcel.Application
			xlApp.DisplayAlerts = False
			xlWorkBooks = xlApp.Workbooks
			xlWorkBook = xlWorkBooks.Open(FileName)
			xlApp.Visible = False
			xlWorkSheets = xlWorkBook.Sheets

			xlWorkSheet = CType(xlWorkSheets(SheetIndex + 1), InteropExcel.Worksheet)
			Dim LastCell As InteropExcel.Range = xlWorkSheet.Cells.SpecialCells(InteropExcel.XlCellType.xlCellTypeLastCell, Type.Missing)

			Dim outputstring As String

			For i = 1 To LastCell.Row
				xlRow = CType(xlWorkSheet.Rows(i), InteropExcel.Range)
				If CType(xlRow.Hidden, Boolean) = False Then
					For j = 1 To LastCell.Column
						xlCell = CType(xlWorkSheet.Cells(i, j), InteropExcel.Range)
						AddCell(Convert.ToBoolean(xlCell.MergeCells), Convert.ToString(xlCell.Value), Convert.ToInt32(xlCell.HorizontalAlignment),
								Convert.ToDouble(xlCell.Width.ToString), Convert.ToDouble(xlCell.Height.ToString), CellLocationX, CellLocationY,
								BorderBool(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeLeft).LineStyle.ToString), Convert.ToInt32(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeLeft).Weight.ToString),
								BorderBool(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeRight).LineStyle.ToString), Convert.ToInt32(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeRight).Weight.ToString),
								BorderBool(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeTop).LineStyle.ToString), Convert.ToInt32(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeTop).Weight.ToString),
								BorderBool(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeBottom).LineStyle.ToString), Convert.ToInt32(xlCell.Borders(InteropExcel.XlBordersIndex.xlEdgeBottom).Weight.ToString))
						CellLocationX += Convert.ToDouble(xlCell.Width.ToString)
						outputstring += CellClassString(CellList(CellList.Count - 1))
						System.Diagnostics.Debug.WriteLine(CellClassString(CellList(CellList.Count - 1)))
					Next
					xlCell = CType(xlWorkSheet.Cells(i, 1), InteropExcel.Range)
					CellLocationY += Convert.ToDouble(xlCell.Height.ToString)
					CellLocationX = 0
				End If

			Next

			Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkSheet)
			xlWorkBook.Close()
			xlApp.UserControl = True
			xlApp.Quit()
			ReleaseComObject(xlCell)
			ReleaseComObject(xlWorkSheets)
			ReleaseComObject(xlWorkSheet)
			ReleaseComObject(xlWorkBook)
			ReleaseComObject(xlWorkBooks)
			ReleaseComObject(xlApp)
			Me.Close()
		Else
			TaskDialog.Show("Error", "'" & FileName & "' not located.")
		End If
	End Sub
	Public Sub ReleaseComObject(ByVal obj As Object)
		Try
			System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
			obj = Nothing
		Catch ex As Exception
			obj = Nothing
		End Try
	End Sub

	Private Sub FileLocBrowse_FileOk(sender As Object, e As EventArgs) Handles BrowseBtn.Click

		FileLocBrowse.RestoreDirectory = True
		FileLocBrowse.Title = "Select an Excel File"
		FileLocBrowse.Filter = "Excel Files (*.xlsx;*.xls;*.xlsm)|*.xlsm;*.xlsx;*.xls;* | All files (*.*)|*.*"


		If FileLocBrowse.ShowDialog() <> System.Windows.Forms.DialogResult.Cancel Then
			ExcelPathTextBox.Text = FileLocBrowse.FileName
			SheetListBox.Items.Clear()
			SheetListBox.Items.Add("Please Wait...")

			Dim xlApp As InteropExcel.Application = Nothing
			Dim xlWorkBooks As InteropExcel.Workbooks = Nothing
			Dim xlWorkBook As InteropExcel.Workbook = Nothing
			Dim xlWorkSheet As InteropExcel.Worksheet = Nothing
			Dim xlWorkSheets As InteropExcel.Sheets = Nothing

			xlApp = New InteropExcel.Application
			xlApp.DisplayAlerts = False
			xlWorkBooks = xlApp.Workbooks
			xlWorkBook = xlWorkBooks.Open(FileLocBrowse.FileName)
			xlApp.Visible = False
			xlWorkSheets = xlWorkBook.Sheets

			SheetListBox.Items.Clear()

			For i = 1 To xlWorkSheets.Count
				xlWorkSheet = CType(xlWorkSheets(i), InteropExcel.Worksheet)
				SheetListBox.Items.Add(xlWorkSheet.Name)
			Next

			SheetListBox.IntegralHeight = False
			SheetListBox.Height = 15 * (SheetListBox.Items.Count + 1)
			SheetListBox.IntegralHeight = True

			frmExcelImport.ActiveForm.Height = 218 + SheetListBox.Height - 49

			Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkSheet)
			xlWorkBook.Close()
			xlApp.UserControl = True
			xlApp.Quit()
			ReleaseComObject(xlWorkSheets)
			ReleaseComObject(xlWorkSheet)
			ReleaseComObject(xlWorkBook)
			ReleaseComObject(xlWorkBooks)
			ReleaseComObject(xlApp)

		Else
			ExcelPathTextBox.Text = ""
		End If

	End Sub

	Private Function BorderBool(linestyle As String) As Boolean
		If Convert.ToInt32(linestyle) = -4142 Then
			Return False
		Else
			Return True
		End If

	End Function

	Private Function CellClassString(CellClass As CellClass) As String
		Return String.Format("AddCell({0}, ""{1}"", {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})", CellClass.IsMerged, CellClass.Content, CellClass.Alignment, CellClass.Width, CellClass.Height, CellClass.LocationX, CellClass.LocationY, CellClass.LeftBorder, CellClass.LeftBorderWidth, CellClass.RightBorder, CellClass.RightBorderWidth, CellClass.TopBorder, CellClass.TopBorderWidth, CellClass.BottomBorder, CellClass.BottomBorderWidth)
	End Function

	Private Sub OKBtn_Click(sender As Object, e As EventArgs) Handles OKBtn.Click
		If String.IsNullOrEmpty(ExcelPathTextBox.Text) Then
			TaskDialog.Show("Warning", "No Excel File Selected.")
		ElseIf SheetListBox.SelectedIndex = -1 Then
			TaskDialog.Show("Warning", "No WorkSheet Selected. Select one from the drop-down list.")
		Else
			Me.DialogResult = DialogResult.OK
		End If

	End Sub
End Class
