CREATE USER 'beemote'@'localhost' IDENTIFIED BY '?beemote';
GRANT ALL ON beemote.* TO 'beemote'@'localhost';

FLUSH PRIVILEGES;