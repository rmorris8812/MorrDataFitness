DROP TABLE UserFood;
DROP TABLE FitnessUser;
DROP TABLE Food;

CREATE SEQUENCE hibernate_sequence START 3000;

CREATE TABLE Food (
       FoodId BIGINT NOT NULL
     , Name VARCHAR(256)
     , Calories INT
     , Serving FLOAT
     , Unit INT
     , Carbs INT
     , Fat INT
     , Salt INT
     , Protein INT
     , Sugar INT
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
     , ExternalAuth BOOLEAN
     , UserRole VARCHAR(32)
     , PRIMARY KEY (UserId)
);

CREATE TABLE Measurement (
       MeasurementId BIGINT NOT NULL
     , CurrentWeight FLOAT
     , Unit INT
     , CurrentNeck INT
     , CurrentChest INT
     , CurrentWaist INT
     , CurrentArm INT
     , CurrentHip INT
     , CurrentLeg INT
     , UserId BIGINT NOT NULL
     , PRIMARY KEY (MeasurementId)
);

CREATE TABLE Goal (
       GoalId BIGINT NOT NULL
     , CurrentWeight FLOAT
     , DesiredWeight FLOAT
     , CompleteDesiredDate DATE
     , CompleteCalculatedDate DATE
     , CompleteActualDate DATE
     , UserId BIGINT NOT NULL
     , PRIMARY KEY (GoalId)
);

CREATE TABLE DailyGoal (
       GoalId BIGINT NOT NULL
     , Calories INT
     , Carbs INT
     , Fat INT
     , Salt INT
     , Protein INT
     , Sugar INT
     , UserId BIGINT NOT NULL
     , PRIMARY KEY (GoalId)
);

CREATE TABLE MealGoal (
       GoalId BIGINT NOT NULL
     , Calories INT
     , Carbs INT
     , Fat INT
     , Salt INT
     , Protein INT
     , Sugar INT
     , PRIMARY KEY (GoalId)
);

CREATE TABLE UserFood (
       UserFoodId BIGINT NOT NULL
     , Serving FLOAT
     , Meal INT
     , ConsumeDate DATE
     , UserId BIGINT NOT NULL
     , FoodId BIGINT NOT NULL
     , PRIMARY KEY (UserFoodId)
);

INSERT INTO FitnessUser (UserId, FirstName, LastName, Email, Password, TenantId, ExternalAuth, UserRole) VALUES (1, 'Roy', 'Morris', 'rmorris8812@gmail.com', 'palm88!2', 'morrdata.com', '0', 'admin');
UPDATE FitnessUser SET Password='cGFsbTg4ITI=';

SELECT * FROM FitnessUser;

INSERT INTO Food (FoodId, Name, Calories, Serving, Unit, Carbs, Fat, Salt)
VALUES (1, 'Banana', 151, 6, 0, 19, 0, 1);

