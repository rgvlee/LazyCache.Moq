using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace LazyCache.Moq.Tests {
    [TestFixture]
    public class Tests {
        private Mock<IAppCache> _cacheMock;
        private IAppCache _mockedCache => _cacheMock.Object;

        [SetUp]
        public void Setup() {
            _cacheMock = MockHelper.CreateMock();
        }

        [Test]
        public void GetOrAdd_Guid_ReturnsExpectedResult() {
            var cacheKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();
            _cacheMock.SetUpItem(cacheKey, expectedResult);

            var actualResult = _mockedCache.GetOrAdd<string>(cacheKey, () => expectedResult, DateTimeOffset.Now.AddMinutes(30));

            Assert.AreSame(expectedResult, actualResult);
        }

        [Test]
        public async Task GetOrAddAsync_Guid_ReturnsExpectedResult() {
            var cacheKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();
            _cacheMock.SetUpItem(cacheKey, expectedResult);

            var actualResult = await _mockedCache.GetOrAddAsync<string>(cacheKey, () => Task.FromResult(expectedResult));

            Assert.AreSame(expectedResult, actualResult);
        }

        [Test]
        public void GetWithEmptyCache_ReturnsNull() {
            var cacheKey = "SomethingInTheCache";

            var actualResult = _mockedCache.Get<string>(cacheKey);

            Assert.IsNull(actualResult);
        }
    }
}