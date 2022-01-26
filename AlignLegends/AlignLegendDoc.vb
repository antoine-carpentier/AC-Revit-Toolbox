
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace AlignLeg


    <Transaction(TransactionMode.Manual)> Partial Public Class AlignLegends
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'set counter 
            Dim counter As Integer = 0

            'check if this model file has sheets and viewports
            If doesModelHaveSheets(curDoc) = True Then
                If doesModelHaveViewports(curDoc) = True Then
                    'open form
                    Using curForm As New frmAlignLegends(curDoc)
                        'show form
                        curForm.ShowDialog()

                        If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                            Return Result.Cancelled
                        Else
                            'get selected views from form
                            Dim primaryView As Viewport = curForm.getPrimaryView(curDoc)
                            Dim viewAlignList As List(Of Viewport) = curForm.getViewList(curDoc)
                            Dim alignType As String = curForm.getAlignmentType

                            'align views
                            Dim primaryCenter As XYZ = primaryView.GetBoxCenter
                            Dim primaryOutline As Outline = primaryView.GetBoxOutline

                            'start transaction
                            Using curTrans As New Transaction(curDoc, "Align Legends")
                                If curTrans.Start = TransactionStatus.Started Then
                                    For Each curVP As Viewport In viewAlignList

                                        If curVP.Id <> primaryView.Id Then
                                            Dim newCenter As XYZ = primaryCenter

                                            'center current view
                                            curVP.SetBoxCenter(primaryCenter)

                                            Dim delta As XYZ
                                            Dim curVPOutline As Outline = curVP.GetBoxOutline

                                            'get new center based on alignment type
                                            Select Case alignType
                                                Case "Center"
                                                    newCenter = primaryCenter

                                                Case "Top Left"
                                                    Dim d1 As New XYZ(primaryOutline.MinimumPoint.X, primaryOutline.MaximumPoint.Y, primaryOutline.MaximumPoint.Z)
                                                    Dim d2 As New XYZ(curVPOutline.MinimumPoint.X, curVPOutline.MaximumPoint.Y, curVPOutline.MaximumPoint.Z)

                                                    delta = d2.Subtract(d1)
                                                    newCenter = curVP.GetBoxCenter.Subtract(delta)

                                                Case "Top Right"
                                                    Dim d1 As XYZ = primaryOutline.MaximumPoint
                                                    Dim d2 As XYZ = curVPOutline.MaximumPoint

                                                    delta = d1.Subtract(d2)
                                                    newCenter = curVP.GetBoxCenter.Add(delta)

                                                Case "Bottom Left"
                                                    Dim d1 As XYZ = primaryOutline.MinimumPoint
                                                    Dim d2 As XYZ = curVPOutline.MinimumPoint

                                                    delta = d2.Subtract(d1)
                                                    newCenter = curVP.GetBoxCenter.Subtract(delta)

                                                Case "Bottom Right"
                                                    Dim d1 As New XYZ(primaryOutline.MaximumPoint.X, primaryOutline.MinimumPoint.Y, primaryOutline.MaximumPoint.Z)
                                                    Dim d2 As New XYZ(curVPOutline.MaximumPoint.X, curVPOutline.MinimumPoint.Y, curVPOutline.MaximumPoint.Z)

                                                    delta = d1.Subtract(d2)
                                                    newCenter = curVP.GetBoxCenter.Add(delta)
                                            End Select

                                            'move to new center
                                            curVP.SetBoxCenter(newCenter)
                                            counter = counter + 1
                                        End If
                                    Next

                                End If

                                'commit changes
                                curTrans.Commit()
                            End Using
                        End If
                    End Using

                    'alert user
                    If counter > 0 Then
                        If counter = 1 Then
                            TaskDialog.Show("Complete", "Aligned " & counter & " view.")
                        Else TaskDialog.Show("Complete", "Aligned " & counter & " views.")
                        End If
                        Return Result.Succeeded
                    Else Return Result.failed
                    End If
                Else
                    TaskDialog.Show("Error", "The current model file does not contain any viewports.")
                    Return Result.Failed
                End If
            Else
                TaskDialog.Show("Error", "The current model file does not contain any sheets.")
                Return Result.Failed
            End If
        End Function

        Private Function doesModelHaveSheets(curDoc As Document) As Boolean
            'get all sheets
            Dim collectors As New ClsCollectors
            Dim sheetList As List(Of ViewSheet) = collectors.GetAllSheets(curDoc)

            'get sheet count
            If sheetList.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function doesModelHaveViewports(curDoc As Document) As Boolean
            'get all viewports
            Dim collectors As New ClsCollectors
            Dim vpList As List(Of Viewport) = collectors.GetAllViewports(curDoc)


            'get vp count
            If vpList.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace