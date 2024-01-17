-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jan 03, 2024 at 07:46 AM
-- Server version: 10.5.19-MariaDB-cll-lve
-- PHP Version: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 
--

-- --------------------------------------------------------

--
-- Table structure for table `Cities`
--

CREATE TABLE IF NOT EXISTS `Cities` (
  `Id` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `Cities`
--

INSERT IGNORE INTO `Cities` (`Id`, `Name`) VALUES
(1, 'Barcelona'),
(2, 'Madrid'),
(4, 'Málaga'),
(3, 'Valencia');

-- --------------------------------------------------------

--
-- Table structure for table `Clients`
--

CREATE TABLE IF NOT EXISTS `Clients` (
  `Id` int(11) NOT NULL,
  `Dni` varchar(9) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Surnames` varchar(128) DEFAULT NULL,
  `Address` varchar(256) DEFAULT NULL,
  `Telephone` varchar(16) DEFAULT NULL,
  `Email` varchar(320) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Crates`
--

CREATE TABLE IF NOT EXISTS `Crates` (
  `Id` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  `Kilograms` double NOT NULL,
  `Price` decimal(6,4) NOT NULL,
  `Stock` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- --------------------------------------------------------

--
-- Table structure for table `CratesSales`
--

CREATE TABLE IF NOT EXISTS `CratesSales` (
  `Id` int(11) NOT NULL,
  `ClientId` int(11) NOT NULL,
  `CrateId` int(11) NOT NULL,
  `Amount` int(11) NOT NULL,
  `TotalPrice` decimal(6,4) NOT NULL,
  `PayOptionId` int(11) NOT NULL,
  `DateTimeUtc` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `CropTypes`
--

CREATE TABLE IF NOT EXISTS `CropTypes` (
  `Id` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Description` varchar(128) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `CropTypes`
--

INSERT IGNORE INTO `CropTypes` (`Id`, `Name`, `Description`) VALUES
(1, 'Hortalizas', NULL),
(2, 'Leguminosas', NULL),
(3, 'Frutales', NULL),
(4, 'Cereales', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `FarmerHirings`
--

CREATE TABLE IF NOT EXISTS `FarmerHirings` (
  `Id` int(11) NOT NULL,
  `ClientId` int(11) NOT NULL,
  `FarmerId` int(11) NOT NULL,
  `DateTimeUtc` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- --------------------------------------------------------

--
-- Table structure for table `Farmers`
--

CREATE TABLE IF NOT EXISTS `Farmers` (
  `Id` int(11) NOT NULL,
  `CityId` int(11) NOT NULL,
  `CropTypeId` int(11) NOT NULL,
  `Dni` varchar(9) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Surnames` varchar(128) DEFAULT NULL,
  `Address` varchar(256) DEFAULT NULL,
  `Telephone` varchar(16) DEFAULT NULL,
  `Email` varchar(320) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `PayOptions`
--

CREATE TABLE IF NOT EXISTS `PayOptions` (
  `Id` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Description` varchar(128) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `PayOptions`
--

INSERT IGNORE INTO `PayOptions` (`Id`, `Name`, `Description`) VALUES
(1, 'Efectivo', NULL),
(2, 'Tarjeta', NULL),
(3, 'Bizum', NULL),
(4, 'PayPal', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `Users`
--

CREATE TABLE IF NOT EXISTS `Users` (
  `Id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Password` longtext NOT NULL,
  `Rol` longtext NOT NULL,
  `RefreshToken` longtext DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `__EFMigrationsHistory`
--

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20240102090447_MySQL_init', '7.0.13');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Cities`
--
ALTER TABLE `Cities`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Cities_Name` (`Name`);

--
-- Indexes for table `Clients`
--
ALTER TABLE `Clients`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Clients_Dni` (`Dni`),
  ADD UNIQUE KEY `IX_Clients_Email` (`Email`);

--
-- Indexes for table `Crates`
--
ALTER TABLE `Crates`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Crates_Name` (`Name`);

--
-- Indexes for table `CratesSales`
--
ALTER TABLE `CratesSales`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_CratesSales_ClientId` (`ClientId`),
  ADD KEY `IX_CratesSales_CrateId` (`CrateId`),
  ADD KEY `IX_CratesSales_PayOptionId` (`PayOptionId`);

--
-- Indexes for table `CropTypes`
--
ALTER TABLE `CropTypes`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_CropTypes_Name` (`Name`);

--
-- Indexes for table `FarmerHirings`
--
ALTER TABLE `FarmerHirings`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_FarmerHirings_ClientId` (`ClientId`),
  ADD KEY `IX_FarmerHirings_FarmerId` (`FarmerId`);

--
-- Indexes for table `Farmers`
--
ALTER TABLE `Farmers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Farmers_Dni` (`Dni`),
  ADD UNIQUE KEY `IX_Farmers_Email` (`Email`);

--
-- Indexes for table `PayOptions`
--
ALTER TABLE `PayOptions`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_PayOptions_Name` (`Name`);

--
-- Indexes for table `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Users_Name` (`Name`);

--
-- Indexes for table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Cities`
--
ALTER TABLE `Cities`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `Clients`
--
ALTER TABLE `Clients`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `Crates`
--
ALTER TABLE `Crates`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `CratesSales`
--
ALTER TABLE `CratesSales`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `CropTypes`
--
ALTER TABLE `CropTypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `FarmerHirings`
--
ALTER TABLE `FarmerHirings`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `Farmers`
--
ALTER TABLE `Farmers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `PayOptions`
--
ALTER TABLE `PayOptions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `CratesSales`
--
ALTER TABLE `CratesSales`
  ADD CONSTRAINT `FK_CratesSales_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_CratesSales_Crates_CrateId` FOREIGN KEY (`CrateId`) REFERENCES `Crates` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_CratesSales_PayOptions_PayOptionId` FOREIGN KEY (`PayOptionId`) REFERENCES `PayOptions` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `FarmerHirings`
--
ALTER TABLE `FarmerHirings`
  ADD CONSTRAINT `FK_FarmerHirings_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_FarmerHirings_Farmers_FarmerId` FOREIGN KEY (`FarmerId`) REFERENCES `Farmers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
