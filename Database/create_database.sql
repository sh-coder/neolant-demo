USE [master]
GO
/****** Object:  Database [neolant]    Script Date: 12.05.2016 8:13:45 ******/
CREATE DATABASE [neolant]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'neolant', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\neolant.mdf' , SIZE = 2304000KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'neolant_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\neolant_log.ldf' , SIZE = 4211392KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [neolant] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [neolant].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [neolant] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [neolant] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [neolant] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [neolant] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [neolant] SET ARITHABORT OFF 
GO
ALTER DATABASE [neolant] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [neolant] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [neolant] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [neolant] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [neolant] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [neolant] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [neolant] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [neolant] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [neolant] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [neolant] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [neolant] SET  DISABLE_BROKER 
GO
ALTER DATABASE [neolant] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [neolant] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [neolant] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [neolant] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [neolant] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [neolant] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [neolant] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [neolant] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [neolant] SET  MULTI_USER 
GO
ALTER DATABASE [neolant] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [neolant] SET DB_CHAINING OFF 
GO
ALTER DATABASE [neolant] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [neolant] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [neolant]
GO
/****** Object:  User [neolant]    Script Date: 12.05.2016 8:13:46 ******/
CREATE USER [neolant] FOR LOGIN [neolant] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [neolant]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [neolant]
GO
/****** Object:  UserDefinedTableType [dbo].[BaseEntityInstances]    Script Date: 12.05.2016 8:13:46 ******/
CREATE TYPE [dbo].[BaseEntityInstances] AS TABLE(
	[INSTANCE_S] [bigint] NOT NULL
)
GO
/****** Object:  StoredProcedure [dbo].[spDelete]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[spDelete] 
	@TableName varchar(128),
	@InstanceS bigint
AS
BEGIN
	SET NOCOUNT ON;
	IF (EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName))
	BEGIN
		DECLARE @sql AS NVARCHAR(max)
		SET @sql = N'DELETE FROM ' + @TableName + ' WHERE(INSTANCE_S=@InstanceS)'
		EXEC sp_executesql @sql, N'@InstanceS bigint', @InstanceS=@InstanceS
	END
END





GO
/****** Object:  StoredProcedure [dbo].[spDeleteAll]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[spDeleteAll] 
	@TableName varchar(128)
AS
BEGIN
	SET NOCOUNT ON;
	IF (EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName))
	BEGIN
		DECLARE @sql AS NVARCHAR(max)
		SET @sql = N'DELETE FROM ' + @TableName
		EXEC sp_executesql @sql
	END
END





GO
/****** Object:  StoredProcedure [dbo].[spInsertCommonUniversalProperty]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- Batch submitted through debugger: SQLQuery5.sql|9|0|C:\Users\User\AppData\Local\Temp\~vsF127.sql



-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql
-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql

CREATE PROCEDURE [dbo].[spInsertCommonUniversalProperty] 
	@InstanceS bigint,
	@UniversalClassS bigint,
	@PropertyKindS bigint,
	@Description nvarchar(max) = null,
	@Sequence int = null
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[COMMON_UNIVERSAL_PROPERTY]([INSTANCE_S], [UNIVERSAL_CLASS_S], [PROPERTY_KIND_S], [DESCRIPTION], [SEQUENCE])
	VALUES(@InstanceS, @UniversalClassS, @PropertyKindS, @Description, @Sequence);
END











GO
/****** Object:  StoredProcedure [dbo].[spInsertFacility]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- Batch submitted through debugger: SQLQuery5.sql|9|0|C:\Users\User\AppData\Local\Temp\~vsF127.sql



-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql
-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql

CREATE PROCEDURE [dbo].[spInsertFacility] 
	@InstanceS bigint,
	@ParentInstanceS bigint = null,
	@Identifier nvarchar(250),
	@Description nvarchar(max) = null,
	@Created datetime = null,
	@CreatedBy nvarchar(250) = null,
	@KindS bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN

	INSERT INTO [dbo].[FACILITY]
           ([INSTANCE_S]
           ,[PARENT_INSTANCE_S]
           ,[IDENTIFIER]
           ,[DESCRIPTION]
           ,[CREATED]
           ,[CREATED_BY]
		   ,[KIND_S])
     VALUES
			(@InstanceS
			,@ParentInstanceS
			,@Identifier
			,@Description
			,@Created
			,@CreatedBy
			,@KindS)
	END
END













GO
/****** Object:  StoredProcedure [dbo].[spInsertFacilityClass]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- Batch submitted through debugger: SQLQuery5.sql|9|0|C:\Users\User\AppData\Local\Temp\~vsF127.sql



-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql
-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql

CREATE PROCEDURE [dbo].[spInsertFacilityClass] 
	@InstanceS bigint,
	@ParentInstanceS bigint = null,
	@Identifier nvarchar(250),
	@Description nvarchar(max) = null,
	@Created datetime = null,
	@CreatedBy nvarchar(250) = null
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN


	INSERT INTO [dbo].[FACILITY_CLASS]
           ([INSTANCE_S]
           ,[PARENT_INSTANCE_S]
           ,[IDENTIFIER]
           ,[DESCRIPTION]
           ,[CREATED]
           ,[CREATED_BY])
     VALUES
			(@InstanceS
			,@ParentInstanceS
			,@Identifier
			,@Description
			,@Created
			,@CreatedBy)
	END
END













GO
/****** Object:  StoredProcedure [dbo].[spInsertProperty]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









-- Batch submitted through debugger: SQLQuery5.sql|9|0|C:\Users\User\AppData\Local\Temp\~vsF127.sql



-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql
-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql

CREATE PROCEDURE [dbo].[spInsertProperty] 
	@InstanceS bigint,
	@PropertyKindS bigint,
	@TargetClassS bigint,
	@NdtDataType nvarchar(250),
	@Description nvarchar(max) = null,
	@Created datetime = null,
	@CreatedBy nvarchar(250) = null,
	@BooleanValue bit = null,
    @StringValue nvarchar(max) = null,
    @BinaryValue binary(1) = null,
    @DateTimeValue datetime = null,
    @DecimalValue decimal = null,
    @FloatValue float = null,
    @RealValue real = null,
    @BigintValue bigint = null,
    @IntegerValue int = null,
    @SmallIntegerValue smallint = null
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN

	INSERT INTO [dbo].[PROPERTY]
           ([INSTANCE_S]
           ,[PROPERTY_KIND_S]
           ,[TARGET_CLASS_S]
		   ,[NDT_DATA_TYPE]
           ,[DESCRIPTION]
           ,[CREATED]
           ,[CREATED_BY]
           ,[BOOLEAN_VALUE]
           ,[STRING_VALUE]
           ,[BINARY_VALUE]
           ,[DATETIME_VALUE]
           ,[DECIMAL_VALUE]
           ,[FLOAT_VALUE]
           ,[REAL_VALUE]
           ,[BIGINT_VALUE]
           ,[INTEGER_VALUE]
           ,[SMALLINTEGER_VALUE])
     VALUES
           (@InstanceS
           ,@PropertyKindS
           ,@TargetClassS
		   ,@NdtDataType
           ,@Description
           ,@Created
           ,@CreatedBy
           ,@BooleanValue
           ,@StringValue
           ,@BinaryValue
           ,@DateTimeValue
           ,@DecimalValue
           ,@FloatValue
           ,@RealValue
           ,@BigintValue
           ,@IntegerValue
           ,@SmallIntegerValue)
	END
END
















GO
/****** Object:  StoredProcedure [dbo].[spInsertPropertyKind]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- Batch submitted through debugger: SQLQuery5.sql|9|0|C:\Users\User\AppData\Local\Temp\~vsF127.sql



-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql
-- Batch submitted through debugger: SQLQuery11.sql|13|0|C:\Users\User\AppData\Local\Temp\~vsF571.sql

CREATE PROCEDURE [dbo].[spInsertPropertyKind] 
	@InstanceS bigint,
	@ParentInstanceS bigint = null,
	@Identifier nvarchar(250),
	@Description nvarchar(max) = null,
	@Created datetime = null,
	@CreatedBy nvarchar(250) = null,
	@NdtDataType nvarchar(250)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN

	INSERT INTO [dbo].[PROPERTY_KIND]
           ([INSTANCE_S]
           ,[PARENT_INSTANCE_S]
           ,[IDENTIFIER]
           ,[DESCRIPTION]
           ,[CREATED]
           ,[CREATED_BY]
		   ,[NDT_DATA_TYPE])
     VALUES
			(@InstanceS
			,@ParentInstanceS
			,@Identifier
			,@Description
			,@Created
			,@CreatedBy
			,@NdtDataType)
	END
END













GO
/****** Object:  StoredProcedure [dbo].[spSelectProperty]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSelectProperty]
  @TargetClasses as [BaseEntityInstances] READONLY
AS
BEGIN
  SET NOCOUNT ON;
  
  SELECT *
  FROM [PROPERTY] WITH(NOLOCK)
  WHERE TARGET_CLASS_S in (SELECT INSTANCE_S FROM @TargetClasses)
END


GO
/****** Object:  StoredProcedure [dbo].[spSelectTable]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTable] 
	@TableName nvarchar(128),
	@InstanceS bigint = null
AS
BEGIN
	SET NOCOUNT ON;
	IF (EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName))
	BEGIN
		DECLARE @sql AS NVARCHAR(max)
		SET @sql = N'SELECT * FROM ' + @TableName + ' WITH(NOLOCK) WHERE 1=1'
		
		IF(@InstanceS IS NOT NULL)
			SET @sql = @sql + ' AND([INSTANCE_S] = @InstanceS)'
		
		EXEC sp_executesql @sql, N'@InstanceS bigint', @InstanceS=@InstanceS
	END
END







GO
/****** Object:  StoredProcedure [dbo].[spSelectTableHierarchy]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTableHierarchy] 
	@TableName nvarchar(128),
	@InstanceS bigint = null
AS
BEGIN
	SET NOCOUNT ON;
	IF (EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName))
	BEGIN
		DECLARE @sql AS NVARCHAR(max)	
		IF(@InstanceS IS NOT NULL)
			SET @sql = N'
With Hierarchy (INSTANCE_S, [PARENT_INSTANCE_S], [LEVEL])
As (
	SELECT INSTANCE_S, [PARENT_INSTANCE_S], 1 As [LEVEL]
	FROM ' + @TableName + '
	WHERE INSTANCE_S = @InstanceS

    UNION All

	SELECT Child.INSTANCE_S, Child.[PARENT_INSTANCE_S], Parent.[LEVEL] + 1 As [LEVEL]
	FROM ' + @TableName + ' As Child
    INNER JOIN Hierarchy As parent On Child.[PARENT_INSTANCE_S] = Parent.INSTANCE_S)
SELECT Source.*, Hierarchy.LEVEL
FROM ' + @TableName + ' as Source
INNER JOIN Hierarchy on Hierarchy.INSTANCE_S = Source.INSTANCE_S
ORDER BY [LEVEL]'
		ELSE
			SET @sql = N'
With Hierarchy (INSTANCE_S, [PARENT_INSTANCE_S], [LEVEL])
As (
	SELECT INSTANCE_S, [PARENT_INSTANCE_S], 1 As [LEVEL]
	FROM ' + @TableName + '
	WHERE PARENT_INSTANCE_S IS NULL

    UNION All

	SELECT Child.INSTANCE_S, Child.[PARENT_INSTANCE_S], Parent.[LEVEL] + 1 As [LEVEL]
	FROM ' + @TableName + ' As Child
    INNER JOIN Hierarchy As parent On Child.[PARENT_INSTANCE_S] = Parent.INSTANCE_S)
SELECT Source.*, Hierarchy.LEVEL
FROM ' + @TableName + ' as Source
INNER JOIN Hierarchy on Hierarchy.INSTANCE_S = Source.INSTANCE_S
ORDER BY [LEVEL]'

		EXEC sp_executesql @sql, N'@InstanceS bigint', @InstanceS=@InstanceS
	END
END






GO
/****** Object:  Table [dbo].[COMMON_UNIVERSAL_PROPERTY]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMMON_UNIVERSAL_PROPERTY](
	[INSTANCE_S] [bigint] NOT NULL,
	[UNIVERSAL_CLASS_S] [bigint] NOT NULL,
	[PROPERTY_KIND_S] [bigint] NOT NULL,
	[DESCRIPTION] [nvarchar](max) NULL,
	[SEQUENCE] [int] NULL,
 CONSTRAINT [PK_COMMON_UNIVERSAL_PROPERTY] PRIMARY KEY NONCLUSTERED 
(
	[INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UNQ_COMMON_UNIVERSAL_PROPERTY] UNIQUE NONCLUSTERED 
(
	[UNIVERSAL_CLASS_S] ASC,
	[PROPERTY_KIND_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FACILITY]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FACILITY](
	[INSTANCE_S] [bigint] NOT NULL,
	[PARENT_INSTANCE_S] [bigint] NULL,
	[IDENTIFIER] [nvarchar](250) NOT NULL,
	[DESCRIPTION] [nvarchar](max) NULL,
	[CREATED] [datetime] NULL,
	[CREATED_BY] [nvarchar](250) NULL,
	[UPDATED] [datetime] NULL,
	[UPDATED_BY] [varchar](250) NULL,
	[KIND_S] [bigint] NOT NULL,
 CONSTRAINT [PK_FACILITY] PRIMARY KEY NONCLUSTERED 
(
	[INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FACILITY_CLASS]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACILITY_CLASS](
	[INSTANCE_S] [bigint] NOT NULL,
	[PARENT_INSTANCE_S] [bigint] NULL,
	[IDENTIFIER] [nvarchar](250) NOT NULL,
	[DESCRIPTION] [nvarchar](max) NULL,
	[CREATED] [datetime] NULL,
	[CREATED_BY] [nvarchar](250) NULL,
	[UPDATED] [datetime] NULL,
	[UPDATED_BY] [nvarchar](250) NULL,
 CONSTRAINT [PK_FACILITY_CLASS] PRIMARY KEY NONCLUSTERED 
(
	[INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PROPERTY]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PROPERTY](
	[INSTANCE_S] [bigint] NOT NULL,
	[PROPERTY_KIND_S] [bigint] NOT NULL,
	[TARGET_CLASS_S] [bigint] NULL,
	[NDT_DATA_TYPE] [nvarchar](250) NOT NULL,
	[CREATED] [datetime] NULL,
	[CREATED_BY] [varchar](240) NULL,
	[UPDATED] [datetime] NULL,
	[UPDATED_BY] [varchar](240) NULL,
	[BOOLEAN_VALUE] [bit] NULL,
	[STRING_VALUE] [nvarchar](max) NULL,
	[BINARY_VALUE] [binary](1) NULL,
	[DATETIME_VALUE] [datetime] NULL,
	[DECIMAL_VALUE] [decimal](18, 0) NULL,
	[FLOAT_VALUE] [float] NULL,
	[REAL_VALUE] [real] NULL,
	[BIGINT_VALUE] [bigint] NULL,
	[INTEGER_VALUE] [int] NULL,
	[SMALLINTEGER_VALUE] [smallint] NULL,
 CONSTRAINT [PK_PROPERTY] PRIMARY KEY NONCLUSTERED 
(
	[INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PROPERTY_KIND]    Script Date: 12.05.2016 8:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PROPERTY_KIND](
	[INSTANCE_S] [bigint] NOT NULL,
	[PARENT_INSTANCE_S] [bigint] NULL,
	[IDENTIFIER] [nvarchar](250) NOT NULL,
	[DESCRIPTION] [nvarchar](max) NULL,
	[CREATED] [datetime] NULL,
	[CREATED_BY] [nvarchar](250) NULL,
	[UPDATED] [datetime] NULL,
	[UPDATED_BY] [nvarchar](250) NULL,
	[NDT_DATA_TYPE] [varchar](250) NOT NULL,
 CONSTRAINT [PK_PROPERTY_KIND] PRIMARY KEY NONCLUSTERED 
(
	[INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UNQ_PROPERTY_KIND] UNIQUE NONCLUSTERED 
(
	[IDENTIFIER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [CIX_COMMON_UNIVERSAL_PROPERTY]    Script Date: 12.05.2016 8:13:46 ******/
CREATE CLUSTERED INDEX [CIX_COMMON_UNIVERSAL_PROPERTY] ON [dbo].[COMMON_UNIVERSAL_PROPERTY]
(
	[INSTANCE_S] ASC,
	[UNIVERSAL_CLASS_S] ASC,
	[PROPERTY_KIND_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CIX_FACILITY]    Script Date: 12.05.2016 8:13:46 ******/
CREATE CLUSTERED INDEX [CIX_FACILITY] ON [dbo].[FACILITY]
(
	[INSTANCE_S] ASC,
	[PARENT_INSTANCE_S] ASC,
	[IDENTIFIER] ASC,
	[KIND_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CIX_FACILITY_CLASS]    Script Date: 12.05.2016 8:13:46 ******/
CREATE CLUSTERED INDEX [CIX_FACILITY_CLASS] ON [dbo].[FACILITY_CLASS]
(
	[INSTANCE_S] ASC,
	[PARENT_INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [CIX_]    Script Date: 12.05.2016 8:13:46 ******/
CREATE CLUSTERED INDEX [CIX_] ON [dbo].[PROPERTY]
(
	[INSTANCE_S] ASC,
	[PROPERTY_KIND_S] ASC,
	[TARGET_CLASS_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CIX_PROPERTY_KIND]    Script Date: 12.05.2016 8:13:46 ******/
CREATE CLUSTERED INDEX [CIX_PROPERTY_KIND] ON [dbo].[PROPERTY_KIND]
(
	[INSTANCE_S] ASC,
	[PARENT_INSTANCE_S] ASC,
	[NDT_DATA_TYPE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PROPERTY_KIND_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_PROPERTY_KIND_S] ON [dbo].[COMMON_UNIVERSAL_PROPERTY]
(
	[PROPERTY_KIND_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SEQUENCE]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_SEQUENCE] ON [dbo].[COMMON_UNIVERSAL_PROPERTY]
(
	[SEQUENCE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UNIVERSAL_CLASS_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_UNIVERSAL_CLASS_S] ON [dbo].[COMMON_UNIVERSAL_PROPERTY]
(
	[UNIVERSAL_CLASS_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_KIND_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_KIND_S] ON [dbo].[FACILITY]
(
	[KIND_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PARENT_INSTANCE_C]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_PARENT_INSTANCE_C] ON [dbo].[FACILITY]
(
	[PARENT_INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PARENT_INSTANCE_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_PARENT_INSTANCE_S] ON [dbo].[FACILITY_CLASS]
(
	[PARENT_INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PROPERTY_KIND_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_PROPERTY_KIND_S] ON [dbo].[PROPERTY]
(
	[PROPERTY_KIND_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TARGET_CLASS_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_TARGET_CLASS_S] ON [dbo].[PROPERTY]
(
	[TARGET_CLASS_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_NDT_DATA_TYPE]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_NDT_DATA_TYPE] ON [dbo].[PROPERTY_KIND]
(
	[NDT_DATA_TYPE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PARENT_INSTANCE_S]    Script Date: 12.05.2016 8:13:46 ******/
CREATE NONCLUSTERED INDEX [IX_PARENT_INSTANCE_S] ON [dbo].[PROPERTY_KIND]
(
	[PARENT_INSTANCE_S] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[COMMON_UNIVERSAL_PROPERTY]  WITH NOCHECK ADD  CONSTRAINT [FK_COMMON_UNIVERSAL_PROPERTY_PROPERTY_KIND] FOREIGN KEY([PROPERTY_KIND_S])
REFERENCES [dbo].[PROPERTY_KIND] ([INSTANCE_S])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[COMMON_UNIVERSAL_PROPERTY] CHECK CONSTRAINT [FK_COMMON_UNIVERSAL_PROPERTY_PROPERTY_KIND]
GO
ALTER TABLE [dbo].[COMMON_UNIVERSAL_PROPERTY]  WITH NOCHECK ADD  CONSTRAINT [FK_COMMON_UNIVERSAL_PROPERTY_UNIVERSAL_CLASS_S] FOREIGN KEY([UNIVERSAL_CLASS_S])
REFERENCES [dbo].[FACILITY_CLASS] ([INSTANCE_S])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[COMMON_UNIVERSAL_PROPERTY] CHECK CONSTRAINT [FK_COMMON_UNIVERSAL_PROPERTY_UNIVERSAL_CLASS_S]
GO
ALTER TABLE [dbo].[FACILITY]  WITH CHECK ADD  CONSTRAINT [FK_FACILITY_PARENT_INSTANCE_S] FOREIGN KEY([PARENT_INSTANCE_S])
REFERENCES [dbo].[FACILITY] ([INSTANCE_S])
GO
ALTER TABLE [dbo].[FACILITY] CHECK CONSTRAINT [FK_FACILITY_PARENT_INSTANCE_S]
GO
ALTER TABLE [dbo].[FACILITY_CLASS]  WITH NOCHECK ADD  CONSTRAINT [FK_FACILITY_CLASS_PARENT_CLASS_S] FOREIGN KEY([PARENT_INSTANCE_S])
REFERENCES [dbo].[FACILITY_CLASS] ([INSTANCE_S])
GO
ALTER TABLE [dbo].[FACILITY_CLASS] CHECK CONSTRAINT [FK_FACILITY_CLASS_PARENT_CLASS_S]
GO
ALTER TABLE [dbo].[PROPERTY]  WITH NOCHECK ADD  CONSTRAINT [FK_PROPERTY_PROPERTY_KIND] FOREIGN KEY([PROPERTY_KIND_S])
REFERENCES [dbo].[PROPERTY_KIND] ([INSTANCE_S])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PROPERTY] CHECK CONSTRAINT [FK_PROPERTY_PROPERTY_KIND]
GO
ALTER TABLE [dbo].[PROPERTY_KIND]  WITH NOCHECK ADD  CONSTRAINT [FK_PROPERTY_KIND_PARENT_INSTANCE_S] FOREIGN KEY([PARENT_INSTANCE_S])
REFERENCES [dbo].[PROPERTY_KIND] ([INSTANCE_S])
GO
ALTER TABLE [dbo].[PROPERTY_KIND] CHECK CONSTRAINT [FK_PROPERTY_KIND_PARENT_INSTANCE_S]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Surrogate key of table COMMON_FACILITY_PROPERTY.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'COMMON_UNIVERSAL_PROPERTY', @level2type=N'COLUMN',@level2name=N'INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The type of property which is commonly associated with the class.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'COMMON_UNIVERSAL_PROPERTY', @level2type=N'COLUMN',@level2name=N'PROPERTY_KIND_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A remark or comment about the instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'COMMON_UNIVERSAL_PROPERTY', @level2type=N'COLUMN',@level2name=N'DESCRIPTION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Surrogate key of table GENERAL_FACILITY.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The more general class of which this class is a specialization. The semantics of a child class must be a subset of the semantics of the parent class.  That is, the child class must represent a specialization of the parent concept by narrowing the meaning of the parent concept. Conformance to a class where the meaning of the class is not altered should be handled by class classification  instead of by parent/child.  The naming system of the child class should either be the same as the  naming system of the parent class or it should be a specialization of  that naming system.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'PARENT_INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The name of the facility.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'IDENTIFIER'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A remark or comment about the instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'DESCRIPTION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the date that this instance was created. This date is defined   by the source if imported, or is the date the instance is created if a new   instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'CREATED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the person, company or application that created this   instance. This identifies the party responsible for loading this instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the date that this instance was last updated. This   value should be updated when any part of this instance is   altered (except for inverse relationships).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'UPDATED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This records the person or application making the update    to this instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'UPDATED_BY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The class which asserts the fundamental nature of the facility.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY', @level2type=N'COLUMN',@level2name=N'KIND_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Surrogate key of table FACILITY_CLASS.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The more general class of which this class is a specialization. The semantics of a child class must be a subset of the semantics of the parent class.  That is, the child class must represent a specialization of the parent concept by narrowing the meaning of the parent concept. Conformance to a class where the meaning of the class is not altered should be handled by class classification  instead of by parent/child.  The naming system of the child class should either be the same as the  naming system of the parent class or it should be a specialization of  that naming system.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'PARENT_INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The name of the instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'IDENTIFIER'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A remark or comment about the instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'DESCRIPTION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the date that this instance was created. This date is defined   by the source if imported, or is the date the instance is created if a new   instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'CREATED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the person, company or application that created this   instance. This identifies the party responsible for loading this instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the date that this instance was last updated. This   value should be updated when any part of this instance is   altered (except for inverse relationships).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'UPDATED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This records the person or application making the update    to this instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACILITY_CLASS', @level2type=N'COLUMN',@level2name=N'UPDATED_BY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Surrogate key of table PROPERTY.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Specifies the type of property. This is the role the data value with respect to the object which has this property.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'PROPERTY_KIND_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' InstanceS TARGET_CLASS_T,  .' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'TARGET_CLASS_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A boolean representation of a property value.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'BOOLEAN_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A character representation of a property value.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'STRING_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A real representation of a property value.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'REAL_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' An integer representation of a property value.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY', @level2type=N'COLUMN',@level2name=N'INTEGER_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Surrogate key of table PROPERTY_KIND.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The more general property of which this property is a specialization.   The semantics of a child property must be a subset of the semantics of the   parent property. That is, the child property must represent a specialization   of the parent concept by narrowing the meaning of the parent concept.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'PARENT_INSTANCE_S'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' The name of the instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'IDENTIFIER'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' A remark or comment about the instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'DESCRIPTION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the date that this instance was created. This date is defined   by the source if imported, or is the date the instance is created if a new   instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'CREATED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the person, company or application that created this   instance. This identifies the party responsible for loading this instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This is the date that this instance was last updated. This   value should be updated when any part of this instance is   altered (except for inverse relationships).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'UPDATED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' This records the person or application making the update    to this instance.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'UPDATED_BY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' If this property kind was specified in a named defined type (ndt)   specification then this is the data type that was declared   in that specification.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PROPERTY_KIND', @level2type=N'COLUMN',@level2name=N'NDT_DATA_TYPE'
GO
USE [master]
GO
ALTER DATABASE [neolant] SET  READ_WRITE 
GO
