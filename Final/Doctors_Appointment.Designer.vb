<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Doctors_Appointment
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
        Me.listView1 = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'listView1
        '
        Me.listView1.BackColor = System.Drawing.SystemColors.Control
        Me.listView1.Location = New System.Drawing.Point(11, 41)
        Me.listView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.listView1.Name = "listView1"
        Me.listView1.Size = New System.Drawing.Size(1258, 348)
        Me.listView1.TabIndex = 0
        Me.listView1.UseCompatibleStateImageBehavior = False
        '
        'Doctors_Appointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 758)
        Me.Controls.Add(Me.listView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Doctors_Appointment"
        Me.Text = "Doctors_Appointment"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents listView1 As System.Windows.Forms.ListView
End Class
