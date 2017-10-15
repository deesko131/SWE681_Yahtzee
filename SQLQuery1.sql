CREATE PROC UesrAdd
@PlayerName varchar(50),
@Email varchar(50),
@Password varchar(5)
As
 INSERT INTO Registeration(PlayerName,Email,Password)
Values(@PlayerName,@Email,@Password)