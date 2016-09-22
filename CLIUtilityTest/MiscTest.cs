/********************************************************************/
/*  Language: C#                                       HCG:   1.0   */
/*                                                                  */
/*  File: MiscTest.cs                                               */
/*  Description:                                                    */
/*                                                                  */
/*  Created By:  alexhart        Created At: 9/15/2010 11:41:10 AM  */
/*  Modified By: alexhart       Modified At: 9/15/2010 12:10:19 PM  */
/*                                                                  */
/********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CLIUtility;

namespace CLIUtilityTest
{
    [TestFixture]
    public class MiscTest
    {
        [TestCase("","",0)]
        [TestCase(null, "",0)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunProgramTest(String program, String arguments, Int32 expectedvalue)
        {
            Int32 actualvalue = Misc.RunProgram(program, arguments);
            Assert.AreEqual(expectedvalue, actualvalue);
        }

        [TestCase("C:\\Utilities\\HCG\\HCG.exe", null,0)]
        [TestCase("C:\\Utilities\\HCG\\HCG.exe", "", 0)]
        [TestCase("C:\\Utilities\\HCG//HCG.exe", "", 0)]
        [TestCase("C:\\Utilities//HCG//HCG.exe", "", 0)]
        [TestCase("C://Utilities//HCG//HCG.exe", "", 0)]
        [TestCase("C:\\Utilities\\HCG\\HCG", null, 0)]
        [TestCase("C:\\Documents and Settings\\alexhart\\HCG.exe", "", 0)]
        [TestCase("\"C:\\Documents and Settings\\alexhart\\HCG.exe\"", "", 0)]
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
        [ExpectedException(typeof(Exception))]
        public void RunProgramTest_3(String program, String arguments, Int32 expectedvalue)
        {
            RunProgramTest(program, arguments, expectedvalue);
        }
    }
}
