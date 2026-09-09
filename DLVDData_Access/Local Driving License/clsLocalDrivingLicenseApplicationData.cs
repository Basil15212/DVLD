using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.Local_Driving_License
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static bool GetLocalDrivingLicenseApplicationInfoByID
            (int LocalDrivingLicenseApplicationID ,ref int ApplicationID ,ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
                            where 
                                LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID";
            SqlCommand cmd = new SqlCommand(query, con );
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                }
                reader.Close();
            }
            catch(Exception ex) {  Console.WriteLine(ex.Message); return false; }
            finally { con.Close(); }
            return IsFound;
        }

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID
                (int ApplicationID ,ref int LocalDrivingLicenseApplicationID ,ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select * from LocalDrivingLicenseApplications
                                where
                                ApplicationID =@ApplicationID";
            SqlCommand cmd = new SqlCommand(query, con );
            cmd.Parameters.AddWithValue("@ApplicationID" , ApplicationID);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }

            finally{ con.Close(); }
            return IsFound;
                
        }

        public static DataTable GetAllLocalDrivingLicensApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications_View
                            order by ApplicationDate desc";

            SqlCommand cmd = new SqlCommand (query, con );
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message ); return null;
            }
            finally{ con.Close(); }
            return dt;
        }

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID , int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"INSERT INTO LocalDrivingLicenseApplications ( 
                            ApplicationID,LicenseClassID)
                             VALUES (@ApplicationID,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString() ,out int NewID))
                {
                    LocalDrivingLicenseApplicationID = NewID;
                }

            }catch(Exception ex) { Console.WriteLine(ex.Message);return -1;}
            finally{ connection.Close(); }
            return LocalDrivingLicenseApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID ,int LicenseClassID)
        {
            int AffectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update  LocalDrivingLicenseApplications  
                            set ApplicationID = @ApplicationID,
                                LicenseClassID = @LicenseClassID
                            where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();

            }catch(Exception ex) { Console.WriteLine(ex.Message); return false; }
            finally{ con.Close(); }
            return AffectedRows > 0;
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID )
        {
            int AffectedRows = 0;
            SqlConnection con =new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Delete LocalDrivingLicenseApplications 
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ";
            SqlCommand cmd = new SqlCommand(@query, con);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); return false; }
            finally{ con.Close(); }
            return AffectedRows > 0;
        }


        //Some Methouds Wiil Be Added Later on 


    }
}
