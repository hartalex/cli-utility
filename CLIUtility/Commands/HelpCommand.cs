using System;

namespace CLIUtility
{
	/// <summary>
	/// An object representing a help command
	/// </summary>
    public class HelpCommand : Command
    {
        private CommandList m_CommandList;

        /// <summary>
        /// Sets the commandlist property
        /// </summary>
        public CommandList CommandList
        {
            set { m_CommandList = value; }
        }

        /// <summary>
		/// Default Constructor
		/// </summary>
        public HelpCommand() : base()
		{
            Name = "Help";
            CommandArguments.Add("-h");
            CommandArguments.Add("--help");
            CommandArguments.Add("-help");
            CommandArguments.Add("-?");
            HelpText = "";
		}

        /// <summary>
        /// Executes the help command
        /// </summary>
        public override int DoCommand()
        {
            Console.Out.WriteLine(m_CommandList.GetUsageStrings());
            return 0;
        }

        /// <summary>
        /// Gets the usage string for this command
        /// </summary>
        /// <returns>The usage string for this command</returns>
        public override String GetUsageString()
        {
            String retval = base.GetUsageString();
            retval  = retval.Replace("[Optional]", "");
            return retval;
        }
    
	}
}
