using NameSort.Domain;

namespace NameSort.Services
{
    public class SortingService : ISortingService
    {
        /// <summary>
        /// Sorts a list of names by surname by default. If sortBySurname is false, it sorts by given name.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="sortBySurname"></param>
        /// <returns></returns>
        public async Task<List<FullName>> SortNamesAsync(List<FullName> names, bool sortBySurname = true)
        {
            return await Task.Run(() => sortBySurname
                ? names.OrderBy(name => (name.SurName, name.GivenName)).ToList()
                : names.OrderBy(name => (name.GivenName, name.SurName)).ToList());
        }
    }
}
