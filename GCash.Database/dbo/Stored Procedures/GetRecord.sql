-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRecord]
	@Keyword nvarchar(max) = '', 
    @OffSet int,
    @PageSize int
AS
BEGIN
	SET NOCOUNT ON;

    SET @OffSet = (@OffSet - 1) * @PageSize

	IF (@PageSize > 0)
	BEGIN
		SELECT * FROM record 
		WHERE ISNULL(@Keyword, '') = '' OR 
			  (ISNULL(@Keyword, '') != '' AND ReferenceNumber LIKE '%' + @Keyword + '%') OR 
			  (ISNULL(@Keyword, '') != '' AND Amount LIKE '%' + @Keyword + '%') OR 
			  (ISNULL(@Keyword, '') != '' AND Id LIKE '%' + @Keyword + '%')
		ORDER BY Id DESC
		OFFSET @OffSet ROWS 
		FETCH NEXT @PageSize ROWS ONLY
	END
	ELSE
	BEGIN
		SELECT * FROM record 
		WHERE ISNULL(@Keyword, '') = '' OR 
			  (ISNULL(@Keyword, '') != '' AND ReferenceNumber LIKE '%' + @Keyword + '%') OR 
			  (ISNULL(@Keyword, '') != '' AND Amount LIKE '%' + @Keyword + '%') OR 
			  (ISNULL(@Keyword, '') != '' AND Id LIKE '%' + @Keyword + '%')
		ORDER BY Id DESC
	END
END