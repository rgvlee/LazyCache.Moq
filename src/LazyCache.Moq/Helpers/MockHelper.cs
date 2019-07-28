using Moq;

namespace LazyCache.Moq.Helpers {
    /// <summary>
    /// Helper for mocks.
    /// </summary>
    public class MockHelper {
        /// <summary>
        /// Creates a lazy cache mock.
        /// </summary>
        /// <returns>A lazy cache mock.</returns>
        public static Mock<IAppCache> CreateLazyCacheMock() {
            var mock = new Mock<IAppCache>();

            var cacheDefaultsMock = new Mock<CacheDefaults>();
            mock.Setup(m => m.DefaultCachePolicy).Returns(cacheDefaultsMock.Object);

            mock.DefaultValueProvider = new LazyCacheDefaultValueProvider();

            return mock;
        }

        /// <summary>
        /// Gets a mocked lazy cache.
        /// </summary>
        /// <returns>A mocked lazy cache.</returns>
        public static IAppCache GetMockedLazyCache() {
            return CreateLazyCacheMock().Object;
        }
    }
}