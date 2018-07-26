-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 26, 2018 at 03:25 PM
-- Server version: 5.6.38
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `POS`
--
CREATE DATABASE IF NOT EXISTS `POS` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `POS`;

-- --------------------------------------------------------

--
-- Table structure for table `drinks`
--

CREATE TABLE `drinks` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `price` double NOT NULL,
  `category` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `drinks`
--

INSERT INTO `drinks` (`id`, `name`, `price`, `category`) VALUES
(1, 'IPA', 6, 'beer'),
(2, 'Pilsner', 5, 'beer'),
(3, 'Porter', 6, 'beer'),
(4, 'Cider', 6, 'beer'),
(5, 'Cola', 2, 'NA'),
(6, 'Diet', 2, 'NA'),
(7, 'Ice Tea', 2, 'NA'),
(8, 'Lemonade', 2, 'NA'),
(9, 'Chardonnay', 7, 'wine'),
(10, 'Pinot Noir', 7, 'wine');

-- --------------------------------------------------------

--
-- Table structure for table `food`
--

CREATE TABLE `food` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `price` double NOT NULL,
  `category` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `food`
--

INSERT INTO `food` (`id`, `name`, `price`, `category`) VALUES
(1, 'Burger', 15, 'entree'),
(2, 'Fries (Basket)', 8, 'app'),
(3, 'BLT', 12, 'entree'),
(4, 'Dr. Pepper Pulled Pork Sandwich', 15, 'entree'),
(5, 'Side Salad', 4, 'app'),
(6, 'Cobb Salad', 12, 'entree'),
(7, 'Onion Rings', 4.5, 'app'),
(8, 'Nachos', 8, 'app');

-- --------------------------------------------------------

--
-- Table structure for table `history`
--

CREATE TABLE `history` (
  `id` int(11) NOT NULL,
  `table_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `mods`
--

CREATE TABLE `mods` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `price` float NOT NULL,
  `category` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mods`
--

INSERT INTO `mods` (`id`, `name`, `price`, `category`) VALUES
(1, 'Ketchup', 0, 'food'),
(2, 'Mustard', 0, 'food'),
(3, 'Onion', 0, 'food'),
(4, 'Mushrooms', 1, 'food'),
(5, 'Bacon', 2, 'food'),
(6, 'Ranch', 0, 'food'),
(7, 'Vinegarette', 0, 'food'),
(8, 'Bleu Cheese', 0, 'food'),
(9, 'Rare', 0, 'temp'),
(10, 'Mid Rare', 0, 'temp'),
(11, 'Medium', 0, 'temp'),
(12, 'Medium Well', 0, 'temp'),
(13, 'Well', 25, 'temp'),
(14, 'Avocado', 3, 'food'),
(15, 'Tomato', 0, 'food'),
(16, 'Gluten Free Bun', 2.5, 'food');

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `ticket_id` int(11) NOT NULL,
  `food_id` int(11) NOT NULL,
  `drink_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `ticket_id`, `food_id`, `drink_id`) VALUES
(1, 2, 2, 2);

-- --------------------------------------------------------

--
-- Table structure for table `tables`
--

CREATE TABLE `tables` (
  `id` int(11) NOT NULL,
  `number` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tables`
--

INSERT INTO `tables` (`id`, `number`) VALUES
(1, 1),
(3, 2),
(4, 3),
(5, 10),
(6, 11),
(7, 12),
(8, 13),
(9, 14),
(10, 15);

-- --------------------------------------------------------

--
-- Table structure for table `tickets`
--

CREATE TABLE `tickets` (
  `id` int(11) NOT NULL,
  `ticket_number` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `table_id` int(11) NOT NULL,
  `active` char(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tickets`
--

INSERT INTO `tickets` (`id`, `ticket_number`, `user_id`, `table_id`, `active`) VALUES
(1, 1, 1, 1, ''),
(2, 23, 1, 4, ''),
(3, 631, 3, 0, ''),
(4, 303, 0, 0, ''),
(5, 590, 3, 1, ''),
(6, 729, 3, 1, ''),
(7, 577, 2, 1, ''),
(8, 360, 2, 1, ''),
(9, 0, 1, 0, ''),
(10, 234, 1, 0, ''),
(11, 897, 3, 1, ''),
(12, 422, 3, 1, ''),
(13, 815, 3, 1, ''),
(14, 610, 3, 1, ''),
(15, 736, 3, 1, ''),
(16, 393, 3, 1, ''),
(17, 494, 3, 1, ''),
(18, 672, 1, 1, ''),
(19, 800, 1, 1, ''),
(20, 284, 1, 1, ''),
(21, 650, 1, 1, ''),
(22, 974, 1, 1, ''),
(23, 276, 1, 1, ''),
(24, 889, 1, 1, ''),
(25, 990, 1, 1, ''),
(26, 517, 1, 1, ''),
(27, 807, 1, 1, ''),
(28, 311, 1, 1, ''),
(29, 491, 1, 1, ''),
(30, 815, 1, 1, ''),
(31, 239, 1, 1, ''),
(32, 688, 1, 1, ''),
(33, 346, 1, 1, ''),
(34, 899, 1, 1, ''),
(35, 323, 1, 1, ''),
(36, 597, 0, 0, ''),
(37, 723, 1, 1, ''),
(38, 233, 0, 0, ''),
(39, 449, 1, 1, ''),
(40, 652, 0, 0, ''),
(41, 993, 1, 1, ''),
(42, 504, 0, 0, ''),
(43, 579, 0, 0, ''),
(44, 941, 0, 0, ''),
(45, 343, 0, 0, ''),
(46, 606, 0, 0, ''),
(47, 329, 0, 0, ''),
(48, 589, 0, 0, ''),
(49, 335, 0, 0, ''),
(50, 989, 0, 0, ''),
(51, 453, 0, 0, ''),
(52, 864, 0, 0, ''),
(53, 317, 0, 0, ''),
(54, 830, 0, 0, ''),
(55, 590, 0, 0, ''),
(56, 606, 0, 0, ''),
(57, 458, 2, 1, ''),
(58, 438, 2, 1, ''),
(59, 657, 0, 0, ''),
(60, 435, 0, 0, ''),
(61, 319, 1, 1, ''),
(62, 819, 1, 1, ''),
(63, 281, 1, 1, ''),
(64, 698, 1, 1, ''),
(65, 331, 1, 1, ''),
(66, 575, 1, 1, ''),
(67, 784, 1, 1, ''),
(68, 347, 1, 1, ''),
(69, 818, 1, 1, ''),
(70, 693, 1, 1, ''),
(71, 768, 1, 1, ''),
(72, 686, 1, 1, ''),
(73, 942, 1, 1, ''),
(74, 576, 1, 1, ''),
(75, 897, 1, 1, ''),
(76, 887, 1, 1, ''),
(77, 607, 1, 1, ''),
(78, 504, 1, 1, ''),
(79, 560, 1, 1, ''),
(80, 372, 1, 1, ''),
(81, 425, 1, 1, ''),
(82, 824, 1, 1, ''),
(83, 308, 1, 1, ''),
(84, 582, 1, 1, ''),
(85, 211, 1, 1, ''),
(86, 619, 1, 1, ''),
(87, 964, 1, 1, ''),
(88, 690, 1, 1, ''),
(89, 225, 1, 1, ''),
(90, 822, 1, 1, ''),
(91, 525, 1, 1, '');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `admin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `admin`) VALUES
(1, 'Jessica', 0),
(2, 'Tim', 1),
(3, 'Franz', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `drinks`
--
ALTER TABLE `drinks`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `food`
--
ALTER TABLE `food`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `history`
--
ALTER TABLE `history`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `mods`
--
ALTER TABLE `mods`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tables`
--
ALTER TABLE `tables`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tickets`
--
ALTER TABLE `tickets`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `drinks`
--
ALTER TABLE `drinks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `food`
--
ALTER TABLE `food`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `history`
--
ALTER TABLE `history`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `mods`
--
ALTER TABLE `mods`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tables`
--
ALTER TABLE `tables`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `tickets`
--
ALTER TABLE `tickets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=92;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
