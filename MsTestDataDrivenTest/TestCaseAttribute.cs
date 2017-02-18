//-------------------------------------------------------------------------------------
//<copyright file="DataDrivenTest.cs" company="Santhos.net">
//     Copyright(c) Santhos.net | All rights reserved
//</copyright>
//-------------------------------------------------------------------------------------

namespace Santhos.MSTest
{
    using System;

    /// <summary>
    /// Defines a test case
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class TestCaseAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseAttribute"/> class.
        /// </summary>
        /// <param name="testCase">Test case arguments (aka the test case)</param>
        public TestCaseAttribute(params object[] testCase)
        {
            this.TestCase = testCase;
        }

        /// <summary>
        /// Gets the test case
        /// </summary>
        public object[] TestCase { get; }
    }
}