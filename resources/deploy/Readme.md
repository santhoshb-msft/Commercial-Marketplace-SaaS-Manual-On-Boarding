### Sample Deployed Architecture
Below sample architecture could be one way of running the sample app in your Azure instance in a Cloud optimized way.

![Architecture Overview and Process Flow of the Solution](~/ReadmeFiles/saas-samplesdk-architecture.png)

</hr>

### Quick Deploy Option 1
Please use this option, if you already have app registrations created and would like to create an App and Storage account.

[![Deploy To Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fsanthoshb-msft%2FCommercial-Marketplace-SaaS-Manual-On-Boarding%2Fsb-quickdeploy%2Fresources%2Fdeploy%2FmainTemplate.json)  
---

### Quick Deploy Option 2
Please use this option, if you like to deploy/create App registrations, App and Storage account. 

```powershell
md landinpageappdeploy #create a staging directory

cd landinpageappdeploy #go to the new staging directory

curl -o deploy.ps1 https://raw.githubusercontent.com/santhoshbomma9/landingpage-deploy-automation/main/deploy.ps1  #copy deploy ps file

curl -o mainTemplate.json https://raw.githubusercontent.com/santhoshbomma9/landingpage-deploy-automation/main/mainTemplate.json  #copy template ps file

Connect-AzureAD #connect to you azure account

.\deploy.ps1 #run the deploy file
```