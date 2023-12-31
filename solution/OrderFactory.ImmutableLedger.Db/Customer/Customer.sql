﻿CREATE VIEW [dbo].[Customer] AS 
with cte as 
(
	SELECT top 1 with ties 
		[RootId],
		[DateCreated],
		[RecordId],
		[Deleted],
		[BasedOnRecordId],
		[FirstName],
		[MiddleName],
		[LastName],
		[DefaultPhoneId]
	FROM [dbo].[CustomerData]
	order by row_number() over (partition by [RootId] order by [DateCreated] DESC, [RecordId] DESC)
)
SELECT 
	[RootId],
	[DateCreated],
	[RecordId],
	[Deleted],
	[BasedOnRecordId],
	[FirstName],
	[MiddleName],
	[LastName],
	[DefaultPhoneId]
from cte where [Deleted] = 0 