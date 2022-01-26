
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Public Module mSheetMaker
    Public Structure StructSheet
        'structure for sheet information
        Public sheetNum As String
        Public sheetName As String
        'Public viewName As String
    End Structure

    Public Sub RunSheetMaker(curDoc As Document)
        'open form
        Using curForm As New frmSheetMaker(curDoc)
            'show form
            curForm.ShowDialog()

            If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                'Result.Cancelled
            Else
                Dim vPortName As String = "Viewport 1"

                'create the sheets
                MakeSheets(curDoc, curForm.GetTblock, vPortName, curForm.GetCSVFile)

            End If
        End Using
    End Sub

    Public Sub MakeSheets(curDoc As Document, tblockName As String(), vPortName As String, CSVFile As String)
        'arguments are: document, title block to use for new sheets, viewport type for views and path to CSV file with sheet names, numbers and views

        'check if title block exists
        Dim tblock As FamilySymbol

        Dim collectors As New ClsCollectors

        If collectors.DoesTblockExist(curDoc, tblockName) = True Then
            'get tblock
            tblock = collectors.GetTitleblock(curDoc, tblockName)
        Else
            TaskDialog.Show("alert", "Could not find titleblock")
            Exit Sub
        End If

        'success and failures
        Dim m_s As New List(Of String)
        Dim m_f As New List(Of String)

        'check if CSV file exists
        If System.IO.File.Exists(CSVFile) = False Then
            'file doesn't exist - abort
            TaskDialog.Show("Error", "The CSV file " & CSVFile & " cannot be found.")
            Exit Sub
        End If

        'transaction
        Using t As New Transaction(curDoc, "Create Sheets")
            If t.Start = TransactionStatus.Started Then
                'the sheet object
                Dim m_vs As ViewSheet


                'read CSV file and create sheet based on info
                Dim sheetList As IList(Of StructSheet)
                sheetList = collectors.ReadCSV(CSVFile, True)
                Dim ExistingSheet As List(Of ViewSheet) = collectors.GetAllSheets(curDoc)
                Dim ExistShNumber As New List(Of String)
                For Each Sh As ViewSheet In ExistingSheet
                    ExistShNumber.Add(Sh.SheetNumber)
                Next

                'create sheet for each sheet in list
                For Each curSheet As StructSheet In sheetList
                    'Try
                    If ExistShNumber.Contains(curSheet.sheetNum) = False Then

                        'create sheets
                        m_vs = ViewSheet.Create(curDoc, tblock.Id)
                        m_vs.Name = curSheet.sheetName
                        m_vs.SheetNumber = curSheet.sheetNum

                        'record success
                        m_s.Add("Sheet " & m_vs.SheetNumber & " created " & vbCr)
                    Else
                        'record sheet already existing
                        m_s.Add("Sheet " & curSheet.sheetNum & " already exists " & vbCr)
                        '	    			
                        '					Catch ex2 As Exception
                        '						'record failure
                        '						m_f.Add("sheet error: " & ex2.Message)

                    End If
                    'End Try
                Next

                'commit
                t.Commit()

                'report sheets created
                If m_s.Count > 0 Then
                    Using m_td As New TaskDialog("Success!!")
                        m_td.MainInstruction = "Created Sheets:"
                        For Each x In m_s
                            m_td.MainContent += x
                        Next

                        'show dialog
                        m_td.Show()

                    End Using
                End If

                If m_f.Count > 0 Then
                    Using m_td2 As New TaskDialog("Failures")
                        m_td2.MainInstruction = "Problems:"
                        For Each x In m_f
                            m_td2.MainContent += x
                        Next

                        'show dialog
                        m_td2.Show()

                    End Using
                End If
            End If
        End Using
    End Sub
End Module
