using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBLibrary;
using System.IO;
using Path = System.IO.Path;

namespace _2C
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+strWorkPath+ @"\Database1.mdf;Integrated Security=True";
            DBLibrary.Connection.SetConnection(connectionString);
            dataGrid1.ItemsSource = DBLibrary.DB.GetData("select * from Books").DefaultView;
            
        }
        
        SqlConnection myConnection = new SqlConnection();
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DBLibrary.DB.ExecuteCmd("alter table [Books] add [ProductId] int default 0 NOT NULL");
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DBLibrary.DB.ExecuteCmd("insert into Books (Title, Barcode, ExtraInfo) values ('"+Title.Text.ToString()+"','"+Barcode.Text.ToString()+"','"+ExtraInfo.Text.ToString()+"')");
            DBLibrary.SPFunc.DGFill("Books", dataGrid1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                var cellInfo = dataGrid1.SelectedCells[0];
                var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                DBLibrary.DB.ExecuteCmd("delete from Books where BookId='" + content.ToString() + "'") ;
                DBLibrary.SPFunc.DGFill("Books", dataGrid1);
            }
            catch {
                MessageBox.Show("Выберите что нибудь");
            }
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                Title.Text = (dataGrid1.SelectedCells[1].Column.GetCellContent(dataGrid1.SelectedCells[1].Item) as TextBlock).Text.ToString();
                Barcode.Text = (dataGrid1.SelectedCells[2].Column.GetCellContent(dataGrid1.SelectedCells[2].Item) as TextBlock).Text.ToString();
                ExtraInfo.Text = (dataGrid1.SelectedCells[3].Column.GetCellContent(dataGrid1.SelectedCells[3].Item) as TextBlock).Text.ToString();
               
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                string str = (dataGrid1.SelectedCells[0].Column.GetCellContent(dataGrid1.SelectedCells[0].Item) as TextBlock).Text.ToString();
                DBLibrary.DB.ExecuteCmd("update Books set Title='" + Title.Text.ToString() + "', Barcode='" + Barcode.Text.ToString() + "', ExtraInfo='" + ExtraInfo.Text.ToString() + "' where BookId=" + str);
                DBLibrary.SPFunc.DGFill("Books", dataGrid1);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DBLibrary.SPFunc.DGFill("Books",dataGrid3) ;

        }
    }
}
