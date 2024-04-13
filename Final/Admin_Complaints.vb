Imports System.Data.SqlClient

Public Class Admin_Complaints

    ' Connection string for connecting to the database
    Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"

    ' This event handler runs when the form is loaded
    Private Sub Complaints_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Create and configure the DataGridView
        Dim dataGridView1 As New DataGridView()
        dataGridView1.Location = New Point(0, 0)
        dataGridView1.Size = New Size(570, 450)
        dataGridView1.BorderStyle = BorderStyle.None ' Remove border
        dataGridView1.BackgroundColor = Me.BackColor ' Set background color to control color
        Me.Controls.Add(dataGridView1)

        ' Add columns to the DataGridView
        Dim columnSr As New DataGridViewTextBoxColumn()
        columnSr.HeaderText = "Sr"
        columnSr.Name = "Sr"
        columnSr.Width = 50
        columnSr.HeaderCell.Style.Font = New Font("Arial", 16) ' Set font for column header
        dataGridView1.Columns.Add(columnSr)

        Dim columnComplaint As New DataGridViewTextBoxColumn()
        columnComplaint.HeaderText = "Complaint"
        columnComplaint.Name = "Complaint"
        columnComplaint.Width = 365
        columnComplaint.HeaderCell.Style.Font = New Font("Arial", 16) ' Set font for column header
        columnComplaint.DefaultCellStyle.WrapMode = DataGridViewTriState.True ' Enable word wrap for cell content
        dataGridView1.Columns.Add(columnComplaint)

        Dim columnFrom As New DataGridViewTextBoxColumn()
        columnFrom.HeaderText = "From"
        columnFrom.Name = "From"
        columnFrom.Width = 150
        columnFrom.HeaderCell.Style.Font = New Font("Arial", 16) ' Set font for column header
        columnFrom.DefaultCellStyle.Padding = New Padding(5) ' Add padding to cell content
        dataGridView1.Columns.Add(columnFrom)

        ' Set DataGridView properties
        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells ' Automatically adjust row height based on content
        dataGridView1.AllowUserToAddRows = False ' Disable adding rows manually
        dataGridView1.AllowUserToDeleteRows = False ' Disable deleting rows manually
        dataGridView1.RowHeadersVisible = False ' Hide row headers

        ' Set the height of the header
        dataGridView1.ColumnHeadersHeight = 40  ' Adjust the value as needed

        ' Fetch complaints from the database and add them to the DataGridView
        LoadComplaints(dataGridView1)

        dataGridView1.BringToFront()
    End Sub

    ' Method to load complaints from the database and populate the DataGridView
    Private Sub LoadComplaints(ByVal dataGridView As DataGridView)
        ' Clear existing rows
        dataGridView.Rows.Clear()

        ' SQL query to fetch complaints from the database
        Dim query As String = "SELECT Complaint_ID, Complaint, Complaint_From FROM Complaints"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' Loop through the database records and add them as rows to the DataGridView
                    While reader.Read()
                        ' Add data to the DataGridView rows
                        Dim row As String() = {reader("Complaint_ID").ToString(), reader("Complaint").ToString(), reader("Complaint_From").ToString()}
                        dataGridView.Rows.Add(row)
                    End While
                Catch ex As Exception
                    ' Display error message if there's an exception
                    MessageBox.Show("Error loading complaints: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

End Class
