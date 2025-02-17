using Microsoft.Extensions.DependencyInjection;
using NameSort;
using NameSort.Services;
using log4net;
using log4net.Config;

// Configure log4net
XmlConfigurator.Configure(new FileInfo("App.config"));
var log = LogManager.GetLogger(typeof(Program));

Console.WriteLine("-----------------Name Sorter---------------");
Console.WriteLine("Executing the program in the following way:");
Console.WriteLine("name-sorter ./unsorted-names-list.txt");

if (args.Length == 0)
{
    Console.WriteLine("Usage: name-sorter <file-path>");
    log.Error("No file path provided. Exiting application.");
    return;
}
var filePath = args[0];

var serviceProvider = new ServiceCollection()
    .AddSingleton<ISortingService, SortingService>()
    .AddSingleton<IFileService, FileService>()
.BuildServiceProvider();

log.Info($"Processing file: {filePath}");
var nameSorter = new NameSorter(serviceProvider.GetService<ISortingService>(), serviceProvider.GetService<IFileService>());
await nameSorter.ExecuteAsync(filePath);
log.Info("Processing completed.");

