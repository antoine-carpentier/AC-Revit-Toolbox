Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System.Windows.Forms


Public Class frmRenameSections

    Public Structure StructSection 'structure for Structural view information
        Public SectionName As String
        Public SectiontmpName As String
        Public SectionNewName As String
        Public Section As Autodesk.Revit.DB.View
        Public SectionId As String
    End Structure

    Public Sub New(curDoc As Document)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        '
        ' TODO : Add constructor code after InitializeComponents
        '		
        'loop through viewports and add to list of structures
        Dim SectionNameList As New List(Of StructSection)
        Dim collectors As New ClsCollectors
        Dim NumberCounter As Integer = 1
        Dim RowCount As Integer = 0
        Dim row As String() = New String() {"1", "Product 1"}


        'Set up DataGrid
        dataGridViewSection.ColumnCount = 4
        dataGridViewSection.Columns(0).Name = "Current"
        dataGridViewSection.Columns(1).Name = "New"
        dataGridViewSection.Columns(0).HeaderText = "Current"
        dataGridViewSection.Columns(1).HeaderText = "New"
        dataGridViewSection.Columns(2).Name = "TmpName"
        DataGridViewSection.Columns(3).Name = "ElementId"
        DataGridViewSection.Columns(2).HeaderText = "tmp"
        DataGridViewSection.Columns(3).HeaderText = "ID"

        DataGridViewSection.AllowUserToAddRows = False
        DataGridViewSection.Columns(2).Visible = False
        DataGridViewSection.Columns(3).Visible = False

        DataGridViewSection.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        'random number generator for tmp section names to avoid conflict
        Static Generator As System.Random = New System.Random()

        For Each curSection As Autodesk.Revit.DB.View In collectors.getAllSections(curDoc)
            Dim tmpStructSection As StructSection

            Dim OnSheet As Boolean = curSection.Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER).HasValue


            'Get the sheet name and detail number of all sections
            Dim SectionNumberString As String = curSection.Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).AsString
            Dim SectionSheet As String = curSection.Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER).AsString

            Dim IsNumber As Boolean = IsNumeric(SectionNumberString)
            Dim SectionNumber As New Integer

            'Format detail number if detail number is numeric
            If IsNumber = True Then
                'If Convert.ToInt64(SectionNumberString) = Int(Convert.ToInt64(SectionNumberString)) Then
#Disable Warning BC42018 ' Operands of type Object used for operator
                If SectionNumberString = Int(SectionNumberString) Then
#Enable Warning BC42018 ' Operands of type Object used for operator
                    SectionNumber = Convert.ToInt32(SectionNumberString)
                    SectionNumberString = SectionNumber.ToString("00")
                End If
            End If

            tmpStructSection.Section = curSection
            tmpStructSection.SectionId = curSection.UniqueId
            tmpStructSection.SectionName = curSection.Name
            If tmpStructSection.SectionName.StartsWith("SW") OrElse tmpStructSection.SectionName.StartsWith("sw") Then
                tmpStructSection.SectiontmpName = tmpStructSection.SectionName
                tmpStructSection.SectionNewName = tmpStructSection.SectionName
            Else

                If tmpStructSection.SectionName.StartsWith("S") OrElse tmpStructSection.SectionName.StartsWith("s") Then
                    tmpStructSection.SectiontmpName = "S" & Generator.Next(1000, 999999999).ToString
                    If OnSheet = True Then
                        tmpStructSection.SectionNewName = SectionSheet & "/" & SectionNumberString
                    Else
                        tmpStructSection.SectionNewName = "Section " & NumberCounter
                        NumberCounter = NumberCounter + 1
                    End If
                Else
                    tmpStructSection.SectiontmpName = tmpStructSection.SectionName
                    tmpStructSection.SectionNewName = tmpStructSection.SectionName
                End If

            End If

            row = New String() {tmpStructSection.SectionName, tmpStructSection.SectionNewName, tmpStructSection.SectiontmpName, tmpStructSection.SectionId.ToString}
            DataGridViewsection.Rows.Add(row)

            For Each RowX As DataGridViewRow In DataGridViewSection.Rows
                If RowX.Cells(0).Value.ToString <> RowX.Cells(1).Value.ToString Then
                    RowX.DefaultCellStyle.BackColor = ForeColor.LightGray
                End If
            Next

            'add to list
            SectionNameList.Add(tmpStructSection)
        Next


    End Sub




    Public Function getAllSectionInfos(curDoc As Document) As List(Of StructSection)
        Dim collectors As New ClsCollectors
        Dim SectionInfoList As New List(Of StructSection)
        Dim tmpStructSection As StructSection



        For Each Row As DataGridViewRow In DataGridViewSection.rows

            tmpStructSection.SectionName = Row.Cells(0).Value.ToString
            tmpStructSection.SectionNewName = Row.Cells(1).Value.ToString
            tmpStructSection.SectiontmpName = Row.Cells(2).Value.ToString
            tmpStructSection.SectionId = Row.Cells(3).Value.ToString


            SectionInfoList.Add(tmpStructSection)

        Next

        Return SectionInfoList

    End Function

    Private Sub frmRenameSections_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class