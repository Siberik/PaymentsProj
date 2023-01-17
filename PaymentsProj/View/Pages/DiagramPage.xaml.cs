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
using Excel = Microsoft.Office.Interop.Excel;

namespace PaymentsProj.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для DiagramPage.xaml
    /// </summary>
    public partial class DiagramPage : Page
    {
        Core db = new Core();
        public DiagramPage()
        {
            InitializeComponent();

            ChartPayments.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true
            };
            ChartPayments.Series.Add(currentSeries);
            ComboUsers.ItemsSource = db.context.Users.ToList();
            ComboUsers.DisplayMemberPath = "last_name";
            ComboUsers.SelectedValuePath = "id_user";
            ComboChartTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var allUsers = db.context.Users.ToList().OrderBy(p => p.last_name).ToList();

            var application = new Excel.Application();
            application.Visible = true;



            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            application.SheetsInNewWorkbook = allUsers.Count();
            //Цикл перебирает листы книги
            for (int i = 0; i < allUsers.Count(); i++)
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
                //
                foreach (var groupCategory in usersCategories)
                {
                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[5][startRowIndex]];
                    headerRange.Merge();
                    headerRange.Value = groupCategory.Key.name_category;
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerRange.Font.Italic = true;

                    startRowIndex++;
                    //Цикл, поробегающий по оплате
                    foreach (var payment in groupCategory)
                    {
                        worksheet.Cells[1][startRowIndex] = payment.date_payment.ToString();

                        worksheet.Cells[2][startRowIndex] = payment.name;
                        worksheet.Cells[3][startRowIndex] = payment.price;
                        worksheet.Cells[4][startRowIndex] = payment.count;
                        worksheet.Cells[5][startRowIndex].Formula = $"=C{startRowIndex}*D{startRowIndex}";




                        startRowIndex++;

                    }
                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "ИТОГО: ";
                    sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    worksheet.Cells[5][startRowIndex].Formula = $"=SUM(E{startRowIndex - groupCategory.Count()}:" + $"E{startRowIndex - 1}";
                    sumRange.Font.Bold = worksheet.Cells[5][startRowIndex].Font.Bold = true;


                    startRowIndex++;

                    Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex - 1]];
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                    worksheet.Columns.AutoFit();

                }
            }
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            int idCurrentUser = Convert.ToInt32(ComboUsers.SelectedValue);
            if ( ComboChartTypes.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartPayments.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();
                var categoriesList = db.context.Category.ToList();
                foreach (var category in categoriesList)
                {
                    currentSeries.Points.AddXY(category.name_category, db.context.Payment.ToList().Where(p => p.user_id == idCurrentUser && p.Category == category).Sum(p => p.price * p.count));
                }

            }
        }
    }
}
