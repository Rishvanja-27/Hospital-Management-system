Imports System.Data.SqlClient
Imports System.Globalization

Public Class Admin_AddDoctor
    ' Connection string for the database
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

    ' Event handler for the TextChanged event of the email TextBox
    Private Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Call the function to validate the email format
        ValidateEmail()
    End Sub

    ' Function to validate the email format
    Private Function ValidateEmail()
        ' Get the entered email address
        Dim enteredEmail As String = txtEmail.Text.Trim()

        ' Check if the entered email address is in a valid format
        If IsValidEmail(enteredEmail) Then
            ' Clear any previous error message
            ErrorProvider1.SetError(txtEmail, "")
            Return True
        Else
            ' Set error message for invalid email format
            ErrorProvider1.SetError(txtEmail, "Invalid email address. Please enter a valid email.")
            Return False
        End If
    End Function

    ' Function to validate the email format using regular expression
    Private Function IsValidEmail(ByVal email As String) As Boolean
        ' Regular expression for basic email validation
        Dim emailRegex As New System.Text.RegularExpressions.Regex("^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$")

        ' Check if the email matches the regular expression
        Return emailRegex.IsMatch(email)
    End Function

    ' Click event handler for the Add Doctor button
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        ' Check for email and mobile number validity before updating the database
        If ValidateEmail() AndAlso ValidateMobileNumber() Then
            ' Update the database with doctor information
            If AddDoctorToDatabase() Then
                ' Display success message
                MessageBox.Show("Doctor added successfully.")
                ' Clear the input fields after successful addition
                ClearForm()
            Else
                ' Display error message if adding doctor failed
                MessageBox.Show("Failed to add doctor. Please try again.")
            End If
        Else
            ' Display error message if there are validation errors
            MessageBox.Show("Please fix the errors before adding the doctor.")
        End If
    End Sub

    ' Function to validate the mobile number format
    Private Function ValidateMobileNumber() As Boolean
        ' Call the function to verify the validity of the mobile number
        VerifyMobileNumber(TextBox2.Text)

        ' Check if there is no error set by the VerifyMobileNumber function
        Return String.IsNullOrEmpty(ErrorProvider2.GetError(TextBox2))
    End Function

    ' Function to add doctor information to the database
    Private Function AddDoctorToDatabase() As Boolean
        Try
            ' Create a new SqlConnection using the connection string
            Using connection As New SqlConnection(connectionString)
                ' Open the database connection
                connection.Open()

                ' SQL query to insert doctor information into the database
                Dim query As String = "INSERT INTO Doctor_Staff (Name, Age, Password, Experience, Email, Mobile_Number, Gender, Morning_Visiting_Time_Start, Morning_Visiting_Time_End, Evening_Visiting_Time_Start, Evening_Visiting_Time_End, Speciality_Designation) " &
                                      "VALUES (@Name, @Age, @Password, @Exp, @Email, @MobileNumber, @Gender, @VisitingTimeStart1, @VisitingTimeEnd1, @VisitingTimeStart2, @VisitingTimeEnd2, @Specialization)"

                ' Create a new SqlCommand with the SQL query and SqlConnection
                Using command As New SqlCommand(query, connection)
                    ' Set parameter values for the SQL query
                    command.Parameters.AddWithValue("@Name", TextBox1.Text.Trim())
                    command.Parameters.AddWithValue("@Password", TextBox3.Text.Trim())
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim())
                    command.Parameters.AddWithValue("@MobileNumber", TextBox2.Text.Trim())
                    command.Parameters.AddWithValue("@Gender", ComboBox1.SelectedItem.ToString())
                    command.Parameters.AddWithValue("@Specialization", TextBox6.Text.Trim())
                    command.Parameters.AddWithValue("@Exp", TextBox7.Text.Trim())

                    ' Parse and set visiting time parameters

                    If TextBox5.Text = "" And TextBox4.Text = "" Then
                        MessageBox.Show("Please enter at least one of the morning or evening visiting time.")
                        Return False
                    Else
                        ' (Assuming time format is "hh:mm-hh:mm")
                        Dim startTime1 As TimeSpan = Nothing
                        Dim endTime1 As TimeSpan = Nothing
                        ' For Morning Visiting Time
                        Dim timeString1 As String = TextBox5.Text
                        If timeString1 = "" Then
                            'MessageBox.Show("Null time accepted")
                            command.Parameters.AddWithValue("@VisitingTimeStart1", startTime1)
                            command.Parameters.AddWithValue("@VisitingTimeEnd1", endTime1)
                        Else
                            Dim timeComponents1 As String() = timeString1.Split("-"c)
                            If timeComponents1.Length = 2 Then

                                If TimeSpan.TryParseExact(timeComponents1(0), "hh\:mm", Nothing, TimeSpanStyles.None, startTime1) Then
                                    If TimeSpan.TryParseExact(timeComponents1(1), "hh\:mm", Nothing, TimeSpanStyles.None, endTime1) Then
                                        command.Parameters.AddWithValue("@VisitingTimeStart1", startTime1)
                                        command.Parameters.AddWithValue("@VisitingTimeEnd1", endTime1)
                                    Else
                                        MessageBox.Show("Invalid end time format.")
                                    End If
                                Else
                                    MessageBox.Show("Invalid start time format.")
                                End If
                            Else
                                MessageBox.Show("Invalid time format. Please enter in hh:mm-hh:mm format.")
                            End If
                        End If

                        Dim startTime2 As TimeSpan = Nothing
                        Dim endTime2 As TimeSpan = Nothing
                        ' For Evening Visiting Time
                        Dim timeString2 As String = TextBox4.Text
                        If timeString2 = "" Then
                            'MessageBox.Show("Null time accepted")
                            command.Parameters.AddWithValue("@VisitingTimeStart2", startTime2)
                            command.Parameters.AddWithValue("@VisitingTimeEnd2", endTime2)
                        Else

                            Dim timeComponents2 As String() = timeString2.Split("-"c)
                            If timeComponents2.Length = 2 Then

                                If TimeSpan.TryParseExact(timeComponents2(0), "hh\:mm", Nothing, TimeSpanStyles.None, startTime2) Then
                                    If TimeSpan.TryParseExact(timeComponents2(1), "hh\:mm", Nothing, TimeSpanStyles.None, endTime2) Then
                                        command.Parameters.AddWithValue("@VisitingTimeStart2", startTime2)
                                        command.Parameters.AddWithValue("@VisitingTimeEnd2", endTime2)
                                    Else
                                        MessageBox.Show("Invalid end time format.")
                                    End If
                                Else
                                    MessageBox.Show("Invalid start time format.")
                                End If
                            Else
                                MessageBox.Show("Invalid time format. Please enter in hh:mm-hh:mm format.")
                            End If
                        End If
                        ' Calculate age from date of birth
                        Dim dateOfBirth As DateTime = DateTimePicker1.Value
                        Dim currentDate As DateTime = DateTime.Now
                        Dim age As Integer = currentDate.Year - dateOfBirth.Year
                        If currentDate.Month < dateOfBirth.Month OrElse (currentDate.Month = dateOfBirth.Month AndAlso currentDate.Day < dateOfBirth.Day) Then
                            age -= 1
                        End If
                        command.Parameters.AddWithValue("@Age", age)

                        ' Execute the SQL query
                        command.ExecuteNonQuery()
                    End If
                End Using

                ' Return True indicating success
                Return True

            End Using
        Catch ex As Exception
            ' Handle database-related errors and display error message
            MessageBox.Show("Error updating database: " & ex.Message)
            Return False
        End Try
    End Function

    ' Function to validate the mobile number format
    Private Sub VerifyMobileNumber(ByVal mobileNumber As String)
        ' Remove non-numeric characters from the input
        Dim numericMobileNumber = New String(mobileNumber.Where(Function(c) Char.IsDigit(c)).ToArray())

        ' Check if the numericMobileNumber is exactly 10 digits long
        If numericMobileNumber.Length = 10 Then
            ' Clear any previous error message
            ErrorProvider2.SetError(TextBox2, "")
        Else
            ' Set error message for invalid mobile number
            ErrorProvider2.SetError(TextBox2, "Invalid mobile number. Please enter a 10-digit mobile number.")
        End If
    End Sub

    ' Function to clear input fields after adding doctor
    Private Sub ClearForm()
        ' Clear input fields
        txtEmail.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox1.Text = ""
    End Sub

    Private Sub Admin_AddDoctor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
