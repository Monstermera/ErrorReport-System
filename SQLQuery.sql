CREATE TABLE Addresses (
	AddressId int not null identity primary key,
	StreetName nvarchar(30) not null,
	ZipCode char(6) not null,
	City nvarchar(30) not null
)


CREATE TABLE Customers (
	CustomId int not null identity primary key,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
	Email nvarchar(50) not null,
	PhoneNumber char(13) not null,

	AddressId int not null references Addresses(AddressId),
)

CREATE TABLE Technicians (
	TechnicianId int not null identity primary key,
	TechFirstName nvarchar(20) not null,
	TechLastName nvarchar(20) not null,
	TechPhoneNumber char(13) not null,
)

CREATE TABLE StatusReports (
	StatusId int not null identity primary key,
	StatusName nvarchar(100) not null,
	DateUpdated DATETIME not null,
)

CREATE TABLE Comments (
	CommentId int not null identity primary key,
    CommentText nvarchar(100) not null,
    CommentCreated DATETIME not null,

	CustomId int not null references Customers(CustomId),
)

CREATE TABLE ErrorReports (
	ErrorId int not null identity primary key,
    Description nvarchar(100) not null,
    DateReported DATETIME not null,

	CustomId int not null references Customers(CustomId),
	TechnicianId int null references Technicians(TechnicianId),
	CommentId int null references Comments(CommentId),
	StatusId int not null references StatusReports(StatusId)
)


	