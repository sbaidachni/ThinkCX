# ThinkCX: Social Intelligence Solution #
In this joint development effort, Microsoft teamed up with ThinkCX to migrate an existing social intelligence solution to Microsoft Azure platform utilizing platform as a service approach. 
In this project, the following services were utilized:
- Azure SQL Data Warehouse
- Azure Functions
- Azure WebJobs
- Azure Stream Analytics
- Event Hub
- Power BI Embedded
- Azure Web Apps
- Azure Blobs
- Azure Machine Learning

The folder structure:
- ARMTemplate – ARM template with all components including optional + stream analytics script;
- FTPCopyToBlob – a console application that may be run as a Web Job to copy files from remote ftp;
- HttpFunction – Azure function to get http request and send them to Event Hub;
- InvokeSpFunc – Azure function to invoke SQL DW stored procedure;
- ReportingProvisioning – a console application to provision and deploy reporting infrastructure;
- ReportingWebApp – a web application to present reports;
- SQLScripts – our testing sql scripts;
- SampleData – some testing data and our test report;
- ThinkCXConsoleTest – an emulator to send http requests;

