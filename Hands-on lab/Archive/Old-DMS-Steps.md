### Task 2: Use Azure Database Migration Service to migrate table data

We will be using Azure Database Migration Service to populate our tables with data. In the Before-the-HOL-document, you created the service itself. However, to perform the actual migration, you will need to create a Migration Project, which we will do here. Then, we will then execute the project.

We will first need to give DMS access to our local Oracle database. This will require us to create an inbound port rule for the VM's Network Security Group, and an inbound port rule for the local Windows Defender Firewall.

1. Navigate to the **hands-on-lab-SUFFIX** resource group and select **LabVM-nsg**.

2. Under **Settings**, select **Inbound security rules**.

    ![Screenshot showing the first step to add an inbound security rule.](./media/create-inbound-rule.png "First step to allow public access to the Oracle instance")

3. Select **+ Add**. Enter the parameters below, and select **Add** at the bottom of the card once you are ready.

    - **Source:** Any.
    - **Source port ranges:** *
    - **Destination:** Any
    - **Destination port ranges:** 1521
    - **Protocol:** TCP
    - **Action:** Allow
    - **Priority:** Accept the default value.
    - **Name:** OracleDB

    ![Screenshot showing the configuration of an inbound traffic NSG rule.](./media/inbound-rule.PNG "NSG rule parameters for Oracle")

4. Navigate to your Lab VM. Enter **Windows Key + R** (or search for **Run**) and type **WF.msc**. Select **OK** once you are ready.

    ![Screenshot showing the process of launching Windows Firewall.](./media/open-windows-firewall.PNG "Launching Windows Firewall as an Administrator")

5. Under **Inbound Rules**, select **New Rule...**.

    ![Screenshot showing the first step to add an inbound security rule to the local Windows Firewall.](./media/new-wf-rule.png "Local Firewall traffic rule")

6. Create a **Port** rule.

    ![Screenshot showing how to permit inbound traffic destined for a certain port.](./media/create-port-rule.PNG "Creating a rule for a port")

7. Open **TCP** port **1521**.

    ![Screenshot showing the process of choosing the correct port.](./media/open-port-1521.PNG "Opening port 1521")

8. Allow the connection. Apply the rule for the **Domain**, **Private**, and **Public** network scenarios. Name the rule **Oracle DB Access DMS**.

9. We will need to enable **TLS 1.0/1.1** support in DMS. Navigate to your Database Migration Service *instance*, and under **Settings**, select **Configuration**.

    ![Screenshot showing a method to access the DMS instance's security configuration.](./media/entering-dms-configuration.PNG "Accessing the DMS instance's security configuration")

10. Under **Enable connections using TLS 1.0 and 1.1 security protocol**, verify that the slider is set to the **Enabled** position. **Save** the change once you are ready.

    >**Note**: A more secure method would be to enable TLS 1.2 on the Oracle instance. Refer to <https://docs.oracle.com/cd/E77767_01/doc.31/E69159/index.htm?toc.htm?209670.htm> to create and populate the correct registry keys.

    ![Screenshot indicating that TLS 1.0 and 1.1 are in use.](./media/enable-tls-1.0.PNG "Verifying that the correct encryption protocol is being used")

11. Navigate back to your Lab VM. Just as we set environment variables when installing ora2pg in Exercise 3, Task 4, set the following values as **System variables**:

    - **ORACLE_HOME**: C:\oraclexe\app\oracle\product\11.2.0\server
    - **ORACLE_SID**: XE

    ![Screenshot demonstrating the ORACLE_HOME and ORACLE_SID environment variables.](./media/home-and-sid.png "Configuration of ORACLE_HOME and ORACLE_SID")

12. Open a new command prompt window. We will access our database with the *sysdba* role.

    ```cmd
    sqlplus / as sysdba
    ```

    >**Note**: If you get the error `ORA-12560: TNS:protocol adapter error` you may have forgotten to close your command prompt after adding the new environment variables. Close your open command prompt and enter the command in a new one.

13. The first feature we will enable is support for Archive Redo Logs. To do this, first shut down the instance.

    ```sql
    SHUTDOWN IMMEDIATE;
    ```

14. When the command returns *ORACLE instance shut down*, enter the following.

    ```sql
    STARTUP MOUNT;
    ```

15. Enable log archiving mode.

    ```sql
    ALTER DATABASE ARCHIVELOG;
    ```

16. Open the database--the shutdown command of step 13 limits additional connections from forming.

    ```sql
    ALTER DATABASE OPEN;
    ```

    >**Note**: Run *SELECT log_mode FROM v$database;* If the previous steps were successful, you should see **ARCHIVELOG** as the result.

17. Now, we must enable supplemental logging at the database level. Enter the following command to do so. Since we are doing an online migration, supplemental logging captures changes that have occurred in the source database after migration began.

    ```sql
    ALTER DATABASE ADD SUPPLEMENTAL LOG DATA;
    ```

18. We will also need to enable supplemental logging for row identification information, like a table's primary key (a ROWID cannot be used to perform DML operations on the target). To perform this, enter the following.

    ```sql
    ALTER DATABASE ADD SUPPLEMENTAL LOG DATA (PRIMARY KEY, UNIQUE) COLUMNS;
    ```

    >**Note**: There are no tables in our database without a primary key. However, if one of your tables does not have a primary key, run *ALTER TABLE [TABLE NAME] ADD SUPPLEMENTAL LOG DATA (ALL) COLUMNS;*

19. Open `C:\oraclexe\app\oracle\product\11.2.0\server\network\ADMIN\listener.ora` in Notepad. Add the following to the **SID_LIST_LISTENER** list:

    ```text
    (SID_DESC =
      (SID_NAME = XE)
      (ORACLE_HOME = C:\oraclexe\app\oracle\product\11.2.0\server)
    )
    ```

    This is how the list should appear.

    ![Screenshot showing the correct SID_LIST_LISTENER list, with XE SID added.](./media/sid-list.png "Adding the XE SID to listener.ora")

20. After making this change, open a command prompt window and enter the following command to restart the listener service.

    ```cmd
    lsnrctl reload
    ```

21. Open another command prompt and enter **sqlplus**. When prompted for your credentials, enter **NW** as the **user-name** and **oracledemo123** as the **password**. Then, execute each of the commands below, line-by-line. We first create a copy of the EmployeeTerritories table (temp_EmployeeTerritories), remove data from the existing table, change the type of the EmployeeID column of the EmployeeTerritories table to *number(10)*, copy data back into the table, and drop the table copy.

    ```sql
    CREATE TABLE temp_EmployeeTerritories as SELECT * FROM EmployeeTerritories;
    DELETE FROM EmployeeTerritories;
    ALTER TABLE EmployeeTerritories MODIFY EmployeeID number(10);
    INSERT INTO EmployeeTerritories SELECT * FROM temp_EmployeeTerritories;
    DROP TABLE temp_EmployeeTerritories;
    COMMIT;
    ```

22. We will also need to make datatype changes on tables in the PostgreSQL database. To do so, first connect to the instance.

    ```cmd
    psql -U NW@[DB Name] -h [DB Name].postgres.database.azure.com -d NW
    ```

    Then, enter each of the commands below. They will change the type of region.regionid and territories.regionid to *double precision*. Changing types is significantly easier because the region and territories tables do not contain data yet.

    ```sql
    ALTER TABLE region ALTER COLUMN regionid TYPE double precision;
    ALTER TABLE territories ALTER COLUMN regionid TYPE double precision;
    ```

23. Navigate to your wwi-dms-SUFFIX Database Migration Service. Then, in the toolbar, select **+ New Migration Project**.

    ![Screenshot showing the creation of a new Migration Service project.](./media/create-new-migration-project.png "Using toolbar to create a new Migration Service project")

24. Enter the following information about the project. Once you have entered everything, select **Create**.

    - **Project name:** OnPremToAzurePostgreSql
    - **Source server type:** Oracle
    - **Target server type:** Azure Database for PostgreSQL
    - **Choose type of activity:** Select Create project only and select **Save**.

    ![Screenshot showing the process of configuring a new project.](./media/migration-project.PNG "Initializing project parameters")

   >**Note**: Using Azure Database Migration Service to perform an online migration requires creating an instance based on the **Premium** pricing tier.

25. Navigate to your resource group and select the migration project (OnPremToAzurePostgreSql). Select **+ New Activity** and **Online data migration [preview]**.

26. On the **Add Source Details** page, enter the following parameters. Then, select **Save**.

    - **Mode:** Verify that it is **Standard mode**.
    - **Source server name:** Enter the IP address of your Lab VM.
    - **Server port:** Enter **1521**.
    - **Oracle SID:** Enter **XE**.
    - **Username:** Enter **NW**.
    - **Password:** Type **oracledemo123**.

    >**Note**: If Azure fails to connect to your Oracle database, run *lsnrctl status* in command prompt. There should be a listener identified by your VM's hostname (LabVM).

    ![Screenshot showing listener endpoints and the host of each endpoint.](./media/listening-endpoint-debug.png "Verifying endpoint hosts")

27. The Data Migration Service will need the OCI driver to proceed. To do this, navigate to <https://www.oracle.com/database/technologies/instant-client/winx64-64-downloads.html>, and download the `instantclient-basiclite-windows.x64-12.2.0.1.0.zip` package. Verify that it is in your `Downloads` directory.

    >**Note**: During the ora2pg installation, you downloaded the Basic version of this package. However, we are downloading the Basic Light Package because it is what Microsoft recommends we use.

28. Right-click your Downloads directory, and select **Properties**. Under **Sharing**, select **Share...** When the **Network access** menu opens, simply select **Share** again.

    ![Screenshot showing the process of enabling network sharing for the Downloads directory.](./media/network-sharing-downloads.png "Enable network sharing of the Downloads directory")

29. We will need to add an inbound port rule for TCP port 445, which is what SMB uses. Navigate to your Lab VM's NSG. **+ Add** an inbound port rule with the following parameters:

    - **Source:** Any
    - **Source port ranges:** *
    - **Destination:** Any
    - **Destination port ranges:** 445
    - **Protocol:** TCP
    - **Action:** Allow
    - **Priority:** Accept the default value.
    - **Name:** SMB

    ![Screenshot showing the process of configuring an NSG rule to allow public access to an SMB share.](./media/inbound-rule-smb.PNG "Opening port 445 for SMB access in the Lab VM NSG")

30. On the **Driver install detail** page of the migration project, enter the following parameters. Then, select **Save**.

    - **OCI driver path:** `\\[LAB VM IP ADDRESS]\Users\demouser\Downloads\instantclient-basiclite-windows.x64-12.2.0.1.0.zip`
    - **Username:** LabVM\demouser
    - **Password:** Password.1!!

31. You will arrive at the **Migration target details** page. Enter the following parameters. Select **Save** once you are ready to continue.

    - **Target server name:** [DB Name].postgres.database.azure.com.
    - **Default Database:** postgres
    - **Username:** NW@[DB Name]
    - **Password:** The password of your NW user.

32. At the **Map to target databases** page, verify that the source **NW** schema is being migrated to the **public** schema of the **NW** target database.

    ![Screenshot showing the selection of the correct target database and schema.](./media/dms-db-and-schema.PNG "Choosing NW as the target database and public as the target schema")

33. You are close. At the **Migration settings** page, expand **NW > Tables 13 of 13**. Verify that each table is correctly mapped to its equivalent in the PostgreSQL database. Select **Save**.

    ![Screenshot showing the default mapping of tables between the source and target databases.](./media/source-target-mapping.PNG "Verifying the correct mapping of source tables to target tables")

34. Finally, at the **Migration summary** page, enter a name for the migration; we will be using **NWOracleToPostgreSQL**--and select **Run migration**.

35. You will be redirected to the running migration activity. Assuming you did everything correctly, you will see **Ready to cutover** under **Migration details** after some time. Note that you may need to **Refresh** multiple times to observe the effect.

    ![Screenshot showing the transition to the cutover stage of migration.](./media/ready-for-cutover.png "NW schema ready for cutover")

36. Select **NW** below **Schema name**. Then, select **Start Cutover** at the top left corner of the page.

    ![Screenshot showing the process of launching a cutover for a particular schema.](./media/start-cutover.PNG "NW schema cutover start")

37. **Confirm** the pending changes and **Apply** the cutover. Wait for the cutover to finish.

    ![Screenshot showing the process of applying a cutover and acknowledging the conditions that must be met prior.](./media/apply-cutover.PNG "Applying cutover")

38. Congratulations! You have successfully migrated data into the new database. If you return to the **NW** schema page, and select the **Full load** tab, you will see a count of the number of rows exported from a table in the Oracle database to its equivalent in the PostgreSQL database. This is our result for the first ten migrated tables.

    ![Screenshot verifying that the correct number of tables were exported.](./media/migrated-row-counts.png "Number of rows copied from source to target")