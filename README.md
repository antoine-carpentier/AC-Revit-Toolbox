# AC-Revit-Toolbox


See Video Demo of the various commands of this addin here: https://www.youtube.com/watch?v=w9bV_fV1cxU

The custom commands are all committed in a single transaction, meaning you can always undo the changes created by the use of one of the tools by clicking on the “Undo” button or by pressing on “Ctrl”+”Z”. 
 
All the tools come with a search box, allowing you to easily filter through the lists of views, sheets and legends.


## How to Use

### Batch Align Legends

The “Batch Align Legends” tool allows you to easily align legends on sheet, without the need to move them one at a time. 

First, select the legend you want to use as the template.  
Then, select all the legends you want to align with the selected template. You can select multiple legends by holding down the Control key (Ctrl) while selecting them, or you can select a contiguous range of legends by holding down the shift key and selecting the first and last legends of the range.  
Finally, select the alignment point (for example, if you select “Top Left”, all the selected legend’s top left corners will be at the same place on their respective sheets).  
Press “OK” to finish the selection and align the legends. 

### Batch Align Views 

The “Batch Align Views” tool allows you to easily align views on sheet, without the need to move them one at a time.  
 
First, select the view you want to use as the template.  
Then, select all the views you want to align with the selected template. You can select multiple views by holding down the Control key (Ctrl) while selecting them, or you can select a contiguous range of views by holding down the Shift key and selecting the first and last views of the range.  
Finally, you can choose to also copy the crop region of the selected view template and apply it to all the selected views by checking the “Also Copy Crop” checkbox.  
Press “OK” to finish the selection and align the views. 

### Batch Apply Revisions

The “Batch Apply Revisions” tool allows you to turn on or off revisions on all selected sheets at once. 
  
First, select the revision you want to turn on or off. You can only select one at a time.  
Then, use the radio buttons to choose whether you want to turn on or off the selected revision.  
Finally, select on what sheets you want the selected revision to be modified. You can select multiple sheets by holding down the Control key (Ctrl) while selecting them, or you can select a contiguous range of sheets by holding down the Shift key and selecting the first and last sheets of the range.  
Alternatively, you can select to modify the selected revision on all sheets by checking the “Select all sheets” checkbox. 

### Batch Create Sheets

The “Batch Create Sheets” allows you to easily create a set of sheets and apply a selected titleblock to it. 
 
First, select a CVS File containing all the sheet names and sheet numbers you want to create. You do not need to filter out existing sheets, only the new ones will be created. The existing ones won’t be affected by the tool.  
To create a new CVS File, press on the “+” button. You will be asked to select where you want the CVS File to be created, and then the CVS File will be open in Excel. Once the CVS File is created and filled appropriately, you can save and close and browse to the CVS file location in the “Sheet Maker” window.   
Then, simply select the wanted titleblock using the drop-down list.  
Finish by clicking on the “Make Sheets” button.  

### Batch Crop Views

The “Batch Crop Views” tool allows you to crop all selected structural plans at once, without the need to copy and paste the wanted crop region outline from plan to plan. 
 
First, select a cropped plan that will be used as the template.  
Then, select all the views you want the crop to match the template crop.   
Finish by clicking on the “OK” button. 

### Batch Duplicate Views
 
The “Batch Duplicate Views” tool allows you to easily duplicate all selected structural plans in how many duplicates you want. 

First, select all the views you want to duplicate. You can select multiple views by holding down the Control key (Ctrl) while selecting them, or you can select a contiguous range of views by holding down the Shift key and selecting the first and last views of the range.  
Then, select how many duplicates of each selected views you want to create.  
Finally, select if you want the selected views to be duplicated, duplicated with Detailing or duplicated as a Dependent.  
Finish by clicking on the “OK” button. 

### Batch Move Details

The “Batch Move Details” tool allows you to easily move selected views, details or sections from one sheet to another. 
 
After starting the command, a selection ribbon will appear below the Add-Ins ribbon.  
First, select the details or sections you want to move. Once they are all selected, press on “Finish” on the selection ribbon.  
Then, a new window will open. In that window, select on what sheet you want the selected viewports to be moved.  
Press “OK” to finish. 

### Batch Rename Sections
 
The “Batch Rename Sections” tool allows you to automatically rename all the sections in the project. 
 
If a section is on a sheet, it will be renamed “SheetNumber/DetailNumber” automatically.  
If it is not on a sheet, it will be renamed “Section X” automatically.  
Only sections whose names start with an “S” will be renamed, so that other special sections (like full building sections for example) will be unaffected by the tool.  
The sections that will be affected by the “Batch Rename Sections” tool will have a grey background. The unaffected ones will have a white background.  
You can manually modify section names in the “New” column.
You can prevent a section from being renamed by deleting its row in the Rename Sections grid. To do so, just click on the handle on the left of the section’s row to select the entire row, then press on the “Delete” key.  
You can permanently delete a section from the project by renaming it to “DELETE” in the “New” column. 

### Batch Rename Views

The “Batch Rename Views” tool allows you to automatically rename structural views in the project. 
 
First, select all the views you want to duplicate. You can select multiple views by holding down the Control key (Ctrl) while selecting them, or you can select a contiguous range of views by holding down the Shift key and selecting the first and last views of the range.  
Then, fill in the blank text boxes with the appropriate text. You can add, replace or delete any text in the view names.  
Finish by clicking on the “OK” button. 

### Batch Renumber Details

The “Batch Renumber Details” tool allows you to easily renumber some or all sections on a sheet. 
 
After starting the “Batch Renumber Details” tool, you will see an instruction ribbon at the bottom left of you screen, reading “Pick viewports in the desired order, then press Escape twice.”.  
Simply select all the sections/details you want to renumber in the desired order you want them numbered, and press on the “Escape” key twice. The selected viewports will be renumbered using the first available numbers starting from 1. 

### Import Excel Table  

The "Import Excel Table" tool allows you to very quickly import any Excel table you wish.

Simply select the Excel file containing the table you wish to import, then select which sheet you wish to import.  
The command will automatically create all the text nodes and detail lines required to draw the table as seen in Excel.
Note: The sheet will be imported exactly as seen in Excel, so make sure all the texts fit into their respective cells to avoid overflowing text. Hidden rows/columns will be ignored.




