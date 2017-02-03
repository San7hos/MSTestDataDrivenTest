# MSTest data driven test

[![NuGet version](https://badge.fury.io/nu/Santhos.MSTest.svg)](https://badge.fury.io/nu/Santhos.MSTest)

Exposes a class that simplifies coding of data driven tests in MSTest.

## Usage

### In a test method

Create a data driven test in a TestMethod:

```csharp
using Santhos.MSTest;

    [TestClass]
    public class SystemMathTest
    {
        [TestMethod]
        public void SystemMathMinReturnsCorrectValues()
        {
            new DataDrivenTest()
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
