CREATE TABLE DeviceTable(
	device_id varchar(40) NOT NULL,
	user_id varchar(40) NOT NULL,
	creation_date datetime2 NOT NULL,
	last_use_date datetime2 NOT NULL
)
WITH(
	CLUSTERED INDEX(device_id) 
);