# MSTest data driven test

[![NuGet version](https://badge.fury.io/nu/Santhos.MSTest.svg)](https://badge.fury.io/nu/Santhos.MSTest)

Exposes a class that simplifies coding of data driven tests in MSTest.

## Usage

### Use TestData and Arrange method to perform a data driven test

```csharp
using Santhos.MSTest;

    [TestClass]
    public class SystemMathTest
    {
        [TestMethod]
        public void SystemMathMinReturnsCorrectValues()
        {
            TestData
                .Arrange(1, 2, 1)
                .Arrange(3, 4, 3)
                .ActAndAssert((int x, int y, int expected) =>
                {
                    var actual = System.Math.Min(x, y);

                    Assert.AreEqual(expected, actual, $"Failed for x {x}, y {y} and expected {expected}");
                });
        }
    }
```

### Use TestData and TestCase attributes to perform a data driven test

```csharp
using Santhos.MSTest;

    [TestClass]
    public class SystemMathTest
    {
        [TestMethod]
		[TestCase(1, 2, 1)]
		[TestCase(3, 4, 3)]
        public void SystemMathMinReturnsCorrectValues()
        {
            TestData.ActAndAssert((int x, int y, int expected) =>
            {
                var actual = System.Math.Min(x, y);

                Assert.AreEqual(expected, actual, $"Failed for x {x}, y {y} and expected {expected}");
            });
        }
    }
```

### Use TestData and external test case source to perform a data driven test

```csharp
using Santhos.MSTest;

    [TestClass]
    public class SystemMathTest
    {
        [TestMethod]
        public void SystemMathMinReturnsCorrectValues()
        {
            TestData
                .ArrangeTestCases(new MyTestCases())
                .ActAndAssert((int x, int y, int expected) =>
                {
                    var actual = System.Math.Min(x, y);

                    Assert.AreEqual(expected, actual, $"Failed for x {x}, y {y} and expected {expected}");
                });
        }
    }

	internal class MyTestCases : IEnumerable<IEnumerable<object>>
    {
        public IEnumerator<IEnumerable<object>> GetEnumerator()
        {
            yield return new List<object> { 1, 2, 1 };
            yield return new List<object> { 3, 4, 3 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
```
