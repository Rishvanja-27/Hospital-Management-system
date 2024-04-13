Imports System.Data.SqlClient

Public Class Admin_GenrateBill
    ' Declare ListView objects and other variables
    Dim ListView2 As New ListView()
    Dim listView3 As New ListView()
    Dim counter As Integer

    ' Connection string for the database
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
    ' Variable to store patient ID
    Dim Patient_Id As Integer
    ' Constant for maximum height of ListView
    Const MAX_HEIGHT As Integer = 120

    ' Form load event handler
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Initialize counter
        counter = 1
        ' Configure ListView2 properties
        ListView2.Name = "ListView2"
        ListView2.Width = 600
        ListView2.View = View.Details
        ListView2.FullRowSelect = True
        ListView2.GridLines = False
        ListView2.BorderStyle = BorderStyle.None
        ListView2.Font = New Font("Arial", 14)
        ListView2.Columns.Add("Sr", 50)
        ListView2.Columns.Add("Name", 190)
        ListView2.Columns.Add("Exp. Date", 130)
        ListView2.Columns.Add("Available", 90)
        ListView2.Columns.Add("MRP", 90)
        ' Set ListView2 location
        ListView2.Location = New Point(55, 200)
        ListView2.MaximumSize = New Size(Integer.MaxValue, MAX_HEIGHT)
        ' Add ListView2 to form controls
        Me.Controls.Add(ListView2)
        ListView2.BringToFront()

        ' Configure listView3 properties
        listView3.Name = "ListView3"
        listView3.Width = 350
        listView3.View = View.Details
        listView3.FullRowSelect = True
        listView3.GridLines = False
        listView3.BorderStyle = BorderStyle.None
        listView3.Font = New Font("Arial", 14)
        listView3.Columns.Add("Sr.", 50)
        listView3.Columns.Add("Name", 150)
        listView3.Columns.Add("Quantity", 150)
        ' Set listView3 location
        listView3.Location = New Point(165, 370)
        listView3.MaximumSize = New Size(Integer.MaxValue, MAX_HEIGHT)
        ' Add listView3 to form controls
        Me.Controls.Add(listView3)
        listView3.BringToFront()
        Button2.BringToFront()

        ' Retrieve medicine data and populate ListView2
        RetrieveMedicineData(ListView2)
    End Sub

    ' Function to retrieve medicine data from the database and populate ListView
    Private Sub RetrieveMedicineData(ByVal listView As ListView)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT * FROM Medicine"
                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim sr As Integer = 1
                        While reader.Read()
                            Dim name As String = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), String.Empty)
                            Dim expiryDate As String = If(reader("Expiry_date") IsNot DBNull.Value, Convert.ToDateTime(reader("Expiry_date")).ToString("MM/dd/yyyy"), String.Empty)
                            Dim quantity As Integer = If(reader("Quantity") IsNot DBNull.Value, Convert.ToInt32(reader("Quantity")), 0)
                            Dim mrp As Double = If(reader("MaximumRetailPrice") IsNot DBNull.Value, Convert.ToDouble(reader("MaximumRetailPrice")), 0.0)
                            Dim row As New ListViewItem(New String() {sr.ToString(), name, expiryDate, quantity.ToString(), mrp.ToString()})
                            listView.Items.Add(row)
                            sr += 1
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving medicine data: " & ex.Message)
        End Try
    End Sub

    ' Event handler for search button click
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button9.Click
        ListView2.Items.Clear()
        Dim searchTerm As String = TextBox2.Text.Trim()
        If Not String.IsNullOrEmpty(searchTerm) Then
            SearchMedicineData(ListView2, searchTerm)
        Else
            RetrieveMedicineData(ListView2)
        End If
    End Sub

    ' Event handler for clearing search
    Private Sub btnSearch_Click2(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        ListView2.Items.Clear()
        RetrieveMedicineData(ListView2)
        TextBox2.Text = ""
    End Sub

    ' Function to search medicine data based on search term
    Private Sub SearchMedicineData(ByVal listView As ListView, ByVal searchTerm As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT * FROM Medicine WHERE Name LIKE @MedicineName"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@MedicineName", "%" & searchTerm & "%")
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim sr As Integer = 1
                        While reader.Read()
                            Dim name As String = If(reader("Name") IsNot DBNull.Value, reader("Name").ToString(), String.Empty)
                            Dim expiryDate As String = If(reader("Expiry_date") IsNot DBNull.Value, Convert.ToDateTime(reader("Expiry_date")).ToString("MM/dd/yyyy"), String.Empty)
                            Dim quantity As Integer = If(reader("Quantity") IsNot DBNull.Value, Convert.ToInt32(reader("Quantity")), 0)
                            Dim mrp As Double = If(reader("MaximumRetailPrice") IsNot DBNull.Value, Convert.ToDouble(reader("MaximumRetailPrice")), 0.0)
                            Dim row As New ListViewItem(New String() {sr.ToString(), name, expiryDate, quantity.ToString(), mrp.ToString()})
                            listView.Items.Add(row)
                            sr += 1
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching for medicine data: " & ex.Message)
        End Try
    End Sub

    ' Event handler for adding medicine to bill
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        If ListView2.SelectedItems.Count > 0 Then
            Dim selectedMedicine As ListViewItem = ListView2.SelectedItems(0)
            Dim medicineName As String = selectedMedicine.SubItems(1).Text
            Dim quantityRequired As Integer
            If Integer.TryParse(TextBox3.Text, quantityRequired) Then
                Dim row As New ListViewItem(New String() {counter.ToString(), medicineName, quantityRequired.ToString()})
                listView3.Items.Add(row)
                listView3.BringToFront()
                counter += 1
            Else
                MessageBox.Show("Please enter a valid quantity.")
            End If
        Else
            MessageBox.Show("Please select a medicine from ListView2.")
        End If
    End Sub

    ' Event handler for generating bill
    Private Sub btnGen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If listView3.Items.Count = 0 Then
            MessageBox.Show("Please Select atleast one medicine")
            Return
        End If
        Dim Appointment_Id As Integer = TextBox1.Text
        Patient_Id = GetPatientID(Appointment_Id)
        InsertIntoBill(Patient_Id)
        Dim Bill_Id As Integer = GetLatestBillID()
        InsertBillIDIntoAppointment(Appointment_Id, Bill_Id)
        For Each item As ListViewItem In listView3.Items
            Dim medicineName As String = item.SubItems(1).Text
            Dim quantity As Integer = Convert.ToInt32(item.SubItems(2).Text)
            Dim medicineId As Integer = GetMedicineID(medicineName)
            InsertIntoBillMedicine(Bill_Id, medicineId, quantity)
            ReduceMedicineQuantity(medicineId, quantity)
        Next
        listView3.Items.Clear()
        TextBox2.Text = ""
        TextBox3.Text = ""
        ListView2.Items.Clear()
        RetrieveMedicineData(ListView2)
        MessageBox.Show("Bill generated successfully.")
    End Sub

    ' Function to get patient ID from Appointment ID
    Private Function GetPatientID(ByVal appntmntID As Integer) As Integer
        Dim patientID As Integer = 0
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT Patient_ID FROM Appointments WHERE Appointment_ID = @pName"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@pName", appntmntID)
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        patientID = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching patient ID: " & ex.Message)
        End Try
        Return patientID
    End Function

    ' Function to insert record into Bill_Medicine table
    Private Sub InsertIntoBillMedicine(ByVal Bill_Id As Integer, ByVal medicineId As Integer, ByVal quantity As Integer)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO Bill_Medicine_Quantity (Bill_Id, Medicine_Id, Quantity) VALUES (@Bill_Id, @MedicineId, @Quantity)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Bill_Id", Bill_Id)
                    command.Parameters.AddWithValue("@MedicineId", medicineId)
                    command.Parameters.AddWithValue("@Quantity", quantity)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating Bill_Medicine table: " & ex.Message)
        End Try
    End Sub

    ' Function to reduce medicine quantity in Medicine table
    Private Sub ReduceMedicineQuantity(ByVal medicineID As Integer, ByVal quantity As Integer)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "UPDATE Medicine SET Quantity = Quantity - @Quantity WHERE Medicine_ID = @MedicineID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@MedicineID", medicineID)
                    command.Parameters.AddWithValue("@Quantity", quantity)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating Medicine table: " & ex.Message)
        End Try
    End Sub

    ' Function to insert record into Bill table
    Private Sub InsertIntoBill(ByVal Patient_Id As Integer)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO Bill (Patient_ID, Total_Price) VALUES (@Patient_Id, @Amount)"
                Using command As New SqlCommand(query, connection)
                    Dim totalAmount As Double = CalculateTotalAmount()
                    command.Parameters.AddWithValue("@Amount", totalAmount)
                    command.Parameters.AddWithValue("@Patient_Id", Patient_Id)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating Bill table: " & ex.Message)
        End Try
    End Sub

    ' Function to get latest Bill ID
    Private Function GetLatestBillID() As Integer
        Dim latestBillID As Integer = 0
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT TOP 1 Bill_ID FROM Bill ORDER BY Bill_ID DESC"
                Using command As New SqlCommand(query, connection)
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        latestBillID = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching latest Bill ID: " & ex.Message)
        End Try
        Return latestBillID
    End Function

    ' Function to insert Bill ID into Appointment table
    Private Sub InsertBillIDIntoAppointment(ByVal Appointment_ID As Integer, ByVal Bill_ID As Integer)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "UPDATE Appointments SET Bill_ID = @Bill_ID, Appointment_Type = @Appointment_Type WHERE Appointment_ID = @Appointment_id"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Bill_ID", Bill_ID)
                    command.Parameters.AddWithValue("@Appointment_id", Appointment_ID)
                    command.Parameters.AddWithValue("@Appointment_Type", 2)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating Appointment table: " & ex.Message)
        End Try
    End Sub

    ' Function to get Medicine ID from Medicine name
    Private Function GetMedicineID(ByVal medicineName As String) As Integer
        Dim medicineID As Integer = 0
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT Medicine_id FROM Medicine WHERE Name = @MedicineName"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@MedicineName", medicineName)
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        medicineID = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching medicine ID: " & ex.Message)
        End Try
        Return medicineID
    End Function

    ' Function to calculate total amount for the bill
    Private Function CalculateTotalAmount() As Double
        Dim totalAmount As Double = 0
        For Each item As ListViewItem In listView3.Items
            Dim quantity As Integer = Convert.ToInt32(item.SubItems(2).Text)
            Dim price As Double = GetMedicinePrice(item.SubItems(1).Text)
            totalAmount += quantity * price
        Next
        Return totalAmount
    End Function

    ' Function to get Medicine price from Medicine name
    Private Function GetMedicinePrice(ByVal medicineName As String) As Double
        Dim price As Double = 0
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT MaximumRetailPrice FROM Medicine WHERE Name = @MedicineName"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@MedicineName", medicineName)
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        price = Convert.ToDouble(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching medicine price: " & ex.Message)
        End Try
        Return price
    End Function
End Class
