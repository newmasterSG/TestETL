using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using TestETL.Application.Services;
using TestETL.Infrastructure.Repository;
using TestETL.Infrastructure.Db;

namespace TestETL.Program
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start working module");

            Stopwatch stopwatch = Stopwatch.StartNew();

            string pathProject = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            string pathToFolder = Path.Combine(pathProject, "Data");
            string pathToFile = Path.Combine(pathToFolder, "sample-cab-data.csv");

            using var dbcontext = new EtldbContext();
            dbcontext.Database.Migrate();
            var csvRep = new CsvRepository(dbcontext);
            var csvService = new CsvService(csvRep);
            await csvService.AddDataToDb(pathToFile);

            stopwatch.Stop();

            // Print elapsed time
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");

            Console.WriteLine("End working module");
        }
    }
}
