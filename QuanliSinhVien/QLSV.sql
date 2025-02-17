USE [master]
GO
/****** Object:  Database [QLISNHVIEN]    Script Date: 11/2/2024 11:15:25 AM ******/
CREATE DATABASE [QLISNHVIEN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLISNHVIEN', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS02\MSSQL\DATA\QLISNHVIEN.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLISNHVIEN_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS02\MSSQL\DATA\QLISNHVIEN_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLISNHVIEN] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLISNHVIEN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLISNHVIEN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLISNHVIEN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLISNHVIEN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLISNHVIEN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLISNHVIEN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLISNHVIEN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLISNHVIEN] SET  MULTI_USER 
GO
ALTER DATABASE [QLISNHVIEN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLISNHVIEN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLISNHVIEN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLISNHVIEN] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLISNHVIEN] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QLISNHVIEN]
GO
/****** Object:  User [LAPTOP-E01MG43U\Thuong's]    Script Date: 11/2/2024 11:15:25 AM ******/
CREATE USER [LAPTOP-E01MG43U\Thuong's] FOR LOGIN [LAPTOP-E01MG43U\Thuong's] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [LAPTOP-E01MG43U\Thuong's]
GO
/****** Object:  Table [dbo].[BANGDIEM]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BANGDIEM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MASV] [varchar](50) NULL,
	[MAMH] [varchar](50) NULL,
	[PHANTRAMTRENLOP] [decimal](5, 2) NULL,
	[PHANTRAMTHI] [decimal](5, 2) NULL,
	[DIEMTHI] [decimal](5, 2) NULL,
	[DIEMTB] [decimal](5, 2) NULL,
	[LOAI] [varchar](50) NULL,
 CONSTRAINT [PK_BANGDIEM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[COVANHOCTAP]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COVANHOCTAP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MACVHT] [varchar](50) NULL,
	[TENCVHT] [varchar](50) NULL,
	[NGAYSINH] [date] NULL,
	[GIOITINH] [varchar](50) NULL,
	[MAKHOA] [varchar](50) NULL,
	[MALOP] [varchar](50) NULL,
 CONSTRAINT [PK_COVANHOCTAP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DANGNHAP]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DANGNHAP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TENDANGNHAP] [varchar](50) NOT NULL,
	[MATKHAU] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DANGNHAP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KHOA]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KHOA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MAKHOA] [varchar](50) NOT NULL,
	[TENKHOA] [varchar](50) NOT NULL,
 CONSTRAINT [PK_KHOA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LOPHOC]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LOPHOC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MALOP] [varchar](50) NOT NULL,
	[TENLOP] [varchar](50) NOT NULL,
	[SOLUONG] [varchar](50) NULL CONSTRAINT [DF_LOPHOC_SOLUONG]  DEFAULT ((0)),
	[MAKHOA] [varchar](50) NULL,
 CONSTRAINT [PK_LOPHOC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MONHOC]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MONHOC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MAMONHOC] [varchar](50) NOT NULL,
	[TENMONHOC] [varchar](50) NOT NULL,
	[SOTINCHI] [int] NULL CONSTRAINT [DF_MONHOC_SOTINCHI]  DEFAULT ((0)),
	[TIETTHUCHANH] [int] NULL CONSTRAINT [DF_MONHOC_TIETTHUCHANH]  DEFAULT ((0)),
	[TIETLYTHUYET] [int] NULL CONSTRAINT [DF_MONHOC_TIETLYTHUYET]  DEFAULT ((0)),
 CONSTRAINT [PK_MONHOC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NGUOIDUNG]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NGUOIDUNG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TENDANGNHAP] [varchar](50) NOT NULL,
	[MATKHAU] [varchar](max) NOT NULL,
	[LOAI] [varchar](50) NULL,
 CONSTRAINT [PK_NGUOIDUNG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SINHVIEN]    Script Date: 11/2/2024 11:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SINHVIEN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MASINHVIEN] [varchar](50) NOT NULL,
	[TENSINHVIEN] [varchar](50) NOT NULL,
	[NGAYSINH] [date] NULL,
	[GIOITINH] [varchar](50) NULL,
	[QUEQUAN] [varchar](50) NULL,
	[NGAYNHAPHOC] [date] NULL,
	[MALOP] [varchar](50) NULL,
	[MAKHOA] [varchar](50) NULL,
	[MACOVAN] [varchar](50) NULL,
 CONSTRAINT [PK_SINHVIEN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [QLISNHVIEN] SET  READ_WRITE 
GO
