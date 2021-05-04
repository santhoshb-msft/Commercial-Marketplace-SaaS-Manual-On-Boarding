md landinpageappdeploy #create a staging directory

cd landinpageappdeploy # go to the new staging directory

curl -o deploy.ps1 https://raw.githubusercontent.com/santhoshbomma9/landingpage-deploy-automation/main/deploy.ps1  # copy files

curl -o mainTemplate.json https://raw.githubusercontent.com/santhoshbomma9/landingpage-deploy-automation/main/mainTemplate.json  # copy files

Connect-AzureAD # connect to you azure account

.\deploy.ps1 # run the deploy file


[![Deploy To Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/text=https%3A%2F%2Fraw.githubusercontent.com%2Fsanthoshb-msft%2FCommercial-Marketplace-SaaS-Manual-On-Boarding%2Fsb-quickdeploy%2Fresources%2Fdeploy%2FmainTemplate.json)  
[![Visualize](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/visualizebutton.svg?sanitize=true)](http://armviz.io/#/?load=text=https%3A%2F%2Fraw.githubusercontent.com%2Fsanthoshb-msft%2FCommercial-Marketplace-SaaS-Manual-On-Boarding%2Fsb-quickdeploy%2Fresources%2Fdeploy%2FmainTemplate.json)
