using DesktopContacts.classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();
            
            ReadDatabase();
        }

        private void btnNewContact_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        void ReadDatabase()
        {
            using (SQLite.SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();

                // Order by and where example in LINQ
                //var filteredList2 = from c2 in contacts
                //                    where c2.Name.ToLower().Contains(txtBoxSearch.Text.ToLower())
                //                    orderby c2.Name
                //                    select c2;

            }
            if (contacts != null)
            {
                // Have to clear each time ReadDatabase is called
                //foreach (var c in contacts)
                //{
                //    lstViewContacts.Items.Add(new ListViewItem()
                //    {

                //        Content = c
                //    });
                //}

                // Easier way.
                lstViewContacts.ItemsSource = contacts;
            }
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBoxSearch = sender as TextBox;

            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
            lstViewContacts.ItemsSource = filteredList;
        }
    }
}
