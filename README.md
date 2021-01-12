# PH.LoggingExtensions [![NuGet Badge](https://buildstats.info/nuget/PH.LoggingExtensions)](https://www.nuget.org/packages/PH.LoggingExtensions/)

Useful extension methods for work with ILogger

## Usage

### BeginSourceScopeContext

Create a Scope tracing CallerMemberName, CallerFilePath and CallerLineNumber
```csharp
//assume myLogger is a Microsoft.Extensions.Logging.ILogger instance
using (var scope = myLogger.BeginSourceScopeContext())
{    
    myLogger.LogTrace(new EventId(42,"Answer to the Ultimate Question of Life, the Universe, and Everything")
                ,"A trace log");
    //the line above produce a log entry as :
    /*
2021-01-12 13:50:05.9791 [ 14] TRACE A trace log - [42]  | { CALLERMEMBERNAME = TEST1, CALLERFILEPATH = P:\DEV\GITLAB\PH.LOGGINGEXTENSIONS\SRC\PH.LOGGINGEXTENSIONS\PH.LOGGINGEXTENSIONS.TEST\UNITTEST1.CS, CALLERLINENUMBER = 27 } | [PH.LoggingExtensions.Test.ScopeTest.Test1(UnitTest1.cs:47)] [PH.LoggingExtensions.Test.ScopeTest]
    */
}
```

### BeginNamedObjectScope

Create a Scope with name and object (a simply `KeyValuePair<string, object>`)
```csharp
//assume logger is a Microsoft.Extensions.Logging.ILogger instance
Guid g = Guid.NewGuid();
using (var scope = logger.BeginNamedObjectScope("GuidScopeVal", g))
{
    logger.LogTrace("A trace log");
}
```