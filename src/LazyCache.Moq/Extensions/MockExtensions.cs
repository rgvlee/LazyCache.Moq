using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace LazyCache.Moq.Extensions {
    /// <summary>
    /// Extensions for mocks.
    /// </summary>
    public static class MockExtensions {
        /// <summary>
        /// Sets up a cache item.
        /// </summary>
        /// <typeparam name="T">The cache item type.</typeparam>
        /// <param name="cacheMock">The cache mock instance.</param>
        /// <param name="key">The cache item key.</param>
        /// <param name="item">The cache item factory.</param>
        /// <returns>The cache item.</returns>
        public static Mock<IAppCache> SetUpCacheItem<T>(this Mock<IAppCache> cacheMock, string key, T item) {
            Console.WriteLine($"Setting up cache item '{key}': '{item.ToString()}'");

            cacheMock.Setup(m => m.Add<T>(
                    It.Is<string>(s => s.Equals(key)),
                    It.IsAny<T>(),
                    It.IsAny<MemoryCacheEntryOptions>()))
                .Callback(() => Console.WriteLine("Cache Add invoked"));

            cacheMock.Setup(m => m.Get<T>(
                    It.Is<string>(s => s.Equals(key))))
                .Callback(() => Console.WriteLine("Cache Get invoked"))
                .Returns(item);

            cacheMock.Setup(m => m.GetOrAdd<T>(
                    It.IsAny<string>(),
                    It.IsAny<Func<ICacheEntry, T>>()))
                .Callback(() => Console.WriteLine("Cache GetOrAdd invoked"))
                .Returns(item);

            cacheMock.Setup(m => m.GetAsync<T>(
                    It.Is<string>(s => s.Equals(key))))
                .Callback(() => Console.WriteLine("Cache GetAsync invoked"))
                .Returns(Task.FromResult(item));

            cacheMock.Setup(m => m.GetOrAddAsync<T>(
                    It.IsAny<string>(),
                    It.IsAny<Func<ICacheEntry, Task<T>>>()))
                .Callback(() => Console.WriteLine("Cache GetOrAddAsync invoked"))
                .Returns(Task.FromResult(item));

            cacheMock.Setup(m => m.Remove(
                    It.Is<string>(s => s.Equals(key))))
                .Callback(() => Console.WriteLine("Cache Remove invoked"));

            return cacheMock;
        }
    }
}
