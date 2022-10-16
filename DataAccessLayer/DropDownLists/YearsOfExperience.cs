using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{

    /// <summary>
    /// Class <c>YearsOfExperience</c> contains a class representing a Years of Experience Range.
    /// This class is used to retrieve a list of Years of Experience Ranges from the database, primarly used as a drop-down list for the user to select a years of experience range.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class YearsOfExperience
    {
        public int YearsOfExperienceID { get; set; }
        public string YearsOfExperienceRangeValue { get; set; }
        public List<YearsOfExperience> YearsOfExperienceList { get; set; }

        public List<YearsOfExperience> GetYearsOfExperienceList()
        {
            List<YearsOfExperience> employmentBasisTypeList = new List<YearsOfExperience>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                SqlDataReader sqlDataReader = null;

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetYearsOfExperienceList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    employmentBasisTypeList.Add(new YearsOfExperience { YearsOfExperienceID = -1, YearsOfExperienceRangeValue = "-- Select a Years of Experience Range--" });
                    while (sqlDataReader.Read())
                    {
                        employmentBasisTypeList.Add(new YearsOfExperience
                        {
                            YearsOfExperienceID = Convert.ToInt32(sqlDataReader["PK_YearsOfExperienceID"]),
                            YearsOfExperienceRangeValue = Convert.ToString(sqlDataReader["Range"])
                        });
                    }
                }

                sqlConnection.Close();

                return employmentBasisTypeList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                employmentBasisTypeList.Clear();
                employmentBasisTypeList.Add(new YearsOfExperience { YearsOfExperienceID = -2, YearsOfExperienceRangeValue = "!! List not Loaded !!" });

                return employmentBasisTypeList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
