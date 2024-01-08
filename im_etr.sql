-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 08, 2024 at 11:18 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `im_etr`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin_acc`
--

CREATE TABLE `admin_acc` (
  `AccountID` int(11) NOT NULL,
  `faculty_ID` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admin_acc`
--

INSERT INTO `admin_acc` (`AccountID`, `faculty_ID`, `password`) VALUES
(1, '21-FID-0209', 'fid102003'),
(2, '21-FID-0210', 'admin123'),
(3, '21-FID-0211', '111'),
(4, '21-FID-0209', '123'),
(5, '21-FID-0212', 'topetope');

-- --------------------------------------------------------

--
-- Table structure for table `scholars`
--

CREATE TABLE `scholars` (
  `scholar_ID` int(11) NOT NULL,
  `student_ID` varchar(255) NOT NULL,
  `Lname` varchar(255) NOT NULL,
  `Fname` varchar(255) NOT NULL,
  `Middle_initial` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `funds` int(11) NOT NULL,
  `signature` longblob DEFAULT NULL,
  `valid_Id` longblob DEFAULT NULL,
  `school_year` varchar(255) NOT NULL,
  `semester` varchar(255) NOT NULL,
  `claim_status` varchar(255) NOT NULL,
  `date_claimed` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `Lname` varchar(255) NOT NULL,
  `Fname` varchar(255) NOT NULL,
  `middle_initial` varchar(255) NOT NULL,
  `student_ID` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`Lname`, `Fname`, `middle_initial`, `student_ID`) VALUES
('Domingo', 'Hernani', 'C', '21-UR-0112'),
('Belmoro', 'Jame', 'M', '21-UR-0116'),
('Ballesteros', 'Ryan King', 'A', '21-UR-0125'),
('Almoite', 'Micaella Shane', 'O', '21-UR-0171'),
('Cueva', 'Vincent Ryan', 'M', '21-UR-0172'),
(' Cayaban', 'Cedric Joel', 'F', '21-UR-0183'),
('Carpio', 'Christoperhson', 'C', '21-UR-0186'),
('Loleng', 'Justin', 'G', '21-UR-0209'),
('Galano', 'Dan Hebron', 'M', '21-UR-0216'),
('Macatiag', 'Elijah Japheth', 'P', '21-UR-0217'),
('Salcedo', 'Lance Jericho', 'A', '21-UR-0219'),
('Tamayo', 'Janela', 'F', '21-UR-0776'),
('Biag', 'Joreson Mark', 'B', '22-UR-0006');

-- --------------------------------------------------------

--
-- Table structure for table `stud_acc`
--

CREATE TABLE `stud_acc` (
  `acc_id` int(11) NOT NULL,
  `student_ID` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `stud_acc`
--

INSERT INTO `stud_acc` (`acc_id`, `student_ID`, `password`) VALUES
(1, '21-UR-0209', 'jloleng10'),
(2, '21-UR-0186', 'topetope'),
(3, '22-UR-0006', 'biag123'),
(4, '21-UR-0125', '12233'),
(5, '21-UR-0112', 'dsds');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin_acc`
--
ALTER TABLE `admin_acc`
  ADD PRIMARY KEY (`AccountID`);

--
-- Indexes for table `scholars`
--
ALTER TABLE `scholars`
  ADD PRIMARY KEY (`scholar_ID`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`student_ID`);

--
-- Indexes for table `stud_acc`
--
ALTER TABLE `stud_acc`
  ADD PRIMARY KEY (`acc_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin_acc`
--
ALTER TABLE `admin_acc`
  MODIFY `AccountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `scholars`
--
ALTER TABLE `scholars`
  MODIFY `scholar_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=63;

--
-- AUTO_INCREMENT for table `stud_acc`
--
ALTER TABLE `stud_acc`
  MODIFY `acc_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
