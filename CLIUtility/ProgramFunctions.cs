
using System;

namespace CLIUtility
{
    /// <summary>
    /// Encapsulates All Possibly return values from a Program
    /// </summary>
    public struct ProgramReturnValues
    {
        /// <summary>
        ///  The Std Output returned from a program as a string
        /// </summary>
        public String StdOutput;
        /// <summary>
        ///  The exit code returned from a program
        /// </summary>
        public Int32 ExitCode;
    }


    /// <summary>
    /// Contains Miscellaneous Command Line Functions
    /// </summary>
    public class ProgramFunctions
    {
        /// <summary>
        /// Runs a program and redirects the standard output and error to the console
        /// This procedure blocks until the program is finished running
        /// </summary>
        /// <param name="program">The path to the program to run</param>
        /// <param name="arguments">Any arguments to run the program with</param>
        /// <exception cref="System.Exception"></exception>
        public static Int32 RunProgram(String program, String arguments) 
        {
            Int32 retval = 0;
            ProgramReturnValues retvals = RunProgram(program, arguments, null);
            retval = retvals.ExitCode;
            return retval;
        }

        /// <summary>
        /// Runs a program and redirects the standard output and error to the console
        /// This procedure blocks until the program is finished running
        /// </summary>
        /// <param name="program">The path to the program to run</param>
        /// <param name="arguments">Any arguments to run the program with</param>
        /// <param name="StdInput">Any Input to send to the program</param>
        /// 
        /// <exception cref="System.Exception"></exception>
        public static ProgramReturnValues RunProgram(String program, String arguments, String StdInput) 
        {
            ProgramReturnValues retval;
            if (program == null || program == String.Empty)
            {
                throw new ArgumentNullException("program");
            }
            if (arguments == null)
            {
                arguments = "";
            }
            if (StdInput == null)
            {
                StdInput = "";
            }
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.Arguments = arguments;
            p.StartInfo.FileName = program;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.ErrorDialog = false;
            p.StartInfo.RedirectStandardOutput = true;
            if (StdInput != "")
            {
                p.StartInfo.RedirectStandardInput = true;
            }
        
            System.IO.StreamReader sro = null;
            System.IO.StreamWriter swi = null;
            try
            {
                p.Start();
                if (StdInput != "")
                {
                    swi = p.StandardInput;
                    swi.Write(StdInput);
                    swi.Flush();
                    swi.Close();
                    swi.Dispose();
                    swi = null;
                }
                sro = p.StandardOutput;
                p.WaitForExit();

                retval.StdOutput = sro.ReadToEnd();
                retval.ExitCode = p.ExitCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (swi != null)
                {
                    swi.Close();
                    swi.Dispose();
                }
                if (sro != null)
                {
                    sro.Close();
                    sro.Dispose();
                }
                p.Close();
                p.Dispose();
            }
            return retval;
        }

    }
}

