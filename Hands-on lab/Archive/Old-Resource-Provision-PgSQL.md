### Task 1: Create Azure Resources

We need to create a PostgreSQL instance and an App Service to host our application. Visual Studio integrates well with Microsoft Azure, simplifying application deployment.

1. Just as you configured resources in **Before the HOL**, you will need to navigate to the **New** page accessed by selecting **+ Create a resource**. Then, navigate to **Databases** under the **Azure Marketplace** section. Select **Azure Database for PostgreSQL**.

    ![Navigating Azure Marketplace to Azure Database for Postgre SQL, which has been highlighted.](./media/creating-new-postgresql-db.png "Azure Database for Postgre SQL")

2. There are two deployment options: **Single Server** and **Hyperscale (Citus)**. Single Server is best suited for traditional transactional workloads whereas Hyperscale is best suited for ultra-high-performance, multi-tenant applications. For our simple application, we will be utilizing a single server for our database.

    ![Screenshot of choosing the correct single server option.](./media/single-server-selection.PNG "Single server")

3. Create a new Azure Database for PostgreSQL resource. Use the following configuration values:

   - **Resource group**: (same as Lab VM)
   - **Server name**: Enter a unique server name.
   - **Version**: 11
   - **Administrator username**: solldba
   - **Password**: (secure password)

    Select **Review + create** button once you are ready.

    ![Configuring the instance details.](./media/postgresql-config.PNG "Project Details window with pertinent details")

4. Select **Create** to start the deployment. Once the deployment completes, we will move on to creating an Azure Web App.

5. At the **New** page, navigate to **Web** under **Azure Marketplace**. Select **Web App**.

    ![Navigating to the Web App option on Azure Marketplace.](./media/creating-web-app.png "Web app option highlighted on Marketplace")

6. Create a new app in your hands-on-lab-SUFFIX resource group. Configure it with the following parameters. Keep all other settings at their default values. Select **Review + create** when you are ready.

    - **Name**: Configure a unique app name (the name you choose will form part of your app's URL).
    - **Runtime stack**: ASP.NET V4.8
    - **Region**: Must support all necessary resources.

    ![Configuring the web app details.](./media/web-app-configuration-asp-48.PNG "Project Details window with pertinent details")

7. Select **Create** after reviewing parameters. Once the deployment finishes, navigate to the **App Service** resource you created. Select **Get publish profile** under the resource's **Overview** page.

    ![Downloading the publish profile.](./media/get-app-publish-file.png "Get publish profile")

8. Save the file and move it to `C:\handsonlab\MCW-Migrating-Oracle-to-Azure-SQL-and-PostgreSQL\Hands-on lab\lab-files\starter-project`. Later, we will need this file to import into Visual Studio for deployment.

9. We need to ensure that Azure supports the version of .NET used in the solution. We will do this by changing the target framework on the solution to **.NET Framework 4.8**. Open the NorthwindMVC solution in Visual Studio. Right-click the NorthwindMVC project (not the solution) and select **Properties**. Find the **Target framework:** dropdown menu and select **.NET Framework 4.8**.

    ![Window to change the target framework of the solution to .NET Framework 4.8](./media/changing-target-framework-4.8.PNG "Target frametwork: .NET Framework 4.8")

    >**Note**: To support ASP.NET 4.8, you may need to install [.NET framework 4.8](https://dotnet.microsoft.com/download/visual-studio-sdks?utm_source=getdotnetsdk&utm_medium=referral). Agree to the license terms and install. Note that you will need to restart your VM afterwards. 

### Task 1: Configure the PostgreSQL server instance

In this task, we will be modifying the PostgreSQL instance to fit our needs.

1. Storage Auto-growth is a feature in which Azure will add more storage automatically when required. We do not need it for our purposes so we will need to disable it. To do this, locate the PostgreSQL instance you created. Under the **Settings** tab, select **Pricing tier**.

    ![Changing the pricing tier in PostGre SQL instance.](./media/changing-tier.PNG "Pricing tier")

2. Find the **Storage Auto-growth** switch, and disable the feature. Select **OK** at the bottom of the page to save your change.

    ![Disabling storage auto-growth feature.](./media/disabling-auto-grow.PNG  "Storage auto-growth toggled to no")

3. Now, we need to implement firewall rules for the PostgreSQL database so we can access it. Locate the **Connection security** selector under the **Settings** tab.

    ![Configuring the Connection Security settings for the database.](./media/entering-connection-settings.png "Connection security highlighted")

4. We will add an access rule. Since we are storing insecure test data, we can open the 0.0.0.0 to 255.255.255.255 range (all IPv4 addresses). Azure makes this option available. Press the **Save** button at the top of the page once you are ready.

    ![Adding IP addresses as an Access Rule](./media/adding-open-ip-address-range.png "IP Addresses highlighted")

    >**Note**: Do not use this type of rule for databases with sensitive data or in a production environment. You are allowing access from any IP address.