
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports System.Globalization

Namespace Duplicate


    <Transaction(TransactionMode.Manual)> Partial Public Class DupDoc
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            'Get the current document

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            Dim counter As Integer = 0

            'check if this model file has views
            If DoesModelHaveViews(curDoc) = True Then
                'open form
                Using curForm As New FrmDupViews(curDoc)
                    'show form
                    curForm.ShowDialog()

                    If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                        Return Result.Cancelled
                    Else
                        'get selected view and view type
                        Dim viewList As List(Of View) = curForm.GetSelectedViews(curDoc)

                        'create transaction
                        Using curTrans As New Transaction(curDoc, "Duplicate Views")
                            If curTrans.Start = TransactionStatus.Started Then

                                'loop through view list and dup views
                                For Each curView As View In viewList

                                    'duplicate view
                                    If curView IsNot Nothing Then
                                        'set duplication settings based on user selection
                                        Dim dupOptions As ViewDuplicateOption

                                        Select Case curForm.GetDuplicateType
                                            Case "duplicate"
                                                dupOptions = ViewDuplicateOption.Duplicate

                                            Case "detailing"
                                                dupOptions = ViewDuplicateOption.WithDetailing

                                            Case "dependent"
                                                dupOptions = ViewDuplicateOption.AsDependent

                                        End Select

                                        'loop through dup count and duplicate views
                                        For i As Integer = 1 To curForm.GetNumDupes
                                            'duplicate view
                                            Try
                                                curView.Duplicate(dupOptions)
                                                counter = counter + 1

                                            Catch ex As Exception
                                                TaskDialog.Show("Error", "Could not duplicate view.")
                                                Throw
                                            End Try
                                        Next
                                    End If
                                Next
                            End If

                            'commit changes
                            curTrans.Commit()
                        End Using

                        'alert user
                        TaskDialog.Show("Complete", "Created " & counter & " view duplicates.")
                        Return Result.Succeeded

                    End If
                End Using
            Else TaskDialog.Show("Error", "The current model file does not contain any structural views.")
                Return Result.Failed
            End If

        End Function

        Private Function DoesModelHaveViews(curDoc As Document) As Boolean
            'get all viewports
            Dim collectors As New ClsCollectors
            Dim vpList As List(Of View) = collectors.GetAllStructuralViews(curDoc)

            'get vp count
            If vpList.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


    End Class

        End Namespace