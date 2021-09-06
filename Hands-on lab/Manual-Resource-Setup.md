![Microsoft Cloud Workshops](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/main/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Migrating Oracle to Azure SQL and PostgreSQL
</div>

<div class="MCWHeader2">
Before the hands-on lab setup guide
</div>

<div class="MCWHeader3">
September 2021
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2019 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

- [Migrating Oracle to Azure SQL and PostgreSQL before the hands-on lab setup guide (manual steps)](#migratingoracletoazuresql-andpostgresql-before-the-hands-on-lab-setup-guide-manual-steps)
  - [Requirements](#requirements)
  - [Before the hands-on lab](#before-the-hands-on-lab)
    - [Task 1: Provision a resource group](#task-1-provision-a-resource-group)
    - [Task 2: Create lab virtual machine](#task-2-create-lab-virtual-machine)
    - [Task 3: Connect to the Lab VM](#task-3-connect-to-the-lab-vm)
    - [Task 4 (Migrate to PostgreSQL): Install pgAdmin on the LabVM](#task-4-migrate-to-postgresql-install-pgadmin-on-the-labvm)
    - [Task 5 (Migrate to Azure SQL Optional Homogenous Migration): Create SQL Server 2008 R2 virtual machine](#task-5-migrate-to-azure-sql-optional-homogenous-migration-create-sql-server-2008-r2-virtual-machine)
    - [Task 6 (Migrate to Azure SQL Optional Homogenous Migration): Connect to the SqlServer2008 VM](#task-6-migrate-to-azure-sql-optional-homogenous-migration-connect-to-the-sqlserver2008-vm)
    - [Task 7 (Migrate to Azure SQL): Provision Azure SQL Database](#task-7-migrate-to-azure-sql-provision-azure-sql-database)
    - [Task 8 (Migrate to Azure SQL Optional Homogenous Migration): Create an Azure SQL Database for the Data Warehouse](#task-8-migrate-to-azure-sql-optional-homogenous-migration-create-an-azure-sql-database-for-the-data-warehouse)
    - [Task 9 (Migrate to Azure SQL Optional Homogenous Migration): Register the Microsoft DataMigration resource provider](#task-9-migrate-to-azure-sql-optional-homogenous-migration-register-the-microsoft-datamigration-resource-provider)
    - [Task 10 (Migrate to Azure SQL Optional Homogenous Migration): Create Azure Database Migration Service for SQL Server](#task-10-migrate-to-azure-sql-optional-homogenous-migration-create-azure-database-migration-service-for-sql-server)
    - [Task 11 (Migrate to PostgreSQL): Provision Azure Database for PostgreSQL](#task-11-migrate-to-postgresql-provision-azure-database-for-postgresql)
    - [Task 12 (Migrate to PostgreSQL): Configure the Azure Database for PostgreSQL Instance](#task-12-migrate-to-postgresql-configure-the-azure-database-for-postgresql-instance)
    - [Task 13 (Migrate to PostgreSQL): Create an App Service Instance](#task-13-migrate-to-postgresql-create-an-app-service-instance)

# Migrating Oracle to Azure SQL and PostgreSQL before the hands-on lab setup guide (manual steps)

## Requirements

- Microsoft Azure subscription must be pay-as-you-go or MSDN.
  - Trial subscriptions will not work.
- A virtual machine configured with:
  - Visual Studio 2019 Community (latest release)

In this lab, there are three major migration paths:

  - Migrating Oracle to Azure SQL
  - Migrating SQL Server 2008 R2 Data Warehouse to Azure SQL (*homogenous* migration)
  - Migrating Oracle to Azure Database for PostgreSQL

Irrespective of which path(s) you complete, follow Tasks 1-3. After that, the Task titles indicate which migration paths they are intended for.

## Before the hands-on lab

Duration: 45 minutes

In the Before the hands-on lab exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all the steps provided in the Before the hands-on lab section to prepare your environment **before attending** the hands-on lab. Failure to do so will significantly impact your ability to complete the lab within the time allowed.

> **Important**: Most Azure resources require unique names. Throughout this lab you will see the word “SUFFIX” as part of resource names. You should replace this with your Microsoft alias, initials, or another value to ensure the resource is uniquely named.

### Task 1: Provision a resource group

In this task, you will create an Azure resource group for the resources used throughout this lab.

1. In the [Azure portal](https://portal.azure.com), select **Resource groups** from the Azure services list.

    ![Resource groups is highlighted in the Azure services list.](media/azure-services-resource-groups.png "Azure services")

2. On the Resource groups blade, select **+Add**.

    ![+Add is highlighted in the toolbar on Resource groups blade.t](media/resource-groups-add.png "Resource groups")

3. Enter the following in the Create an empty resource group blade:

    - **Subscription**: Select the subscription you are using for this hands-on lab.
    - **Resource group**: Enter hands-on-lab-SUFFIX.
    - **Region**: Select the region you would like to use for resources in this hands-on lab. Remember this location so you can use it for the other resources you'll provision throughout this lab.

    ![Add Resource group Resource groups is highlighted in the navigation pane of the Azure portal, +Add is highlighted in the Resource groups blade, and "hands-on-labs" is entered into the Resource group name box on the Create an empty resource group blade.](./media/create-resource-group.png "Create resource group")

4. Select **Review + Create**.

5. On the Review + Create tab, select **Create** to provision the resource group.

### Task 2: Create lab virtual machine

In this task, you will provision a virtual machine (VM) in Azure. The VM image used will have Visual Studio Community 2019 installed.

1. In the [Azure portal](https://portal.azure.com/), select the **Show portal menu** icon and then select **+Create a resource** from the menu.

    ![The Show portal menu icon is highlighted and the portal menu is displayed. Create a resource is highlighted in the portal menu.](media/create-a-resource.png "Create a resource")

2. Enter "visual studio 2019" into the Search the Marketplace box and select Visual Studio Latest.

    !["Visual studio 2019" is entered into the Search the Marketplace box. Visual Studio 2019 Latest is highlighted in the results.](./media/create-resource-visual-studio-2019-latest.png "Visual Studio 2019 Latest")

3. On the Visual Studio 2019 Latest blade, select the Select a software plan drop down list and then select **Visual Studio 2019 Community (latest release) on Windows Server 2019 (x64)** from the list.

    ![The Select a software plan drop down list is expanded and Visual Studio 2019 Community (latest release) on Windows Server 2019 (x64) is highlighted in the list.](media/select-a-software-plan-visual-studio.png "Visual Studio")

4. Select **Create** on the Visual Studio 2019 Latest blade.

5. On the Create a virtual machine Basics tab, set the following configuration:

    - Project Details:

        - **Subscription**: Select the subscription you are using for this hands-on lab.
        - **Resource Group**: Select the hands-on-lab-SUFFIX resource group from the list of existing resource groups.

    - Instance Details:

        - **Virtual machine name**: Enter LabVM.
        - **Region**: Select the region you are using for resources in this hands-on lab.
        - **Availability options**: Select no infrastructure redundancy required.
        - **Image**: Leave Visual Studio 2019 Community (latest release) on Windows Server 2019 (x64) selected.
        - **Size**: Accept the default size, Standard D4s v3. If this VM tier is not the default size, select it. Having a larger memory capacity will accelerate the Oracle database install.

    - Administrator Account:

        - **Username**: demouser
        - **Password**: Password.1!!

    - Inbound Port Rules:

        - **Public inbound ports**: Choose Allow selected ports.
        - **Select inbound ports**: Select RDP (3389) in the list.

        ![Screenshot of the Basics tab, with fields set to the previously mentioned settings.](media/lab-virtual-machine-basics-tab.png "Create a virtual machine Basics tab")

6. Select **Review + create**.

7. On the **Review + create** tab, ensure the Validation passed message is displayed, and then select **Create** to provision the virtual machine.

    ![The Review + create tab is displayed, with a Validation passed message.](media/lab-virtual-machine-review-create-tab.png "Create a virtual machine Review + create tab")

8. It may take 10+ minutes for the virtual machine to complete provisioning. You can move on to the next task while waiting for the lab VM to provision.

### Task 3: Connect to the Lab VM

In this task, you will create an RDP connection to your Lab virtual machine (VM) and disable Internet Explorer Enhanced Security Configuration.

1. In the [Azure portal](https://portal.azure.com), select **Resource groups** from the Azure services list.

    ![Resource groups is highlighted in the Azure services list.](media/azure-services-resource-groups.png "Azure services")

2. On the Resource groups blade, enter your resource group name (hands-on-lab-SUFFIX) into the filter box, and select it from the list.

    ![Resource groups is selected in the Azure navigation pane, "hands" is entered into the filter box, and the "hands-on-lab-SUFFIX" resource group is highlighted.](./media/resource-groups.png "Resource groups list")

3. In the list of resources for your resource group, select the LabVM Virtual Machine.

    ![The list of resources in the hands-on-lab-SUFFIX resource group are displayed, and LabVM is highlighted.](./media/resource-group-resources-labvm.png "LabVM in resource group list")

4. On your Lab VM blade, select **Connect** from the top menu.

    ![The LabVM blade is displayed, with the Connect button highlighted in the top menu.](./media/connect-vm.png "Connect to LabVM")

5. Select **Download RDP file**, then open the downloaded RDP file.

    ![The Connect to virtual machine blade is displayed, and the Download RDP file button is highlighted.](./media/connect-to-virtual-machine.png "Connect to virtual machine")

6. Select **Connect** on the Remote Desktop Connection dialog.

    ![In the Remote Desktop Connection Dialog Box, the Connect button is highlighted.](./media/remote-desktop-connection.png "Remote Desktop Connection dialog")

7. Enter the following credentials when prompted:

    - **Username**: demouser
    - **Password**: Password.1!!

8. Select **Yes** to connect, if prompted that the identity of the remote computer cannot be verified.

    ![In the Remote Desktop Connection dialog box, a warning states that the identity of the remote computer cannot be verified, and asks if you want to continue anyway. At the bottom, the Yes button is circled.](./media/remote-desktop-connection-identity-verification-labvm.png "Remote Desktop Connection dialog")

9. Once logged in, launch the **Server Manager**. This should start automatically, but you can access it via the Start menu if it does not.

    ![The Server Manager tile is circled in the Start Menu.](./media/start-menu-server-manager.png "Server Manager tile in the Start menu")

10. Select **Local Server**, then select **On** next to **IE Enhanced Security Configuration**.

    ![Screenshot of the Server Manager. In the left pane, Local Server is selected. In the right, Properties (For LabVM) pane, the IE Enhanced Security Configuration, which is set to On, is highlighted.](./media/windows-server-manager-ie-enhanced-security-configuration.png "Server Manager")

11. In the Internet Explorer Enhanced Security Configuration dialog, select **Off** under both Administrators and Users, and then select **OK**.

    ![Screenshot of the Internet Explorer Enhanced Security Configuration dialog box, with Administrators set to Off.](./media/internet-explorer-enhanced-security-configuration-dialog.png "Internet Explorer Enhanced Security Configuration dialog box")

12. Close the Server Manager.

### Task 4 (Migrate to PostgreSQL): Install pgAdmin on the LabVM

PgAdmin greatly simplifies database administration and configuration tasks by providing an intuitive GUI. Hence, we will be using it to create a new application user and test the migration.

1. On the LabVM, in Internet Explorer, navigate to <https://www.pgadmin.org/download/pgadmin-4-windows/> to obtain **pgAdmin 4**. At the time of writing, **v5.5** is the most recent version. Select the link to the installer, as shown below.

    ![The screenshot shows the correct version of the pgAdmin utility to be installed.](./media/pgadmin-5.5-install.png "pgAdmin 4 v5.5")

2. Download the **pgadmin4-5.5-x64.exe** file.

3. Once the installer launches, accept all defaults. Complete the installation steps.

4. To open pgAdmin, use the Windows Search utility. Type `pgAdmin`.

   ![The screenshot shows pgAdmin in the Windows Search text box.](./media/2020-07-04-12-45-20.png "Find pgAdmin manually in Windows Search bar")

5. PgAdmin will prompt you to set a password to govern access to database credentials. Enter `oracledemo123`. Confirm your choice. For now, our configuration of pgAdmin is complete.

### Task 5 (Migrate to Azure SQL Optional Homogenous Migration): Create SQL Server 2008 R2 virtual machine

In this task, you will provision another virtual machine (VM) in Azure which will host your "on-premises" instance of SQL Server 2008 R2. The VM will use the SQL Server 2008 R2 SP3 Standard on Windows Server 2008 R2 image.

>**Note**:  An older version of Windows Server is being used because SQL Server 2008 R2 is not supported on Windows Server 2016.

1. In the [Azure portal](https://portal.azure.com/), select the **Show portal menu** icon and then select **+Create a resource** from the menu.

    ![The Show portal menu icon is highlighted and the portal menu is displayed. Create a resource is highlighted in the portal menu.](media/create-a-resource.png "Create a resource")

2. Enter "SQL Server 2008R2SP3 on Windows Server 2008R2" into the Search the Marketplace box and press Enter.

3. On the **SQL Server 2008 R2 SP3 on Windows Server 2008 R2** blade, select **SQL Server R2 SP3 Standard on Windows Server 2008 R2** for the software plan and then select **Create**.

    ![The SQL Server 2008 R2 SP3 on Windows Server 2008 R2 blade is displayed with the standard edition selected for the software plan, and the Create button highlighted.](media/create-resource-sql-server-2008-r2.png "Create SQL Server 2008 R2 Resource")

4. On the Create a virtual machine Basics tab, set the following configuration:

   - Project Details:

     - **Subscription**: Select the subscription you are using for this hands-on lab.
     - **Resource Group**: Select the hands-on-lab-SUFFIX resource group from the list of existing resource groups.

   - Instance Details:

     - **Virtual machine name**: Enter SqlServer2008.
     - **Region**: Select the region you are using for resources in this hands-on lab.
     - **Availability options**: Select no infrastructure redundancy required.
     - **Image**: Leave SQL Server 2008 R2 SP3 Standard on Windows Server 2008 R2 selected.
     - **Size**: Select Standard D4s v3. You can accept the default larger tier, but to minimize costs, we recommend Standard D4s v3.

   - Administrator Account:

     - **Username**: demouser
     - **Password**: Password.1!!

   - Inbound Port Rules:

     - **Public inbound ports**: Choose Allow selected ports.
     - **Select inbound ports**: Select RDP (3389) in the list.

    ![Screenshot of the Basics tab, with fields set to the previously mentioned settings.](media/sql-server-2008-r2-vm-basics-tab.png "Create a virtual machine Basics tab")

5. Select the **SQL Server settings** tab from the top menu. The default values will be used for Disks, Networking, Management and Advanced, so you don't need to do anything on those tabs.

    ![The SQL Server settings tab is highlighted and selected in the Create a virtual machine configuration tabs list.](media/sql-2017-create-vm-tabs.png "Create a virtual machine configuration tabs")

6. On the **SQL Server settings** tab, set the following properties:

   - Security & Networking:

     - **SQL connectivity**: Select Public (Internet).
     - **Port**: Leave set to 1433.

     > **Note**: SQL Connectivity is being set to public for this hands-on lab to simplify access during the lab. In a production environment, you would want to limit connectivity to only those IP addresses that require access.

   - SQL Authentication:

     - **SQL Authentication**: Select Enable.
     - **Login name**: demouser
     - **Password**: Password.1!!

     ![The previously specified values are entered into the SQL Server Settings blade.](media/sql-server-2017-create-vm-sql-settings.png "SQL Server Settings")

7. Select **Review + create** to review the VM configuration.

8. On the **Review + create** tab, ensure the Validation passed message is displayed, and then select **Create** to provision the virtual machine.

    ![The Review + create tab is displayed, with a Validation passed message.](media/sql-server-2008-r2-vm-review-create-tab.png "Create a virtual machine Review + create tab")

9. It may take 10+ minutes for the virtual machine to complete provisioning. You can move on to the next task while waiting for the SqlServer2008 VM to provision.

### Task 6 (Migrate to Azure SQL Optional Homogenous Migration): Connect to the SqlServer2008 VM

In this task, you will create an RDP connection to the SqlServer2008 VM and disable Internet Explorer Enhanced Security Configuration.

1. In the [Azure portal](https://portal.azure.com), select **Resource groups** from the Azure services list.

    ![Resource groups is highlighted in the Azure services list.](media/azure-services-resource-groups.png "Azure services")

2. On the Resource groups blade, enter your resource group name (hands-on-lab-SUFFIX) into the filter box, and select it from the list.

    ![Resource groups is selected in the Azure navigation pane, "hands" is entered into the filter box, and the "hands-on-lab-SUFFIX" resource group is highlighted.](./media/resource-groups.png "Resource groups list")

3. In the list of resources for your resource group, select the SqlServer2008 Virtual Machine.

    ![The list of resources in the hands-on-lab-SUFFIX resource group are displayed, and SqlServer2008 is highlighted.](media/resource-group-resources-sqlserver2008r2.png "SqlServer2008 VM in resource group list")

4. On the SqlServer2008 blade in the [Azure portal](https://portal.azure.com), select **Overview** from the left-hand menu, and then select **Connect** from the top menu.

    ![The SqlServer2008 blade is displayed, with the Connect button highlighted in the top menu.](media/connect-sqlserver2008r2.png "Connect to SqlServer2008")

5. Select **Download RDP file**, then open the downloaded RDP file.

    ![The Connect to virtual machine blade is displayed, and the Download RDP file button is highlighted.](./media/connect-to-virtual-machine.png "Connect to virtual machine")

6. Select **Connect** on the Remote Desktop Connection dialog.

    ![In the Remote Desktop Connection Dialog Box, the Connect button is highlighted.](./media/remote-desktop-connection.png "Remote Desktop Connection dialog")

7. Enter the following credentials when prompted:

    - **Username**: demouser
    - **Password**: Password.1!!

8. Select **Yes** to connect, if prompted that the identity of the remote computer cannot be verified.

    ![In the Remote Desktop Connection dialog box, a warning states that the identity of the remote computer cannot be verified, and asks if you want to continue anyway. At the bottom, the Yes button is circled.](./media/remote-desktop-connection-identity-verification-sqlserver2008r2.png "Remote Desktop Connection dialog")

9. Once logged in, launch the **Server Manager**. This should open automatically, but you can access it via the task bar or Start menu if it does not.

    ![The Server Manager tile is circled in the Start Menu's Administrative Tools menu, and in the task bar.](media/windows-server2008r2-start-menu.png "Windows Server 2008 R2 Start Menu")

10. In Server Manager, select **Configure IE ESC** in the Security Information section of the Server Summary.

    ![Configure IE ESC is highlighted in the Server Manager.](media/windows-server-2008r2-server-manager.png "Windows Server 2008 R2 Server Manager")

11. On the Internet Explorer Enhanced Security Configuration dialog, select **Off** under both Administrators and Users, and then select **OK**.

    ![Internet Explorer Enhanced Security Configuration dialog, with Off highlighted under both Administrators and Users.](media/windows-server-2008-ie-esc.png "Internet Explorer Enhanced Security Configuration dialog")

12. Close the Server Manager.

### Task 7 (Migrate to Azure SQL): Provision Azure SQL Database

In this task, you will create an Azure SQL Database, which will serve as the target database for migration of the on-premises Oracle database into the cloud. This is for the OLTP database migration.

1. In the [Azure portal](https://portal.azure.com/), select the **Show portal menu** icon and then select **+Create a resource** from the menu.

    ![The Show portal menu icon is highlighted and the portal menu is displayed. Create a resource is highlighted in the portal menu.](media/create-a-resource.png "Create a resource")

2. Enter "sql database" into the Search the Marketplace box, select **SQL Database** from the results, and then select **Create**.

    ![+Create a resource is selected in the Azure navigation pane, and "sql database" is entered into the Search the Marketplace box. SQL Database is selected in the results.](media/create-resource-azure-sql-database.png "Create SQL Server")

3. On the SQL Database Basics tab, enter the following:

    - Project Details:

      - **Subscription**: Select the subscription you are using for this hands-on lab.
      - **Resource Group**: Select the hands-on-lab-SUFFIX resource group from the list of existing resource groups.

    - Database Details:

      - **Database name**: Enter Northwind
      - **Server**: Select Create new, and then on the New server blade, enter the following:
        - **Server name**: Enter a unique name, such as northwind-server-SUFFIX.
        - **Server admin login**: demouser
        - **Password**: Password.1!!
        - **Location**: Select the location you are using for resources in this hands-on lab.
        - Select **OK**.
      - **Want to use SQL elastic pool?**: Select **No**.

      ![The Basic tab with the values specified above entered into the appropriate fields is displayed.](media/azure-sql-database-create-basic-tab-oltp.png "Create SQL Database Basic tab")

      - **Compute + storage**: Select **Configure database**.

      ![Configure database is highlighted under Compute + storage.](media/azure-sql-database-create-compute-storage.png "Compute + storage")

      - On the Compute + storage blade, expand the **Service tier** dropdown. Select **Premium** below **DTU-based purchasing model**. The default configuration will be `125 DTUs` and `500 GB` storage. Accept the default value for **Read scale-out**. Select **Apply**.

      ![The Configure pricing tier for SQL Server is displayed, with Premium selected and highlighted.](media/select-premium-service-tier.png "SQL Pricing tier configuration")

4. Select **Next: Networking**.

5. On the Networking tab, set the following configuration:

    - **Connectivity method**: Select **Public endpoint**.
    - **Allow Azure services and resources to access this server**: Select **Yes**.
    - **Add current client IP address**: Select **No**. If you would like to be able to access the database from your local machine (not required for this lab), you can set this to Yes.

    ![The values specified above are entered into the Networking tab.](media/azure-sql-database-networking-tab.png "Create SQL Database Networking tab")

6. Select **Review + Create**.

7. On the Review + Create tab, select **Create** to provision the Azure SQL Database.

    > **Note**: The [Azure SQL Database firewall](https://docs.microsoft.com/azure/sql-database/sql-database-firewall-configure) prevents external applications and tools from connecting to the server or any database on the server unless a firewall rule is created to open the firewall for the specific IP address. When creating the new server above, the **Allow azure services to access server** setting was allowed, which allows any services using an Azure IP address to access this server and databases, so there is no need to create a specific firewall rule for this hands-on lab. To access the SQL server from an on-premises computer or application, you need to [create a server level firewall rule](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-portal#create-a-server-level-firewall-rule) to allow the specific IP addresses to access the server.

### Task 8 (Migrate to Azure SQL Optional Homogenous Migration): Create an Azure SQL Database for the Data Warehouse

If you are completing the optional homogenous migration, complete this task to create the landing zone for the Data Warehouse migration.

1. Repeat the steps in the previous task, creating a new database called `WideWorldImportersDW`. **However, do not create a new server! Select the existing server that you created in the last Task.**

    ![Creating the WideWorldImportersDW database on the existing northwind-server-SUFFIX Azure SQL server.](./media/creating-sql-database-homogenous-migration.png "Creating data warehouse Azure SQL database on the existing server")

An Azure SQL Database *server* is just a management entity, akin to how multiple SQL Server databases reside on an individual SQL Server instance.

### Task 9 (Migrate to Azure SQL Optional Homogenous Migration): Register the Microsoft DataMigration resource provider

In this task, you will register the `Microsoft.DataMigration` resource provider with your subscription in Azure.

1. In the [Azure portal](https://portal.azure.com/), navigate to the Home page and then select **Subscriptions** from the Navigate list found midway down the page.

    ![Subscriptions is highlighted in the Navigate menu.](media/azure-navigate-subscriptions.png "Navigate menu")

2. Select the subscription you are using for this hands-on lab from the list, select **Resource providers**, enter "migration" into the filter box, and then select **Register** next to **Microsoft.DataMigration**.

    ![The Subscription blade is displayed, with Resource providers selected and highlighted under Settings. On the Resource providers blade, migration is entered into the filter box, and Register is highlighted next to Microsoft.DataMigration.](media/azure-portal-subscriptions-resource-providers-register-microsoft-datamigration.png "Resource provider registration")

### Task 10 (Migrate to Azure SQL Optional Homogenous Migration): Create Azure Database Migration Service for SQL Server

In this task, you will provision an instance of the Azure Database Migration Service (DMS).

1. In the [Azure portal](https://portal.azure.com/), select the **Show portal menu** icon and then select **+Create a resource** from the menu.

    ![The Show portal menu icon is highlighted and the portal menu is displayed. Create a resource is highlighted in the portal menu.](media/create-a-resource.png "Create a resource")

2. Enter "database migration" into the Search the Marketplace box, select **Azure Database Migration Service** from the results, and select **Create**.

    !["Database migration" is entered into the Search the Marketplace box. Azure Database Migration Service is selected in the results.](media/create-resource-azure-database-migration-service.png "Create Azure Database Migration Service")

3. On the Create Migration Service blade, enter the following:

    - **Subscription**: Select the subscription you are using for this hands-on lab.
    - **Resource Group**: Select the hands-on-lab-SUFFIX resource group from the list of existing resource groups.
    - **Migration service name**: Enter **wwi-dms-SUFFIX**.
    - **Location**: Select the location you are using for resources in this hands-on lab.
    - **Pricing tier**: Select Standard: 1 vCores.

    > **Note**: If you see the message `Your subscription doesn't have proper access to Microsoft.DataMigration`, refresh the browser window before proceeding. If the message persists, verify you successfully registered the resource provider, and then you can safely ignore this message.

   ![The Create Migration Service blade is displayed, with the values specified above entered into the appropriate fields.](media/create-migration-service.png "Create Migration Service")

4. Select **Next: Networking**.

5. On the Network tab, select the **hands-on-lab-SUFFIX-vnet/default** virtual network. This will place the DMS instance into the same VNet as your SQL Server VM and Lab VM.

    ![The hands-on-lab-SUFFIX-vnet/default is selected in the list of available virtual networks.](media/create-migration-service-networking-tab.png "Create migration service")

6. Select **Review + create**.
  
7. Select **Create**.

>**Note**: It can take 15 minutes to deploy the Azure Data Migration Service.

### Task 11 (Migrate to PostgreSQL): Provision Azure Database for PostgreSQL

If you are completing the PostgreSQL migration lab, in this Task, you will prepare the landing zone.

1. Navigate to the **New** page accessed by selecting **+ Create a resource**. Then, navigate to **Databases** under the **Azure Marketplace** section. Select **Azure Database for PostgreSQL**.

    ![Navigating Azure Marketplace to Azure Database for Postgre SQL, which has been highlighted.](./media/creating-new-postgresql-db.png "Azure Database for Postgre SQL")

2. There are four deployment options. For our simple transactional application, we will be utilizing a single server for our database.

    ![Screenshot of choosing the correct single server option.](./media/single-server-selection.png "Single server")

3. Create a new Azure Database for PostgreSQL resource. Use the following configuration values:

   - **Resource group**: Same as the other lab resources
   - **Server name**: Enter a unique server name, such as `northwind-ora2pg-SUFFIX`
   - **Data source**: Select `None`
   - **Location** Select the same region as your other lab resources
   - **Version**: 11
   - **Compute + storage**: `General Purpose (4 vCores, 100 GB storage)`
   - **Administrator username**: `solldba`
   - **Password**: Provide a secure password

    Select the **Review + create** button once you are ready.

    ![Configuring the instance details.](./media/postgresql-provision-parameters.png "Project Details window with pertinent details")

4. Select **Create** to start the deployment. Once the deployment completes, we will move on to configuring the instance.

### Task 12 (Migrate to PostgreSQL): Configure the Azure Database for PostgreSQL Instance

In this task, we will be modifying the PostgreSQL instance to fit our needs.

1. Storage Auto-growth is a feature in which Azure will add more storage automatically when required. We do not need it for our purposes so we will need to disable it. To do this, locate the PostgreSQL instance you created. Under the **Settings** tab, select **Pricing tier**.

    ![Changing the pricing tier in PostGre SQL instance.](./media/changing-tier.png "Pricing tier")

2. Find the **Storage Auto-growth** switch, and disable the feature. Select **OK** at the bottom of the page to save your change.

    ![Disabling storage auto-growth feature.](./media/disabling-auto-grow.png  "Storage auto-growth toggled to no")

3. Now, we need to implement firewall rules for the PostgreSQL database so we can access it. Locate the **Connection security** selector under the **Settings** tab.

    ![Configuring the Connection Security settings for the database.](./media/entering-connection-settings.png "Connection security highlighted")

4. We will add a network access rule. Since we are storing insecure test data, select **Allow access to Azure services**. This means that all public IP addresses associated with Azure have network access to the PostgreSQL instance, even if they are located in other subscriptions or tenants.

    ![Allow Azure resources access to the PostgreSQL instance.](./media/allow-azure-services-network-access.png "Grant Azure resources PostgreSQL network access")

    >**Note**: Do not use this type of rule for databases with sensitive data or in a production environment. You are allowing access from any Azure IP address.

### Task 13 (Migrate to PostgreSQL): Create an App Service Instance

As part of the PostgreSQL lab, you will host the modified application in Azure App Service. You will provision a Web App and an App Service Plan in this Task.

1. At the **New** page, navigate to **Web** under **Azure Marketplace**. Select **Web App**.

    ![Navigating to the Web App option on Azure Marketplace.](./media/creating-web-app.png "Web app option highlighted on Marketplace")

2. Configure the following details for the App Service instance.

    - **Subscription**: Use the lab Azure subscription
    - **Resource Group**: Select the hands-on-lab-SUFFIX resource group
    - **Name**: Use `northwind-ora2pg-app-SUFFIX`
    - **Publish**: Select `Code`
    - **Runtime stack**: Select `.NET 5`
    - **Operating system**: Select `Linux`
    - **Region**: If available, choose the region that you are using for the lab resources

    ![Configure the App Service instance with the parameters listed above.](./media/app-service-details.png "App Service instance parameters")

3. Scroll to the **App Service Plan** section.

    - **Linux Plan (REGION)**: Select **Create new**
      - Enter a unique name for the App Service Plan, like `northwind-ora2pg-plan-SUFFIX`
    - **Sku and size**: Select **Change size**. Select **Standard S1**.
      - Note that you may need to expand the **Production** pricing tiers to locate Standard S1

        ![Select the Standard S1 pricing tier in the Spec Picker.](./media/standard-pricing-tier.png "Select the Standard S1 pricing tier")

    ![Finalized App Service Plan details.](./media/plan-details.png "App Service Plan details")

4. Select **Review + create**. Once validation passes, select **Create**.

You should follow all steps provided *before* performing the Hands-on lab.
