using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.Tests_ForLicense.Test_Appointment
{
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentByID( int TestAppointmentID , ref int TestTypeID ,ref int LocalDrivingLicenseApplicationID,
                    ref DateTime AppointmentDate ,ref float PaidFees ,ref int CreatedByUserID ,
                    ref bool IsLocked ,ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"Select * from TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.Parameters.AddWithValue("@TestAppointmentID" , TestAppointmentID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

                }
                reader.Close();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); isFound = false; }
            finally { con.Close(); }
            return isFound;
        }

        public static bool GetLastTestAppointment
            (
            int LocalDrivingLicenseApplicationID, int TestTypeID , ref int TestAppointmentID,
                ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID,
                ref bool IsLocked, ref int RetakeTestApplicationID
            )
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select top 1 * from TestAppointments 
                                where (TestTypeID = @TestTypeID) and
                                (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                                    order by TestAppointmentID desc";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

                }
                else
                { 
                    isFound = false;
                }
                reader.Close();
            }catch(Exception ex) { Console.WriteLine(ex.Message); isFound = false; }
            finally { con.Close(); }
            return isFound; 
        }

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select * from TestAppointments_View
                            order by AppointmentDate desc";

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
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return dt;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID ,int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"select TestAppointmentID ,AppointmentDate ,PaidFees ,IsLocked
                                from TestAppointments 
                            where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and 
                                TestTypeID =@TestTypeID 
                                order by AppointmentDate desc";
            SqlCommand cmd = new SqlCommand(query , con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally{ con.Close(); }
            return dt;
        }

        public static int AddNewTestAppointment(int TestTypeID , int LocalDrivingLicenseApplicationID ,DateTime AppointmentDate ,
                    float PaidFees ,int CreatedByUserID , bool IsLocked ,int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"insert int  TestAppointments 
                            (TestTypeID , LocalDrivingLicenseApplicationID,AppointmentDate,
                                PaidFees ,CreatedByUserID, IsLocked, RetakeTestApplicationID)
                                values
                            (@TestTypeID,@LocalDrivingLicenseApplicationID ,@AppointmentDate, 
                                @PaidFees,@CreatedByUserID,@IsLocked ,@RetakeTestApplicationID)
                                SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand (query , con);
            cmd.Parameters.AddWithValue("@TestTypeID",TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate",AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees",PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked",IsLocked);

            if(RetakeTestApplicationID == -1)
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString() ,out int NewID))
                {
                    TestAppointmentID = NewID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
                    float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int affectedRws = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update  TestAppointments 
                            set
                            TestTypeID =@TestTypeID,
                            LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID,
                            AppointmentDate =@AppointmentDate,
                            PaidFees =@PaidFees,
                            CreatedByUserID =@CreatedByUserID,
                            IsLocked =@IsLocked, 
                            RetakeTestApplicationID =@RetakeTestApplicationID
                            where TestAppointmentID =@TestAppointmentID ;";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == -1)
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                con.Open();
                affectedRws = cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return affectedRws > 0;
            
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"select TestID from Tests where TestAppointmentID =@TestAppointmentID; ";
            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.Parameters.AddWithValue("@TestAppointmentID" , TestAppointmentID);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    TestID = NewID;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return TestID;
        }
    }
}
