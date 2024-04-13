Imports System.Data.SqlClient

Public Class Admin_EditDoctor

    ' Connection string for the database
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

    ' Variable to store the doctor's ID
    Private doctorID As Integer

    ' Event to notify when doctor details are successfully updated
    Public Event DoctorDetailsUpdatedSuccessfully As EventHandler

    ' Constructor to initialize the form with doctor details
    Public Sub New(ByVal doctorName As String, ByVal MorningvisTime As String, ByVal EveningvisTime As String, ByVal doctorID As String)
        InitializeComponent()
        ' Assign values to form controls
        Me.doctorID = doctorID
        lblDoctorName.Text = doctorName
        TextBox1.Text = MorningvisTime
        TextBox2.Text = EveningvisTime
    End Sub

    ' Function to validate the format of time string
    Private Function IsValidTimeFormat(ByVal time As String) As Boolean
        ' Split the time string into parts using "-" as the delimiter
        Dim parts As String() = time.Split("-"c)

        ' Check if there are exactly two parts
        If parts.Length <> 2 Then
            Return False
        End If

        ' Validate the format of each part (HH:MM)
        For Each part As String In parts
            Dim timeParts As String() = part.Split(":"c)

            ' Check if there are exactly two parts (hours and minutes)
            If timeParts.Length <> 2 Then
                Return False
            End If

            ' Try parsing hours and minutes
            Dim hour As Integer
            Dim minute As Integer
            If Not Integer.TryParse(timeParts(0), hour) OrElse Not Integer.TryParse(timeParts(1), minute) Then
                Return False
            End If

            ' Check if hour is between 00 and 23 and minute is between 00 and 59
            If hour < 0 Or hour > 23 Or minute < 0 Or minute > 59 Then
                Return False
            End If
        Next

        ' If all checks pass, return true
        Return True
    End Function

    ' Event handler for the Update button click
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        ' Validate the format of TextBox1 and TextBox2
        If IsValidTimeFormat(TextBox1.Text) AndAlso IsValidTimeFormat(TextBox2.Text) Then
            ' Both time formats are valid, proceed with updating the doctor's details
            Dim query As String = "UPDATE Doctor_Staff SET Name = @DoctorName, Morning_Visiting_Time_Start = @MorningStartTime, Morning_Visiting_Time_End = @MorningEndTime, Evening_Visiting_Time_Start = @EveningStartTime, Evening_Visiting_Time_End = @EveningEndTime WHERE Doctor_ID_Staff_ID = @DoctorID"
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    ' Add parameters for the SQL query
                    command.Parameters.AddWithValue("@DoctorName", lblDoctorName.Text)
                    command.Parameters.AddWithValue("@MorningStartTime", TextBox1.Text.Split("-"c)(0).Trim())
                    command.Parameters.AddWithValue("@MorningEndTime", TextBox1.Text.Split("-"c)(1).Trim())
                    command.Parameters.AddWithValue("@EveningStartTime", TextBox2.Text.Split("-"c)(0).Trim())
                    command.Parameters.AddWithValue("@EveningEndTime", TextBox2.Text.Split("-"c)(1).Trim())
                    command.Parameters.AddWithValue("@DoctorID", doctorID)

                    ' Open connection and execute the query
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    ' Check if the update was successful
                    If rowsAffected > 0 Then
                        ' Notify user and raise event
                        MessageBox.Show("Doctor details updated successfully.")
                        RaiseEvent DoctorDetailsUpdatedSuccessfully(Me, EventArgs.Empty)
                    Else
                        MessageBox.Show("Failed to update doctor details.")
                    End If
                End Using
            End Using
        Else
            MessageBox.Show("Invalid time format. Please enter time in HH:MM-HH:MM format.")
        End If
    End Sub

    Private Sub Admin_EditDoctor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
