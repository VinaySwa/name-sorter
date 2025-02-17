using NameSort.Domain;
using NameSort.Services;

namespace name_sorter_tests
{
    public class SortingServiceTests
    {
        private readonly ISortingService _sortingService = new SortingService();

        [Fact]
        public async Task SortNames_EmptyList_ShouldReturnEmptyList()
        {
            var names = new List<FullName>();
            var sorted = await _sortingService.SortNamesAsync(names, sortBySurname: true);
            sorted.Count.Equals(0);
        }

        [Fact]
        public async Task SortNames_SortsBySurnameCorrectly()
        {
            var names = new List<FullName>
            {
                 new FullName("Janet", "Parsons"),
                 new FullName("Vaughn", "Lewis"),
                 new FullName("Adonis Julius", "Archer"),
                 new FullName("Shelby Nathan", "Yoder"),
                 new FullName("Marin", "Alvarez"),
                 new FullName("London", "Lindsey"),
                 new FullName("Beau Tristan", "Bentley"),
                 new FullName("Leo", "Gardner"),
                 new FullName("Hunter Uriah Mathew", "Clarke"),
                 new FullName("Mikayla", "Lopez"),
                 new FullName("Frankie Conner", "Ritter")
            };

            var sorted = await _sortingService.SortNamesAsync(names, sortBySurname: true);

            Assert.Equal("Marin Alvarez", sorted[0].ToString());
            Assert.Equal("Adonis Julius Archer", sorted[1].ToString());
            Assert.Equal("Beau Tristan Bentley", sorted[2].ToString());
            Assert.Equal("Hunter Uriah Mathew Clarke", sorted[3].ToString());
            Assert.Equal("Leo Gardner", sorted[4].ToString());
            Assert.Equal("Vaughn Lewis", sorted[5].ToString());
            Assert.Equal("London Lindsey", sorted[6].ToString());
            Assert.Equal("Mikayla Lopez", sorted[7].ToString());
            Assert.Equal("Janet Parsons", sorted[8].ToString());
            Assert.Equal("Frankie Conner Ritter", sorted[9].ToString());
            Assert.Equal("Shelby Nathan Yoder", sorted[10].ToString());
        }

        [Fact]
        public async Task SortNames_SortsByGivenNameCorrectly()
        {
            var names = new List<FullName>
            {
                 new FullName("Janet", "Parsons"),
                 new FullName("Vaughn", "Lewis"),
                 new FullName("Adonis Julius", "Archer"),
                 new FullName("Shelby Nathan", "Yoder"),
                 new FullName("Marin", "Alvarez"),
                 new FullName("London", "Lindsey"),
                 new FullName("Beau Tristan", "Bentley"),
                 new FullName("Leo", "Gardner"),
                 new FullName("Hunter Uriah Mathew", "Clarke"),
                 new FullName("Mikayla", "Lopez"),
                 new FullName("Frankie Conner", "Ritter")
            };

            var sorted = await _sortingService.SortNamesAsync(names, sortBySurname: false);

            Assert.Equal("Adonis Julius Archer", sorted[0].ToString());
            Assert.Equal("Beau Tristan Bentley", sorted[1].ToString());
            Assert.Equal("Frankie Conner Ritter", sorted[2].ToString());
            Assert.Equal("Hunter Uriah Mathew Clarke", sorted[3].ToString());
            Assert.Equal("Janet Parsons", sorted[4].ToString());
            Assert.Equal("Leo Gardner", sorted[5].ToString());
            Assert.Equal("London Lindsey", sorted[6].ToString());
            Assert.Equal("Marin Alvarez", sorted[7].ToString());
            Assert.Equal("Mikayla Lopez", sorted[8].ToString());
            Assert.Equal("Shelby Nathan Yoder", sorted[9].ToString());
            Assert.Equal("Vaughn Lewis", sorted[10].ToString());
        }
    }
}