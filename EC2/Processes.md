## Data Validation

### Use Case
>The engineering team refreshes catalog information on drugs approved by the FDA
on a weekly basis.

>Please provide a Test Plan document outlining the steps you would take as a QA member to
confirm that the refresh each week is correct? Please be as detailed as possible.

### Proposal

> Initial questions I would ask about this would be process related. Does this catalog include only new drugs? Or is it a full file of all approved drugs released each week? If not will we need to verify if a drug is pulled from this list? Or is that a seperate process? Does QA have access to the files or are they pulled from a data source? Is the process twice run safe? Can people add drugs to this list outside of this process?

>I will make a few assumptions about this problem. This catalog only includes new drugs and we won't need to worry about removing them at this time. Second, the process is twice run safe. Third, the process will NOT update drugs that already exist. The drugs have a unique ID to them. QA also has access to where the names are stored and can query upon that.

The overall goal is to ensure accuracy, completeness, and quality. While also considering employee time. 

The simplest and fastest way would be to validate during loading and to get a count of the current drugs. Run the proceess and get a second count. If the difference is equal to the number of drugs in the catalog I would assume the process ran succesfully. 

If the processes is twice run safe and it was run a 2nd time and the process reports that 'zero drugs were added' we'd need to verify that it was previously run. This would also apply if the processes is opaque to QA.

For that I would want to verify the datetime of new or update drug entries. What was loaded since the last prior known run? If this last run was known to be ran on the 1st and we have 200 entries for the 7th as well as a catalog with 200 entries I'd be content.

But, maybe there are 300 entries in the catalog or we don't have access to the catalog? How can we be sure of the completeness? If we have access to the catalog I'd grab the first entry from the file, the last entry, and a random entry and search the database for them. Even if they were not added we can check that they exist. If QA doesn't readily have access to the file we'll need to base it off the dates and give a judgement based on what seems normal. Do we normally load 200-300 a week? Did we load in that range? 

Third, I'd want to verify that the data loaded correctly. I'd search the database for empty drug names, or any other common criterea. As well as for duplicates. This can be done with simple sql querying. 

Ultimately though, I feel this process would be properly covered through unit and interation tests. It's a core business process and at this stage I feel QA should mostly be worried that 'it ran' and we can trust our automation and code testing processes.


## Procedure
### Overview
>The engineering development team wants to have more frequent releases of their services.
Today, one of their applications deploys the frontend and backend on the same build image.
The team is planning to split out that deployment routine so that the frontend and backend can
be deployed independently. Your team has been tasked in enabling the development team to
ensure this transition goes smoothly.

>What processes, pipelines, and/or tooling would your team build to assist the development
team on this endeavor? What reports would you provide that the team can review as they are
making this transition?
### Proposal

Much of this depends on our existing processes. How easily can we split this out? Will we need to build from the ground up and replace everything? Are we otherwise happy with the process? What weaknesses does it currently have? What existing tools do we have such as a loadbalancer? Do we want automated deployment with a CI/CD system? Or partially automated with one button deploy. Is downtime a concern?

Many cloud resources have their own pipelines and I feel it would be best to leverage those, such as Azure DevOps Pipeline or AWS CodePipeline. Regardless of what we choose security is a big part of it. I would point development/DevOps to the security best practices documentation for the pipline and tooling we choose. [Azure](https://docs.microsoft.com/en-us/azure/devops/migrate/security-validation-cicd-pipeline?view=azure-devops&viewFallbackFrom=vsts) and [Docker](https://docs.docker.com/engine/security/) provide their own. Most pipelines also provide their own tooling and flows to automatically run tests, perform static code analysis, and other security testing.

If feasable I would strongly recommend a [Blue-Green](https://azure.microsoft.com/en-us/blog/blue-green-deployments-using-azure-traffic-manager/) deployment method. If you are are currently on Api.Blue and Web.Blue you would be able to deploy code changes to Web.Green and redirect local/office traffic to that server for validation. Automated tests and smoke testing could then be done before swapping all public traffic over to Web.Green. If there are any issues traffic can be reverted back to Web.Blue with limited or no downtime. This can also be done using containers such as Docker.

From a quality perspective we would focus on building automated tests to be run on the deployed code before we send public traffic to the new deployment.

The only thing I would otherwise recommend to build is if we needed to leverage something like Chef or Puppet, which requires one to write custom scripts. Otherwise, I would heavily lean on existing technologies and software.

Regarding reports, we would need to document the existing processes so we can better understand the flow for the updated deployments. Do we need to clear out Redis or other caches? Are there any post or predeployment scripts that need to be run?
