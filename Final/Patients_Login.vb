Imports System.Data.SqlClient
Public Class Patients_Login

    Private Property par As Patients_MainForm
    Public Sub New(ByVal parent As Patients_MainForm)
        InitializeComponent()
        par = parent
    End Sub

    Public Function HandleSubmit() As Boolean
        ' Email validation
        Dim email As String = RichTextBox2.Text
        If Not System.Text.RegularExpressions.Regex.IsMatch(email, "^\S+@\S+(\.\S+)+$") Then
            MessageBox.Show("Please enter a valid email address.")
            Return False
        End If
        Dim pattern As String = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"

        ' Initial check if the password matches the overall pattern
        Dim password As String = TextBox1.Text
        If Not System.Text.RegularExpressions.Regex.IsMatch(password, pattern) Then
            ' If the password does not meet the overall pattern, check for specific criteria
            Dim errorMessage As New System.Text.StringBuilder("Password does not meet the following criteria:" & Environment.NewLine)

            ' Check for minimum length
            If password.Length < 8 Then
                errorMessage.AppendLine("- Must be at least 8 characters long.")
            End If

            ' Check for uppercase letter
            If Not System.Text.RegularExpressions.Regex.IsMatch(password, "(?=.*[A-Z])") Then
                errorMessage.AppendLine("- Must contain at least one uppercase letter.")
            End If

            ' Check for a number
            If Not System.Text.RegularExpressions.Regex.IsMatch(password, "(?=.*\d)") Then
                errorMessage.AppendLine("- Must contain at least one number.")
            End If

            ' Check for a special character
            If Not System.Text.RegularExpressions.Regex.IsMatch(password, "(?=.*[^a-zA-Z\d])") Then
                errorMessage.AppendLine("- Must contain at least one special character.")
            End If

            MessageBox.Show(errorMessage.ToString())
            Return False
        End If
        Return True

    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Validate email format and password complexity using HandleSubmit function
        'If Not HandleSubmit() Then
        'Return ' If validation fails, return without further processing
        'End If

        Dim email As String = RichTextBox2.Text
        Dim password As String = TextBox1.Text

        ' Check if the user is an admin
        If email = "admin@example.com" AndAlso password = "Adminpass@123" Then ' Change with appropriate admin credentials
            par.Hide()
            Dim adminMainForm As New Admin_MainForm(par)
            adminMainForm.Show()
            ' Call the cleanup function to reset the form fields
            CleanupForm()
            Return
        End If

        ' Check if the user is a doctor
        Dim doctorID As Integer = GetDoctorID(email, password)
        If doctorID > 0 Then
            par.Hide()
            Dim doctorsMainForm As New Doctors_MainForm(par, doctorID)
            doctorsMainForm.Show()
            ' Call the cleanup function to reset the form fields
            CleanupForm()
            Return
        End If

        ' Check if the user is a patient
        Dim patientID As Integer = GetPatientID(email, password)
        If patientID > 0 Then
            par.Hide()
            Dim patientsMainPage As New Patients_MainPage(par, patientID)
            patientsMainPage.Show()
            ' Call the cleanup function to reset the form fields
            CleanupForm()
            Return
        End If

        ' If none of the credentials match, show invalid message
        MessageBox.Show("Invalid email or password.")
    End Sub



    Private Sub LoginForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        par.Close()
        Application.Exit()
    End Sub

    Private Function GetDoctorID(ByVal email As String, ByVal password As String) As Integer
        Dim doctorID As Integer = -1
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        ' Query to retrieve doctor's ID based on email and password
        Dim query As String = "SELECT Doctor_ID_Staff_ID FROM Doctor_Staff WHERE Email = @Email AND Password = @Password"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Password", password)

                Try
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        doctorID = Convert.ToInt32(result)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error retrieving doctor's ID: " & ex.Message)
                End Try
            End Using
        End Using

        Return doctorID
    End Function

    Private Function GetPatientID(ByVal email As String, ByVal password As String) As Integer
        Dim patientID As Integer = -1
        Dim connectionString As String = "Data Source=RASHAADPC\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        ' Query to retrieve doctor's ID based on email and password
        Dim query As String = "SELECT Patient_ID FROM Patient WHERE Email = @Email AND Password = @Password"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Password", password)

                Try
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        patientID = Convert.ToInt32(result)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error retrieving patient's ID: " & ex.Message)
                End Try
            End Using
        End Using

        Return patientID
    End Function
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox1.PasswordChar = ""
        Else
            TextBox1.PasswordChar = "*"
        End If
    End Sub

    Private Sub CleanupForm()
        RichTextBox2.Text = ""
        TextBox1.Text = ""
    End Sub

    Private Sub Patients_Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class