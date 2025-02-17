using NameSort.Domain;
using log4net;
using NameSort.Services;

namespace NameSort
{
    public class NameSorter
    {
        private readonly ISortingService _sortingService;
        private readonly IFileService _fileService;
        private static readonly ILog log = LogManager.GetLogger(typeof(NameSorter));

        public NameSorter(ISortingService sortingService, IFileService fileService)
        {
            _sortingService = sortingService;
            _fileService = fileService;
        }

        /// <summary>
        /// Execute the name sorting
        /// </summary>
        /// <param name="filePath"></param>
        public async Task ExecuteAsync(string filePath)
        {
            try
            {
                var message = string.Empty;
                if (string.IsNullOrEmpty(filePath))
                {
                    message = "File path is required.";
                    Console.WriteLine(message);
                    log.Error(message);
                    return;
                }
                var names = await _fileService.ReadFileAsync(filePath);                
                if (!names.Any())
                {
                    message = $"File not found or empty: {filePath}";
                    Console.WriteLine(message);
                    log.Error(message);
                    return;
                }

                var fullNames = names.Select(ParseFullName).ToList();
                var sortedNames = await _sortingService.SortNamesAsync(fullNames);

                Console.WriteLine("-------------------------------------------------------------");
                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine("-------------------------------------------------------------");

                var sortedFileName = "sorted-names-list.txt";
                await _fileService.WriteFileAsync(sortedFileName, sortedNames.Select(n => n.ToString()).ToList());
                message = $"A new file, '{sortedFileName}' has been created with the sorted names.";
                Console.WriteLine(message);
                log.Info(message);
            }
            catch (Exception ex) 
            {
                log.Error(ex);
                Console.WriteLine($"Error - {ex.Message}");
            }
        }

        /// <summary>
        /// Parse a full name into a FullName object
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private FullName ParseFullName(string fullName)
        {
            var names = fullName.Trim().Split(' ');
            if (names.Length < 2 || names.Length > 4)
            {
                var message = $"Invalid '{fullName}' - name format. Each name must have 1 surname and up to 3 given names.";
                log.Error(message);
                throw new ArgumentException(message);
            }
            return new FullName(string.Join(" ", names.Take(names.Length - 1)), names.Last());
        }
    }
}
