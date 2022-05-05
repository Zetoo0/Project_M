-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2022. Máj 05. 09:14
-- Kiszolgáló verziója: 10.4.17-MariaDB
-- PHP verzió: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `lonely_knight`
--
CREATE DATABASE IF NOT EXISTS `lonely_knight` DEFAULT CHARACTER SET utf16 COLLATE utf16_hungarian_ci;
USE `lonely_knight`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ertekeles`
--

CREATE TABLE `ertekeles` (
  `ertekeles_id` int(11) NOT NULL,
  `ertekeles_nev` varchar(50) CHARACTER SET utf16 COLLATE utf16_hungarian_ci NOT NULL,
  `ertekeles_uzenet` varchar(100) CHARACTER SET utf16 COLLATE utf16_hungarian_ci NOT NULL,
  `ertekeles_date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- A tábla adatainak kiíratása `ertekeles`
--

INSERT INTO `ertekeles` (`ertekeles_id`, `ertekeles_nev`, `ertekeles_uzenet`, `ertekeles_date`) VALUES
(2, 'dfs', 'XDDDDD', '2022-01-18'),
(3, 'teest', 'eawaad', '2022-01-18'),
(4, 'test', 'test', '2022-01-18'),
(5, 'A', 'A', '2022-01-18'),
(6, 'Gg', 'Ff', '2022-01-18'),
(7, 'Alma', 'Alma', '2022-01-18'),
(8, 'Alma', 'Alma', '2022-01-18'),
(9, 'Brh', 'Bfbf', '2022-01-19'),
(10, 'Web', 'Eb', '2022-01-21');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `level`
--

CREATE TABLE `level` (
  `level_id` int(11) NOT NULL,
  `mapPart_id` int(11) NOT NULL,
  `level_isUnlocked` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `mappart`
--

CREATE TABLE `mappart` (
  `mapPart_id` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `mappart`
--

INSERT INTO `mappart` (`mapPart_id`) VALUES
(1),
(2),
(3);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

CREATE TABLE `roles` (
  `id` int(11) NOT NULL,
  `name` varchar(255) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `createdAt` datetime NOT NULL,
  `updatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `roles`
--

INSERT INTO `roles` (`id`, `name`, `createdAt`, `updatedAt`) VALUES
(1, 'user', '2020-08-02 14:57:31', '2020-08-02 14:57:31'),
(2, 'moderator', '2020-08-02 14:57:31', '2020-08-02 14:57:31'),
(3, 'admin', '2020-08-02 14:57:31', '2020-08-02 14:57:31');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `statisztika`
--

CREATE TABLE `statisztika` (
  `statisztika_id` int(11) NOT NULL,
  `statisztika_user_id` int(11) NOT NULL,
  `statisztika_nev` varchar(50) NOT NULL,
  `statisztika_pont` int(11) NOT NULL,
  `statisztika_halal` int(11) NOT NULL,
  `statisztika_ido` varchar(50) NOT NULL,
  `statisztika_date` date NOT NULL,
  `statisztika_level_id` int(11) NOT NULL,
  `statisztika_part_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- A tábla adatainak kiíratása `statisztika`
--

INSERT INTO `statisztika` (`statisztika_id`, `statisztika_user_id`, `statisztika_nev`, `statisztika_pont`, `statisztika_halal`, `statisztika_ido`, `statisztika_date`, `statisztika_level_id`, `statisztika_part_id`) VALUES
(4, 5, 'Ilyennincs', 250, 0, '6,344666', '2022-04-17', 1, 1),
(6, 6, 'Anyd', 250, 0, '6,344666', '2022-04-17', 1, 1),
(7, 6, 'Anyd', 250, 2, '19,85943', '2022-04-04', 1, 2),
(8, 6, 'Anyd', 250, 0, '6,344666', '2022-04-17', 1, 1),
(9, 8, 'Kivagyte', 250, 0, '6,344666', '2022-04-17', 1, 1),
(10, 8, 'Kivagyte', 250, 1, '19,76003', '2022-04-05', 1, 2),
(11, 8, 'Kivagyte', 125, 8, '20,64068', '2022-04-05', 3, 1),
(12, 5, 'Ilyennincs', 250, 0, '19,88435', '2022-04-17', 1, 2),
(13, 5, 'Ilyennincs', 250, 2, '13,12104', '2022-04-17', 3, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(50) NOT NULL,
  `user_password` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `user`
--

INSERT INTO `user` (`user_id`, `user_name`, `user_password`) VALUES
(1, 'ugysincsmégilyen', 'anyamkinja'),
(5, 'Ilyennincs', 'ilyense'),
(6, 'Anyd', 'anyd123'),
(7, 'Regtest', 'regtest123'),
(8, 'Kivagyte', 'nemtudom');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `email` varchar(255) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `password` varchar(255) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `createdAt` datetime NOT NULL,
  `updatedAt` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `username`, `email`, `password`, `createdAt`, `updatedAt`) VALUES
(1, 'user', 'user', '$2a$08$gXM4pyuhZFlC72PeAwxrUOR0uA31/d2PdgnHP35JGV.0bQNiZBatS', '0000-00-00 00:00:00', '0000-00-00 00:00:00'),
(2, 'mod', 'mod', '$2a$08$gXM4pyuhZFlC72PeAwxrUOR0uA31/d2PdgnHP35JGV.0bQNiZBatS', '0000-00-00 00:00:00', '0000-00-00 00:00:00'),
(3, 'admin', 'admin', '$2a$08$97Ze1/hXfOX44WdC62Rq8uRkog9HYC1HukRX8eld2ZEKPyenM5v.G', '2020-08-02 15:03:59', '2020-08-02 15:03:59'),
(14, 'gdancso', 'enyedidani@gmail.com', '$2a$08$cTph/81zm/9jwNwLRWL7ruoLd7afxblop1t5Ke.dWn6PeTEc7VXii', '2022-01-21 10:42:21', '2022-01-21 10:42:21');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_roles`
--

CREATE TABLE `user_roles` (
  `createdAt` datetime NOT NULL,
  `updatedAt` datetime NOT NULL,
  `roleId` int(11) NOT NULL,
  `userId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `user_roles`
--

INSERT INTO `user_roles` (`createdAt`, `updatedAt`, `roleId`, `userId`) VALUES
('0000-00-00 00:00:00', '0000-00-00 00:00:00', 1, 1),
('2020-08-02 15:04:00', '2020-08-02 15:04:00', 1, 3),
('0000-00-00 00:00:00', '0000-00-00 00:00:00', 2, 2),
('0000-00-00 00:00:00', '0000-00-00 00:00:00', 2, 3),
('2020-08-02 15:04:00', '2020-08-02 15:04:00', 3, 3),
('2022-01-21 10:42:21', '2022-01-21 10:42:21', 3, 14);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `ertekeles`
--
ALTER TABLE `ertekeles`
  ADD PRIMARY KEY (`ertekeles_id`);

--
-- A tábla indexei `level`
--
ALTER TABLE `level`
  ADD PRIMARY KEY (`level_id`),
  ADD UNIQUE KEY `mapPart_id` (`mapPart_id`);

--
-- A tábla indexei `mappart`
--
ALTER TABLE `mappart`
  ADD PRIMARY KEY (`mapPart_id`);

--
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `statisztika`
--
ALTER TABLE `statisztika`
  ADD PRIMARY KEY (`statisztika_id`),
  ADD KEY `statisztika_user_id` (`statisztika_user_id`);

--
-- A tábla indexei `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `user_roles`
--
ALTER TABLE `user_roles`
  ADD PRIMARY KEY (`roleId`,`userId`),
  ADD KEY `userId` (`userId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `ertekeles`
--
ALTER TABLE `ertekeles`
  MODIFY `ertekeles_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `statisztika`
--
ALTER TABLE `statisztika`
  MODIFY `statisztika_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT a táblához `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `statisztika`
--
ALTER TABLE `statisztika`
  ADD CONSTRAINT `statisztika_ibfk_1` FOREIGN KEY (`statisztika_user_id`) REFERENCES `user` (`user_id`);

--
-- Megkötések a táblához `user_roles`
--
ALTER TABLE `user_roles`
  ADD CONSTRAINT `user_roles_ibfk_1` FOREIGN KEY (`roleId`) REFERENCES `roles` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `user_roles_ibfk_2` FOREIGN KEY (`userId`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
