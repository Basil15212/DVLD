using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.Drivers
{
    public  class clsDriverData
    {


        public static bool GetDriverInfoByID(int DriverID , ref int PersonID ,ref int CreatedByUSerID ,ref DateTime CreateDate)
        {
            bool IsFound = false;
            //int PersonId = -1, CreatedByUserID = -1; DateTime CreateDate = DateTime.Now;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Qury = @"Select * from Drivers where DriverID = @DriverID;";

            SqlCommand cmd = new SqlCommand(Qury, con);
            cmd.Parameters.AddWithValue("@DriverID" , DriverID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    CreateDate = (DateTime)reader["CreatedDate"];
                    CreatedByUSerID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                IsFound = false;
            }
            finally { con.Close(); }
            return IsFound;


        }

        public static  bool GetDriverByPersonID(int PErsonID ,ref int DriverID , ref int CreatedByPersonID ,ref DateTime CreatedTime)
        {
            bool IsFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Qury = @"Select * from Drivers where PersonID = @PersonID;";
            SqlCommand cmd = new SqlCommand(Qury, con);
            cmd.Parameters.AddWithValue("@PersonID", PErsonID);

            try
            {

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    DriverID = (int)reader["DriverID"];
                    CreatedTime = (DateTime)reader["CreatedDate"];
                    CreatedByPersonID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                IsFound = false;
            }
            finally { con.Close(); }
            return IsFound;


        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"SELECT * FROM Drivers_View order by FullName";

            SqlCommand cmd = new SqlCommand(qury, con);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch {  }   finally { con.Close(); }
            return dt;

        }

        public static int AddNewDRiver(int PersonID ,int CreatedByUserID)
        {
            int DriverID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"INSERT INTO Drivers 
                            (PersonID ,CreatedByUserID ,CreatedDate)
                            Values 
                            (@PersonID, @CreatedByUserID ,@CreatedDate);
                            
                                    SELECT SCOPE_IDENTITY();";

            
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString() ,out int NewID))
                {
                    DriverID = NewID;
                }
            }
            catch { DriverID = -1; }
            finally { con.Close(); }
            return DriverID;

        }

        public static bool UpdateDriver(int DriverID ,int PErosnID ,int CreatedByUserID)
        {
            int affectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"UPDATE Drivers 
                            SET 
                            PersonID =@PersonID , CreatedByUserID =@CreatedByUserID 

                            where DriverID =@DriverID";

            SqlCommand cmd = new SqlCommand(query ,con);
            cmd.Parameters.AddWithValue("@PersonID", PErosnID);
            cmd.Parameters.AddWithValue("@CreatedByUserID" , CreatedByUserID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                con.Open();
                affectedRows = cmd.ExecuteNonQuery();

            }
            catch { return false; }
            finally { con.Close(); }
            return affectedRows > 0;
            


        }


    }
}
