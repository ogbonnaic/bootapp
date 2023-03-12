# Bootapp

<div style="text-align:center">

![](/Bootapp/client-app/src/assets/logo_new.png)

# HAVE YOU HAD TO DEVELOP APIS A LOT FROM DATABASES OR OTHER DATASTORES? DO YOU FIND YOURSELF THINKING OF HOW TO EASILY SPIN UP APIS FROM DATABASE TABLES?

# BOOTAPP IS HERE TO HELP

Bootapp is an open-source system that easily extends datastores (databases, file storage, document databases) into a project with application programming interfaces (API) that you can use to access alll the objects in the database. You can also configure Bootapp for authentication and file management.

Bootapp features also includes; managing multiple datastores, security of the API, blacklisting and whitelisting of hosts that can connect to your APIs

</div>

# Table of contents

- [Overview](#overview)
- [Getting started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
    - [Windows on IIs](#windows-on-iis)
    - [Ubuntu](#ubuntu)
  - [Setup database connection](#setup-database-connection)
  - [Setting up Bootapp admin account](#setting-up-bootapp-admin-account)
- [Bootapp project](#bootapp-project)

  - [Overview of a Bootapp project](#overview-of-a-bootapp-project)
  - [Create a new Bootapp project](#create-a-new-bootapp-project)
  - [Manage an existing project](#manage-an-existing-project)
    - [Configure endpoints for a datastore object or a table](#configure-endpoints-for-a-datastore-object-or-a-table)

- [Accessing Endpoints](#accessing-endpoints)
  - [GET](#get)
  - [POST](#post)
  - [PUT](#get)
  - [DELETE](#delete)
- [Allowing domains to access Bootapp](#allowing-domains-to-access-bootapp)
- [Creating additional users](#creating-additional-users)
- [License](/LICENSE.md)

# Overview

Bootapp is designed with the modern design concepts and . The design considerations for Bootapp include

- Resilience
- Ability to work across different platforms
- Secure
- Great user experience
- The high performance application to handle huge requests
- Gives users control of their endpoints

# Getting started

These are the steps to setting up Bootapp

## Prerequisites

Bootapp requires .NET Core 6.0 and PostgreSql 12+

- Windows: On windows you can download and install .NET Core 6.0 from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). Download and install the SDK and the runtime
- Ubuntu: To setup .NET Core 6.0 on linux, run the following commands.

1. Add the Microsoft package signing key to your list of trusted keys and add the package repository (ensure to adjust your ubuntu version)

```
wget https://packages.microsoft.com/config/ubuntu/22.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

```

2. Install .NET core 6 SDK

```
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-7.0

```

3. Install .NET core 6 runtime

```
sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-7.0

```

## Installation

Bootapp can be installed across multiple operating systems

### Windows on IIS

### Ubuntu

Before you start - please install, configure the nginx server. Run the following commands to install it

```shell
sudo apt update
sudo apt install nginx
sudo ufw app list
sudo ufw allow 'Nginx HTTP'
sudo ufw status
systemctl status nginx
```

You should get an output like the one below. That shows that your installation was successful

```
Output
● nginx.service - A high performance web server and a reverse proxy server
   Loaded: loaded (/lib/systemd/system/nginx.service; enabled; vendor preset: enabled)
   Active: active (running) since Fri 2020-04-20 16:08:19 UTC; 3 days ago
     Docs: man:nginx(8)
 Main PID: 2369 (nginx)
    Tasks: 2 (limit: 1153)
   Memory: 3.5M
   CGroup: /system.slice/nginx.service
           ├─2369 nginx: master process /usr/sbin/nginx -g daemon on; master_process on;
           └─2380 nginx: worker process
```

Now that nginx installation is complete, pull the code from git. You first create a folder and clone the repository inside the folder that you created

```
mkdir ~/source
cd ~/source
git clone https://github.com/ogbonnaic/bootapp.git
```

Navigate to the solution file of the project and run the following commands to the restore the packages from the package maanger

```
cd ~/source/Bootapp
dotnet restore Bootapp.sln
```

Now build the application for production. To do this, run the following commands

```shell
cd Bootapp
dotnet publish --configuration release
```

Once the building is complete, copy the files from /Bootapp/bin/release/net6.0 to /var/www/bootapp.

At this point, create a service that will can be used to automatically start and stop the application. To do that, run the following commands

```
sudo nano /etc/systemd/system/bootapp.service
```

Paste the following content, and save changes:

```
[Unit]
Description=Bootapp

[Service]
WorkingDirectory=/var/www/bootapp
ExecStart=/usr/bin/dotnet /var/www/bootapp/Bootapp.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-example
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

Enable and start the service to run Bootapp

```
sudo systemctl enable grandnode.service
sudo systemctl start grandnode.service
```

Bootapp will be running at port 5000. Open your browser and enter localhost:5000 to open Bootapp

#### Optional (Setting up proxy with Nginx)

Using Nginx, proxy the application to the required port.
Ensure that Nginx is already install on the machine do the following

```
sudo nano /etc/nginx/sites-available/default
```

Paste this onto the file and save

```
server {
    listen        80;
    server_name   example.com *.example.com;




    location / {
        proxy_pass         http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }

}

```

Restart Nginx and navigate the port that Bootapp is mapped to on Nginx. Navigate to your browser (localhost:port) to ensure Bootapp is loaded.

Congratulations, you have now completed the installation of Bootapp almost there

## Setup database connection

Create a new database on postgres {db_name}

Navigate to the application folder and open appsettings.json

```json
"ConnectionStrings": {
    "DefaultConnection": "User ID=postgres;Password=********;Host=localhost;Port=5432;Database={db_name};Pooling=true;",

  }
```

Locate the connectionstring section of the file and update this connection with the corresponding one from the database server.

## Setting up Bootapp admin account

Bootapp admin account is required to access Bootapp features. On the browser, navigate to (localhost:port) that Bootapp is configured to run on.

Fill in the account name, code, the email address of the admin and their password, submit to create your account, you will be redirected to the login page. Login with the email address and the password you specified.

# Bootapp project

## Overview of a Bootapp project

Project is the base object that Bootapp uses to manage all the operations that it extends to the datastore

## Create a new Bootapp project

To create a new project, from the navigation menu click on Projects (Navigation Bar -> Projects). The page will list all the existing projects in the system already (if you have projects) and to add a new one, click on the _**New Project**_ button on the top right corner of the page. This pops up a dialog for creating the project enter the required information and click the _**Create Project**_

| Field name            | Description                                                                                                                                                  |
| --------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Name                  | The name you want to assign to the new project                                                                                                               |
| Code                  | A unique code assigned to the project                                                                                                                        |
| Data source           | This is the datastore that Bootapp will convert into an API                                                                                                  |
| Host                  | The host of the data store specied above                                                                                                                     |
| Port                  | The TCP Port number of the datastore                                                                                                                         |
| User ID               | The username that for the datastore                                                                                                                          |
| Password              | The password corresponding to the User ID provided above                                                                                                     |
| Database              | The name of the particular database on the datastore                                                                                                         |
| Enable Authentication | Select this option if you want Bootapp to expose authentication APIs for this project. Note: _This will require creating additional tables in your database_ |

![create_project](/images/create_project.png)

## Manage an existing project

Once a project is created, you can now access the endpoints of the datastore that you linked to the Bootapp. To access the endpoint, you will need an API_KEY and API_SECRET. Both were created alongside the project. This will be supplied to the requests that will be made to the endpoints. This is for security and you are advised to ensure that both keys are kept secret.

From the Navigation Menu -> Projects, click **View** to open the project. You will see a view like the on below

![project_view](/images/project_view.png)

To access the API*KEY and API_SECRET, click the **Access Keys** button on the top right corner of the page. You can also edit and existing project by clicking the **Edit** button beside it. You can also toggle the Active switch to activate or deactivate the project. Note: \_If a project is deactivated, the endpoints will not be accessible*

### Configure endpoints for a datastore object or a table

This page also shows the list of all the objects or tables in the datastore. This list also shows the toggles switches for each of the CRUD operations. Delete is deactivated by default, using the toggle switches you can restrict access any of the objects in the datastore. Any operation that is deactivated for any of the objects will not be allowed for that object.

# Accessing endpoints

![project_endpoints](/images/project_details.png)

From the image above, clicking each of the objects on the datastore shows the details of the object to the right side of the page and also the available endpoints for the selected object. These endpoints can be accessed by adding API_KEY and API_SECRETS as headers to the request to Bootapp

## GET

Example of request to the GET endpoint

Python

```python
import requests

url = "http://localhost:5000/api/v1/data/app_country"

payload={}
headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs='
}

response = requests.request("GET", url, headers=headers, data=payload)

print(response.text)

```

Javascript XHR

```javascript
var xhr = new XMLHttpRequest();
xhr.withCredentials = true;

xhr.addEventListener("readystatechange", function () {
  if (this.readyState === 4) {
    console.log(this.responseText);
  }
});

xhr.open("GET", "http://localhost:5000/api/v1/data/app_country");
xhr.setRequestHeader("API_KEY", "LVJGDESDCDFK9TCO");
xhr.setRequestHeader(
  "API_SECRET",
  "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs="
);

xhr.send();
```

Javascript JQuery

```javascript
var settings = {
  url: "http://localhost:5000/api/v1/data/app_country",
  method: "GET",
  timeout: 0,
  headers: {
    API_KEY: "LVJGDESDCDFK9TCO",
    API_SECRET: "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=",
  },
};

$.ajax(settings).done(function (response) {
  console.log(response);
});
```

Dart

```dart
var headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs='
};
var request = http.Request('GET', Uri.parse('http://localhost:5000/api/v1/data/app_country'));

request.headers.addAll(headers);

http.StreamedResponse response = await request.send();

if (response.statusCode == 200) {
  print(await response.stream.bytesToString());
}
else {
  print(response.reasonPhrase);
}

```

Go

```go
import (
  "fmt"
  "net/http"
  "io/ioutil"
)

func main() {

  url := "http://localhost:5000/api/v1/data/app_country"
  method := "GET"

  client := &http.Client {
  }
  req, err := http.NewRequest(method, url, nil)

  if err != nil {
    fmt.Println(err)
    return
  }
  req.Header.Add("API_KEY", "LVJGDESDCDFK9TCO")
  req.Header.Add("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=")

  res, err := client.Do(req)
  if err != nil {
    fmt.Println(err)
    return
  }
  defer res.Body.Close()

  body, err := ioutil.ReadAll(res.Body)
  if err != nil {
    fmt.Println(err)
    return
  }
  fmt.Println(string(body))
}
```

C# using Restsharp

```csharp
var options = new RestClientOptions("")
{
  MaxTimeout = -1,
};
var client = new RestClient(options);
var request = new RestRequest("http://localhost:5000/api/v1/data/app_country", Method.Get);
request.AddHeader("API_KEY", "LVJGDESDCDFK9TCO");
request.AddHeader("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=");
RestResponse response = await client.ExecuteAsync(request);
Console.WriteLine(response.Content);
```

The GET endpoint access the following querystring

| Name        | Description                                                                                                                                                                        | Data Type    | Nullable |
| :---------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :----------- | :------- |
| fields      | Columns that you are retrieving out from the table, if this is not provided, the API will return all the columns in the table. Leave this empty if you want to get all the columns | String array | Yes      |
| args        | The columns that you are searching values from for the table                                                                                                                       | String array | Yes      |
| values      | The corresponding values that you are searching against each of the args. This must correspond to the number of args that provided above                                           | object array | Yes      |
| orderBy     | The columns that you are ordering the result (this is for ascending)                                                                                                               | String array | Yes      |
| orderByDesc | The columns ordered with on descending order                                                                                                                                       | String array | Yes      |
| append      | Additional sql query that you want to add to the query                                                                                                                             | String       | Yes      |
| PageNumber  | Maximum number of rows to be retrieved from the table in the database                                                                                                              | integer      | Yes      |
| PageSize    | The size of rows to be read from the table                                                                                                                                         | integer      | Yes      |

## POST

Python

```python
import requests
import json

url = "http://localhost:5000/api/v1/data/address"

payload = json.dumps({
  "user_id": 5,
  "contact_name": "Amarchi Obetta",
  "phone_number": "0",
  "address_name": "5845 AUSTIN WATERS",
  "country": "US",
  "state": "NJ",
  "city": "The Colony",
  "zip_code": "75056",
  "is_shipping": 0,
  "created_at": "2021-11-25T16:55:26.41036",
  "status": 1
})
headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=',
  'Content-Type': 'application/json'
}

response = requests.request("POST", url, headers=headers, data=payload)

print(response.text)

```

Javascript XHR

```javascript
var data = JSON.stringify({
  user_id: 5,
  contact_name: "Amarchi Obetta",
  phone_number: "0",
  address_name: "5845 AUSTIN WATERS",
  country: "US",
  state: "NJ",
  city: "The Colony",
  zip_code: "75056",
  is_shipping: 0,
  created_at: "2021-11-25T16:55:26.41036",
  status: 1,
});

var xhr = new XMLHttpRequest();
xhr.withCredentials = true;

xhr.addEventListener("readystatechange", function () {
  if (this.readyState === 4) {
    console.log(this.responseText);
  }
});

xhr.open("POST", "http://localhost:5000/api/v1/data/address");
xhr.setRequestHeader("API_KEY", "LVJGDESDCDFK9TCO");
xhr.setRequestHeader(
  "API_SECRET",
  "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs="
);
xhr.setRequestHeader("Content-Type", "application/json");

xhr.send(data);
```

Javascript JQuery

```javascript
var settings = {
  url: "http://localhost:5000/api/v1/data/address",
  method: "POST",
  timeout: 0,
  headers: {
    API_KEY: "LVJGDESDCDFK9TCO",
    API_SECRET: "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=",
    "Content-Type": "application/json",
  },
  data: JSON.stringify({
    user_id: 5,
    contact_name: "Amarchi Obetta",
    phone_number: "0",
    address_name: "5845 AUSTIN WATERS",
    country: "US",
    state: "NJ",
    city: "The Colony",
    zip_code: "75056",
    is_shipping: 0,
    created_at: "2021-11-25T16:55:26.41036",
    status: 1,
  }),
};

$.ajax(settings).done(function (response) {
  console.log(response);
});
```

Dart

```dart
var headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=',
  'Content-Type': 'application/json'
};
var request = http.Request('POST', Uri.parse('http://localhost:5000/api/v1/data/address'));
request.body = json.encode({
  "user_id": 5,
  "contact_name": "Amarchi Obetta",
  "phone_number": "0",
  "address_name": "5845 AUSTIN WATERS",
  "country": "US",
  "state": "NJ",
  "city": "The Colony",
  "zip_code": "75056",
  "is_shipping": 0,
  "created_at": "2021-11-25T16:55:26.41036",
  "status": 1
});
request.headers.addAll(headers);

http.StreamedResponse response = await request.send();

if (response.statusCode == 200) {
  print(await response.stream.bytesToString());
}
else {
  print(response.reasonPhrase);
}

```

GO

```go
package main

import (
  "fmt"
  "strings"
  "net/http"
  "io/ioutil"
)

func main() {

  url := "http://localhost:5000/api/v1/data/address"
  method := "POST"

  payload := strings.NewReader(`  {
        "user_id": 5,
        "contact_name": "Amarchi Obetta",
        "phone_number": "0",
        "address_name": "5845 AUSTIN WATERS",
        "country": "US",
        "state": "NJ",
        "city": "The Colony",
        "zip_code": "75056",
        "is_shipping": 0,
        "created_at": "2021-11-25T16:55:26.41036",
        "status": 1
    }`)

  client := &http.Client {
  }
  req, err := http.NewRequest(method, url, payload)

  if err != nil {
    fmt.Println(err)
    return
  }
  req.Header.Add("API_KEY", "LVJGDESDCDFK9TCO")
  req.Header.Add("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=")
  req.Header.Add("Content-Type", "application/json")

  res, err := client.Do(req)
  if err != nil {
    fmt.Println(err)
    return
  }
  defer res.Body.Close()

  body, err := ioutil.ReadAll(res.Body)
  if err != nil {
    fmt.Println(err)
    return
  }
  fmt.Println(string(body))
}
```

C# using Restsharp

```c#
var options = new RestClientOptions("")
{
  MaxTimeout = -1,
};
var client = new RestClient(options);
var request = new RestRequest("http://localhost:5000/api/v1/data/address", Method.Post);
request.AddHeader("API_KEY", "LVJGDESDCDFK9TCO");
request.AddHeader("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=");
request.AddHeader("Content-Type", "application/json");
var body = @"  {" + "\n" +
@"        ""user_id"": 5," + "\n" +
@"        ""contact_name"": ""Amarchi Obetta""," + "\n" +
@"        ""phone_number"": ""0""," + "\n" +
@"        ""address_name"": ""5845 AUSTIN WATERS""," + "\n" +
@"        ""country"": ""US""," + "\n" +
@"        ""state"": ""NJ""," + "\n" +
@"        ""city"": ""The Colony""," + "\n" +
@"        ""zip_code"": ""75056""," + "\n" +
@"        ""is_shipping"": 0," + "\n" +
@"        ""created_at"": ""2021-11-25T16:55:26.41036""," + "\n" +
@"        ""status"": 1" + "\n" +
@"    }";
request.AddStringBody(body, DataFormat.Json);
RestResponse response = await client.ExecuteAsync(request);
Console.WriteLine(response.Content);
```

## PUT

Python

```python
import requests
import json

url = "http://localhost:5000/api/v1/data/address"

payload = json.dumps({
  "toAlter": [
    "contact_name"
  ],
  "alValues": [
    "John Doe"
  ],
  "args": [
    "id"
  ],
  "values": [
    280
  ]
})
headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=',
  'Content-Type': 'application/json'
}

response = requests.request("PUT", url, headers=headers, data=payload)

print(response.text)

```

Javascript XHR

```javascript
var data = JSON.stringify({
  toAlter: ["contact_name"],
  alValues: ["John Doe"],
  args: ["id"],
  values: [280],
});

var xhr = new XMLHttpRequest();
xhr.withCredentials = true;

xhr.addEventListener("readystatechange", function () {
  if (this.readyState === 4) {
    console.log(this.responseText);
  }
});

xhr.open("PUT", "http://localhost:5000/api/v1/data/address");
xhr.setRequestHeader("API_KEY", "LVJGDESDCDFK9TCO");
xhr.setRequestHeader(
  "API_SECRET",
  "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs="
);
xhr.setRequestHeader("Content-Type", "application/json");

xhr.send(data);
```

Javascript JQuery

```javascript
var settings = {
  url: "http://localhost:5000/api/v1/data/address",
  method: "PUT",
  timeout: 0,
  headers: {
    API_KEY: "LVJGDESDCDFK9TCO",
    API_SECRET: "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=",
    "Content-Type": "application/json",
  },
  data: JSON.stringify({
    toAlter: ["contact_name"],
    alValues: ["John Doe"],
    args: ["id"],
    values: [280],
  }),
};

$.ajax(settings).done(function (response) {
  console.log(response);
});
```

Dart

```dart
var headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=',
  'Content-Type': 'application/json'
};
var request = http.Request('PUT', Uri.parse('http://localhost:5000/api/v1/data/address'));
request.body = json.encode({
  "toAlter": [
    "contact_name"
  ],
  "alValues": [
    "John Doe"
  ],
  "args": [
    "id"
  ],
  "values": [
    280
  ]
});
request.headers.addAll(headers);

http.StreamedResponse response = await request.send();

if (response.statusCode == 200) {
  print(await response.stream.bytesToString());
}
else {
  print(response.reasonPhrase);
}

```

Go

```go
package main

import (
  "fmt"
  "strings"
  "net/http"
  "io/ioutil"
)

func main() {

  url := "http://localhost:5000/api/v1/data/address"
  method := "PUT"

  payload := strings.NewReader(`{
    "toAlter":["contact_name"],
    "alValues":["John Doe"],
    "args": ["id"],
    "values":[280]
}`)

  client := &http.Client {
  }
  req, err := http.NewRequest(method, url, payload)

  if err != nil {
    fmt.Println(err)
    return
  }
  req.Header.Add("API_KEY", "LVJGDESDCDFK9TCO")
  req.Header.Add("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=")
  req.Header.Add("Content-Type", "application/json")

  res, err := client.Do(req)
  if err != nil {
    fmt.Println(err)
    return
  }
  defer res.Body.Close()

  body, err := ioutil.ReadAll(res.Body)
  if err != nil {
    fmt.Println(err)
    return
  }
  fmt.Println(string(body))
}
```

C# using Restsharp

```c#

var options = new RestClientOptions("")
{
  MaxTimeout = -1,
};
var client = new RestClient(options);
var request = new RestRequest("http://localhost:5000/api/v1/data/address", Method.Put);
request.AddHeader("API_KEY", "LVJGDESDCDFK9TCO");
request.AddHeader("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=");
request.AddHeader("Content-Type", "application/json");
var body = @"{" + "\n" +
@"    ""toAlter"":[""contact_name""]," + "\n" +
@"    ""alValues"":[""John Doe""]," + "\n" +
@"    ""args"": [""id""]," + "\n" +
@"    ""values"":[280]" + "\n" +
@"}";
request.AddStringBody(body, DataFormat.Json);
RestResponse response = await client.ExecuteAsync(request);
Console.WriteLine(response.Content);

```

PUT Parameters

| Name     | Description                                                                                                                                            | Data Type    | Nullable |
| :------- | :----------------------------------------------------------------------------------------------------------------------------------------------------- | :----------- | :------- |
| toAlter  | The columns in the table to be updated                                                                                                                 | String array | No       |
| alValues | The new values that will be used to update the columns that are going to be altered (this should be in the same order with the toAlter)                | object array | No       |
| args     | The columns for which the search conditions are going to be gotten from (Note that if this is not provided, all the rows in the table will be updated) | String array | Yes      |
| values   | The values to search the criteria columns with. This must correspond to the number of args                                                             | Object array | Yes      |

## DELETE

Python

```python
import http.client
import json

conn = http.client.HTTPSConnection("localhost", 5000)
payload = json.dumps({
  "args": [
    "id"
  ],
  "values": [
    280
  ]
})
headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=',
  'Content-Type': 'application/json'
}
conn.request("DELETE", "/api/v1/data/address", payload, headers)
res = conn.getresponse()
data = res.read()
print(data.decode("utf-8"))
```

Javascript XHR

```javascript
var data = JSON.stringify({
  args: ["id"],
  values: [280],
});

var xhr = new XMLHttpRequest();
xhr.withCredentials = true;

xhr.addEventListener("readystatechange", function () {
  if (this.readyState === 4) {
    console.log(this.responseText);
  }
});

xhr.open("DELETE", "http://localhost:5000/api/v1/data/address");
xhr.setRequestHeader("API_KEY", "LVJGDESDCDFK9TCO");
xhr.setRequestHeader(
  "API_SECRET",
  "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs="
);
xhr.setRequestHeader("Content-Type", "application/json");

xhr.send(data);
```

Javascript JQuery

```javascript
var settings = {
  url: "http://localhost:5000/api/v1/data/address",
  method: "DELETE",
  timeout: 0,
  headers: {
    API_KEY: "LVJGDESDCDFK9TCO",
    API_SECRET: "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=",
    "Content-Type": "application/json",
  },
  data: JSON.stringify({
    args: ["id"],
    values: [280],
  }),
};

$.ajax(settings).done(function (response) {
  console.log(response);
});
```

Dart

```dart
var headers = {
  'API_KEY': 'LVJGDESDCDFK9TCO',
  'API_SECRET': '2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=',
  'Content-Type': 'application/json'
};
var request = http.Request('DELETE', Uri.parse('http://localhost:5000/api/v1/data/address'));
request.body = json.encode({
  "args": [
    "id"
  ],
  "values": [
    280
  ]
});
request.headers.addAll(headers);

http.StreamedResponse response = await request.send();

if (response.statusCode == 200) {
  print(await response.stream.bytesToString());
}
else {
  print(response.reasonPhrase);
}

```

GO

```go
package main

import (
  "fmt"
  "strings"
  "net/http"
  "io/ioutil"
)

func main() {

  url := "http://localhost:5000/api/v1/data/address"
  method := "DELETE"

  payload := strings.NewReader(`{

    "args": ["id"],
    "values":[280]
}`)

  client := &http.Client {
  }
  req, err := http.NewRequest(method, url, payload)

  if err != nil {
    fmt.Println(err)
    return
  }
  req.Header.Add("API_KEY", "LVJGDESDCDFK9TCO")
  req.Header.Add("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=")
  req.Header.Add("Content-Type", "application/json")

  res, err := client.Do(req)
  if err != nil {
    fmt.Println(err)
    return
  }
  defer res.Body.Close()

  body, err := ioutil.ReadAll(res.Body)
  if err != nil {
    fmt.Println(err)
    return
  }
  fmt.Println(string(body))
}
```

C# using Restsharp

```c#
var options = new RestClientOptions("")
{
  MaxTimeout = -1,
};
var client = new RestClient(options);
var request = new RestRequest("http://localhost:5000/api/v1/data/address", Method.Delete);
request.AddHeader("API_KEY", "LVJGDESDCDFK9TCO");
request.AddHeader("API_SECRET", "2U6tUMt8OZaEEg4iB4eUZsizGydXVwzDw5PnaqhBaUs=");
request.AddHeader("Content-Type", "application/json");
var body = @"{" + "\n" +
@"" + "\n" +
@"    ""args"": [""id""]," + "\n" +
@"    ""values"":[280]" + "\n" +
@"}";
request.AddStringBody(body, DataFormat.Json);
RestResponse response = await client.ExecuteAsync(request);
Console.WriteLine(response.Content);
```

DELETE Parameters

| Name   | Description                                                                                                                                                 | Data Type    | Nullable |
| :----- | :---------------------------------------------------------------------------------------------------------------------------------------------------------- | :----------- | :------- |
| args   | The columns for which the search conditions are going to be gotten from (Note that if this is not provided, all the rows in the table will be updated) into | String array | No       |
| values | The values to search the criteria columns with. This must correspond to the number of args                                                                  | object array | No       |

# Allowing domains to access Bootapp

To ensure your APIs are secured, Bootapp provides a feature that can limit the domains that can access your endpoints. To add a new domain to be allowed by Bootapp, go to Navigation Menu => Settings. On the settings page, click the plus button which will pop up a dialog with a form to add a new domain to Bootapp. _It is important to note that once you make a change on the domain, Bootapp will need to be restarted for it to take effect_

![whitelisting](/images/domains.png)

# Creating additional users

Additional users can be added to a Bootapp setup. To do this. Navigate to the users page. New User button is located at the top right corner of the page. Fill the required information on the form to create a new user. You can also edit an existing user or deactivate a user
