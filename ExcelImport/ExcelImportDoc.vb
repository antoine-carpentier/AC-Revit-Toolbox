
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace Excel


    <Transaction(TransactionMode.Manual)> Partial Public Class ExcelImport
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'open form
            Using curForm As New frmExcelImport(curDoc)
                'show form
                curForm.ShowDialog()

                If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                    Return Result.Cancelled
                Else
                    'If String.IsNullOrEmpty(curForm.ExcelPathTextBox.Text) Then
                    '    TaskDialog.Show("Warning", "No Excel File Selected.")
                    'ElseIf curForm.SheetListBox.SelectedIndex = -1 Then
                    '    TaskDialog.Show("Warning", "No WorkSheet Selected. Select one from the drop-down list.")
                    'Else
                    'start transaction
                    Using curTrans As New Transaction(curDoc, " Excel Table Import")
                            If curTrans.Start = TransactionStatus.Started Then

                                curForm.OpenExcel(curForm.FileLocBrowse.FileName, curForm.SheetListBox.SelectedIndex)
                                curForm.DrawTable(curDoc)

                            End If

                            'commit changes
                            curTrans.Commit()
                            Return Result.Succeeded
                        End Using
                    'End If
                End If

            End Using

        End Function

    End Class
End Namespace