CREATE TABLE [dbo].[CustomerData]
(
    [RootId] UNIQUEIDENTIFIER NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    [RecordId] UNIQUEIDENTIFIER NOT NULL,
    [Deleted] BIT NOT NULL DEFAULT 0,
    [BasedOnRecordId] UNIQUEIDENTIFIER NULL,
    [ledger_start_transaction_id] bigint GENERATED ALWAYS AS TRANSACTION_ID START HIDDEN NOT NULL,
    [ledger_start_sequence_number]    bigint GENERATED ALWAYS AS SEQUENCE_NUMBER START,
    [FirstName] NVARCHAR(100) NOT NULL,
    [MiddleName] NVARCHAR(100) NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_CustomerData] PRIMARY KEY ([RootId], [DateCreated] DESC, [RecordId] DESC),
    CONSTRAINT [FK_CustomerData_CustomerRoot] FOREIGN KEY ([RootId]) REFERENCES [dbo].[CustomerRoot]([Id])
)
WITH
(
  LEDGER = ON
  (
    APPEND_ONLY = ON,
    LEDGER_VIEW = [dbo].[CustomerData_Ledger]
    (
      transaction_id_column_name = [ledger_transaction_id],
      sequence_number_column_name = [ledger_sequence_number],
      operation_type_column_name = [ledger_operation_type],
      operation_type_desc_column_name = [ledger_operation_type_desc]
    )
  )
)