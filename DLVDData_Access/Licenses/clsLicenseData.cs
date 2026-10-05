using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.Licenses
{
    public class clsLicenseData
    {

        public static bool GetLicenseInfoByID(int LicenseID ,ref int ApplicationID ,ref int DriverID ,ref int LicenseClass,
                            ref DateTime IssueDate ,ref DateTime ExpirationDate ,ref string Notes ,ref decimal PaidFees,
                             ref bool IsActive ,ref byte IssueReason ,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select * from Licenses where LicenseID =@LicenseID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    //Notes ....
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }


                }
                else
                    isFound = false;

                reader.Close();
            }
            catch { return false; }
            finally { con.Close(); }
            return isFound;

        }

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select * from Licenses";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch {}
            finally { con.Close(); }
            return dt;
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select  
                            Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                            from Licenses join LicenseClasses 
                            ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where  where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand cmd = new SqlCommand(@query, con);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch { }
            finally { con.Close(); }
            return dt;
        }

        public static int AddNewLicense(int ApplicationID ,int DriverID ,int LicenseClass , DateTime IssueDate ,DateTime ExpirationDate,
                                        string Notes ,decimal PaidFees ,bool IsActive ,byte IssueReason ,int CreatedByUserID)
        {

            int LicenseID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Insert into Licenses
                            (ApplicationID ,DriverID ,LicenseClass,IssueDate ,ExpirationDate ,Notes, 
                                PaidFees ,IsActive ,IssueReason ,CreatedByUserID)
                            Values 
                            (@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,
                            @PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                              SELECT SCOPE_IDENTITY();  ";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ApplicationID",ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID",DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass",LicenseClass);
            cmd.Parameters.AddWithValue("@IssueDate",IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate",ExpirationDate);
            cmd.Parameters.AddWithValue("@PaidFees",PaidFees);
            cmd.Parameters.AddWithValue("@IsActive",IsActive);
            cmd.Parameters.AddWithValue("@IssueReason",IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            if(Notes == null || Notes== string.Empty)
            {
                cmd.Parameters.AddWithValue("@Notes",DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Notes",Notes);
            }

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(),out int NewID))
                {
                    LicenseID = NewID;
                }

            }
            catch { }
            finally { con.Close(); }
            return LicenseID;
            
        }

        public static bool UpdateLicense(int LicenseID , int ApplicationID, int DriverID, int LicenseClass,
                                        DateTime IssueDate, DateTime ExpirationDate,
                                        string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int affectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update Licenses
                            ApplicationID =@ApplicationID ,
                            DriverID =@DriverID ,
                            LicenseClass =@LicenseClass,
                            IssueDate =@IssueDate ,
                            ExpirationDate =@ExpirationDate,
                            Notes =@Notes ,
                            PaidFees =@PaidFees ,
                            IsActive =@IsActive ,
                            IssueReason =@IssueReason,
                            CreatedByUserID =@CreatedByUserID
                            where LicenseID =@LicenseID;";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            if(Notes == "")
            {
                cmd.Parameters.AddWithValue("@Notes",DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Notes",Notes);
            }

            try
            {
                con.Open();
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch { return false; }
            finally { con.Close(); }
            return affectedRows > 0;
            
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID , int LicenseClassID)
        {
            int LicenseID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select Licenses.LicenseID
                            from Licenses join Drivers on 
                            Licenses.DriverID = Drivers.DriverID
                            where 
                                Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand cmd  = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@LicenseClass",LicenseClassID);
            cmd.Parameters.AddWithValue("@PersonID",PersonID);
            
            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if(result !=null && int.TryParse(result.ToString(),out int NewID))
                {
                    LicenseID = NewID;
                }

            }
            catch { }
            finally { con.Close(); }
            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"update Licenses
                            set 
                             IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                con.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch { }
            finally{ con.Close(); }
            return rowsAffected > 0;
        }
    }
}
