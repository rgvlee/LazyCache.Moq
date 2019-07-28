# LazyCache.Moq
__*A mocking library for LazyCache*__

If you want to mock the cache rather than use the built-in fake provided by LazyCache, this is the library you need.

## But why? LazyCache already provides a test class to do this?
Yes, LazyCache does provide the MockCachingService class for unit testing. It works as advertised for most cases __however__ it doesn't work for all.

If you're using Get\<T> you're out of luck. You'll get nulls.

LazyCache.Moq is the cake and eat it too solution. It provides:
- out of the box functionality if you are only using GetOrAdd and GetOrAddAsync. No explicit set up required, it just works.
- If you do need support for other methods e.g., Get\<T>, use the included fluent extension method to do so.
- Access to all good stuff that Moq provides such as ```Verify```. 

## Resources
- [Source repository](https://github.com/rgvlee/LazyCache.Moq)
- [NuGet](https://www.nuget.org/packages/LazyCache.Moq/)

## The disclaimer
I have built this library based on my current needs. The mocking doesn't extend to the CacheProvider so if you're hitting that explicitly then you're in for a world of nulls. I might add it in a later release but in general I think accessing the CacheProvider in the test subjects should be avoided. If you find this library useful and something is missing, not working as you'd expect or you need additional behaviour mocked flick me a message and I'll see what I can do.

## Fluent interface
It's a small library requiring very little to get going. That being said I've provided a fluent interface for the cache item set up for ease of use.

## Basic usage
- Get a mocked lazy cache
- Consume

```
[Test]
public void MinimumViableInterface_Guid_ReturnsExpectedResult() {
    var cacheKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();

    var mockedCache = MockHelper.GetMockedLazyCache();
            
    var actualResult = mockedCache.GetOrAdd(cacheKey, () => expectedResult, DateTimeOffset.Now.AddMinutes(30));

    Assert.AreSame(expectedResult, actualResult);
}
```

## But I want the cache mock
No problem. Use the mock helper to create the mock. At this point it's a Mock\<IAppCache> for you to specify additional set ups, assert verify method invocations etc.

```
 [Test]
public void GetOrAddWithNoSetUp_TestObject_ReturnsExpectedResult() {
    var cacheKey = "SomethingInTheCache";
    var expectedResult = new TestObject();

    var cacheMock = MockHelper.CreateLazyCacheMock();
    var mockedCache = cacheMock.Object;

    var actualResult = mockedCache.GetOrAdd(cacheKey, () => expectedResult, DateTimeOffset.Now.AddMinutes(30));

    Assert.Multiple(() => {
        Assert.AreSame(expectedResult, actualResult);
        cacheMock.Verify(m => m.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<ICacheEntry, TestObject>>()), Times.Once);
    });
}
```

## Let's get explicit
If you want to explicitly specify a cache item set up, use the fluent extension method.

```
[Test]
public void GetOrAddWithSetUp_Guid_ReturnsExpectedResult() {
    var cacheKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();

    var cacheMock = MockHelper.CreateLazyCacheMock();
    cacheMock.SetUpCacheItem(cacheKey, expectedResult);
    var mockedCache = cacheMock.Object;

    var actualResult = mockedCache.GetOrAdd(cacheKey, () => expectedResult, DateTimeOffset.Now.AddMinutes(30));

    Assert.AreSame(expectedResult, actualResult);
}
```

## I'm using Get\<T>, what do I need to do?
You'll need to use the explicit set up as described above - the mock needs to know what to return.

## Async? Please tell me you support the async methods.
The survey says, yes the async methods are supported. You're welcome.