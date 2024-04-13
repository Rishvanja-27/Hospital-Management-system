<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Doctors_LeaveForm
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
        Me.StartDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.EndDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ReasonComboBox = New System.Windows.Forms.ComboBox()
        Me.CommentsTextBox = New System.Windows.Forms.TextBox()
        Me.SubmitButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'StartDateTimePicker
        '
        Me.StartDateTimePicker.CalendarFont = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.StartDateTimePicker.Location = New System.Drawing.Point(250, 118)
        Me.StartDateTimePicker.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.StartDateTimePicker.Name = "StartDateTimePicker"
        Me.StartDateTimePicker.Size = New System.Drawing.Size(288, 22)
        Me.StartDateTimePicker.TabIndex = 0
        '
        'EndDateTimePicker
        '
        Me.EndDateTimePicker.CalendarFont = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EndDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.EndDateTimePicker.Location = New System.Drawing.Point(638, 118)
        Me.EndDateTimePicker.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.EndDateTimePicker.Name = "EndDateTimePicker"
        Me.EndDateTimePicker.Size = New System.Drawing.Size(288, 22)
        Me.EndDateTimePicker.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(250, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(288, 36)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Start Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(638, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(288, 36)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "End Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ReasonComboBox
        '
        Me.ReasonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ReasonComboBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReasonComboBox.FormattingEnabled = True
        Me.ReasonComboBox.Location = New System.Drawing.Point(250, 220)
        Me.ReasonComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ReasonComboBox.Name = "ReasonComboBox"
        Me.ReasonComboBox.Size = New System.Drawing.Size(391, 36)
        Me.ReasonComboBox.TabIndex = 4
        '
        'CommentsTextBox
        '
        Me.CommentsTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CommentsTextBox.Location = New System.Drawing.Point(248, 293)
        Me.CommentsTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CommentsTextBox.Multiline = True
        Me.CommentsTextBox.Name = "CommentsTextBox"
        Me.CommentsTextBox.Size = New System.Drawing.Size(679, 174)
        Me.CommentsTextBox.TabIndex = 5
        '
        'SubmitButton
        '
        Me.SubmitButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubmitButton.Location = New System.Drawing.Point(488, 503)
        Me.SubmitButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(258, 73)
        Me.SubmitButton.TabIndex = 6
        Me.SubmitButton.Text = "Submit"
        Me.SubmitButton.UseVisualStyleBackColor = True
        '
        'Doctors_LeaveForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1244, 633)
        Me.Controls.Add(Me.SubmitButton)
        Me.Controls.Add(Me.CommentsTextBox)
        Me.Controls.Add(Me.ReasonComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.EndDateTimePicker)
        Me.Controls.Add(Me.StartDateTimePicker)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Doctors_LeaveForm"
        Me.Text = "Doctors_LeaveForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StartDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents EndDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ReasonComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CommentsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SubmitButton As System.Windows.Forms.Button
End Class
