Public Class Patients_MainForm

    Private originalColor As Color = Color.FromArgb(217, 217, 217)
    Private selectedColor As Color = Color.Gray

    Private Sub ResetButtonColors()
        ' Reset the color of all buttons to their original color
        Button1.BackColor = originalColor
        Button2.BackColor = originalColor
        ' Add more buttons as needed
    End Sub

    Sub SwitchPanel(ByVal panel As Form)
        Panel1.Controls.Clear()
        panel.TopLevel = False
        Panel1.Controls.Add(panel)
        panel.Show()
    End Sub

    Private Sub Patients_MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Call the Login_Click event handler method to display the login form initially
        Login_Click(sender, e)
    End Sub

    Private Sub Login_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Reset button colors before changing color of clicked button
        ResetButtonColors()
        ' Change color of clicked button
        Button2.BackColor = selectedColor

        Dim Patients_Login As New Patients_Login(Me)
        SwitchPanel(Patients_Login)
    End Sub

    Private Sub Register_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Reset button colors before changing color of clicked button
        ResetButtonColors()
        ' Change color of clicked button
        Button1.BackColor = selectedColor

        Dim Patients_Register As New Patients_Register(Me)
        SwitchPanel(Patients_Register)
    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
End Class
