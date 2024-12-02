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

--//ggg
  CREATE TABLE Users  
  (
  id INT PRIMARY KEY identity,  --מפתח ראשי(קוד המשתמש                                              
  UserName NVARCHAR(100),      --שם משתמש   
  Password NVARCHAR(50),    --סיסמה                                                        
  Email NVARCHAR(100),   --אימייל
  City NVARCHAR(100),    --עיר מגורים
  UserType NVARCHAR(100) -- סוג משתמש 
  );


CREATE TABLE Parents(    --טבלת הורים
ParentId INT PRIMARY KEY,      -- מפתח ראשי
KidsN INT,                              --מספר ילדים
Pets Bit,                               --בעלי חיים
CONSTRAINT FK_Parents FOREIGN KEY (ParentId) REFERENCES Users(id)          
);


CREATE TABLE Babysiters(  --טבלת בייביסיטרים
BabysiterId INT PRIMARY KEY ,      -- מפתח ראשי
Age INT,                                   --גיל
ExperienceY INT,                           --שנות ניסיון
License  Bit,                             --רשיון
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

insert into Babysiters values (15,5,1)
insert into Parents values (3,0)
insert into StatusTable values ('approve')
insert into StatusTable values ('decline')
insert into StatusTable values ('waiting')
insert into WaitingLb values ('approve')
insert into Users values('shira','123','shira@gmail.com', 'hodash','babysiter') 

select * from StatusTable
Select * From Babysiters
Select * From Users
Select * From Parents

--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BSDbContext -DataAnnotations -force
*/