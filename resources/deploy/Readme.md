md landinpageappdeploy #create a staging directory
cd landinpageappdeploy # go to the new staging directory
curl -o deploy.ps1 https://raw.githubusercontent.com/santhoshbomma9/landingpage-deploy-automation/main/deploy.ps1  # copy files
curl -o mainTemplate.json https://raw.githubusercontent.com/santhoshbomma9/landingpage-deploy-automation/main/mainTemplate.json  # copy files
Connect-AzureAD # connect to you azure account
.\deploy.ps1 # run the deploy file
