CREATE PROCEDURE [dbo].[GetCorrespondanceAddressDetails]
	@UserId UNIQUEIDENTIFIER
AS
	SELECT        
        [ad].[NameNumber],
		[ad].[AddressLine1],
		[ad].[AddressLine2],
		[ad].[City],	
		[ad].[County],	
		[ad].[Postcode]
    FROM
        [UserCorrespondanceAddress] uca
	INNER JOIN
		[Address] ad
	ON [uca].[AddressId] = [ad].Id
    WHERE
        uca.[UserId] = @UserId
