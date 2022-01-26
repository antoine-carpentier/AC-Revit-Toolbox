
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace Revisions

    <Transaction(TransactionMode.Manual)> Partial Public Class AutoRev
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            'Get the current document
            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'set counter 
            Dim counter As Integer = 0

            'check if this model file has views
            If DoesModelHaveRevisions(curDoc) = True Then
                'open form
                Using curForm As New frmAutomateRevisions(curDoc)
                    'show form
                    curForm.ShowDialog()

                    If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                        Return Result.Cancelled
                    Else
                        'get selected views from form
                        Dim primaryBox As Revision = curForm.GetPrimaryBox(curDoc)
                        Dim primaryboxId As ElementId = primaryBox.Id
                        Dim Shlist As List(Of ViewSheet)
                        If curForm.AllSheetSelected(curDoc) = True Then
                            Dim collectors As New ClsCollectors
                            Shlist = collectors.GetAllSheets(curDoc)
                        Else
                            Shlist = curForm.GetSheetList(curDoc)
                        End If

                        'automate revision

                        'start transaction
                        Using curTrans As New Transaction(curDoc, "Apply Revisions")
                            If curTrans.Start = TransactionStatus.Started Then

                                Dim OnOff As Boolean = curForm.OnOrOff(curDoc)

                                For Each curSh As ViewSheet In Shlist
                                    Dim curShRev As IList(Of ElementId) = curSh.GetAllRevisionIds
                                    If OnOff = True Then
                                        If curShRev.Contains(primaryboxId) = True Then
                                        Else
                                            Dim RevOnSh As ICollection(Of ElementId) = curSh.GetAdditionalRevisionIds
                                            RevOnSh.Add(primaryboxId)
                                            curSh.SetAdditionalRevisionIds(RevOnSh)
                                        End If
                                    Else
                                        If curShRev.Contains(primaryboxId) = False Then
                                        Else
                                            Dim RevOnSh As ICollection(Of ElementId) = curSh.GetAdditionalRevisionIds
                                            RevOnSh.Remove(primaryboxId)
                                            curSh.SetAdditionalRevisionIds(RevOnSh)
                                        End If

                                    End If


                                    counter = counter + 1
                                Next '								
                            End If

                            'commit changes
                            curTrans.Commit()
                        End Using
                        'Else TaskDialog.Show("Select Cropped View", "The selected view is not cropped.                         Select another one.")
                    End If
                End Using
                '				
                'alert user
                If counter > 0 Then
                    TaskDialog.Show("Complete", "Revisions modified on " & counter & " sheets.")
                End If
                Return Result.Succeeded

            Else
                TaskDialog.Show("Error", "The current model file does not contain any revisions.")
                Return Result.Failed

            End If

        End Function


        Private Function DoesModelHaveRevisions(curDoc As Document) As Boolean
            'get all revisions
            Dim collectors As New ClsCollectors
            Dim vpList As List(Of Revision) = collectors.GetAllRevisions(curDoc)

            'get vp count
            If vpList.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace