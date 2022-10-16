using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{
    /// <summary>
    /// Class <c>AcademicEducationQualificationLevel</c> contains a class representing the Academic Qualification Level, used in Job Vacancies and Job Applications.
    /// This class is used to retrieve a list of the Academic Education Qualification Levels from the database, primarly used as a drop-down list.
    /// </summary>
    public class AcademicEducationQualificationLevel
    {
        public int AcademicEducationQualificationLevelID { get; set; }
        public string AcademicEducationQualificationLevelName { get; set; }

        public List<AcademicEducationQualificationLevel> AcademicEducationQualificationLevelList { get; set; }

        /// <summary>
        /// Method <c>GetAcademicEducationQualificationLevelList</c> retrieves a list.
        /// This class is used to retrieve a list of the Academic Education Qualification Levels from the database using an SQL Stored Procedure.
        /// </summary>
        public List<AcademicEducationQualificationLevel> GetAcademicEducationQualificationLevelList()
        {
            List<AcademicEducationQualificationLevel> academicEducationQualificationLevelList = new List<AcademicEducationQualificationLevel>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";
            SqlConnection sqlConnection = new SqlConnection();

            // Try block containing statements which interact with the database to retrieve the list from the database
            try
            {
                // SqlDataReader class reads a forward-only stream of rows from an SQL Server database. This will contain the rows containing the Academic Education
                // Qualification Level List retrieved from the database
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader?view=dotnet-plat-ext-6.0
                SqlDataReader sqlDataReader = null;

                // SqlConnection class represents a connection to an SQL Server database.
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-6.0
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                // Initiate a new SqlCommand object (which represents a Transact-SQL statement or stored procedure to be executed against the SQL Server database)
                // and set the SQL connection, stored procedure name (residing in the SQL Server database) and the command type.
                //Reference: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=dotnet-plat-ext-6.0
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetAcademicEducationLevelList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Open the SQL Connection
                sqlConnection.Open();

                // Populate the SQLDataReader with the results from the SQL Command's Execute Reader method.
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executereader?view=dotnet-plat-ext-6.0
                sqlDataReader = sqlCommand.ExecuteReader();

                // If SqlDataReader contains rows
                if (sqlDataReader.HasRows)
                {
                    // Add a new AcademicQualificationLevel to the list - this is used as the first object in the list, and does not pass form validation in the view
                    academicEducationQualificationLevelList.Add(new AcademicEducationQualificationLevel { AcademicEducationQualificationLevelID = -1, AcademicEducationQualificationLevelName = "-- Select An Academic Education Qualification Level--" });
                    
                    // While loop to add the rows contained in the SQLDataReader to the list (a row's column corresponds with the object's parameters
                    while (sqlDataReader.Read())
                    {
                        academicEducationQualificationLevelList.Add(new AcademicEducationQualificationLevel
                        {
                            AcademicEducationQualificationLevelID = Convert.ToInt32(sqlDataReader["PK_AcademicEducationLevelID"]),
                            AcademicEducationQualificationLevelName = Convert.ToString(sqlDataReader["Name"])
                        });
                    }
                }

                // SQL Connections are closed as a best practice (in terms of performance)
                sqlConnection.Close();

                // return the list
                return academicEducationQualificationLevelList;
            }

            // If an exception within the try block occured
            catch (Exception ex)
            {
                // Write the Exception message to console
                Console.WriteLine(ex.ToString());
                // Clear the list, and  add a new object indicating that something went wrong.
                academicEducationQualificationLevelList.Clear();
                academicEducationQualificationLevelList.Add(new AcademicEducationQualificationLevel { AcademicEducationQualificationLevelID = -2, AcademicEducationQualificationLevelName = "!! List not Loaded !!" });
               
                // return list
                return academicEducationQualificationLevelList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
