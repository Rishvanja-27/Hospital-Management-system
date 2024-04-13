Imports System.Data.SqlClient

Public Class Admin_LeaveRequests
    Dim listView1 As New ListView()

    Private Sub Admin_LeaveRequests_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Set up column headings for the ListView
        listView1.Name = "listView1"
        listView1.Height = 723
        listView1.Width = 907
        listView1.View = View.Details
        listView1.BorderStyle = BorderStyle.None
        listView1.FullRowSelect = True
        listView1.GridLines = False ' Display grid lines
        listView1.BackColor = SystemColors.Control ' Set background color to default Control color
        listView1.Font = New Font("Arial", 12)
        listView1.Columns.Add("Leave ID", 80)
        listView1.Columns.Add("Doctor Name", 100)
        listView1.Columns.Add("Start Date", 108)
        listView1.Columns.Add("End Date", 108)
        listView1.Columns.Add("Reason", 170)
        listView1.Columns.Add("Status", 100)
        listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable ' Make column headers non-clickable
        listView1.HideSelection = True ' Remove highlighting effect after clicking
        AddHandler listView1.Click, AddressOf listView1_SelectedIndexChanged
        listView1.Location = New Point(0, 0)

        ' Add the ListView to the form
        Me.Controls.Add(listView1)

        ' Load leave requests from the database
        LoadLeaveRequests(listView1)

        ' Bring the ListView to the front
        listView1.BringToFront()
    End Sub

    ' Function to load leave requests from the database
    Private Sub LoadLeaveRequests(ByVal listView As ListView)
        ' Clear existing items in the ListView
        listView.Items.Clear()

        ' Connect to the database and retrieve leave requests data
        Dim connectionString As String = "Data Source=DESKTOP-EEQT867\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        Dim query As String = "SELECT l.Leave_Application_ID, d.Name AS DoctorName, l.Leave_Period_Start, l.Leave_Period_End, l.Reason, l.Status " &
                              "FROM Leave_Application l " &
                              "INNER JOIN Doctor_Staff d ON l.Doctor_ID = d.Doctor_ID_Staff_ID"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' Loop through the data and add each leave request to the ListView
                    While reader.Read()
                        Dim leaveID As String = reader("Leave_Application_ID").ToString()
                        Dim doctorName As String = reader("DoctorName").ToString()
                        Dim startDate As String = reader("Leave_Period_Start").ToString()
                        Dim endDate As String = reader("Leave_Period_End").ToString()
                        Dim reason As String = reader("Reason").ToString()
                        Dim status As String = reader("Status").ToString()

                        ' Create a ListViewItem and add it to the ListView
                        Dim item As New ListViewItem({leaveID, doctorName, startDate, endDate, reason, status})
                        listView.Items.Add(item)

                    End While

                    ' Set background color for specific subitems
                    For Each item As ListViewItem In listView.Items
                        item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Leave ID
                        item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Doctor Name
                        item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Start Date
                        item.SubItems(3).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' End Date
                        item.SubItems(4).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Reason
                        item.SubItems(5).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Status
                    Next

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error loading leave requests: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    ' Event handler for ListView selection change
    Private Sub listView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Check if an item is selected
        If listView1.SelectedItems.Count = 1 Then
            ' Get the selected item
            Dim selectedItem As ListViewItem = listView1.SelectedItems(0)
            ' Extract the leave ID and current status from the selected item
            Dim leaveID As Integer = Convert.ToInt32(selectedItem.SubItems(0).Text)
            Dim currentStatus As String = selectedItem.SubItems(5).Text

            ' Define the default action text and confirmation message
            Dim actionText As String = "Confirm"
            Dim confirmationMessage As String = "Do you want to confirm this leave?"

            ' Check the current status and update action text and confirmation message accordingly
            Select Case currentStatus
                Case "Pending"
                    actionText = "Confirm or Reject"
                    confirmationMessage = "Do you want to confirm or reject this leave?"
                Case "Confirmed"
                    actionText = "Reject"
                    confirmationMessage = "Do you want to reject this leave?"
                Case "Rejected"
                    actionText = "Confirm"
                    confirmationMessage = "Do you want to confirm this leave?"
            End Select

            ' Display a dialog box with updated action text and confirmation message
            Dim result As DialogResult = MessageBox.Show(confirmationMessage, "Leave " & actionText, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            ' Handle the user's response
            If result = DialogResult.Yes Then
                ' Update the leave status
                If currentStatus = "Pending" Then
                    UpdateLeaveStatus(leaveID, "Confirmed")
                ElseIf currentStatus = "Confirmed" Then
                    UpdateLeaveStatus(leaveID, "Rejected")
                ElseIf currentStatus = "Rejected" Then
                    UpdateLeaveStatus(leaveID, "Confirmed")
                End If
            End If
        End If
    End Sub

    ' Function to update the leave status in the database
    Private Sub UpdateLeaveStatus(ByVal leaveID As Integer, ByVal status As String)
        ' Connect to the database and update the leave status
        Dim connectionString As String = "Data Source=RASHAADPC\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;"
        Dim query As String = "UPDATE Leave_Application SET Status = @Status WHERE Leave_Application_ID = @LeaveID"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@Status", status)
                command.Parameters.AddWithValue("@LeaveID", leaveID)

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Leave status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ' Reload leave requests data after updating status
                    LoadLeaveRequests(listView1)
                Catch ex As Exception
                    MessageBox.Show("Error updating leave status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
End Class
