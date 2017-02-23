// <copyright file="TestData.cs" company="Santhos.net">
// Copyright (c) Santhos.net. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Santhos.MSTest
{
    using System.Collections.Generic;

    /// <summary>
    /// Data driven test factory
    /// The naming is as short as possible
    /// </summary>
    public class TestData
    {
        private const int SkipStackFrames = 2;

        /// <summary>
        /// Creates a data driven test and arranges given test case
        /// </summary>
        /// <param name="testCase">The first test case to arrange</param>
        /// <returns>A data driven test with one test case</returns>
        public static DataDrivenTest Arrange(params object[] testCase)
        {
            return new DataDrivenTest(SkipStackFrames).Arrange(testCase);
        }

        /// <summary>
        /// Creates a data driven test and arranges test cases that
        /// are specified by the test case set
        /// </summary>
        /// <param name="testCaseSet">Set of test cases</param>
        /// <returns>A data driven test with arranged test cases</returns>
        public static DataDrivenTest ArrangeTestCases(IEnumerable<IEnumerable<object>> testCaseSet)
        {
            return new DataDrivenTest(SkipStackFrames).ArrangeTestCases(testCaseSet);
        }

        /// <summary>
        /// Creates a data driven test and arranges (multiple) test cases from <see cref="TestCaseAttribute"/>s
        /// </summary>
        /// <returns>A data driven test with arranged test cases</returns>
        public static DataDrivenTest ArrangeFromAttributes()
        {
            return new DataDrivenTest(SkipStackFrames).ArrangeFromAttributes();
        }
    }
}