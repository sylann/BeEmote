CREATE DATABASE  IF NOT EXISTS `beemote`;
USE `beemote`;
DROP PROCEDURE IF EXISTS `averagecallsperday_emotion`;
DROP PROCEDURE IF EXISTS `averagecallsperday_textanalysis`;
DROP PROCEDURE IF EXISTS `averagefacecount_emotion`;
DROP PROCEDURE IF EXISTS `callsperday_emotion`;
DROP PROCEDURE IF EXISTS `callsperday_textanalysis`;
DROP PROCEDURE IF EXISTS `dominantdistribution_emotion`;
DROP PROCEDURE IF EXISTS `insertinto_emotion`;
DROP PROCEDURE IF EXISTS `insertinto_imganalysis`;
DROP PROCEDURE IF EXISTS `insertinto_textanalysis`;
DROP PROCEDURE IF EXISTS `languagedistribution_textanalysis`;
DROP PROCEDURE IF EXISTS `sentimentdistribution_textanalysis`;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `averagecallsperday_emotion`()
BEGIN

	SELECT AVG(callperday)
    FROM (
		SELECT count(*) callperday, datestamp
		FROM beemote.imganalysis
		GROUP BY DAYOFYEAR(datestamp)
	) AS calls;

END ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `averagecallsperday_textanalysis`()
BEGIN

	SELECT AVG(callperday)
    FROM (
		SELECT count(*) callperday, datestamp
		FROM beemote.textanalysis
		GROUP BY DAYOFYEAR(datestamp)
	) AS calls;

END ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `averagefacecount_emotion`()
BEGIN

	SELECT AVG(nbfaces)
	FROM beemote.imganalysis;

END ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `callsperday_emotion`()
BEGIN

	SELECT COUNT(*) calls, datestamp
    FROM beemote.imganalysis
    GROUP BY DAYOFYEAR(datestamp);

END ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `callsperday_textanalysis`()
BEGIN

	SELECT COUNT(*) calls, datestamp
    FROM beemote.textanalysis
    GROUP BY DAYOFYEAR(datestamp);

END ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `dominantdistribution_emotion`()
BEGIN

	SELECT Dominant as `Name`, COUNT(Dominant) as `Count`
	FROM emotion
	GROUP BY Dominant
	ORDER BY `Count` DESC;

END ;;
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `languagedistribution_textanalysis`()
BEGIN

	SELECT langname as `Name`, COUNT(langname) / t.totalcount as `Proportion`
	FROM textanalysis, (
		SELECT COUNT(*) as totalcount
        FROM textanalysis
	) t
	GROUP BY `Name`;

END ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sentimentdistribution_textanalysis`()
BEGIN

	SELECT "[0.00 - 0.30]" as `Rank`, COUNT(*) as `Count`
    FROM beemote.textanalysis
    WHERE textscore BETWEEN 0 AND 0.3
    
	UNION ALL
	
    SELECT "[0.31 - 0.60]" as `Rank`, COUNT(*) as `Count`
    FROM beemote.textanalysis
    WHERE textscore BETWEEN 0.31 AND 0.6
	
    UNION ALL
	
    SELECT "[0.61 - 1.00]" as `Rank`, COUNT(*) as `Count`
    FROM beemote.textanalysis
    WHERE textscore BETWEEN 0.61 AND 1;

END ;;
DELIMITER ;
