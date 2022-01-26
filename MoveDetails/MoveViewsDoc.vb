Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace MoveDetails


    <Transaction(TransactionMode.Manual)> Partial Public Class MoveDetailsDoc
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document
            Dim CurUIDoc As UIDocument = commandData.Application.ActiveUIDocument

            Dim idlist As New List(Of ElementId)
            Dim RefList As New List(Of Reference)
            Dim TargetSheet As ViewSheet

            Dim SheetList As New List(Of ElementId)
            Dim collectors As New ClsCollectors

            For Each sh As ViewSheet In collectors.GetAllSheets(curDoc)
                SheetList.Add(sh.Id)

            Next

            Dim vPortFilter As ISelectionFilter = New ViewportSelectionFilter()
            Dim selectViewports As Boolean = True

            ''start transaction
            'Using curTrans As New Transaction(curDoc, "Move Details")
            '    If curTrans.Start = TransactionStatus.Started Then



            Try
                        RefList = CType(CurUIDoc.Selection.PickObjects(ObjectType.Element, vPortFilter, "Pick viewports you wish to move, then click on ""Finish""."), List(Of Reference))
                    Catch __unusedOperationCanceledException1__ As Autodesk.Revit.Exceptions.OperationCanceledException
                    End Try

                    If RefList IsNot Nothing And RefList.Count > 0 Then

                        For Each Ref As Reference In RefList
                            idlist.Add(Ref.ElementId)
                        Next


                        'open form
                        Using curForm As New frmMoveDetails(curDoc)
                            'show form
                            curForm.ShowDialog()

                            If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                                Return Result.Cancelled
                            Else

                                TargetSheet = curForm.getOneSheet(curDoc)

                        'start transaction
                        Using curTrans As New Transaction(curDoc, "Move Details")
                            If curTrans.Start = TransactionStatus.Started Then

                                For Each vpid As ElementId In idlist
                                    Try
                                        Dim curvp As Viewport = DirectCast(curDoc.GetElement(vpid), Viewport)
                                        Dim curViewId As ElementId = curvp.ViewId
                                        Dim Curcenter As XYZ = curvp.GetBoxCenter
                                        Dim CurVpType As ElementId = curvp.GetTypeId()


                                        curDoc.Delete(curvp.Id)

                                        Dim NewVp As Viewport = Viewport.Create(curDoc, TargetSheet.Id, curViewId, Curcenter)
                                        NewVp.ChangeTypeId(CurVpType)


                                    Catch ex As Exception
                                        TaskDialog.Show("Error Temporary Parameter Setting", ex.Message)
                                    End Try
                                Next

                            End If

                            'commit changes
                            curTrans.Commit()

                        End Using

                        'alert user
                        If RefList.Count > 0 Then
                            If RefList.Count = 1 Then
                                TaskDialog.Show("Complete", RefList.Count.ToString & " View moved to sheet " & TargetSheet.SheetNumber & ".")
                            Else
                                TaskDialog.Show("Complete", RefList.Count.ToString & " Views moved to sheet " & TargetSheet.SheetNumber & ".")
                            End If
                            Return Result.Succeeded
                        Else Return Result.Failed

                                End If

                            End If

                        End Using

                    Else Return Result.Failed

                    End If

            '    End If

            '    'commit changes
            '    curTrans.Commit()

            'End Using

        End Function

    End Class

End Namespace