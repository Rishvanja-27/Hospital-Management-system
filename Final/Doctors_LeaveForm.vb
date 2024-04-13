Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class Doctors_LeaveForm
    Private initialText As String = "Comments..."
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
    Private doctorID As Integer
    Private par As Patients_MainForm

    Public Sub New(ByVal parent As Patients_MainForm, ByVal doctorID As Integer)
        InitializeComponent()
        par = parent
        Me.doctorID = doctorID ' Store the doctor's ID
    End Sub

    Private Sub LeaveForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        CommentsTextBox.Text = initialText
        CommentsTextBox.ForeColor = Color.Gray ' Set the text color to gray
        ' Customize the appearance of DateTimePicker controls
        CustomizeDateTimePicker(StartDateTimePicker)
        CustomizeDateTimePicker(EndDateTimePicker)
        PopulateReasonsComboBox()
    End Sub

    Private Sub CustomizeDateTimePicker(ByVal dateTimePicker As DateTimePicker)
        ' Set the custom format to display only the date
        dateTimePicker.Format = DateTimePickerFormat.Custom
        dateTimePicker.CustomFormat = "MM/dd/yyyy"

        ' Adjust the font and color
        dateTimePicker.Font = New Font("Arial", 12, FontStyle.Bold)
        dateTimePicker.ForeColor = Color.DarkBlue

        ' Set background and border color
        dateTimePicker.BackColor = Color.LightCyan
    End Sub

    Private Sub PopulateReasonsComboBox()
        ' Define the reasons for leave
        Dim reasons As String() = {"Vacation", "Sick Leave", "Family Emergency", "Personal Reasons", "Other"}

        ' Set the ComboBox data source
        ReasonComboBox.DataSource = reasons
    End Sub

    Private Sub CommentsTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles CommentsTextBox.Enter
        ' Clear the text when the user clicks inside
        If CommentsTextBox.Text = initialText Then
            CommentsTextBox.Text = ""
            CommentsTextBox.ForeColor = Color.Black ' Set the text color to black
        End If
    End Sub

    Private Sub CommentsTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles CommentsTextBox.Leave
        ' Restore the initial text when the user leaves the TextBox without entering anything
        If String.IsNullOrWhiteSpace(CommentsTextBox.Text) Then
            CommentsTextBox.Text = initialText
            CommentsTextBox.ForeColor = Color.Gray ' Set the text color to gray
        End If
    End Sub

    Private Sub SubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitButton.Click
        ' Get the input values
        Dim leavePeriodStart As Date = StartDateTimePicker.Value
        Dim leavePeriodEnd As Date = EndDateTimePicker.Value
        Dim reason As String = ReasonComboBox.SelectedItem.ToString()
        Dim comments As String = If(CommentsTextBox.Text = initialText, "", CommentsTextBox.Text)

        ' Check if leave period start date is not in the past
        If leavePeriodStart < Date.Today Then
            MessageBox.Show("Leave period start date cannot be in the past.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check if leave period end date is at least one day ahead of the current date
        If leavePeriodEnd <= Date.Today Then
            MessageBox.Show("Leave period end date must be at least one day ahead of the current date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check if leave period end date is after leave period start date
        If leavePeriodEnd <= leavePeriodStart Then
            MessageBox.Show("Leave period end date must be after the leave period start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Insert into the database
        Dim query As String = "INSERT INTO Leave_Application (Doctor_ID, Leave_Period_Start, Leave_Period_End, Reason) " &
                               "VALUES (@DoctorID, @LeavePeriodStart, @LeavePeriodEnd, @Reason)"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@DoctorID", doctorID)
                command.Parameters.AddWithValue("@LeavePeriodStart", leavePeriodStart)
                command.Parameters.AddWithValue("@LeavePeriodEnd", leavePeriodEnd)
                command.Parameters.AddWithValue("@Reason", reason)
                command.Parameters.AddWithValue("@Comments", comments)

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Leave application submitted successfully!")
                Catch ex As Exception
                    MessageBox.Show("Error submitting leave application: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
End Class