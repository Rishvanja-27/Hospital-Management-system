Imports System.Data.SqlClient

Public Class Admin_DoctorsList

    ' Declare a ListView control
    Dim listView1 As New ListView()

    ' Declare an instance of Admin_EditDoctor form
    Private WithEvents adminEditDoctorInstance As Admin_EditDoctor ' Declare without WithEvents

    ' Load event handler for the form
    Private Sub Admin_DoctorsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Initialize and configure the ListView control
        listView1 = New ListView()
        listView1.Name = "listView1"
        listView1.Height = 723
        listView1.Width = 907
        listView1.View = View.Details ' Set view to Details mode
        listView1.FullRowSelect = True ' Select entire row when clicked
        listView1.GridLines = False ' Hide grid lines
        listView1.BorderStyle = BorderStyle.None
        listView1.Font = New Font("Arial", 16)
        listView1.BackColor = SystemColors.Control ' Set background color to default Control color
        listView1.Columns.Add("Sr.", 45)
        listView1.Columns.Add("Doctor", 120)
        listView1.Columns.Add("Specialization", 150)
        listView1.Columns.Add("Morning Visiting Time", 140)
        listView1.Columns.Add("Evening Visiting Time", 140)
        listView1.Columns.Add("Action", 85)
        listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable ' Make column headers non-clickable
        listView1.HideSelection = True ' Remove highlighting effect after clicking
        AddHandler listView1.Click, AddressOf listView1_SelectedIndexChanged
        listView1.Location = New Point(0, 0) ' Set location to (30, 200)

        ' Add the ListView to the form
        Me.Controls.Add(listView1)

        ' Load doctors' data from the database
        LoadDoctors()

        ' Bring the ListView to the front
        listView1.BringToFront()
    End Sub

    ' Method to load doctors' data from the database and populate the ListView
    Private Sub LoadDoctors()
        ' Connection string for the database
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' SQL query to retrieve doctors' data
            Dim query As String = "SELECT Doctor_ID_Staff_ID, Name, Speciality_Designation, " &
                "CONVERT(VARCHAR(5), Morning_Visiting_Time_Start, 108) + '-' + CONVERT(VARCHAR(5), Morning_Visiting_Time_End, 108) AS Morning_Visiting_Time, " &
                "CONVERT(VARCHAR(5), Evening_Visiting_Time_Start, 108) + '-' + CONVERT(VARCHAR(5), Evening_Visiting_Time_End, 108) AS Evening_Visiting_Time " &
                "FROM Doctor_Staff;"

            ' Execute the SQL query
            Using command As New SqlCommand(query, connection)
                Dim adapter As New SqlDataAdapter(command)
                Dim table As New DataTable()

                ' Fill the DataTable with data from the database
                adapter.Fill(table)

                ' Loop through the DataTable and add each doctor to the ListView
                For Each row As DataRow In table.Rows
                    Dim doctorID As String = row("Doctor_ID_Staff_ID").ToString()
                    Dim name As String = row("Name").ToString()
                    Dim specialization As String = row("Speciality_Designation").ToString()
                    Dim morningTime As String = row("Morning_Visiting_Time").ToString()
                    Dim eveningTime As String = row("Evening_Visiting_Time").ToString()

                    ' Create a ListViewItem and add it to the ListView
                    Dim item As New ListViewItem({doctorID, name, specialization, morningTime, eveningTime, "Edit"})
                    listView1.Items.Add(item)

                Next

                ' Set background color for specific columns
                For Each item As ListViewItem In listView1.Items
                    item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Sr.
                    item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Doctor Name
                    item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Specialization
                    item.SubItems(3).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Morning Visiting Time
                    item.SubItems(4).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Evening Visiting Time
                    item.SubItems(5).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Action
                Next
            End Using
        End Using
    End Sub

    ' Event handler for the DoctorDetailsUpdatedSuccessfully event raised by Admin_EditDoctor form
    Private Sub Admin_EditDoctor_DoctorDetailsUpdatedSuccessfully(ByVal sender As Object, ByVal e As EventArgs) Handles adminEditDoctorInstance.DoctorDetailsUpdatedSuccessfully
        ' This event handler will be called when the DoctorDetailsUpdatedSuccessfully event is raised in the Admin_EditDoctor form
        ' Handle the event here...
        MyBase.OnLoad(e)
    End Sub

    ' Event handler for the ListView's SelectedIndexChanged event
    Private Sub listView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Check if an item is selected
        If listView1.SelectedItems.Count = 1 Then
            ' Get the selected item
            Dim selectedItem As ListViewItem = listView1.SelectedItems(0)
            ' Extract the doctor's name and visiting time from the selected item
            Dim doctorName As String = selectedItem.SubItems(1).Text
            Dim MorningvisTime As String = selectedItem.SubItems(3).Text
            Dim EveningvisTime As String = selectedItem.SubItems(4).Text

            ' Create a new instance of Admin_EditDoctor form with the doctor's name and visiting time as parameters
            adminEditDoctorInstance = New Admin_EditDoctor(doctorName, MorningvisTime, EveningvisTime, selectedItem.SubItems(0).Text)
            ' Add event handler for DoctorDetailsUpdatedSuccessfully event
            AddHandler adminEditDoctorInstance.DoctorDetailsUpdatedSuccessfully, AddressOf Admin_EditDoctor_DoctorDetailsUpdatedSuccessfully
            ' Show the Admin_EditDoctor form
            adminEditDoctorInstance.ShowDialog()
        End If
    End Sub

End Class
