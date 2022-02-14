-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Gép: localhost:3306
-- Létrehozás ideje: 2022. Feb 10. 17:38
-- Kiszolgáló verziója: 5.7.35-38
-- PHP verzió: 7.2.34-28+ubuntu18.04.1+deb.sury.org+1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `s4_Project_M`
--
CREATE DATABASE IF NOT EXISTS `s4_Project_M` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `s4_Project_M`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ertekeles`
--

CREATE TABLE `ertekeles` (
  `ertekeles_id` int(11) NOT NULL,
  `ertekeles_nev` varchar(50) NOT NULL,
  `ertekeles_uzenet` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- A tábla adatainak kiíratása `ertekeles`
--

INSERT INTO `ertekeles` (`ertekeles_id`, `ertekeles_nev`, `ertekeles_uzenet`) VALUES
(1, 'idkxddd', 'nem tudom hmmmmmmmm');

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
-- Tábla szerkezet ehhez a táblához `score`
--

CREATE TABLE `score` (
  `score_id` int(11) NOT NULL,
  `score_user_id` int(11) NOT NULL,
  `score_scored_point` int(11) NOT NULL,
  `score_date_time` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `statisztika`
--

CREATE TABLE `statisztika` (
  `statisztika_id` int(11) NOT NULL,
  `statisztika_nev` varchar(50) NOT NULL,
  `statisztika_pont` int(11) NOT NULL,
  `statisztika_halal` int(11) NOT NULL,
  `statisztika_ido` varchar(50) NOT NULL,
  `statisztika_date` date NOT NULL,
  `statisztika_level_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- A tábla adatainak kiíratása `statisztika`
--

INSERT INTO `statisztika` (`statisztika_id`, `statisztika_nev`, `statisztika_pont`, `statisztika_halal`, `statisztika_ido`, `statisztika_date`, `statisztika_level_id`) VALUES
(8, 'Shatest', 250, 0, '9,2319689', '2022-02-09', 1),
(9, 'Shatest', 500, 0, '19,0537992', '2022-02-09', 2),
(10, 'Shatest', 1125, 2, '27,5458397', '2022-02-09', 3);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(50) COLLATE utf8_hungarian_ci NOT NULL,
  `user_personal_highscore` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `user`
--

INSERT INTO `user` (`user_id`, `user_name`, `user_personal_highscore`) VALUES
(1, 'nigr', 0),
(2, 'test', 420);

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
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `score`
--
ALTER TABLE `score`
  ADD PRIMARY KEY (`score_id`),
  ADD KEY `score_user_id` (`score_user_id`);

--
-- A tábla indexei `statisztika`
--
ALTER TABLE `statisztika`
  ADD PRIMARY KEY (`statisztika_id`);

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
  MODIFY `ertekeles_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `score`
--
ALTER TABLE `score`
  MODIFY `score_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `statisztika`
--
ALTER TABLE `statisztika`
  MODIFY `statisztika_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT a táblához `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `score`
--
ALTER TABLE `score`
  ADD CONSTRAINT `score_ibfk_1` FOREIGN KEY (`score_user_id`) REFERENCES `user` (`user_id`);

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
