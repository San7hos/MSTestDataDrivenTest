// <copyright file="DataDrivenTest.cs" company="Santhos.net">
// Copyright (c) Santhos.net. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Santhos.MSTest
{
    using System;
    using System.Collections;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DataDrivenTest"/> class.
        /// </summary>
        public DataDrivenTest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataDrivenTest"/> class.
        /// Sets how many frames to skip when using data attributes.
        /// </summary>
        /// <param name="skipStackFrames">Number of frames to skip in stack</param>
        internal DataDrivenTest(int skipStackFrames)
        {
            this.SkipStackFrames = skipStackFrames;
        }

        /// <summary>
        /// Gets test cases
        /// </summary>
        internal IReadOnlyCollection<object[]> TestCases => this.testCases;

        /// <summary>
        /// Gets number of arguments of a test case
        /// </summary>
        internal int TestCaseArgumentCount { get; private set; } = 0;

        /// <summary>
        /// Gets number of skipped stack frames
        /// Used when test cases are arranged from attributes
        /// </summary>
        internal int SkipStackFrames { get; } = 1;

        /// <summary>
        /// Arranges a test case with given arguments
        /// </summary>
        /// <param name="testCase">Test case arguments (aka the test case)</param>
        /// <returns>Fluent API</returns>
        public DataDrivenTest Arrange(params object[] testCase)
        {
            if (!this.testCases.Any())
            {
                this.TestCaseArgumentCount = testCase.Length;
            }

            if (testCase.Length != this.TestCaseArgumentCount)
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
        /// <returns>Fluent API</returns>
        public DataDrivenTest ArrangeTestCases(IEnumerable<IEnumerable<object>> testCaseSet)
        {
            foreach (IEnumerable<object> testCase in testCaseSet)
            {
                this.Arrange(testCase);
            }

            return this;
        }

        /// <summary>
        /// Arranges multiple test cases at once
        /// </summary>
        /// <param name="testCaseSet">Function that returns set of test cases</param>
        /// <returns>Fluent API</returns>
        public DataDrivenTest ArrangeTestCases(Func<IEnumerable<IEnumerable<object>>> testCaseSet)
        {
            foreach (IEnumerable<object> testCase in testCaseSet())
            {
                this.Arrange(testCase);
            }

            return this;
        }

        /// <summary>
        /// Arranges (multiple) test cases from <see cref="TestCaseAttribute"/>s
        /// </summary>
        /// <returns>Fluent API</returns>
        public DataDrivenTest ArrangeFromAttributes()
        {
            IEnumerable<object[]> testCasesFromAttributes = new StackFrame(this.SkipStackFrames)
                .GetMethod()
                .GetCustomAttributes<TestCaseAttribute>()
                .Select(a => a.TestCase);

            foreach (object[] testCase in testCasesFromAttributes)
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
            if (actAndAssert == null)
            {
                throw new ArgumentNullException(nameof(actAndAssert));
            }

            if (!this.testCases.Any())
            {
                Assert.Fail("There are no test cases. Have you forgotten to arrange them?");
            }

            foreach (object[] arguments in this.testCases)
            {
                try
                {
                    actAndAssert.DynamicInvoke(arguments);
                }
                catch (TargetInvocationException ex)
                {
                    // throw inner exception with its original stack trace
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }
            }
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1>(Action<T1> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2>(Action<T1, T2> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3>(Action<T1, T2, T3> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4>(Action<T1, T2, T3, T4> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <typeparam name="T5">Test case argument 5</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <typeparam name="T5">Test case argument 5</typeparam>
        /// <typeparam name="T6">Test case argument 6</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <typeparam name="T5">Test case argument 5</typeparam>
        /// <typeparam name="T6">Test case argument 6</typeparam>
        /// <typeparam name="T7">Test case argument 7</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <typeparam name="T5">Test case argument 5</typeparam>
        /// <typeparam name="T6">Test case argument 6</typeparam>
        /// <typeparam name="T7">Test case argument 7</typeparam>
        /// <typeparam name="T8">Test case argument 8</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <typeparam name="T5">Test case argument 5</typeparam>
        /// <typeparam name="T6">Test case argument 6</typeparam>
        /// <typeparam name="T7">Test case argument 7</typeparam>
        /// <typeparam name="T8">Test case argument 8</typeparam>
        /// <typeparam name="T9">Test case argument 9</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }

        /// <summary>
        /// Calls actAndAssert method for each previously given test case
        /// </summary>
        /// <typeparam name="T1">Test case argument 1</typeparam>
        /// <typeparam name="T2">Test case argument 2</typeparam>
        /// <typeparam name="T3">Test case argument 3</typeparam>
        /// <typeparam name="T4">Test case argument 4</typeparam>
        /// <typeparam name="T5">Test case argument 5</typeparam>
        /// <typeparam name="T6">Test case argument 6</typeparam>
        /// <typeparam name="T7">Test case argument 7</typeparam>
        /// <typeparam name="T8">Test case argument 8</typeparam>
        /// <typeparam name="T9">Test case argument 9</typeparam>
        /// <typeparam name="T10">Test case argument 10</typeparam>
        /// <param name="actAndAssert">Method that should act on and assert each test case</param>
        public void ActAndAssert<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> actAndAssert)
        {
            this.ActAndAssert((Delegate)actAndAssert);
        }
    }
}
