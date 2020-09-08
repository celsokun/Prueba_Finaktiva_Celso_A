create database [prueba_finaktiva_celso]
GO

CREATE TABLE [dbo].[municipio](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_municipio] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[municipio_region](
	[codigo_region] [int] NOT NULL,
	[codigo_municipio] [int] NOT NULL,
 CONSTRAINT [PK_municipio_region] PRIMARY KEY CLUSTERED 
(
	[codigo_region] ASC,
	[codigo_municipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[region](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NOT NULL,
 CONSTRAINT [PK_region] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[municipio_region]  WITH CHECK ADD  CONSTRAINT [FK_municipio_region_municipio] FOREIGN KEY([codigo_municipio])
REFERENCES [dbo].[municipio] ([codigo])
GO
ALTER TABLE [dbo].[municipio_region] CHECK CONSTRAINT [FK_municipio_region_municipio]
GO
ALTER TABLE [dbo].[municipio_region]  WITH CHECK ADD  CONSTRAINT [FK_municipio_region_region] FOREIGN KEY([codigo_region])
REFERENCES [dbo].[region] ([codigo])
GO
ALTER TABLE [dbo].[municipio_region] CHECK CONSTRAINT [FK_municipio_region_region]
GO
