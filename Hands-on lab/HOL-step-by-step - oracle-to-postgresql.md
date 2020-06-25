
**Contents** 

- [Exercise 1: Setup Oracle 11g Express Edition](#exercise-1-setup-oracle-11g-express-edition)
    - [Task 1: Install Oracle XE](#task-1-install-oracle-xe)
    - [Task 2: Install Oracle Data Access components](#task-2-install-oracle-data-access-components)
    - [Task 3: Install dbForge Fusion tool](#task-3-install-dbforge-fusion-tool)
    - [Task 4: Create the Northwind database in Oracle 11g XE](#task-4-create-the-northwind-database-in-oracle-11g-xe)
    - [Task 5: Configure the Starter Application to use Oracle](#task-5-configure-the-starter-application-to-use-oracle)

- [Exercise 2: Assess the Oracle 11g Database before Migrating to PostgreSQL](#exercise-2-assess-the-oracle-11g-database-before-migrating-to-postgresql)
    - [Task 1: Update Statistics and Identify Invalid Objects](#task-1-update-statistics-and-identify-invalid-objects)

- [Exercise 3: Migrate the Oracle database to PostgreSQL](#exercise-3-migrate-the-oracle-database-to-postgresql)
    - [Task 1: Create Azure resources and configure the Key Vault](#task-1-create-azure-resources-and-configure-the-key-vault)
    - [Task 2: Configure the PostgreSQL server instance](#task-2-configure-the-postgresql-server-instance)
    - [Task 3: Create a VM migration server](#task-3-create-a-vm-migration-server)
    - [Task 4: Install pgAdmin utilities on the migration server](#task-4-install-pgadmin-utilities-on-the-migration-server)
    - [Task 5: Install ora2pg](#task-5-install-ora2pg)
    - [Task 6: Prepare the Postgre instance using pgAdmin and create an ora2pg project structure](#task-6-prepare-the-postgre-instance-using-pgadmin-and-create-an-ora2pg-project-structure)
    - [Task 7: Create a migration report](#task-7-create-a-migration-report)

- [Exercise 4: Migrate the Application](#exercise-4-migrate-the-application)
    - [Task 1: Migrate the database table schema using ora2pg and psql and copy data into the database]
    - [Task 2: Migrate views]
    - [Task 2: Migrate stored procedures and package bodies]
    - [Task 3: Create new Entity Data Models and update the application on the Lab VM]
    - [Task 4: Deploy the application to Azure]

- [After the hands-on lab](#after-the-hands-on-lab)
    - [Task 1: Delete the resource group](#task-1-delete-the-resource-group)


### Exercise 1: Setup Oracle 11g Express Edition
Duration: 45 minutes

In this exercise, you will install Oracle XE on your Lab VM, load a sample database supporting an application. 

### Task 1: Install Oracle XE

1. Connect to your Lab VM, as you did in Task 5 of the [Before the Hands-on Lab](./Before%20the%20HOL%20-%20Data%20Platform%20upgrade%20and%20migration.md#task-5-connect-to-the-lab-vm) exercise.

    - **Username**: demouser
    - **Password**: Password.1!!

2. In a web browser on your Lab VM, navigate to <https://www.oracle.com/technetwork/database/database-technologies/express-edition/downloads/xe-prior-releases-5172097.html>.

3. On the Oracle Database XE Prior Release Archive page, select **Oracle Database 11gR2 Express Edition for Windows x64** download link.

    ![Accept the license agreement and Oracle Database 11g Express Edition Release 2 for Windows x64 are highlighted under Oracle Database Express Edition 11g Release 2.](./media/oracle-11g-download.png "Oracle 11g download")

4. Accept the license agreement, when prompted, and then select **Download OracleXE112_Win64.zip**.

    ![The license agreement checkbox is checked on the license agreement dialog.](media/download-oracle-xe.png "Download Oracle XE")

5. Sign in with your Oracle account to complete the download. If you don't already have a free Oracle account, you will need to create one.

    ![This is a screenshot of the Sign in screen.](./media/oracle-sign-in.png "Sign in to complete the download")

6. After signing in, the file will download.

7. Unzip the file, and navigate to the `DISK1` folder.

8. Right-select `setup.exe`, and select **Run as administrator**.

    ![In File Explorer, setup.exe is selected, and Run as administrator is highlighted in the shortcut menu.](./media/windows-file-menu-run-as-administrator.png "Run setup.exe as an administrator")

9. Select **Next** to step through each screen of the installer, accepting the license agreement and default values, until you get to the **Specify Database Passwords** screen.

10. On the **Specify Database Passwords** screen, set the password to **Password.1!!**, and select **Next**.

    ![The above credentials are entered on the Specify Database Passwords screen.](./media/oracle-11g-install-passwords.png "Set the password")

11. On the Summary screen, take note of the ports being assigned, and select **Install**.

    ![Several of the ports being assigned are highlighted on the Summary screen.](./media/oracle-11g-install-summary.png "Note the ports being assigned")

12. Select **Finish** on the final dialog to compete the installation.

### Task 2: Install Oracle Data Access components

In this task, you will download and configure Oracle Data Access components so that you can connect and access to the Northwind database. 

1. On your Lab VM, navigate to <http://www.oracle.com/technetwork/database/windows/downloads/index-090165.html>.

2. On the 64-bit Oracle Data Access Components (ODAC) Downloads page, scroll down and locate the **64-bit ODAC 12.2c Release 1 (12.2.0.1.1) for Windows x64** section, and then select the **ODAC122011_x64.zip** link.

    ![Accept the license agreement and ODAC122010\_x64.zip are highlighted on the 64-bit Oracle Data Access Components (ODAC) Downloads screen.](./media/oracle-odac-download.png "64-bit Oracle Data Access Components (ODAC) Downloads screen")

3. Accept the license agreement, and then select **Download ODAC122011_x64.zip**.

    ![The Oracle license agreement dialog is displayed for downloading the Oracle Data Access Components.](media/oracle-odac-license-dialog.png "Download ODAC")

4. When the download completes, extract the contents of the ZIP file to a local drive.

5. Navigate to the folder containing the extracted ZIP file, and right-select `setup.exe`, then select **Run as administrator** to begin the installation.

6. Select **Next** to accept the default language, English, on the first screen.

7. On the Specify Oracle Home User screen, accept the default, Use Windows Built-in Account, and select **Next**.

8. Accept the default installation locations, and select **Next**.

9. On the **Available Product Components**, uncheck **Oracle Data Access Components Documentation for Visual Studio**, and select **Next**.

    ![Oracle Data Access Components Documentation for Visual Studio is cleared on the Available Product Components screen, and Next is selected at the bottom.](./media/oracle-odac-install-product-components.png "Clear Oracle Data Access Components Documentation for Visual Studio")

10. On the ODP.NET screen, check the box for **Configure ODP.NET and/or Oracle Providers for ASP.NET at machine-wide level**, and select **Next**.

    ![Configure ODP.NET and/or Oracle Providers for ASP.NET at machine-wide level is selected on the ODP.NET screen, and Next is selected at the bottom.](./media/oracle-odac-install-odp-net.png "Select Configure ODP.NET and/or Oracle Providers for ASP.NET at machine-wide level")

11. On the DB Connection Configuration screen, enter the following:

    - **Connection Alias**: Northwind
    - **Port Number**: 1521
    - **Database Host Name**: localhost
    - **Database Service Name**: XE

        ![The information above is entered on the DB Connection Configuration screen, and Next is selected at the bottom.](./media/oracle-odac-install-db-connection.png "Enter the information")

    - Select **Next**.

12. If the Next button is disabled on the Perform Prerequisite Checks screen, check the **Ignore All** box, and then select **Next**. This screen will be skipped by the installer if no missing requisites are found.

    ![The Ignore All box is cleared on highlighted on the Perform Prerequisite Checks screen, and Next is selected at the bottom.](./media/oracle-odac-install-prerequisite-checks.png "Perform Prerequisite Checks")

13. On the Summary screen, select **Install**.

14. On the Finish screen, select **Close**.

### Task 3: Install dbForge Fusion tool

In this task, you will install a third-party extension to Visual Studio to enable interaction with, and script execution for, Oracle databases in Visual Studio 2019 Community Edition.

> This step is required because the Oracle Developer Tools extension does not currently work with the Community edition of Visual Studio.

1. On your Lab VM, open a web browser and navigate to <https://www.devart.com/dbforge/oracle/fusion/download.html>.

2. Scroll down on the page, and download a Trial of the current version by selecting the blue download link.

    ![The Download button is highlighted under dbForge Fusion, Current Version.](./media/dbforge-trial-download.png "dbForge Fusion, Current Version section")

3. Run the installer.

    >**Note**: Close Visual Studio if it is open to complete the installation.

4. Select **Next** on the Welcome screen.

    ![Next is selected on the Devart dbForge Fusion for Oracle Welcome screen.](./media/dbforge-install-welcome.png "Select Next on the Welcome screen")

5. Select **Next** on each screen, accepting the license agreement and default settings, until reaching the Ready to Install screen.

6. Select **Install** on the Ready to Install screen.

    ![Install is selected on the Ready to Install screen.](./media/dbforge-install-ready-to-install.png "Select Install ")

7. Select **Finish** when the installation is complete.

### Task 4: Create the Northwind database in Oracle 11g XE

WWI has provided you with a copy of their application, including a database script to create their Oracle database. They have asked that you use this as a starting point for migrating their database and application to PostGre SQL. In this task, you will create a connection to the Oracle database on your Lab VM, and create a database called Northwind.

1. Navigate to the Lab VM and download the starter project by downloading a .zip copy of the Data Platform upgrade and migration project from the GitHub repo.

6. In a web browser, navigate to the [Data Platform upgrade and migration MCW repo](https://github.com/Microsoft/MCW-Data-Platform-upgrade-and-migration)

7. On the repo page, select **Clone or download**, then select **Download ZIP**.

    ![Download .zip containing the Data Platform upgrade and migration repository](media/git-hub-download-repo.png "Download ZIP")

8. Unzip the contents to **C:\handsonlab**.

9. Within the **handsonlab** folder, navigate to the folder `MCW-Data-Platform-upgrade-and-migration-master\Hands-on lab\lab-files\starter-project`, and double-select `NorthwindMVC.sln` to open the project in Visual Studio 2019.

10. If prompted for how you want to open the file, select **Visual Studio 2019**, and select **OK**.

11. Sign into Visual Studio (or create an account if you don't have one), when prompted.

12. At the Security Warning screen, uncheck **Ask me for every project in this solution**, and select **OK**.

    ![Ask me for every project in this solution is cleared and OK is selected on the Security Warning screen.](./media/visual-studio-security-warning.png "Clear Ask me for every project in this solution")

13. Once then solution is open in Visual Studio, select the **Extensions -> Fusion** menu, and then select **New Connection**.

    ![New Connection is highlighted in the Fusion menu in Visual Studio.](./media/visual-studio-fusion-menu-new-connection.png "Select New Connection")

14. In the Database Connection properties dialog, set the following values:

    - **Host**: localhost
    - **Port**: Leave 1521 selected.
    - Select **SID**, and enter **XE**.
    - **User**: system
    - **Password**: Password.1!!
    - Check **Allow saving password**.
    - **Connect as**: Normal
    - **Connection Name**: Northwind

    ![The information above is entered in the Database Connection Properties * Oracle dialog box, and OK is selected at the bottom.](./media/visual-studio-fusion-new-database-connection.png "Specify the settings")

15. Select **Test Connection** to verify the settings are correct, and select **OK** to close the popup.

16. Select **OK** to create the Database Connection.

17. You will now see the Northwind connection in the Database Explorer window.

    ![The Northwind connection is selected in the Database Explorer window.](./media/visual-studio-fusion-database-explorer.png "View the Northwind connection")

18. In Visual Studio, select **File** in the menu, then select **Open File**, and navigate to `C:\handsonlab\MCW-Data-Platform-upgrade-and-migration-master\Hands-on lab\lab-files\starter-project\Oracle Scripts\`, select the file `1.northwind.oracle.schema.sql`, and then select **Open**.

    ![The file, 1.northwind.oracle.schema.sql, is selected and highlighted in the Open File window.](./media/visual-studio-open-file.png "Open File dialog")

    > **Note**: You may receive a notification that your Fusion trial has expired when you do this. This can be ignored for this hands-on lab. Close that dialog, and continue to the query window that opens in Visual Studio.

19. Select the **Execute** Fusion script button on the Visual Studio toolbar to run the SQL script.

    ![The Execute Fusion script icon is highlighted on the Visual Studio toolbar.](./media/visual-studio-fusion-execute.png "Run the script")

20. The results of execution can be viewed in the Output window, found at the bottom left of the Visual Studio window.

    ![Output is highlighted in the Output window.](./media/visual-studio-fusion-output-query-1.png "View the results")

21. In the Database Explorer window, right-select on the **Northwind** connection, and select **Modify Connection** (If the Database Explorer is not already open, you can open it by selecting Fusion in the menu, then selecting Database Explorer).

    ![Modify Connection is highlighted in the submenu for the Northwind connection in the Database Explorer window.](./media/visual-studio-database-explorer-modify-connection.png "Modify Connection")

22. In the Modify Connection dialog, change the username and password as follows:

    - **Username**: NW
    - **Password**: oracledemo123

23. Select **Test Connection** to verify the new credentials work.

    ![The information above is entered and highlighted in the Database Connection Properties * Oracle dialog box, and Test Connection is selected at the bottom.](./media/visual-studio-database-explorer-modify-connection-update.png "Specify the settings")

24. Select **OK** to close the Database Connection properties dialog.

25. Select the **Open File** icon on the Visual Studio toolbar.

    ![The Open File icon is highlighted on the Visual Studio toolbar.](./media/visual-studio-toolbar-open-file.png "Select Open File")

26. In the Open File dialog, navigate to `C:\handsonlab\MCW-Data-Platform-upgrade-and-migration-master\Hands-on lab\lab-files\starter-project\Oracle Scripts`, select the file `2.northwind.oracle.tables.views.sql`, and then select **Open**.

27. As you did previously, select the **Execute** Fusion script button on the toolbar, and view the results of execute in the Output pane.

    ![The Execute Fusion script icon is highlighted on the Visual Studio toolbar.](./media/visual-studio-fusion-execute.png "Run the script")

28. Repeat steps 26 - 27, replacing the file name in step 26 with each of the following:

    - `3.northwind.oracle.packages.sql`

    - `4.northwind.oracle.sps.sql`

        - During the Execute script step for this file, you will need to execute each CREATE OR REPLACE statement independently.

        - Using your mouse, select the first statement, starting with CREATE and going to END;

        ![The first statement between CREATE and END is highlighted.](./media/visual-studio-fusion-query-selection.png "Select the first statement")

        - Next, select **Execute Selection** in the Visual Studio toolbar.

        ![Execute Selection is highlighted on the Visual Studio toolbar.](./media/visual-studio-fusion-query-execute-selection.png "Select Execute Selection")

        - Repeat this for each of the remaining CREATE OR REPLACE... END; blocks in the script file (there are 7 more to execute, for 8 total).

    - `5.northwind.oracle.seed.sql`

        > **Important**: This query can take several minutes to run, so make sure you wait until you see **Execute succeeded** message, followed by **Done: 5.northwind.oracle.seed.sql**, in the output window before executing the next file, like the following:

        ![This is a screenshot of the Execute succeeded message in the output window.](./media/visual-studio-fusion-query-completed.png "Execute succeeded message")

    - `6.northwind.oracle.constraints.sql
    
### Exercise 2: Assess the Oracle 11g Database before Migrating to PostgreSQL
Duration: 15 mins

In this exercise, you will prepare the existing Oracle database for its migration to PostgreSQL. This will be done through updating statistics and identifying invalid objects. You will update statistics about the database as they become outdated as data volumes tend to change over time, as well as column charateristics. You will also identify invalid objects in the Oracle database that will not be exported over by ora2pg, the migration utility used. 

### Task 1: Update Statistics and Identify Invalid Objects

1. In Visual Studio, access the NW Schema in the Database Explorer. To create a new SQL file, where we will house the updated statements, navigate to **Create New SQL** button near the top right corner of Visual Studio.

    ![Creating new SQL file](./media/creating-new-sql-file.png)

2. Now, you will populate the new file with the following statements:
```sql
-- 11g script
EXECUTE DBMS_STATS.GATHER_SCHEMA_STATS(ownname => 'NW');
EXECUTE DBMS_STATS.GATHER_DATABASE_STATS;
EXECUTE DBMS_STATS.GATHER_DICTIONARY_STATS;
```
3. Save the file as `update-llg-stats.sql` in the `C:\handsonlab\MCW-Data-Platform-upgrade-and-migration\Hands-on lab\lab-files\starter-project` directory. Run the file as you did when you created database objects.

    ![The Execute Fusion script icon is highlighted on the Visual Studio toolbar.](./media/visual-studio-fusion-execute.png "Run the script")

4. Now, we will utilize a query that lists database objects that are invalid, and hence unsupported by ora2pg meaning that ora2pg will not be converted and exported. It is recommended to fix any errors and compile the objects before starting the migration process. Create a new file, titled `show-invalid-objects.sql`, in the `C:\handsonlab\MCW-Data-Platform-upgrade-and-migration\Hands-on lab\lab-files\starter-project` directory. This simple query returns all invalid objects in the current schema.

```sql
SELECT owner, object_type, object_name
FROM all_objects
WHERE status = 'INVALID';
```

### Exercise 3: Migrate the Oracle database to PostgreSQL
Duration: 2 hours

In this exercise, you will configure Azure Database for PostgreSQL in the lab VM, migrate to it from the Oracle database, and then update the application with the new data source.

### Task 1: Create Azure Resources

We need to create a PostgreSQL instance and an App Service to host our application.

1. As shown in **Before the HOL**, you will need to navigate to the **New** page accessed by selecting **+ Create a resource**. Then, navigate to **Databases** under the **Azure Marketplace** section. Select **Azure Database for PostgreSQL**.

    ![Navigating Azure Marketplace](./media/creating-new-postgresql-db.png)

2. There are two deployment options: **Single Server** and **Hyperscale (Citus)**. Single Server is best suited for traditional transactional workloads whereas Hyperscale is best suited for ultra high-performance, multi-tenant applications. For our simple application, we will be utilizing a single server for our database. 

    ![Choosing the right deployment option](./media/single-server-selection.PNG)

3. Configure your instance to reside in the hands-on-lab-SUFFIX resource group. Enter a unique server name. Make sure to choose PostgreSQL version 11, not 10. As for the administrator credentials, we will be using **solldba** as the admin username, and **Password.1!!** as the admin password. Select **Review + create** once you are ready.

    ![Configuring instance details](./media/postgresql-config.PNG)

4. Select **Create** to start the deployment. Once the deployment completes, we will move on to creating an Azure Web App.

5. At the **New** page, navigate to **Web** under **Azure Marketplace**. Select **Web App**.

    ![Navigating to Web App on Azure Marketplace](./media/creating-web-app.png)

6. Create the new app in your hands-on-lab-SUFFIX resource group. Configure a unique app name (the name you choose will form part of your app's URL). Choose **ASP.NET V4.7** as your runtime stack. Select a region that supports the necessary resources. Keep all other settings at their default values. Select **Review + create** when you are ready.

    ![Configuring web app details](./media/web-app-configuration.PNG)

7. Select **Create** after reviewing parameters. Once the deployment finishes, navigate to the **App Service** resource you created. Select **Get publish profile** under the resource's **Overview** page.

    ![Downloading Get Publish Profile file](./media/get-app-publish-file.png)

8. Save the file and move it to `C:\handsonlab\MCW-Data-Platform-upgrade-and-migration\Hands-on lab\lab-files\starter-project`. Later, we will need this file to import into Visual Studio for deployment. 

9. We need to ensure that Azure supports the version of .NET used in the solution. We will do this by changing the target framework on the solution to **.NET Framework 4.7.2**. Open the NorthwindMVC solution in Visual Studio. Right-select the NorthwindMVC project (not the solution) and select **Properties**. Find the **Target framework:** dropdown menu and select **.NET Framework 4.7.2**.

    ![Changing the target framework of the solution](./media/changing-target-framework.PNG)

### Task 2: Configure the PostgreSQL server instance


1. Storage auto-growth is a feature in which Azure will add more storage automatically when you run out of it. We do not need it for our purposes so we will need to disable it. To do this, locate the PostgreSQL instance you created. Then, under the **Settings** tab, select **Pricing tier**.

    ![Changing the pricing tier](./media/changing-tier.PNG)

2. Find the **Storage Auto-growth** switch, and disable the feature. Select **OK** at the bottom of the page to save your change. 

    ![Disabling storage auto-growth](./media/disabling-auto-grow.PNG)

3. Now, we need to implement firewall rules for the PostgreSQL database so we can access it. Locate the **Connection security** selector under the **Settings** tab.

    ![Connection Security settings](./media/entering-connection-settings.PNG)

4. We will add an access rule. Since we are storing insecure test data, we can open the 0.0.0.0 to 255.255.255.255 range (all IPv4 addresses). Azure makes this option available. Press the **Save** button at the top of the page once you are ready.

    ![Adding IP addresses as an Access Rule](./media/adding-open-ip-address-range.png)

### Task 3: Install pgAdmin on the LabVM

1. You will need to navigate to <https://www.pgadmin.org/download/pgadmin-4-windows/> to obtain the latest version of pgAdmin 4, which, at the time of writing, is **v4.22**. Select the link to the installer, as shown below.

    ![Install pgAdmin](./media/installing-pgadmin.PNG)

2. Download the **pgadmin4-4.22-x86.exe** file--not the one with the **.asc** extension.

    ![Choosing the correct installer](./media/correct-pgadmin-installer.PNG)

3. Once the installer launches, accept all defaults. Complete the installation.

4. By default, pgAdmin opens automatically. If it does not, however, you can search for it using the Windows key. When launched, pgAdmin should open in Internet Explorer.

5. pgAdmin will prompt you to set a password to govern access to database credentials. I will use **Password.1!!** as my entry. Confirm your choice. For now, our configuration of pgAdmin is complete.

### Task 4: Install ora2pg

ora2pg is the tool we will use to migrate database objects and data. Luckily, Microsoft's Data Migration Team has greatly simplified the process of obtaining this tool by providing the **installora2pg.ps1** script, available here <https://github.com/microsoft/DataMigrationTeam/blob/master/IP%20and%20Scripts/PostgreSQL%20Migration%20and%20Assessment%20Tools/installora2pg.ps1>. We have made this script available to you at `C:\handsonlab\MCW-Data-Platform-upgrade-and-migration\Hands-on lab\lab-files\starter-project\Postgre Scripts\installora2pg.ps1`.

1. Navigate to the location mentioned above and right-select `installora2pg.ps1`. Then, select **Run with PowerShell**. 

    ![Installing ora2pg](./media/running-ora2pg-install-script.png)

2. After Perl is installed (about 5 minutes after starting the script), you will need to install the Oracle client library and SDK. To do this, you will first need to navigate to https://www.oracle.com/database/technologies/instant-client/winx64-64-downloads.html. Then, scroll to **Version 12.2.0.1.0**. Select the installer for the **Basic Package**. Feel free to download the zipped folder to your Downloads directory, since we will need to unzip it to a certain location. 

    ![Downloading basic package](./media/basic-package.PNG)

3. On the same Oracle page as above, again under the Version 12.2.0.1.0 section, locate the **SDK Package** installer under the **Development and Runtime - optional packages** section. Once more, feel free to keep the zipped file in the Downloads directory.

    ![SDK Package download](./media/sdk-package.PNG)

4. Navigate to the directory where the zipped instant client packages reside. First, for the basic package, right-select it, and select **Extract All...**. When prompted to choose the destination directory, navigate to the `C:\` location. Finally, select **Extract**. Repeat this process for the zipped SDK.

    ![Installing client and SDK Packages](./media/installing-basic-instantclient-package.PNG)

5. Return to the PowerShell script. Press any key to terminate the script's execution. Launch the script once more. If the previous steps were successful, the script should be able to locate **oci.dll** under `C:\instantclient_12_2\oci.dll`.

6. Once ora2pg installs, you will need to configure PATH variables. Search for **View advanced system settings** in Windows. Select the result, and the **System Properties** dialog box should open. By default, the **Advanced** tab should be showing, but if not, navigate to it. Then, select **Environment Variables...**. 
    
    ![Entering environment labels](./media/enter-environment-variables.png)

7. Under **System variables**, select **Path**. Select **Edit...**.

    ![Selecting correct path](./media/selecting-path.png)

8. The **Edit environment variable** box should be displaying. Select **New**. Enter **C:\instantclient_12_2**. Repeat this process, but enter **%%PATH%%** instead.

    ![Path variable configuration](./media/path-variable-configuration.png)

### Task 5: Prepare the Postgre instance using pgAdmin

1. Launch pgAdmin and enter your master password.

2. Under the **Quick Links** section of the Dashboard, there is the option to **Add New Server**. When selected, the **Create - Server** dialog box will open.

3. Under the **General** tab, enter a name for your connection.

    ![Entering the connection name](./media/entering-connection-name.PNG)

4. Navigate to the **Connection** tab. You can pull your instance's host name from the Azure portal (it is available in the resource's overview). As for the **Username**, enter the admin username available on the instance's overview. The **Password** is simply the admin user password you provided during deployment. Press **Save** when you are ready to connect. 

    ![Specifying the database connection](./media/specifying-db-connection.PNG)

5. If the connection succeeded, it should appear under the **Servers** browser dropdown.

    ![Successful connection created](./media/successful-connection.PNG)

6. To create a new database, right-select **Databases** under the connection you just created, and select **Create > Database...**. Name your database **NW** and save. 

7. Now, we will create a new role. The application will reference this role. Under your connection, right-select **Login/Group Roles**. Select **Create > Login/Group Role...**. Name the role **NW**. 

8. Under **Definition**, provide a password. We will use **oracledemo123**. 

9. Under **Privileges**, change the **Can login?** slider to the **Yes** position. 

    ![Defining privileges](./media/nw-defined-privileges.PNG)

10. Finally, under **Membership**, select the **Roles** box and add the **azure_pg_admin** role. Do not select the checkbox next to the role name (this user will not be granting the azure_pg_admin role to others). Select **Save**.

Our configuration in pgAdmin is now complete. 


### Task 6: Create an ora2pg project structure

ora2pg allows database objects to be exported in multiple files, meaning that it is simple to organize and review changes. In this task, we will show you how to create this project structure.  

1. Open a command prompt window and navigate to `C:\ora2pg`.
```
cd C:\ora2pg
```

2. To create a project, we will use the ora2pg command with the --init_project flag. In the example below, our migration project is titled nw_migration.
```
ora2pg --init_project nw_migration
```

In some cases, ora2pg may fail to find its configuration file. In scenarios such as these, you may need to provide the -c flag with the name of the actual configuration file in your ora2pg directory. For instance, **ora2pg.conf.dist** did not exist in my directory, but the file ora2pg_dist.conf was available.

```
ora2pg -c ora2pg_dist.conf --init_project nw_migration
```

3. Verify that the command succeded. There should be a folder with the same name as your migration project in the C:\ora2pg directory.

    ![New project in directory](./media/ora2pg-new-project.png)

4. Enter the project directory. Then, locate **config\ora2pg.conf**. Select the file to open it. If you are asked to select an application to open the file, select Notepad. We will need to collect multiple parameters of the local Oracle database to enter into the configuration file. These parameters are available by entering **lsnrctl status** into the command prompt, as seen in the following.

    ![Database parameters](./media/database-parameter.png)

5. In the **config\ora2pg.conf** file, replace the old values in the file with the correct information. 

    ![Replacing values](./media/ora2pg-conf.png)

6. Confirm that all information entered is correct. The command below should display the version of your local Oracle database. 
```
cd nw_migration
ora2pg -t SHOW_VERSION -c config\ora2pg.conf
```

7. We will also need to populate connection information for our Postgre instance. We will use the role we created in the previous task.

    ![Populating connection information ](./media/ora2pg-conf-pgsql.png)

8. We need to set the schema that we wish to migrate. In this scenario, we are migrating the NW schema.

    ![Setting the appropriate schema to migrate](./media/set-schema.PNG)

### Task 7: Create a migration report

Migration reports tell us the "man-hours" required to fully migrate our application and components. This can be used as a planning tool to tell us how much effort we will need to migrate. Let's take a closer look. 

1. Navigate to the `C:\ora2pg\nw_migration` directory in command prompt.

2. ora2pg provides a reporting functionality which displays information about the objects in the existing schema and the estimated effort required to ensure compatibility with PostgreSQL. The command below creates a report titled **6-23-report.html** in the reports folder (when executed within the `C:\ora2pg\nw_migration` directory). 

```
ora2pg -c config\ora2pg.conf -t SHOW_REPORT --estimate_cost --dump_as_html > reports\6-23-report.html
```
Note that the report displays information for the provided schema--in our case, we placed schema information in `config\ora2pg.conf` before executing the command. 

![Report Schema](./media/report-schema.PNG)

Of particular interest is the migration level. In our case, it is B-5, which implicates code rewriting, since there are multiple stored procedures which must be altered.

![Migration level descrption](./media/report-migration-level.png)

This concludes creating a migration report and preparing the database for migration. Read on to complete the migration to Azure. 

