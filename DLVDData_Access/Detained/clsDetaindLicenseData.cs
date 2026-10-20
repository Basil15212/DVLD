using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.Detained
{
    public class clsDetainedLicenseData
    {
        public static bool GetDetainedLicenseInfoByID(int DetainedID,
            ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased,
            ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select * from DetainedLicenses where DetainedID =@DetainedID and IsReleased =0;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DetainedID", DetainedID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = DateTime.MaxValue;
                    else
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];


                }
                else
                    isFound = false;
                reader.Close();
            }
            catch { isFound = false; }
            finally { con.Close(); }
            return isFound;
        }


        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
               ref int DetainedID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased,
               ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select * from DetainedLicenses where LicenseID = @LicenseID and IsReleased =0;";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DetainedID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = DateTime.MaxValue;
                    else
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); return false; ;
            }
            finally { con.Close(); }
            return isFound;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select * from detainedLicenses_View order by IsReleased ,DetainID;";

            SqlCommand cmd = new SqlCommand(query, con);

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
            catch (Exception ex) { Console.WriteLine(ex.Message); } finally { con.Close(); } return dt;
        }

        public static int AddNewDetainedLicense(int LicenseID ,DateTime DetainDate ,decimal FineFees ,int CreatedByUserID)

        {
            int DetainID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"insert into DetainedLicenses
                            (LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased)
                            values
                            (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0);
                                SELECT SCOPE_IDENTITY();";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LicenseID",LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                con.Open();
                object Result = cmd.ExecuteScalar();
                if(Result != null && int.TryParse(Result.ToString(),out int NewID))
                {
                    DetainID = NewID;
                }

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return DetainID;

        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID)
        {
            int affectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update DetainedLicenses
                                set
                                LicenseID =@LicenseID ,
                                DetainDate =@DetainDate,
                                FineFees = @FineFees,
                                CreatedByUserID =@CreatedByUserID
                                where DetainID =@DetainID;";

            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                con.Open();
                affectedRows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }finally { con.Close(); } return (affectedRows > 0);
        }

        public static bool ReleaseDetainedLicense(int DetainID ,int ReleasedByUserID ,int ReleaseApplicationID)
        {
            int affectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update dbo.DetainedLicenses
                            set 
                            IsReleased = @IsReleased,
                            ReleasedByUserID =@ReleasedByUserID,
                            ReleaseApplicationID = @ReleaseApplicationID,
                            
                            ReleaseDate =@ReleaseDate
                            where DetainID =@DetainID;";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            cmd.Parameters.AddWithValue("@ReleaseDate" ,DateTime.Now);
            cmd.Parameters.AddWithValue("@DetainID", DetainID);
            cmd.Parameters.AddWithValue("@IsReleased", 1);

            try

            {
                con.Open();
                affectedRows = cmd.ExecuteNonQuery();

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return affectedRows > 0;
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetaind = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select IsDetained =1
                            from detainedLicenses
                            where LicenseID =@LicenseID
                                    and
                            IsReleased =0;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    IsDetaind = Convert.ToBoolean(result);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return IsDetaind;
        }
    }
}
