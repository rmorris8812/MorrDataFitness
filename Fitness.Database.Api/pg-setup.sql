CREATE SEQUENCE hibernate_sequence START 3000;

CREATE TABLE Food (
       FoodId BIGINT NOT NULL
     , Name VARCHAR(256)
     , Calories INT
     , PRIMARY KEY (FoodId)
);

CREATE TABLE FitnessUser (
       UserId BIGINT NOT NULL
     , FirstName VARCHAR(256)
     , LastName VARCHAR(256)
     , Email VARCHAR(256)
     , Password VARCHAR(256)
     , Token VARCHAR(1024)
     , TenantId VARCHAR(64)
     , ExternalAuth BIT
     , PRIMARY KEY (UserId)
);

CREATE TABLE UserRole (
      UserRoalId BIGINT NOT NULL
    , UserRole VARCHAR(64)
    , UserId BIGINT NOT NULL
);

CREATE TABLE UserFood (
       UserFoodId BIGINT NOT NULL
     , UserId BIGINT NOT NULL
     , FoodId BIGINT NOT NULL
     , PRIMARY KEY (UserFoodId)
);

INSERT INTO FitnessUser (UserId, FirstName, LastName, Email, Password) VALUES (1, 'Roy', 'Morris', 'rmorris8812@gmail.com', 'palm88!2');
UPDATE FitnessUser SET Password='cGFsbTg4ITI=';

SELECT * FROM FitnessUser;
