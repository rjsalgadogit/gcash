-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DeleteRecord
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

    DELETE record WHERE Id = @Id
END