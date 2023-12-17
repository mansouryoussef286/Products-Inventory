<a name="readme-top"></a>
<!-- PROJECT LOGO -->
<br />
<div align="center">
<!--   <a href="https://github.com/othneildrew/Best-README-Template">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->

  <h3 align="center">Products Inventory System</h3>

  <p align="center">
    Basic Inventory system for products management
    <br />
    <a href="https://github.com/mansouryoussef286/ProductsInventory"><strong>Explore the code »</strong></a>
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#code">Code</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project
<a name="about-the-project"></a>
This is a simple Inventory system managing API consisting of products that can be manipulated with CRUD operations and these operations are audited to be monitored.
Changing in the inventory fires push notification events through integration with firebase to notify the users in charge.
Also Added Unit tests to ensure code expected behaviour while applying changes through development process. 


<p align="right">(<a href="#readme-top">back to top</a>)</p>



## Built With

<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Microsoft_.NET_logo.svg/1200px-Microsoft_.NET_logo.svg.png" alt=".net logo" width="50" height="50">
<img src="https://cdn-media-1.freecodecamp.org/images/0*CPTNvq87xG-sUGdx.png" alt="firebase logo" width="50" height="50">

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Code

* The code architecture follows a common pattern in software development known as Hexagonal Architecture.
* Also wrapping any external dependency outside the domain layer using adapter design pattern to insure the code extensibility in the future.
* These patterns promotes separation of concerns and independence of the core business logic from external dependencies.
* Here's an overview of the layers:
  <ol>
  <li><strong>View Layer:</strong>
      <ul>
          <li>Responsible for user interface components.</li>
          <li>Presents data to users and captures user inputs.</li>
          <li>Represented by the controllers and data validations.</li>
      </ul>
  </li>
  
  <li><strong>Domain Layer:</strong>
      <ul>
          <li>Contains the business logic and business data entities of the application.</li>
          <li>Enforces business rules and encapsulates the core functionality of the application.</li>
          <li>Includes services, DTOs (Data Transfer Objects), and interfaces defining contracts for interactions within the domain.</li>
              <ul>
                  <li>Services for the auditing of the products regarding the method that will be used for the auditing, whether it's a database or logging in ELK.</li>
                  <li>Interfaces that must be implemented by any data layer repository to access the data persistence layer.</li>
              </ul>
      </ul>
  </li>
  
  <li><strong>Data Layer:</strong>
      <ul>
          <li>Responsible for data access and storage.</li>
          <li>Connects to databases, external APIs, or any other data source.</li>
          <li>Implements interfaces defined in the domain layer to provide concrete implementations for data access.</li>
      </ul>
  </li>
</ol>
<p align="center">
    <img src="https://www.happycoders.eu/wp-content/uploads/2023/01/hexagonal-architecture-with-control-flow.v4-800x474.png" alt="GitHub Logo" height="300">
</p>
<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

# set up your project locally:
To get a local copy up and running follow these simple example steps.

## Prerequisites

To run this project locally you need to have on your machine:
* Visual Studio IDE
* DBMS
* Postgres driver installed to run the DB server


## Installation
<a name="installation"></a>

1. Clone the repo
   ```sh
   git clone https://github.com/mansouryoussef286/ProductsInventory.git
   ```
2. Run the following DB scripts in the dbms
   ```sql
     CREATE TABLE public.products (
    	product_id int4 NOT NULL,
    	"name" varchar(255) NOT NULL,
    	description text NULL,
    	price numeric(10, 2) NOT NULL,
    	quantity int4 NOT NULL,
    	created_at timestamptz NULL DEFAULT CURRENT_TIMESTAMP,
    	updated_at timestamptz NULL DEFAULT CURRENT_TIMESTAMP,
    	CONSTRAINT products_pkey PRIMARY KEY (product_id)
    );

    CREATE TABLE public.audit_logs (
    	table_name varchar(255) NOT NULL,
    	id int4 NOT NULL,
    	old_value text NULL,
    	new_value text NULL,
    	change_date timestamptz NULL DEFAULT CURRENT_TIMESTAMP,
    	operation_type varchar NULL,
    	log_id varchar NOT NULL DEFAULT nextval(0::regclass),
    	CONSTRAINT audit_logs_pk PRIMARY KEY (log_id)
    );
   ```
3. Run the server from visual studio
4. Use swagger link to test the api: https://localhost:44300/swagger
<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Test the Products Functionality using the Api endpoints

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Youssef Mansour - [@LinkedIn](https://twitter.com/your_username) - mansouryoussef286@gmail.com

Project Link: [https://github.com/your_username/repo_name](https://github.com/your_username/repo_name)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
