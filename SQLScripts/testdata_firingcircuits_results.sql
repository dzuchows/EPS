USE [MES]
GO

/****** Object:  Table [dbo].[testdata_firingcircuits_results]    Script Date: 4/3/2013 9:31:43 PM ******/
DROP TABLE [dbo].[testdata_firingcircuits_results]
GO

/****** Object:  Table [dbo].[testdata_firingcircuits_results]    Script Date: 4/3/2013 9:31:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[testdata_firingcircuits_results](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [datetime] NULL,
	[step] [int] NULL,
	[status] [varchar](256) NULL,
	[progtime] [varchar](32) NULL,
	[steptime] [varchar](32) NULL,
	[cycle] [int] NULL,
	[cyclelevel] [int] NULL,
	[procedure] [varchar](256) NULL,
	[voltagev] [float] NULL,
	[currenta] [float] NULL,
	[ahaccu] [float] NULL,
	[ahcha] [float] NULL,
	[ahdch] [float] NULL,
	[ahstep] [float] NULL,
	[energy] [float] NULL,
	[whstep] [float] NULL,
	[datetime] [datetime] NULL,
	[testdata_firingcircuits_id] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

