### Let us know how we’re doing!  
Please take a moment to fill out the [Microsoft Cloud Workshop Survey](https://forms.office.com/Pages/ResponsePage.aspx?id=v4j5cvGGr0GRqy180BHbRyEtIpX7sDdChuWsXhzKJXJUNjFBVkROWDhSSVdYT0dSRkY4UVFCVzZBVy4u) and help us improve our offerings.

# Migrating Oracle to Azure SQL and PostgreSQL

Wide World Importers (WWI) has experienced significant growth in the last few years. In addition to predictable growth, they’ve had a substantial amount of growth in the data they store in their data warehouse. Their data warehouse is starting to show its age, slowing down during extract, transform, and load (ETL) operations and during critical queries. The data warehouse is running on SQL Server 2008 R2 Standard Edition.

The WWI CIO has recently read about new performance enhancements of Azure SQL Database and SQL Server 2017. She is excited about the potential performance improvements related to clustered ColumnStore indexes. She is also hoping that table compression can improve performance and backup times.

WWI is concerned about upgrading their database to Azure SQL Database or SQL Server 2017. The data warehouse has been successful for a long time. As it has grown, it has filled with data, stored procedures, views, and security. WWI wants assurance that if it moves its data store, it won’t run into any incompatibilities with the storage engine of Azure SQL Database or SQL Server 2017.

WWI’s CIO would like a POC of a data warehouse move and proof that the new technology can help ETL and query performance.

November 2020

## Target audience

- Application developer
- SQL Developer
- Database Administrator

## Abstracts

### Workshop

In this workshop, you gain a better understanding of how to conduct a site analysis for a customer to compare cost, performance, and level of effort required to migrate from Oracle to SQL Server or PostgreSQL. There are two migration paths included in this workshop and the training files are named appropriately for each path. Both workshops share a common customer Oracle migration scenario. Each workshop path has tailored database platform migration steps to assist you in this learning journey.  You will evaluate the dependent applications and reports that need to be updated and come up with a migration plan. Also, you will design and build a proof of concept (POC) to help the customer take advantage of new SQL Server or PostgreSQL features to improve performance and resiliency.

For those students focusing on the SQL Server migration, you will explore ways to migrate from an old version of SQL Server to the latest version and consider the impact of migrating from on-premises to the cloud.

At the end of this workshop, you will be better able to conduct a site analysis for compare cost, performance, and level of effort required to migrate from Oracle to SQL Server or PostgreSQL.  Given the time required to complete the workshop, it is recommended the student and trainer pick a single migration path.

### Whiteboard design session

In this whiteboard design session, you work with a group to design a proof of concept (POC) for conducting a site analysis for a customer to compare cost, performance, and level of effort required to migrate from Oracle to SQL Server or PostgreSQL. You evaluate the dependent applications and reports that need to be updated and come up with a migration plan. Also, you review ways to help the customer take advantage of the database features to improve performance and resiliency. For the SQL Server path, you explore ways to migrate from an old version of SQL Server to the latest version and consider the impact of migrating from on-premises to the cloud.

At the end of this whiteboard design session, you will be better able to design a database migration plan and execute the steps.

### Hands-on lab

In this hands-on lab, you implement a proof of concept (POC) for conducting a site analysis for a customer to compare cost, performance, and migration level of effort. You will evaluate the dependent applications and reports that need to be updated and come up with a migration plan. Also, you help the customer take advantage of new features to improve performance and resiliency and perform a migration+.

At the end of this hands-on lab, you will be better able to design and build a database migration plan and implement any required application changes associated with changing database technologies.

## Azure services and related products

- Azure App Services
- Azure Database Migration Service (DMS)
- Azure SQL Database
- Azure SQL Data Warehouse
- SQL Server on Azure Virtual Machines (SQL Server 2008 R2 and SQL Server 2017)
- Data Migration Assistant (DMA)
- SQL Server Management Studio (SSMS)
- SQL Server Migration Assistant (SSMA)
- Visual Studio 2019
- Azure Database for PostgreSQL
- ora2pg
- pgAdmin

## Azure solution

Data Modernization to Azure

## Related references

[MCW](https://github.com/Microsoft/MCW)

## Help & Support

We welcome feedback and comments from Microsoft SMEs & learning partners who deliver MCWs.

**_Having trouble?_**

- First, verify you have followed all written lab instructions (including the Before the Hands-on lab document).
- Next, submit an issue with a detailed description of the problem.
- Do not submit pull requests. Our content authors will make all changes and submit pull requests for approval.

If you are planning to present a workshop, _review and test the materials early_! We recommend at least two weeks prior.

**Please allow 5 - 10 business days for review and resolution of issues.**
