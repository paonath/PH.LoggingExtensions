using System;
using System.Collections.Generic;
using System.Dynamic;
using Autofac;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace PH.LoggingExtensions.Test
{
   
    public class ScopeTest : BaseUnitTest
    {
        public ScopeTest(ITestOutputHelper output) : base(output, LogLevel.Trace)
        {
        }

       
        [Fact]
        public void Test1()
        {
            var logger = Scope.Resolve<ILogger<ScopeTest>>();
            
            int    n             = 0;
            string method        = "";
            bool   isDisposable = false;

            using (var scope = logger.BeginSourceScopeContext())
            {
                var dynScope = (dynamic)scope ;
                
                var nameOfProperty = "Value";
                var propertyInfo   = dynScope.GetType().GetProperty(nameOfProperty);
                var value          = propertyInfo.GetValue(dynScope, null);

                var callerMemberNameValue =
                    (string) value.GetType().GetProperty("CallerMemberName").GetValue(value, null);

                method = callerMemberNameValue;

                var callerFilePathValue =
                    (string) value.GetType().GetProperty("CallerFilePath").GetValue(value, null);

                var callerLineNumberValue =
                    (int) value.GetType().GetProperty("CallerLineNumber").GetValue(value, null);

                n = callerLineNumberValue;
                logger.LogTrace(new EventId(42,"Answer to the Ultimate Question of Life, the Universe, and Everything"),"A trace log");
                if (scope is IDisposable disposable)
                {
                    isDisposable = true;
                }
            }
            Assert.True(n > 0);
            Assert.NotEmpty(method);
            Assert.Equal(nameof(Test1), method);
            Assert.True(isDisposable);
        }

        [Fact]
        public void Test2()
        {
            var logger = Scope.Resolve<ILogger<ScopeTest>>();
            
            int    n            = 0;
            string method       = "";
            bool   isDisposable = false;

            Guid g = Guid.NewGuid();
            Guid stateGuid = Guid.Empty;

            using (var scope = logger.BeginSourceScopeContext(g))
            {
                var dynScope = (dynamic)scope ;
                
                var nameOfProperty = "Value";
                var propertyInfo   = dynScope.GetType().GetProperty(nameOfProperty);
                var value          = propertyInfo.GetValue(dynScope, null);

                var state          = value.GetType().GetProperty("State").GetValue(value, null);
                var sourceLogState = value.GetType().GetProperty("SourceLogState").GetValue(value, null);

                stateGuid = Guid.Parse(state.ToString());

                var callerMemberNameValue =
                    (string) sourceLogState.GetType().GetProperty("CallerMemberName").GetValue(sourceLogState, null);

                method = callerMemberNameValue;

                var callerFilePathValue =
                    (string) sourceLogState.GetType().GetProperty("CallerFilePath").GetValue(sourceLogState, null);

                var callerLineNumberValue =
                    (int) sourceLogState.GetType().GetProperty("CallerLineNumber").GetValue(sourceLogState, null);

                n = callerLineNumberValue;
                logger.LogTrace("A trace log");
                if (scope is IDisposable)
                {
                    isDisposable = true;
                }
            }
            Assert.True(n > 0);
            Assert.NotEmpty(method);
            Assert.Equal(nameof(Test2), method);
            Assert.True(isDisposable);
            Assert.True(stateGuid != Guid.Empty);
            Assert.Equal(stateGuid, g);
        }

        [Fact]
        public void TestNamedObjectScope()
        {
            var logger = Scope.Resolve<ILogger<ScopeTest>>();
            Guid g = Guid.NewGuid();

            using (var scope = logger.BeginNamedObjectScope("NameOfTheScope", g))
            {
                var dbg = scope;

                logger.LogTrace("A trace log");
            }
        }
    }
}
