using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PaymentsProj.UsControls
{
    /// <summary>
    /// Логика взаимодействия для UserTextBox.xaml
    /// </summary>
    public partial class UserTextBox : UserControl
    {
        public UserTextBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        int count=0;
        
       


        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            count=Convert.ToInt32(CountTextBox.Text);
            count++;
            CountTextBox.Text=count.ToString();
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            count = Convert.ToInt32(CountTextBox.Text);
            count = count-1;
            CountTextBox.Text = count.ToString();
            
        }
    }
}
