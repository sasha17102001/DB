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
                cmd.CommandText = "update "+TableName+ "set "+cmdStr+" where &Identity=" + ID;
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
        //получить количество записей таблицы указанной в качестве аргумента
        public static int getCountOfRecords(string TableName)
        {
            int count = 0;
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT COUNT(*) FROM "+TableName;
                count = (int)cmd.ExecuteScalar();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return count;
        }
        //получить количество записей таблицы указанной в качестве аргумента
        public static int getNumberOfCulumns(string TableName)
        {
            int count = 0;
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.Columns where TABLE_NAME='" + TableName+"'"; 
                count = (int)cmd.ExecuteScalar();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return count;
        }
        //получить имена полей таблицы указанной в качестве аргумента, в виде массива
        public static string[] getColumnsName(string TableName)
        {
            List<string> listacolumnas = new List<string>();
            using (SqlConnection con = new SqlConnection(Connection.GetConnection()))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '"+TableName+"' and t.type = 'U'";
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listacolumnas.Add(reader.GetString(0));
                    }
                }
            }
            return listacolumnas.ToArray();
        }
        //получить тип поля таблицы указанной в качестве аргумента, в виде массива
        public static Type getColumnType(string TableName,int c)
        {
           
            Type type = null;
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM ["+TableName+"]";
                SqlDataReader rdr = cmd.ExecuteReader();
                type = rdr.GetFieldType(c);
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return type;
        }
        //добавить поле в таблицу
        public static void AddColumn(string TableName, string ColumnName, string DataType)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "ALTER TABLE "+ TableName+" ADD "+ColumnName+" "+DataType ;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //удалить поле из таблицы
        public static void DeleteColumn(string TableName, string ColumnName)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "ALTER TABLE " + TableName + " DROP COLUMN " + ColumnName;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //изменить тип данных поля
        public static void ModifyColumn(string TableName, string ColumnName, string DataType)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "ALTER TABLE " + TableName + " ADD " + ColumnName + " " + DataType;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void TableCreate(string TableName, string ColumnName_DataType)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.GetConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "CREATE TABLE "+TableName+ "("+ ColumnName_DataType + ")";
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


    public class SPFunc
    {

        //загрузить в datagrid таблицу 
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
        //загрузить в combobox названия таблиц 
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
