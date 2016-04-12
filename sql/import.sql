insert into location (mappoint, name) 

select point(longitude, latitude) as mappoint, name from mytable where latitude is not null 

