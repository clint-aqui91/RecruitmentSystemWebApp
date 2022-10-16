using RecruitmentSystemWebApplication.Models;
using System.Text;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer
{
    /// <summary>
    /// Class <c>CompanyDataAccess</c> contains the Data Access methods for a company. It is called by the Application Logic Layer.
    /// It uses Transact-SQL statements and stored procedures to interact with the database.
    /// </summary>
    public class CompanyDataAccess
    {
        // Connection string for the Web Application Database
        string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";

        /// <summary>
        /// Method <c>RegisterCompany</c> handles the registration of a company. It gets the file bytes of the Company Logo present in the model, using the GetLogoFileBytes method
        /// and inserts the company record in the database by using the InsertCompanyRecordInDatabase method.
        /// </summary>
        public CompanyModel RegisterCompany(CompanyModel companyModel)
        {
            byte[] fileBytes = new byte[companyModel.CompanyLogoFile.Length];
            fileBytes = GetLogoFileBytes(companyModel);
            companyModel.FileBytes = fileBytes;

            bool result;
            result = InsertCompanyRecordInDatabase(companyModel);
            companyModel.SuccessfulCompanyRegistrationResponse = result;

            // boolean which comapres file bytes (for testing purposes)
            bool FileBytesCompare;

            //Test to compare fileBytes & CompanyModel File Bytes & display console messages
            FileBytesCompare = fileBytes.SequenceEqual(companyModel.FileBytes);

            if (FileBytesCompare == true)
            {
                Console.WriteLine("Bytes Match");
            }

            else
            {
                Console.WriteLine("Bytes Do Not Match");
            }

            return companyModel;
        }

        /// <summary>
        /// Method <c>InsertCompanyRecordInDatabase</c> handles the the creation of a company record in the database. It calls a database stored procedure which handles the
        /// record creation/insertion.
        /// </summary>
        public bool InsertCompanyRecordInDatabase(CompanyModel companyModel)
        {
            bool result = true;

            // This String parameter is used to compare the result returned from the database
            string ResponseFromDatabase = "";

            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                // Initialise a new instance of the SqlCommand class, with the SqlConnection, and stored procedure type. 
                SqlCommand sqlCommand = new SqlCommand("usp_CreateCompany", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add the Company Model parameters to the SQL Command parameters
                sqlCommand.Parameters.AddWithValue("@CompanyName", companyModel.CompanyName);
                sqlCommand.Parameters.AddWithValue("@RecruiterSignInEmailAddress", companyModel.RecruiterSignInEmailAddress);
                sqlCommand.Parameters.AddWithValue("@CompanyContactEmailAddress", companyModel.CompanyContactEmailAddress);
                sqlCommand.Parameters.AddWithValue("@LogoFileBytes", companyModel.FileBytes);
                sqlCommand.Parameters.AddWithValue("@CompanyTownID", companyModel.TownID);
                sqlCommand.Parameters.AddWithValue("@CompanyIndustryID", companyModel.IndustryID);
                sqlCommand.Parameters.AddWithValue("@CompanySizeID", companyModel.CompanySizeID);

                // Open a connection with the database
                sqlConnection.Open();

                // SqlCommand.ExecuteScalar().ToString(); Executes the query (in this case the database stored procedure) and returns the first row result (other rows are ignored).
                //Reference: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executescalar?view=dotnet-plat-ext-6.0
                ResponseFromDatabase = sqlCommand.ExecuteScalar().ToString();

                // If the returned database result matches the expected message, set the result to true and return it
                if (ResponseFromDatabase == "COMPANY REGISTRATION SUCCESSFUL")
                {
                    sqlConnection.Close();
                    result = true;
                    return result;
                }

                // Else (Hence returned database result does not match the expected message, set the ersult to false and return it to the calling method.
                else
                {
                    result = false;
                    return result;
                }
            }

            // Catch potential exceptions, output them to console and set thte boolean result to false.
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return result = false;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetCompanyDetailsByRecruiterUsername</c> Retrieves comapny details/record by Recruiter Username. Since a recruiter is related to a single company and
        /// vice-verse, only a single company record is returned. This method also uses a stored procedure to query the database for the company's details/record.
        /// </summary>
        public CompanyModel GetCompanyDetailsByRecruiterUsername(CompanyModel companyModel)
        {
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand("usp_GetCompanyByRecruiterUsername", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@RecruiterUserName", companyModel.RecruiterSignInEmailAddress);

                // Set each parameter returned from the database and add to SQL command.
                // Reference https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/configuring-parameters-and-parameter-data-types

                // Company Name Parameter
                SqlParameter companyName = new SqlParameter();
                companyName.ParameterName = "@CompanyName";
                companyName.DbType = DbType.String;
                companyName.Size = 256;
                companyName.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(companyName);

                // Company Contact Email Address Parameter
                SqlParameter companyContactEmailAddress = new SqlParameter();
                companyContactEmailAddress.ParameterName = "@CompanyContactEmailAddress";
                companyContactEmailAddress.DbType = DbType.String;
                companyContactEmailAddress.Size = 256;
                companyContactEmailAddress.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(companyContactEmailAddress);

                // Company Industry ID
                SqlParameter industryID = new SqlParameter();
                industryID.ParameterName = "@IndustryID";
                industryID.DbType = DbType.Int32;
                industryID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(industryID);

                // Company Town ID
                SqlParameter townID = new SqlParameter();
                townID.ParameterName = "@TownID";
                townID.DbType = DbType.Int32;
                townID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(townID);

                // Company Size ID
                SqlParameter companySizeID = new SqlParameter();
                companySizeID.ParameterName = "@CompanySizeID";
                companySizeID.DbType = DbType.Int32;
                companySizeID.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(companySizeID);

                sqlConnection.Open();

                // ExecuteNonQuery primarly returns number of rows affected by the command sent to the database. However, output parameters data are also returned.
                // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=dotnet-plat-ext-6.0
                sqlCommand.ExecuteNonQuery();

                // Set the company model object with the parameter values returned from the database
                companyModel.CompanyName = sqlCommand.Parameters["@CompanyName"].Value.ToString();
                companyModel.CompanyContactEmailAddress = sqlCommand.Parameters["@CompanyContactEmailAddress"].Value.ToString();
                string IndustryID = sqlCommand.Parameters["@IndustryID"].Value.ToString();
                companyModel.IndustryID = (Convert.ToInt32(IndustryID));
                companyModel.TownID = Convert.ToInt32(sqlCommand.Parameters["@TownID"].Value.ToString());
                companyModel.CompanySizeID = Convert.ToInt32(sqlCommand.Parameters["@CompanySizeID"].Value.ToString());

                // Get Company Logo File Bytes using the GetFileBytesByRecruiterUserName
                companyModel.FileBytes = GetFileBytesByRecruiterUsername(companyModel.RecruiterSignInEmailAddress);

                sqlConnection.Close();

                return companyModel;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                companyModel.FileBytes = GetFileBytesByRecruiterUsername(companyModel.RecruiterSignInEmailAddress);
                return companyModel;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetFileBytesByRecruiterUsername</c> retrieves Company Logo File Bytes. It uses a Transact-SQL Statement instead of a Stored Procedure.
        /// Based on example - https://stackoverflow.com/questions/7724550/retrieve-varbinarymax-from-sql-server-to-byte-in-c-sharp
        /// </summary>
        public byte[] GetFileBytesByRecruiterUsername(string RecruiterUsername)
        {

            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);
                sqlConnection.Open();

                // SQL Query
                string sqlquery = "SELECT LogoFileBlob FROM Company WHERE SignInEmailAddress = '" + RecruiterUsername + "'";

                SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@RecruiterUserName", RecruiterUsername);


                // Execute Scalar - https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executescalar?view=dotnet-plat-ext-6.0
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

        /// <summary>
        /// Method <c>UpdateCompany</c> handles data access layer responsibility for the Update Company functionality.
        /// First it gets the fileBytes of the logo file found in the Company Model Object, and then uses the UpdateCompanyRecordInDatabase method to update the company
        /// record by using a database stored procedure.
        /// </summary>
        public CompanyModel UpdateCompany(CompanyModel companyModel)
        {
            byte[] fileBytes = new byte[companyModel.CompanyLogoFile.Length];
            fileBytes = GetLogoFileBytes(companyModel);

            companyModel.FileBytes = fileBytes;
            companyModel = UpdateCompanyRecordInDatabase(companyModel);

            return (companyModel);
        }

        /// <summary>
        /// Method <c>UpdateCompanyRecordInDatabase</c> handles data access layer's interaction with the database to update a company record.
        /// </summary>
        public CompanyModel UpdateCompanyRecordInDatabase(CompanyModel companyModel)
        {
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand("usp_UpdateCompany", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Set the SqlCommand's parameters with the CompanyModel object.
                sqlCommand.Parameters.AddWithValue("@CompanyName", companyModel.CompanyName);
                sqlCommand.Parameters.AddWithValue("@RecruiterSignInEmailAddress", companyModel.RecruiterSignInEmailAddress);
                sqlCommand.Parameters.AddWithValue("@CompanyContactEmailAddress", companyModel.CompanyContactEmailAddress);
                sqlCommand.Parameters.AddWithValue("@LogoFileBytes", companyModel.FileBytes);
                sqlCommand.Parameters.AddWithValue("@CompanyTownID", companyModel.TownID);
                sqlCommand.Parameters.AddWithValue("@CompanyIndustryID", companyModel.IndustryID);
                sqlCommand.Parameters.AddWithValue("@CompanySizeID", companyModel.CompanySizeID);

                sqlConnection.Open();

                string ResultFromDatabase = sqlCommand.ExecuteScalar().ToString();

                if (ResultFromDatabase == "COMPANY UPDATE SUCCESSFUL")
                {
                    // This parameter is used to display a successful Company Update feedback message in the view for the user.
                    companyModel.CompanyUpdateAlertID = 1;
                }

                else
                {
                    // This parameter is used to display a failed Company Update feedback message in the view for the user.
                    companyModel.CompanyUpdateAlertID = 2;
                }

                return companyModel;
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());

                // This parameter is used to display a failed Company Update feedback message in the view for the user.
                companyModel.CompanyUpdateAlertID = 2;

                return companyModel;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method <c>GetLogoFileBytes</c> uses MemoryStream to read the Company Logo file and puts them into an array of type bytes.
        /// Based on example - https://stackoverflow.com/questions/56914414/inputstream-in-asp-net-core-mvc
        /// </summary>
        public byte[] GetLogoFileBytes(CompanyModel companyModel)
        {
            byte[] bufferTemp = new byte[companyModel.CompanyLogoFile.Length];

            using (var memoryStream = new MemoryStream())
            {
                //OpensReadStream on the uploaded CompanyLogoFile of type IFormFIle and copies the stream into a memory stream.
                // References: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile.openreadstream?view=aspnetcore-6.0
                // & https://docs.microsoft.com/en-us/dotnet/api/system.io.stream.copyto?view=net-6.0
                companyModel.CompanyLogoFile.OpenReadStream().CopyTo(memoryStream);

                // Write the stream to a byte array
                var tempMemoryStream = memoryStream.ToArray();

                // Copy the tempMemoryStream to the bufferTemp
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream.toarray?view=net-6.0
                Array.Copy(tempMemoryStream, bufferTemp, tempMemoryStream.Length);
            }

            return bufferTemp;
        }

        // Method to test file creation from file found in Model
        // This method was created to test and confirm that file bytes converted to byte array was implemented properly.
        // Source: https://stackoverflow.com/questions/381508/can-a-byte-array-be-written-to-a-file-in-c
        public void GetFileWithBinaryReaderUsingFile(CompanyModel companyModel)
        {
            Stream fileStream = companyModel.CompanyLogoFile.OpenReadStream();
            BinaryReader fileBinaryReader = new BinaryReader(fileStream);

            byte[] testFileBytes = fileBinaryReader.ReadBytes((Int32)fileStream.Length);
            string FullFilePath = @"C:\Support\FileTest\companylogo.png";

            BinaryWriter Writer = null;

            // Create a new stream to write to the file
            Writer = new BinaryWriter(File.OpenWrite(FullFilePath));

            // Writer raw data                
            Writer.Write(testFileBytes);
            Writer.Flush();
            Writer.Close();
        }

        // Method Test to test file creation from bytes.
        // This method was created to test and confirm that file bytes converted to byte array was implemented properly.
        // Reference: https://stackoverflow.com/questions/381508/can-a-byte-array-be-written-to-a-file-in-c
        public void GetFileFromBytes(byte[] fileBytes)
        {
            string FullFilePath = @"C:\Support\FileTest\companylogo2.png";

            BinaryWriter Writer = null;
            Writer = new BinaryWriter(File.OpenWrite(FullFilePath));

            // Writer raw data                
            Writer.Write(fileBytes);
            Writer.Flush();
            Writer.Close();
        }
    }
}
