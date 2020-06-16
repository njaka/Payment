CREATE TABLE [dbo].[Payment]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [PaymentId] UNIQUEIDENTIFIER NOT NULL, 
    [CardId] BIGINT NOT NULL,
    [Amount] DECIMAL NOT NULL, 
    [Status] SMALLINT NOT NULL, 
    [CreationDate] DATETIME2 NOT NULL, 

    CONSTRAINT [FK_Payment_Card] FOREIGN KEY ([CardId]) REFERENCES [Card]([Id]) 
)

GO
