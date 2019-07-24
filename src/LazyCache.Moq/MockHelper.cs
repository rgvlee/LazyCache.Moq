using Moq;

namespace LazyCache.Moq {
    public class MockHelper {
        public static Mock<IAppCache> CreateMock() {
            var mock = new Mock<IAppCache>();

            var cacheDefaultsMock = new Mock<CacheDefaults>();
            mock.Setup(m => m.DefaultCachePolicy).Returns(cacheDefaultsMock.Object);

            return mock;
        }
    }
}