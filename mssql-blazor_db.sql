USE [master]
GO

IF DB_ID('blazor_db') IS NOT NULL
  set noexec on               -- prevent creation when already exists

/****** Object:  Database [blazor_db]    Script Date: 04.09.2023 18:33:09 ******/
CREATE DATABASE [blazor_db];
GO

USE [blazor_db]
GO

GRANT ALL ON blazor_db TO [user];
GO

USE [master]
GO