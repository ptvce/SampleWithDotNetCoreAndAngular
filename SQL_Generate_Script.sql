USE [master]
GO
/****** Object:  Database [CoreLearningDB]    Script Date: 5/21/2020 1:44:04 AM ******/
CREATE DATABASE [CoreLearningDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoreLearningDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CoreLearningDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CoreLearningDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CoreLearningDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CoreLearningDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoreLearningDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoreLearningDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoreLearningDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoreLearningDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoreLearningDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoreLearningDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoreLearningDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CoreLearningDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoreLearningDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoreLearningDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoreLearningDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoreLearningDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoreLearningDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoreLearningDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoreLearningDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoreLearningDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CoreLearningDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoreLearningDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoreLearningDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoreLearningDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoreLearningDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoreLearningDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoreLearningDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoreLearningDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CoreLearningDB] SET  MULTI_USER 
GO
ALTER DATABASE [CoreLearningDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoreLearningDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoreLearningDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoreLearningDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CoreLearningDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CoreLearningDB', N'ON'
GO
ALTER DATABASE [CoreLearningDB] SET QUERY_STORE = OFF
GO
USE [CoreLearningDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/21/2020 1:44:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](120) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/21/2020 1:44:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](120) NULL,
	[UnitPrice] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (1, N'Mobile')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (2, N'Tablet')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (3, N'Tv')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (4, N'Modem')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (5, N'Monitor')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (6, N'Mouse')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (7, N'Fan')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (8, N'Cpu')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (9, N'Ram')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (10, N'Hard')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (11, N'GraphicCard')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (12, N'Laptop')
GO
INSERT [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (13, N'Keyboard')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [ProductName], [UnitPrice]) VALUES (1, 1, N'3434', 34)
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [ProductName], [UnitPrice]) VALUES (2, 1, N'12121', 12)
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [ProductName], [UnitPrice]) VALUES (3, 1, N'2415', 22)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_Categories_GetAll_WithPagination]    Script Date: 5/21/2020 1:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Categories_GetAll_WithPagination] 
@skip int,
@take int,
@total int output
AS
BEGIN
	SET NOCOUNT ON;

	select @total=COUNT(*) from Categories 

    select * from Categories order by CategoryId
	offset @skip rows fetch next @take rows only
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_GetById]    Script Date: 5/21/2020 1:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Products_GetById] 
@ProductId int
AS
BEGIN
	SET NOCOUNT ON;
    select * from Products where productId=@ProductId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Products_Insert]    Script Date: 5/21/2020 1:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Products_Insert] 
	@ProductName nvarchar(120),
	@CategoryId int,
	@UnitPrice int,

	@id int output
	AS
BEGIN
	SET NOCOUNT ON;
	if(Exists(select * from products where ProductName = @ProductName))
	begin
	  raiserror('already exist',16,1)
	  return
	end
    Insert into Products(ProductName,CategoryId,UnitPrice) Values(@ProductName,@CategoryId,@UnitPrice)
	set @id = SCOPE_IDENTITY()
END
GO
USE [master]
GO
ALTER DATABASE [CoreLearningDB] SET  READ_WRITE 
GO
