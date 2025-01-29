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
  [Gender] NVARCHAR(50) not null     -- מגדר

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


  CREATE TABLE StatusTable(               --טבלת סטטוס 
  StatusId INT PRIMARY KEY Identity, --קוד סטטוס 
  StatusDescription NVARCHAR(250),        --(תיאור לכל קוד סטטוס(אושר, ממתין וכו
  );
  
  CREATE TABLE WaitingLP(  --טבלת רשימת המתנה, בקשות שהורים שלחו לבייביסיטרים
  Id INT PRIMARY KEY Identity,                                --מספר בקשה        
  ParentId int FOREIGN KEY(ParentId) REFERENCES Parents(ParentId),         --שם ההורה ששלח את הבקשה
  BabysiterId int FOREIGN KEY(BabysiterId) REFERENCES Babysiters(BabysiterId), --שם הבייביסיטר אליו נשלחה הבקשה
  StatusCode int FOREIGN KEY(StatusCode)REFERENCES StatusTable(StatusId) ,     --(סטטוס הבקשה (ממתין/אושר/נדחה 
  );

  CREATE TABLE WaitingLB(  --טבלת רשימת המתנה, בקשות שבייביסיטרים שלחו להורים
  Id INT PRIMARY KEY Identity,                                                 --מספר בקשה        
  ParentId int FOREIGN KEY(ParentId) REFERENCES Parents(ParentId),            --שם ההורה אליו נשלחה הבקשה
  BabysiterId int FOREIGN KEY(BabysiterId) REFERENCES Babysiters(BabysiterId),--שם הבייביסיטר ששלח את הבקשה
  StatusCode int FOREIGN KEY(StatusCode)REFERENCES StatusTable(StatusId) ,    --(סטטוס הבקשה (ממתין/אושר/נדחה 
  );

   --Create a login for the admin user
CREATE LOGIN [Login] WITH PASSWORD = 'shira123';
Go

-- Create a user in the BabySisMatchDB database for the login
CREATE USER [AdminUser] FOR LOGIN [Login];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go

insert into StatusTable (StatusDescription) values ('Approve')
insert into StatusTable (StatusDescription) values ('Decline')
insert into StatusTable (StatusDescription) values ('Waiting')
Go

insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender) VALUES ('b@b.com','shira','levy', 'Hod Hasharon', '123','B Name', 'Female') 
insert into Users (Email,FirstName,LastName, [Address], [Password], UserName, Gender) VALUES ('p@p.com','ziv','porat', 'Raanana', '123','P Name', 'Male')

insert into Babysiters (BabysiterId, BirthDate, License, ExperienceY,Payment) values (1,'10-JAN-2009',0, 1,50)
insert into Parents (ParentId, KidsN, Pets) values (2, 4, 1)
--insert into Users values('shira','123','shira@gmail.com', 'hodash','babysiter') 

select * from StatusTable
Select * From Babysiters
Select * From Users
Select * From Parents

--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BSDbContext -DataAnnotations -force
*/