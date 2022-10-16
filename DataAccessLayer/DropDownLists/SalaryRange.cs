using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{

    /// <summary>
    /// Class <c>SalaryRange</c> contains a class representing a Salary Range.
    /// This class is used to retrieve a list of Salary Ranges from the database, primarly used as a drop-down list for the user to select a salary range.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class SalaryRange
    {
        public int SalaryRangeID { get; set; }
        public string SalaryRangeValue { get; set; }

        public List<SalaryRange> SalaryRangeList { get; set; }

        public List<SalaryRange> GetSalaryRangeList()
        {
            List<SalaryRange> salaryRangeList = new List<SalaryRange>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                SqlDataReader sqlDataReader = null;

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetSalaryRangeList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    salaryRangeList.Add(new SalaryRange { SalaryRangeID = -1, SalaryRangeValue = "-- Select A Salary Range--" });
                    while (sqlDataReader.Read())
                    {
                        salaryRangeList.Add(new SalaryRange
                        {
                            SalaryRangeID = Convert.ToInt32(sqlDataReader["PK_SalaryRangeID"]),
                            SalaryRangeValue = Convert.ToString(sqlDataReader["Range"])

                        });
                    }
                }

                sqlConnection.Close();
                return salaryRangeList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                salaryRangeList.Clear();
                salaryRangeList.Add(new SalaryRange { SalaryRangeID = -2, SalaryRangeValue = "!! List not Loaded !!" });

                return salaryRangeList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
