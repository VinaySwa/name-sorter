using NameSort.Domain;

namespace NameSort.Services
{
    public interface ISortingService
    {
        /// <summary>
        /// Sorts a list of names by surname by default. If sortBySurname is false, it sorts by given name. 
        /// </summary>
        /// <param name="names"></param>
        /// <param name="sortBySurname"></param>
        /// <returns></returns>
        public Task<List<FullName>> SortNamesAsync(List<FullName> names, bool sortBySurname = true);
    }
}
