using System;
using System.Collections.Generic;

namespace CLIUtility
{
	/// <summary>
	/// An object representing a collection of possible command line commands
	/// </summary>
    public class CommandList
	{
        private List<Command> m_Commands;
        
        /// <summary>
        /// Default Constructor
        /// </summary>
		public CommandList()
		{
            m_Commands = new List<Command>();
		}
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CommandList(Boolean IncludeHelp) : this()
        {
            if (IncludeHelp)
            {
                AddCommand(new HelpCommand());
            }
        }
        
        /// <summary>
        /// Add a command to the list of command
        /// </summary>
        /// <param name="cmd">A command to add to the list</param>
        /// <exception cref="System.ArgumentNullException">When <paramref name="cmd"/> is null</exception>
        public void AddCommand(Command cmd)
        {
            if (cmd != null)
			{
                m_Commands.Add(cmd);
			} else
			{
                throw new ArgumentNullException("cmd");
			}
        }

        /// <summary>
        /// Retrieves a command from the list with a matching argument
        /// </summary>
        /// <param name="Argument">A command argument</param>
        /// <returns>the last command from the list that has a matching command argument.\n
        /// Defaults to HelpCommand object.</returns>
        public Command FindCommandByArgument(String Argument)
        {
			Command retval = new HelpCommand();
			if (Argument != null)
			{
				foreach (Command c in m_Commands)
				{
					if (c.CommandArguments.Contains(Argument))
					{
						retval = c;
					}
				}
		    }
			if (retval.GetType() == typeof(HelpCommand))
			{
                ((HelpCommand)retval).CommandList = this;
			}
           return retval;
        }

        /// <summary>
        /// Gets the usage strings from each command in the list
        /// </summary>
        /// <returns>A concatenated usage string from each command in the list </returns>
        public String GetUsageStrings()
        {
            String retval = "Usage:\n";
            foreach (Command c in m_Commands)
            {
                retval +="  " + c.GetUsageString() + "\n";
            }
            return retval;
        }
    	
        /// <summary>
		/// Executes one of the commands in the list based on the argumentlist
		/// </summary>
		/// <param name="args">List of command line arguments.  Most likely from the main procedure</param>
		public int DoCommand(String[] args)
		{
            int retval = 0;
            try
            {
                Command com = null;
                if (args.Length >= 1)
                {
                    com = FindCommandByArgument(args[0]);
                    com.AddArguments(args);
                }
                else
                {
                    com = FindCommandByArgument(null); // Should return HelpCommand
                }
                retval = com.DoCommand();
            }
            catch (ArgumentNullException ane)
            {
                Console.Error.WriteLine(ane.Message);
                retval = 1;
            }
            return retval;
		}

	}
}
