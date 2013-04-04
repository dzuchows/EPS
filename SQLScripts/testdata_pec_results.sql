USE [MES]
GO

ALTER TABLE [dbo].[testdata_pec_results] DROP CONSTRAINT [DF_testdata_pec_results_datetime]
GO

/****** Object:  Table [dbo].[testdata_pec_results]    Script Date: 4/3/2013 9:32:08 PM ******/
DROP TABLE [dbo].[testdata_pec_results]
GO

/****** Object:  Table [dbo].[testdata_pec_results]    Script Date: 4/3/2013 9:32:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[testdata_pec_results](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[testdata_pec_id] [int] NULL,
	[testnumber] [varchar](256) NULL,
	[test] [int] NULL,
	[step] [int] NULL,
	[cycle] [int] NULL,
	[totaltime] [float] NULL,
	[cyclechannel] [int] NULL,
	[cycledischargetime] [int] NULL,
	[voltage] [float] NULL,
	[current] [float] NULL,
	[chargecapacityah] [float] NULL,
	[dischargecapacityah] [float] NULL,
	[chargecapacitywh] [float] NULL,
	[dischargecapacitywh] [float] NULL,
	[datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[testdata_pec_results] ADD  CONSTRAINT [DF_testdata_pec_results_datetime]  DEFAULT (getdate()) FOR [datetime]
GO

