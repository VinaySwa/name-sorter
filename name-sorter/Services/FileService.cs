using log4net;

namespace NameSort.Services
{
    public class FileService : IFileService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FileService));
        

        /// <summary>
        /// Reads a file
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <returns></returns>
        public async Task<List<string>> ReadFileAsync(string filePath)
        {
            try
            {
                return File.Exists(filePath) ? await File.ReadAllLinesAsync(filePath).ContinueWith(t => t.Result.ToList()) : new List<string>();
            }
            catch (Exception ex)
            {
                log.Error($"Error reading file-'{filePath}'", ex);
                return new List<string>();
            }
        }

        /// <summary>
        /// Writes to a file
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="contents">file content</param>
        public async Task WriteFileAsync(string filePath, List<string> contents)
        {
            try
            {
                await File.WriteAllLinesAsync(filePath, contents);
                log.Info($"File written successfully-{filePath}");
            }
            catch (Exception ex)
            {
                log.Error($"Error writing file-{filePath}", ex);
            }
        }
    }
}
