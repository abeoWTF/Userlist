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
        List<User> Thelist = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
         
            if (lstUsers.Items.Contains(txtName.Text) || lstUsers.Items.Contains(txtName.Text))
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
                Thelist.Add(new User(txtEMail.Text, txtEMail.Text)
                {
                    Name = txtName.Text,
                    EMail = txtEMail.Text
                });

                lstUsers.Items.Add(Thelist.LastOrDefault());
                btnAddUser.IsEnabled = false;
                InvalidEmailAdress.Visibility = Visibility.Visible;
            }


            //ta bort text från textboxarna
            ClearTextBoxes();

            //sortera listan alfabetiskt efter förändring i user-listboxen
            //SortUserListBox();


        }
        

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex != -1)
            {
                int index = lstUsers.SelectedIndex;
                if (!CheckMail(txtEMail.Text))
                {
                    InvalidEmailAdress.Visibility = Visibility.Visible;
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

                    //sortera listan alfabetiskt efter förändring i user-listboxen
                    //SortUserListBox();

                }
            }
            else if (lstAdmins.SelectedIndex != -1)
            {
                int index = lstAdmins.SelectedIndex;
                if (!CheckMail(txtEMail.Text))
                {
                    InvalidEmailAdress.Visibility = Visibility.Visible;
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

                    //sortera listan alfabetiskt efter förändring i user-listboxen
                    //SortUserListBox();
                }

            }

            //ta bort text från textboxarna
            ClearTextBoxes();

            //sortera listan alfabetiskt efter uppdatering i user-listboxen
            //SortUserListBox();

        }

       //sägger enable tll av eller på beroende på om item är valt i user-listboxen för tre stycken knappar
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




        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            lstUsers.Items.Remove(lstUsers.SelectedItem);

            else if(lstAdmins != null)
                lstAdmins.Items.Remove(lstAdmins.SelectedItem);


        }

        private void btnBecomeAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                
                lstAdmins.Items.Add(lstUsers.SelectedItem);
                lstUsers.Items.Remove(lstUsers.SelectedItem);
            }

            //sortera i adminlistan efter element har lags till där från user-listan
            //SortAdminListBox();


        }

        private void btnBecomeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdmins.SelectedItem != null)
            {
                lstUsers.Items.Add(lstAdmins.SelectedItem);
                lstAdmins.Items.Remove(lstAdmins.SelectedItem);
            }

            //sortera i userlistan efter element har lagts tlll där från admin-listan
            //SortUserListBox();
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



        //hjälpmetoder för rensning av textboxar samt sortering av listbox
        public void ClearTextBoxes()
        {
            txtName.Clear();
            txtEMail.Clear();
        }

        //public void SortUserListBox()
        //{
        //    lstUsers.Items.SortDescriptions.Add(
        //        new System.ComponentModel.SortDescription("",
        //            System.ComponentModel.ListSortDirection.Ascending));


        //}

        //public void SortAdminListBox()
        //{
        //    lstAdmins.Items.SortDescriptions.Add(
        //        new System.ComponentModel.SortDescription("",
        //            System.ComponentModel.ListSortDirection.Ascending));
        //}

        private void txtEMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckMail(txtEMail.Text))
            {
                InvalidEmailAdress.Visibility = Visibility.Hidden;
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


        public override string ToString()
        {

            return String.Format("Email: {0}", this.EMail);

        }

    }





    


} //EOF
