-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Gegenereerd op: 07 okt 2016 om 09:22
-- Serverversie: 10.1.13-MariaDB
-- PHP-versie: 7.0.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `emotiondb`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `action`
--

CREATE TABLE `action` (
  `actId` int(11) NOT NULL,
  `mappingId` int(11) NOT NULL,
  `act1` int(11) NOT NULL,
  `act2` int(11) NOT NULL,
  `act3` int(11) NOT NULL,
  `act4` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `commands`
--

CREATE TABLE `commands` (
  `commandId` int(11) NOT NULL,
  `Command1` int(11) NOT NULL,
  `Command2` int(11) NOT NULL,
  `Command3` int(11) NOT NULL,
  `Command4` int(11) NOT NULL,
  `mappingId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `mapping`
--

CREATE TABLE `mapping` (
  `mappingId` int(11) NOT NULL,
  `actionId` int(11) NOT NULL,
  `profilesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `profiles`
--

CREATE TABLE `profiles` (
  `profilesId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `mappingId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user`
--

CREATE TABLE `user` (
  `userId` int(11) NOT NULL,
  `firstName` text NOT NULL,
  `lastName` text NOT NULL,
  `profilesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `action`
--
ALTER TABLE `action`
  ADD PRIMARY KEY (`actId`);

--
-- Indexen voor tabel `commands`
--
ALTER TABLE `commands`
  ADD PRIMARY KEY (`commandId`);

--
-- Indexen voor tabel `mapping`
--
ALTER TABLE `mapping`
  ADD PRIMARY KEY (`mappingId`);

--
-- Indexen voor tabel `profiles`
--
ALTER TABLE `profiles`
  ADD PRIMARY KEY (`profilesId`);

--
-- Indexen voor tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`userId`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `action`
--
ALTER TABLE `action`
  MODIFY `actId` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `commands`
--
ALTER TABLE `commands`
  MODIFY `commandId` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `mapping`
--
ALTER TABLE `mapping`
  MODIFY `mappingId` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `profiles`
--
ALTER TABLE `profiles`
  MODIFY `profilesId` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `user`
--
ALTER TABLE `user`
  MODIFY `userId` int(11) NOT NULL AUTO_INCREMENT;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
