﻿USE master
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


CREATE TABLE Parents(    --טבלת הורים
ParentId INT PRIMARY KEY Identity,      -- מפתח ראשי
UserName NVARCHAR(100),                 --שם משתמש
Pass NVARCHAR(50),                      -- סיסמה
KidsN INT,                              --מספר ילדים
Pets Bit,                               --בעלי חיים
Email NVARCHAR(100),                    --אימייל
City NVARCHAR(100),                 --עיר מגורים
);


CREATE TABLE Babysiters(  --טבלת בייביסיטרים
BabysiterId INT PRIMARY KEY Identity,      -- מפתח ראשי
UserName NVARCHAR(100),                    --שם משתמש
Pass NVARCHAR(50),                         -- סיסמה
Age INT,                                   --גיל
ExperienceY INT,                           --שנות ניסיון
License  Bit,                             --רשיון
Email NVARCHAR(100),                       --אימייל
City NVARCHAR(100),                    --עיר מגורים
);



CREATE TABLE Supervisors (  --טבלת מפקחים עירוניים
SupervisorId INT PRIMARY KEY Identity,      -- מפתח ראשי
UserName NVARCHAR(100),                    --שם משתמש
Pass NVARCHAR(50),                         -- סיסמה
City NVARCHAR(100),                    --עיר מגורים
);
  
    CREATE TABLE StatusTable(               --טבלת סטטוס 
  StatusId INT PRIMARY KEY Identity, --קוד סטטוס 
  StatusDescription NVARCHAR(250),        --(תיאור לכל קוד סטטוס(אושר, ממתין וכו
  );
  
  CREATE TABLE WaitingLP(  --טבלת רשימת המתנה, בקשות שהורים שלחו לבייביסיטרים
  Id INT PRIMARY KEY Identity,                                --מספר בקשה        
  ParentId int FOREIGN KEY(ParentId) REFERENCES Parents(ParentId),         --שם ההורה ששלח את הבקשה
  BabysiterId int FOREIGN KEY(BabysiterId) REFERENCES Babysiters(BabysiterId), --שם הבייביסיטר אליו נשלחה הבקשה
  StatusCode int FOREIGN KEY(StatusCode)REFERENCES StatusTable(StatusId) ,                                                 --(סטטוס הבקשה (ממתין/אושר/נדחה 
  );

  CREATE TABLE WaitingLB(  --טבלת רשימת המתנה, בקשות שבייביסיטרים שלחו להורים
  Id INT PRIMARY KEY Identity,                                                 --מספר בקשה        
  ParentId int FOREIGN KEY(ParentId) REFERENCES Parents(ParentId),            --שם ההורה אליו נשלחה הבקשה
  BabysiterId int FOREIGN KEY(BabysiterId) REFERENCES Babysiters(BabysiterId),--שם הבייביסיטר ששלח את הבקשה
  StatusCode int FOREIGN KEY(StatusCode)REFERENCES StatusTable(StatusId) ,                                                                 --(סטטוס הבקשה (ממתין/אושר/נדחה 
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



insert into Parents values('shira',123, 2, 0, 'shirale@','hodash')
insert into Parents values('rotem',555, 5, 0, 'rotem@#','hodash')
insert into Babysiters values('shiri',1245, 17, 3, 1,'shii12','hodash')
insert into Babysiters values('tami',12456, 17, 2, 1,'tami12','hodash')
insert into StatusTable values('approve')
insert into StatusTable values('decline')
insert into StatusTable values('wating')
insert into Supervisors values('shay', 123456, 'hoash')
insert into WaitingLP values(1,1,1)
insert into WaitingLB values(2,2,3)

Go

select * from WaitingLP
select * from WaitingLB
select * from Supervisors
select * from StatusTable
Select * From Babysiters
select * from parents



--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BSDbContext -DataAnnotations -force
*/