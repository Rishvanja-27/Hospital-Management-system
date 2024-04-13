Imports System.Data.SqlClient ' Import the namespace for SQL Server data access

Public Class Patients_Bill ' Define a class named Patients_Bill

    Public Sub New(ByVal AppointmentID As Integer, ByVal billingTime As String)
        ' Constructor for the Patients_Bill class, takes AppointmentID and billingTime as parameters

        ' This call is required by the designer to initialize the form components.
        InitializeComponent()

        ' Define connection string to connect to the database
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Initialize variables to store bill information
        Dim doctorName As String = ""
        Dim patient As String = ""
        Dim fees As Decimal = 0

        ' Initialize DataTable to store medicine data
        Dim medicineData As New DataTable()

        ' Query the database to retrieve bill information based on the AppointmentID
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                ' SQL query to retrieve bill information
                Dim query As String = "SELECT D.Name AS DoctorName, P.Name AS PatientName, B.Total_Price, B.Billing_Time FROM Appointments A INNER JOIN Doctor_Staff D ON A.Doctor_ID = D.Doctor_ID_Staff_ID INNER JOIN Bill B ON A.Bill_ID = B.Bill_ID INNER JOIN Patient P ON A.Patient_ID = P.Patient_ID WHERE A.Appointment_ID = @AppointmentID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID)
                    ' Execute the command and read the result
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Extract doctor name, patient name, and fees from the reader
                            doctorName = reader("DoctorName").ToString()
                            patient = reader("PatientName").ToString()
                            fees = Convert.ToDecimal(reader("Total_Price"))
                        Else
                            ' Show error message if no bill found for the appointment and return from the constructor
                            MessageBox.Show("No bill found for the appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If
                    End Using
                End Using

                ' Fetch medicine information associated with the bill from Bill_Medicine_Quantity table
                Dim medicineQuery As String = "SELECT ROW_NUMBER() OVER(ORDER BY M.Name) AS SNo, M.Name AS MedicineName, M.Expiry_date AS ExpiryDate, M.MaximumRetailPrice AS Price, B.Quantity FROM Bill_Medicine_Quantity B INNER JOIN Medicine M ON B.Medicine_ID = M.Medicine_ID WHERE B.Bill_ID = (SELECT Bill_ID FROM Appointments WHERE Appointment_ID = @AppointmentID)"
                Using adapter As New SqlDataAdapter(medicineQuery, connection)
                    adapter.SelectCommand.Parameters.AddWithValue("@AppointmentID", AppointmentID)
                    ' Fill the medicineData DataTable with the result of the query
                    adapter.Fill(medicineData)
                End Using
            Catch ex As Exception
                ' Show error message if an exception occurs during database operation
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Calculate GST and total amount
        Dim gstRate As Decimal = 0.848 ' GST rate is 18%
        Dim Amountwogst As Decimal = Math.Round(fees * gstRate, 2)
        Dim gst As Decimal = fees - Amountwogst

        ' Set the bill information on the form labels
        lblDoctorName.Text = "Doctor: " & doctorName
        lblpatient.Text = "Patient: " & patient
        lblFees.Text = "Amount: $" & Amountwogst.ToString("0.00")
        lblGST.Text = "GST (18%): $" & gst.ToString("0.00")
        lblTotalAmount.Text = "Total Amount: $" & fees.ToString("0.00")
        Label2.Text = "Date: " & billingTime

        ' Hide the row headers column in the DataGridView
        DataGridView1.RowHeadersVisible = False

        ' Bind medicine data to the DataGridView
        DataGridView1.DataSource = medicineData

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
