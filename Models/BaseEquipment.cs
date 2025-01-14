using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ost_Inventory_b4.Models
{
    public class BaseEquipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReceiveDate { get; set; }

        public List<BaseEquipment> LstEquipment()
        {
            List<BaseEquipment> lstEquips = new List<BaseEquipment>();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            try
            {
                sqlConnection.Open();
                string Query = "spOST_LstEquipment";
                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;

                //cmd.ExecuteNonQuery();//insert delete update 
                SqlDataReader reader = cmd.ExecuteReader(); //fetch mode list data 
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BaseEquipment baseEquipment = new BaseEquipment();
                        baseEquipment.EquipmentId = Convert.ToInt32(reader["EquipmentId"].ToString());
                        baseEquipment.EquipmentName = reader["EquipmentName"].ToString();
                        baseEquipment.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                        baseEquipment.Stock = Convert.ToInt32(reader["Stock"].ToString());
                        baseEquipment.EntryDate = Convert.ToDateTime(reader["EntryDate"].ToString());
                        baseEquipment.ReceiveDate = Convert.ToDateTime(reader["ReceiveDate"].ToString());
                        lstEquips.Add(baseEquipment);
                    }
                }

                //transaction
                reader.Close();
                cmd.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
            }
            return lstEquips;
        }
        public static DataTable ListCustomerAssigned()
        {
            DataTable dataTable1 = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            try
            {
                sqlConnection.Open();
                string Query = "spOst_LstCustomerEquiAssignment";
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
    }
}