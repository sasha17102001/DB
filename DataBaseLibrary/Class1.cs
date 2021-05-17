using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DBLibrary
{
    public  class DB
    {
       
        //получение таблицы в виде DataTable
        public static DataTable GetData(string cmdStr)
        {
            DataTable dt = new DataTable("DataTable");
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                SqlDataAdapter dataAdp = new SqlDataAdapter(cmd);
                dataAdp.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        //удаление элемента
        public static void DeleteById(string TableName, string ID, string value)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from " + TableName + " where " + ID + "=" + value;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //добавление данных в таблицу
        public static void InsertIn(string TableName, string ColumnNames, string InsertInfo)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into " + TableName + "(" + ColumnNames + ") values (" + InsertInfo + ")";
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //изменение данных в таблице
        public static void UpdateById(string TableName, string ID, string cmdStr)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update "+TableName+ "set "+cmdStr+" where BookId=" + ID;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //выполнить команду указанную в качестве аргумента
        public static void ExecuteCmd(string cmdStr)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = cmdStr;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void TableCreate()
        {
            throw new System.NotImplementedException();
        }
    }
    public class SPFunc
    {

        //загрузить в датагрид таблицу 
        public static void DGFill(string TableName, DataGrid dataGrid1)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                string cmdStr = "SELECT * FROM " + TableName;
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                SqlDataAdapter dataAdp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(TableName);
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //загрузить в комбобокс названия таблиц (доработать)
        public static void ComboFill(ComboBox ComboBox1)
        {
 
            try
            {
                DataTable dt = new DataTable();
                string cmdStr = "select * from sys.tables";
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmdStr, Connection.GetConnection());
                sda.Fill(dt);
               
                foreach (DataRow row in dt.Rows)
                {
                    ComboBox1.Items.Add(row["name"]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
    public class Connection
    {
        
        private static string conStr;

        public static void SetConnection(string ConStr)
        {
            conStr = ConStr;
        }

        public static string GetConnection()
        {
            return conStr;
        }

    }


}
