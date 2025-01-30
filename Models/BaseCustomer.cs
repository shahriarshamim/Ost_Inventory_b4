using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ost_Inventory_b4.Models
{
    public class BaseCustomer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustAddress { get; set; }        

        public static DataTable ListCustomer() 
        {
            DataTable dataTable1 = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            try
            {
                sqlConnection.Open();
                string Query = "spOst_LstCustomer";
                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);    //fetch mode table data  
                adapter.Fill(dataTable1);
                cmd.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
            }
            return dataTable1;
        }
        public bool SaveCustomer()
        {
            bool status = false;
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            try
            {
                sqlConnection.Open();
                string Query = "spOST_InsCustomer";
                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@CustomerName", this.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", this.CustomerMobile));
                cmd.Parameters.Add(new SqlParameter("@CustAddress", this.CustAddress));              
                cmd.CommandTimeout = 0;

                int returnvalue = cmd.ExecuteNonQuery();//insert delete update
                if (returnvalue > 0)
                {
                    status = true;
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