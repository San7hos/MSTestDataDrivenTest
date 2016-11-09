namespace Santhos.MSTest
{
    using System;
    using System.Collections.Generic;
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
        /// Arranges a test case with given arguments
        /// </summary>
        /// <param name="testCase">Test case arguments (aka the test case)</param>
        /// <returns>Fluent API</returns>
        public DataDrivenTest Arrange(params object[] testCase)
        {
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
    }
}
