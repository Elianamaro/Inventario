USE [master]
GO
/****** Object:  Database [Registro_Tiendas]    Script Date: 12-10-2021 1:16:56 ******/
CREATE DATABASE [Registro_Tiendas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Registro_Tiendas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Registro_Tiendas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Registro_Tiendas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Registro_Tiendas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Registro_Tiendas] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Registro_Tiendas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Registro_Tiendas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET ARITHABORT OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Registro_Tiendas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Registro_Tiendas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Registro_Tiendas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Registro_Tiendas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Registro_Tiendas] SET  MULTI_USER 
GO
ALTER DATABASE [Registro_Tiendas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Registro_Tiendas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Registro_Tiendas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Registro_Tiendas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Registro_Tiendas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Registro_Tiendas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Registro_Tiendas] SET QUERY_STORE = OFF
GO
USE [Registro_Tiendas]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 12-10-2021 1:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 12-10-2021 1:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[id_marca] [int] IDENTITY(1,1) NOT NULL,
	[nombre_marca] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Marca] PRIMARY KEY CLUSTERED 
(
	[id_marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 12-10-2021 1:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre_producto] [varchar](50) NOT NULL,
	[codigo_producto] [varchar](50) NOT NULL,
	[precio_compra] [int] NOT NULL,
	[precio_venta] [int] NOT NULL,
	[descripcion_producto] [varchar](500) NULL,
	[id_marca] [int] NOT NULL,
	[id_tienda] [int] NOT NULL,
	[id_categoria] [int] NOT NULL,
	[fecha_recepcion] [date] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tienda]    Script Date: 12-10-2021 1:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tienda](
	[id_tienda] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[ubicacion] [varchar](150) NOT NULL,
	[telefono] [int] NOT NULL,
	[correo] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Tienda] PRIMARY KEY CLUSTERED 
(
	[id_tienda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categorias] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[Categorias] ([id_categoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categorias]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Marca] FOREIGN KEY([id_marca])
REFERENCES [dbo].[Marca] ([id_marca])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Marca]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Tienda1] FOREIGN KEY([id_tienda])
REFERENCES [dbo].[Tienda] ([id_tienda])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Tienda1]
GO
USE [master]
GO
ALTER DATABASE [Registro_Tiendas] SET  READ_WRITE 
GO
