CREATE DEFINER=`root`@`localhost` FUNCTION `harvesine`(lat1 double, lon1 double, lat2 double, lon2 double) RETURNS double
return  3956 * 2 * ASIN(SQRT(POWER(SIN((lat1 - abs(lat2)) * pi()/180 / 2), 2) 
         + COS(abs(lat1) * pi()/180 ) * COS(abs(lat2) * pi()/180) * POWER(SIN((lon1 - lon2) * pi()/180 / 2), 2) ))