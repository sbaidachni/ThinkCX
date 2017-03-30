CREATE TABLE UserTable
(
  user_id varchar(40) NOT NULL,
  creation_date datetime2 NOT NULL
)
WITH 
( 
	CLUSTERED INDEX (user_id) 
)

  




