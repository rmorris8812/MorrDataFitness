DROP TABLE Measurement;
DROP TABLE MealGoal;
DROP TABLE DailyGoal;
DROP TABLE Goal;
DROP TABLE UserFood;
DROP TABLE Food;
DROP TABLE FitnessUser;

CREATE SEQUENCE hibernate_sequence START 3000;

CREATE TABLE Food (
       FoodId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
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
       UserId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
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
       MeasurementId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
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
       GoalId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
     , CurrentWeight FLOAT
     , DesiredWeight FLOAT
     , CompleteDesiredDate DATE
     , CompleteCalculatedDate DATE
     , CompleteActualDate DATE
     , UserId BIGINT NOT NULL
     , PRIMARY KEY (GoalId)
);

CREATE TABLE DailyGoal (
       GoalId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
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
       GoalId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
     , Calories INT
     , Carbs INT
     , Fat INT
     , Salt INT
     , Protein INT
     , Sugar INT
     , PRIMARY KEY (GoalId)
);

CREATE TABLE UserFood (
       UserFoodId BIGINT NOT NULL DEFAULT nextval('hibernate_sequence')
     , Serving FLOAT
     , Meal INT
     , ConsumeDate DATE
     , UserId BIGINT NOT NULL
     , FoodId BIGINT NOT NULL
     , PRIMARY KEY (UserFoodId)
);

INSERT INTO FitnessUser (UserId, FirstName, LastName, Email, Password, TenantId, ExternalAuth, UserRole) 
VALUES (1, 'Foo', 'Bar', 'foobar@acme.com', 'cGFsbTg4ITI=', 'acme.com', '0', 'user');

INSERT INTO FitnessUser (UserId, FirstName, LastName, Email, Password, TenantId, ExternalAuth, UserRole) 
VALUES (2, 'Roy', 'Morris', 'roy.morris@morrdata.com', 'cGFsbTg4ITI=', 'morrdata.com', '0', 'admin');

INSERT INTO Food (FoodId, Name, Calories, Serving, Unit, Carbs, Fat, Salt, Protein, Sugar)
VALUES (1, 'Banana', 151, 6, 0, 19, 0, 1, 1, 1);
