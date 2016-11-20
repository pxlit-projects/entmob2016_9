-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Gegenereerd op: 07 okt 2016 om 15:36
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
  `act1` int(11) NOT NULL,
  `act2` int(11) NOT NULL,
  `act3` int(11) NOT NULL,
  `act4` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `action`
--

INSERT INTO `action` (`actId`, `act1`, `act2`, `act3`, `act4`) VALUES
(1, 555, 666, 777, 888),
(2, 11, 22, 33, 44);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `command`
--

CREATE TABLE `command` (
  `commandId` int(11) NOT NULL,
  `Command1` int(11) NOT NULL,
  `Command2` int(11) NOT NULL,
  `Command3` int(11) NOT NULL,
  `Command4` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `command`
--

INSERT INTO `command` (`commandId`, `Command1`, `Command2`, `Command3`, `Command4`) VALUES
(1, 55, 70, 80, 100),
(2, 1, 2, 3, 4);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `profile`
--

CREATE TABLE `profile` (
  `profileId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `actionId` int(11) NOT NULL,
  `commandId` int(11) NOT NULL,
  `profileName` varchar(999) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `profile`
--

INSERT INTO `profile` (`profileId`, `userId`, `actionId`, `commandId`, `profileName`) VALUES
(1, 1, 1, 1, ''),
(2, 1, 2, 1, ''),
(3, 2, 2, 2, '');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user`
--

CREATE TABLE `user` (
  `userId` int(11) NOT NULL,
  `firstName` text NOT NULL,
  `lastName` text NOT NULL,
  `Password` mediumtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `user`
--

INSERT INTO `user` (`userId`, `firstName`, `lastName`, `Password`) VALUES
(1, 'Giel', 'Reynders', 'dsfqfsqfsqfsqf'),
(2, 'Maarten', 'Hermans', 'dfaefzrzarzfsqdf');
--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `action`
--
ALTER TABLE `action`
  ADD PRIMARY KEY (`actId`);

--
-- Indexen voor tabel `command`
--
ALTER TABLE `command`
  ADD PRIMARY KEY (`commandId`);

--
-- Indexen voor tabel `profile`
--
ALTER TABLE `profile`
  ADD PRIMARY KEY (`profileId`);

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
  MODIFY `actId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT voor een tabel `command`
--
ALTER TABLE `command`
  MODIFY `commandId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT voor een tabel `profile`
--
ALTER TABLE `profile`
  MODIFY `profileId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT voor een tabel `user`
--
ALTER TABLE `user`
  MODIFY `userId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
