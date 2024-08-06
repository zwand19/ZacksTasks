# Zack's Tasks: The Ultimate Game-Changer in Task Management
This is the greatest and best ~~song~~ site in the world

Welcome to Zack's Tasks, the revolutionary task management site that's here to change your life—or at least help you pretend you have it all together. Why settle for ordinary to-do lists when you can experience the sheer brilliance of Zack's Tasks?

Gone are the days of aimlessly scribbling down tasks like “Buy milk” or “Call Mom” on sticky notes that inevitably end up in the trash. With Zack's Tasks, you can unleash your inner productivity guru! Create and delete tasks with the flick of a button. But wait—there’s more! Our state-of-the-art AI functionality takes it to the next level. Just type in “plan my wedding,” and watch in awe as Zack's Tasks magically creates 12 perfectly crafted sub-tasks, making your big day almost stress-free (don’t worry, it’s still going to be a total nightmare, but at least your to-do list will look organized).

But that's not all. Feeling overwhelmed? Had enough of those pesky tasks? With a single click, you can obliterate all your responsibilities at once! It’s like an undo button for your life, minus the consequences.

Zack's Tasks isn’t just a tool—it’s a lifestyle, a movement, and quite possibly the best thing to happen to task management since, well, ever. So, go ahead, dive into the magical world of Zack's Tasks. It might not actually revolutionize your life, but hey, it'll sure look like you’ve got it all figured out!

#### Features
- Create & Delete Tasks: Manage your tasks with ease.
- AI Task Generation: Automatically generate sub-tasks based on a single task description.
- One-Click Task Deletion: Wipe your slate clean with a single click.

## Installation Guide

Below, you'll find everything you need to get Zack's Tasks up and running, so you can start revolutionizing your to-do list in no time.

### Prerequisites
Make sure you have the following installed on your machine:

- .NET Core SDK 7.0
- SQL Server Express
- Node.js (includes npm)
- Angular CLI


### Installation

##### Clone the repository:
In powershell/bash/etc:
```
git clone https://github.com/zwand19/ZacksTasks.git
cd ZacksTasks
```

##### Restore .NET dependencies:
Navigate to the Web project folder and restore the necessary dependencies:

```
cd Web
dotnet restore
```

##### Install Angular dependencies:
Move into the ClientApp directory and install the Angular dependencies:

```
cd ClientApp
npm install
```

##### Running the Application
###### Set up your SQL Server:

The application comes pre-configured with a default connection string in Web/appsettings.Development.json. It’s set to use a local SQL Server Express instance with the following configuration:

```
"ConnectionStrings": {
    "DefaultConnection": "Server=\\SQLEXPRESS;Database=OriginDigitalDb;Trusted_Connection=True;Encrypt=False;"
}
```

If you have SQL Server Express installed and running, this connection string should work out of the box. If not, you'll need to adjust it to match your environment.

###### Add your OpenAI API Key (Optional)
The site will run without this, and will hide the relevant functionality from the UI, but this is recommended to get access to all the latest and greatest functionality that Zack's Tasks has to offer. Add your OpenAI API Key either in Web/appsettings.Development.json, or set as an environment variable on your machine. _OpenAI__ApiKey_ should be the key name.

##### Run the .NET application:
In the Web project folder, run the following command to build and launch the back-end:

```
dotnet run
```

This will spin up the .NET server, and the application will be ready to serve requests.

##### Run the Angular client:
Open a new terminal, navigate to the ClientApp folder, and start the Angular development server:

```
cd Web/ClientApp
ng serve
```

By default, this will serve your Angular app from port 4200

##### Access the application:
Open your browser and navigate to https://localhost:7166 to start using Zack's Tasks!

Thank you for checking out Zack's Tasks. Now go forth and conquer your to-do list with newfound enthusiasm (or at least look like you are)!
