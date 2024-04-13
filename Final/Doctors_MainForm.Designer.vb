<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Doctors_MainForm
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Appointment = New System.Windows.Forms.Button()
        Me.Leave_Form = New System.Windows.Forms.Button()
        Me.Medicines = New System.Windows.Forms.Button()
        Me.Complaints = New System.Windows.Forms.Button()
        Me.Logout = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(12, 90)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1426, 800)
        Me.Panel1.TabIndex = 3
        '
        'Appointment
        '
        Me.Appointment.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Appointment.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Appointment.FlatAppearance.BorderSize = 0
        Me.Appointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Appointment.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Appointment.Location = New System.Drawing.Point(-1, 0)
        Me.Appointment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Appointment.Name = "Appointment"
        Me.Appointment.Size = New System.Drawing.Size(254, 84)
        Me.Appointment.TabIndex = 26
        Me.Appointment.Text = "Appointment"
        Me.Appointment.UseVisualStyleBackColor = False
        '
        'Leave_Form
        '
        Me.Leave_Form.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Leave_Form.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Leave_Form.FlatAppearance.BorderSize = 0
        Me.Leave_Form.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Leave_Form.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Leave_Form.Location = New System.Drawing.Point(260, 0)
        Me.Leave_Form.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Leave_Form.Name = "Leave_Form"
        Me.Leave_Form.Size = New System.Drawing.Size(319, 84)
        Me.Leave_Form.TabIndex = 27
        Me.Leave_Form.Text = "Leave_Form"
        Me.Leave_Form.UseVisualStyleBackColor = False
        '
        'Medicines
        '
        Me.Medicines.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Medicines.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Medicines.FlatAppearance.BorderSize = 0
        Me.Medicines.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Medicines.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Medicines.Location = New System.Drawing.Point(585, 0)
        Me.Medicines.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Medicines.Name = "Medicines"
        Me.Medicines.Size = New System.Drawing.Size(350, 84)
        Me.Medicines.TabIndex = 28
        Me.Medicines.Text = "Medicines"
        Me.Medicines.UseVisualStyleBackColor = False
        '
        'Complaints
        '
        Me.Complaints.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Complaints.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Complaints.FlatAppearance.BorderSize = 0
        Me.Complaints.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Complaints.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Complaints.Location = New System.Drawing.Point(941, 0)
        Me.Complaints.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Complaints.Name = "Complaints"
        Me.Complaints.Size = New System.Drawing.Size(360, 84)
        Me.Complaints.TabIndex = 29
        Me.Complaints.Text = "Complaints"
        Me.Complaints.UseVisualStyleBackColor = False
        '
        'Logout
        '
        Me.Logout.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Logout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Logout.FlatAppearance.BorderSize = 0
        Me.Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Logout.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Logout.Location = New System.Drawing.Point(1307, 0)
        Me.Logout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Logout.Name = "Logout"
        Me.Logout.Size = New System.Drawing.Size(155, 84)
        Me.Logout.TabIndex = 31
        Me.Logout.Text = "Logout"
        Me.Logout.UseVisualStyleBackColor = False
        '
        'Doctors_MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1476, 918)
        Me.Controls.Add(Me.Logout)
        Me.Controls.Add(Me.Complaints)
        Me.Controls.Add(Me.Medicines)
        Me.Controls.Add(Me.Leave_Form)
        Me.Controls.Add(Me.Appointment)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Doctors_MainForm"
        Me.Text = "Doctors_MainForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Appointment As System.Windows.Forms.Button
    Friend WithEvents Leave_Form As System.Windows.Forms.Button
    Friend WithEvents Medicines As System.Windows.Forms.Button
    Friend WithEvents Complaints As System.Windows.Forms.Button
    Friend WithEvents Logout As System.Windows.Forms.Button

End Class
