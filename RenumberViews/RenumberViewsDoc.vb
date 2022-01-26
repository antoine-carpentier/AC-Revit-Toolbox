Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics


Namespace Renumber

    <Transaction(TransactionMode.Manual)> Partial Public Class RenumberViews
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document
            Dim CurUIDoc As UIDocument = commandData.Application.ActiveUIDocument

            Dim idlist As New List(Of ElementId)
            Dim DetailNumber As Integer
            DetailNumber = 1

            Dim SheetList As New List(Of ElementId)
            Dim collectors As New ClsCollectors
            For Each sh As ViewSheet In collectors.GetAllSheets(curDoc)
                SheetList.Add(sh.Id)

            Next

            Dim vPortFilter As ISelectionFilter = New ViewportSelectionFilter()
            Dim selectViewports As Boolean = True

            'start transaction
            Using curTrans As New Transaction(curDoc, "Renumber Details")
                If curTrans.Start = TransactionStatus.Started Then

                    While selectViewports

                        Try
                            Dim r As Reference = CurUIDoc.Selection.PickObject(ObjectType.Element, vPortFilter, "Pick viewports in the desired order, then press Escape twice.")
                            idlist.Add(r.ElementId)
                            CurUIDoc.Selection.SetElementIds(idlist)
                        Catch __unusedOperationCanceledException1__ As Autodesk.Revit.Exceptions.OperationCanceledException
                            selectViewports = False
                        End Try
                    End While


                    If idlist IsNot Nothing And idlist.Count > 0 Then

                        For Each vpid As ElementId In idlist
                            Try
                                Dim vp As Viewport = DirectCast(curDoc.GetElement(vpid), Viewport)
                                Dim vpNumber As Parameter = vp.Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER)
                                vpNumber.Set(vpNumber.AsString & "TEMP")
                            Catch ex As Exception
                                TaskDialog.Show("Error Temporary Parameter Setting", ex.Message)
                            End Try
                        Next

                        Dim vpid0 As ElementId = idlist(0)
                        Dim SheetNumber As String = DirectCast(curDoc.GetElement(vpid0), Viewport).Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER).AsString
                        Dim HostSheetid As ElementId

                        For Each sh As ElementId In SheetList
                            If SheetNumber = DirectCast(curDoc.GetElement(sh), ViewSheet).SheetNumber Then
                                HostSheetid = sh
                            End If
                        Next

                        Dim HostSheet As ViewSheet = DirectCast(curDoc.GetElement(HostSheetid), ViewSheet)

                        Dim VpOnSheet As ICollection(Of ElementId) = HostSheet.GetAllViewports()

                        Dim DetailList As New List(Of String)


                        For Each vpid As ElementId In VpOnSheet
                            DetailList.Add(DirectCast(curDoc.GetElement(vpid), Viewport).Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).AsString)
                        Next


                        Dim i As Integer

                        For i = 0 To idlist.Count - 1
                            Try
                                Dim vp As Viewport = DirectCast(curDoc.GetElement(idlist(i)), Viewport)
                                Dim vpNumber As Parameter = vp.Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER)


                                If DetailList.Contains(DetailNumber.ToString) = True Then
                                    DetailNumber = DetailNumber + 1
                                    i = i - 1
                                Else
                                    vpNumber.Set(DetailNumber.ToString)
                                    DetailNumber = DetailNumber + 1
                                End If

                            Catch ex As Exception
                                TaskDialog.Show("Error Temporary Parameter Setting", ex.Message)
                            End Try
                        Next

                    End If


                End If


                'commit changes
                curTrans.Commit()

            End Using


            Return Result.Succeeded


        End Function

    End Class

End Namespace