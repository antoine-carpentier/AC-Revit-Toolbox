
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Namespace Align


    <Transaction(TransactionMode.Manual)> Partial Public Class AlignViews
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute

            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'set counter 
            Dim counter As Integer = 0

            'Create boolean for copy crop error
            Dim CopyNonExistingCrop As New Boolean
            CopyNonExistingCrop = False

            'Check whether the fake VP label family is loaded or not

            Dim VPTitlefamily As Autodesk.Revit.DB.Family = Nothing
            Dim FilePath As String = "S:\REVIT"
            Dim FileName As String = "fake VP title.rfa"

            Dim collectors As New ClsCollectors
            Dim FamilyList As List(Of Family) = collectors.GetAllFamilies(curDoc)


            For Each f As Family In FamilyList
                If f.Name = "fake VP title" Then
                    VPTitlefamily = f
                End If
            Next

            'Get all information regarding  "no Title" VP Info
            Dim FamilyInstanceList As New List(Of FamilyInstance)
            Dim FamilyInstanceParamList As New List(Of Parameter)

            'Create a negative Id for a scope box (negative Id represents placeholders, so are not associated with any elements)
            Dim NoScopeBoxId As New ElementId(-1)



            Dim NoTitleVPID As ElementId = Nothing
            For Each et As ElementType In collectors.GetAllVPTypes(curDoc)
                If et.Name = "Title None" OrElse et.Name = "No Title" Then
                    NoTitleVPID = et.Id
                End If
            Next


            'check if this model file has sheets and viewports
            If DoesModelHaveSheets(curDoc) = True Then
                If DoesModelHaveViewports(curDoc) = True Then
                    'open form
                    Using curForm As New frmAlignViews(curDoc)
                        'show form
                        curForm.ShowDialog()

                        If curForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                            Return Result.Cancelled
                        Else
                            'start transaction				
                            Using curTrans As New Transaction(curDoc, "Align Views")
                                If curTrans.Start = TransactionStatus.Started Then

                                    'get selected views from form (Primary VP, label and scale)
                                    Dim primaryViewPort As Viewport = curForm.getPrimaryViewPort(curDoc)
                                    Dim viewAlignList As List(Of Viewport) = curForm.getViewList(curDoc)
                                    Dim PrimaryView As View = DirectCast(curDoc.GetElement(primaryViewPort.ViewId), View)
                                    Dim PrimaryScale As Integer = PrimaryView.Scale

                                    'Dim Variables used for the label alignment,

                                    Dim PrimaryTitleOutline As Outline = Nothing
                                    Dim TitleMinPoint As XYZ = Nothing
                                    Dim TitleMaxPoint As XYZ = Nothing

                                    Try
                                        PrimaryTitleOutline = primaryViewPort.GetLabelOutline
                                        TitleMinPoint = PrimaryTitleOutline.MinimumPoint
                                        TitleMaxPoint = PrimaryTitleOutline.MaximumPoint
                                    Catch ex As Autodesk.Revit.Exceptions.InternalException
                                        TaskDialog.Show("Cannot Compute Title Location", "Please check that the original viewport has a title (i.e. the viewport is not a ""No Title"" viewport).")
                                        Return Result.Failed
                                    End Try

                                    Dim Deltalabel As New XYZ(0.02083333, 0.02083333, 0)

                                    Dim PrimaryScopeBox As Parameter = PrimaryView.Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP)

                                    If PrimaryScopeBox.AsElementId.ToString <> "-1" Then

                                        Using curForm2 As New frmAlignViewsScopeBox
                                            'show form
                                            curForm2.ShowDialog()

                                            Dim ProceedBool As Boolean = curForm2.YesorNo

                                            If ProceedBool = True Then

                                                Dim eleId As ElementId = New ElementId(-1)
                                                Dim TestBool As Boolean = PrimaryView.Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP).Set(eleId)

                                            Else
                                                Return Result.Cancelled
                                            End If
                                            'End If
                                        End Using

                                    End If


                                    'if fake vp title family isn't loaded yet, do so now
                                    If VPTitlefamily Is Nothing Then
                                        curDoc.LoadFamily(FilePath + "/" + FileName, VPTitlefamily)
                                    End If

                                    Dim LabelSymbolid As ISet(Of ElementId) = VPTitlefamily.GetFamilySymbolIds
                                    Dim LabelSymbol As FamilySymbol = DirectCast(curDoc.GetElement(LabelSymbolid(0)), FamilySymbol)

                                    'align views

                                    Dim PrimaryViewCropped As New Boolean
                                    Dim primaryCropSaved As New CurveLoop

                                    If PrimaryView.CropBoxActive = True Then
#If RELEASE2015 Then
                                        primaryCropSaved = primaryView.GetCropRegionShapeManager.GetCropRegionShape
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                        primaryCropSaved = PrimaryView.GetCropRegionShapeManager.GetCropShape(0)
#End If
                                        PrimaryViewCropped = True
                                    Else PrimaryViewCropped = False
                                        CopyNonExistingCrop = True
                                    End If

                                    Dim SecondaryCropSaved As CurveLoop
                                    Dim SecondaryViewCropped As New Boolean


                                    'Create Temporary Crop
                                    Dim Point1 = New XYZ(-10000, -10000, 0)
                                    Dim Point2 = New XYZ(-10000, 10000, 0)
                                    Dim Point3 = New XYZ(10000, -10000, 0)
                                    Dim Point4 = New XYZ(10000, 10000, 0)

                                    Dim LineBottom As Line = Line.CreateBound(Point1, Point3)
                                    Dim LineTop As Line = Line.CreateBound(Point4, Point2)
                                    Dim LineLeft As Line = Line.CreateBound(Point2, Point1)
                                    Dim LineRight As Line = Line.CreateBound(Point3, Point4)

                                    Dim TmpCrop As New CurveLoop
                                    TmpCrop.Append(LineBottom)
                                    TmpCrop.Append(LineRight)
                                    TmpCrop.Append(LineTop)
                                    TmpCrop.Append(LineLeft)

                                    'Set Temporary crop on Primary View

                                    PrimaryView.CropBoxActive = True

                                    Try
#If RELEASE2015 Then
                                        primaryView.GetCropRegionShapeManager.SetCropRegionShape(TmpCrop)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                        PrimaryView.GetCropRegionShapeManager.SetCropShape(TmpCrop)
#End If
                                    Catch ex As Autodesk.Revit.Exceptions.InvalidOperationException


                                    End Try

                                    PrimaryView.CropBoxActive = True
                                    curDoc.Regenerate()


                                    Dim primaryCenter As XYZ = primaryViewPort.GetBoxCenter

                                    'Work on each secondary views 
                                    For Each curVP As Viewport In viewAlignList

                                        'Get the info regarding the existing instances of fake Vp title
                                        'It needs to be done for each sheet, as deleting one will alter the DB
                                        FamilyInstanceList.Clear()
                                        FamilyInstanceParamList.Clear()
                                        For Each Fi As FamilyInstance In collectors.GetAllFamilyInstances(curDoc)
                                            If Fi.Name = "fake VP title" Then
                                                Dim CurfamInstanceParam As IList(Of Parameter) = Fi.GetOrderedParameters
                                                FamilyInstanceList.Add(Fi)
                                                FamilyInstanceParamList.Add(CurfamInstanceParam(3))
                                            End If
                                        Next

                                        If curVP.Id <> primaryViewPort.Id Then

                                            Dim Curview As View = DirectCast(curDoc.GetElement(curVP.ViewId), View)
                                            Dim CurSheet As ViewSheet = DirectCast(curDoc.GetElement(curVP.SheetId), ViewSheet)

                                            'If the scope box is not read only, set it to "None". The scope box being read-only means a crop is applied, so it is on "none" already
                                            If Curview.Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP).IsReadOnly = False Then
                                                Curview.Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP).Set(NoScopeBoxId)
                                            End If

                                            'Delete the possible fake VP title already associated with each secondary view
                                            For i = 0 To FamilyInstanceList.Count - 1
                                                If FamilyInstanceParamList(i).AsString = Curview.Name Then
                                                    curDoc.Delete(FamilyInstanceList(i).Id)
                                                End If
                                            Next

                                            'Change the VP of the secondary view to No Title
                                            curVP.ChangeTypeId(NoTitleVPID)

                                            'Dim the detail number, the sheet and the symbol to be used for the fake VP title
                                            Dim CurNumber As String = curVP.Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).AsString
                                            Dim CurHost As Element = DirectCast(curDoc.GetElement(CurSheet.Id), Element)
                                            Dim CurFamilyInstance As FamilyInstance = curDoc.Create.NewFamilyInstance(TitleMinPoint + Deltalabel, LabelSymbol, CType(CurHost, View)) '

                                            'List all the parameters that will be used for each fake vp title
                                            Dim CurVpInstanceParam As IList(Of Parameter) = CurFamilyInstance.GetOrderedParameters

                                            'Get the view scale, as well as the string that will be used to represent said scale
                                            Dim CurViewScale As Integer = Curview.Scale
                                            Dim ScaleText As String = ""

                                            If CurViewScale = 48 Then
                                                ScaleText = "1/4"" = 1'-0"""
                                            ElseIf CurViewScale = 64 Then
                                                ScaleText = "3/16"" = 1'-0"""
                                            ElseIf CurViewScale = 96 Then
                                                ScaleText = "1/8"" = 1'-0"""
                                            ElseIf CurViewScale = 120 Then
                                                ScaleText = "1"" = 10'-0"""
                                            ElseIf CurViewScale = 128 Then
                                                ScaleText = "3/32"" = 1'-0"""
                                            ElseIf CurViewScale = 192 Then
                                                ScaleText = "1/16"" = 1'-0"""
                                            ElseIf CurViewScale = 240 Then
                                                ScaleText = "1"" = 20'-0"""
                                            End If

                                            'Modify each fake vp title parameters to match correct info
                                            CurVpInstanceParam(0).Set(CurNumber)
                                            CurVpInstanceParam(1).Set(CurSheet.Name)
                                            CurVpInstanceParam(2).Set(ScaleText)
                                            CurVpInstanceParam(3).Set(Curview.Name)
                                            CurVpInstanceParam(4).Set(PrimaryTitleOutline.GetDiagonalLength - 0.0425)

                                            ''Check if the current view has a scope box assigned

                                            'Dim CurScopeBox As Parameter = Curview.Parameter(BuiltInParameter.VIEWER_VOLUME_OF_INTEREST_CROP)

                                            'If CurScopeBox.AsElementId.ToString = "-1" Then
                                            'Else
                                            '    'Change the Scope Box to "None"

                                            '    CurScopeBox.Set(ScopeId)
                                            'End If

                                            'Get current views cropbox
                                            If Curview.CropBoxActive = True Then
#If RELEASE2015 Then
                                                                                    SecondaryCropSaved = Curview.GetCropRegionShapeManager.GetCropRegionShape
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                                SecondaryCropSaved = Curview.GetCropRegionShapeManager.GetCropShape(0)
#End If
                                                SecondaryViewCropped = True
                                            Else Curview.CropBoxActive = True
                                                SecondaryViewCropped = False
                                            End If

                                            'Set temporary crop on secondary views
#If RELEASE2015 Then
                                                                                Curview.GetCropRegionShapeManager.SetCropRegionShape(TmpCrop)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                            Curview.GetCropRegionShapeManager.SetCropShape(TmpCrop)
#End If
                                            Curview.CropBoxActive = True
                                            curDoc.Regenerate()

                                            'move to new center
                                            curVP.SetBoxCenter(primaryCenter)

                                            'restore secondary crops
                                            If curForm.CopyCropCheckBox = True Then
                                                If PrimaryViewCropped = True Then
#If RELEASE2015 Then
                                                                                        Curview.GetCropRegionShapeManager.SetCropRegionShape(primaryCropSaved)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                                    Curview.GetCropRegionShapeManager.SetCropShape(primaryCropSaved)
#End If
                                                    Curview.CropBoxActive = False
                                                    Curview.CropBoxActive = True
                                                ElseIf SecondaryViewCropped = True Then
#If RELEASE2015 Then
                                                                                    Curview.GetCropRegionShapeManager.SetCropRegionShape(SecondaryCropSaved)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                                    Curview.GetCropRegionShapeManager.SetCropShape(SecondaryCropSaved)
#End If
                                                    Curview.CropBoxActive = False
                                                    Curview.CropBoxActive = True
                                                Else Curview.CropBoxActive = False
                                                End If
                                            ElseIf SecondaryViewCropped = True Then
#If RELEASE2015 Then
                                                                                    Curview.GetCropRegionShapeManager.SetCropRegionShape(SecondaryCropSaved)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                                Curview.GetCropRegionShapeManager.SetCropShape(SecondaryCropSaved)
#End If
                                                Curview.CropBoxActive = False
                                                Curview.CropBoxActive = True
                                            Else Curview.CropBoxActive = False
                                            End If
                                            counter = counter + 1
                                        End If
                                    Next

                                    'restore primary crop
                                    If PrimaryViewCropped = True Then
#If RELEASE2015 Then
                                        PrimaryView.GetCropRegionShapeManager.SetCropRegionShape(primaryCropSaved)
#ElseIf RELEASE2016 OrElse RELEASE2017 OrElse RELEASE2018 OrElse RELEASE2019 OrElse RELEASE2020 OrElse RELEASE2021 Then
                                        PrimaryView.GetCropRegionShapeManager.SetCropShape(primaryCropSaved)
#End If
                                        PrimaryView.CropBoxActive = False
                                        PrimaryView.CropBoxActive = True
                                    Else PrimaryView.CropBoxActive = False

                                    End If
                                End If

                                'commit changes to first transaction
                                curTrans.Commit()

                            End Using


                            'alert user

                            If counter > 0 Then
                                If counter = 1 Then
                                    If curForm.CopyCropCheckBox = True Then
                                        If CopyNonExistingCrop = True Then
                                            TaskDialog.Show("Complete", "Aligned " & counter & " view.                                               Couldn't copy crop as the master view is not cropped.")
                                        Else TaskDialog.Show("Complete", "Aligned and Cropped " & counter & " view. ")
                                        End If
                                    Else TaskDialog.Show("Complete", "Aligned " & counter & " view.")
                                    End If
                                Else
                                    If curForm.CopyCropCheckBox = True Then
                                        If CopyNonExistingCrop = True Then
                                            TaskDialog.Show("Complete", "Aligned " & counter & " views.                                               Couldn't copy crop as the master view is not cropped.")
                                        Else TaskDialog.Show("Complete", "Aligned and Cropped " & counter & " views.")
                                        End If
                                    Else TaskDialog.Show("Complete", "Aligned " & counter & " views.")
                                    End If
                                End If
                                Return Result.Succeeded
                            Else Return Result.Failed
                            End If
                        End If
                    End Using
                Else
                    TaskDialog.Show("Error", "The current model file does not contain any viewports.")
                    Return Result.Failed
                End If
            Else
                TaskDialog.Show("Error", "The current model file does not contain any sheets.")
                Return Result.Failed
            End If

        End Function

        Private Function DoesModelHaveSheets(curDoc As Document) As Boolean
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

        Private Function DoesModelHaveViewports(curDoc As Document) As Boolean
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