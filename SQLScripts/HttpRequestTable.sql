CREATE TABLE HttpRequestTable
(
	device_id varchar(40) NOT NULL,
	user_agent varchar(100),
	ip_address varchar(40),
	url_params varchar(400),
	header_string varchar(400),
	ip_carrier_id smallint,
	device_carrier_id smallint,
	time_stamp datetime2
)
WITH
(
	CLUSTERED INDEX (device_id)
)