Public Class Admin_Security
    ' Declare a new instance of ListView
    Dim ListView2 As New ListView()

    ' Event handler for when the Admin_Security form is loaded
    Private Sub Admin_Security_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set properties for the ListView
        ListView2.Name = "ListView2"
        ListView2.Width = 710 ' Set width to 710
        ListView2.View = View.Details ' Set view to Details mode
        ListView2.FullRowSelect = True ' Select entire row when clicked
        ListView2.GridLines = False ' Display grid lines
        ListView2.BorderStyle = BorderStyle.None
        ListView2.Font = New Font("Arial", 14)
        ListView2.BackColor = SystemColors.Control

        ' Add columns to the ListView
        ListView2.Columns.Add("Sr", 50)
        ListView2.Columns.Add("Name", 200)
        ListView2.Columns.Add("Start Time", 130)
        ListView2.Columns.Add("End Time", 130)
        ListView2.Columns.Add("Contact No.", 200)
        ListView2.HeaderStyle = ColumnHeaderStyle.Nonclickable ' Make column headers non-clickable
        ListView2.HideSelection = True ' Remove highlighting effect after clicking

        ' Add data to the rows
        Dim row1 As New ListViewItem(New String() {"1", "Rampal", "6:00", "14:00", "9210389218"})
        Dim row2 As New ListViewItem(New String() {"2", "Dharampal", "6:00", "14:00", "9093209301"})
        Dim row3 As New ListViewItem(New String() {"3", "Ramesh", "14:00", "22:00", "8298313001"})
        Dim row4 As New ListViewItem(New String() {"4", "Suresh", "14:00", "22:00", "8132093201"})
        Dim row5 As New ListViewItem(New String() {"5", "Mukesh", "22:00", "6:00", "8321313318"})
        Dim row6 As New ListViewItem(New String() {"6", "Mukesh", "22:00", "6:00", "8956706741"})

        ' Add rows to the ListView
        ListView2.Items.AddRange(New ListViewItem() {row1, row2, row3, row4, row5, row6})

        ' Set background color for specific subitems
        For Each item As ListViewItem In ListView2.Items
            item.SubItems(0).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Sr.
            item.SubItems(1).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Name
            item.SubItems(2).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Start Time
            item.SubItems(3).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' End Time
            item.SubItems(4).BackColor = ColorTranslator.FromHtml("#D9D9D9") ' Contact No.
        Next

        ' Set the location of the ListView
        ListView2.Location = New Point(0, 0) ' Set location to (0, 50)

        ' Set the ListView's height to fit its content
        ListView2.Height = (ListView2.Items.Count * 35) ' Add some extra space for aesthetics

        ' Add the ListView to the form's controls
        Me.Controls.Add(ListView2)

        ' Bring the ListView to the front
        ListView2.BringToFront()
    End Sub
End Class
