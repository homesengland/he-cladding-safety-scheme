/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\Seed\ApplicationStatus.sql
GO
:r .\Seed\ApplicationStages.sql
GO
:r .\Seed\TaskStatus.sql
GO
:r .\Seed\ApplicationRepresentationType.sql
GO
:r .\Seed\ApplicationBankDetailsRelationship.sql
GO
:r .\Seed\ApplicationResponsibleEntityType.sql
GO
:r .\Seed\ApplicationDeveloperInBusinessType.sql
GO
:r .\Seed\ApplicationFundingRoutesType.sql
GO
:r .\Seed\FireRiskAssessorList.sql
GO
:r .\Seed\ApplicationResponsibleEntityOrganisation.sql
GO
:r .\Seed\ApplicationOtherSourcesPursuedType.sql
GO
:r .\Seed\ApplicationBuildingRelationship.sql
GO
:r .\Seed\ApplicationReferenceNumber.sql
GO
:r .\Seed\ApplicationResponsibleEntityOrganisationSubType.sql
GO