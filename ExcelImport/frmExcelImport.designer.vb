'
' Created by SharpDevelop.
' User: antoine
' Date: 3/27/2015
' Time: 2:58 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class frmExcelImport
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
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.BrowseBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExcelPathTextBox = New System.Windows.Forms.TextBox()
        Me.FileLocBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SheetListBox = New System.Windows.Forms.ListBox()
        Me.OKBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CancelBtn
        '
        Me.CancelBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CancelBtn.Location = New System.Drawing.Point(602, 132)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(75, 23)
        Me.CancelBtn.TabIndex = 4
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.UseCompatibleTextRendering = True
        Me.CancelBtn.UseVisualStyleBackColor = True
        '
        'BrowseBtn
        '
        Me.BrowseBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrowseBtn.Location = New System.Drawing.Point(694, 48)
        Me.BrowseBtn.Name = "BrowseBtn"
        Me.BrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.BrowseBtn.TabIndex = 5
        Me.BrowseBtn.Text = "Browse"
        Me.BrowseBtn.UseCompatibleTextRendering = True
        Me.BrowseBtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Excel File to Import:"
        '
        'ExcelPathTextBox
        '
        Me.ExcelPathTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExcelPathTextBox.Location = New System.Drawing.Point(147, 48)
        Me.ExcelPathTextBox.Name = "ExcelPathTextBox"
        Me.ExcelPathTextBox.Size = New System.Drawing.Size(500, 23)
        Me.ExcelPathTextBox.TabIndex = 7
        '
        'FileLocBrowse
        '
        Me.FileLocBrowse.FileName = "FileLocBrowse"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Sheet to Import:"
        '
        'SheetListBox
        '
        Me.SheetListBox.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SheetListBox.FormattingEnabled = True
        Me.SheetListBox.ItemHeight = 15
        Me.SheetListBox.Items.AddRange(New Object() {"Select an Excel File"})
        Me.SheetListBox.Location = New System.Drawing.Point(147, 92)
        Me.SheetListBox.Name = "SheetListBox"
        Me.SheetListBox.Size = New System.Drawing.Size(120, 19)
        Me.SheetListBox.TabIndex = 9
        '
        'OKBtn
        '
        Me.OKBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OKBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OKBtn.Location = New System.Drawing.Point(694, 132)
        Me.OKBtn.Name = "OKBtn"
        Me.OKBtn.Size = New System.Drawing.Size(75, 23)
        Me.OKBtn.TabIndex = 10
        Me.OKBtn.Text = "OK"
        Me.OKBtn.UseCompatibleTextRendering = True
        Me.OKBtn.UseVisualStyleBackColor = True
        '
        'frmExcelImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 179)
        Me.Controls.Add(Me.OKBtn)
        Me.Controls.Add(Me.SheetListBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ExcelPathTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BrowseBtn)
        Me.Controls.Add(Me.CancelBtn)
        Me.KeyPreview = True
        Me.Name = "frmExcelImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Impot Excel Table"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private CancelBtn As System.Windows.Forms.Button
    Private WithEvents BrowseBtn As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents ExcelPathTextBox As Windows.Forms.TextBox
    Friend WithEvents FileLocBrowse As Windows.Forms.OpenFileDialog
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents SheetListBox As Windows.Forms.ListBox
    Private WithEvents OKBtn As Windows.Forms.Button
End Class
