VIN PROCESSING API

This is an API that provides functionality for bulk processing of VIN data, storage of the results, and search/retrieval for the data.

USAGE

Clone, compile, and configure the app settings file. Settings are defined for the csv file dump and a log directory. The application uses a
code-first approach to definind the data layer, and will create the database and tables on first run. 

To process data, POST to /Vin/ with the data provided as form-data in the body, named csvVinList. If the API can locate the file defined in
the TestFile setting in appsettings, it will instead use that. 

ARCHITECTURAL NOTES

Written in C# with an MSSQL database. I went with a clean architecture design owing to familiarity. I elected not to create separate
projects for each of the layers due to this being a demonstration project, it would have been overkill. 

I used the CQRS pattern for database access for a few reasons. I like the idea of the pattern, using multiple ORMs and playing to the 
strengths of each, and wanted to try it out. I enjoy working with Dapper, and I wanted to get some practice in with Entity Framework Core.
I haven't used it much and wanted to learn more about working with it. 

I opted to put submitted VIN data into a queue table instead of processing it immediately. I didn't want to block the API call on the NHTSA
website, and processing it in a queue is more resilient and makes it easier to work around duplicate entries. This does introduce some small
latency between submitting the VIN data and being able to retrieve it, but the use case described to me for this API did not seem to me one
that would be sensitive to that latency. If submission volumes were to increase to the point that the queue couldn't keep up, I believe the 
code is written in such a way that it would be easy to scale with demand. The size of the queue batch could be increased, and multiple
workers could be introduced without issue. The NHTSA's rate limit could start to become a concern, though, and that was unaccounted for in 
this API.

I did not have time to implement authentication. Given the stated use case as an internal application, I would have integrated with Windows AD
for authentication. 
