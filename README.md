# BloodDonorsAPI
API in ASP.NET Core which gives access to blood donors database.

This API is a learning project, simple server for storing/serving blood donors data, which is used by: https://github.com/KMielnik/BloodDonorsClientWPF

It should be accessible via: blooddonorsapi.azurewebsites.net

# How to use
If you want to run it by yourself, you should first configure SQL server in appsettings.json, or simply use the database stored in RAM by changing "inMemory" to true in that file.

# Example user accounts
Those are the default users the database gets initialized with.

[X] means one digit, for example donors pesel can be 51234567890 for fifth donor.


Donor:

 - pesel: [X]1234567890

 - password: [X]1234567890 (same as pesel)


Personnel:

 - pesel: [X]0987654321

 - password: [X]0987654321 (same as pesel)
