using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Labb5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
       
        public bool IsUser = true;
        //Email-check(Regex)
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

        //Create user.
        private void CreateUser_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_RegularUser.Items.Contains(TextBox_CreateName.Text) || ListBox_AdminUser.Items.Contains(TextBox_CreateName.Text))
            {
                MessageBox.Show("The name already exists.", "Error");
                CreateUser_btn.IsEnabled = false;


            }
            else if (string.IsNullOrWhiteSpace(TextBox_CreateName.Text))
            {
                MessageBox.Show("Please enter a valid name.", "Error");
                CreateUser_btn.IsEnabled = false;

            }
            else if (TextBox_CreateName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid name.", "Error");
                CreateUser_btn.IsEnabled = false;
                
            }
            else
            {
                CreateUser_btn.IsEnabled = true;
                Thelist.Add(new User(TextBox_CreateName.Text, TextBox_CreateEmail.Text)
                {
                    Name = TextBox_CreateName.Text,
                    Email = TextBox_CreateEmail.Text
                });
                ListBox_RegularUser.Items.Add(TextBox_CreateName.Text);
                //Clear text-extry boxes
                TextBox_CreateName.Text = String.Empty;
                TextBox_CreateEmail.Text = string.Empty;
                CreateUser_btn.IsEnabled = false;
                InvalidEmailAdress.Visibility = Visibility.Visible;
            }
        }

        //Check if correct email.
        private void TextBox_CreateEmail_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox_CreateEmail.Opacity = 100;
            TextBox_CreateEmail.FontSize = 10;

            if (CheckMail(TextBox_CreateEmail.Text))
            {
                InvalidEmailAdress.Visibility = Visibility.Hidden;
                CreateUser_btn.IsEnabled = true;
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            
            if (IsUser)
            {
                int index = ListBox_RegularUser.SelectedIndex;
                if (!CheckMail(TextBox_CreateEmail.Text))
                {
                    InvalidEmailAdress.Visibility = Visibility.Visible;
                    MessageBox.Show("Plese enter a valid email adress!", "Error!");
                }
                else
                {
                    ListBox_RegularUser.Items.RemoveAt(index);
                    
                    Thelist.Add(new User(TextBox_CreateName.Text, TextBox_CreateEmail.Text)
                    {
                        Name = TextBox_CreateName.Text,
                        Email = TextBox_CreateEmail.Text
                    });
                    ListBox_RegularUser.Items.Add(TextBox_CreateName.Text);
                    TextBox_CreateName.Text = String.Empty;
                    TextBox_CreateEmail.Text = string.Empty;
                }
            }
            else if (IsUser == false)
            {
                int index = ListBox_AdminUser.SelectedIndex;
                if (!CheckMail(TextBox_CreateEmail.Text))
                {
                    InvalidEmailAdress.Visibility = Visibility.Visible;
                    MessageBox.Show("Plese enter a valid email adress!", "Error!");
                }
                else
                {
                    ListBox_AdminUser.Items.RemoveAt(index);
                    Thelist.Add(new User(TextBox_CreateName.Text, TextBox_CreateEmail.Text)
                    {
                        Name = TextBox_CreateName.Text,
                        Email = TextBox_CreateEmail.Text
                    });
                    ListBox_AdminUser.Items.Add(TextBox_CreateName.Text);
                    TextBox_CreateName.Text = String.Empty;
                    TextBox_CreateEmail.Text = string.Empty;
                }

            }

        }

        private void ListBox_RegularUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (ListBox_RegularUser.SelectedIndex != -1 || ListBox_AdminUser.SelectedIndex != -1)
            {
                RemoveUser_btn.IsEnabled = true;
                MoveTo_adminBtn.IsEnabled = true;
                UserInfo.Visibility = Visibility.Visible;
                EditUser.IsEnabled = true;
                object pos = ListBox_RegularUser.SelectedItems;

                foreach (var c in Thelist)

                {
                    UserInfo.Content = c.Email;
                }


                
                

                Console.ReadLine();

                







            }
            else
            {
                RemoveUser_btn.IsEnabled = false;
                MoveTo_adminBtn.IsEnabled = false;
                UserInfo.Visibility = Visibility.Hidden;
                EditUser.IsEnabled = false;
            }
            
        }

        private void MoveTo_adminBtn_Click(object sender, RoutedEventArgs e)
        {
          
                ListBox_AdminUser.Items.Add(ListBox_RegularUser.SelectedItem);
                IsUser = false;
                ListBox_RegularUser.Items.Remove(ListBox_RegularUser.SelectedItem);
            
        }

        private void MoveToUser_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_RegularUser.SelectedIndex != -1 || ListBox_AdminUser.SelectedIndex != -1)
            {
                ListBox_RegularUser.Items.Add(ListBox_AdminUser.SelectedItem);
                IsUser = true;
                ListBox_AdminUser.Items.Remove(ListBox_AdminUser.SelectedItem);
            }
        }

        private void ListBox_AdminUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ListBox_RegularUser.SelectedIndex != -1 || ListBox_AdminUser.SelectedIndex != -1)
            {
                RemoveUser_btn.IsEnabled = true;
                MoveToUser_btn.IsEnabled = true;
                AdminUserInfo_label.Visibility = Visibility.Visible;
                EditUser.IsEnabled = true;
                //int pos = ListBox_RegularUser.SelectedIndex;
                try
                {

                   
                 //   UserInfo.Content = Thelist.ElementAt(pos);
                }

                catch { }

            }
            else
            {
                MoveToUser_btn.IsEnabled = false;
                RemoveUser_btn.IsEnabled = false;
                MoveTo_adminBtn.IsEnabled = false;
                AdminUserInfo_label.Visibility = Visibility.Hidden;
                EditUser.IsEnabled = false;
            }
        }

        private void RemoveUser_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_RegularUser.SelectedIndex != -1 || ListBox_AdminUser.SelectedIndex != -1)
            {
               
                if (IsUser)
                {
                    int index = ListBox_RegularUser.SelectedIndex;
                    ListBox_RegularUser.Items.RemoveAt(index);
                    Thelist.RemoveAt(index);
                }
                else if (IsUser == false)
                {
                    try
                    {
                        int index = ListBox_AdminUser.SelectedIndex;
                        ListBox_AdminUser.Items.RemoveAt(index);
                        Thelist.RemoveAt(index);
                    }
                    catch
                    {
                    }
                }
            }
           

        }


    }
    }