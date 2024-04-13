Imports System.Data.SqlClient

Public Class Doctors_Complaints

    ' Store the initial text
    Private initialText As String = "Write your complaints ..."

    Private Sub YourForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Set up the ComboBox with initial text and categories
        InitializeComplaintsComboBox()
        InitializeComplaintsTextBox()
    End Sub

    Private Sub InitializeComplaintsComboBox()
        ' Set the initial text
        ComplaintsComboBox.Text = initialText

        ComplaintsComboBox.Items.Add("Type of Complaint")
        ComplaintsComboBox.Items.Add("Collegial Communication")
        ComplaintsComboBox.Items.Add("Resource Allocation")
        ComplaintsComboBox.Items.Add("Policy or Protocol Concerns")
        ComplaintsComboBox.Items.Add("Administrative Issues")
        ComplaintsComboBox.Items.Add("Quality of Care")
        ComplaintsComboBox.Items.Add("Work-Life Balance")
        ComplaintsComboBox.Items.Add("Others")

        ComplaintsComboBox.SelectedIndex = 0

    End Sub


    Private Sub InitializeComplaintsTextBox()
        ' Set the initial text
        ComplaintsTextBox.Text = initialText
    End Sub

    Private Sub ComplaintsTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles ComplaintsTextBox.Enter
        ' Clear the text when the user clicks inside
        If ComplaintsTextBox.Text = initialText Then
            ComplaintsTextBox.Text = ""
            ComplaintsTextBox.ForeColor = Color.Black ' Set the text color to black
        End If
    End Sub

    Private Sub ComplaintsTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles ComplaintsTextBox.Leave
        ' Restore the initial text when the user leaves without entering anything
        If String.IsNullOrWhiteSpace(ComplaintsTextBox.Text) Then
            InitializeComplaintsTextBox()
        End If
    End Sub

    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

    Private Sub AddComplaint(ByVal Complaint_type As String, ByVal Complaint As String)
        Dim Complaint_From = "Medical Staff"

        Dim query As String = "INSERT INTO Complaints (Complaint, Complaint_Type, Complaint_From) VALUES (@Complaint, @Complaint_type, @Complaint_From)"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Complaint", Complaint)
                command.Parameters.AddWithValue("@Complaint_type", Complaint_type)
                command.Parameters.AddWithValue("@Complaint_From", Complaint_From)
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Complaint Registered Succefully")
                Catch ex As Exception
                    MessageBox.Show("Error inserting complaint: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub SubmitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubmitButton.Click

        If ComplaintsComboBox.SelectedIndex = -1 OrElse ComplaintsComboBox.SelectedIndex = 0 Then
            MessageBox.Show("Please select a valid complaint type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check if the RichTextBox contains a valid complaint
        Dim Complaint As String = ComplaintsTextBox.Text.Trim()
        If String.IsNullOrEmpty(Complaint) OrElse Complaint = "Write your complaints ..." Then
            MessageBox.Show("Please write your complaint.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim Complaint_type As String = ComplaintsComboBox.SelectedItem.ToString()
        AddComplaint(Complaint_type, Complaint)
        ComplaintsTextBox.Text = "Write your complaints ..."
        ComplaintsComboBox.SelectedIndex = 0
    End Sub

    Private Sub ComplaintsTextBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles ComplaintsTextBox.TextChanged

    End Sub
End Class
