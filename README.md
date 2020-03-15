# ServerAllocation_Task1
Task 1 - Implement Weighted Round Robin algorithm for server allocation.

Task 3 - How you will make Task 2 more secure.

Answer - 
In order to make the Task 2 more secure, I will enforce the web site to work only with HTTPS connection. 
As I am planning to deploy the Task2 work in Azure App Service Web App (Work is in progress), I will configure the App Service to use HTTPS
in Azure Portal as follows -
1. Login to Azure portal.
2. Navigate to the App Service to update.
3. Open Custom Domains blad for the App Service.
4. Select "On" for Https Only setting on that blad and save the changes.

This will redirect all the HTTP requests through HTTPS. This way all the network communication would be secured.
