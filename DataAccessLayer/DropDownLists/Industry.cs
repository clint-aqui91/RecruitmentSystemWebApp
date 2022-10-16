using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{

    /// <summary>
    /// Class <c>Industry</c> contains a class representing an Industry.
    /// This class is used to retrieve a list of Industries from the database, primarly used as a drop-down list for the user to select an industry.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class Industry
    {
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }

        public List<Industry> IndustryList { get; set; }

        public List<Industry> GetIndustryList()
        {
            List<Industry> industryList = new List<Industry>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";

            try
            {
                SqlConnection sqlConnection = null;

                SqlDataReader sqlDataReader = null;

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetIndustryList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    industryList.Add(new Industry { IndustryID = -1, IndustryName = "-- Select An  Industry --" });
                    while (sqlDataReader.Read())
                    {
                        industryList.Add(new Industry
                        {
                            IndustryID = Convert.ToInt32(sqlDataReader["PK_IndustryID"]),
                            IndustryName = Convert.ToString(sqlDataReader["Name"])
                        });
                    }
                }

                sqlConnection.Close();

                return industryList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                industryList.Clear();
                industryList.Add(new Industry { IndustryID = -2, IndustryName = "!! List not Loaded !!" });
                return industryList;
            }
        }
    }
}
