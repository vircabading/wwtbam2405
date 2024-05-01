-- CREATE SCHEMA p2

CREATE TABLE p2.Users(
    UserId INT PRIMARY KEY IDENTITY,
    Username nvarchar(20)
);

CREATE TABLE p2.Games(
        GameId INT PRIMARY Key Identity,
        UserId int  FOREIGN KEY REFERENCES p2.USERS(UserId),
        Score INT,
        PlayedAt DATETIME
    )

DROP TABLE p2.USERS
DROP TABLE p2.Games