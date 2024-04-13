Imports System.Data.SqlClient ' Import the namespace for SQL Server data access

Public Class Patients_BookAppointment ' Define a class named Patients_BookAppointment
    Public patientId As Integer
    Public Sub New(ByVal pID As Integer)
        InitializeComponent()
        patientId = pID
    End Sub

    ' Define variables for database connection and selected ListBox
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
    Dim selectedListBox As ListBox = Nothing
    Dim DoctorID As Integer = -1
    Dim Morning_Visiting_Time_Start As DateTime
    Dim Morning_Visiting_Time_End As DateTime
    Dim Evening_Visiting_Time_Start As DateTime
    Dim Evening_Visiting_Time_End As DateTime
    Dim Morning_Time As Integer
    Dim Evening_Time As Integer
    ' Function to handle null values and convert to string
    Function GetStringFromNullable(ByVal readerValue As Object) As String
        If readerValue Is DBNull.Value Then
            Return "NA"
        Else
            Return readerValue.ToString()
        End If
    End Function
    ' Event handler for the form's Load event
    Private Sub Patient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Enable horizontal scrollbar for the FlowLayoutPanel
        MessageBox.Show(patientId)
        FlowLayoutPanel1.HorizontalScroll.Enabled = True
        FlowLayoutPanel1.HorizontalScroll.Visible = True
        FlowLayoutPanel1.HorizontalScroll.Maximum = 0

        ' Set height and items for the ComboBox
        ComboBox1.Height = 50
        ComboBox1.Items.AddRange(New String() {"Select Specialization", "General Practitioner", "Cardiology", "Dermatology", "Endocrinology", "Gastroenterology", "Pediatrics"})
        ComboBox1.SelectedIndex = 0
    End Sub

    ' Event handler for the ComboBox's SelectedIndexChanged event
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Load doctors when a specialization is selected
        If ComboBox1.SelectedIndex > 0 Then
            Dim specialization As String = ComboBox1.SelectedItem.ToString()
            LoadDoctors(specialization)
        End If
    End Sub

    ' Method to load doctors based on specialization
    Private Sub LoadDoctors(ByVal specialization As String)
        ' Clear existing controls from the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()

        ' Query database for doctors based on specialization
        Dim query As String = "SELECT Name,Experience,Age, Gender, Speciality_Designation, Morning_Visiting_Time_Start, Morning_Visiting_Time_End, Evening_Visiting_Time_Start, Evening_Visiting_Time_End FROM Doctor_Staff WHERE Speciality_Designation = @Specialization"

        ' Establish connection and execute the query
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Specialization", specialization)

                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' Loop through the result set and create ListBox controls for each doctor
                    While reader.Read()
                        Dim listBox As New ListBox() ' Create a new ListBox for each doctor
                        listBox.Width = 210
                        listBox.Height = 170
                        listBox.BorderStyle = BorderStyle.None
                        listBox.Font = New Font("Arial", 13)
                        listBox.Items.Add("")
                        listBox.Items.Add(" Dr. " + reader("Name").ToString())
                        listBox.Items.Add(" " + reader("Age").ToString() + " years, " + reader("Gender").ToString())
                        listBox.Items.Add(" Experience: " + reader("Experience").ToString() + " years")
                        listBox.Items.Add(" Visiting Time:- ")


                        ' Add morning visiting time to the listBox
                        listBox.Items.Add(GetStringFromNullable(reader("Morning_Visiting_Time_Start")) & " to " & GetStringFromNullable(reader("Morning_Visiting_Time_End")))

                        ' Add evening visiting time to the listBox
                        listBox.Items.Add(GetStringFromNullable(reader("Evening_Visiting_Time_Start")) & " to " & GetStringFromNullable(reader("Evening_Visiting_Time_End")))


                        ' Add margin between ListBoxes
                        listBox.Margin = New Padding(10)

                        ' Make ListBox content unselectable
                        listBox.SelectionMode = SelectionMode.None

                        ' Add the ListBox to the FlowLayoutPanel
                        FlowLayoutPanel1.Controls.Add(listBox)

                        ' Add event handler for ListBox click
                        AddHandler listBox.Click, AddressOf DoctorListBox_Click
                    End While
                Catch ex As Exception
                    MessageBox.Show("Error loading doctors: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' Function to calculate age based on date of birth and current date
    Private Function CalculateAge(ByVal dateOfBirth As Date, ByVal currentDate As Date) As Integer
        Dim age As Integer = currentDate.Year - dateOfBirth.Year
        If currentDate < dateOfBirth.AddYears(age) Then
            age -= 1
        End If
        Return age
    End Function

    ' Event handler for ListBox click event
    Private Sub DoctorListBox_Click(ByVal sender As Object, ByVal e As EventArgs)
        If TypeOf sender Is ListBox Then
            If selectedListBox IsNot Nothing Then
                selectedListBox.BackColor = Color.White ' Clear border from previously selected ListBox
            End If
            selectedListBox = DirectCast(sender, ListBox)
            selectedListBox.BackColor = Color.Beige ' Set border to clicked ListBox
        End If
    End Sub

    ' Event handler for Button click event to book appointment
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        If ComboBox1.SelectedIndex = -1 OrElse ComboBox1.SelectedIndex = 0 Then
            MessageBox.Show("Please select a valid specialization type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        If selectedListBox Is Nothing Then
            MessageBox.Show("Please select one of the doctors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If DateTimePicker1.Value.Date <= DateTime.Today.Date Then
            MessageBox.Show("Please select a future date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If



        Dim doctorName As String = selectedListBox.Items(1).ToString().Replace(" Dr. ", "")
        Dim doctorSpecialization As String = ComboBox1.SelectedItem.ToString()

        GetDoctorInfo(doctorName, doctorSpecialization)

        ' Calculate morning and evening visiting time
        Morning_Time = Convert.ToInt32((Morning_Visiting_Time_End - Morning_Visiting_Time_Start).TotalMinutes)
        Evening_Time = Convert.ToInt32((Evening_Visiting_Time_End - Evening_Visiting_Time_Start).TotalMinutes)

        ' Check if doctor is found and appointments are available
        If DoctorID <> -1 Then
            Dim appointmentCount As Integer = GetAppointmentCount(DoctorID, DateTimePicker1.Value)
            If ((appointmentCount + 1) * 20) > (Evening_Time + Morning_Time) Then
                MessageBox.Show("Sorry, appointments are not available for this day. Please try for a date in the future. Thank you for your understanding.")
            Else
                BookAppointment(appointmentCount, patientId, DoctorID)
            End If
        Else
            MessageBox.Show("Doctor not found.")
        End If
    End Sub

    ' Method to book appointment in database
    Private Sub BookAppointment(ByVal appointmentCount As Integer, ByVal Patient_ID As String, ByVal doctorID As Integer)
        appointmentCount = appointmentCount + 1 ' Increment appointment count
        Dim selectedDateTime As DateTime = DateTimePicker1.Value
        Dim formattedString As String = selectedDateTime.ToString("yyyy-MM-dd HH:mm:ss")

        ' Add 10 seconds to the DateTime for new appointment
        Dim newDateTime As DateTime = selectedDateTime.AddSeconds(appointmentCount)
        Dim formattedNewDateTime As String = newDateTime.ToString("yyyy-MM-dd HH:mm:ss") ' Format new DateTime

        ' Query to insert appointment into database
        Dim query As String = "INSERT INTO Appointments (Appointment_Number, Doctor_ID, Patient_ID ,Appointment_Time, Appointment_Type) VALUES (@appointmentCount,@doctorID,@Patient_ID,@formattedNewDateTime,0)"

        ' Establish connection and execute the query
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@appointmentCount", appointmentCount)
                command.Parameters.AddWithValue("@doctorID", doctorID)
                command.Parameters.AddWithValue("@Patient_ID", Patient_ID)
                command.Parameters.AddWithValue("@formattedNewDateTime", formattedNewDateTime)
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Appointment Booked Successfully")
                Catch ex As Exception
                    MessageBox.Show("Error inserting appointment: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' Function to get appointment count for a doctor on a specific date
    Private Function GetAppointmentCount(ByVal doctorID As Integer, ByVal selectedDate As DateTime) As Integer
        Dim appointmentCount As Integer = 0

        ' Query to count appointments for the selected doctor on the selected date
        Dim query As String = "SELECT COUNT(*) FROM Appointments WHERE Doctor_ID = @DoctorID AND CONVERT(DATE, Appointment_Time) = CONVERT(DATE,@SelectedDate)"

        ' Establish connection and execute the query
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@DoctorID", doctorID)
                command.Parameters.AddWithValue("@SelectedDate", selectedDate)

                Try
                    connection.Open()
                    appointmentCount = Convert.ToInt32(command.ExecuteScalar())
                Catch ex As Exception
                    MessageBox.Show("Error getting appointment count: " & ex.Message)
                End Try
            End Using
        End Using

        ' Return the appointment count
        Return appointmentCount
    End Function

    ' Method to retrieve doctor information from the database
    Private Sub GetDoctorInfo(ByVal doctorName As String, ByVal specialization As String)
        ' Query to get doctor information based on name and specialization
        Dim query As String = "SELECT Doctor_ID_Staff_ID ,Morning_Visiting_Time_Start,Morning_Visiting_Time_End ,Evening_Visiting_Time_Start, Evening_Visiting_Time_End FROM Doctor_Staff WHERE Name = @DoctorName AND Speciality_Designation = @Specialization"

        ' Establish connection and execute the query
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@DoctorName", doctorName)
                command.Parameters.AddWithValue("@Specialization", specialization)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' If doctor information is found, fill the relevant variables
                    If reader.Read() Then

                        Dim morning_time_start As TimeSpan
                        Dim morning_time_end As TimeSpan
                        Dim evening_time_start As TimeSpan
                        Dim evening_time_end As TimeSpan

                        ' Check if the reader value is null for Morning_Visiting_Time_Start
                        If reader("Morning_Visiting_Time_Start") Is DBNull.Value Then
                            morning_time_start = TimeSpan.Zero
                        Else
                            morning_time_start = CType(reader("Morning_Visiting_Time_Start"), TimeSpan)
                        End If

                        ' Check if the reader value is null for Morning_Visiting_Time_End
                        If reader("Morning_Visiting_Time_End") Is DBNull.Value Then
                            morning_time_end = TimeSpan.Zero
                        Else
                            morning_time_end = CType(reader("Morning_Visiting_Time_End"), TimeSpan)
                        End If

                        ' Check if the reader value is null for Evening_Visiting_Time_Start
                        If reader("Evening_Visiting_Time_Start") Is DBNull.Value Then
                            evening_time_start = TimeSpan.Zero
                        Else
                            evening_time_start = CType(reader("Evening_Visiting_Time_Start"), TimeSpan)
                        End If

                        ' Check if the reader value is null for Evening_Visiting_Time_End
                        If reader("Evening_Visiting_Time_End") Is DBNull.Value Then
                            evening_time_end = TimeSpan.Zero
                        Else
                            evening_time_end = CType(reader("Evening_Visiting_Time_End"), TimeSpan)
                        End If

                        ' Set the corresponding DateTime values
                        Morning_Visiting_Time_Start = DateTime.Today.Add(morning_time_start)
                        Morning_Visiting_Time_End = DateTime.Today.Add(morning_time_end)
                        Evening_Visiting_Time_Start = DateTime.Today.Add(evening_time_start)
                        Evening_Visiting_Time_End = DateTime.Today.Add(evening_time_end)


                        DoctorID = Convert.ToInt32(reader("Doctor_ID_Staff_ID"))
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error getting doctor information: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

End Class
