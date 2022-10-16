using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{
    /// <summary>
    /// Class <c>CompanySize</c> contains a class representing a company size.
    /// This class is used to retrieve a list of company sizes from the database, primarly used as a drop-down list for the user to select a company size range.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class CompanySize
    {
        public int CompanySizeID { get; set; }
        public string CompanySizeName { get; set; }

        public List<CompanySize> CompanySizeList { get; set; }

        public List<CompanySize> GetCompanySizeList()
        {
            List<CompanySize> companySizeList = new List<CompanySize>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                SqlDataReader sqlDataReader = null;

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetCompanySizeList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    companySizeList.Add(new CompanySize { CompanySizeID = -1, CompanySizeName = "-- Select A Company Size --" });

                    while (sqlDataReader.Read())
                    {
                        companySizeList.Add(new CompanySize
                        {
                            CompanySizeID = Convert.ToInt32(sqlDataReader["PK_CompanySizeID"]),
                            CompanySizeName = Convert.ToString(sqlDataReader["Range"])
                        });
                    }
                }

                sqlConnection.Close();
                return companySizeList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                companySizeList.Clear();
                companySizeList.Add(new CompanySize { CompanySizeID = -2, CompanySizeName = "!! List not Loaded !!" });
                return companySizeList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
