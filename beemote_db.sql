CREATE DATABASE  IF NOT EXISTS `beemote` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin */;
USE `beemote`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: beemote
-- ------------------------------------------------------
-- Server version	5.7.18-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `emotion`
--

DROP TABLE IF EXISTS `emotion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `emotion` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idimg` int(11) unsigned NOT NULL COMMENT 'identifies the image analysis where this emotion was present',
  `dominant` varchar(10) COLLATE utf8mb4_bin NOT NULL,
  `rleft` int(11) unsigned DEFAULT NULL,
  `rtop` int(11) unsigned DEFAULT NULL,
  `rwidth` int(11) unsigned DEFAULT NULL,
  `rheight` int(11) unsigned DEFAULT NULL,
  `anger` double unsigned NOT NULL,
  `contempt` double unsigned NOT NULL,
  `disgust` double unsigned NOT NULL,
  `fear` double unsigned NOT NULL,
  `happiness` double unsigned NOT NULL,
  `neutral` double unsigned NOT NULL,
  `sadness` double unsigned NOT NULL,
  `surprise` double unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `imganalysis`
--

DROP TABLE IF EXISTS `imganalysis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `imganalysis` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `datestamp` datetime NOT NULL COMMENT 'Allows to get information per day',
  `nbfaces` int(11) unsigned NOT NULL COMMENT 'Corresponds to the number of emotion entries linked to this analysis',
  `imagepath` text COLLATE utf8mb4_bin,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `textanalysis`
--

DROP TABLE IF EXISTS `textanalysis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `textanalysis` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `datestamp` datetime NOT NULL,
  `langname` varchar(25) COLLATE utf8mb4_bin DEFAULT NULL,
  `langiso` char(2) COLLATE utf8mb4_bin DEFAULT NULL,
  `langscore` double unsigned DEFAULT NULL,
  `textscore` double unsigned DEFAULT NULL,
  `textcontent` text COLLATE utf8mb4_bin,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'beemote'
--
/*!50003 DROP PROCEDURE IF EXISTS `insertinto_emotion` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertinto_emotion`(
	IN IdImg int(11) UNSIGNED,
	IN Dominant VARCHAR(10),
	IN RLeft INT(11) UNSIGNED,
    IN RTop INT(11) UNSIGNED,
    IN RWidth INT(11) UNSIGNED,
    IN RHeight INT(11) UNSIGNED,
    IN Anger DOUBLE UNSIGNED,
    IN Contempt DOUBLE UNSIGNED,
    IN Disgust DOUBLE UNSIGNED,
    IN Fear DOUBLE UNSIGNED,
    IN Happiness DOUBLE UNSIGNED,
    IN Neutral DOUBLE UNSIGNED,
    IN Sadness DOUBLE UNSIGNED,
    IN Surprise DOUBLE UNSIGNED
)
    READS SQL DATA
    COMMENT 'Inserts a new entry in the emotion table. Then returns the last insert id.'
BEGIN
    
	INSERT INTO emotion
    (idimg, dominant, rleft, rtop, rwidth, rheight, anger, contempt, disgust, fear, happiness, neutral, sadness, surprise)
    Values
    (IdImg, Dominant, RLeft, RTop, RWidth, RHeight, Anger, Contempt, Disgust, Fear, Happiness, Neutral, Sadness, Surprise);
    
    SELECT LAST_INSERT_ID();

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertinto_imganalysis` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertinto_imganalysis`(
	IN NbFaces INT(11) UNSIGNED,
    IN ImagePath VARCHAR(255)
)
    READS SQL DATA
    COMMENT 'Inserts a new entry in the imganalysis table. Then returns the last insert id.'
BEGIN

	INSERT INTO imganalysis
    (datestamp, nbfaces, imagepath)
    Values
    (NOW(), NbFaces, ImagePath);
    
    SELECT LAST_INSERT_ID();

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertinto_textanalysis` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertinto_textanalysis`(
    IN LangName VARCHAR(25),
    IN LangISO CHAR(2),
    IN LangScore DOUBLE UNSIGNED,
    IN TextScore DOUBLE UNSIGNED,
    IN textcontent TEXT
)
    READS SQL DATA
    COMMENT 'Inserts a new entry in the textanalysis table. Then returns the last insert id.'
BEGIN

	INSERT INTO textanalysis
    (datestamp, langname, langiso, langscore, textscore, textcontent)
    VALUES
    (NOW(), LangName, LangISO, LangScore, TextScore, TextContent);
    
    SELECT LAST_INSERT_ID();

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-25 14:21:35
