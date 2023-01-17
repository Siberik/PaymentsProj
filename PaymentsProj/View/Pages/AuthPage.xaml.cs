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
            
        }

      

        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            var loginUsers = db.context.Users.OrderBy(p => p.login).ToList();

            for (int i = 0; i < loginUsers.Count; i++)
            {
                UsersComboBox.Items.Add(loginUsers[i]);
            }


            



           }
            }
    }

