USE [MES]
GO

ALTER TABLE [dbo].[testdata_pec] DROP CONSTRAINT [DF_testdata_pec_uploadon]
GO

/****** Object:  Table [dbo].[testdata_pec]    Script Date: 4/3/2013 9:31:52 PM ******/
DROP TABLE [dbo].[testdata_pec]
GO

/****** Object:  Table [dbo].[testdata_pec]    Script Date: 4/3/2013 9:31:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[testdata_pec](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[module_test_id] [int] NULL,
	[regime_name] [varchar](256) NULL,
	[regime_suffix] [varchar](256) NULL,
	[regime_cellsize] [varchar](256) NULL,
	[regime_version] [int] NULL,
	[starttime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[uploadedon] [datetime] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[testdata_pec] ADD  CONSTRAINT [DF_testdata_pec_uploadon]  DEFAULT (getdate()) FOR [uploadedon]
GO

