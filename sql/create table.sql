CREATE TABLE `location` (
  `mappoint` point NOT NULL,
  `name` varchar(150) NOT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`,`mappoint`(25)),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=196606 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;