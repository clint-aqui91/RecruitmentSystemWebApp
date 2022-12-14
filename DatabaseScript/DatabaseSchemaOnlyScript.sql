USE [RECRUITMENTSYSTEMDB]
GO
/****** Object:  Table [dbo].[AcademicEducationLevel]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicEducationLevel](
	[PK_AcademicEducationLevelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Weight] [int] NOT NULL,
 CONSTRAINT [PK_LevelOfAcademicEducation] PRIMARY KEY CLUSTERED 
(
	[PK_AcademicEducationLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[PK_CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[SignInEmailAddress] [nvarchar](256) NOT NULL,
	[ContactEmailAddress] [nvarchar](256) NOT NULL,
	[LogoFileBLOB] [varbinary](max) NOT NULL,
	[FK_Company_TownID] [int] NOT NULL,
	[FK_Company_IndustryID] [int] NOT NULL,
	[FK_Company_CompanySizeID] [int] NOT NULL,
	[FK_Company_FileExtensionID] [int] NOT NULL,
	[ActivationStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[PK_CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanySize]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanySize](
	[PK_CompanySizeID] [int] IDENTITY(1,1) NOT NULL,
	[Range] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_CompanySize] PRIMARY KEY CLUSTERED 
(
	[PK_CompanySizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[PK_CountryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[PK_CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmploymentBasisType]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentBasisType](
	[PK_EmploymentBasisTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_EmploymentBasisType] PRIMARY KEY CLUSTERED 
(
	[PK_EmploymentBasisTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileExtension]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileExtension](
	[PK_FileExtensionID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_FileExtension] PRIMARY KEY CLUSTERED 
(
	[PK_FileExtensionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Industry]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industry](
	[PK_IndustryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED 
(
	[PK_IndustryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobApplication]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobApplication](
	[PK_JobApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[CoveringLetter] [nvarchar](400) NOT NULL,
	[CVFileBLOB] [varbinary](max) NOT NULL,
	[FK_JobApplication_JobVacancyID] [int] NOT NULL,
	[FK_JobApplication_JobseekerID] [int] NOT NULL,
	[FK_JobApplication_YearsOfExperienceID] [int] NOT NULL,
	[FK_JobApplication_AcademicEducationLevelID] [int] NOT NULL,
	[FK_JobApplication_SalaryRangeID] [int] NOT NULL,
	[FK_JobApplication_FileExtensionID] [int] NOT NULL,
	[ActivationStatus] [bit] NOT NULL,
 CONSTRAINT [PK_JobApplication] PRIMARY KEY CLUSTERED 
(
	[PK_JobApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobPreference]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobPreference](
	[PK_JobPreferenceID] [int] IDENTITY(1,1) NOT NULL,
	[FK_JobPreference_JobseekerID] [int] NOT NULL,
	[FK_JobPreference_JobSpecializationID] [int] NOT NULL,
 CONSTRAINT [PK_JobPreference] PRIMARY KEY CLUSTERED 
(
	[PK_JobPreferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobseeker]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobseeker](
	[PK_JobseekerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Surname] [nvarchar](256) NOT NULL,
	[EmailAddress] [nvarchar](256) NOT NULL,
	[ActivationStatus] [bit] NOT NULL,
 CONSTRAINT [PK_JobSeeker] PRIMARY KEY CLUSTERED 
(
	[PK_JobseekerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobSpecialization]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobSpecialization](
	[PK_JobSpecializationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_JobSpecialization] PRIMARY KEY CLUSTERED 
(
	[PK_JobSpecializationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobVacancy]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobVacancy](
	[PK_JobVacancyID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](400) NOT NULL,
	[FK_JobVacancy_CompanyID] [int] NOT NULL,
	[FK_JobVacancy_JobSpecializationID] [int] NOT NULL,
	[FK_JobVacancy_EmploymentBasisTypeID] [int] NOT NULL,
	[FK_JobVacancy_YearsOfExperienceID] [int] NOT NULL,
	[FK_JobVacancy_AcademicEducationLevelID] [int] NOT NULL,
	[FK_JobVacancy_SalaryRangeID] [int] NOT NULL,
	[FK_JobVacancy_TownID] [int] NOT NULL,
	[ActivationStatus] [bit] NOT NULL,
 CONSTRAINT [PK_JobVacancy] PRIMARY KEY CLUSTERED 
(
	[PK_JobVacancyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryRange]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryRange](
	[PK_SalaryRangeID] [int] IDENTITY(1,1) NOT NULL,
	[Range] [nvarchar](256) NOT NULL,
	[Weight] [int] NOT NULL,
 CONSTRAINT [PK_SalaryRange] PRIMARY KEY CLUSTERED 
(
	[PK_SalaryRangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Town]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Town](
	[PK_TownID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[FK_Town_CountryID] [int] NOT NULL,
 CONSTRAINT [PK_Town] PRIMARY KEY CLUSTERED 
(
	[PK_TownID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YearsOfExperience]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearsOfExperience](
	[PK_YearsOfExperienceID] [int] IDENTITY(1,1) NOT NULL,
	[Range] [nvarchar](256) NOT NULL,
	[RangeWeight] [int] NOT NULL,
 CONSTRAINT [PK_YearsOfExperience] PRIMARY KEY CLUSTERED 
(
	[PK_YearsOfExperienceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_CompanySize] FOREIGN KEY([FK_Company_CompanySizeID])
REFERENCES [dbo].[CompanySize] ([PK_CompanySizeID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_CompanySize]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_FileExtension] FOREIGN KEY([FK_Company_FileExtensionID])
REFERENCES [dbo].[FileExtension] ([PK_FileExtensionID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_FileExtension]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Industry] FOREIGN KEY([FK_Company_IndustryID])
REFERENCES [dbo].[Industry] ([PK_IndustryID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Industry]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Town] FOREIGN KEY([FK_Company_TownID])
REFERENCES [dbo].[Town] ([PK_TownID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Town]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD  CONSTRAINT [FK_JobApplication_AcademicEducationLevel] FOREIGN KEY([FK_JobApplication_AcademicEducationLevelID])
REFERENCES [dbo].[AcademicEducationLevel] ([PK_AcademicEducationLevelID])
GO
ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_AcademicEducationLevel]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD  CONSTRAINT [FK_JobApplication_FileExtension] FOREIGN KEY([FK_JobApplication_FileExtensionID])
REFERENCES [dbo].[FileExtension] ([PK_FileExtensionID])
GO
ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_FileExtension]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD  CONSTRAINT [FK_JobApplication_Jobseeker] FOREIGN KEY([FK_JobApplication_JobseekerID])
REFERENCES [dbo].[Jobseeker] ([PK_JobseekerID])
GO
ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_Jobseeker]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD  CONSTRAINT [FK_JobApplication_JobVacancy] FOREIGN KEY([FK_JobApplication_JobVacancyID])
REFERENCES [dbo].[JobVacancy] ([PK_JobVacancyID])
GO
ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_JobVacancy]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD  CONSTRAINT [FK_JobApplication_SalaryRange] FOREIGN KEY([FK_JobApplication_SalaryRangeID])
REFERENCES [dbo].[SalaryRange] ([PK_SalaryRangeID])
GO
ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_SalaryRange]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD  CONSTRAINT [FK_JobApplication_YearsOfExperience] FOREIGN KEY([FK_JobApplication_YearsOfExperienceID])
REFERENCES [dbo].[YearsOfExperience] ([PK_YearsOfExperienceID])
GO
ALTER TABLE [dbo].[JobApplication] CHECK CONSTRAINT [FK_JobApplication_YearsOfExperience]
GO
ALTER TABLE [dbo].[JobPreference]  WITH CHECK ADD  CONSTRAINT [FK_JobPreference_Jobseeker] FOREIGN KEY([FK_JobPreference_JobseekerID])
REFERENCES [dbo].[Jobseeker] ([PK_JobseekerID])
GO
ALTER TABLE [dbo].[JobPreference] CHECK CONSTRAINT [FK_JobPreference_Jobseeker]
GO
ALTER TABLE [dbo].[JobPreference]  WITH CHECK ADD  CONSTRAINT [FK_JobPreference_JobSpecialization] FOREIGN KEY([FK_JobPreference_JobSpecializationID])
REFERENCES [dbo].[JobSpecialization] ([PK_JobSpecializationID])
GO
ALTER TABLE [dbo].[JobPreference] CHECK CONSTRAINT [FK_JobPreference_JobSpecialization]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_AcademicEducationLevel] FOREIGN KEY([FK_JobVacancy_AcademicEducationLevelID])
REFERENCES [dbo].[AcademicEducationLevel] ([PK_AcademicEducationLevelID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_AcademicEducationLevel]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_Company] FOREIGN KEY([FK_JobVacancy_CompanyID])
REFERENCES [dbo].[Company] ([PK_CompanyID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_Company]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_EmploymentBasisType] FOREIGN KEY([FK_JobVacancy_EmploymentBasisTypeID])
REFERENCES [dbo].[EmploymentBasisType] ([PK_EmploymentBasisTypeID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_EmploymentBasisType]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_JobSpecialization] FOREIGN KEY([FK_JobVacancy_JobSpecializationID])
REFERENCES [dbo].[JobSpecialization] ([PK_JobSpecializationID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_JobSpecialization]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_JobVacancy] FOREIGN KEY([FK_JobVacancy_TownID])
REFERENCES [dbo].[Town] ([PK_TownID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_JobVacancy]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_SalaryRange] FOREIGN KEY([FK_JobVacancy_SalaryRangeID])
REFERENCES [dbo].[SalaryRange] ([PK_SalaryRangeID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_SalaryRange]
GO
ALTER TABLE [dbo].[JobVacancy]  WITH CHECK ADD  CONSTRAINT [FK_JobVacancy_YearsOfExperience] FOREIGN KEY([FK_JobVacancy_YearsOfExperienceID])
REFERENCES [dbo].[YearsOfExperience] ([PK_YearsOfExperienceID])
GO
ALTER TABLE [dbo].[JobVacancy] CHECK CONSTRAINT [FK_JobVacancy_YearsOfExperience]
GO
ALTER TABLE [dbo].[Town]  WITH CHECK ADD  CONSTRAINT [FK_Town_Country] FOREIGN KEY([FK_Town_CountryID])
REFERENCES [dbo].[Country] ([PK_CountryID])
GO
ALTER TABLE [dbo].[Town] CHECK CONSTRAINT [FK_Town_Country]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckJobApplicationExistence]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CheckJobApplicationExistence] 
	@JobVacancyID INT,
	@JobseekerUserName NVARCHAR(30),
	@Occurrence INT = 0
	
/*
<documentation>
    <summary>Checks whether a Job Application Record with the supplied JobVacancyID
	and JobseekerUserName (gets the Jobseeker's Primary Key from the supplied
	parameter value)</summary>
    <returns>SELECT statement based on the query's count result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>20-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT @Occurrence = COUNT(*)

FROM JobApplication

WHERE (JobApplication.FK_JobApplication_JobVacancyID = @JobVacancyID) AND 
(JobApplication.FK_JobApplication_JobseekerID = (SELECT Jobseeker.PK_JobseekerID FROM Jobseeker WHERE Jobseeker.EmailAddress = @JobseekerUserName))

END

IF (@Occurrence != 0)
	BEGIN
	SELECT 'JOB APPLICATION BY THE SAME JOBSEEKER FOR THE SAME JOB VACANCY ALREADY EXISTS'
END

ELSE
	BEGIN
	SELECT 'JOB APPLICATION BY THE SAME JOBSEEKER FOR THE SAME JOB VACANCY DOES NOT EXISTS'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateCompany]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_CreateCompany] 
	@CompanyName NVARCHAR(30),
	@RecruiterSignInEmailAddress NVARCHAR(30),
	@CompanyContactEmailAddress NVARCHAR(30),
	@LogoFileBytes VARBINARY(MAX),
	@CompanyTownID INT,
	@CompanyIndustryID INT,
	@CompanySizeID INT
	
/*
<documentation>
    <summary>Creates a Company Record with the provided parameters</summary>
    <returns>SELECT statement based on the query's result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>11-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.3</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

INSERT INTO Company
	(Name,
	SignInEmailAddress,
	ContactEmailAddress,
	LogoFileBLOB,
	FK_Company_TownID,
	FK_Company_IndustryID,
	FK_Company_CompanySizeID,
	FK_Company_FileExtensionID,
	ActivationStatus)

VALUES
	(@CompanyName,
	@RecruiterSignInEmailAddress,
	@CompanyContactEmailAddress,
	@LogoFileBytes,
	@CompanyTownID,
	@CompanyIndustryID, 
	@CompanySizeID,
	(SELECT FileExtension.PK_FileExtensionID FROM FileExtension WHERE FileExtension.Type = '.png'),
	1)

IF(@@ROWCOUNT >0)
	BEGIN
	SELECT 'COMPANY REGISTRATION SUCCESSFUL'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateJobApplication]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CreateJobApplication] 

	@JobVacancyID INT,
	@JobseekerUserName NVARCHAR(256),
	@CVFileBytes VARBINARY(MAX),
	@YearsOfExperienceID INT,
	@AcademicEducationQualificationLevelID INT,
	@PreferredSalaryrangeID INT,
	@CoveringLetter NVARCHAR(400)
	
/*
<documentation>
    <summary>Creates a Job Application record with the provided parameters</summary>
    <returns>SELECT statement based on the query's result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>11-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.3</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

INSERT INTO JobApplication
	(FK_JobApplication_JobVacancyID,
	FK_JobApplication_JobseekerID,
	CVFileBLOB,
	FK_JobApplication_YearsOfExperienceID,
	FK_JobApplication_AcademicEducationLevelID,
	FK_JobApplication_SalaryRangeID,
	CoveringLetter,
	FK_JobApplication_FileExtensionID,
	ActivationStatus)

VALUES
	(@JobVacancyID,
	(SELECT Jobseeker.PK_JobseekerID FROM Jobseeker WHERE Jobseeker.EmailAddress= @JobseekerUserName),
	@CVFileBytes,
	@YearsOfExperienceID,
	@AcademicEducationQualificationLevelID,
	@PreferredSalaryrangeID,
	@CoveringLetter,
	1,
	1)

IF(@@ROWCOUNT >0)
	BEGIN
	SELECT 'SUCCESSFUL JOB APPLICATION CREATION'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateJobseeker]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CreateJobseeker] 
	@JobseekerName NVARCHAR(30),
	@JobseekerSurname NVARCHAR(30),
	@JobseekerEmailAddress NVARCHAR(30)

/*
<documentation>
    <summary>Inserts a Jobseeker record with the provided parameters</summary>
    <returns>SELECT statement based on the query's result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>17-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.2</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

INSERT INTO Jobseeker
	(Name,
	Surname,
	EmailAddress,
	ActivationStatus)
	
VALUES
	(@JobseekerName,
	@JobseekerSurname,
	@JobseekerEmailAddress,
	1)

IF(@@ROWCOUNT >0)
	BEGIN
	SELECT 'JOBSEEKER REGISTRATION SUCCESSFUL'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateJobVacancy]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CreateJobVacancy] 
	@JobVacancyTitle NVARCHAR(256),
	@JobVacancyDescription NVARCHAR(256),
	@RecruiterUserName NVARCHAR(400),
	@JobSpecializationID NVARCHAR(30),
	@JobVacancyLocationID INT,
	@EmploymentBasisTypeID INT,
	@RequiredYearsOfWorkingExperienceRangeID INT,
	@RequiredAcademicEducationQualificationLevelID INT,
	@OfferedSalaryRangeID INT
	
/*
<documentation>
    <summary>Inserts a Job Vacancy record with the provided parameters</summary>
    <returns>SELECT statement based on the query's result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>17-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/
	
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO JobVacancy
	(Title,
	Description,
	FK_JobVacancy_CompanyID,
	FK_JobVacancy_JobSpecializationID,
	FK_JobVacancy_TownID,
	FK_JobVacancy_EmploymentBasisTypeID,
	FK_JobVacancy_YearsOfExperienceID,
	FK_JobVacancy_AcademicEducationLevelID,
	FK_JobVacancy_SalaryRangeID,
	ActivationStatus)
	
VALUES
	(@JobVacancyTitle,
	@JobVacancyDescription,
	(SELECT Company.PK_CompanyID FROM Company WHERE Company.SignInEmailAddress = @RecruiterUserName),
	@JobSpecializationID,
	@JobVacancyLocationID,
	@EmploymentBasisTypeID,
	@RequiredYearsOfWorkingExperienceRangeID,
	@RequiredAcademicEducationQualificationLevelID,
	@OfferedSalaryRangeID,
	1)

IF(@@ROWCOUNT >0)
	BEGIN
	SELECT 'SUCCESSFUL JOB VACANCY CREATION'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteJobVacancy]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_DeleteJobVacancy] 
	@JobVacancyID INT

/*
<documentation>
    <summary>Deletes a Job Vacancy record with the supplied Job Vacancy ID. Deletion is achieved
	by updating the job vacany record's ActivationStatus to 0, to preserve data integrity and prevent
	issues with other tables having a relationship through the deleted record's primary key (as a
	foreign key).</summary>
    <returns>SELECT statement based on the query's result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>22-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

    
UPDATE JobVacancy
	
SET
	JobVacancy.ActivationStatus = 0

WHERE (JobVacancy.PK_JobVacancyID = @JobVacancyID)

IF(@@ROWCOUNT >0)
	BEGIN
	SELECT 'SUCCESSFUL JOB VACANCY DELETION'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAcademicEducationLevelList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetAcademicEducationLevelList] 

/*
<documentation>
    <summary>Gets records from the AcademicEducationLevel table.</summary>
    <returns>All records from the AcademicEducationLevel</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>14-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	AcademicEducationLevel.PK_AcademicEducationLevelID,
	AcademicEducationLevel.Name

FROM AcademicEducationLevel

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCompanyByRecruiterUsername]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetCompanyByRecruiterUsername] 

	@RecruiterUserName NVARCHAR(256),
	@CompanyName NVARCHAR(256) OUTPUT,
	@CompanyContactEmailAddress NVARCHAR(256) OUTPUT,
	@IndustryID BIGINT OUTPUT,
	@TownID BIGINT OUTPUT,
	@CompanySizeID BIGINT OUTPUT

/*
<documentation>
    <summary>Gets a company record by its ID (with the supplied Recruiter Username).</summary>
    <returns>A Company record by the provided recruiter username.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>13-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	@CompanyName = Company.Name,
	@CompanyContactEmailAddress = Company.ContactEmailAddress,
	@IndustryID = Company.FK_Company_IndustryID,
	@TownID = Company.FK_Company_TownID,
	@CompanySizeID = Company.FK_Company_CompanySizeID
	
FROM Company
	
WHERE (Company.SignInEmailAddress = @RecruiterUserName)

END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetCompanySizeList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetCompanySizeList] 

/*
<documentation>
    <summary>Gets records from the CompanySize table.</summary>
    <returns>All records from the CompanySize table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>05-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	CompanySize.PK_CompanySizeID,
	CompanySize.Range

FROM CompanySize

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEmploymentBasisTypeList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetEmploymentBasisTypeList] 

/*
<documentation>
    <summary>Gets records from the EmploymentBasisType table.</summary>
    <returns>All records from the EmploymentBasisType table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>05-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	EmploymentBasisType.PK_EmploymentBasisTypeID,
	EmploymentBasisType.Name

FROM EmploymentBasisType

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetIndustryList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetIndustryList] 

/*
<documentation>
    <summary>Gets records from the Industry table.</summary>
    <returns>All records from the Industry table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>05-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	Industry.PK_IndustryID,
	Industry.Name
	
FROM Industry

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetJobSpecializationList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetJobSpecializationList] 

/*
<documentation>
    <summary>Gets records from the JobSpecialization table.</summary>
    <returns>All records from the JobSpecialization table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>14-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	JobSpecialization.PK_JobSpecializationID,
	JobSpecialization.Name

FROM JobSpecialization

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetJobVacancyByJobVacancyID]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetJobVacancyByJobVacancyID] 

	@JobVacancyID BIGINT,
	@JobVacancyTitle NVARCHAR(256) OUTPUT,
	@JobVacancyDescription NVARCHAR(256) OUTPUT,
	@JobSpecializationName NVARCHAR(256) OUTPUT,
	@JobVacancyLocationName NVARCHAR(256) OUTPUT,
	@OfferedSalaryRangeValue NVARCHAR(256) OUTPUT,
	@RequiredAcademicEducationQualificationLevelName NVARCHAR(256) OUTPUT,
	@EmploymentBasisTypeName NVARCHAR(256) OUTPUT,
	@RequiredYearsOfExperienceRangeValue NVARCHAR(256) OUTPUT,

	@CompanyID BIGINT OUTPUT,
	@CompanyName NVARCHAR(256) OUTPUT,
	@CompanyContactEmailAddress NVARCHAR(256) OUTPUT,
	@CompanyIndustryName NVARCHAR(256) OUTPUT,
	@CompanySizeRange NVARCHAR(256) OUTPUT,
	@CompanyHeadQuartersLocation NVARCHAR(256) OUTPUT,

	@JobSpecializationID BIGINT OUTPUT,
	@JobVacancyLocationID BIGINT OUTPUT,
	@EmploymentBasisTypeID BIGINT OUTPUT,
	@RequiredYearsOfWorkingExperienceRangeID BIGINT OUTPUT,
	@RequiredAcademicEducationQualificationLevelID BIGINT OUTPUT,
	@OfferedSalaryRangeID BIGINT OUTPUT

/*
<documentation>
    <summary>Gets a job vacancy's details by the provided Job Vacancy ID. It returns foreign keys and
	values from the related table.</summary>
    <returns>A job vacancy record along with values from related tables.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>18-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	/* Job Vacancy Details */
	@JobVacancyTitle = JobVacancy.Title,
	@JobVacancyDescription = JobVacancy.Description,
	@JobSpecializationName = JobSpecialization.Name,
	@JobVacancyLocationName = JobVacancyTownName.Name,
	@OfferedSalaryRangeValue = SalaryRange.Range,
	@RequiredAcademicEducationQualificationLevelName = AcademicEducationLevel.Name,
	@EmploymentBasisTypeName = EmploymentBasisType.Name,
	@RequiredYearsOfExperienceRangeValue = YearsOfExperience.Range,

	/* Foreign Keys */
	@JobSpecializationID = JobVacancy.FK_JobVacancy_JobSpecializationID,
	@JobVacancyLocationID = JobVacancy.FK_JobVacancy_TownID,
	@EmploymentBasisTypeID  = JobVacancy.FK_JobVacancy_EmploymentBasisTypeID,
	@RequiredYearsOfWorkingExperienceRangeID = JobVacancy.FK_JobVacancy_YearsOfExperienceID,
	@RequiredAcademicEducationQualificationLevelID = JobVacancy.FK_JobVacancy_AcademicEducationLevelID,
	@OfferedSalaryRangeID = JobVacancy.FK_JobVacancy_SalaryRangeID,
	@CompanyID = JobVacancy.FK_JobVacancy_CompanyID,

	/* Company Details (from Company record with matching Foreign Key from Job Vacancy Table */
	@CompanyName = Company.Name,
	@CompanyContactEmailAddress = Company.ContactEmailAddress,
	@CompanyIndustryName = Industry.Name,
	@CompanySizeRange = CompanySize.Range,
	@CompanyHeadQuartersLocation = CompanyHeadQuartersLocation.Name
	
FROM JobVacancy
	JOIN Town AS JobVacancyTownName ON (JobVacancyTownName.PK_TownID = JobVacancy.FK_JobVacancy_TownID)
	JOIN JobSpecialization ON (JobSpecialization.PK_JobSpecializationID = JobVacancy.FK_JobVacancy_JobSpecializationID)
	JOIN SalaryRange ON (SalaryRange.PK_SalaryRangeID = JobVacancy.FK_JobVacancy_SalaryRangeID)
	JOIN YearsOfExperience ON (YearsOfExperience.PK_YearsOfExperienceID = JobVacancy.FK_JobVacancy_YearsOfExperienceID)
	JOIN EmploymentBasisType ON (EmploymentBasisType.PK_EmploymentBasisTypeID = JobVacancy.FK_JobVacancy_EmploymentBasisTypeID)
	JOIN AcademicEducationLevel ON (AcademicEducationLevel.PK_AcademicEducationLevelID = JobVacancy.FK_JobVacancy_AcademicEducationLevelID)
	JOIN Company ON (Company.PK_CompanyID = JobVacancy.FK_JobVacancy_CompanyID)
	JOIN Industry ON (Industry.PK_IndustryID = Company.FK_Company_IndustryID)
	JOIN CompanySize ON (CompanySize.PK_CompanySizeID = Company.FK_Company_CompanySizeID)
	INNER JOIN Town AS CompanyHeadQuartersLocation ON (CompanyHeadQuartersLocation.PK_TownID = Company.FK_Company_TownID)

WHERE (JobVacancy.PK_JobVacancyID = @JobVacancyID)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetJobVacancyList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetJobVacancyList] 

/*
<documentation>
    <summary>Gets all job vacancy records from the JobVacancy table, which have the property
	ActivationStatus set to TRUE/1.</summary>
    <returns>All job vacancy records which have the ActivationStatus property to TRUE/1.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>15-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	JobVacancy.PK_JobVacancyID AS JobVacancyID,
	JobVacancy.Title AS JobVacancyTitle,
	JobVacancy.Description AS JobVacancyDescription,
	Town.Name AS JobLocationTownName,
	SalaryRange.Range AS OfferedSalaryRange,
	YearsOfExperience.Range AS RequiredYearsOfExperienceRange,
	EmploymentBasisType.Name AS EmploymentBasisTypeName,
	AcademicEducationLevel.Name AS RequiredAcademicEducationQualificationLevel,
	JobVacancy.FK_JobVacancy_CompanyID AS CompanyID,
	Company.Name AS CompanyName,
	Industry.Name AS CompanyIndustryName,
	CompanySize.Range AS CompanySizeRange,
	CompanyTown.Name AS CompanyHeadQuartersLocation
	
FROM JobVacancy 
	JOIN Town ON (Town.PK_TownID = JobVacancy.FK_JobVacancy_TownID)
	JOIN SalaryRange ON (SalaryRange.PK_SalaryRangeID = JobVacancy.FK_JobVacancy_SalaryRangeID)
	JOIN YearsOfExperience ON (YearsOfExperience.PK_YearsOfExperienceID = JobVacancy.FK_JobVacancy_YearsOfExperienceID)
	JOIN EmploymentBasisType ON (EmploymentBasisType.PK_EmploymentBasisTypeID = JobVacancy.FK_JobVacancy_EmploymentBasisTypeID)
	JOIN AcademicEducationLevel ON (AcademicEducationLevel.PK_AcademicEducationLevelID = JobVacancy.FK_JobVacancy_AcademicEducationLevelID)
	JOIN Company ON (Company.PK_CompanyID = JobVacancy.FK_JobVacancy_CompanyID)
	JOIN Industry ON (Industry.PK_IndustryID = Company.FK_Company_IndustryID)
	JOIN CompanySize ON (CompanySize.PK_CompanySizeID = Company.FK_Company_CompanySizeID)
	INNER JOIN Town AS CompanyTown ON (CompanyTown.PK_TownID = Company.FK_Company_TownID)

	WHERE JobVacancy.ActivationStatus = 'TRUE'

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetJobVacancyListByCompanyID]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetJobVacancyListByCompanyID] 
	
	@RecruiterUsername NVARCHAR(256)

/*
<documentation>
    <summary>Gets job vacancy records with the propery ActivationStatus set to True/1, by CompanyID.
	RecruiterUsername is provided, and CompanyID is retrieved by that value.</summary>
    <returns>Job Vacancy records with ActivationStatus Set to True, by a CompanyID.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>20-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	JobVacancy.PK_JobVacancyID AS JobVacancyID,
	JobVacancy.Title AS JobVacancyTitle,
	JobVacancy.Description AS JobVacancyDescription,
	Town.Name AS JobLocationTownName,
	SalaryRange.Range AS OfferedSalaryRange,
	YearsOfExperience.Range AS RequiredYearsOfExperienceRange,
	EmploymentBasisType.Name AS EmploymentBasisTypeName,
	AcademicEducationLevel.Name AS RequiredAcademicEducationQualificationLevel,
	JobVacancy.FK_JobVacancy_CompanyID AS CompanyID,
	Company.Name AS CompanyName,
	Industry.Name AS CompanyIndustryName,
	CompanySize.Range AS CompanySizeRange,
	CompanyTown.Name AS CompanyHeadQuartersLocation

	
FROM JobVacancy 
	JOIN Town ON (Town.PK_TownID = JobVacancy.FK_JobVacancy_TownID)
	JOIN SalaryRange ON (SalaryRange.PK_SalaryRangeID = JobVacancy.FK_JobVacancy_SalaryRangeID)--Company.PK_CompanyID
	JOIN YearsOfExperience ON (YearsOfExperience.PK_YearsOfExperienceID = JobVacancy.FK_JobVacancy_YearsOfExperienceID)
	JOIN EmploymentBasisType ON (EmploymentBasisType.PK_EmploymentBasisTypeID = JobVacancy.FK_JobVacancy_EmploymentBasisTypeID)
	JOIN AcademicEducationLevel ON (AcademicEducationLevel.PK_AcademicEducationLevelID = JobVacancy.FK_JobVacancy_AcademicEducationLevelID)
	JOIN Company ON (Company.PK_CompanyID = JobVacancy.FK_JobVacancy_CompanyID)
	JOIN Industry ON (Industry.PK_IndustryID = Company.FK_Company_IndustryID)
	JOIN CompanySize ON (CompanySize.PK_CompanySizeID = Company.FK_Company_CompanySizeID)
	INNER JOIN Town AS CompanyTown ON (CompanyTown.PK_TownID = Company.FK_Company_TownID)

WHERE (JobVacancy.ActivationStatus = 'TRUE') AND
	(JobVacancy.FK_JobVacancy_CompanyID = (SELECT Company.PK_CompanyID FROM Company WHERE Company.SignInEmailAddress = @RecruiterUsername))

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSalaryRangeList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetSalaryRangeList] 

/*
<documentation>
    <summary>Gets records from the SalaryRange table.</summary>
    <returns>All records from the SalaryRange table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>14-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	SalaryRange.PK_SalaryRangeID,
	SalaryRange.Range

FROM SalaryRange

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetShortlistedJobApplicationListByVacancyID]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetShortlistedJobApplicationListByVacancyID] 

	@JobVacancyID INT

/*
<documentation>
    <summary>Gets shortlisted job application records (where specific foreign keys match those of a specified
	job vacancy record by its ID).</summary>
    <returns>Job Application records which have matching foreign key values with those of a specific
	job vacancy.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>21-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;
    
SELECT
	JobApplication.PK_JobApplicationID AS JobApplicationID,
	Jobseeker.Name AS JobseekerName,
	Jobseeker.Surname AS JobseekerSurname,
	Jobseeker.EmailAddress AS JobseekerContactEmailAddress,
	JobApplication.CoveringLetter AS JobApplicationCoveringLetter,
	YearsOfExperience.Range AS PossessedYearsOfExperienceRange,
	AcademicEducationLevel.Name AS PossessedAcademicEducationQualificationLevel,
	SalaryRange.Range AS PreferredSalaryRange
	
FROM JobApplication
	JOIN Jobseeker ON (Jobseeker.PK_JobseekerID = JobApplication.FK_JobApplication_JobseekerID)
	JOIN YearsOfExperience ON (YearsOfExperience.PK_YearsOfExperienceID = JobApplication.FK_JobApplication_YearsOfExperienceID)
	JOIN AcademicEducationLevel ON (AcademicEducationLevel.PK_AcademicEducationLevelID = JobApplication.FK_JobApplication_AcademicEducationLevelID)
	JOIN SalaryRange ON (SalaryRange.PK_SalaryRangeID = JobApplication.FK_JobApplication_SalaryRangeID)
	JOIN JobVacancy ON (JobVacancy.PK_JobVacancyID = JobApplication.FK_JobApplication_JobVacancyID)

WHERE (JobApplication.FK_JobApplication_JobVacancyID = @JobVacancyID) AND
	(JobApplication.FK_JobApplication_YearsOfExperienceID = JobVacancy.FK_JobVacancy_YearsOfExperienceID) AND
	(JobApplication.FK_JobApplication_AcademicEducationLevelID = JobVacancy.FK_JobVacancy_AcademicEducationLevelID) AND
	(JobApplication.FK_JobApplication_SalaryRangeID = JobVacancy.FK_JobVacancy_SalaryRangeID)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTownList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetTownList] 

/*
<documentation>
    <summary>Gets records from the Town table.</summary>
    <returns>All records from the Town table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>05-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	Town.PK_TownID,
	Town.Name

FROM Town

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetYearsOfExperienceList]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetYearsOfExperienceList] 

/*
<documentation>
    <summary>Gets records from the YearsOfExperience table.</summary>
    <returns>All records from the YearsOfExperience table.</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>14-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

SELECT
	YearsOfExperience.PK_YearsOfExperienceID,
	YearsOfExperience.Range
	
FROM YearsOfExperience

END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateCompany]    Script Date: 16/10/2022 13:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateCompany] 
	
	@CompanyName NVARCHAR(256),
	@RecruiterSignInEmailAddress NVARCHAR(256),
	@CompanyContactEmailAddress NVARCHAR(256),
	@LogoFileBytes VARBINARY(MAX),
	@CompanyTownID INT,
	@CompanyIndustryID INT,
	@CompanySizeID INT

/*
<documentation>
    <summary>Updates a Company Record with the matching provided Recruiter Username.</summary>
    <returns>SELECT statement based on the query's result</returns>
    <issues>No</issues>
    <author>Clinton Aquilina</author>
    <created>13-07-2022</created>
    <modified>02-08-2022</modified>
    <version>1.1</version>
    <sourceLink>-</sourceLink>
</documentation>
*/

AS
BEGIN
SET NOCOUNT ON;

UPDATE Company
	
SET
	Company.Name = @CompanyName,
	Company.ContactEmailAddress = @CompanyContactEmailAddress,
	Company.LogoFileBLOB = @LogoFileBytes,
	Company.FK_Company_TownID = @CompanyTownID,
	Company.FK_Company_IndustryID = @CompanyIndustryID,
	Company.FK_Company_CompanySizeID = @CompanySizeID

WHERE (Company.SignInEmailAddress = @RecruiterSignInEmailAddress)

IF(@@ROWCOUNT >0)
	BEGIN
	SELECT 'COMPANY UPDATE SUCCESSFUL'
	END
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Column to hold country name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Country', @level2type=N'COLUMN',@level2name=N'Name'
GO
