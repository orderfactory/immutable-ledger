CREATE VIEW [dbo].[CustomerPhone] AS 
with cte as 
(
	SELECT top 1 with ties 
		[RootId],
		[DateCreated],
		[RecordId],
		[Deleted],
		[BasedOnRecordId],
		[CustomerId],
		[PhoneId]
	FROM [dbo].[CustomerPhoneData]
	order by row_number() over (partition by [RootId] order by [DateCreated] DESC, [RecordId] DESC)
)
SELECT 
	[RootId],
	[DateCreated],
	[RecordId],
	[Deleted],
	[BasedOnRecordId],
	[CustomerId],
	[PhoneId]
from cte where [Deleted] = 0 