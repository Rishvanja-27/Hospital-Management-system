Imports System.Data.SqlClient ' Import the namespace for SQL Server data access

Public Class Patients_Complaints ' Define a class named Patients_Complaints

    ' Event handler for the form's Load event
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the initial text in the RichTextBox
        RichTextBox1.Text = "Write your complaints ..."

        ' Add items to the ComboBox
        ComboBox1.Items.Add("Type of Complaint")
        ComboBox1.Items.Add("Staff Behavior")
        ComboBox1.Items.Add("Facility Conditions")
        ComboBox1.Items.Add("Wait Times")
        ComboBox1.Items.Add("Treatment Issues")
        ComboBox1.Items.Add("Billing and Financial Concerns")
        ComboBox1.Items.Add("Others")

        ' Set the default selected index for the ComboBox
        ComboBox1.SelectedIndex = 0
    End Sub

    ' Event handler for RichTextBox's Enter event
    Private Sub RichTextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.Enter
        ' Remove placeholder text when RichTextBox is entered
        If RichTextBox1.Text = "Write your complaints ..." Then
            RichTextBox1.Text = ""
        End If
    End Sub

    ' Event handler for RichTextBox's Leave event
    Private Sub RichTextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.Leave
        ' Add placeholder text when RichTextBox is exited and no text has been entered
        If RichTextBox1.Text = "" Then
            RichTextBox1.Text = "Write your complaints ..."
        End If
    End Sub

    ' Connection string to connect to the SQL Server database
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

    ' Method to add a complaint to the database
    Private Sub AddComplaint(ByVal Complaint_type As String, ByVal Complaint As String)
        Dim Complaint_From = "Patient" ' Define the source of the complaint

        ' SQL query to insert the complaint into the database
        Dim query As String = "INSERT INTO Complaints (Complaint, Complaint_Type, Complaint_From) VALUES (@Complaint, @Complaint_type, @Complaint_From)"

        ' Using block to automatically close the connection after execution
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to the SQL command
                command.Parameters.AddWithValue("@Complaint", Complaint)
                command.Parameters.AddWithValue("@Complaint_type", Complaint_type)
                command.Parameters.AddWithValue("@Complaint_From", Complaint_From)
                Try
                    connection.Open() ' Open the database connection
                    command.ExecuteNonQuery() ' Execute the SQL command
                    MessageBox.Show("Complaint Registered Successfully") ' Show a success message
                Catch ex As Exception
                    MessageBox.Show("Error inserting complaint: " & ex.Message) ' Show an error message if an exception occurs
                End Try
            End Using
        End Using
    End Sub

    ' Event handler for the Button's Click event
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Check if a valid complaint type is selected
        If ComboBox1.SelectedIndex = -1 OrElse ComboBox1.SelectedIndex = 0 Then
            MessageBox.Show("Please select a valid complaint type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check if a valid complaint is entered in the RichTextBox
        Dim Complaint As String = RichTextBox1.Text.Trim()
        If String.IsNullOrEmpty(Complaint) OrElse Complaint = "Write your complaints ..." Then
            MessageBox.Show("Please write your complaint.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get the selected complaint type from the ComboBox
        Dim Complaint_type As String = ComboBox1.SelectedItem.ToString()

        ' Call the method to add the complaint to the database
        AddComplaint(Complaint_type, Complaint)

        ' Reset the RichTextBox and ComboBox
        RichTextBox1.Text = "Write your complaints ..."
        ComboBox1.SelectedIndex = 0
    End Sub
End Class
