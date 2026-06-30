CREATE TABLE [dbo].[TBContato] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Nome]     NVARCHAR (100)   NOT NULL,
    [Email]    NVARCHAR (255)   NOT NULL,
    [Telefone] NVARCHAR (20)    NOT NULL,
    [Cargo]    NVARCHAR (100)   NULL,
    [Empresa]  NVARCHAR (100)   NULL
);
GO

ALTER TABLE [dbo].[TBContato]
    ADD CONSTRAINT [PK_TBContato] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

