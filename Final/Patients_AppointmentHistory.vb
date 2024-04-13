Imports System.Data.SqlClient ' Import the namespace for SQL Server data access

Public Class Patients_AppointmentHistory ' Define a class for displaying patient appointment history
    Public patientId As Integer
    Public Sub New(ByVal pID As Integer)
        InitializeComponent()
        patientId = pID
    End Sub
    ' Create a ListView control for displaying appointment history
    Dim listView1 As New ListView()

    ' Event handler for the form load event
    Private Sub Patient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Define connection string
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        ' Set properties for the ListView
        listView1.Name = "listView1" ' Assign a name to the ListView control
        listView1.Width = 980 ' Set width to 1000
        listView1.MaximumSize = New Size(1000, 550) ' Set maximum width to 1000 and maximum height to 500
        listView1.MinimumSize = New Size(1000, 70) ' Set maximum width to 1000 and maximum height to 500
        listView1.View = View.Details ' Set view to Details mode
        listView1.FullRowSelect = True ' Select entire row when clicked
        listView1.GridLines = False ' Display grid lines
        listView1.BorderStyle = BorderStyle.None ' Set border style
        listView1.Font = New Font("Arial", 14) ' Set font
        listView1.Columns.Add("App.", 75) ' Add columns to the ListView
        listView1.Columns.Add("Doctor Name", 340)
        listView1.Columns.Add("Fees(in rupees)", 223)
        listView1.Columns.Add("Date", 200)
        listView1.Columns.Add("Action", 158)
        listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable ' Make column headers non-clickable
        listView1.HideSelection = True ' Remove highlighting effect after clicking

        Using connection As New SqlConnection(connectionString)
            Try
                ' Open the connection
                connection.Open()

                ' Define SQL query to fetch appointments for a specific patient
                Dim query As String = "SELECT A.[Appointment_ID], D.Name AS DoctorName, A.[Appointment_Time], A.[Appointment_Type], B.Total_Price FROM Appointments A INNER JOIN Doctor_Staff D ON A.Doctor_ID = D.Doctor_ID_Staff_ID LEFT JOIN Bill B ON A.Bill_ID = B.Bill_ID WHERE A.[Patient_ID] = @PatientID"

                ' Create a SqlCommand object
                Using command As New SqlCommand(query, connection)
                    ' Set the parameter value for the patient ID
                    command.Parameters.AddWithValue("@PatientID", patientId)

                    ' Execute the command and create a SqlDataReader
                    Using reader As SqlDataReader = command.ExecuteReader()
                        ' Check if there are rows returned
                        If reader.HasRows Then
                            ' Loop through the rows
                            While reader.Read()
                                ' Retrieve appointment details from the reader
                                Dim appointmentID As String = reader("Appointment_ID").ToString()
                                Dim doctorName As String = reader("DoctorName").ToString()
                                Dim appointmentTime As DateTime = Convert.ToDateTime(reader("Appointment_Time"))
                                Dim appointmentType As String = reader("Appointment_Type").ToString()
                                Dim fees As Decimal = If(IsDBNull(reader("Total_Price")), 0, Convert.ToDecimal(reader("Total_Price")))

                                If appointmentType = "2" Then
                                    appointmentType = "Receipt"
                                ElseIf appointmentType = "1" Then
                                    appointmentType = "No Bill"
                                Else
                                    appointmentType = "Exp. Time"
                                End If

                                ' Add appointment details to the ListView
                                Dim row As New ListViewItem(New String() {appointmentID, doctorName, fees, appointmentTime.ToString("dd-MM-yyyy"), appointmentType})
                                listView1.Items.Add(row)
                            End While
                        Else
                            ' No appointments found for the patient
                            MessageBox.Show("No appointments found for the patient.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Set the location of the ListView
        listView1.Location = New Point(30, 50) ' Set location to (30, 200)

        ' Handle the SelectedIndexChanged event to perform custom actions when any item is clicked
        AddHandler listView1.Click, AddressOf listView1_SelectedIndexChanged

        ' Set the ListView's height to fit its content
        listView1.Height = (listView1.Items.Count * 40) ' Add some extra space for aesthetics

        ' Add the ListView to the form
        Me.Controls.Add(listView1)

        ' Bring the ListView to the front
        listView1.BringToFront()

        ' Set background color for specific columns (1st, 2nd, 3rd, 4th, 5th)
        For Each item As ListViewItem In listView1.Items
            item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' App.
            item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Doctor Name
            item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Fees
            item.SubItems(3).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Date
            item.SubItems(4).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Action
        Next
    End Sub

    Private Sub listView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Check if an item is selected
        If listView1.SelectedItems.Count = 1 Then
            ' Get the selected item
            Dim selectedItem As ListViewItem = listView1.SelectedItems(0)
            ' Handle the LostFocus event to remove selection when ListView loses focus
            listView1.SelectedItems.Clear()
            selectedItem.BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Extract the bill information from the selected item
            Dim action As String = selectedItem.SubItems(4).Text
            If action = "Receipt" Then
                ' Extract the bill information from the selected item
                Dim doctorName As String = selectedItem.SubItems(1).Text
                Dim fees As Decimal = Decimal.Parse(selectedItem.SubItems(2).Text)
                Dim appointmentID As Decimal = Decimal.Parse(selectedItem.SubItems(0).Text)
                Dim billingtime As String = selectedItem.SubItems(3).Text

                ' Show the custom print form
                Dim printForm As New Patients_Bill(appointmentID, billingtime)
                printForm.ShowDialog()
            ElseIf action = "No Bill" Then
                MessageBox.Show("Go to medicine shop with your prescription. Ignore if no medicine prescribed", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' MessageBox.Show("Printing the bill...", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim appointmentID As Decimal = Decimal.Parse(selectedItem.SubItems(0).Text)
                Dim connectionString As String = "Data Source=RASHAADPC\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
                Dim appointmentTime As DateTime = DateTime.Today
                Try
                    ' Fetch the doctor's ID and appointment time based on the selected appointment
                    Dim doctorID As Integer = 0
                    Dim doctorName As String = ""
                    Dim morningVisitingStartTime As TimeSpan? = Nothing ' Nullable TimeSpan
                    Dim morningVisitingEndTime As TimeSpan? = Nothing ' Nullable TimeSpan
                    Dim eveningVisitingStartTime As TimeSpan? = Nothing ' Nullable TimeSpan
                    Dim eveningVisitingEndTime As TimeSpan? = Nothing ' Nullable TimeSpan
                    Using connection As New SqlConnection(connectionString)
                        connection.Open()
                        Dim queryDoctorDetails As String = "SELECT D.Doctor_ID_Staff_ID, D.Name AS DoctorName, D.Morning_Visiting_Time_Start, D.Morning_Visiting_Time_End, D.Evening_Visiting_Time_Start, D.Evening_Visiting_Time_End, A.Appointment_Time FROM Appointments A INNER JOIN Doctor_Staff D ON A.Doctor_ID = D.Doctor_ID_Staff_ID WHERE A.Appointment_ID = @AppointmentID"
                        Using commandDoctorDetails As New SqlCommand(queryDoctorDetails, connection)
                            commandDoctorDetails.Parameters.AddWithValue("@AppointmentID", appointmentID)
                            Using readerDoctorDetails As SqlDataReader = commandDoctorDetails.ExecuteReader()
                                If readerDoctorDetails.Read() Then
                                    doctorID = Convert.ToInt32(readerDoctorDetails("Doctor_ID_Staff_ID"))
                                    doctorName = readerDoctorDetails("DoctorName").ToString()
                                    ' Check for null values and assign TimeSpan values accordingly
                                    If Not IsDBNull(readerDoctorDetails("Morning_Visiting_Time_Start")) Then
                                        morningVisitingStartTime = TimeSpan.Parse(readerDoctorDetails("Morning_Visiting_Time_Start").ToString())
                                    End If
                                    If Not IsDBNull(readerDoctorDetails("Morning_Visiting_Time_End")) Then
                                        morningVisitingEndTime = TimeSpan.Parse(readerDoctorDetails("Morning_Visiting_Time_End").ToString())
                                    End If
                                    If Not IsDBNull(readerDoctorDetails("Evening_Visiting_Time_Start")) Then
                                        eveningVisitingStartTime = TimeSpan.Parse(readerDoctorDetails("Evening_Visiting_Time_Start").ToString())
                                    End If
                                    If Not IsDBNull(readerDoctorDetails("Evening_Visiting_Time_End")) Then
                                        eveningVisitingEndTime = TimeSpan.Parse(readerDoctorDetails("Evening_Visiting_Time_End").ToString())
                                    End If
                                    appointmentTime = Convert.ToDateTime(readerDoctorDetails("Appointment_Time"))
                                End If
                            End Using
                        End Using
                    End Using

                    ' Calculate the expected time of the appointment
                    Dim expTime As TimeSpan
                    Using connection As New SqlConnection(connectionString)
                        connection.Open()

                        ' Fetch the doctor's average operating time
                        Dim avgOperatingTime As Integer = 0
                        Dim queryAvgOperatingTime As String = "SELECT Average_Operating_Time FROM Doctor_Staff WHERE Doctor_ID_Staff_ID = @DoctorID"
                        Using commandAvgOperatingTime As New SqlCommand(queryAvgOperatingTime, connection)
                            commandAvgOperatingTime.Parameters.AddWithValue("@DoctorID", doctorID)
                            avgOperatingTime = Convert.ToInt32(commandAvgOperatingTime.ExecuteScalar())
                        End Using

                        ' Fetch the rank of the appointment
                        Dim rank As Integer = 0
                        Dim queryRank As String = "SELECT COUNT(*) FROM Appointments A1 WHERE A1.Doctor_ID = @DoctorID AND CONVERT(DATE, A1.Appointment_Time) = CONVERT(DATE, (SELECT Appointment_Time FROM Appointments WHERE Appointment_ID = @AppointmentID)) AND A1.Appointment_Time <= (SELECT Appointment_Time FROM Appointments WHERE Appointment_ID = @AppointmentID)"
                        Using commandRank As New SqlCommand(queryRank, connection)
                            commandRank.Parameters.AddWithValue("@DoctorID", doctorID)
                            commandRank.Parameters.AddWithValue("@AppointmentID", appointmentID)
                            rank = Convert.ToInt32(commandRank.ExecuteScalar())
                        End Using

                        ' Calculate the expected time based on rank and average operating time
                        expTime = TimeSpan.FromMinutes((rank - 1) * avgOperatingTime)
                    End Using

                    ' Calculate the arrival time of the patient
                    Dim arrivalTime As DateTime = appointmentTime
                    Dim arriTime As String = appointmentTime.ToString()
                    Dim appointmentDate As DateTime = appointmentTime.Date

                    ' Determine visiting time slot based on expected appointment time
                    If expTime.TotalMinutes <= If(morningVisitingEndTime.HasValue, (morningVisitingEndTime.Value - morningVisitingStartTime.Value).TotalMinutes, 0) Then
                        ' Patient should arrive during morning visiting hours
                        If morningVisitingStartTime.HasValue Then
                            arrivalTime = appointmentDate.Add(morningVisitingStartTime.Value).Add(expTime)
                        End If
                    Else
                        ' Patient should arrive during evening visiting hours
                        If eveningVisitingStartTime.HasValue Then
                            arrivalTime = appointmentDate.Add(eveningVisitingStartTime.Value).Add(expTime.Subtract(TimeSpan.FromMinutes(If(morningVisitingEndTime.HasValue, (morningVisitingEndTime.Value - morningVisitingStartTime.Value).TotalMinutes, 0)))) ' Subtract an hour to ensure it's within the visiting hours
                        End If
                    End If

                    ' Display message with expected appointment time and arrival time
                    MessageBox.Show("Expected time of your appointment: " & arrivalTime.ToString("HH\:mm") & " (hh:mm) on " & appointmentTime.ToString("dd-MM-yyyy") & ". " & Environment.NewLine & _
                                    "Doctor's Morning visiting time: " & If(morningVisitingStartTime.HasValue, morningVisitingStartTime.Value.ToString("hh\:mm"), "N/A") & " - " & If(morningVisitingEndTime.HasValue, morningVisitingEndTime.Value.ToString("hh\:mm"), "N/A") & ", Evening visiting time: " & If(eveningVisitingStartTime.HasValue, eveningVisitingStartTime.Value.ToString("hh\:mm"), "N/A") & " - " & If(eveningVisitingEndTime.HasValue, eveningVisitingEndTime.Value.ToString("hh\:mm"), "N/A") & ". ", "Expected Time of Arrival", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub
End Class
