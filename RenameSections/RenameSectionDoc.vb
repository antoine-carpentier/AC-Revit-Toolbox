Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace Rename

    <Transaction(TransactionMode.Manual)> Partial Public Class RenameSectionDoc
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute



            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'set counter
            Dim counter As Integer = 0

            'check if this model file has views
            If doesModelHaveSections(curDoc) = True Then
                'open form
                Using curForm As New frmRenameSections(curDoc)

                    'show form
                    curForm.ShowDialog()

                    If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                        Return Result.Cancelled

                    Else

                        counter = counter + 1
                        Dim Results As List(Of frmRenameSections.StructSection) = curForm.getAllSectionInfos(curDoc)

                        'start transaction
                        Using curTrans As New Transaction(curDoc, "Rename Sections")
                            If curTrans.Start = TransactionStatus.Started Then

                                For Each tmpStructSection As frmRenameSections.StructSection In Results
                                    If tmpStructSection.SectionName <> tmpStructSection.SectionNewName Then
                                        counter = counter + 1

                                        Dim tmpSection As View = DirectCast(curDoc.GetElement(tmpStructSection.SectionId), View)
#If RELEASE2015 OrElse RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 Then
                                        tmpSection.ViewName = tmpStructSection.SectiontmpName
#ElseIf RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                        tmpSection.Name = tmpStructSection.SectiontmpName
#End If
                                    End If
                                Next

                                For Each tmpStructSection As frmRenameSections.StructSection In Results
                                    Dim tmpSection As View = DirectCast(curDoc.GetElement(tmpStructSection.SectionId), View)

                                    If tmpStructSection.SectionNewName = "DELETE" Then
                                        curDoc.Delete(tmpSection.Id)
                                    Else
#If RELEASE2015 OrElse RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 Then
                                        tmpSection.ViewName = tmpStructSection.SectionNewName
#ElseIf RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                        tmpSection.Name = tmpStructSection.SectionNewName
#End If
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
                    If counter > 1 Then
                        TaskDialog.Show("Complete", "All Sections were renamed properly.")
                    Else
                        TaskDialog.Show("Error", "No Sections needed renamed.")

                    End If
                    Return Result.Succeeded
                Else Return Result.Cancelled

                End If
            Else
                TaskDialog.Show("Error", "The model does not contain any sections.")
                Return Result.Failed
            End If

        End Function


        Private Function doesModelHaveSections(curDoc As Document) As Boolean
            'get all viewports
            Dim collectors As New ClsCollectors
            Dim vpList As List(Of View) = collectors.getAllSections(curDoc)

            'get vp count
            If vpList.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


    End Class

End Namespace
