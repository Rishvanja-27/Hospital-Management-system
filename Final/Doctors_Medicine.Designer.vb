<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Doctors_Medicine
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Doctors_Medicine))
        Me.searchTextBox = New System.Windows.Forms.TextBox()
        Me.searchButton = New System.Windows.Forms.PictureBox()
        Me.listView1 = New System.Windows.Forms.ListView()
        CType(Me.searchButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'searchTextBox
        '
        Me.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.searchTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchTextBox.Location = New System.Drawing.Point(346, 78)
        Me.searchTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.searchTextBox.Multiline = True
        Me.searchTextBox.Name = "searchTextBox"
        Me.searchTextBox.Size = New System.Drawing.Size(720, 48)
        Me.searchTextBox.TabIndex = 0
        Me.searchTextBox.Text = "search medicine..."
        Me.searchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'searchButton
        '
        Me.searchButton.BackColor = System.Drawing.SystemColors.ControlDark
        Me.searchButton.ErrorImage = CType(resources.GetObject("searchButton.ErrorImage"), System.Drawing.Image)
        Me.searchButton.Image = CType(resources.GetObject("searchButton.Image"), System.Drawing.Image)
        Me.searchButton.ImageLocation = "C:\Users\rangu\Downloadsicons8-search-50"
        Me.searchButton.InitialImage = CType(resources.GetObject("searchButton.InitialImage"), System.Drawing.Image)
        Me.searchButton.Location = New System.Drawing.Point(1072, 78)
        Me.searchButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.searchButton.Name = "searchButton"
        Me.searchButton.Size = New System.Drawing.Size(47, 47)
        Me.searchButton.TabIndex = 1
        Me.searchButton.TabStop = False
        '
        'listView1
        '
        Me.listView1.Location = New System.Drawing.Point(30, 181)
        Me.listView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.listView1.Name = "listView1"
        Me.listView1.Size = New System.Drawing.Size(1344, 476)
        Me.listView1.TabIndex = 2
        Me.listView1.UseCompatibleStateImageBehavior = False
        '
        'Doctors_Medicine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1388, 779)
        Me.Controls.Add(Me.listView1)
        Me.Controls.Add(Me.searchButton)
        Me.Controls.Add(Me.searchTextBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Doctors_Medicine"
        Me.Text = "Doctors_Medicine"
        CType(Me.searchButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents searchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents searchButton As System.Windows.Forms.PictureBox
    Friend WithEvents listView1 As System.Windows.Forms.ListView
End Class
