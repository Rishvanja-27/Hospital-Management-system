Imports System.Data.SqlClient

Public Class Admin_Cleanliness
    Dim listView1 As New ListView()

    Private Sub Admin_Cleanliness_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set properties for the ListView
        listView1.Name = "listView1"
        listView1.Height = 200
        listView1.Width = 350
        listView1.View = View.Details
        listView1.FullRowSelect = True
        listView1.GridLines = False
        listView1.BorderStyle = BorderStyle.None
        listView1.BackColor = SystemColors.Control ' Set background color to default Control color
        listView1.Font = New Font("Arial", 16)
        listView1.Columns.Add("Sr.", 50)
        listView1.Columns.Add("Room Name/No.", 200)
        listView1.Columns.Add("Cleaned", 100)
        listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable
        listView1.HideSelection = True

        ' Set the location of the ListView
        listView1.Location = New Point(0, 0)

        ' Add event handler for Click event of the ListView
        AddHandler listView1.Click, AddressOf listView1_SelectedIndexChanged

        ' Add the ListView to the form
        Me.Controls.Add(listView1)

        ' Load data from the Rooms table into the ListView
        LoadRoomData()

        ' Bring the ListView to the front
        listView1.BringToFront()

        ' Set background color for specific columns
        For Each item As ListViewItem In listView1.Items
            item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Sr.
            item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Room Name/No.
            item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Last Cleaned
        Next
    End Sub

    Private Sub LoadRoomData()
        ' Clear existing items in the ListView
        listView1.Items.Clear()

        ' Connect to the database and retrieve room data
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        Dim query As String = "SELECT Room_Number, Cleaning_Required FROM Rooms"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' Loop through the data and add each room to the ListView
                    While reader.Read()
                        Dim roomNumber As String = reader("Room_Number").ToString()
                        Dim Cleaning_Required As String = reader("Cleaning_Required").ToString()

                        ' Create a ListViewItem and add it to the ListView
                        Dim item As New ListViewItem({(listView1.Items.Count + 1).ToString(), roomNumber, Cleaning_Required})
                        listView1.Items.Add(item)
                    End While

                    reader.Close()
                Catch ex As Exception
                    ' Display error message if there's an exception
                    MessageBox.Show("Error loading room data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub listView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Check if an item in the ListView is selected
        If listView1.SelectedItems.Count = 1 Then
            Dim selectedItem As ListViewItem = listView1.SelectedItems(0)
            Dim roomNumber As String = selectedItem.SubItems(1).Text

            ' Show a dialog box to change the cleaning status
            Dim result As DialogResult = MessageBox.Show("Do you want to change the cleaning status of Room " & roomNumber & "?", "Change Cleaning Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Toggle the cleaning status (from Yes to No or vice versa)
                Dim newCleaningStatus As String = If(selectedItem.SubItems(2).Text = "Yes", "No", "Yes")

                ' Update the cleaning status in the database
                UpdateCleaningStatus(roomNumber, newCleaningStatus)

                ' Update the ListView to reflect the change
                selectedItem.SubItems(2).Text = newCleaningStatus
            End If
        End If
    End Sub

    Private Sub UpdateCleaningStatus(ByVal roomNumber As String, ByVal newStatus As String)
        ' Connect to the database and update the cleaning status
        Dim connectionString As String = "Data Source=RASHAADPC\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        Dim query As String = "UPDATE Rooms SET Cleaning_Required = @NewStatus WHERE Room_Number = @RoomNumber"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@NewStatus", newStatus)
                command.Parameters.AddWithValue("@RoomNumber", roomNumber)

                Try
                    ' Open the database connection and execute the query
                    connection.Open()
                    command.ExecuteNonQuery()
                Catch ex As Exception
                    ' Display error message if there's an exception
                    MessageBox.Show("Error updating cleaning status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
End Class
