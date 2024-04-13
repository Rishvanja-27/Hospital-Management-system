Imports System.Data.SqlClient

Public Class Patients_Register
    Private Property par As Patients_MainForm

    Private phoneNumber As String
    Private Patient_Name As String
    Private age As Integer
    Private email As String
    Private gender As String
    Private bloodGroup As String
    Private password As String

   Public Function HandleSubmit() As Boolean
        ' Phone number validation
        phoneNumber = RichTextBox1.Text
        ' Check if the name is not null or empty
        Patient_Name = RichTextBox2.Text
        If String.IsNullOrWhiteSpace(Patient_Name) Then
            MessageBox.Show("Name cannot be empty.")
            Return False ' The name is not valid
        End If

        ' Age validation
        If Not Integer.TryParse(RichTextBox3.Text, age) OrElse age < 0 OrElse age > 120 Then
            MessageBox.Show("Please enter a valid age between 0 and 120.")
            Return False
        End If

        ' Email validation
        email = RichTextBox4.Text
        If Not System.Text.RegularExpressions.Regex.IsMatch(email, "^\S+@\S+\.\S+$") Then
            MessageBox.Show("Please enter a valid email address.")
            Return False
        End If

        gender = RichTextBox5.Text
        If Not (gender = "Male" OrElse gender = "Female" OrElse gender = "Others") Then
            MessageBox.Show("Please enter a valid gender: Male, Female, or Others.")
            Return False
        End If

        ' Blood group validation
        Dim validBloodGroups As HashSet(Of String) = New HashSet(Of String)({"A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"})
        bloodGroup = RichTextBox6.Text

        If Not String.IsNullOrWhiteSpace(bloodGroup) And Not validBloodGroups.Contains(bloodGroup) Then
            MessageBox.Show("Please enter a valid blood group (A+, A-, B+, B-, O+, O-, AB+, AB-).")
            Return False
        End If


        Dim pattern As String = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"

        ' Initial check if the password matches the overall pattern
        password = TextBox7.Text
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

        ' All validations passed, proceed with data processing or storing logic
        ' For now, just return true to indicate successful validation
        Return True
    End Function


    Public Sub New(ByVal parent As Patients_MainForm)
        InitializeComponent()
        par = parent
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set a common font size for all RichTextBox controls
        Dim commonFontSize As Integer = 16 ' You can change this to your desired font size

        RichTextBox1.Font = New Font(RichTextBox1.Font.FontFamily, commonFontSize)
        RichTextBox2.Font = New Font(RichTextBox2.Font.FontFamily, commonFontSize)
        RichTextBox3.Font = New Font(RichTextBox3.Font.FontFamily, commonFontSize)
        RichTextBox4.Font = New Font(RichTextBox4.Font.FontFamily, commonFontSize)
        RichTextBox5.Font = New Font(RichTextBox5.Font.FontFamily, commonFontSize)
        RichTextBox6.Font = New Font(RichTextBox6.Font.FontFamily, commonFontSize)
        TextBox7.Font = New Font(TextBox7.Font.FontFamily, commonFontSize)

        ' Create a dummy label and set focus to it to prevent any control from being selected by default
        Dim dummyLabel As New Label()
        Me.Controls.Add(dummyLabel)
        dummyLabel.Visible = False
        dummyLabel.Select()

        ' Set placeholder text for RichTextBox controls
        SetRichTextBoxPlaceholder(RichTextBox1, "Phone No.*")
        SetRichTextBoxPlaceholder(RichTextBox2, "Name*")
        SetRichTextBoxPlaceholder(RichTextBox3, "Age*")
        SetRichTextBoxPlaceholder(RichTextBox4, "Email Id*")
        SetRichTextBoxPlaceholder(RichTextBox5, "Gender*")
        SetRichTextBoxPlaceholder(RichTextBox6, "Blood Group*")
        TextBox7.Text = "Password*"
    End Sub

    Private Sub RichTextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.Enter, RichTextBox2.Enter, RichTextBox3.Enter, RichTextBox4.Enter, RichTextBox5.Enter, RichTextBox6.Enter
        HandleRichTextBoxEnter(sender)
    End Sub

    Private Sub RichTextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.Leave, RichTextBox2.Leave, RichTextBox3.Leave, RichTextBox4.Leave, RichTextBox5.Leave, RichTextBox6.Leave
        HandleRichTextBoxLeave(sender)
    End Sub

    Private Sub HandleRichTextBoxEnter(ByVal richTextBox As RichTextBox)
        ' Remove placeholder text when the RichTextBox is entered
        Dim placeholder As String = GetRichTextBoxPlaceholder(richTextBox)
        If richTextBox.Text = placeholder Then
            richTextBox.Text = ""
            richTextBox.ForeColor = Color.Black ' Set text color to black
        End If
    End Sub

    Private Sub HandleRichTextBoxLeave(ByVal richTextBox As RichTextBox)
        ' Add placeholder text when the RichTextBox is exited and no text has been entered
        Dim placeholder As String = GetRichTextBoxPlaceholder(richTextBox)
        If richTextBox.Text = "" Then
            richTextBox.Text = placeholder
            richTextBox.ForeColor = Color.Gray ' Set text color to gray
        End If
    End Sub

    Private Function GetRichTextBoxPlaceholder(ByVal richTextBox As RichTextBox) As String
        ' Return the placeholder text based on the RichTextBox control
        Select Case richTextBox.Name
            Case "RichTextBox1"
                Return "Phone No.*"
            Case "RichTextBox2"
                Return "Name*"
            Case "RichTextBox3"
                Return "Age*"
            Case "RichTextBox4"
                Return "Email Id*"
            Case "RichTextBox5"
                Return "Gender*"
            Case "RichTextBox6"
                Return "Blood Group*"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub TextBox7_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.Enter
        ' Remove placeholder text when the TextBox is entered
        If TextBox7.Text = "Password*" Then
            TextBox7.Text = ""
            TextBox7.ForeColor = Color.Black ' Set text color to black
        End If
    End Sub

    Private Sub TextBox7_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.Leave
        ' Add placeholder text when the TextBox is exited and no text has been entered
        If TextBox7.Text = "" Then
            TextBox7.Text = "Password*"
            TextBox7.ForeColor = Color.Gray ' Set text color to gray
        End If
    End Sub

    Private Sub SetRichTextBoxPlaceholder(ByVal richTextBox As RichTextBox, ByVal placeholder As String)
        ' Set placeholder text and color for RichTextBox
        richTextBox.Text = placeholder
        richTextBox.ForeColor = Color.Gray
    End Sub

    Private Sub RegisterForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        par.Close()
        Application.Exit()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Hide the current form (Form2)
        If Not HandleSubmit() Then
            Return
        End If

        ' Check if the provided email is already registered
        Dim isRegistered As Boolean = CheckIfRegistered()

        If isRegistered Then
            MessageBox.Show("This email is already registered. Please Login", "Already Registered", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Register the patient
        RegisterPatient()
    End Sub

    Private Function CheckIfRegistered() As Boolean
        ' Connection string to your database
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Query to check if the email exists
        Dim query As String = "SELECT COUNT(*) FROM Patient WHERE Email = @Email"

        ' Create a connection object
        Using connection As New SqlConnection(connectionString)
            ' Create a command object with the query and connection
            Dim command As New SqlCommand(query, connection)

            ' Add the email parameter to the command
            command.Parameters.AddWithValue("@Email", email)

            ' Open the database connection
            connection.Open()

            ' Execute the query and get the result
            ' Execute the query and get the result
            Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
            ' Return True if the count is greater than 0, indicating the email exists in the database
            Return result > 0
        End Using
    End Function

    Private Sub RegisterPatient()
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        Dim query As String = "INSERT INTO Patient (Name, Password, Blood_Group, Age, Mobile_Number, Email, Gender) VALUES (@Name, @Password, @Blood_Group, @Age, @Mobile_Number, @Email, @Gender)"
        Try
            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Name", Patient_Name)
                command.Parameters.AddWithValue("@Password", password)
                command.Parameters.AddWithValue("@Blood_Group", bloodGroup)
                command.Parameters.AddWithValue("@Age", age)
                command.Parameters.AddWithValue("@Mobile_Number", phoneNumber)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Gender", gender)
                connection.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("Patient registered successfully. Go to login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Call the cleanup function to reset the form fields
                CleanupForm()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error registering patient: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox7.PasswordChar = ""
        Else
            TextBox7.PasswordChar = "*"
        End If
    End Sub
    Private Sub CleanupForm()
        ' Clear all textboxes and reset their placeholder text/colors
        RichTextBox1.Text = ""
        RichTextBox1.ForeColor = Color.Gray
        SetRichTextBoxPlaceholder(RichTextBox1, "Phone No.*")

        RichTextBox2.Text = ""
        RichTextBox2.ForeColor = Color.Gray
        SetRichTextBoxPlaceholder(RichTextBox2, "Name*")

        RichTextBox3.Text = ""
        RichTextBox3.ForeColor = Color.Gray
        SetRichTextBoxPlaceholder(RichTextBox3, "Age*")

        RichTextBox4.Text = ""
        RichTextBox4.ForeColor = Color.Gray
        SetRichTextBoxPlaceholder(RichTextBox4, "Email Id*")

        RichTextBox5.Text = ""
        RichTextBox5.ForeColor = Color.Gray
        SetRichTextBoxPlaceholder(RichTextBox5, "Gender*")

        RichTextBox6.Text = ""
        RichTextBox6.ForeColor = Color.Gray
        SetRichTextBoxPlaceholder(RichTextBox6, "Blood Group*")

        TextBox7.Text = ""
        TextBox7.ForeColor = Color.Gray
        TextBox7.Text = "Password*"
    End Sub

End Class
