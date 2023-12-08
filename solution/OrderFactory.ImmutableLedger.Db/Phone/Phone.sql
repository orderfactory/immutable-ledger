CREATE VIEW [dbo].[Phone] AS
with cte as 
(
	SELECT top 1 with ties 
		[RootId],
		[DateCreated],
		[RecordId],
		[Deleted],
		[BasedOnRecordId],
		[PhoneNumber]
	FROM [dbo].[PhoneData]
	order by row_number() over (partition by [RootId] order by [DateCreated] DESC, [RecordId] DESC)
)
SELECT 
	[RootId],
	[DateCreated],
	[RecordId],
	[Deleted],
	[BasedOnRecordId],
	[PhoneNumber]
from cte where [Deleted] = 0