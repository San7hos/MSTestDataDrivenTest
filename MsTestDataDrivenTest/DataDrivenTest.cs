//-------------------------------------------------------------------------------------
//<copyright file="DataDrivenTest.cs" company="Santhos.net">
//     Copyright(c) Santhos.net | All rights reserved
//</copyright>
//-------------------------------------------------------------------------------------

namespace Santhos.MSTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.ExceptionServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Represents a data driven test intended to be used with MSTest library
    /// </summary>
    public class DataDrivenTest
    {
        private readonly List<object[]> testCases = new List<object[]>();

        private int testCaseArgumentCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataDrivenTest"/> class.
        /// </summary>
        public DataDrivenTest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataDrivenTest"/> class.
        /// </summary>
        /// <param name="testCaseSet">Set of initial test cases</param>
        public DataDrivenTest(IEnumerable<IEnumerable<object>> testCaseSet)
        {
            this.ArrangeTestCases(testCaseSet);
        }

        /// <summary>
        /// Arranges a test case with given arguments
        /// </summary>
        /// <param name="testCase">Test case arguments (aka the test case)</param>
        /// <returns>Fluent API</returns>
        public DataDrivenTest Arrange(params object[] testCase)
        {
            if (testCase == null)
            {
                this.testCaseArgumentCount = 1;
                this.testCases.Add(new object[] { null });

                return this;
            }

            if (!this.testCases.Any())
            {
                this.testCaseArgumentCount = testCase.Length;
            }

            if (testCase.Length != this.testCaseArgumentCount)
            {
                Assert.Fail($"Test case [{string.Join(", ", testCase)}] has different number of arguments than the previous test cases.");
            }

            this.testCases.Add(testCase);

            return this;
        }

        /// <summary>
        /// Arranges a test case with given arguments
        /// </summary>
        /// <param name="testCase">Test case arguments (aka the test case)</param>
        /// <returns>Fluent API</returns>
        public DataDrivenTest Arrange(IEnumerable<object> testCase)
        {
            return this.Arrange(testCase.ToArray());
        }

        /// <summary>
        /// Arranges a test case with result of the given function
        /// </summary>
        /// <param name="testCase">Test case function that returns the test case (arguments)</param>
        /// <returns>Fluent API</returns>
        public DataDrivenTest Arrange(Func<IEnumerable<object>> testCase)
        {
            return this.Arrange(testCase());
        }

        /// <summary>
        /// Arranges multiple test cases at once
        /// </summary>
        /// <param name="testCaseSet">Set of test cases</param>
        /// <returns>FluentAPI</returns>
        public DataDrivenTest ArrangeTestCases(IEnumerable<IEnumerable<object>> testCaseSet)
        {
            foreach (var testCase in testCaseSet)
            {
                this.Arrange(testCase);
            }

            return this;
        }

        public DataDrivenTest ArrangeFromAttributes()
        {
            IEnumerable<object[]> testCases = new StackFrame(1)
                .GetMethod()
                .GetCustomAttributes<TestCaseAttribute>()
                .Select(a => a.TestCase);

            foreach (object[] testCase in testCases)
            {
                this.Arrange(testCase);
            }

            return this;
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert(Delegate actAndAssert)
        {
            foreach (object[] arguments in this.testCases)
            {
                try
                {
                    actAndAssert.DynamicInvoke(arguments);
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException != null)
                    {
                        // throw inner exception with its original stack trace
                        ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        #region Typed ActAndAssert

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1>(Action<T1> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2>(Action<T1, T2> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3>(Action<T1, T2, T3> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4>(Action<T1, T2, T3, T4> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        #endregion
    }
}
