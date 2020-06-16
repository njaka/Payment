CREATE TABLE [dbo].[Card]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [CardNumber] VARCHAR(50) NOT NULL, 
    [ExpirationDate] DATETIME2 NOT NULL, 
    [CCV] NCHAR(3) NOT NULL
)
