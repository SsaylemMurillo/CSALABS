CREATE TABLE patient (
    [id] int NOT NULL,
    [id_type] NVARCHAR (50) NOT NULL,
	[first_name] NVARCHAR (50) NOT NULL,
	[second_name] NVARCHAR (50) NOT NULL,
	[last_name] NVARCHAR (50) NOT NULL,
	[second_lastname] NVARCHAR (50) NOT NULL,
	[born_date] DATETIME NOT NULL,
	[expedition_date] DATETIME NOT NULL,
	[expedition_place] NVARCHAR (50) NOT NULL,
	[phone] int NOT NULL,
	[address] NVARCHAR (50) NOT NULL,
    PRIMARY KEY (id)
);