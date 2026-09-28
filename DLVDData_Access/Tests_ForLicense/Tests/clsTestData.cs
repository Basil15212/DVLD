using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.Tests_ForLicense.Tests
{
    public class clsTestData
    {
        public static bool GetTestInfoByID(int TestID ,
            ref int TestAppointmentID ,ref bool TestResult ,
            ref string Notes ,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select * from Tests where TestID =@TestID ;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            finally { con.Close(); }
            return isFound;
        }

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID ,int LicenseClssID, int TestTypeID ,
                ref  int TestID ,ref int TestAppointmentID , ref bool TestResult, ref string  Notes, ref int CreatedByUSerID)
        {
            bool isFound = false;
            SqlConnection con  = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"select Top 1 Tests.TestID ,Tests.TestAppointmentID ,Tests.TestResult ,Tests.Notes,Tests.CreatedByUserID,
                        Applications.ApplicantPersonID
                        from LocalDrivingLicenseApplications join Tests join TestAppointments on
                        Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                        on TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                        join Applications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                        WHERE (Applications.ApplicantPersonID =  @PersonID)
                        and (LocalDrivingLicenseApplications.LicenseClassID =@LicenseClassID)
                        and(TestAppointments.TestTypeID = @TestTypeID)
                        order By Tests.TestAppointmentID desc;";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClssID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                    CreatedByUSerID = (int)reader["CreatedByUSerID"];

                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            finally { con.Close(); }
            return isFound;

        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select * from Tests";
            SqlCommand cmd = new SqlCommand(query, con);
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
            catch(Exception ex) { Console.WriteLine(ex.Message);  }
            finally { con.Close(); }
            return dt;
        }

        public static int AddNewTest(int TestID ,int TestAppointmentID ,bool TestResult, string Notes ,int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"INSERT INTO Tests 
                                 (TestAppointmentID),(TestResult),(Notes),(CreatedByUserID)
                            Values
                                  (@TestAppointmentID),(@TestResult),(@Notes),(@CreatedByUserID);

                               Update TestAppointments
                                SET IsLocked=1
                                where TestAppointmentID = @TestAppointmentID
                                    SELECT SCOPE_IDENTITY();";
            SqlCommand cmd =new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);
          
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            if(Notes != null && Notes != "")
            {
                cmd.Parameters.AddWithValue("@Notes",Notes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Notes", System.DBNull.Value);
            }

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    ID = NewID;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return ID;

        }

        public static bool UpdateTest(int TestID , int TestAppointmentID , bool TestResult ,string Notes ,int CreatedByUserID)
        {
            int AffectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update Tests 
                            
                            set  
                            (TestAppointmentID) =(@TestAppointmentID) ,
                            (TestResult) = (@TestResult) ,
                            (Notes) =(@Notes) ,
                             (CreatedByUserID)= (@CreatedByUserID) 
                             
                            where (TestID) =(@TestID) ;";

            SqlCommand cmd= new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);
            cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally{ con.Close(); }
            return AffectedRows > 0;
        }

        public static byte GetPassedTestsCount(int LocalDrivingLicesApplicationID)
        {
            byte PAssedTess = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Select PassedTestCount =Count(TestTypeID)
                                from Tests join TestAppointments on  Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                                where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID 
                                and TestResult=1";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicesApplicationID);

            try
            {
                con.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && byte.TryParse(Result.ToString(), out byte CountTests))
                {
                    PAssedTess = CountTests;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return PAssedTess;
        }
        
    }
}
