/********************************************************************/
/*  Language: C#                                       HCG:   1.0   */
/*                                                                  */
/*  File: Misc.cs                                                   */
/*  Description:                                                    */
/*                                                                  */
/*  Created By:  alexhart        Created At: 9/15/2010 11:23:30 AM  */
/*  Modified By: alexhart       Modified At: 9/15/2010 12:07:58 PM  */
/*                                                                  */
/********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace CLIUtility
{
    /// <summary>
    /// Contains Miscellaneous Command Line Functions
    /// </summary>
    public class Misc
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
            if (program == null || program == String.Empty)
            {
                throw new ArgumentNullException("program");
            }
            if (arguments == null)
            {
                arguments = "";
            }
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.Arguments = arguments;
            p.StartInfo.FileName = program;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.ErrorDialog = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            System.IO.StreamReader sro = null;
            System.IO.StreamReader sre = null;
            try
            {
                p.Start();
                sro = p.StandardOutput;
                sre = p.StandardError;
                p.WaitForExit();

                while (!sro.EndOfStream)
                {
                    Console.Out.WriteLine(sro.ReadLine());
                }
                
                while (!sre.EndOfStream)
                {
                    Console.Out.WriteLine(sre.ReadLine());
                }
                sre.Close();
                retval = p.ExitCode;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error running the program: " + program + " " + arguments, ex);
            }
            finally
            {
                if (sro != null)
                {
                    sro.Close();
                    sro.Dispose();
                }
                if (sre != null)
                {
                    sre.Close();
                    sre.Dispose();
                }
                p.Close();
                p.Dispose();
            }
            return retval;
        }
    }
}

