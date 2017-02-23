// <copyright file="FullBehaviourTests.cs" company="Santhos.net">
// Copyright (c) Santhos.net. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace MsTestDataDrivenTest.UnitTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Santhos.MSTest;

    [TestClass]
    public class FullBehaviourTests
    {
        [TestMethod]
        public void DataDrivenTestPassesCorrectly()
        {
            Func<int, int, int> correctAdder = (x, y) => x + y;

            TestData
                .Arrange(1, 1, 2)
                .Arrange(1, 2, 3)
                .ActAndAssert((int a, int b, int expected) =>
                {
                    var sut = correctAdder;
                    int actual = sut(a, b);

                    Assert.AreEqual(actual, expected);
                });
        }

        [TestMethod]
        public void DataDrivenTestFailsCorrectly()
        {
            var expectedMessage = "Assert.AreEqual failed. Expected:<4>. Actual:<3>. ";
            try
            {
                var epicFail = 0;
                Func<int, int, int> brokenAdder = (x, y) => x + y + epicFail++;

                TestData
                    .Arrange(1, 1, 2)
                    .Arrange(1, 2, 3)
                    .ActAndAssert((int a, int b, int expected) =>
                    {
                        var sut = brokenAdder;
                        int actual = sut(a, b);

                        Assert.AreEqual(actual, expected);
                    });
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        public void DataDrivenTestPassesCorrectlyWhenUsingTestCaseSourceClass()
        {
            var actualCalls = 0;
            TestData
                .ArrangeTestCases(new MyTestCases())
                .ActAndAssert((int x, int y, int expected) =>
                {
                    var actual = Math.Min(x, y);

                    Assert.AreEqual(expected, actual, $"Failed for x {x}, y {y} and expected {expected}");

                    actualCalls++;
                });

            Assert.AreEqual(2, actualCalls, "Act should be called twice");
        }

        private class MyTestCases : IEnumerable<IEnumerable<object>>
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
    }
}
