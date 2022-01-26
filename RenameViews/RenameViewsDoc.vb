Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports System.Globalization
Imports Microsoft.VisualBasic


Namespace RenameV

    <Transaction(TransactionMode.Manual)> Partial Public Class RenameViews
        Implements IExternalCommand
        Public Shared uiapp As UIApplication


        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'set counter 
            Dim counter As Integer = 0
            Dim Viewcounter As Integer = 0
            Dim i As Integer = 0

            'check if this model file has views
            If doesModelHaveViews(curDoc) = True Then
                'open form
                Using curForm As New frmRenameViews(curDoc)
                    'show form
                    curForm.ShowDialog()

                    If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                        'Result.Cancelled
                    Else
                        'get selected views from form
                        Dim viewList As List(Of View) = curForm.GetViewList(curDoc)
                        Dim TmpName As String = ""
                        Dim TmpNameNum As String = ""
                        Dim collector As New ClsCollectors
                        Dim AllStructViews As List(Of View) = collector.GetAllStructuralViews(curDoc)
                        Dim AllstructViewsName As New List(Of String)

                        For Each curview As View In collector.GetAllStructuralViews(curDoc)
                            AllstructViewsName.Add(curview.Name)
                        Next

                        'start transaction
                        Using curTrans As New Transaction(curDoc, "Rename Views")
                            If curTrans.Start = TransactionStatus.Started Then
                                For Each curView As View In viewList


                                    'Add at Beginning
                                    If curForm.GetAddedBeginning <> "" Then

                                        TmpName = curForm.GetAddedBeginning & " " & curView.Name
                                        TmpNameNum = TmpName

                                        While AllstructViewsName.Contains(TmpNameNum)
                                            Viewcounter = Viewcounter + 1
                                            TmpNameNum = TmpName & " " & Viewcounter.ToString
                                        End While

                                        curView.Name = TmpNameNum
                                        Viewcounter = 0

                                    End If


                                    'Add at End
                                    If curForm.GetAddedEnd <> "" Then
                                        TmpName = curView.Name & " " & curForm.GetAddedEnd
                                        TmpNameNum = TmpName

                                        While AllstructViewsName.Contains(TmpNameNum)
                                            Viewcounter = Viewcounter + 1
                                            TmpNameNum = TmpName & " " & Viewcounter.ToString
                                        End While

                                        curView.Name = TmpNameNum
                                        Viewcounter = 0

                                    End If

                                    'Replace
                                    If curForm.GetReplaced <> "" Then
                                        If curView.Name.ToLower.Contains(curForm.GetReplaced.ToLower) Then

                                            TmpName = Microsoft.VisualBasic.Strings.Replace(curView.Name, curForm.GetReplaced, curForm.Replacing, 1, -1, Constants.vbTextCompare)
                                            TmpNameNum = TmpName

                                            While AllstructViewsName.Contains(TmpNameNum)
                                                Viewcounter = Viewcounter + 1
                                                TmpNameNum = TmpName & " " & Viewcounter.ToString
                                            End While

                                            curView.Name = TmpNameNum
                                            Viewcounter = 0

                                        End If
                                    End If

                                    'Delete
                                    If curForm.GetDeleted <> "" Then
                                        If curView.Name.ToLower.Contains(curForm.GetDeleted.ToLower) Then

                                            TmpName = Microsoft.VisualBasic.Strings.Replace(curView.Name, curForm.GetDeleted, "", 1, -1, Constants.vbTextCompare)
                                            TmpNameNum = TmpName

                                            While AllstructViewsName.Contains(TmpNameNum)
                                                Viewcounter = Viewcounter + 1
                                                TmpNameNum = TmpName & " " & Viewcounter.ToString
                                            End While

                                            curView.Name = TmpNameNum
                                            Viewcounter = 0

                                        End If
                                    End If

                                    counter = counter + 1
                                Next
                            End If

                            'commit changes
                            curTrans.Commit()
                        End Using

                    End If

                End Using

                'alert user
                If counter > 0 Then
                    TaskDialog.Show("Complete", "Renamed " & counter & " views.")
                End If
                Return Result.Succeeded
            Else
                TaskDialog.Show("Error", "The current model file does not contain any structural views.")
                Return Result.Failed
            End If

        End Function


        Private Function doesModelHaveViews(curDoc As Document) As Boolean
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