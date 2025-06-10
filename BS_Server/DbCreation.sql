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



insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('b@b.com','ziv','porat', 'Hod Hasharon', '123','B Name', 'Female','0505805203') 
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('p@p.com','marom','hai', 'Hod Hasharon', '123','P Name', 'Male','0506519451')
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('s@.com','shiri','kehat', 'Hod Hasharon', '123','S Name', 'Female','0541234567') 
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('parent@.com','amit','geva', 'Hod Hasharon', '1234','a Name', 'Male','0501234567')
insert into Users (Email,FirstName,LastName, [Address],IsAdmin, [Password], UserName, Gender, Phone) VALUES ('shirale200@gmail.com','shira','levy', 'Hod Hasharon','1', 's123','S Name', 'Male','0501234567')
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('noa20032007@gmail.com','noa','cohen', 'Hod Hasharon', 'n123','N Name', 'Female','0505805209') 
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender, Phone) VALUES ('geva.ori1@gmail.com','ori','geva', 'Hod Hasharon', 'o123','O Name', 'Male','0505805208') 

select * from users

insert into Babysiters (BabysiterId, BirthDate, License, ExperienceY,Payment) values (1,'10-JAN-2009',0, 1,50)
insert into Parents (ParentId, KidsN, Pets) values (2, 4, 1)
insert into Babysiters (BabysiterId, BirthDate, License, ExperienceY,Payment) values (3,'15-FEB-2007',1, 1,75)
insert into Parents (ParentId, KidsN, Pets) values (4, 2, 0)
insert into Parents (ParentId, KidsN, Pets) values (14, 1, 0)
insert into Babysiters (BabysiterId, BirthDate, License, ExperienceY,Payment) values (6,'16-JAN-2008',1, 6,55)
insert into Parents (ParentId, KidsN, Pets) values (7, 7, 1)


Select * From Babysiters
Select * From Users
Select * From Parents
Select * From Rating
Select * From Tips

insert into Rating (UserId, RatingValue) values(1, 4)
insert into Rating (UserId, RatingValue) values(1, 5)
insert into Rating (UserId, RatingValue) values(2, 5)
insert into Rating (UserId, RatingValue) values(3, 4)
insert into Rating (UserId, RatingValue) values(4, 4)
insert into Rating (UserId, RatingValue) values(5, 3)
insert into Rating (UserId, RatingValue) values(5, 4)
insert into Rating (UserId, RatingValue) values(6, 3)
insert into Rating (UserId, RatingValue) values(7, 5)

insert into Recommendation (UserId,RecommendationText) values(1, 'She was great!')
insert into Recommendation (UserId,RecommendationText) values(2, 'reliable and patient')
insert into Recommendation (UserId,RecommendationText) values(3, 'Patient and nice with the children')
insert into Recommendation (UserId,RecommendationText) values(4, 'very responsible')
insert into Recommendation (UserId,RecommendationText) values(5, 'The best in town')
insert into Recommendation (UserId,RecommendationText) values(6, 'She was great!')
insert into Recommendation (UserId,RecommendationText) values(7, 'reliable and patient')

insert into Tips(UserId, TipText, StatusId) values (1, 'bake with the kids', 1)
insert into Tips(UserId, TipText, StatusId) values (2, 'play hide and seek', 1)
insert into Tips(UserId, TipText, StatusId) values (2, 'Tell them a story and let them invent a new ending', 1)
insert into Tips(UserId, TipText, StatusId) values (1, 'Have a fancy movie night', 3)
insert into Tips(UserId, TipText, StatusId) values (1, 'Go to the park and play soccer', 3)

select * from Rating
select * from Recommendation
select * from Tips


--DELETE FROM Tips WHERE TipId = 2;
--DELETE FROM Tips WHERE TipId = 1;

--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BSDbContext -DataAnnotations -force
*/