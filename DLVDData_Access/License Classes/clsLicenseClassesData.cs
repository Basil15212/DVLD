using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLVDData_Access.License_Classes
{
    public class clsLicenseCLassesData
    {

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"Select * from LicenseClasses";
            SqlCommand cmd = new SqlCommand(qury, conn);

            try
            {
                conn.Open();
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
            finally { conn.Close(); }
            return dt;
        }

        public static bool GetLicenseClassWithID(int ID, ref string ClassName, ref string ClassDescription, ref short MinimumAllowedAge,
            ref short DefaultValidityLength, ref decimal ClassFees)
        {

            bool isfound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"select * from LicenseClasses where LicenseClassID =@LicenseClassID";

            SqlCommand cmd = new SqlCommand(@qury, con);
            cmd.Parameters.AddWithValue("@LicenseClassID", ID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (short)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (short)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isfound = false;
            }
            finally
            {
                con.Close();

            }
            return isfound;
        }
        public static bool GetLicenseClassWithName(string Name, ref int ID, ref string Dscrip, ref short minAge,  ref short MaxValid,ref decimal Fees)
        {
            bool isFound = false;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"Select * from LicenseClasses where ClassName =@ClassName";
            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.Parameters.AddWithValue("@ClassName", Name);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["LicenseClassID"];
                    Dscrip = (string)reader["ClassDescription"];
                    minAge = (byte)reader["MinimumAllowedAge"];
                    MaxValid = (byte)reader["DefaultValidityLength"];
                    Fees = (decimal)reader["ClassFees"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally { con.Close(); }
            return isFound;
        }

        public static int AddNew(string Name, string Dscrip, short MinAge, short MaxValid, decimal Fees)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string Query = @"INSERT INTO LicenseClasses 

                            ClassName ,ClassDescription ,MinimumAllowedAge ,DefaultValidityLength ,ClassFees
                            VALUES
                            @ClassName ,@ClassDescription,@MinimumAllowedAge,@DefaultValidityLength,@ClassFees;

                             SELECT SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@ClassName", Name);
            cmd.Parameters.AddWithValue("@ClassDescription", Dscrip);
            cmd.Parameters.AddWithValue("@MinimumAllowedAge", MinAge);
            cmd.Parameters.AddWithValue("@DefaultValidityLength", MaxValid);
            cmd.Parameters.AddWithValue("@ClassFees", Fees);

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

        public static bool Update(int LicenseClassID, string Name, string Discrp, short MinAge, short MaxValid, decimal Fees)
        {
            int AffectedRows = 0;

            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string query = @"Update LicenseClasses
                                set 
                                ClassName =@ClassName ,
                                ClassDescription =@ClassDescription,
                                MinimumAllowedAge =@MinimumAllowedAge,
                                DefaultValidityLength =@DefaultValidityLength,
                                ClassFees =@ClassFees
                                  where LicenseClassID =@LicenseClassID;";

            SqlCommand cmd = new SqlCommand(@query, con);
            cmd.Parameters.AddWithValue("@ClassName", Name);
            cmd.Parameters.AddWithValue("@ClassDescription", Discrp);
            cmd.Parameters.AddWithValue("@MinimumAllowedAge", MinAge);
            cmd.Parameters.AddWithValue("@DefaultValidityLength", MaxValid);
            cmd.Parameters.AddWithValue("@ClassFees", Fees);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                con.Open();
                AffectedRows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return (AffectedRows > 0);

        }

        public static bool Delete(int LicenseClassID)
        {
            int affectedRows = 0;
            SqlConnection con = new SqlConnection(clsDataSittings.ConnectionString);
            string qury = @"delete from LicenseClasses where LicenseClassID =@LicenseClassID;";
            SqlCommand cmd = new SqlCommand(qury, con);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                con.Open();
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { con.Close(); }
            return (affectedRows > 0);


        }


    }
}
