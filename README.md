# Don't Think Just Drink API

API for the Don't Think Just Drink mobile application.

## Getting started

### Requirements

- .NET 5
- MongoDB
- Docker

### Installation

#### Database

##### Standalone servers

Download MongoDb. The default installation includes a downloaded service that should run a default standalone server automatically.

##### Replica Sets

If code is changed so that some API calls use Transactions, standalone server configuration will not be sufficient.
This is because transactions are not supported with standalone mongo servers.
It is advised to use the command line to start up a mongod process manually. The instructions below will be using the same default port 27017 so if there is a Windows Service running, it will need to be shutdown first:

1. Stop your MongoDB Windows Service if it is running on port 27017 (the default on standard installations).
2. In terminal type the following command: `mongod --port 27017 --dbpath C:\mongo-data --replSet rs0 --bind_ip localhost`. You may change the path (i.e. C:\mongo-data) and name of replica set (i.e. rs0) to your liking. If you have gone through below steps before you can stop here, otherwise first time setups will need to continue with below steps.
3. Open up another temrinal and connect to mongo shell: `mongo`. You may need to add mongo to your systems PATH variable if you haven't already.
4. `rs.initate()`;

#### API

##### Method 1: Dockerfile and Visual Studio

Use this method to be able to add break points for debugging.

1. Open Solution with Visual Studio.
2. Target Docker and click Play button.
3. A new browser should open up to swagger page with path /swagger/index.html.
4. For API requests, target http://localhost:5000 or https://localhost:5001

##### Method 2: Docker Compose

1. You may need to go to the reverseproxy/nginx.conf file and ensure the upstream app_servers block targets dontthinkjustdrinkapi:5000
```
upstream app_servers {
    server dontthinkjustdrinkapi:5000;
}
```
2. Navigate to root directory in command line and run `docker-compose build`
3. `docker-compose up`. No browser will open, but the app should be running on http://localhost:80

## Production

### Prerequisites

- AWS CLI
- AWS access and permissions
- Docker
- Mongo Atlas access and permissions

### Setup

The following steps have already been completed and can be skipped. This has been placed here for reference.

1. Setup login configuration on aws for an account able to write to ecr
2. Follow steps 1-5 below in 'New Release'.
3. Configure Security Group allowing at least http and https
4. Create Task Definition which uses the created docker images (latest version)
5. Create cluster
6. Create ALB which also includes same security group as cluster
7. Inthe created cluster create a service that targets the same vpc, subnets and the created ALB. For the container to load, select the nginx container.
8. Login to the DB Cluster/s and ensure ALB is allowed access.

### New Release

1. Update reverseproxy/nginx.conf file's upstream target to be 127.0.0.1:5000
```
upstream app_servers {
    server 127.0.0.1:5000;
}
```
2. In appsettings.json (in root of the API project directory), update the db connection string and basic security settings. 
3. Get password and login: `aws ecr get-login-password | docker login --username AWS --password-stdin <yourawsaccountnumber>.dkr.ecr.ap-southeast-2.amazonaws.com`
4. Build the updated image: `docker-compose build`
5. Check the docker image names: `docker image ls`
6. Tag docker image `docker tag <dockerimagename> <yourawsaccountnumber>.dkr.ecr.ap-southeast-2.amazonaws.com/<docker_repository_name>`
7. Push up to ecr: `docker push <yourawsaccountnumber>.dkr.ecr.ap-southeast-2.amazonaws.com/<docker_repository_name>`
8. Repeat steps 2 to 4 for any other images that have changed
9. On AWS, go to Task Definition currently being used and either create new revision or update service and tick checkbox for 'Force new deployment'

#### New Release Example

1. Get password and login: `aws ecr get-login-password | docker login --username AWS --password-stdin 209196940283.dkr.ecr.ap-southeast-2.amazonaws.com`
2. Create repository: `aws ecr create-repository --repository-name dontthinkjustdrinkapi --region ap-southeast-2`
3. Tag docker image `docker tag dontthinkjustdrinkapi_dontthinkjustdrinkapi 209196940283.dkr.ecr.ap-southeast-2.amazonaws.com/dontthinkjustdrinkapi`
4. Push up to ecr: `docker push 209196940283.dkr.ecr.ap-southeast-2.amazonaws.com/dontthinkjustdrinkapi`
5. Create repository for nginx: `aws ecr create-repository --repository-name dontthinkjustdrinkapi-reverseproxy --region ap-southeast-2`
6. Tag docker image for nginx: `docker tag dontthinkjustdrinkapi_reverseproxy 209196940283.dkr.ecr.ap-southeast-2.amazonaws.com/dontthinkjustdrinkapi-reverseproxy`
7. Push up nginx tag to ecr: `docker push 209196940283.dkr.ecr.ap-southeast-2.amazonaws.com/dontthinkjustdrinkapi-reverseproxy`

## Authors

Jeffrey Huang - jeffrey.huang@recaura.com
