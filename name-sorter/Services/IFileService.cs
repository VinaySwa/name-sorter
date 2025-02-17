namespace NameSort.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Reads a file
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <returns></returns>
        public Task<List<string>> ReadFileAsync(string filePath);

        /// <summary>
        /// Writes to a file
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="contents">file content</param>
        public Task WriteFileAsync(string filePath, List<string> contents);
    }
}
