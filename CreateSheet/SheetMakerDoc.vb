
Imports System
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics
imports System.Globalization

Namespace SheetMaker


    <Transaction(TransactionMode.Manual)> Partial Public Class SheetMakerDoc
        Implements IExternalCommand
        Public Shared uiapp As UIApplication

        Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute


            Dim curDoc As Document = commandData.Application.ActiveUIDocument.Document

            'run sheet macro 
            RunSheetMaker(curDoc)
            Return Result.Succeeded

        End Function
    End Class

End Namespace