'
' Created by SharpDevelop.
' User: antoine
' Date: 9/15/2014
' Time: 1:12 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class frmSheetMaker
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblSelectFile = New System.Windows.Forms.Label()
        Me.ofdCSVFile = New System.Windows.Forms.OpenFileDialog()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.tbxCSVFile = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cmbTitleblock = New System.Windows.Forms.ComboBox()
        Me.btnCreateCSV = New System.Windows.Forms.Button()
        Me.ttCreateCSV = New System.Windows.Forms.ToolTip(Me.components)
        Me.sfdCreateCSV = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'lblSelectFile
        '
        Me.lblSelectFile.Location = New System.Drawing.Point(12, 9)
        Me.lblSelectFile.Name = "lblSelectFile"
        Me.lblSelectFile.Size = New System.Drawing.Size(100, 23)
        Me.lblSelectFile.TabIndex = 0
        Me.lblSelectFile.Text = "Select CSV File:"
        Me.lblSelectFile.UseCompatibleTextRendering = True
        '
        'ofdCSVFile
        '
        Me.ofdCSVFile.FileName = "openFileDialog1"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(401, 4)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseCompatibleTextRendering = True
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'tbxCSVFile
        '
        Me.tbxCSVFile.Location = New System.Drawing.Point(118, 6)
        Me.tbxCSVFile.Name = "tbxCSVFile"
        Me.tbxCSVFile.Size = New System.Drawing.Size(277, 20)
        Me.tbxCSVFile.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(401, 72)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseCompatibleTextRendering = True
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnProcess.Location = New System.Drawing.Point(311, 72)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(84, 23)
        Me.btnProcess.TabIndex = 4
        Me.btnProcess.Text = "Make Sheets"
        Me.btnProcess.UseCompatibleTextRendering = True
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(12, 37)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(100, 23)
        Me.label1.TabIndex = 5
        Me.label1.Text = "Select Title Block:"
        Me.label1.UseCompatibleTextRendering = True
        '
        'cmbTitleblock
        '
        Me.cmbTitleblock.FormattingEnabled = True
        Me.cmbTitleblock.Location = New System.Drawing.Point(118, 34)
        Me.cmbTitleblock.Name = "cmbTitleblock"
        Me.cmbTitleblock.Size = New System.Drawing.Size(277, 21)
        Me.cmbTitleblock.TabIndex = 7
        '
        'btnCreateCSV
        '
        Me.btnCreateCSV.Location = New System.Drawing.Point(12, 72)
        Me.btnCreateCSV.Name = "btnCreateCSV"
        Me.btnCreateCSV.Size = New System.Drawing.Size(24, 23)
        Me.btnCreateCSV.TabIndex = 10
        Me.btnCreateCSV.Text = "+"
        Me.ttCreateCSV.SetToolTip(Me.btnCreateCSV, "Click button to create a default CSV file. You will be prompted to select a locat" &
        "ion to save the file. The CSV file will automatically open after it is created.")
        Me.btnCreateCSV.UseCompatibleTextRendering = True
        Me.btnCreateCSV.UseVisualStyleBackColor = True
        '
        'frmSheetMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 102)
        Me.Controls.Add(Me.btnCreateCSV)
        Me.Controls.Add(Me.cmbTitleblock)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.tbxCSVFile)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.lblSelectFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(499, 141)
        Me.Name = "frmSheetMaker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sheet Maker"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private sfdCreateCSV As System.Windows.Forms.SaveFileDialog
	Private ttCreateCSV As System.Windows.Forms.ToolTip
    Private WithEvents btnCreateCSV As System.Windows.Forms.Button
    Private cmbTitleblock As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
    Private WithEvents btnProcess As System.Windows.Forms.Button
    Private btnCancel As System.Windows.Forms.Button
	Private tbxCSVFile As System.Windows.Forms.TextBox
    Private WithEvents btnBrowse As System.Windows.Forms.Button
    Private WithEvents ofdCSVFile As System.Windows.Forms.OpenFileDialog
    Private lblSelectFile As System.Windows.Forms.Label
	
	Sub Button1Click(sender As Object, e As System.EventArgs)
		Me.Close
		
	End Sub
End Class
