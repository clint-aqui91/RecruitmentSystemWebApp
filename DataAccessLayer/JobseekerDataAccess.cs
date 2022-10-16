using Microsoft.Data.SqlClient;
using RecruitmentSystemWebApplication.Models;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer
{

    /// <summary>
    /// Class <c>JobseekerDataAccess</c> contains the Data Access methods for a jobseeker. It is called by the Application Logic Layer.
    /// It uses stored procedures to interact with the database.
    /// </summary>
    public class JobseekerDataAccess
    {
        string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";

        /// <summary>
        /// Method <c>RegisterJobseeker</c> calls the InsertJobseekerRecordInDatabase method.
        /// </summary>
        public JobseekerModel RegisterJobseeker(JobseekerModel jobseekerModel)
        {
            bool Result;

            Result = InsertJobseekerRecordInDatabase(jobseekerModel);
            jobseekerModel.SuccessfulJobseekerRegistrationResponse = Result;

            return jobseekerModel;
        }

        /// <summary>
        /// Method <c>InsertJobseekerRecordInDatabase</c> interacts with the database to insert a jobseeker record in the database using a stored procedure.
        /// </summary>
        public bool InsertJobseekerRecordInDatabase(JobseekerModel jobseekerModel)
        {
            bool Result = true;

            string ResponseFromDatabase = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_CreateJobseeker", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@JobseekerName", jobseekerModel.JobseekerName);
                sqlCommand.Parameters.AddWithValue("@JobseekerSurname", jobseekerModel.JobseekerSurname);
                sqlCommand.Parameters.AddWithValue("@JobseekerEmailAddress", jobseekerModel.JobseekerEmailAddress);

                sqlConnection.Open();
                ResponseFromDatabase = sqlCommand.ExecuteScalar().ToString();

                if (ResponseFromDatabase == "JOBSEEKER REGISTRATION SUCCESSFUL")
                {
                    sqlConnection.Close();
                    Result = true;
                    return Result;
                }

                else
                {
                    return Result = false;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result = false;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
