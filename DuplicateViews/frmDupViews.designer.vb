'
' Created by SharpDevelop.
' User: antoine
' Date: 12/17/2015
' Time: 1:04 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class FrmDupViews
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
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.cmbNumDupes = New System.Windows.Forms.ComboBox()
        Me.rbDup = New System.Windows.Forms.RadioButton()
        Me.rbDupDetailing = New System.Windows.Forms.RadioButton()
        Me.rbDupDependent = New System.Windows.Forms.RadioButton()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbxViews = New System.Windows.Forms.ListBox()
        Me.SearchBox = New System.Windows.Forms.TextBox()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label3.Location = New System.Drawing.Point(12, 157)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(178, 23)
        Me.label3.TabIndex = 13
        Me.label3.Text = "Select Number of Duplicates:"
        Me.label3.UseCompatibleTextRendering = True
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(12, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(154, 23)
        Me.label2.TabIndex = 11
        Me.label2.Text = "Select Views to Duplicate:"
        Me.label2.UseCompatibleTextRendering = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(199, 318)
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
        Me.btnOK.Location = New System.Drawing.Point(118, 318)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseCompatibleTextRendering = True
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'cmbNumDupes
        '
        Me.cmbNumDupes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbNumDupes.FormattingEnabled = True
        Me.cmbNumDupes.Location = New System.Drawing.Point(12, 174)
        Me.cmbNumDupes.Name = "cmbNumDupes"
        Me.cmbNumDupes.Size = New System.Drawing.Size(262, 21)
        Me.cmbNumDupes.TabIndex = 14
        '
        'rbDup
        '
        Me.rbDup.Location = New System.Drawing.Point(6, 19)
        Me.rbDup.Name = "rbDup"
        Me.rbDup.Size = New System.Drawing.Size(104, 24)
        Me.rbDup.TabIndex = 15
        Me.rbDup.TabStop = True
        Me.rbDup.Text = "Duplicate"
        Me.rbDup.UseCompatibleTextRendering = True
        Me.rbDup.UseVisualStyleBackColor = True
        '
        'rbDupDetailing
        '
        Me.rbDupDetailing.Location = New System.Drawing.Point(6, 49)
        Me.rbDupDetailing.Name = "rbDupDetailing"
        Me.rbDupDetailing.Size = New System.Drawing.Size(188, 24)
        Me.rbDupDetailing.TabIndex = 16
        Me.rbDupDetailing.TabStop = True
        Me.rbDupDetailing.Text = "Duplicate with Detailing"
        Me.rbDupDetailing.UseCompatibleTextRendering = True
        Me.rbDupDetailing.UseVisualStyleBackColor = True
        '
        'rbDupDependent
        '
        Me.rbDupDependent.Location = New System.Drawing.Point(6, 79)
        Me.rbDupDependent.Name = "rbDupDependent"
        Me.rbDupDependent.Size = New System.Drawing.Size(217, 24)
        Me.rbDupDependent.TabIndex = 17
        Me.rbDupDependent.TabStop = True
        Me.rbDupDependent.Text = "Duplicate as a Dependent"
        Me.rbDupDependent.UseCompatibleTextRendering = True
        Me.rbDupDependent.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.rbDup)
        Me.groupBox1.Controls.Add(Me.rbDupDependent)
        Me.groupBox1.Controls.Add(Me.rbDupDetailing)
        Me.groupBox1.Location = New System.Drawing.Point(12, 201)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(262, 111)
        Me.groupBox1.TabIndex = 18
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Duplicate Settings"
        Me.groupBox1.UseCompatibleTextRendering = True
        '
        'lbxViews
        '
        Me.lbxViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxViews.FormattingEnabled = True
        Me.lbxViews.Location = New System.Drawing.Point(12, 54)
        Me.lbxViews.Name = "lbxViews"
        Me.lbxViews.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbxViews.Size = New System.Drawing.Size(262, 95)
        Me.lbxViews.TabIndex = 20
        '
        'SearchBox
        '
        Me.SearchBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchBox.Location = New System.Drawing.Point(12, 35)
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Size = New System.Drawing.Size(262, 20)
        Me.SearchBox.TabIndex = 22
        '
        'FrmDupViews
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 349)
        Me.Controls.Add(Me.SearchBox)
        Me.Controls.Add(Me.lbxViews)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.cmbNumDupes)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.KeyPreview = True
        Me.Name = "FrmDupViews"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Batch Duplicate Views"
        Me.groupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private lbxViews As System.Windows.Forms.ListBox
    'Private clbViewsToDup As System.Windows.Forms.CheckedListBox
    Private groupBox1 As System.Windows.Forms.GroupBox
    Private rbDupDependent As System.Windows.Forms.RadioButton
    Private rbDupDetailing As System.Windows.Forms.RadioButton
    Private rbDup As System.Windows.Forms.RadioButton
    Private cmbNumDupes As System.Windows.Forms.ComboBox
    Private btnOK As System.Windows.Forms.Button
    Private btnCancel As System.Windows.Forms.Button
    Private label2 As System.Windows.Forms.Label
    Private label3 As System.Windows.Forms.Label
    Friend WithEvents SearchBox As Windows.Forms.TextBox
End Class
