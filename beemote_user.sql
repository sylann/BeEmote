USE mysql;

DELETE FROM `user` WHERE `User` = 'beemote';
DELETE FROM `db` WHERE `Db` = 'beemote' AND `User` = 'beemote';

INSERT INTO `user` VALUES ('localhost','beemote','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','N','','','','',0,0,0,0,'mysql_native_password','*F88AF64F1D4762F2B3FCD678C228468E582543DD','N','2017-05-09 04:04:43',NULL,'N');
INSERT INTO `db` VALUES ('localhost','beemote','beemote','Y','Y','Y','Y','Y','Y','N','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y');

FLUSH PRIVILEGES;