using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{

    /// <summary>
    /// Class <c>JobSpecialization</c> contains a class representing an Job Specialization.
    /// This class is used to retrieve a list of Job Specializations from the database, primarly used as a drop-down list for the user to select a job specialization.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class JobSpecialization
    {
        public int JobSpecializationID { get; set; }
        public string JobSpecializationName { get; set; }

        public List<JobSpecialization> JobSpecializationList { get; set; }

        public List<JobSpecialization> GetJobSpecializationList()
        {
            List<JobSpecialization> jobSpecializationList = new List<JobSpecialization>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {

                SqlDataReader sqlDataReader = null;

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetJobSpecializationList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    jobSpecializationList.Add(new JobSpecialization { JobSpecializationID = -1, JobSpecializationName = "-- Select A Job Specialization--" });
                    while (sqlDataReader.Read())
                    {
                        jobSpecializationList.Add(new JobSpecialization
                        {
                            JobSpecializationID = Convert.ToInt32(sqlDataReader["PK_JobSpecializationID"]),
                            JobSpecializationName = Convert.ToString(sqlDataReader["Name"])
                        });
                    }
                }

                sqlConnection.Close();
                return jobSpecializationList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                jobSpecializationList.Clear();
                jobSpecializationList.Add(new JobSpecialization { JobSpecializationID = -2, JobSpecializationName = "!! List not Loaded !!" });

                return jobSpecializationList;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
