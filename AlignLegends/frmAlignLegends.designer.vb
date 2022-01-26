'
' Created by SharpDevelop.
' User: antoine
' Date: 3/27/2015
' Time: 2:58 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class frmAlignLegends
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
        Me.cmbPrimary = New System.Windows.Forms.ComboBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lbxViews = New System.Windows.Forms.ListBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.cmbAlignType = New System.Windows.Forms.ComboBox()
        Me.SearchBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'cmbPrimary
        '
        Me.cmbPrimary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPrimary.FormattingEnabled = True
        Me.cmbPrimary.Location = New System.Drawing.Point(11, 32)
        Me.cmbPrimary.Name = "cmbPrimary"
        Me.cmbPrimary.Size = New System.Drawing.Size(260, 21)
        Me.cmbPrimary.TabIndex = 0
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(11, 7)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(260, 22)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Select legend to align to:"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.label1.UseCompatibleTextRendering = True
        '
        'lbxViews
        '
        Me.lbxViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxViews.FormattingEnabled = True
        Me.lbxViews.Location = New System.Drawing.Point(12, 106)
        Me.lbxViews.Name = "lbxViews"
        Me.lbxViews.ScrollAlwaysVisible = True
        Me.lbxViews.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbxViews.Size = New System.Drawing.Size(259, 173)
        Me.lbxViews.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(196, 341)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseCompatibleTextRendering = True
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(115, 341)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseCompatibleTextRendering = True
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(11, 60)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(146, 23)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Select legends to align:"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.label2.UseCompatibleTextRendering = True
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label3.Location = New System.Drawing.Point(12, 282)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(260, 22)
        Me.label3.TabIndex = 6
        Me.label3.Text = "Select alignment point:"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.label3.UseCompatibleTextRendering = True
        '
        'cmbAlignType
        '
        Me.cmbAlignType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbAlignType.FormattingEnabled = True
        Me.cmbAlignType.Items.AddRange(New Object() {"Top Left", "Top Right", "Center", "Bottom Left", "Bottom Right"})
        Me.cmbAlignType.Location = New System.Drawing.Point(12, 307)
        Me.cmbAlignType.Name = "cmbAlignType"
        Me.cmbAlignType.Size = New System.Drawing.Size(260, 21)
        Me.cmbAlignType.TabIndex = 7
        '
        'SearchBox
        '
        Me.SearchBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchBox.Location = New System.Drawing.Point(12, 87)
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Size = New System.Drawing.Size(259, 20)
        Me.SearchBox.TabIndex = 9
        '
        'frmAlignLegends
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 374)
        Me.Controls.Add(Me.SearchBox)
        Me.Controls.Add(Me.cmbAlignType)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lbxViews)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.cmbPrimary)
        Me.KeyPreview = True
        Me.Name = "frmAlignLegends"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Align Legends"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private cmbAlignType As System.Windows.Forms.ComboBox
    Private label3 As System.Windows.Forms.Label
    Private label2 As System.Windows.Forms.Label
    Private btnCancel As System.Windows.Forms.Button
    Private btnOK As System.Windows.Forms.Button
    Private lbxViews As System.Windows.Forms.ListBox
    Private label1 As System.Windows.Forms.Label
    Private cmbPrimary As System.Windows.Forms.ComboBox
    Friend WithEvents SearchBox As Windows.Forms.TextBox
End Class
