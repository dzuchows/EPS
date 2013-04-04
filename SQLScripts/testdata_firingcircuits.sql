USE [MES]
GO

ALTER TABLE [dbo].[testdata_firingcircuits] DROP CONSTRAINT [DF_testdata_firingcircuits_datetime]
GO

/****** Object:  Table [dbo].[testdata_firingcircuits]    Script Date: 4/3/2013 9:31:29 PM ******/
DROP TABLE [dbo].[testdata_firingcircuits]
GO

/****** Object:  Table [dbo].[testdata_firingcircuits]    Script Date: 4/3/2013 9:31:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[testdata_firingcircuits](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[module_test_id] [int] NOT NULL,
	[measurement_id] [int] NOT NULL,
	[batteryname] [varchar](256) NOT NULL,
	[circuit] [varchar](256) NOT NULL,
	[program] [varchar](256) NOT NULL,
	[starttime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[testsection] [varchar](256) NULL,
	[comment] [varchar](256) NULL,
	[orderno] [varchar](256) NULL,
	[producer] [varchar](256) NULL,
	[type] [varchar](256) NULL,
	[nominalvoltage] [float] NULL,
	[nominalcurrent] [float] NULL,
	[nominalcapacity] [float] NULL,
	[cells] [varchar](256) NULL,
	[maximumvoltage] [float] NULL,
	[gassingvoltage] [float] NULL,
	[breakvoltage] [float] NULL,
	[chargefactor] [float] NULL,
	[impedance] [float] NULL,
	[coldcrankingcurrent] [float] NULL,
	[energydensity] [float] NULL,
	[datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[testdata_firingcircuits] ADD  CONSTRAINT [DF_testdata_firingcircuits_datetime]  DEFAULT (getdate()) FOR [datetime]
GO

