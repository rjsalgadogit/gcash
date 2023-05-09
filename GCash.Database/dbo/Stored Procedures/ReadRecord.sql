-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ReadRecord
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM record WHERE Id = @Id
END