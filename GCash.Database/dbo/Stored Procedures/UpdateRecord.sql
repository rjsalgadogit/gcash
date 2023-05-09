-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRecord]
	@Id int,
	@ReferenceNumber nvarchar(50),
	@Amount decimal(18, 2),
	@IsClaimed bit
AS
BEGIN
	SET NOCOUNT ON;

    IF @Id > 0
	BEGIN
		UPDATE record
		SET ReferenceNumber = @ReferenceNumber,
			Amount = @Amount,
			IsClaimed = @IsClaimed
		WHERE
			Id = @Id
	END
	ELSE
	BEGIN
		INSERT INTO record (ReferenceNumber
			, Amount
			, CreatedDate
			, CreatedBy)
		SELECT 
			@ReferenceNumber
			, @Amount
			, GETUTCDATE()
			, 1
	END
END