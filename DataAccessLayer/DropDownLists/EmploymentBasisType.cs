using Microsoft.Data.SqlClient;
using System.Data;

namespace RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists
{

    /// <summary>
    /// Class <c>EmploymentBasisType</c> contains a class representing an Employment Basis Type.
    /// This class is used to retrieve a list of Employment Basis Types from the database, primarly used as a drop-down list for the user to select an Employment Basis Type.
    /// This class is very similar to the AcademicEducationQualifcationLevel, and follows its same logic. Documentation and comments of that class apply to this
    /// class as well.
    /// </summary>
    public class EmploymentBasisType
    {
        public int EmploymentBasisTypeID { get; set; }
        public string EmploymentBasisTypeName { get; set; }

        public List<EmploymentBasisType> EmploymentBasisTypeList { get; set; }

        public List<EmploymentBasisType> GetEmploymentBasisList()
        {
            List<EmploymentBasisType> employmentBasisTypeList = new List<EmploymentBasisType>();

            string WebApplicationDatabaseConnectionString = "Server=Win10-Dev;Database=RECRUITMENTSYSTEMDB;Trusted_Connection=True";
            SqlConnection sqlConnection = new SqlConnection();
            
            try
            {
                SqlDataReader sqlDataReader = null;

                sqlConnection = new SqlConnection(WebApplicationDatabaseConnectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "usp_GetEmploymentBasisTypeList";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    employmentBasisTypeList.Add(new EmploymentBasisType { EmploymentBasisTypeID = -1, EmploymentBasisTypeName = "-- Select an Employment Basis Type --" });
                    while (sqlDataReader.Read())
                    {
                        employmentBasisTypeList.Add(new EmploymentBasisType
                        {
                            EmploymentBasisTypeID = Convert.ToInt32(sqlDataReader["PK_EmploymentBasisTypeID"]),
                            EmploymentBasisTypeName = Convert.ToString(sqlDataReader["Name"])
                        });
                    }
                }

                sqlConnection.Close();
                return employmentBasisTypeList;
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

                employmentBasisTypeList.Clear();
                employmentBasisTypeList.Add(new EmploymentBasisType { EmploymentBasisTypeID = -2, EmploymentBasisTypeName = "!! List not Loaded !!" });
                
                return employmentBasisTypeList;
            }

            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
