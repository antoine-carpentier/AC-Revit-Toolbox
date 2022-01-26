
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports Autodesk.Revit.DB.Architecture
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Public Class ClsCollectors
    Public Function GetAllSheets(curDoc As Document) As List(Of ViewSheet)
        'get all sheets
        Dim m_colViews As New FilteredElementCollector(curDoc)
        m_colViews.OfCategory(BuiltInCategory.OST_Sheets)

        Dim m_Sheets As New List(Of ViewSheet)
        For Each x As ViewSheet In m_colViews.ToElements
            m_Sheets.Add(x)
        Next

        Return m_Sheets
    End Function

    Public Function GetAllStructuralViews(curDoc As Document) As List(Of View)
        'get all viewports
        Dim vpCollector As New FilteredElementCollector(curDoc)
        vpCollector.OfCategory(BuiltInCategory.OST_Views)


        'output structural views to list
        Dim vpList As New List(Of View)
        For Each curView As View In vpCollector
            If curView.ViewType = ViewType.EngineeringPlan Then
                vpList.Add(curView)
            End If

        Next

        Return vpList

    End Function

    Public Function GetAllViewports(curDoc As Document) As List(Of Viewport)
        'get all viewports
        Dim vpCollector As New FilteredElementCollector(curDoc)
        vpCollector.OfCategory(BuiltInCategory.OST_Viewports)

        'output viewports to list
        Dim vpList As New List(Of Viewport)
        For Each curVP As Viewport In vpCollector
            'add to list
            vpList.Add(curVP)
        Next

        Return vpList

    End Function

    Public Function GetAllRevisions(curDoc As Document) As List(Of Revision)
        'get all viewports
        Dim RevCollector As New FilteredElementCollector(curDoc)
        RevCollector.OfCategory(BuiltInCategory.OST_Revisions)


        'output revisions to list
        Dim RevList As New List(Of Revision)
        For Each curRev As Revision In RevCollector
            RevList.Add(curRev)

        Next

        Return RevList

    End Function

    Public Function GetAllFamilies(curdoc As Document) As List(Of Family)
        'Get all loaded families in the project
        Dim m_colFamily As New FilteredElementCollector(curdoc)
        m_colFamily.OfClass(GetType(Family))

        Dim m_Family As New List(Of Family)
        For Each F As Family In m_colFamily
            m_Family.Add(F)
        Next

        Return m_Family
    End Function

    Public Function GetAllFamilyInstances(CurDoc As Document) As List(Of FamilyInstance)

        'Get all family instances used in the project
        Dim m_colFamInstance As New FilteredElementCollector(CurDoc)
        m_colFamInstance.OfClass(GetType(FamilyInstance))

        Dim m_FamInstance As New List(Of FamilyInstance)
        For Each FI As FamilyInstance In m_colFamInstance
            m_FamInstance.Add(FI)
        Next

        Return m_FamInstance
    End Function

    Public Function GetAllVPTypes(curdoc As Document) As List(Of ElementType)

        'Get all element types in the project

        Dim m_colVPType As New FilteredElementCollector(curdoc)
        m_colVPType.OfClass(GetType(ElementType))

        Dim m_VPtype As New List(Of ElementType)

        'if the element type belongs to the viewport family, then add it to the list
        For Each ET As ElementType In m_colVPType
            If ET.FamilyName = "Viewport" Then
                m_VPtype.Add(ET)
            End If
        Next

        Return m_VPtype
    End Function


    Public Function GetTitleblock(curDoc As Document, tblockName As String()) As FamilySymbol
        'get all titleblocks
        Dim tBlockList As List(Of FamilySymbol) = GetAllTitleblocks(curDoc)

        'loop through titleblocks and find specified tblock
        If tBlockList.Count > 0 Then
            'loop through tblocks
            For Each curTB As FamilySymbol In tBlockList
                If curTB.FamilyName Like tblockName(0) And curTB.Name Like tblockName(1) Then
                    Return curTB
                    Exit Function
                End If
            Next
        End If

        Return Nothing
    End Function

    Public Function GetAllTitleblocks(curDoc As Document) As List(Of FamilySymbol)
        'get all titleblocks
        Dim colTblock As New FilteredElementCollector(curDoc)
        colTblock.WhereElementIsElementType()
        colTblock.OfCategory(BuiltInCategory.OST_TitleBlocks)

        Dim tBlockList As New List(Of FamilySymbol)
        For Each curElem As Element In colTblock.ToElements
            Dim curTB As FamilySymbol = DirectCast(curElem, FamilySymbol)
            tBlockList.Add(curTB)
        Next

        Return tBlockList
    End Function

    Public Function GetAllTitleblockNames(curDoc As Document) As List(Of String)
        'get all titleblocks
        Dim tBlockList As List(Of FamilySymbol) = GetAllTitleblocks(curDoc)
        Dim tBlockNames As New List(Of String)

        For Each curTB As FamilySymbol In tBlockList
            tBlockNames.Add(curTB.FamilyName & " | " & curTB.Name)
        Next

        Return tBlockNames

    End Function

    Public Function DoesTblockExist(curDoc As Document, tblockName As String()) As Boolean
        Dim curTblock As FamilySymbol = Nothing

        curTblock = GetTitleblock(curDoc, tblockName)

        If curTblock IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub WriteCSVFile(filePath As String, fileContents As String)
        Dim file As System.IO.StreamWriter

        'write to file
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, True)
        file.WriteLine(fileContents)

        'close file
        file.Close()

    End Sub

    Public Function ReadCSV(filePathName As String, hasHeaders As Boolean) As List(Of StructSheet)
        Dim sheetList As New List(Of StructSheet)

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(filePathName)
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim currentRow As String()

            'if CSV file has headers then discard first line
            If hasHeaders = True Then
                MyReader.ReadLine().Skip(1)
            End If

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    'create temp sheet
                    Dim curSheet As New StructSheet With {
                        .sheetNum = currentRow(0),
                        .sheetName = currentRow(1)
                    }
                    'curSheet.viewName = currentRow(2)

                    'add sheet to list
                    sheetList.Add(curSheet)

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    Debug.Print("Line " & ex.Message & " is invalid.  Skipping")
                End Try
            End While

            Return sheetList.ToList
        End Using

    End Function


    Public Function getAllSections(curDoc As Document) As List(Of View)
        'get all viewports
        Dim vpCollector As New FilteredElementCollector(curDoc)
        vpCollector.OfCategory(BuiltInCategory.OST_Views)


        'output structural views to list
        Dim SectionList As New List(Of View)
        For Each curView As View In vpCollector
            If curView.ViewType = ViewType.Section Then
                If curView.Name <> "Structural Section" AndAlso curView.Name <> "Architectural Section" AndAlso curView.Name <> "Site Section" _
                    AndAlso curView.Name <> "Structural Framing Elevation" AndAlso curView.Name <> "Architectural Elevation" _
                    AndAlso curView.Name <> "Structural Building Elevation" AndAlso curView.Name <> "3/4"" steel" Then
                    SectionList.Add(curView)
                End If
            End If

        Next

        Return SectionList

    End Function

    Public Function getAllTextTypes(CurDoc As Document) As List(Of TextNoteType)
        Dim TextCollector As New FilteredElementCollector(CurDoc)
        TextCollector.OfClass(GetType(TextNoteType))

        Dim m_TextType As New List(Of TextNoteType)

        For Each F As TextNoteType In TextCollector
            If F.Name.Contains("5/64") Then
                m_TextType.Add(F)
            End If
        Next

        Return m_TextType

    End Function


End Class

Public Class ViewportSelectionFilter
    Implements ISelectionFilter

    Public Function AllowElement(ByVal elem As Element) As Boolean Implements ISelectionFilter.AllowElement
        If elem.Category.Id.IntegerValue = CInt(BuiltInCategory.OST_Viewports) Then
            Return True
        End If

        Return False
    End Function

    Public Function AllowReference(ByVal reference As Reference, ByVal position As XYZ) As Boolean Implements ISelectionFilter.AllowReference
        Return False
    End Function
End Class