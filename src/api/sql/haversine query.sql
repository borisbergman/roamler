set @lat= 37.615223;
set @lon = -122.389979;
set @dist = 1000;
set @rlon1 = @lon-@dist/abs(cos(radians(@lat))*110); 
set @rlon2 = @lon+@dist/abs(cos(radians(@lat))*111); 
set @rlat1 = @lat-(@dist/111); 
set @rlat2 = @lat+(@dist/111);

select harvesine(y(mappoint), x(mappoint), @lat, @lon ) as distance, 
                                        x(mappoint) as latitude, y(mappoint) as longitude, mappoint,
                                        name 
                                        from location where st_within(mappoint, envelope(linestring(point(@rlon1, @rlat1), point(@rlon2, @rlat2))))
                                        order by st_distance(point(@lon, @lat), mappoint) limit 4