using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.International_Driving_License
{
    public class clsInternationalDrivingLicenseData
    {
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,
            ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"
                            select * from InternationalLicenses where InternationalLicenseID =@InternationalLicenseID ";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return isFound;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"
                          select  InternationalLicenseID, ApplicationID,DriverID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
                        from InternationalLicenses 
                        order by IsActive, ExpirationDate desc; ";

            SqlCommand cmd = new SqlCommand(Query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return dt;

        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"
                            select InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
                        from InternationalLicenses 
                        where DriverID=@DriverID
                        order by ExpirationDate desc;";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return dt;
        }

        public static int AddNewInternationalLicense(int ApplicationID ,int DriverID , int IssuedUsingLocalLicenseID,
                        DateTime IssueDate ,DateTime ExpirationDate ,bool IsActive ,int CreatedByUserID)
        {

            int InternationalLicenseID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"
                                Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;
                        
                            insert into InternationalLicenses

                            (ApplicationID, DriverID ,IssuedUsingLocalLicenseID ,IssueDate ,ExpirationDate ,IsActive, CreatedByUserID)
                            values
                            (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive ,@CreatedByUserID);
                                SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    InternationalLicenseID = NewID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID , 
            int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
                        DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int AffectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"
                            update InternationalLicenses
                            set 
                            ApplicationID =@ApplicationID,
                            DriverID =@DriverID,
                            IssuedUsingLocalLicenseID =@IssuedUsingLocalLicenseID,
                            IssueDate=@IssueDate,
                            ExpirationDate =@ExpirationDate,
                            IsActive =@IsActive, 
                            CreatedByUserID =@CreatedByUserID
                            where InternationalLicenseID =@InternationalLicenseID;";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return AffectedRows > 0;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"
                            selecct top 1 InternationalLicenseID 
                            from InternationalLicenses 
                                where
                            DriverID =@DriverID and GetDate() between IssueDate and ExpirationDate
                            order by ExpirationDate Desc;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    InternationalLicenseID = NewID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return InternationalLicenseID;
        }


    }
}
