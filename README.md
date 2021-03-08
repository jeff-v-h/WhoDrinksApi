# Don't Think Just Drink API

API for the Don't Think Just Drink mobile application.

## Getting started

### Requirements

- .NET 5
- MongoDB
- Docker

### Installation

#### Docker

Docker configuration is already setup via Visual Studio so when VS detects it isn't running, an alert should pop up asking if you want to start up a process.

##### Database

Some API calls use Transactions which are not supported with standalone mongo servers. Standard installations of mongo locally are usually standalone servers (such as Mongo Windows Service). So it is advised to use the command line to start up a mongod process manually. We will be using the same default port 27017 so if there is a Windows Service running, it will need to be shutdown first.

1. Stop your MongoDB Windows Service if it is running on port 27017 (the default on standard installations).
2. In terminal type the following command: `mongod --port 27017 --dbpath C:\mongo-data --replSet rs0 --bind_ip localhost`. You may change the path (i.e. C:\mongo-data) and name of replica set (i.e. rs0) to your liking. If you have gone through below steps before you can stop here, otherwise first time setups will need to continue with below steps.
3. Open up another temrinal and connect to mongo shell: `mongo`. You may need to add mongo to your systems PATH variable if you haven't already.
4. `rs.initate()`;

##### API

1. Open Solution with Visual Studio.
2. Target Docker and click Play button.
3. A new browser should open up to swagger page with path /swagger/index.html.

## Production

Not currently deployed.

## Authors

Jeffrey Huang - jeffrey.huang@recaura.com
