USE master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'BS_DB')
BEGIN
    DROP DATABASE BS_DB;
END
Go
Create Database BS_DB
Go
Use BS_DB
Go


  CREATE TABLE Users  
  (
  id INT PRIMARY KEY identity,  --מפתח ראשי(קוד המשתמש                                              
  FirstName  NVARCHAR(100) not null, --שם פרטי
  LastName NVARCHAR(100) not null, --שם משפחה
  UserName NVARCHAR(100) not null,      --שם משתמש   
  [Password] NVARCHAR(50) not null,    --סיסמה                                                        
  Email NVARCHAR(100) unique not null,   --אימייל
  [Address] NVARCHAR(100) not null,    --כתובת מגורים
  [IsAdmin] bit default(0) not null, --האם מנהל
  [Gender] NVARCHAR(50) not null,     -- מגדר
  [Phone] NVARCHAR(50) not null       --מספר טלפון

  );


CREATE TABLE Parents(    --טבלת הורים
ParentId INT PRIMARY KEY,      -- מפתח ראשי
KidsN INT not null,                              --מספר ילדים
Pets Bit not null,                               --בעלי חיים
CONSTRAINT FK_Parents FOREIGN KEY (ParentId) REFERENCES Users(id)          
);


CREATE TABLE Babysiters(  --טבלת בייביסיטרים
BabysiterId INT PRIMARY KEY ,      -- מפתח ראשי
BirthDate date not null,                                   --גיל
ExperienceY INT not null,                           --שנות ניסיון
License  Bit not null,                             --רשיון
Payment INT not null,             --תשלום שהבייביסיטר לוקחת 
CONSTRAINT FK_Babysiters FOREIGN KEY (BabysiterId) REFERENCES Users(id)     
);

  Create Table Rating (
  RatingId int primary key identity,
  UserId int foreign key(UserId) references Users(id),
  RatingValue int default(0));

  Create Table Recommendation (
  RecommendationId int primary key identity,
  UserId int foreign key(UserId) references Users(id),
  RecommendationText nvarchar(500));
  Go

  Create Table Tips (
  TipId int primary key identity,
  UserId int foreign key(UserId) references Users(id),
  TipText nvarchar(500),
  StatusId int --Approved(1), Declined(2), Pending(3)
  );
  Go

   --Create a login for the admin user
CREATE LOGIN [Login] WITH PASSWORD = 'shira123';
Go

-- Create a user in the BabySisMatchDB database for the login
CREATE USER [AdminUser] FOR LOGIN [Login];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go



insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('b@b.com','shira','levy', 'Hod Hasharon', '123','B Name', 'Female','0505805203') 
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('p@p.com','ziv','porat', 'Raanana', '123','P Name', 'Male','0506519451')
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('s@.com','shiri','kehat', 'USA', '123','S Name', 'Female','0541234567') 
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('shirale200@gmail.com','ori','geva', 'france', '123','O Name', 'Male','0501234567')
insert into Users (Email,FirstName,LastName, [Address], IsAdmin, [Password], UserName, Gender, Phone) VALUES 
('admin@gamil.com','admin','adminlastname', 'france','1', '12345','admin1', 'Female','0505805205')


insert into Babysiters (BabysiterId, BirthDate, License, ExperienceY,Payment) values (1,'10-JAN-2009',0, 1,50)
insert into Parents (ParentId, KidsN, Pets) values (2, 4, 1)
insert into Babysiters (BabysiterId, BirthDate, License, ExperienceY,Payment) values (3,'15-FEB-2007',1, 1,75)
insert into Parents (ParentId, KidsN, Pets) values (4, 2, 0)



Select * From Babysiters
Select * From Users
Select * From Parents
Select * From Rating

insert into Rating values(1, 4)
insert into Rating values(1, 2)
insert into Rating values(2, 2)
insert into Rating values(3, 2)
insert into Rating values(4, 2)
insert into Rating values(2, 2)

insert into Recommendation values(1, 'She was great!')
insert into Recommendation values(2, 'She was great2!')
insert into Recommendation values(3, 'She was great3!')
insert into Recommendation values(4, 'She was great4!')
insert into Recommendation values(1, 'She was great5!')

select * from Rating
select * from Recommendation
--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BSDbContext -DataAnnotations -force
*/