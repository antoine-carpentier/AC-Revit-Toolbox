
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace Crop


    <Transaction(TransactionMode.Manual)> Partial Public Class CropViews
        Implements IExternalCommand
        Public Shared uiapp As UIApplication



        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'set counter 
            Dim counter As Integer = 0

            'check if this model file has views
            If DoesModelHaveViews(curDoc) = True Then
                'open form
                Using curForm As New frmCropViews(curDoc)
                    'show form
                    curForm.ShowDialog()

                    If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                        Return Result.Cancelled
                    Else
                        'get selected views from form
                        Dim primaryView As View = curForm.GetPrimaryView(curDoc)
                        Dim viewList As List(Of View) = curForm.GetViewList(curDoc)
                        'end if	
                        'align crops
                        If primaryView.CropBoxActive = True Then
#If RELEASE2015 Then
                            Dim CropBox As CurveLoop = primaryView.GetCropRegionShapeManager.GetCropRegionShape
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                            Dim CropBox As CurveLoop = primaryView.GetCropRegionShapeManager.GetCropshape(0)
#End If
                            'start transaction
                            Using curTrans As New Transaction(curDoc, "Copy Crop")
                                If curTrans.Start = TransactionStatus.Started Then
                                    For Each curView As View In viewList

                                        If curView.Id <> primaryView.Id Then

                                            'copy crop on current view
                                            curView.CropBoxVisible = True
                                            curView.CropBoxActive = True
#If RELEASE2015 Then
                                            curView.GetCropRegionShapeManager.SetCropRegionShape(CropBox)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                            curView.GetCropRegionShapeManager.SetCropShape(CropBox)
#End If
                                            curView.CropBoxActive = False
                                            curView.CropBoxActive = True

                                            counter = counter + 1

                                        End If
                                    Next '								
                                End If

                                'commit changes
                                curTrans.Commit()
                            End Using
                        Else TaskDialog.Show("Select Cropped View", "The selected view is not cropped.                         Select another one.")
                        End If
                        'Return Result.Succeeded
                    End If
                End Using
                '				
                'alert user
                If counter > 0 Then
                    If counter = 1 Then
                        TaskDialog.Show("Complete", "Cropped " & counter & " view.")
                    Else
                        TaskDialog.Show("Complete", "Cropped " & counter & " views.")
                    End If
                End If
                Return Result.Succeeded
            Else
                    TaskDialog.Show("Error", "The current model file does not contain any structural views.")
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