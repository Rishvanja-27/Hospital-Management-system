Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D

Public Class Doctors_Medicine

    Private par As Patients_MainForm
    Private doctorID As Integer

    ' Store the initial text
    Private initialText As String = "Search medicine..."

    ' Constructor modified to accept parent form and doctor ID as parameters
    Public Sub New(ByVal parent As Patients_MainForm, ByVal doctorID As Integer)
        InitializeComponent()
        par = parent
        Me.doctorID = doctorID ' Store the doctor's ID
    End Sub

    Private Sub Doctors_Medicine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Define connection string
        InitializeSearchTextBox()
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Set properties for the ListView
        listView1.Name = "listView1"
        listView1.Width = 1000 ' Set width to 1000
        listView1.View = View.Details ' Set view to Details mode
        listView1.FullRowSelect = True ' Select entire row when clicked
        listView1.GridLines = False ' Display grid lines
        listView1.BorderStyle = BorderStyle.None
        listView1.Font = New Font("Arial", 14)
        listView1.Columns.Add("Medicine ID", 100)
        listView1.Columns.Add("Medicine Name", 260)
        listView1.Columns.Add("Expiry Date", 200)
        listView1.Columns.Add("Manufacturing Date", 240)
        listView1.Columns.Add("Quantity", 100)

        ' Create a new connection
        Using connection As New SqlConnection(connectionString)
            Try
                ' Open the connection
                connection.Open()

                ' Define SQL query to fetch medicine data
                Dim query As String = "SELECT Medicine_ID, Name, CONVERT(varchar, Expiry_date, 23) AS Expiry_date, CONVERT(varchar, Mfg_date, 23) AS Mfg_date, Quantity FROM Medicine"

                ' Create a SqlCommand object
                Using command As New SqlCommand(query, connection)
                    ' Execute the command and create a SqlDataReader
                    Using reader As SqlDataReader = command.ExecuteReader()
                        ' Check if there are rows returned
                        If reader.HasRows Then
                            ' Loop through the rows
                            While reader.Read()
                                ' Retrieve medicine details from the reader
                                Dim medicineID As String = reader("Medicine_ID").ToString()
                                Dim medicineName As String = reader("Name").ToString()
                                Dim expiryDate As String = reader("Expiry_date").ToString()
                                Dim manufacturingDate As String = reader("Mfg_date").ToString()
                                Dim quantity As String = reader("Quantity").ToString()

                                ' Add medicine details to the ListView
                                Dim row As New ListViewItem({medicineID, medicineName, expiryDate, manufacturingDate, quantity})
                                listView1.Items.Add(row)
                            End While
                        Else
                            ' No medicine found
                            MessageBox.Show("No medicine found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Set the location of the ListView
        listView1.Location = New Point(30, 90) ' Set location to (30, 90)

        ' Set background color for specific columns
        For Each item As ListViewItem In listView1.Items
            item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Medicine ID
            item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Medicine Name
            item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Expiry Date
            item.SubItems(3).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Manufacturing Date
            item.SubItems(4).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Quantity
        Next

        ' Populate medicine data when the form loads
        PopulateMedicineData()

        ' Initialize the curved TextBox
        InitializeCurvedTextBox()
    End Sub


    Private Sub InitializeCurvedTextBox()
        ' Create a GraphicsPath to define the custom shape
        Dim path As New GraphicsPath()

        ' Specify the curvature of the ends (adjust these values as needed)
        Dim curvature As Integer = 20

        ' Define the shape of the textbox
        path.AddArc(0, 0, curvature * 2, curvature * 2, 180, 90)   ' Top-left corner
        path.AddArc(searchTextBox.Width - curvature * 2, 0, curvature * 2, curvature * 2, 270, 90)   ' Top-right corner
        path.AddArc(searchTextBox.Width - curvature * 2, searchTextBox.Height - curvature * 2, curvature * 2, curvature * 2, 0, 90)   ' Bottom-right corner
        path.AddArc(0, searchTextBox.Height - curvature * 2, curvature * 2, curvature * 2, 90, 90)   ' Bottom-left corner

        ' Create a region with the defined shape and set it to the TextBox
        Dim region As New Region(path)
        searchTextBox.Region = region
    End Sub

    Private Sub PopulateMedicineData()
        ' Define the SQL query to fetch all medicines
        Dim query As String = "SELECT * FROM Medicine"

        ' Define the connection string
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Create a connection to the database
        Using connection As New SqlConnection(connectionString)
            ' Create a command object with the SQL query
            Using command As New SqlCommand(query, connection)
                Try
                    ' Open the connection
                    connection.Open()

                    ' Execute the query and populate the ListView with the results
                    Dim adapter As New SqlDataAdapter(command)
                    Dim medicineTable As New DataTable()
                    adapter.Fill(medicineTable)
                    PopulateListViewWithDataTable(medicineTable)
                Catch ex As Exception
                    ' Display an error message if an exception occurs
                    MessageBox.Show("Error retrieving medicine data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub PopulateListViewWithDataTable(ByVal dataTable As DataTable)
        ' Clear the existing items from the ListView
        listView1.Items.Clear()

        ' Loop through the rows of the DataTable and add them to the ListView
        For Each row As DataRow In dataTable.Rows
            Dim medicineID As String = row("Medicine_ID").ToString()
            Dim medicineName As String = row("Name").ToString()
            Dim expiryDate As String = CType(row("Expiry_date"), DateTime).ToString("yyyy-MM-dd") ' Convert to string with date format
            Dim manufacturingDate As String = CType(row("Mfg_date"), DateTime).ToString("yyyy-MM-dd") ' Convert to string with date format
            Dim quantity As String = row("Quantity").ToString()


            Dim listViewItem As New ListViewItem({medicineID, medicineName, expiryDate, manufacturingDate, quantity})
            listView1.Items.Add(listViewItem)
        Next
    End Sub

    Private Sub searchButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles searchButton.Click
        ' Get the search text from the TextBox and convert it to lowercase
        Dim searchText As String = searchTextBox.Text.Trim().ToLower()

        ' If the search text is empty, display all medicines
        If String.IsNullOrEmpty(searchText) Then
            PopulateMedicineData()
            Return
        End If

        ' Define the SQL query with a WHERE clause to filter medicine by name (case-insensitive)
        Dim query As String = "SELECT * FROM Medicine WHERE LOWER(Name) LIKE @SearchText"

        ' Define the connection string
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

        ' Create a connection to the database
        Using connection As New SqlConnection(connectionString)
            ' Create a command object with the SQL query
            Using command As New SqlCommand(query, connection)
                ' Add a parameter for the search text
                command.Parameters.AddWithValue("@SearchText", "%" & searchText & "%")

                Try
                    ' Open the connection
                    connection.Open()

                    ' Execute the query and populate the ListView with the search results
                    Dim adapter As New SqlDataAdapter(command)
                    Dim medicineTable As New DataTable()
                    adapter.Fill(medicineTable)
                    PopulateListViewWithDataTable(medicineTable)
                Catch ex As Exception
                    ' Display an error message if an exception occurs
                    MessageBox.Show("Error retrieving medicine data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub


    Private Sub searchTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles searchTextBox.Enter
        ' Clear the text when the user clicks inside if it matches the initial text
        If searchTextBox.Text = initialText Then
            searchTextBox.Text = ""
            searchTextBox.ForeColor = Color.Black ' Set the text color to black
        End If
    End Sub

    Private Sub searchTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles searchTextBox.Leave
        ' Restore the initial text when the user leaves the TextBox without entering anything
        If String.IsNullOrWhiteSpace(searchTextBox.Text) Then
            searchTextBox.Text = initialText
            searchTextBox.ForeColor = Color.Gray ' Set the text color to gray
        End If
    End Sub
    Private Sub InitializeSearchTextBox()
        ' Set the initial text
        searchTextBox.Text = initialText
        searchTextBox.ForeColor = Color.Gray ' Set the text color to gray
    End Sub
End Class
