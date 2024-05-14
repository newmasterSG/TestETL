# TestETL

Since the database was created initially, I used the database first approach.

This line should create the dbcontext as it is in ms sql

Scaffold-DbContext ‘Server=.\SQLExpress;Database=ETLDb;Trusted_Connection=True;TrustServerCertificate=true;’ Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

# Ansewers to questions:
 
1. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file. 

In my opinion you can start to do through parallel foreach the creation of dto, also do more batch, like ef core supports this. If you need to read data, you can do it in such a way that we find out how many records there are and batch depending on it, you can also use semaphore.

2. Number of rows after running program - 29889 rows
