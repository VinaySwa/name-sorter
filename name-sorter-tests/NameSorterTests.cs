using NameSort.Domain;
using NameSort.Services;
using NameSort;
using Moq;

namespace name_sorter_tests
{
    public class NameSorterTests
    {
        private readonly Mock<ISortingService> _mockSortingService = new Mock<ISortingService>();
        private readonly Mock<IFileService> _mockFileService = new Mock<IFileService>();

        [Fact]
        public async Task ExecuteAsync_NoFilePath_NoSortingAndWriteingToFileAsync()
        {
            _mockFileService.Setup(f => f.ReadFileAsync("test.txt")).ReturnsAsync(new List<string>());
            _mockSortingService.Setup(s => s.SortNamesAsync(It.IsAny<List<FullName>>(), true)).ReturnsAsync(new List<FullName>());

            var nameSorter = new NameSorter(_mockSortingService.Object, _mockFileService.Object);
            await nameSorter.ExecuteAsync(string.Empty);

            _mockFileService.Verify(f => f.ReadFileAsync(string.Empty), Times.Never);
            _mockSortingService.Verify(s => s.SortNamesAsync(It.IsAny<List<FullName>>(), true), Times.Never);
            _mockFileService.Verify(f => f.WriteFileAsync("sorted-names-list.txt", It.IsAny<List<string>>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_NoNames_NoSortingAndWriteingToFileAsync()
        {
            var names = new List<string>();
            var fullNames = names.Select(n => {
                var parts = n.Split(' ');
                return new FullName(string.Join(" ", parts.Take(parts.Length - 1)), parts.Last());
            }).ToList();

            var sortedNames = fullNames.OrderBy(n => n.SurName).ToList();

            _mockFileService.Setup(f => f.ReadFileAsync("test.txt")).ReturnsAsync(names);
            _mockSortingService.Setup(s => s.SortNamesAsync(It.IsAny<List<FullName>>(), true)).ReturnsAsync(sortedNames);

            var nameSorter = new NameSorter(_mockSortingService.Object, _mockFileService.Object);
            await nameSorter.ExecuteAsync("test.txt");

            _mockFileService.Verify(f => f.ReadFileAsync("test.txt"), Times.Once);
            _mockSortingService.Verify(s => s.SortNamesAsync(It.IsAny<List<FullName>>(), true), Times.Never);
            _mockFileService.Verify(f => f.WriteFileAsync("sorted-names-list.txt", It.IsAny<List<string>>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_SortsAndWritesToFileAsync()
        {
            var names = new List<string> { "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer" };
            var fullNames = names.Select(n => {
                var parts = n.Split(' ');
                return new FullName(string.Join(" ", parts.Take(parts.Length - 1)), parts.Last());
            }).ToList();

            var sortedNames = fullNames.OrderBy(n => n.SurName).ToList();

            _mockFileService.Setup(f => f.ReadFileAsync("test.txt")).ReturnsAsync(names);
            _mockSortingService.Setup(s => s.SortNamesAsync(It.IsAny<List<FullName>>(), true)).ReturnsAsync(sortedNames);

            var nameSorter = new NameSorter(_mockSortingService.Object, _mockFileService.Object);
            await nameSorter.ExecuteAsync("test.txt");

            _mockFileService.Verify(f => f.ReadFileAsync("test.txt"), Times.Once);
            _mockSortingService.Verify(s => s.SortNamesAsync(It.IsAny<List<FullName>>(), true), Times.Once);
            _mockFileService.Verify(f => f.WriteFileAsync("sorted-names-list.txt", It.IsAny<List<string>>()), Times.Once);
        }
    }
}
