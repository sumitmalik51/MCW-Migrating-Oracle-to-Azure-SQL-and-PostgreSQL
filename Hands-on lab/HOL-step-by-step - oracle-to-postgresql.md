[Exercise 1: Setup Oracle 11g Express Edition](#exercise-4-setup-oracle-11g-express-edition)
    - [Task 1: Install Oracle XE](#task-1-install-oracle-xe)
    - [Task 2: Install Oracle Data Access components](#task-2-install-oracle-data-access-components)
    - [Task 3: Install dbForge Fusion tool](#task-4-install-dbforge-fusion-tool)
    - [Task 4: Create the Northwind database in Oracle 11g XE](#task-5-create-the-northwind-database-in-oracle-11g-xe)
    - [Task 5: Configure the Starter Application to use Oracle](#task-6-configure-the-starter-application-to-use-oracle)

[Assesment tasks]

[Exercise 3: Migrate the Oracle database to PostgreSQL]
    - [Task 1: Create Azure resources and configure the Key Vault]
    - [Task 2: Configure the PostgreSQL server instance]
    - [Task 3: Create a VM migration server (Lab VM)]
    - [Task 4: Install pgAdmin utilities on the migration server]
    - [Task 5: Install ora2pg]
    - [Task 6: Prepare the Postgre instance using pgAdmin, create an ora2pg project structure on the migration server, and migrate the database table schema]
    - [Task 7: Migrate schema objects using ora2pg]
    - [Task 8: Migrate the remaining database objects to Postgre and alter procedures]
    - [Task 9: Create new Entity Data Models and update the application on the Lab VM]

[Exercise 4: Migrate the Application]
    - [Task 1: Install Visual Studio extensions]
    - [Task 2: Deploy to Azure Database for PostGre SQL]

[After the hands-on lab](#after-the-hands-on-lab)
    - [Task 1: Delete the resource group](#task-1-delete-the-resource-group)


### Exercise 1: Setup Oracle 11g Express Edition
Duration: 45 minutes

In this exercise, you will install Oracle XE on your Lab VM, load a sample database supporting an application, and then migrate the database to Azure Database for PostGre SQL. 

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

8. Right-click `setup.exe`, and select **Run as administrator**.

    ![In File Explorer, setup.exe is selected, and Run as administrator is highlighted in the shortcut menu.](./media/windows-file-menu-run-as-administrator.png "Run setup.exe as an administrator")

9. Select **Next** to step through each screen of the installer, accepting the license agreement and default values, until you get to the **Specify Database Passwords** screen.

10. On the **Specify Database Passwords** screen, set the password to **Password.1!!**, and select **Next**.

    ![The above credentials are entered on the Specify Database Passwords screen.](./media/oracle-11g-install-passwords.png "Set the password")

11. On the Summary screen, take note of the ports being assigned, and select **Install**.

    ![Several of the ports being assigned are highlighted on the Summary screen.](./media/oracle-11g-install-summary.png "Note the ports being assigned")

12. Select **Finish** on the final dialog to compete the installation.


