<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Doctors_Complaints
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ComplaintsComboBox = New System.Windows.Forms.ComboBox()
        Me.ComplaintsTextBox = New System.Windows.Forms.TextBox()
        Me.SubmitButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ComplaintsComboBox
        '
        Me.ComplaintsComboBox.BackColor = System.Drawing.Color.White
        Me.ComplaintsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComplaintsComboBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComplaintsComboBox.FormattingEnabled = True
        Me.ComplaintsComboBox.Location = New System.Drawing.Point(80, 65)
        Me.ComplaintsComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComplaintsComboBox.Name = "ComplaintsComboBox"
        Me.ComplaintsComboBox.Size = New System.Drawing.Size(1074, 36)
        Me.ComplaintsComboBox.TabIndex = 0
        '
        'ComplaintsTextBox
        '
        Me.ComplaintsTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComplaintsTextBox.Location = New System.Drawing.Point(80, 156)
        Me.ComplaintsTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComplaintsTextBox.Multiline = True
        Me.ComplaintsTextBox.Name = "ComplaintsTextBox"
        Me.ComplaintsTextBox.Size = New System.Drawing.Size(1074, 289)
        Me.ComplaintsTextBox.TabIndex = 1
        '
        'SubmitButton
        '
        Me.SubmitButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubmitButton.Location = New System.Drawing.Point(488, 503)
        Me.SubmitButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(258, 73)
        Me.SubmitButton.TabIndex = 7
        Me.SubmitButton.Text = "Submit"
        Me.SubmitButton.UseVisualStyleBackColor = True
        '
        'Doctors_Complaints
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 623)
        Me.Controls.Add(Me.SubmitButton)
        Me.Controls.Add(Me.ComplaintsTextBox)
        Me.Controls.Add(Me.ComplaintsComboBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Doctors_Complaints"
        Me.Text = "Doctors_Complaints"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComplaintsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ComplaintsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SubmitButton As System.Windows.Forms.Button
End Class
