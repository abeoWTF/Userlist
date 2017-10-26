using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Labb5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Checkar mailadress.
        public bool CheckMail(string emailText)
        {
            Match matches = Regex.Match(emailText, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                   + "@"
                                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (matches.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Nyskapad lista av Users
        List<User> Thelist = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        //Checka och lägga till user.
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var item = Thelist.FirstOrDefault(o => o.Name == txtName.Text);
            if (item != null)
            {
                MessageBox.Show("The name already exists.", "Error");
                btnAddUser.IsEnabled = false;
            }
            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a valid name.", "Error");
                btnAddUser.IsEnabled = false;
            }
            else if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid name.", "Error");
                btnAddUser.IsEnabled = false;
            }
            else
            {
                btnAddUser.IsEnabled = true;
                Thelist.Add(new User(txtName.Text, txtEMail.Text)
                {
                    Name = txtName.Text,
                    EMail = txtEMail.Text
                });
               
                lstUsers.Items.Add(Thelist.LastOrDefault());
                btnAddUser.IsEnabled = false;
            }

            //ta bort text från textboxarna
            ClearTextBoxes();
        }
        
        //Uppdatera user
        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex != -1)
            {
                int index = lstUsers.SelectedIndex;
                if (!CheckMail(txtEMail.Text))
                {
                    MessageBox.Show("Plese enter a valid email adress!", "Error!");
                }
                else
                {
                    lstUsers.Items.RemoveAt(index);

                    Thelist.Add(new User(txtName.Text, txtEMail.Text)
                    {
                        Name = txtName.Text,
                        EMail = txtEMail.Text
                    });
                    lstUsers.Items.Add(Thelist.LastOrDefault());

                    //ta bort text från textboxarna
                    ClearTextBoxes();
                }
            }
            else if (lstAdmins.SelectedIndex != -1)
            {
                int index = lstAdmins.SelectedIndex;
                if (!CheckMail(txtEMail.Text))
                {
                    MessageBox.Show("Plese enter a valid email adress!", "Error!");
                }
                else
                {
                    lstAdmins.Items.RemoveAt(index);
                    Thelist.Add(new User(txtName.Text, txtEMail.Text)
                    {
                        Name = txtName.Text,
                        EMail = txtEMail.Text
                    });
                    lstAdmins.Items.Add(Thelist.LastOrDefault());

                    //ta bort text från textboxarna
                    ClearTextBoxes();
                }

            }

            //ta bort text från textboxarna
            ClearTextBoxes();
        }

       //sätter enable tll av eller på beroende på om item är valt i user-listboxen för tre stycken knappar
        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                btnUpdateUser.IsEnabled = true;
                btnDeleteUser.IsEnabled = true;
                btnBecomeAdmin.IsEnabled = true;
                lblFullUserInfo.Visibility = Visibility.Visible;
            }
            else if(lstUsers.SelectedItem == null)
            {
                btnDeleteUser.IsEnabled = false;
                btnBecomeAdmin.IsEnabled = false;
                lblFullUserInfo.Visibility = Visibility.Hidden;
            }
            
            object pos = lstUsers.SelectedItem;
            try
            {
                lblFullUserInfo.Content = pos.ToString();
            }
            catch { }

        }

        //sätter enable till av eller på beroende på om item är valt i admin-listboxen för BecomeUser-knappen
        private void lstAdmins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAdmins.SelectedItem != null) { 
                btnBecomeUser.IsEnabled = true;
                btnUpdateUser.IsEnabled = true;
                btnDeleteUser.IsEnabled = true;

                lblFullAdminInfo.Visibility = Visibility.Visible;
            }
        else if (lstAdmins.SelectedItem == null)
            {
                lblFullAdminInfo.Visibility = Visibility.Hidden;
                btnBecomeUser.IsEnabled = false;
                btnDeleteUser.IsEnabled = false;
            }
            
            object pos = lstAdmins.SelectedItem;
            try
            {
                lblFullAdminInfo.Content = pos.ToString();
            }
            catch { }
        }
        // Tar bort User från listan av users och listbox
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                try { 
                var item = Thelist.LastOrDefault(x => txtName.Text == Name);
                    if (item != null)
                    {
                        Thelist.Remove(item);
                    }
                    lstUsers.Items.Remove(lstUsers.SelectedItem);
                }
                catch { }
            }

            else if (lstAdmins.SelectedItem != null)
            {
                try
                {
                    var item = Thelist.LastOrDefault(x => txtName.Text == Name);
                    if (item != null)
                    {
                        Thelist.Remove(item);
                    }
                    lstAdmins.Items.Remove(lstAdmins.SelectedItem);
                }
                catch { }
            }
        }

        //Omvandla user till admin
        private void btnBecomeAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                lstAdmins.Items.Add(lstUsers.SelectedItem);
                lstUsers.Items.Remove(lstUsers.SelectedItem);
            }
            
        }
        //Omvandla till user
        private void btnBecomeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdmins.SelectedItem != null)
            {
                lstUsers.Items.Add(lstAdmins.SelectedItem);
                lstAdmins.Items.Remove(lstAdmins.SelectedItem);
            }
 
        }
        //rensar textboxarna från default-text
        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
           txtName.Clear();
        }

        private void txtEMail_GotFocus(object sender, RoutedEventArgs e)
        {
           txtEMail.Clear();
        }

        //hjälpmetod för rensning av textboxar 
        public void ClearTextBoxes()
        {
            txtName.Clear();
            txtEMail.Clear();
        }


        //Slår på add-user om mail-adressen är korrekt ifylld. (regex)
        private void txtEMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckMail(txtEMail.Text))
            {
                btnAddUser.IsEnabled = true;
            }
        }
    }  // MainWindow class ends here

    public class User
    {
        public string Name { get; set; }
        public  string EMail { get; set; }

        public User(string name, string email)
        {
            this.Name= name;
            this.EMail = email;
        }
        
        //overtide för korrekt info
        public override string ToString()
        {
            return String.Format("Name: {0}\n@: {1}", this.Name, this.EMail);
        }
    }
} //EOF
