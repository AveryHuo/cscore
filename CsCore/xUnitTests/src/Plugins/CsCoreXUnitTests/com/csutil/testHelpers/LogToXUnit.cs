
using com.csutil;
using com.csutil.logging;
using Xunit.Abstractions;

namespace Xunit {

    public static class ITestOutputHelperExtensions {

        ///<summary> Call this to use the xunit Logging system as the output for Log.d(..) etc </summary>
        public static void UseAsLoggingOutput(this ITestOutputHelper xunitLogger) {
            if (!(Log.instance is LogToXUnit)) { Log.instance = new LogToXUnit(xunitLogger); }
        }

    }

    public class LogToXUnit : LogDefaultImpl {

        private ITestOutputHelper xunitLogger;

        public LogToXUnit(ITestOutputHelper xunitLogger) { this.xunitLogger = xunitLogger; }

        protected override void PrintDebugMessage(string debugLogMsg, params object[] args) {
            xunitLogger.WriteLine(debugLogMsg);
        }

        protected override void PrintErrorMessage(string errorMsg, params object[] args) {
            xunitLogger.WriteLine(errorMsg);
        }

        protected override void PrintWarningMessage(string warningMsg, params object[] args) {
            xunitLogger.WriteLine(warningMsg);
        }

    }

}