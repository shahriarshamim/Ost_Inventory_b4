using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ost_Inventory_b4.Models
{
    public class BaseAccount
    {
        public bool VerifyUser(string UserName, string Password)
        {
            bool status = false;
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            try
            {
                sqlConnection.Open();
                string Query = "select * from OSTMember where Name='"+UserName+"' and Password='"+Password+"'";
                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;

                //cmd.EndExecuteNonQuery();//insert delete update
                DataTable dataTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);//fetch more table data
                adapter.Fill(dataTable);
                //SqlDataReader reader = cmd.ExecuteReader();//fetch mode list data
                if(dataTable.Rows.Count>0)
                {
                    status=true;
                }
                //transaction
                cmd.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
            }
            return status;
        }
    }
}