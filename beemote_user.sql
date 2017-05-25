USE mysql;

DELETE FROM `user` WHERE `User` = 'beemote';
DELETE FROM `db` WHERE `Db` = 'beemote' AND `User` = 'beemote';

CREATE USER 'beemote'@'localhost' IDENTIFIED BY '?beemote';
GRANT ALL ON beemote.* TO 'beemote'@'localhost';

FLUSH PRIVILEGES;