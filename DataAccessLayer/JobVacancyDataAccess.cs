using Microsoft.Data.SqlClient;
using RecruitmentSystemWebApplication.Models;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer
{
    /// <summary>
    /// Class <c>JobVacancyDataAccess</c> contains the Data Access methods for a job vacancy. It is called by the Application Logic Layer.
    /// </summary>
    public class JobVacancyDataAccess
    {
        string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";

        /// <summary>
        /// Method <c>InsertJobVacancyRecordInDatabase</c> interacts with the database to insert a job vacancy record in the database using a stored procedure.
        /// </summary>
        public JobVacancyModel InsertJobVacancyRecordInDatabase(JobVacancyModel jobVacancyModel)
        {
            string ResponseFromDatabase = "";
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_CreateJobVacancy", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@JobVacancyTitle", jobVacancyModel.JobVacancyTitle);
                sqlCommand.Parameters.AddWithValue("@JobVacancyDescription", jobVacancyModel.JobVacancyDescription);
                sqlCommand.Parameters.AddWithValue("@RecruiterUserName", jobVacancyModel.RecruiterUsername);
                sqlCommand.Parameters.AddWithValue("@OfferedSalaryRangeID", jobVacancyModel.OfferedSalaryRangeID);
                sqlCommand.Parameters.AddWithValue("@JobSpecializationID", jobVacancyModel.JobSpecializationID);
                sqlCommand.Parameters.AddWithValue("@JobVacancyLocationID", jobVacancyModel.JobVacancyLocationID);
                sqlCommand.Parameters.AddWithValue("@EmploymentBasisTypeID", jobVacancyModel.EmploymentBasisTypeID);
                sqlCommand.Parameters.AddWithValue("@RequiredYearsOfWorkingExperienceRangeID", jobVacancyModel.RequiredYearsOfWorkingExperienceRangeID);
                sqlCommand.Parameters.AddWithValue("@RequiredAcademicEducationQualificationLevelID", jobVacancyModel.RequiredAcademicEducationQualificationLevelID);

                sqlConnection.Open();
                ResponseFromDatabase = sqlCommand.ExecuteScalar().ToString();

                if (ResponseFromDatabase == "SUCCESSFUL JOB VACANCY CREATION")
                {
                    sqlConnection.Close();
                    jobVacancyModel.JobVacancyCreationAlertID = 1;
                    return jobVacancyModel;
                }

                else
                {
                    jobVacancyModel.JobVacancyCreationAlertID = 2;
                    return jobVacancyModel;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                jobVacancyModel.JobVacancyCreationAlertID = 2;
                return jobVacancyModel;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        public List<JobVacancyModel> GetAllJobVacancies(List<JobVacancyModel> jobVacancyList)
        {
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_GetJobVacancyList", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader sqlDataReader = null;
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        // Create a new Job Vacancy Model Object
                        JobVacancyModel jobVacancyModelObject = new JobVacancyModel();

                        // Set the job vacancy model object with job vacancy parameter values from DataReader
                        jobVacancyModelObject.JobVacancyID = int.Parse(sqlDataReader["JobVacancyID"].ToString());
                        jobVacancyModelObject.JobVacancyTitle = sqlDataReader["JobVacancyTitle"].ToString();
                        jobVacancyModelObject.JobVacancyDescription = sqlDataReader["JobVacancyDescription"].ToString();
                        jobVacancyModelObject.JobVacancyLocationName = sqlDataReader["JobLocationTownName"].ToString();
                        jobVacancyModelObject.OfferedSalaryRangeValue = sqlDataReader["OfferedSalaryRange"].ToString();
                        jobVacancyModelObject.RequiredYearsOfWorkingExperienceRangeValue = sqlDataReader["RequiredYearsOfExperienceRange"].ToString();
                        jobVacancyModelObject.EmploymentBasisTypeName = sqlDataReader["EmploymentBasisTypeName"].ToString();
                        jobVacancyModelObject.RequiredAcademicEducationQualificationLevelName = sqlDataReader["RequiredAcademicEducationQualificationLevel"].ToString();

                        // Set the job vacancy model object with company parameter values from DataReader
                        jobVacancyModelObject.CompanyID = int.Parse(sqlDataReader["CompanyID"].ToString());
                        jobVacancyModelObject.CompanyName = sqlDataReader["CompanyName"].ToString();
                        jobVacancyModelObject.CompanyIndustryName = sqlDataReader["CompanyIndustryName"].ToString();
                        jobVacancyModelObject.CompanySizeRange = sqlDataReader["CompanySizeRange"].ToString();
                        jobVacancyModelObject.CompanyMainOfficeTownLocation = sqlDataReader["CompanyHeadQuartersLocation"].ToString();
                        jobVacancyModelObject.CompanyLogoFileBytes = GetFileBytesByCompanyID(jobVacancyModelObject.CompanyID);
                        jobVacancyModelObject.CompanyLogoFileBytesToBase64 = Convert.ToBase64String(jobVacancyModelObject.CompanyLogoFileBytes);

                        // Add Job Vacancy Model Object to list
                        jobVacancyList.Add(jobVacancyModelObject);
                    }
                }

                else
                {
                    jobVacancyList.Clear();
                }

                return jobVacancyList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return jobVacancyList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetAllJobVacanciesByCompany</c> interacts with the database to retrieve all job vacancy records by a company. The recruiter username is used, since a single
        /// recruiter is related to a single company and vice-versa.
        /// </summary>
        public List<JobVacancyModel> GetAllJobVacanciesByCompany(string RecruiterUsername)
        {
            SqlConnection sqlConnection = new SqlConnection();
            List<JobVacancyModel> jobVacancyList = new List<JobVacancyModel>();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand("usp_GetJobVacancyListByCompanyID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@RecruiterUsername", RecruiterUsername);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader sqlDataReader = null;
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        // Create a new Job Vacancy Model Object
                        JobVacancyModel jobVacancyModelObject = new JobVacancyModel();

                        // Set the job vacancy model object with job vacancy parameter values from DataReader
                        jobVacancyModelObject.JobVacancyID = int.Parse(sqlDataReader["JobVacancyID"].ToString());
                        jobVacancyModelObject.JobVacancyTitle = sqlDataReader["JobVacancyTitle"].ToString();
                        jobVacancyModelObject.JobVacancyDescription = sqlDataReader["JobVacancyDescription"].ToString();
                        jobVacancyModelObject.JobVacancyLocationName = sqlDataReader["JobLocationTownName"].ToString();
                        jobVacancyModelObject.OfferedSalaryRangeValue = sqlDataReader["OfferedSalaryRange"].ToString();
                        jobVacancyModelObject.RequiredYearsOfWorkingExperienceRangeValue = sqlDataReader["RequiredYearsOfExperienceRange"].ToString();
                        jobVacancyModelObject.EmploymentBasisTypeName = sqlDataReader["EmploymentBasisTypeName"].ToString();
                        jobVacancyModelObject.RequiredAcademicEducationQualificationLevelName = sqlDataReader["RequiredAcademicEducationQualificationLevel"].ToString();

                        // Set the job vacancy model object with company parameter values from DataReader
                        jobVacancyModelObject.CompanyID = int.Parse(sqlDataReader["CompanyID"].ToString());
                        jobVacancyModelObject.CompanyName = sqlDataReader["CompanyName"].ToString();
                        jobVacancyModelObject.CompanyIndustryName = sqlDataReader["CompanyIndustryName"].ToString();
                        jobVacancyModelObject.CompanySizeRange = sqlDataReader["CompanySizeRange"].ToString();
                        jobVacancyModelObject.CompanyMainOfficeTownLocation = sqlDataReader["CompanyHeadQuartersLocation"].ToString();
                        jobVacancyModelObject.CompanyLogoFileBytes = GetFileBytesByCompanyID(jobVacancyModelObject.CompanyID);
                        jobVacancyModelObject.CompanyLogoFileBytesToBase64 = Convert.ToBase64String(jobVacancyModelObject.CompanyLogoFileBytes);

                        // Add Job Vacancy Model Object to list
                        jobVacancyList.Add(jobVacancyModelObject);
                    }
                }

                else
                {
                    jobVacancyList.Clear();
                }

                return jobVacancyList;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return jobVacancyList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetJobVacancyByJobVacancyID</c> interacts with the database to retrieve a job vacancy record by its ID.
        /// </summary>
        public JobVacancyModel GetJobVacancyByJobVacancyID(int JobVacancyID)
        {
            SqlConnection sqlConnection = new SqlConnection();
            JobVacancyModel jobVacancyModel = new JobVacancyModel();
            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                SqlCommand sqlCommand = new SqlCommand("usp_GetJobVacancyByJobVacancyID", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@JobVacancyID", JobVacancyID);

                // Set each required parameter from database and add to SQL command.
                // Reference: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/configuring-parameters-and-parameter-data-types

                // Job Vacancy Parameter values

                SqlParameter JobVacancyTitle = new SqlParameter();
                JobVacancyTitle.ParameterName = "@JobVacancyTitle";
                JobVacancyTitle.DbType = DbType.String;
                JobVacancyTitle.Size = 256;
                JobVacancyTitle.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(JobVacancyTitle);

                SqlParameter JobVacancyDescription = new SqlParameter();
                JobVacancyDescription.ParameterName = "@JobVacancyDescription";
                JobVacancyDescription.DbType = DbType.String;
                JobVacancyDescription.Size = 256;
                JobVacancyDescription.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(JobVacancyDescription);

                SqlParameter JobSpecializationName = new SqlParameter();
                JobSpecializationName.ParameterName = "@JobSpecializationName";
                JobSpecializationName.DbType = DbType.String;
                JobSpecializationName.Size = 256;
                JobSpecializationName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(JobSpecializationName);

                SqlParameter JobVacancyLocationName = new SqlParameter();
                JobVacancyLocationName.ParameterName = "@JobVacancyLocationName";
                JobVacancyLocationName.DbType = DbType.String;
                JobVacancyLocationName.Size = 256;
                JobVacancyLocationName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(JobVacancyLocationName);

                SqlParameter OfferedSalaryRangeValue = new SqlParameter();
                OfferedSalaryRangeValue.ParameterName = "@OfferedSalaryRangeValue";
                OfferedSalaryRangeValue.DbType = DbType.String;
                OfferedSalaryRangeValue.Size = 256;
                OfferedSalaryRangeValue.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(OfferedSalaryRangeValue);

                SqlParameter EmploymentBasisTypeName = new SqlParameter();
                EmploymentBasisTypeName.ParameterName = "@EmploymentBasisTypeName";
                EmploymentBasisTypeName.DbType = DbType.String;
                EmploymentBasisTypeName.Size = 256;
                EmploymentBasisTypeName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(EmploymentBasisTypeName);

                SqlParameter RequiredAcademicEducationQualificationLevelName = new SqlParameter();
                RequiredAcademicEducationQualificationLevelName.ParameterName = "@RequiredAcademicEducationQualificationLevelName";
                RequiredAcademicEducationQualificationLevelName.DbType = DbType.String;
                RequiredAcademicEducationQualificationLevelName.Size = 256;
                RequiredAcademicEducationQualificationLevelName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(RequiredAcademicEducationQualificationLevelName);

                SqlParameter RequiredYearsOfExperienceRangeValue = new SqlParameter();
                RequiredYearsOfExperienceRangeValue.ParameterName = "@RequiredYearsOfExperienceRangeValue";
                RequiredYearsOfExperienceRangeValue.DbType = DbType.String;
                RequiredYearsOfExperienceRangeValue.Size = 256;
                RequiredYearsOfExperienceRangeValue.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(RequiredYearsOfExperienceRangeValue);

                // Company Parameter values

                SqlParameter CompanyID = new SqlParameter();
                CompanyID.ParameterName = "@CompanyID";
                CompanyID.DbType = DbType.Int32;
                CompanyID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(CompanyID);

                SqlParameter CompanyName = new SqlParameter();
                CompanyName.ParameterName = "@CompanyName";
                CompanyName.DbType = DbType.String;
                CompanyName.Size = 256;
                CompanyName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(CompanyName);

                SqlParameter CompanyIndustryName = new SqlParameter();
                CompanyIndustryName.ParameterName = "@CompanyIndustryName";
                CompanyIndustryName.DbType = DbType.String;
                CompanyIndustryName.Size = 256;
                CompanyIndustryName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(CompanyIndustryName);

                SqlParameter CompanySizeRange = new SqlParameter();
                CompanySizeRange.ParameterName = "@CompanySizeRange";
                CompanySizeRange.DbType = DbType.String;
                CompanySizeRange.Size = 256;
                CompanySizeRange.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(CompanySizeRange);

                SqlParameter CompanyHeadQuartersLocation = new SqlParameter();
                CompanyHeadQuartersLocation.ParameterName = "@CompanyHeadQuartersLocation";
                CompanyHeadQuartersLocation.DbType = DbType.String;
                CompanyHeadQuartersLocation.Size = 256;
                CompanyHeadQuartersLocation.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(CompanyHeadQuartersLocation);

                SqlParameter CompanyContactEmailAddress = new SqlParameter();
                CompanyContactEmailAddress.ParameterName = "@CompanyContactEmailAddress";
                CompanyContactEmailAddress.DbType = DbType.String;
                CompanyContactEmailAddress.Size = 256;
                CompanyContactEmailAddress.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(CompanyContactEmailAddress);

                // Job Vacancy Parameter Values

                SqlParameter JobSpecializationID = new SqlParameter();
                JobSpecializationID.ParameterName = "@JobSpecializationID";
                JobSpecializationID.DbType = DbType.Int32;
                JobSpecializationID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(JobSpecializationID);

                SqlParameter JobVacancyLocationID = new SqlParameter();
                JobVacancyLocationID.ParameterName = "@JobVacancyLocationID";
                JobVacancyLocationID.DbType = DbType.Int32;
                JobVacancyLocationID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(JobVacancyLocationID);

                SqlParameter EmploymentBasisTypeID = new SqlParameter();
                EmploymentBasisTypeID.ParameterName = "@EmploymentBasisTypeID";
                EmploymentBasisTypeID.DbType = DbType.Int32;
                EmploymentBasisTypeID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(EmploymentBasisTypeID);

                SqlParameter RequiredYearsOfWorkingExperienceRangeID = new SqlParameter();
                RequiredYearsOfWorkingExperienceRangeID.ParameterName = "@RequiredYearsOfWorkingExperienceRangeID";
                RequiredYearsOfWorkingExperienceRangeID.DbType = DbType.Int32;
                RequiredYearsOfWorkingExperienceRangeID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(RequiredYearsOfWorkingExperienceRangeID);

                SqlParameter RequiredAcademicEducationQualificationLevelID = new SqlParameter();
                RequiredAcademicEducationQualificationLevelID.ParameterName = "@RequiredAcademicEducationQualificationLevelID";
                RequiredAcademicEducationQualificationLevelID.DbType = DbType.Int32;
                RequiredAcademicEducationQualificationLevelID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(RequiredAcademicEducationQualificationLevelID);

                SqlParameter OfferedSalaryRangeID = new SqlParameter();
                OfferedSalaryRangeID.ParameterName = "@OfferedSalaryRangeID";
                OfferedSalaryRangeID.DbType = DbType.Int32;
                OfferedSalaryRangeID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(OfferedSalaryRangeID);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                // Job Vacancy Model set with job vacancy parameter values from database record
                jobVacancyModel.JobVacancyID = JobVacancyID;
                jobVacancyModel.JobVacancyTitle = sqlCommand.Parameters["@JobVacancyTitle"].Value.ToString();
                jobVacancyModel.JobVacancyDescription = sqlCommand.Parameters["@JobVacancyDescription"].Value.ToString();
                jobVacancyModel.JobSpecializationName = sqlCommand.Parameters["@JobSpecializationName"].Value.ToString();
                jobVacancyModel.JobVacancyLocationName = sqlCommand.Parameters["@JobVacancyLocationName"].Value.ToString();
                jobVacancyModel.OfferedSalaryRangeValue = sqlCommand.Parameters["@OfferedSalaryRangeValue"].Value.ToString();
                jobVacancyModel.RequiredAcademicEducationQualificationLevelName = sqlCommand.Parameters["@RequiredAcademicEducationQualificationLevelName"].Value.ToString();
                jobVacancyModel.EmploymentBasisTypeName = sqlCommand.Parameters["@EmploymentBasisTypeName"].Value.ToString();
                jobVacancyModel.RequiredYearsOfWorkingExperienceRangeValue = sqlCommand.Parameters["@RequiredYearsOfExperienceRangeValue"].Value.ToString();

                // Job Vacancy Model set with company attributes of the same vacancy
                jobVacancyModel.CompanyID = Convert.ToInt32(sqlCommand.Parameters["@CompanyID"].Value.ToString());
                jobVacancyModel.CompanyName = sqlCommand.Parameters["@CompanyName"].Value.ToString();
                jobVacancyModel.CompanySizeRange = sqlCommand.Parameters["@CompanySizeRange"].Value.ToString();
                jobVacancyModel.CompanyMainOfficeTownLocation = sqlCommand.Parameters["@CompanyHeadQuartersLocation"].Value.ToString();
                jobVacancyModel.CompanyIndustryName = sqlCommand.Parameters["@CompanyIndustryName"].Value.ToString();
                jobVacancyModel.CompanyContactEmailAddress = sqlCommand.Parameters["@CompanyContactEmailAddress"].Value.ToString();
                jobVacancyModel.CompanyLogoFileBytes = GetFileBytesByCompanyID(jobVacancyModel.CompanyID);
                jobVacancyModel.CompanyLogoFileBytesToBase64 = Convert.ToBase64String(jobVacancyModel.CompanyLogoFileBytes);

                // Job Vacancy Attributes to be used by a recruiter to update a Job Vacancy (Update Job Vacancy functionality not implemented in its entirety)
                jobVacancyModel.JobSpecializationID = Convert.ToInt32(sqlCommand.Parameters["@JobSpecializationID"].Value.ToString());
                jobVacancyModel.JobVacancyLocationID = Convert.ToInt32(sqlCommand.Parameters["@JobVacancyLocationID"].Value.ToString());
                jobVacancyModel.EmploymentBasisTypeID = Convert.ToInt32(sqlCommand.Parameters["@EmploymentBasisTypeID"].Value.ToString());
                jobVacancyModel.RequiredYearsOfWorkingExperienceRangeID = Convert.ToInt32(sqlCommand.Parameters["@RequiredYearsOfWorkingExperienceRangeID"].Value.ToString());
                jobVacancyModel.RequiredAcademicEducationQualificationLevelID = Convert.ToInt32(sqlCommand.Parameters["@RequiredAcademicEducationQualificationLevelID"].Value.ToString());
                jobVacancyModel.OfferedSalaryRangeID = Convert.ToInt32(sqlCommand.Parameters["@OfferedSalaryRangeID"].Value.ToString());

                sqlConnection.Close();
                return jobVacancyModel;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                jobVacancyModel = new JobVacancyModel();
                return jobVacancyModel;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>DeleteJobVacancy</c> interacts with the database to delete a job vacancy record by its ID.
        /// </summary>
        public JobVacancyModel DeleteJobVacancy(int JobVacancyID)
        {
            JobVacancyModel jobVacancyModelObject = new JobVacancyModel();
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand("usp_DeleteJobVacancy", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@JobVacancyID", JobVacancyID);

                sqlConnection.Open();

                string ResponseFromDatabase = sqlCommand.ExecuteScalar().ToString();

                if (ResponseFromDatabase == "SUCCESSFUL JOB VACANCY DELETION")
                {
                    jobVacancyModelObject.JobVacancyDeleteAlertID = 1;
                }

                else
                {
                    jobVacancyModelObject.JobVacancyDeleteAlertID = 2;
                }

                return jobVacancyModelObject;
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                jobVacancyModelObject.JobVacancyDeleteAlertID = 2;
                return jobVacancyModelObject;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetFileBytesByCompanyID</c> interacts with the database to retrieve byte array for the Company Logo BLOB.
        /// </summary>
        public byte[] GetFileBytesByCompanyID(int companyID)
        {
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                sqlConnection.Open();

                string sqlquery = "SELECT LogoFileBlob FROM Company WHERE PK_CompanyID = '" + companyID + "'";
                SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);

                // Execute Scalar - https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executescalar?view=dotnet-plat-ext-6.0
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
