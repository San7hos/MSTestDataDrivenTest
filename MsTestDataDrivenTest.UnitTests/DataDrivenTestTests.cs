// <copyright file="DataDrivenTestTests.cs" company="Santhos.net">
// Copyright (c) Santhos.net. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace MsTestDataDrivenTest.UnitTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Santhos.MSTest;

    [TestClass]
    public class DataDrivenTestTests
    {

        [TestMethod]
        public void EmptyConstructorSetsSkipStackFramesCorrectly()
        {
            var sut = new DataDrivenTest();

            Assert.AreEqual(1, sut.SkipStackFrames);
        }

        [TestMethod]
        public void ConstructorSetsSkipStackFramesCorrectly()
        {
            var expectedSkipStackFrames = 123;

            var sut = new DataDrivenTest(expectedSkipStackFrames);

            Assert.AreEqual(expectedSkipStackFrames, sut.SkipStackFrames);
        }

        [TestMethod]
        public void ArrangeWithNoArgumentsSetsTestCaseCorrectly()
        {
            var sut = new DataDrivenTest().Arrange();

            Assert.AreEqual(1, sut.TestCases.Count, "There should only be a single test case");
            Assert.AreEqual(0, sut.TestCaseArgumentCount, "The test case should not have any arguments");
            Assert.AreEqual(0, sut.TestCases.First().Length, "There should not be any arguments in the first test case");
        }

        [TestMethod]
        public void ArrangeSetsTestCasesCorrectly()
        {
            var sut = new DataDrivenTest()
                .Arrange("test", "case")
                .Arrange("test 2", "case 2")
                .Arrange("test 3", "case 3");

            Assert.AreEqual(3, sut.TestCases.Count, "There should be three test cases");
            Assert.AreEqual(2, sut.TestCaseArgumentCount, "Each test case should have two arguments");
            Assert.AreEqual("case 2", sut.TestCases.ElementAt(1)[1], "Incorrect value in the second argument of the second test case");
        }

        [TestMethod]
        public void ArrangeGenericIEnumerableSetsTestCaseCorrectly()
        {
            IEnumerable<object> expected = new List<object> { "test", "case" };

            var sut = new DataDrivenTest().Arrange(expected);

            Assert.AreEqual(1, sut.TestCases.Count, "There should only be a single test case");
            Assert.AreEqual(2, sut.TestCaseArgumentCount, "The test case should have two arguments");
            Assert.AreEqual("test", sut.TestCases.First().First(), "Incorrect value of the first argument");
        }

        [TestMethod]
        public void ArrangeFuncSetsTestCaseCorrectly()
        {
            Func<IEnumerable<object>> expected = () => new List<object> { "test", "case" };

            var sut = new DataDrivenTest().Arrange(expected);

            Assert.AreEqual(1, sut.TestCases.Count, "There should only be a single test case");
            Assert.AreEqual(2, sut.TestCaseArgumentCount, "The test case should have two arguments");
            Assert.AreEqual("test", sut.TestCases.First().First(), "Incorrect value of the first argument");
        }

        [TestMethod]
        public void ArrangeThrowsWhenTestCasesHaveDifferentArgumentCount()
        {
            var expected = "Assert.Fail failed. Test case [test case, throws] has different number of arguments than the previous test cases.";
            try
            {
                new DataDrivenTest().Arrange("test case").Arrange("test case", "throws");
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public void ArrangeTestCasesSetsTestCasesCorrectly()
        {
            var expected = new List<List<object>>
            {
                new List<object> { "test", "case" },
                new List<object> { "test 2", "case 2" },
                new List<object> { "test 3", "case 3" }
            };

            var sut = new DataDrivenTest().ArrangeTestCases(expected);

            Assert.AreEqual(3, sut.TestCases.Count, "There should be three test cases");
            Assert.AreEqual(2, sut.TestCaseArgumentCount, "Each test case should have two arguments");
            Assert.AreEqual("case 2", sut.TestCases.ElementAt(1)[1], "Incorrect value in the second argument of the second test case");
        }

        [TestMethod]
        public void ArrangeTestCasesFuncSetsTestCasesCorrectly()
        {
            Func<IEnumerable<IEnumerable<object>>> expected = () => new List<List<object>>
            {
                new List<object> { "test", "case" },
                new List<object> { "test 2", "case 2" },
                new List<object> { "test 3", "case 3" }
            };

            var sut = new DataDrivenTest().ArrangeTestCases(expected);

            Assert.AreEqual(3, sut.TestCases.Count, "There should be three test cases");
            Assert.AreEqual(2, sut.TestCaseArgumentCount, "Each test case should have two arguments");
            Assert.AreEqual("case 2", sut.TestCases.ElementAt(1)[1], "Incorrect value in the second argument of the second test case");
        }

        [TestMethod]
        public void ArrangeTestCasesThrowsWhenTestCasesHaveDifferentArgumentCount()
        {
            var expected = "Assert.Fail failed. Test case [test case, throws] has different number of arguments than the previous test cases.";
            try
            {
                new DataDrivenTest().ArrangeTestCases(new List<List<object>>
                {
                    new List<object> {"test case"},
                    new List<object> {"test case", "throws"}
                });
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        [TestCase("test")]
        public void ArrangeFromAttributesSetsTestCaseCorrectly()
        {
            var sut = new DataDrivenTest().ArrangeFromAttributes();

            Assert.AreEqual(1, sut.TestCases.Count, "There should only be a single test case");
            Assert.AreEqual(1, sut.TestCaseArgumentCount, "The test case should only have one argument");
            Assert.AreEqual("test", sut.TestCases.First().First(), "Incorrect argument value");
        }

        [TestMethod]
        [TestCase("test")]
        [TestCase("test", "exception")]
        public void ArrangeFromAttributesThrowsWhenTestCasesHaveDifferentArgumentCount()
        {
            var expected = "Assert.Fail failed. Test case [test, exception] has different number of arguments than the previous test cases.";
            try
            {
                new DataDrivenTest().ArrangeFromAttributes();
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public void ActAndAssertIsCalledCorrectly()
        {
            var sut = new DataDrivenTest()
                .Arrange("test 0", "case 0")
                .Arrange("test 1", "case 1");

            var actualCallCount = 0;
            var act = new Action<string, string>((test, expected) =>
            {
                Assert.AreEqual(test, $"test {actualCallCount}");
                Assert.AreEqual(expected, $"case {actualCallCount}");

                actualCallCount++;
            });

            sut.ActAndAssert(act);

            Assert.AreEqual(2, actualCallCount, "Act should be called twice");
        }

        [TestMethod]
        public void ActAndAssertThrowsWhenNoTestCasesHaveBeenArranged()
        {
            var expected = "Assert.Fail failed. There are no test cases. Have you forgotten to arrange them?";
            try
            {
                var sut = new DataDrivenTest();
                sut.ActAndAssert((object x) => { });
            }
            catch (AssertFailedException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ActAndAssertThrows()
        {
            var sut = new DataDrivenTest().Arrange("testcase");
            sut.ActAndAssert((string x) => { throw new InvalidOperationException(); });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ActAndAssertThrowsWhenActAndAssertDelegateIsNull()
        {
            var sut = new DataDrivenTest();
            sut.ActAndAssert(null);
        }
    }
}
