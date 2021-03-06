using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Windows.Forms;

namespace Call_AGV
{
    class MySQL_dosomething
    {
        private static MySqlConnection SQL_Connection;
        public static string pwd = "12345678";
        public string error_message;
        public string StrCon = "Server = 127.0.0.1; UId = root; Pwd = " + pwd + "; Pooling = false; Character Set=utf8";
        public void DataTable_2_Database(DataTable dt,string table_name,string database) 
        {
            try
            {
                string cmd1 = "DROP TABLE IF EXISTS " + table_name + "";
                string cmd2 = @"CREATE TABLE " + table_name + " (ID VARCHAR(50), X TEXT, Y TEXT, RUNSTOP TEXT, LR TEXT, SPEED TEXT, DIR TEXT, LIFT TEXT)";
                
                bool kq1 = SQL_command(cmd1, database);
                bool kq2 = SQL_command(cmd2, database);
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    string data = dt.Rows[i][0].ToString();
                    string query = "INSERT INTO " + table_name + " (ID) VALUES('" + data + "')";
                    bool rs1 = SQL_command(query, database);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    string query = "UPDATE " + table_name + " SET `X`= '" + dt.Rows[i][1].ToString() + "'," +
                        "`Y`= '" + dt.Rows[i][2].ToString() + "'," +
                        "`RUNSTOP`= '" + dt.Rows[i][3].ToString() + "'," +
                        "`LR`= '" + dt.Rows[i][4].ToString() + "'," +
                        "`SPEED`= '" + dt.Rows[i][5].ToString() + "'," +
                        "`DIR`= '" + dt.Rows[i][6].ToString() + "'," +
                        "`LIFT`= '" + dt.Rows[i][7].ToString() + "'" +
                        " WHERE ID = '" + dt.Rows[i][0].ToString() + "'";
                    bool b2 = SQL_command(query, database);
                }
              
            }
            catch (Exception ex)
            {

                error_message = ex.Message;
            }
            
        }
        public DataTable Get_Database_Name()
        {
            DataTable dt = new DataTable();
            try
            {

                SQL_Connection = new MySqlConnection(StrCon);
                SQL_Connection.Open();
                dt = SQL_Connection.GetSchema("Databases");
                error_message = string.Empty;
                SQL_Connection.Close();
                return dt;

            }
            catch (Exception ex)
            {

                error_message = ex.Message;
                return dt;
            }

        }
        public List<string> Get_table_Name(string datebase)
        {
            DataTable dt = new DataTable();
            List<string> listName = new List<string>();
            try
            {

                using (SQL_Connection = new MySqlConnection(StrCon))
                {
                    SQL_Connection.Open();
                    //using (MySqlCommand com = new MySqlCommand(" SELECT * FROM sys.tables where table_schema = 'agv_data'", con))
                    using (MySqlCommand com = new MySqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'AND TABLE_SCHEMA='" + datebase + "' ", SQL_Connection))
                    {

                        using (MySqlDataReader reader = com.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                listName.Add((string)reader["TABLE_NAME"]);
                            }
                        }
                    }
                    error_message = string.Empty;
                    return listName;

                }
            }
            catch (Exception ex)
            {

                error_message = ex.Message;
                return listName;
            }

        }
        public int count = 0;
        public void Fill_data(string database, string table, ref DataGridView dataGridView)
        {
            try
            {
                using (SQL_Connection = new MySqlConnection("Server = 127.0.0.1;Database =" + database + "; UId = root; Pwd = " + pwd + "; Pooling = false; Character Set=utf8"))
                {
                    string str = "SELECT * FROM " + table + "";
                    MySqlDataAdapter adp = new MySqlDataAdapter(str, SQL_Connection);
                    MySqlCommandBuilder cmd = new MySqlCommandBuilder(adp);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    dataGridView.DataSource = dt;

                    error_message = string.Empty;
                }
            }
            catch (Exception ex)
            {

                error_message = ex.Message;
            }
        }
        public DataTable Read_data(string database, string table)
        {
            DataTable dataTable = new DataTable();
            try
            {

                using (SQL_Connection = new MySqlConnection("Server = 127.0.0.1;Database =" + database + "; UId = root; Pwd = " + pwd + "; Pooling = false; Character Set=utf8"))
                {
                    SQL_Connection.Open();
                    int i = 0;
                    string str = "SELECT * FROM " + table + "";

                    MySqlCommand cmd = new MySqlCommand(str, SQL_Connection);
                    var SQL_Reader = cmd.ExecuteReader();
                    if (SQL_Reader.HasRows)
                    {
                        while (SQL_Reader.Read())
                        {
                            dataTable.Rows.Add();
                            for (int j = 0; j < SQL_Reader.FieldCount; j++)
                            {
                                if (SQL_Reader.IsDBNull(j)) break;
                                dataTable.Columns.Add();
                                dataTable.Rows[i][j] = SQL_Reader.GetString(j);

                            }
                            i++;

                        }
                    }
                    SQL_Connection.Close();
                    error_message = string.Empty;
                    return dataTable;
                }

            }
            catch (Exception ex)
            {
                error_message = ex.Message;
                return dataTable;
            }
        }
        public bool SQL_command(string command, string database)
        {
            try
            {
                SQL_Connection = new MySqlConnection("Server = 127.0.0.1;Database =" + database + "; UId = root; Pwd = " + pwd + "; Pooling = false; Character Set=utf8");
                SQL_Connection.Open();
                MySqlCommand cmd = new MySqlCommand(command, SQL_Connection);

                cmd.ExecuteNonQuery();
                SQL_Connection.Close();
                error_message = string.Empty;
                return true;


            }
            catch (Exception ex)
            {

                error_message = ex.Message;
                return false;
            }

        }
    }
}
