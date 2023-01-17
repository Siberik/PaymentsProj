using Microsoft.VisualBasic.ApplicationServices;
using PaymentsProj.Model;
using PaymentsProj.View.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace PaymentsProj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Core db = new Core();
        public MainWindow()
        {
            

            InitializeComponent();
             
         
            MainFrame.Navigate(new AuthPage());
        }

        
    }
}
