CREATE TABLE GeoDataTable
(
	device_id varchar(40) NOT NULL,
	device_type varchar(4), 
	publisher_id varchar(8),
	user_agent varchar(100),
	ip_address varchar(40),  -- could be ipv6
	latitude real,
	longitude real,
	accuracy int,
	ip_carrier_id smallint,
	device_carrier_id smallint,
	time_stamp datetime2
)
WITH 
( 
	CLUSTERED INDEX (device_id) 
);
