Imports System.Data.SqlClient

Public Class Doctors_Appointment

    Private par As Patients_MainForm
    Private doctorID As Integer

    ' Constructor modified to accept parent form and doctor ID as parameters
    Public Sub New(ByVal parent As Patients_MainForm, ByVal doctorID As Integer)
        InitializeComponent()
        par = parent
        Me.doctorID = doctorID ' Store the doctor's ID
    End Sub
    Private Sub Doctors_Appointment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Define connection string
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Set properties for the ListView
        listView1.Width = 1250 ' Set width to 1000
        listView1.View = View.Details ' Set view to Details mode
        listView1.FullRowSelect = True ' Select entire row when clicked
        listView1.GridLines = False ' Display grid lines
        listView1.BorderStyle = BorderStyle.None
        listView1.Font = New Font("Arial", 14)
        listView1.Columns.Add("Appointment Number", 120)
        listView1.Columns.Add("Patient Name", 280)
        listView1.Columns.Add("Age", 80)
        listView1.Columns.Add("Gender", 120)
        listView1.Columns.Add("Appointment Time", 240)
        listView1.Columns.Add("Status", 100)

        ' Create a new connection
        Using connection As New SqlConnection(connectionString)
            Try
                ' Open the connection
                connection.Open()

                ' Define SQL query to fetch appointments for a specific doctor
                Dim query As String = "SELECT A.[Appointment_ID], P.Name AS PatientName, P.Age, P.Gender, A.[Appointment_Time], A.[Appointment_Type] FROM Appointments A INNER JOIN Patient P ON A.Patient_ID = P.Patient_ID WHERE A.[Doctor_ID] = @DoctorID"

                ' Create a SqlCommand object
                Using command As New SqlCommand(query, connection)
                    ' Set the parameter value for the doctor ID
                    command.Parameters.AddWithValue("@DoctorID", doctorID)

                    ' Execute the command and create a SqlDataReader
                    Using reader As SqlDataReader = command.ExecuteReader()
                        ' Check if there are rows returned
                        If reader.HasRows Then
                            ' Loop through the rows
                            While reader.Read()
                                ' Retrieve appointment details from the reader
                                Dim appointmentNumber As String = reader("Appointment_Number").ToString()
                                Dim patientName As String = reader("PatientName").ToString()
                                Dim age As String = reader("Age").ToString()
                                Dim gender As String = reader("Gender").ToString()
                                Dim appointmentTime As DateTime = Convert.ToDateTime(reader("Appointment_Time"))
                                Dim appointmentType As String = reader("Appointment_Type").ToString()

                                ' Determine the status based on the appointment type
                                Dim status As String = If(appointmentType = "1", "Done", "Not Done")


                                ' Add appointment details to the ListView
                                Dim row As New ListViewItem(New String() {appointmentNumber, patientName, age, gender, appointmentTime.ToString("dd-MM-yyyy HH:mm"), status})
                                listView1.Items.Add(row)


                                ' Set background color for status column initially
                                If status = "Done" Then
                                    row.SubItems(5).BackColor = Color.LightGreen
                                End If
                            End While
                        Else
                            ' No appointments found for the doctor
                            MessageBox.Show("No appointments found for the doctor.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Set the location of the ListView
        listView1.Location = New Point(12, 51) ' Set location to (12, 51)

        ' Add the ListView to the form
        Me.Controls.Add(listView1)

        ' Set background color for specific columns
        For Each item As ListViewItem In listView1.Items
            item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Appointment Number
            item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Patient Name
            item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Age
            item.SubItems(3).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Gender
            item.SubItems(4).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Appointment Time
            item.SubItems(5).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Status
        Next
        For Each item As ListViewItem In listView1.Items
            ' Set background color for each item
            SetBackgroundColor(item)
        Next
    End Sub



    Private Sub listView1_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles listView1.MouseClick
        ' Get the item at the clicked location
        Dim item As ListViewItem = listView1.GetItemAt(e.X, e.Y)

        ' If an item is clicked and it's the last column (Status), toggle its value
        If item IsNot Nothing AndAlso item.SubItems.IndexOf(item.GetSubItemAt(e.X, e.Y)) = 5 Then
            ToggleStatus(item)
        End If

        ' Change color if clicked only for the status column
        For Each selectedItem As ListViewItem In listView1.SelectedItems
            For Each subItem As ListViewItem.ListViewSubItem In selectedItem.SubItems
                ' Check if the clicked subItem is the status column
                If selectedItem.SubItems.IndexOf(subItem) = 5 Then
                    If selectedItem Is item Then
                        subItem.BackColor = Color.LightBlue ' Set color for selected status column
                    Else
                        subItem.BackColor = Color.White ' Set default color for other status columns
                    End If
                End If
            Next
        Next
    End Sub




    ' Function to update the status in the database
    Private Sub UpdateStatusInDatabase(ByVal appointmentNumber As String, ByVal newStatus As String)
        ' Define connection string
        Dim connectionString As String = "Data Source=RASHAADPC\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Define SQL query to update the status
        Dim query As String = "UPDATE Appointments SET Appointment_Type = @NewStatus WHERE Appointment_Number = @AppointmentNumber"

        ' Create a new connection
        Using connection As New SqlConnection(connectionString)
            Try
                ' Open the connection
                connection.Open()

                ' Create a SqlCommand object
                Using command As New SqlCommand(query, connection)
                    ' Set the parameters
                    command.Parameters.AddWithValue("@NewStatus", If(newStatus = "Done", 1, 0))
                    command.Parameters.AddWithValue("@AppointmentNumber", appointmentNumber)

                    ' Execute the command
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub ToggleStatus(ByVal item As ListViewItem)
        ' Get the current status
        Dim currentStatus As String = item.SubItems(5).Text

        ' Check if the status is "Not Done" before toggling
        If currentStatus = "Not Done" Then
            ' Toggle the status
            Dim newStatus As String = "Done"

            ' Update the status in the ListView
            item.SubItems(5).Text = newStatus

            ' Update the background color of the entire ListViewItem
            item.BackColor = Color.LightGreen ' Set color for "Done" status

            ' Update the status in the database (you need to implement this part)
            UpdateStatusInDatabase(item.Text, newStatus)
        End If
    End Sub

    Private Sub SetBackgroundColor(ByVal item As ListViewItem)
        ' Get the status of the appointment
        Dim status As String = item.SubItems(5).Text

        ' Set background color based on status
        If status = "Done" Then
            item.SubItems(5).BackColor = Color.LightGreen ' Set color for "Done" status
        Else
            item.SubItems(5).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Set default color for "Not Done" status
        End If
    End Sub

End Class