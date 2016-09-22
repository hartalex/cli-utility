using System;
using System.ComponentModel;
using NUnit.Framework;
using CLIUtility;

namespace CLIUtilityTest
{
    [TestFixture]
    public class ProgramFunctionsTest
    {
        [TestCase("","",0)]
        [TestCase(null, "",0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunProgramTest(String program, String arguments, Int32 expectedExitCode)
        {
            Int32 actualExitCode = ProgramFunctions.RunProgram(program, arguments);
            Assert.AreEqual(expectedExitCode, actualExitCode);
        }


        [TestCase("echo ", "%PATH%", null, "%PATH%\r\n",  0)]
        public void RunProgramTest(String program, String arguments, String Input, String expectedOutput, Int32 expectedExitCode)
        {
            ProgramReturnValues retval = ProgramFunctions.RunProgram(program, arguments,Input);
            Assert.AreEqual(expectedExitCode, retval.ExitCode);
            Assert.AreEqual(expectedOutput, retval.StdOutput);
        }


        [TestCase("C:\\Utilities\\HCG\\HCG.exe", null,0)]
        [TestCase("C:\\Utilities\\HCG\\HCG.exe", "", 0)]
        [TestCase("C:\\Utilities\\HCG//HCG.exe", "", 0)]
        [TestCase("C:\\Utilities//HCG//HCG.exe", "", 0)]
        [TestCase("C://Utilities//HCG//HCG.exe", "", 0)]
        [TestCase("C:\\Utilities\\HCG\\HCG", null, 0)]
        [TestCase("C:\\Utilities\\HCG\\HCG.exe", "-h", 0)]
        [TestCase("C:\\Utilities\\HCG\\HCG.exe", "-\n", 0)]
        [TestCase("C:\\Utilities\\HCG\\HCG.exe", "-\r", 0)]
        public void RunProgramTest_2(String program, String arguments, Int32 expectedvalue)
        {
            RunProgramTest(program, arguments, expectedvalue);
        }

        [TestCase("sol", "", 0)]
        [TestCase("sol", null, 0)]
        [TestCase("soltest", "", 0)]
        [TestCase("C:\\Utislities\\HCG\\HCG.exe", "", 0)]
        [TestCase("soltest", null, 0)]
        [TestCase("C:\\Utilsities\\HCG\\HCG.exe", null, 0)]
        [TestCase("C:\\Documents and Settings\\alexhart\\HCG.exe", "", 0)]
        [TestCase("\"C:\\Documents and Settings\\alexhart\\HCG.exe\"", "", 0)]
        [ExpectedException(typeof(Win32Exception))]
        public void RunProgramTest_3(String program, String arguments, Int32 expectedvalue)
        {
            RunProgramTest(program, arguments, expectedvalue);
        }
    }
}
