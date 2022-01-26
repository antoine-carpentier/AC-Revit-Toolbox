#Region "Imported Namespaces"
Imports Autodesk.Revit.ApplicationServices
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.UI.ui
Imports Autodesk.Revit.UI.Events
Imports System
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports System.Windows.Media.Imaging
Imports System.Reflection

#End Region

Class AdskApplication
    Implements IExternalApplication
    Public Shared uiapp As UIApplication
    ''' <summary>
    ''' This method is called when Revit starts up before a 
    ''' document or default template is actually loaded.
    ''' </summary>
    ''' <param name="app">An object passed to the external 
    ''' application which contains the controlled application.</param>
    ''' <returns>Return the status of the external application. 
    ''' A result of Succeeded means that the external application started successfully. 
    ''' Cancelled can be used to signify a problem. If so, Revit informs the user that 
    ''' the external application failed to load and releases the internal reference.
    ''' </returns>
    Public Function OnStartup(ByVal app As UIControlledApplication) _
    As Result Implements IExternalApplication.OnStartup

        Dim Panel As RibbonPanel = app.CreateRibbonPanel("AC's Toolbox")
        Dim MainButton As PulldownButton = TryCast(Panel.AddItem(New PulldownButtonData("MainButton", "AC's Toolbox")), PulldownButton)

        'Get dll assembly path
        Dim ThisAssemblyPath As String = Assembly.GetExecutingAssembly().Location

        Dim AlignViewButtonData As New PushButtonData("CmdAlignView", "Batch Align Views", ThisAssemblyPath, "ACRevitAddin.Align.AlignViews")
        Dim AlignLegendButtonData As New PushButtonData("CmdAlignLeg", "Batch Align Legends", ThisAssemblyPath, "ACRevitAddin.AlignLeg.AlignLegends")
        Dim RevisionButtonData As New PushButtonData("CmdRevision", "Batch Apply Revisions", ThisAssemblyPath, "ACRevitAddin.Revisions.AutoRev")
        Dim SheetMakerButtonData As New PushButtonData("CmdShMaker", "Batch Create Sheets", ThisAssemblyPath, "ACRevitAddin.SheetMaker.SheetMakerDoc")
        Dim CropViewButtonData As New PushButtonData("CmdCropView", "Batch Crop Views", ThisAssemblyPath, "ACRevitAddin.Crop.CropViews")
        Dim DupViewsButtonData As New PushButtonData("CmdViewDup", "Batch Duplicate Views", ThisAssemblyPath, "ACRevitAddin.Duplicate.DupDoc")
        Dim RenameSectionsButtonData As New PushButtonData("CmdRenameSections", "Batch Rename Sections", ThisAssemblyPath, "ACRevitAddin.Rename.RenameSectionDoc")
        Dim RenumberViewsButtonData As New PushButtonData("CmdRenumberViews", "Batch Renumber Details", ThisAssemblyPath, "ACRevitAddin.Renumber.RenumberViews")
        Dim MoveDetailsButtonData As New PushButtonData("CmdMoveDetails", "Batch Move Details", ThisAssemblyPath, "ACRevitAddin.MoveDetails.MoveDetailsDoc")
        Dim RenameViewsButtonData As New PushButtonData("CmdRenameViews", "Batch Rename Views", ThisAssemblyPath, "ACRevitAddin.RenameV.RenameViews")
        Dim ImportExcelButtonData As New PushButtonData("CmdImportExcel", "Import Excel Table", ThisAssemblyPath, "ACRevitAddin.Excel.ExcelImport")

        MainButton.AddPushButton(AlignLegendButtonData)
        MainButton.AddPushButton(AlignViewButtonData)
        MainButton.AddPushButton(RevisionButtonData)
        MainButton.AddPushButton(SheetMakerButtonData)
        MainButton.AddPushButton(CropViewButtonData)
        MainButton.AddPushButton(DupViewsButtonData)
        MainButton.AddPushButton(MoveDetailsButtonData)
        MainButton.AddPushButton(RenameSectionsButtonData)
        MainButton.AddPushButton(RenameViewsButtonData)
        MainButton.AddPushButton(RenumberViewsButtonData)
        MainButton.AddPushButton(ImportExcelButtonData)

        'Set the large image shown on button
        Dim bitmap As BitmapImage = ToBitmapImage(My.Resources.ACRlogo)

        MainButton.LargeImage = bitmap

        'Dimiss annoying "attach wall" message
        AddHandler app.DialogBoxShowing, AddressOf AppDialogShowing

        'Must return some code
        Return Result.Succeeded
    End Function

    ''' <summary>
    ''' This method is called when Revit is about to exit.
    ''' All documents are closed before this method is called.
    ''' </summary>
    ''' <param name="app">An object passed to the external 
    ''' application which contains the controlled application.</param>
    ''' <returns>Return the status of the external application. 
    ''' A result of Succeeded means that the external application successfully shutdown. 
    ''' Cancelled can be used to signify that the user cancelled the external operation 
    ''' at some point. If false is returned then the Revit user should be warned of the 
    ''' failure of the external application to shut down correctly.</returns>
    Public Function OnShutdown(
      ByVal app As UIControlledApplication) _
    As Result Implements IExternalApplication.OnShutdown

        'TODO: Add your code here

        'Must return some code
        Return Result.Succeeded
    End Function

    Private Function ToBitmapImage(img As System.Drawing.Image) As BitmapImage
        Dim ms As New IO.MemoryStream
        Dim bi As New BitmapImage
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
        bi.BeginInit()
        bi.StreamSource = ms
        bi.EndInit()
        Return bi
    End Function

    Private Sub AppDialogShowing(sender As Object, args As DialogBoxShowingEventArgs)
        Dim t As TaskDialogShowingEventArgs = TryCast(args, TaskDialogShowingEventArgs)
        If t IsNot Nothing AndAlso t.Message = "Would you like walls that go up to this floor's level to attach to its bottom?" Then
            args.OverrideResult(1)
        End If
    End Sub

End Class
