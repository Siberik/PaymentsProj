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
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var allUsers = db.context.Users.ToList().OrderBy(p => p.last_name).ToList();

            var application = new Excel.Application();
            application.Visible = true;

            application.SheetsInNewWorkbook = allUsers.Count();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            for(int i =0; i< allUsers.Count();i++)
            {
                int startRowIndex = 1;
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = allUsers[i].last_name;

                worksheet.Cells[1][startRowIndex] = "Дата платежа";
                worksheet.Cells[2][startRowIndex] = "Название";
                worksheet.Cells[3][startRowIndex] = "Стоимость";
                worksheet.Cells[4][startRowIndex] = "Количество";
                worksheet.Cells[5][startRowIndex] = "Сумма";

                startRowIndex++;

                var usersCategories = allUsers[i].Payment.OrderBy(p => p.date_payment).GroupBy(p => p.Category).OrderBy(p => p.Key.name_category);


            }
        }
    }
}
