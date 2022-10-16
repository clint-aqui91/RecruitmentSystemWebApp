using Microsoft.Data.SqlClient;
using System.Data;
//using Microsoft.Extensions.Configuration;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{
    /// <summary>
    /// Class <c>Town</c> contains a class representing a town.
    /// This class is used to retrieve a list of towns from the database, primarly used as a drop-down list for the user to select a town.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class Town
    {
        public string TownName { get; set; }
        public int TownID { get; set; }

        public List<Town> TownList { get; set; }

        public List<Town> GetTownList()
        {
            List<Town> townList = new List<Town>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";

            SqlConnection sqlConnection = null;

            try
            {
                SqlDataReader sqlDataReader = null;     

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetTownList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    townList.Add(new Town { TownID = -1, TownName = "-- Select A Town --" });
                    while (sqlDataReader.Read())
                    {
                        townList.Add(new Town
                        {
                            TownID = Convert.ToInt32(sqlDataReader["PK_TownID"]),
                            TownName = Convert.ToString(sqlDataReader["Name"])
                        });
                    }
                }

                sqlConnection.Close();

                return townList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                townList.Clear();
                townList.Add(new Town { TownID = -2, TownName = "!! List not Loaded !!" });

                return townList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }

}
