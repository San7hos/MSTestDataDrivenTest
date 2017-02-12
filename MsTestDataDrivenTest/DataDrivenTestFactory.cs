//-------------------------------------------------------------------------------------
//<copyright file="DataDrivenTestFactory.cs" company="Santhos.net">
//     Copyright(c) Santhos.net | All rights reserved
//</copyright>
//-------------------------------------------------------------------------------------

namespace Santhos.MSTest
{
    using System.Collections.Generic;

    /// <summary>
    /// Factory to create data driven tests
    /// </summary>
    public static class DataDrivenTestFactory
    {
        /// <summary>
        /// Creates a data driven test, basically an alias for 'new DataDrivenTest()'
        /// </summary>
        /// <returns>A data driven test</returns>
        public static DataDrivenTest Create()
        {
            return new DataDrivenTest();
        }

        /// <summary>
        /// Creates a data driven test and arranges given test case
        /// </summary>
        /// <param name="testCase">The first test case to arrange</param>
        /// <returns>A data driven test with one test case</returns>
        public static DataDrivenTest Arrange(params object[] testCase)
        {
            return new DataDrivenTest().Arrange(testCase);
        }

        /// <summary>
        /// Creates a data driven test and arranges test cases that 
        /// are specified by the test case set
        /// </summary>
        /// <param name="testCaseSet">Set of test cases</param>
        /// <returns>A data driven test with arranged test cases</returns>
        public static DataDrivenTest ArrangeTestCases(IEnumerable<IEnumerable<object>> testCaseSet)
        {
            return new DataDrivenTest().ArrangeTestCases(testCaseSet);
        }
    }
}
