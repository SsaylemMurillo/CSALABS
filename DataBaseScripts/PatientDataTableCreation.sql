
DROP DATABASE IF EXISTS csalabs
CREATE DATABASE csalabs

CREATE TABLE patient (
    [id] INT NOT NULL,
    [id_type] NVARCHAR (50) NOT NULL,
	[first_name] NVARCHAR (50) NOT NULL,
	[second_name] NVARCHAR (50) NOT NULL,
	[last_name] NVARCHAR (50) NOT NULL,
	[second_lastname] NVARCHAR (50) NOT NULL,
	[born_date] DATETIME NOT NULL,
	[expedition_date] DATETIME NOT NULL,
	[expedition_place] NVARCHAR (50) NOT NULL,
	[phone] INT NOT NULL,
	[address] NVARCHAR(50) NOT NULL,
	[nacionality] NVARCHAR(50) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE exam(
	[id_exam] INT IDENTITY(1,1) NOT NULL,
	[value_measures] NVARCHAR(50) NOT NULL,
	[name] NVARCHAR(10) NOT NULL,
	[description] NVARCHAR(50) NOT NULL,
	[results] NVARCHAR(50) NULL,
	PRIMARY KEY CLUSTERED ([id_exam])
);

CREATE TABLE laboratory(
	[id_lab] INT IDENTITY(1,1) NOT NULL,
	[id_patient] INT NOT NULL,
	[result] NVARCHAR(50) NULL,
	[date_lab] DATETIME DEFAULT getDate()NOT NULL,
	[place] NVARCHAR(50) NOT NULL,
	PRIMARY KEY CLUSTERED ([id_lab]),
	FOREIGN KEY (id_patient) REFERENCES patient(id),
);

CREATE TABLE [dbo].[laboratorio_examenes](
	[id_lab] INT  NOT NULL,
	[id_exam] INT  NOT NULL,
	PRIMARY KEY (id_lab, id_exam),
	FOREIGN KEY (id_lab) REFERENCES laboratory (id_lab),
	FOREIGN KEY (id_exam) REFERENCES exam (id_exam),
);