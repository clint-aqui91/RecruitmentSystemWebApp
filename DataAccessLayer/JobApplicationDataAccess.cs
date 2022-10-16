using Microsoft.Data.SqlClient;
using RecruitmentSystemWebApplication.Models;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer
{

    /// <summary>
    /// Class <c>JobApplicationDataAccess</c> contains the Data Access methods for a job application. It is called by the Application Logic Layer.
    /// It uses stored procedures to interact with the database.
    /// </summary>
    public class JobApplicationDataAccess
    {
        string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";

        /// <summary>
        /// Method <c>InsertJobApplicationRecordInDatabase</c> interacts with the database to insert a job application record using a database stored procedure.
        /// </summary>
        public JobApplicationModel InsertJobApplicationRecordInDatabase(JobApplicationModel jobApplicationModelObject)
        {
            jobApplicationModelObject.CVFileBytes = GetCVFileBytes(jobApplicationModelObject.CVFile);

            string ResponseFromDatabase = "";

            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_CreateJobApplication", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Set the SqlCommand's parameters with the Job Application Mode Object's parameter values.
                sqlCommand.Parameters.AddWithValue("@JobVacancyID", jobApplicationModelObject.JobVacancyID);
                sqlCommand.Parameters.AddWithValue("@CVFileBytes", jobApplicationModelObject.CVFileBytes);
                sqlCommand.Parameters.AddWithValue("@JobseekerUserName", jobApplicationModelObject.JobseekerUsername);
                sqlCommand.Parameters.AddWithValue("@YearsOfExperienceID", jobApplicationModelObject.PossessedYearsOfWorkingExperienceRangeID);
                sqlCommand.Parameters.AddWithValue("@AcademicEducationQualificationLevelID", jobApplicationModelObject.PossessedAcademicEducationQualificationLevelID);
                sqlCommand.Parameters.AddWithValue("@PreferredSalaryrangeID", jobApplicationModelObject.PreferredSalaryRangeID);
                sqlCommand.Parameters.AddWithValue("@CoveringLetter", jobApplicationModelObject.JobApplicationCoveringLetter);

                sqlConnection.Open();
                ResponseFromDatabase = sqlCommand.ExecuteScalar().ToString();

                if (ResponseFromDatabase == "SUCCESSFUL JOB APPLICATION CREATION")
                {
                    sqlConnection.Close();
                    jobApplicationModelObject.JobApplicationCreationAlertID = 1;
                    return jobApplicationModelObject;
                }

                else
                {
                    jobApplicationModelObject.JobApplicationCreationAlertID = 2;
                    return jobApplicationModelObject;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jobApplicationModelObject.JobApplicationCreationAlertID = 2;
                return jobApplicationModelObject;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetCVFileBytes</c> gets file bytes (in the form an array of type byte from the uploaded CV File).
        /// </summary>
        public byte[] GetCVFileBytes(IFormFile CVFile)
        {

            byte[] bufferTemp = new byte[CVFile.Length];

            using (var memoryStream = new MemoryStream())
            {
                CVFile.OpenReadStream().CopyTo(memoryStream);
                var tempMemoryStream = memoryStream.ToArray();
                Array.Copy(tempMemoryStream, bufferTemp, tempMemoryStream.Length);
            }

            return bufferTemp;
        }

        /// <summary>
        /// Method <c>CheckJobApplicationExistence</c> interacts with the database to check if a job application for a job vacancy by the same jobseeker exists.
        /// </summary>
        public JobApplicationModel CheckJobApplicationExistence(JobApplicationModel jobApplicationModelObject)
        {
            string ResponseFromDatabase = "";
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_CheckJobApplicationExistence", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add the Job Vacancy ID and Jobseeker username as parameters to the SQL Command.
                sqlCommand.Parameters.AddWithValue("@JobVacancyID", jobApplicationModelObject.JobVacancyID);
                sqlCommand.Parameters.AddWithValue("@JobseekerUserName", jobApplicationModelObject.JobseekerUsername);

                sqlConnection.Open();
                ResponseFromDatabase = sqlCommand.ExecuteScalar().ToString();

                if (ResponseFromDatabase == "JOB APPLICATION BY THE SAME JOBSEEKER FOR THE SAME JOB VACANCY ALREADY EXISTS")
                {
                    sqlConnection.Close();
                    jobApplicationModelObject.JobApplicationAlreadyExistsState = true;
                    return jobApplicationModelObject;
                }

                else if (ResponseFromDatabase == "JOB APPLICATION BY THE SAME JOBSEEKER FOR THE SAME JOB VACANCY DOES NOT EXISTS")
                {
                    jobApplicationModelObject.JobApplicationAlreadyExistsState = false;
                    return jobApplicationModelObject;
                }

                // Something unexpected happened, hence the failed job application creation message must be displayed to the user
                else
                {
                    jobApplicationModelObject.JobApplicationCreationAlertID = 2;
                    return jobApplicationModelObject;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jobApplicationModelObject.JobApplicationCreationAlertID = 2;
                return jobApplicationModelObject;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetShortListedJobApplicationByJobVacancyID</c> interacts with the database to get a list of shortlisted job application for a Job Vacancy by its ID.
        /// </summary>
        public List<JobApplicationModel> GetShortListedJobApplicationByJobVacancyID(int JobVacancyID)
        {
            SqlConnection sqlConnection = new SqlConnection();
            List<JobApplicationModel> shorlistedJobApplicationsList = new List<JobApplicationModel>();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_GetShortlistedJobApplicationListByVacancyID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@JobVacancyID", JobVacancyID);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader sqlDataReader = null;
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        // Create a new JobVacancyModel Object, populate with values from DataReader and add it to the list
                        JobApplicationModel jobApplicationModelObject = new JobApplicationModel();
                        jobApplicationModelObject.JobVacancyID = JobVacancyID;
                        jobApplicationModelObject.JobApplicationID = int.Parse(sqlDataReader["JobApplicationID"].ToString());
                        jobApplicationModelObject.JobseekerName = sqlDataReader["JobseekerName"].ToString();
                        jobApplicationModelObject.JobseekerSurname = sqlDataReader["JobseekerSurname"].ToString();
                        jobApplicationModelObject.JobseekerContactEmailAddress = sqlDataReader["JobseekerContactEmailAddress"].ToString();
                        jobApplicationModelObject.JobApplicationCoveringLetter = sqlDataReader["JobApplicationCoveringLetter"].ToString();
                        jobApplicationModelObject.PossessedYearsOfWorkingExperienceRangeValue = sqlDataReader["PossessedYearsOfExperienceRange"].ToString();
                        jobApplicationModelObject.PossessedAcademicEducationQualificationLevelName = sqlDataReader["PossessedAcademicEducationQualificationLevel"].ToString();
                        jobApplicationModelObject.PreferredSalaryRangeValue = sqlDataReader["PreferredSalaryRange"].ToString();
                        jobApplicationModelObject.CVFileBytes = GetCVFileBytesByJobApplicationID(jobApplicationModelObject.JobApplicationID);
                        jobApplicationModelObject.CVFileBytesToBase64 = Convert.ToBase64String(jobApplicationModelObject.CVFileBytes);

                        shorlistedJobApplicationsList.Add(jobApplicationModelObject);
                    }
                }

                else
                {
                    shorlistedJobApplicationsList.Clear();
                }

                return shorlistedJobApplicationsList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return shorlistedJobApplicationsList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetCVFileBytesByJobApplicationID</c> interacts with the database to get a file bytes array of a job application's CV file Binary Large Object (BLOB) from the
        /// database.
        /// </summary>
        public byte[] GetCVFileBytesByJobApplicationID(int JobApplicationID)
        {
            //byte[] filebytes = null;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                sqlConnection.Open();

                string sqlquery = "SELECT CVFileBLOB FROM JobApplication WHERE PK_JobApplicationID = '" + JobApplicationID + "'";
                SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);
                //sqlCommand.Parameters.AddWithValue("@CompanyID", companyID);

                // example - https://stackoverflow.com/questions/7724550/retrieve-varbinarymax-from-sql-server-to-byte-in-c-sharp
                return sqlCommand.ExecuteScalar() as byte[];
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return null;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
