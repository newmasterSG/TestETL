IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CSV')
BEGIN
    CREATE TABLE CSV (
        Id INT CONSTRAINT PK_CSVId PRIMARY KEY IDENTITY NOT NULL,
		tpep_pickup_datetime DATETIME NOT NULL,
		tpep_dropoff_datetime DATETIME NOT NULL,
		passenger_count INT NOT NULL,
		trip_distance NUMERIC(5, 3),
		store_and_fwd_flag NVARCHAR(4),
		PULocationID INT NOT NULL,
		DOLocationID INT NOT NULL,
		fare_amount DECIMAL(5, 2),
		tip_amount DECIMAL (5, 2)
    );

	CREATE INDEX IX_PULocationID ON CSV (PULocationID) INCLUDE (tip_amount);
    CREATE INDEX IX_trip_distance ON CSV (trip_distance) INCLUDE (tpep_dropoff_datetime);
    CREATE INDEX IX_tpep_dropoff_datetime ON CSV (tpep_dropoff_datetime) INCLUDE (trip_distance);
END