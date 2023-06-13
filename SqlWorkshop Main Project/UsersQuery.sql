CREATE USER Director WITHOUT LOGIN; 
ALTER ROLE [db_owner] ADD MEMBER Director; 
--EXEC AS Director;

CREATE USER [Lead Manager] WITHOUT LOGIN; 
ALTER ROLE [db_datareader] ADD MEMBER [Lead Manager]; 
DENY SELECT ON  Users(Salary) TO [Lead Manager];

CREATE USER [Middle Manager] WITHOUT LOGIN; 
ALTER ROLE [db_datareader] ADD MEMBER [Middle Manager]; 
DENY SELECT ON Logins TO [Middle Manager];

CREATE USER [Supervisor] WITHOUT LOGIN; 
ALTER ROLE [db_datareader] ADD MEMBER [Supervisor]; 
DENY SELECT ON Logins TO [Supervisor];
DENY SELECT ON  Users(Salary) TO [Supervisor];

CREATE USER [Users] WITHOUT LOGIN; 
ALTER ROLE [db_datareader] ADD MEMBER [Users]; 
DENY SELECT ON Logins TO [Users];
DENY SELECT ON Users(Salary) TO [Users];



EXEC AS USER = 'Director';
SELECT * FROM Logins;
SELECT * FROM Users;
SELECT * FROM Emails;
SELECT * FROM Locations;
SELECT * FROM Phones;
SELECT * FROM Pictures;

REVERT

GRANT UNMASK TO [Lead Manager]
EXEC AS USER = 'Lead Manager';

SELECT [Id], [Firstname], [Lastname], [BirthDate], [Age], [Nationality], [Gender],[CreditCard] FROM Users;
SELECT * FROM Logins;
SELECT * FROM Emails;
SELECT * FROM Locations;
SELECT * FROM Phones;
SELECT * FROM Pictures;

REVERT


GRANT UNMASK ON Phones TO [Middle Manager]
GRANT UNMASK ON Emails TO [Middle Manager]
EXEC AS USER = 'Middle Manager';
--SELECT * FROM Logins;
SELECT * FROM Users;
SELECT * FROM Emails;
SELECT * FROM Locations;
SELECT * FROM Phones;
SELECT * FROM Pictures;

REVERT

GRANT UNMASK ON Phones(Cell) TO [Supervisor]
EXEC AS USER = 'Supervisor';
--SELECT * FROM Logins;
SELECT [Id], [Firstname], [Lastname], [BirthDate], [Age], [Nationality], [Gender], [CreditCard]FROM Users;
SELECT * FROM Emails;
SELECT * FROM Locations;
SELECT * FROM Phones;
SELECT * FROM Pictures;

REVERT

EXEC AS USER = 'Users';
--SELECT * FROM Logins;
SELECT [Id], [Firstname], [Lastname], [BirthDate], [Age], [Nationality], [Gender], [CreditCard]FROM Users;
SELECT * FROM Emails;
SELECT * FROM Locations;
SELECT * FROM Phones;
SELECT * FROM Pictures;

REVERT

REVOKE UNMASK FROM Director
REVOKE UNMASK FROM [Lead Manager]
REVOKE UNMASK FROM [Middle Manager]
REVOKE UNMASK FROM [Supervisor]
REVOKE UNMASK FROM [Users]
