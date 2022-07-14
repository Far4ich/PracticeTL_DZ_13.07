use Medicine

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Hospital')
BEGIN
	create table Hospital (
	Id int identity(1,1) constraint PK_Hospital primary key, 
	Name nvarchar(100),
	Address nvarchar(245),
	TelephoneNumber nvarchar(30)
)
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Doctor')
BEGIN
create table Doctor(
	Id int identity(1,1) constraint PK_Doctor primary key, 
	Name nvarchar(100),
	TelephoneNumber nvarchar(30),
	HospitalId int constraint FK_Doctor_Hospital references Hospital(Id) on delete cascade
)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Patient')
BEGIN
create table Patient(
	Id int identity(1,1) constraint PK_Patient primary key,
	Name nvarchar(100),
	HealthCardNumber int,
	DoctorId int constraint FK_Patient_Doctor references Doctor(Id) on delete cascade
)
END

