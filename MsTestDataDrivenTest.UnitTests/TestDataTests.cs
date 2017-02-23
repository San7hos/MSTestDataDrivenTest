// <copyright file="TestDataTests.cs" company="Santhos.net">
// Copyright (c) Santhos.net. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace MsTestDataDrivenTest.UnitTests
{
    using Santhos.MSTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestDataTests
    {
        [TestMethod]
        public void ArrangeCreatesDataDrivenTestCorrectly()
        {
            var actual = TestData.Arrange("test", "case");

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.TestCases.Count, "There should be a single test case");
            Assert.AreEqual(2, actual.TestCaseArgumentCount, "The test case should have two arguments");
            Assert.AreEqual("case", actual.TestCases.First()[1], "Incorrect value of the second argument");
        }

        [TestMethod]
        public void ArrangeTestCasesCreatesDataDrivenTestCorrectly()
        {
            var expected = new List<List<object>>
            {
                new List<object> { "test", "case" },
                new List<object> { "test 2", "case 2" },
                new List<object> { "test 3", "case 3" }
            };

            var actual = TestData.ArrangeTestCases(expected);

            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.TestCases.Count, "There should be a single test case");
            Assert.AreEqual(2, actual.TestCaseArgumentCount, "Each test case should have two arguments");
            Assert.AreEqual("test 2", actual.TestCases.ElementAt(1)[0], "Incorrect value of the first argument of the second test case");
        }

        [TestMethod]
        [TestCase("test", "case")]
        [TestCase("test 2", "case 2")]
        [TestCase("test 3", "case 3")]
        public void ArrangeFromAttributesCreatesDataDrivenTestCorrectly()
        {
            var actual = TestData.ArrangeFromAttributes();

            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.TestCases.Count, "There should be a single test case");
            Assert.AreEqual(2, actual.TestCaseArgumentCount, "Each test case should have two arguments");
            Assert.AreEqual("test 2", actual.TestCases.ElementAt(1)[0], "Incorrect value of the first argument of the second test case");
        }
    }
}
