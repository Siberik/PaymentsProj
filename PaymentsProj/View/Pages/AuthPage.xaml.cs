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
            var allUsers = db.context.Users.ToList().OrderBy(p => p.last_name).ToList();

         

            for (int i = 0; i < allUsers.Count(); i++)
            {

                UsersComboBox.Items.Add(allUsers[i]);
            }
            }
    }
}
