
Imports System
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Diagnostics
Imports Autodesk.Revit.UI

Public Partial Class frmSheetMaker
	Public Sub New(curDoc As Autodesk.Revit.DB.Document)
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		Dim curTblocks As list(Of String)
        Dim collectors As New ClsCollectors

        'get titleblocks in current model and output to combo box
        curTblocks = collectors.GetAllTitleblockNames(curDoc)
        For Each tblock As String In curTblocks
			Me.cmbTitleblock.Items.Add(tblock)
		Next

        'set combo box to first titleblock
        Me.cmbTitleblock.SelectedIndex = 0

        'set btnCreateCSV tooltip
        Me.ttCreateCSV.ToolTipTitle = "Create Default CSV File"
		
	End Sub

    Sub BtnBrowseClick(sender As Object, e As System.EventArgs) Handles btnBrowse.Click
        'set open file dialog settings
        ofdCSVFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        ofdCSVFile.Title = "Select a CSV file"
        ofdCSVFile.FileName = ""
        ofdCSVFile.Filter = "CSV Files (*.csv)|*.csv"

        If ofdCSVFile.ShowDialog() <> System.Windows.Forms.DialogResult.Cancel Then
            tbxCSVFile.Text = ofdCSVFile.FileName
        Else
            tbxCSVFile.Text = ""
        End If
    End Sub

    Sub OpenFileDialog1FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdCSVFile.FileOk

    End Sub

    Sub BtnProcessClick(sender As Object, e As System.EventArgs) Handles btnProcess.Click

        'validate data
        If tbxCSVFile.Text = "" Then
            'prompt user to select file
            TaskDialog.Show("Select CSV File", "Please select a CSV file.")
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        ElseIf cmbTitleblock.SelectedItem.ToString = "" Then
            TaskDialog.Show("Select Title block", "Please select a title block.")
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        Else
            'close dialog
            Me.Close()
        End If

    End Sub

    Public Function GetCSVFile() As String
        'return selected CSV file
        Return Me.tbxCSVFile.Text
    End Function

    Public Function GetTblock() As String()
        'return selected tblock
        Dim tBlockString As String = Me.cmbTitleblock.SelectedItem.ToString
        Dim tmpArr As String() = tBlockString.Split(New Char() {"|"c})

        Dim tBlockArr(1) As String
        tBlockArr(0) = tmpArr(0).Trim
        tBlockArr(1) = tmpArr(1).Trim

        Return tBlockArr
    End Function

    Sub BtnCreateCSVClick(sender As Object, e As System.EventArgs) Handles btnCreateCSV.Click
        'launch file save dialog to save default CSV file
        sfdCreateCSV.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        sfdCreateCSV.Title = "Select location to save default CSV file"
        sfdCreateCSV.FileName = "default.csv"
        sfdCreateCSV.Filter = "CSV Files (*.csv)|*.csv"

        If sfdCreateCSV.ShowDialog <> System.Windows.Forms.DialogResult.Cancel Then
            Dim saveFile As String = sfdCreateCSV.FileName

            'create CSV file
            Dim headerString As String = "Sheet #, Sheet Name"
            Dim Line1 As String = "S000,GENERAL NOTES"
            Dim Line2 As String = "S101,TEST SHEET 1"
            Dim Line3 As String = "S102,TEST SHEET 2"

            'check if file exists - if so then delete
            If File.Exists(saveFile) = True Then
                'delete file
                File.Delete(saveFile)
            End If

            Dim collectors As New ClsCollectors

            'write CSV data to file
            collectors.WriteCSVFile(saveFile, headerString)
            collectors.WriteCSVFile(saveFile, Line1)
            collectors.WriteCSVFile(saveFile, Line2)
            collectors.WriteCSVFile(saveFile, Line3)

            'open CSV file if possible
            Try
                System.Diagnostics.Process.Start(saveFile)

            Catch ex As Exception
                Debug.Print("Could not open file.")
            End Try

        End If
    End Sub

    Private Sub frmSheetMaker_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

End Class