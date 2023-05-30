CREATE DATABASE `pizzas`;

CREATE TABLE `pizzas`.`pizza` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) NOT NULL,
  `Image` longblob,
  `Price` double NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `pizzas`.`topping` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) NOT NULL,
  `ToppingAditionalPrice` double NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `pizzas`.`pizza_topping` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdPizza` int NOT NULL,
  `IdTopping` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdPizza_idx` (`IdPizza`),
  KEY `IdTopping_idx` (`IdTopping`),
  CONSTRAINT `IdPizza` FOREIGN KEY (`IdPizza`) REFERENCES `pizza` (`Id`),
  CONSTRAINT `IdTopping` FOREIGN KEY (`IdTopping`) REFERENCES `topping` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `pizzas`.`order` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdPizza` int DEFAULT NULL,
  `CustomerName` varchar(200) NOT NULL,
  `Address` varchar(300) NOT NULL,
  `TotalPrice` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdPizza_idx` (`IdPizza`),
  CONSTRAINT `IdPizza2` FOREIGN KEY (`IdPizza`) REFERENCES `pizza` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `pizzas`.`order_topping` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdTopping` int NOT NULL,
  `IdOrder` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdOrder2_idx` (`IdOrder`),
  KEY `IdTopping2_idx` (`IdTopping`),
  CONSTRAINT `IdIdOrder2` FOREIGN KEY (`IdOrder`) REFERENCES `order` (`Id`),
  CONSTRAINT `IdTopping2` FOREIGN KEY (`IdTopping`) REFERENCES `topping` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
