
-- init_postgres.sql
-- Create database and schema for ConcreteCrackManager (run as postgres superuser)

CREATE DATABASE concrete_inspections;

\connect concrete_inspections

CREATE SCHEMA IF NOT EXISTS app;

CREATE TABLE IF NOT EXISTS app.AppRoles (
  Id SERIAL PRIMARY KEY,
  Name TEXT NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS app.AppUsers (
  Id SERIAL PRIMARY KEY,
  Username TEXT NOT NULL UNIQUE,
  DisplayName TEXT,
  RoleId INT REFERENCES app.AppRoles(Id)
);

CREATE TABLE IF NOT EXISTS app.Inspections (
  Id SERIAL PRIMARY KEY,
  Name TEXT NOT NULL,
  Location TEXT,
  InspectedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),
  InspectorId INT REFERENCES app.AppUsers(Id)
);

CREATE TABLE IF NOT EXISTS app.Images (
  Id SERIAL PRIMARY KEY,
  InspectionId INT REFERENCES app.Inspections(Id),
  FilePath TEXT NOT NULL,
  UploadedAt TIMESTAMP WITH TIME ZONE DEFAULT now()
);

CREATE TABLE IF NOT EXISTS app.Defects (
  Id SERIAL PRIMARY KEY,
  ImageId INT REFERENCES app.Images(Id),
  X INT,
  Y INT,
  Width INT,
  Height INT,
  Severity INT,
  Description TEXT
);

-- Example roles (change passwords)
CREATE ROLE concrete_admin WITH LOGIN PASSWORD 'AdminPass123';
CREATE ROLE inspector_user WITH LOGIN PASSWORD 'InspectorPass123';

GRANT ALL PRIVILEGES ON DATABASE concrete_inspections TO concrete_admin;
GRANT CONNECT ON DATABASE concrete_inspections TO inspector_user;

GRANT USAGE ON SCHEMA app TO inspector_user;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA app TO inspector_user;
GRANT SELECT, UPDATE ON ALL SEQUENCES IN SCHEMA app TO inspector_user;
