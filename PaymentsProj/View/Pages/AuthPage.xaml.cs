using Microsoft.VisualBasic.ApplicationServices;
using PaymentsProj.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaymentsProj.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    
    public partial class AuthPage : Page
    {
        Core db = new Core();
        public AuthPage()
        {
            InitializeComponent();
            UsersComboBox.ItemsSource = db.context.Users.ToList();
            UsersComboBox.DisplayMemberPath = "login";
            
        }

      

        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
          
           

            var selectPassword = UserPasswordBox.Password;
            Users currentUser = db.context.Users.Where(x => x.login == UsersComboBox.Text).Where(x =>x.password==selectPassword).FirstOrDefault();
            if (currentUser != null) {
                App.CurrentUser = currentUser;
                this.NavigationService.Navigate(new DiagramPage());
            }
            if (currentUser == null)
            {
                MessageBox.Show("Неправильный пароль");
            }
        }
       }
    }

