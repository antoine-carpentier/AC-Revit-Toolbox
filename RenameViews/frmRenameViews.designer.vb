'
' Created by SharpDevelop.
' User: antoine
' Date: 12/17/2015
' Time: 1:04 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class frmRenameViews
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
        Me.label2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lbxViews = New System.Windows.Forms.ListBox()
        Me.SearchBox = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.ReplaceBox = New System.Windows.Forms.TextBox()
        Me.ByBox = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.AddBeginningBox = New System.Windows.Forms.TextBox()
        Me.AddEndBox = New System.Windows.Forms.TextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.DeleteBox = New System.Windows.Forms.TextBox()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(12, 13)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(178, 23)
        Me.label2.TabIndex = 11
        Me.label2.Text = "Select Views to Rename:"
        Me.label2.UseCompatibleTextRendering = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(199, 358)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseCompatibleTextRendering = True
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(118, 358)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseCompatibleTextRendering = True
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lbxViews
        '
        Me.lbxViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxViews.FormattingEnabled = True
        Me.lbxViews.Location = New System.Drawing.Point(12, 52)
        Me.lbxViews.Name = "lbxViews"
        Me.lbxViews.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbxViews.Size = New System.Drawing.Size(260, 108)
        Me.lbxViews.TabIndex = 20
        '
        'SearchBox
        '
        Me.SearchBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchBox.Location = New System.Drawing.Point(12, 34)
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Size = New System.Drawing.Size(260, 20)
        Me.SearchBox.TabIndex = 18
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label1.Location = New System.Drawing.Point(14, 284)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(64, 20)
        Me.label1.TabIndex = 21
        Me.label1.Text = "● Replace:"
        Me.label1.UseCompatibleTextRendering = True
        '
        'label4
        '
        Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label4.Location = New System.Drawing.Point(158, 284)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(23, 20)
        Me.label4.TabIndex = 23
        Me.label4.Text = "By:"
        Me.label4.UseCompatibleTextRendering = True
        '
        'ReplaceBox
        '
        Me.ReplaceBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ReplaceBox.Location = New System.Drawing.Point(75, 282)
        Me.ReplaceBox.Name = "ReplaceBox"
        Me.ReplaceBox.Size = New System.Drawing.Size(76, 20)
        Me.ReplaceBox.TabIndex = 25
        '
        'ByBox
        '
        Me.ByBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ByBox.Location = New System.Drawing.Point(181, 282)
        Me.ByBox.Name = "ByBox"
        Me.ByBox.Size = New System.Drawing.Size(84, 20)
        Me.ByBox.TabIndex = 26
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(15, 28)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(100, 23)
        Me.label3.TabIndex = 0
        Me.label3.Text = "● At the beginning:"
        Me.label3.UseCompatibleTextRendering = True
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(15, 60)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(100, 23)
        Me.label6.TabIndex = 2
        Me.label6.Text = "● At the end:"
        Me.label6.UseCompatibleTextRendering = True
        '
        'AddBeginningBox
        '
        Me.AddBeginningBox.Location = New System.Drawing.Point(121, 25)
        Me.AddBeginningBox.Name = "AddBeginningBox"
        Me.AddBeginningBox.Size = New System.Drawing.Size(76, 20)
        Me.AddBeginningBox.TabIndex = 28
        '
        'AddEndBox
        '
        Me.AddEndBox.Location = New System.Drawing.Point(121, 57)
        Me.AddEndBox.Name = "AddEndBox"
        Me.AddEndBox.Size = New System.Drawing.Size(76, 20)
        Me.AddEndBox.TabIndex = 29
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.AddEndBox)
        Me.groupBox1.Controls.Add(Me.AddBeginningBox)
        Me.groupBox1.Controls.Add(Me.label6)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Location = New System.Drawing.Point(14, 172)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(253, 94)
        Me.groupBox1.TabIndex = 27
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Add:"
        Me.groupBox1.UseCompatibleTextRendering = True
        '
        'label5
        '
        Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label5.Location = New System.Drawing.Point(14, 322)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(64, 20)
        Me.label5.TabIndex = 28
        Me.label5.Text = "● Delete:"
        Me.label5.UseCompatibleTextRendering = True
        '
        'DeleteBox
        '
        Me.DeleteBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DeleteBox.Location = New System.Drawing.Point(75, 319)
        Me.DeleteBox.Name = "DeleteBox"
        Me.DeleteBox.Size = New System.Drawing.Size(76, 20)
        Me.DeleteBox.TabIndex = 29
        '
        'frmRenameViews
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 389)
        Me.Controls.Add(Me.DeleteBox)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.ByBox)
        Me.Controls.Add(Me.ReplaceBox)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.SearchBox)
        Me.Controls.Add(Me.lbxViews)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.KeyPreview = True
        Me.Name = "frmRenameViews"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Batch Rename Views"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private DeleteBox As System.Windows.Forms.TextBox
	Private label5 As System.Windows.Forms.Label
	Private AddEndBox As System.Windows.Forms.TextBox
	Private AddBeginningBox As System.Windows.Forms.TextBox
	Private label6 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private ByBox As System.Windows.Forms.TextBox
	Private ReplaceBox As System.Windows.Forms.TextBox
	Private label4 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private withevents SearchBox As System.Windows.Forms.TextBox
	Private lbxViews As System.Windows.Forms.ListBox
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private btnOK As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private label2 As System.Windows.Forms.Label
End Class
