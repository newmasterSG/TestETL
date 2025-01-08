# TestETL

# Ansewers to questions:
 
1. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file. 

In my opinion you can start to do through parallel foreach the creation of dto, also do more batch, like ef core supports this. If you need to read data, you can do it in such a way that we find out how many records there are and batch depending on it, you can also use semaphore.

2. Number of rows after running program - 29889 rows
