![Microsoft Cloud Workshops](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Data Platform upgrade and migration
</div>

<div class="MCWHeader2">
Hands-on lab unguided
</div>

<div class="MCWHeader3">
June 2018
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

## Contents

- [Data Platform upgrade and migration hands-on lab unguided](#data-platform-upgrade-and-migration-hands-on-lab-unguided)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Overview](#overview)
  - [Solution architecture](#solution-architecture)
  - [Before the hands-on lab](#before-the-hands-on-lab)
  - [Requirements](#requirements)
  - [Exercise 1: Deploy SQL Server instances](#exercise-1-deploy-sql-server-instances)
    - [Task 1: Install SQL Server 2017](#task-1-install-sql-server-2017)
      - [Tasks to Complete](#tasks-to-complete)
      - [Exit Criteria](#exit-criteria)
    - [Task 2: Install SQL Server 2008 R2](#task-2-install-sql-server-2008-r2)
      - [Tasks to Complete](#tasks-to-complete-1)
      - [Exit Criteria](#exit-criteria-1)
    - [Task 3: Install AdventureWorks sample database](#task-3-install-adventureworks-sample-database)
      - [Tasks to Complete](#tasks-to-complete-2)
      - [Exit Criteria](#exit-criteria-2)
    - [Task 4: Update SQL Server settings using Configuration Manager](#task-4-update-sql-server-settings-using-configuration-manager)
      - [Tasks to Complete](#tasks-to-complete-3)
      - [Exit Criteria](#exit-criteria-3)
    - [Task 5: Add inbound port rule](#task-5-add-inbound-port-rule)
      - [Tasks to Complete](#tasks-to-complete-4)
      - [Exit Criteria](#exit-criteria-4)
    - [Task 6: Copy the SqlServerDw VM IP address](#task-6-copy-the-sqlserverdw-vm-ip-address)
      - [Tasks to Complete](#tasks-to-complete-5)
      - [Exit Criteria](#exit-criteria-5)
  - [Exercise 2: Migrate SQL Server to Azure SQL Database using DMS](#exercise-2-migrate-sql-server-to-azure-sql-database-using-dms)
    - [Task 1: Register the Microsoft DataMigration resource provider](#task-1-register-the-microsoft-datamigration-resource-provider)
      - [Tasks to Complete](#tasks-to-complete-6)
      - [Exit Criteria](#exit-criteria-6)
    - [Task 2: Create Azure Database Migration Service](#task-2-create-azure-database-migration-service)
      - [Tasks to Complete](#tasks-to-complete-7)
      - [Exit Criteria](#exit-criteria-7)
    - [Task 3: Assess the on-premises database](#task-3-assess-the-on-premises-database)
      - [Tasks to Complete](#tasks-to-complete-8)
      - [Exit Criteria](#exit-criteria-8)
    - [Task 4: Migrate the database schema](#task-4-migrate-the-database-schema)
      - [Tasks to Complete](#tasks-to-complete-9)
      - [Exit Criteria](#exit-criteria-9)
    - [Task 5: Create a migration project](#task-5-create-a-migration-project)
      - [Tasks to Complete](#tasks-to-complete-10)
      - [Exit Criteria](#exit-criteria-10)
    - [Task 6: Run the migration](#task-6-run-the-migration)
      - [Tasks to Complete](#tasks-to-complete-11)
      - [Exit Criteria](#exit-criteria-11)
    - [Task 7: Verify data migration](#task-7-verify-data-migration)
      - [Tasks to Complete](#tasks-to-complete-12)
      - [Exit Criteria](#exit-criteria-12)
  - [Exercise 3: Post Upgrade Enhancement](#exercise-3-post-upgrade-enhancement)
    - [Task 1: Table Compression](#task-1-table-compression)
      - [Tasks to Complete](#tasks-to-complete-13)
      - [Exit Criteria](#exit-criteria-13)
    - [Task 2: Clustered ColumnStore Index](#task-2-clustered-columnstore-index)
      - [Tasks to Complete](#tasks-to-complete-14)
      - [Exit Criteria](#exit-criteria-14)
  - [Exercise 4: Setup Oracle 11g Express Edition](#exercise-4-setup-oracle-11g-express-edition)
    - [Task 1: Install Oracle XE](#task-1-install-oracle-xe)
      - [Tasks to Complete](#tasks-to-complete-15)
      - [Exit Criteria](#exit-criteria-15)
    - [Task 2: Install Oracle Data Access Components](#task-2-install-oracle-data-access-components)
      - [Tasks to Complete](#tasks-to-complete-16)
      - [Exit Criteria](#exit-criteria-16)
    - [Task 3: Install SQL Server Migration Assistant for Oracle](#task-3-install-sql-server-migration-assistant-for-oracle)
      - [Tasks to Complete](#tasks-to-complete-17)
      - [Exit Criteria](#exit-criteria-17)
    - [Task 4: Install dbForge Fusion tool](#task-4-install-dbforge-fusion-tool)
      - [Tasks to Complete](#tasks-to-complete-18)
      - [Exit Criteria](#exit-criteria-18)
    - [Task 5: Create the Northwind database in Oracle 11g XE](#task-5-create-the-northwind-database-in-oracle-11g-xe)
      - [Tasks to Complete](#tasks-to-complete-19)
      - [Exit Criteria](#exit-criteria-19)
    - [Task 6: Configure the Starter Application to use Oracle](#task-6-configure-the-starter-application-to-use-oracle)
      - [Tasks to Complete](#tasks-to-complete-20)
      - [Exit Criteria](#exit-criteria-20)
  - [Exercise 5: Migrate the Oracle database to SQL Server 2017](#exercise-5-migrate-the-oracle-database-to-sql-server-2017)
    - [Task 1: Migrate the Oracle database to SQL Server 2017 using SSMA](#task-1-migrate-the-oracle-database-to-sql-server-2017-using-ssma)
      - [Tasks to Complete](#tasks-to-complete-21)
      - [Exit Criteria](#exit-criteria-21)
  - [Exercise 6: Migrate the application](#exercise-6-migrate-the-application)
    - [Task 1: Create a new Entity Model against SQL Server](#task-1-create-a-new-entity-model-against-sql-server)
      - [Tasks to Complete](#tasks-to-complete-22)
      - [Exit Criteria](#exit-criteria-22)
    - [Task 2: Modify Application Code](#task-2-modify-application-code)
      - [Tasks to Complete](#tasks-to-complete-23)
      - [Exit Criteria](#exit-criteria-23)
  - [After the hands-on lab](#after-the-hands-on-lab)
    - [Task 1: Delete the resource group](#task-1-delete-the-resource-group)

# Data Platform upgrade and migration hands-on lab unguided

If you have not yet completed the steps to set up your environment in [Before the hands-on lab](./Before%20the%20HOL%20-%20Data%20Platform%20upgrade%20and%20migration.md), you will need to do that before proceeding.

## Abstract and learning objectives

In this hands-on lab, you will implement a proof of concept (POC) for conducting a site analysis for a customer to compare cost, performance, and level of effort required to migrate from Oracle to SQL Server. You will evaluate the dependent applications and reports that will need to be updated and come up with a migration plan. In addition, you will help the customer take advantage of new SQL Server features to improve performance and resiliency, as well as conduct a migration from an old version of SQL Server to Azure SQL Database.

At the end of this hands-on lab, you will be better able to design and build a database migration plan and implement any required application changes associated with changing database technologies.

## Overview

Wide World Importers (WWI) has experienced significant growth in the last few years. In addition to predictable growth, they’ve had a substantial amount of growth in the data they store in their data warehouse. Their data warehouse is starting to show its age; slowing down during extract, transform, and load (ETL) operations and during critical queries. It was built on SQL Server 2008 R2 Standard Edition.

The WWI CIO has recently read about new performance enhancements of Azure SQL Database and SQL Server 2017. She is excited about the potential performance improvements related to clustered ColumnStore indexes. She is also hoping that table compression will improve performance and backup times.

WWI is concerned about upgrading their database to Azure SQL Database or SQL Server 2017. The data warehouse has been successful for a long time. As it has grown, it has filled with data, stored procedures, views, and security. WWI wants assurance that if it moves its data store, it won’t run into any incompatibilities with the storage engine of Azure SQL Database or SQL Server 2017.

WWI’s CIO would like a POC of a data warehouse move and proof that the new technology will help ETL and query performance.

## Solution architecture

Below is a diagram of the solution architecture you will build in this lab. Please study this carefully, so you understand the whole of the solution as you are working on the various components.

![This solution diagram is divided in to Microsoft Azure, and On Premises. Microsoft Azure includes SQL Server 2017 in a VM as an Always On Secondary, and Azure SQL Stretch Database to extend the audit table to Azure. On Premise includes the following elements: API App for vendor connections; Web App for Internet Sales Transactions; ASP.NET Core App for inventory management; SQL Server 2017 OLTP for Always On and JSON store; SSRS 2017 for Reporting of OLTP, Data Warehouse, and Cubes; SSIS 2017 for a Data Warehouse Load; Excel for reporting; SQL Server 2017 Enterprise for a Data Warehouse; and SSAS 2017 for a Data Warehouse. ](./media/preferred-solution-architecture.png "Preferred Solution diagram")

The solution begins with using the Microsoft Data Migration Assistant to perform an assessment to see what potentials issues need to be addressed in upgrading the database to SQL Server 2017 or Azure SQL Database. After correcting any issues, the SQL Server 2008 database is migrated to Azure SQL Database, using the Azure Database Migration Service. Two features of Azure SQL Database, Table Compression and ColumnStore Index, will be applied to demonstrate value and performance improvements from the upgrade. For the ColumnStore Index, a new table based on the existing FactResellerSales table will be created, and a ColumnStore index applied. Next, the Oracle XE database supporting the application will be migrated to an on-premises SQL Server 2017 Enterprise instance using SQL Server Migration Assistant (SSMA) 7.x for Oracle. Once the Oracle database has been migrated, the NorthwindMVC application will be updated, so it targets SQL Server 2017 instead of Oracle. The entity models are updated against SQL Server, and code updates are made to use the new Entity Framework context based on SQL Server. Corrections to stored procedures are made due to differences in how stored procedures are accessed in Oracle versus SQL Server. Azure SQL Stretch Database will be used to extend the audit log table to Azure, helping to prevent the recurrence of a system crash caused by the audit log table filling up.

## Before the hands-on lab

If you have not yet completed the steps to set up your environment in [Before the HOL](./Before%20the%20HOL.md), you will need to do that before proceeding.

## Requirements

- Microsoft Azure subscription must be pay-as-you-go or MSDN
  - Trial subscriptions will not work
- A virtual machine configured with:
  - Visual Studio 2019 Community or later

## Exercise 1: Deploy SQL Server instances

Duration: 60 minutes

In this exercise, you will install and configure SQL Server 2017 and SQL Server 2008 R2 on the SqlServerDw VM. The databases on this VM will act as the "on-premises" databases for this hands-on lab.

### Task 1: Install SQL Server 2017

In this task, you will install SQL Server 2017 and Microsoft SQL Server Management Studio (SSMS) on the SqlServerDw VM.

#### Tasks to Complete

- Install SQL Server 2017 Developer Edition on the VM from <https://www.microsoft.com/sql-server/sql-server-downloads>
  - Ensure the SQL Server Database Engine runs under the demouser Windows account.
  - Enable Mixed Mode Authentication
    - Ensure the SA password is set to Password.1!!
  - Make sure to add the demouser to the SQL Server Administrators group
- Install SQL Server Management Studio (SSMS) 17

#### Exit Criteria

- You have successfully installed SQL Server 2017 Developer Edition and SSMS 17 on the VM

### Task 2: Install SQL Server 2008 R2

In this task, you will install SQL Server 2008 R2 as a Named Instance on the SqlServerDw VM

#### Tasks to Complete

- Install SQL Server 2008 R2 Express with Advanced Services on the VM from <https://www.microsoft.com/download/details.aspx?id=30438>
  - Be sure that the SQL Database Engine runs under the demouser Windows account
  - Enable Mixed Mode Authentication
    - Ensure the SA password is set to Password.1!!
  - Ensure the demouser account is added to the SQL Server Administrators group

#### Exit Criteria

- You have successfully installed both versions of SQL Server on the same VM

### Task 3: Install AdventureWorks sample database

In this task, you will install the AdventureWorks database in SQL 2008 R2. It will act as the "on-premises" data warehouse database that you will migrate to Azure SQL Database.

#### Tasks to Complete

- Download the AdventureWorks 2008R2 DW script from <https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2008r2>
- Execute `instawdwdb.sql` against the SQL Server 2008 instance on your VM
- Rename the database to WorldWideImporters

#### Exit Criteria

- You have successfully created the WorldWideImporters database

### Task 4: Update SQL Server settings using Configuration Manager

In this task, you will update the SQL Server service accounts and other settings associated with the SQL Server instances installed on the VM.

#### Tasks to Complete

- Set the services account for the SQL Server service (in both the SQL Server 2008 and 2017 instances) to use the demouser Windows account
- Ensure that remote TCP/IP connections are allowed to the SQL Server 2008 and 2017 instances
- Enable the SA login with the password Password.1!!
- Create a new, empty database called Northwind in the SQL Server 2017 instance
- Copy the dynamic port assign to the SQL Server 2008 R2 named instance

#### Exit Criteria

- You have successfully created the Northwind target database in SQL Server 2017, and can access this SQL Server using SQL Server credentials

### Task 5: Add inbound port rule

In this task, you will add an inbound port rule for the SqlServerDw VM, to allow the dynamic port used by the SQL Server 2008 R2 Named Instance.

#### Tasks to Complete

- Create an inbound port rule for the SqlServerDw VM, opening the dynamic port assigned to the SQL Server 2008 R2 named instance

#### Exit Criteria

- The SQL Server 2008 R2 named instance on the SqlServerDw VM is accessible from external applications

### Task 6: Copy the SqlServerDw VM IP address

#### Tasks to Complete

- Copy the IP address assigned to the SqlServerDw VM

#### Exit Criteria

- You have copied the IP address of the SqlServerDw VM into a text editor, such as Notepad, for later reference

## Exercise 2: Migrate SQL Server to Azure SQL Database using DMS

Duration: 60 minutes

Wide World Importers would like a Proof of Concept (POC) that moves their database warehouse to Azure SQL Database. They would like to know about any incompatible features that will block their eventual production move. In this exercise, you will use the [Azure Database Migration Service](https://azure.microsoft.com/services/database-migration/) (DMS) to migrate the WorldWideImporters database from the "on-premises" SQL Server 2008 R2 instance to [Azure SQL Database](https://docs.microsoft.com/azure/sql-database/).

### Task 1: Register the Microsoft DataMigration resource provider

In this task, you will register the Microsoft.DataMigration resource provider with your subscription in Azure.

#### Tasks to Complete

- Register the Microsoft.DataMigration resource provider with your Azure subscription

#### Exit Criteria

- The Microsoft.DataMigration resource provider is accessible from your Azure subscription

### Task 2: Create Azure Database Migration Service

In this task, you will provision an instance of the Azure Database Migration Service (DMS)

#### Tasks to Complete

- Create an instance of the Azure Database Migration Service (DMS)

#### Exit Criteria

- DMS has been provisioned in Azure

### Task 3: Assess the on-premises database

Wide World Importers would like an assessment to see what potential issues they would have to address in moving their database to Azure SQL Database.

#### Tasks to Complete

- Download the Data Migration Assistant (DMA) v3.x from <https://www.microsoft.com/download/details.aspx?id=53595>
- Perform an assessment of the migration of the SQL Server 2008 R2 WorldWideImporters database to Azure SQL Database using DMA

#### Exit Criteria

- You have a list of the issues WWI will need to consider in upgrading their database to Azure SQL Database

### Task 4: Migrate the database schema

After you have reviewed the assessment results and you have ensured the database is a candidate for migration to Azure SQL Database, use the Data Migration Assistant to migrate the schema to Azure SQL Database.

#### Tasks to Complete

- Using DMA, deploy the database schema for the SQL Server 2008 R2 WorldWideImporters database to Azure SQL Database

#### Exit Criteria

- You are able to view the empty tables created by the schema deployment in the WorldWideImporters database in Azure SQL Database.

### Task 5: Create a migration project

In this task, you will create a new migration project for the WorldWideImporters database

#### Tasks to Complete

- Using DMs, create a new migration project for the migration of the SQL Server 2008 R2 WorldWideImporters database to Azure SQL Database

#### Exit Criteria

- On the Azure Database Migration Project blade, you see a success message, similar to the following

  ![On the Azure Database Migration Project blade, a success message is displayed.](media/dms-migration-project-successfully-created.png "DMS project created successfully")

### Task 6: Run the migration

In this task, you will create a new activity in the Azure Database Migration Service to execute the migration from the "on-premises" SQL Server 2008 R2 server to Azure SQL Database.

#### Tasks to Complete

- Using DMs, migrate the SQL Server 2008 R2 WorldWideImporters database to Azure SQL Database

#### Exit Criteria

- You successfully migrated the SQL Server 2008 R2 database to Azure SQL Database

### Task 7: Verify data migration

In this task, you will use SSMS to verify the database was successfully migrated to Azure SQL Database

#### Tasks to Complete

- Connect to your Azure SQL Database in SSMS and verify data exists in the tables

#### Exit Criteria

- You can access data in the tables migrated by DMS

## Exercise 3: Post Upgrade Enhancement

Duration: 30 minutes

In this exercise, you will confirm that the POC was successful. To demonstrate value from the upgrade, the new features of SQL Server 2017 (Table Compression and ColumnStore Index) will be enabled to immediately show benefit.

### Task 1: Table Compression

#### Tasks to Complete

- Take note of the data space and index space used by the FactInternetSales table
- Enable Table Compression (page type) on the FactInternetSales table

#### Exit Criteria

- You observe a change in the data space and index space

### Task 2: Clustered ColumnStore Index

In this task, you will create a new table based on the existing FactResellerSales table and apply a ColumnStore index

#### Tasks to Complete

- Create a new table called ColumnStore_FactResellerSales from the FactResellersSales table
- Create a clustered ColumnStore index on the new table
- Query the tables and compare the execution plan query costs, batch percentage
- Query the tables with statistic IO on and compare the logical and lob logical reads

#### Exit Criteria

- You observe a performance improvement

  ![Various information is highlighted on the Messages tab of the Results pane.](./media/ssms-query-results-messages-stastics-io.png "Compare the information")

## Exercise 4: Setup Oracle 11g Express Edition

Duration: 45 minutes

In this exercise, you will install Oracle XE on your Lab VM, load a sample database supporting an application, and then migrate the database to SQL Server 2017.

### Task 1: Install Oracle XE

#### Tasks to Complete

- Install Oracle XE on your lab VM from: <http://www.oracle.com/technetwork/database/database-technologies/express-edition/downloads/index.html>
- Be sure to set the SYS and SYSTEM password to Password.1!!

#### Exit Criteria

- You have successfully installed Oracle

### Task 2: Install Oracle Data Access Components

#### Tasks to Complete

- Install Oracle Data Access Components (ODAC) from <http://www.oracle.com/technetwork/database/windows/downloads/index-090165.html>
- Be sure the install the 64-bit ODAC components
- Ensure you configure ODP.NET and Oracle Providers for ASP.NET at machine-wide level

#### Exit Criteria

- You have successfully installed ODAC

### Task 3: Install SQL Server Migration Assistant for Oracle

#### Tasks to Complete

- Install SSMA 7.x from <https://www.microsoft.com/en-us/download/details.aspx?id=54258>

#### Exit Criteria

- You have successfully installed SSMA on your Lab VM

### Task 4: Install dbForge Fusion tool

In this task, you will install a third-party extension to Visual Studio to enable interaction with, and script execution for, the Oracle database in Visual Studio 2019 Community Edition. This step is necessary because the Oracle Developer Tools extension does not currently work with Visual Studio 2019 Community Edition.

#### Tasks to Complete

- Install dbForge Fusion for Oracle Professional trial version from <https://www.devart.com/dbforge/oracle/fusion/download.html>

#### Exit Criteria

- The dbForge Fusion extension is successfully installed in Visual Studio 2019 Community

### Task 5: Create the Northwind database in Oracle 11g XE

In this task, you will create a connection to the Oracle database on your Lab VM, and create a database called Northwind

#### Tasks to Complete

- Download the Data Platform upgrade and migration GitHub repo to obtain the start project files from <https://github.com/Microsoft/MCW-Data-Platform-upgrade-and-migration>
- Using Database Explorer provided under the Fusion menu installed by dbForge Fusion in Visual Studio, create a connection to the Oracle database
- Run the SQL scripts in order provided under the Oracle Scripts folder of the starter

#### Exit Criteria

- You have created the Northwind database with schema and data in Oracle

### Task 6: Configure the Starter Application to use Oracle

In this task, you will add the necessary configuration to the NorthwindMVC solution to connect to the Oracle database you created in the previous task.

#### Tasks to Complete

- Modify the web.config of the NorthwindMVC web app so the OracleConnectionString points to your Oracle instance of Northwind

#### Exit Criteria

- You can run the application and see the Dashboard from Northwind Traders load

  ![This is a screenshot of the Northwind Traders Dashboard.](./media/northwind-traders-dashboard.png "View the dashboard")

## Exercise 5: Migrate the Oracle database to SQL Server 2017

Duration: 30 minutes

In this exercise, you will migrate the Oracle database into SQL Server 2017 using SSMA.

### Task 1: Migrate the Oracle database to SQL Server 2017 using SSMA

#### Tasks to Complete

- Use SSMA to migrate the schema and data from the NW database on Oracle to the Northwind database in SQL Server 2017

  - Fix data type errors reported during schema conversion
  - In SQL Server 2017, Microsoft now by default requires that all type of assemblies (SAFE, EXTERNAL_ACCESS, UNSAFE) are authorized for UNSAFE access. Add the `SSMA4OracleSQLServerCollections.NET` and `SSMA4OracleSQLServerExtensions.NET` assemblies to the SQLCLR whitelist for the SQL Server 2017 instance.
  - Execute the script below in SSMS for each assembly

  Note: The binary value for each assembly can be obtained by saving the assemblies as a script in SSMA.

  ```sql
  USE master;
  GO

  DECLARE @clrName nvarchar(4000) = '[INSERT ASSEMBLY NAME]'
  DECLARE @asmBin varbinary(max) = [INSERT BINARY];
  DECLARE @hash varbinary(64);

  SELECT @hash = HASHBYTES('SHA2_512', @asmBin);

  EXEC sys.sp_add_trusted_assembly @hash = @hash, @description = @clrName;
  ```

#### Exit Criteria

- Your Northwind database in SQL Server has the schema objects and the tables have data

## Exercise 6: Migrate the application

Duration: 15 minutes

In this exercise, you will modify the NorthwindMVC application, so it targets SQL Server 2017 instead of Oracle.

### Task 1: Create a new Entity Model against SQL Server

#### Tasks to Complete

- Modify the web.config so that SqlServerConnectionString is correct for your environment
- Delete the existing entity model present in the files under the Data folder in Solution explorer
- Connect to the SQL Server database through Server Explorer in Visual Studio
- Create a new Code First model against the SQL Server database

#### Exit Criteria

- You have successfully generated a new model

### Task 2: Modify Application Code

#### Tasks to Complete

- Change the DataContext to use your SqlServerConnectionString
- Within `HomeController.cs` uncomment the SQL Server specific code and comment out the Oracle specific code
- Modify `SALESBYYEAR.cs` so that `YEAR` is of type int and `SUBTOTAL` is of type decimal
- Modify `SalesByYearViewModel.cs` so that `Year` is of type int
- Run the SALES_BY_YEAR_fix.sql

#### Exit Criteria

- You can run the application and see the dashboard showing correctly, this time with data coming from SQL Server

  ![This is a screenshot of the Northwind Traders Dashboard.](./media/northwind-traders-dashboard.png "View the dashboard")

## After the hands-on lab

Duration: 10 mins

In this exercise, you will delete any Azure resources that were created in support of the lab. You should follow all steps provided after attending the Hands-on lab to ensure your account does not continue to be charged for lab resources.

### Task 1: Delete the resource group

1. Using the [Azure portal](https://portal.azure.com), navigate to the Resource group you used throughout this hands-on lab by selecting Resource groups in the left menu
2. Search for the name of your research group, and select it from the list
3. Select Delete in the command bar, and confirm the deletion by re-typing the Resource group name, and selecting Delete

You should follow all steps provided _after_ attending the Hands-on lab.
