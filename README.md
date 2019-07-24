# LazyCache.Moq
Moq set ups for LazyCache mocking.
If you want to mock the cache rather than use the fake provided by LazyCache, this library contains the moq set ups that you'll need to get going.
GetAsync<T> and GetOrAddAsync<T> methods supported.

# Usage

- Create the mock
- Set up the cache items
- Consume

```
[Test]
public void GetOrAdd_Guid_ReturnsExpectedResult() {
    var cacheKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();
	
    _cacheMock = MockHelper.CreateMock();
    _cacheMock.SetUpItem(cacheKey, expectedResult);

    var actualResult = _mockedCache.GetOrAdd<string>(cacheKey, () => expectedResult, DateTimeOffset.Now.AddMinutes(30));

    Assert.AreSame(expectedResult, actualResult);
}
```