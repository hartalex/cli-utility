using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace CLIUtility
{
    /// <summary>
    /// An object representing a command
    /// </summary>
    public class Command
    {
        private String m_Name;
        private String m_HelpText;
        private List<String> m_CommandArguments;
        private List<Argument> m_Arguments;

        /// <summary>
        /// Gets/Sets the name of this command
        /// </summary>
        [XmlAttribute]
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        /// <summary>
        /// Gets/Sets the CommandArguments for this command
        /// </summary>
        [XmlElement]
        public List<String> CommandArguments
        {
            get { return m_CommandArguments; }
            set { m_CommandArguments = value; }
        }

        /// <summary>
        /// Gets/Sets the description to be displayed when help is requested.
        /// </summary>
        [XmlAttribute]
        public String HelpText
        {
            get { return m_HelpText; }
            set { m_HelpText = value; }
        }
        
        /// <summary>
        /// Gets/Sets the arguments for this command
        /// </summary>
        [XmlElement]
        public List<Argument> Arguments
        {
            get { return m_Arguments; }
            set { m_Arguments = value; }
        }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Command()
        {
            Name = "Command";
            CommandArguments = new List<String>();
            HelpText = "";
            m_Arguments = new List<Argument>();
        }

        /// <summary>
        /// Executes this command
        /// </summary>
        public virtual int DoCommand()
        { return 0; }
        
        /// <summary>
        /// Add an argument to this command
        /// </summary>
        /// <param name="Argument">The argument to add</param>
        /// /// <exception cref="System.ArgumentNullException">When <paramref name="Argument"/>is null.</exception>
		public void AddArgument(String Argument)
        {
			if (Argument != null)
			{
				Argument[] list = m_Arguments.ToArray();
				int i = 0;
				while(i < m_Arguments.Count && m_Arguments[i].IsSet)
				{   
					i++;
				}	
				if (i < m_Arguments.Count)
				{
					m_Arguments[i].Value = Argument;
				}	
				else
				{
					m_Arguments.Add(new OptionalArgument("Optional"));
					m_Arguments[i].Value = Argument;
				}
			}else
			{
				throw new ArgumentNullException("Argument");
			}
        }
        /// <summary>
        /// Adds an array of arguments to this command
        /// </summary>
        /// <param name="Arguments">An array of arguments</param>
        /// <exception cref="System.ArgumentNullException">When <paramref name="Arguments"/>or any strings within <paramref name="Arguments"/> are null.</exception>
		public void AddArguments(String[] Arguments)
		{
				if (Arguments != null)
				{
					// Skip the first Argument as this tells us what the command is
					for (int i = 1; i < Arguments.Length; i++)
					{
						AddArgument(Arguments[i]);
					}
				}else
				{
					throw new ArgumentNullException("Arguments");
				}
                ValidateArguments();
		}
		/// <summary>
		/// Gets the usage string for this command
		/// </summary>
		/// <returns>The usage string for this command</returns>
        public virtual String GetUsageString()
        {
            StringBuilder retval = new StringBuilder("[");
            bool isFirst = true;
            foreach (String cmdarg in CommandArguments)
            {
                if (!isFirst)
                {
                    retval.Append(",");
                }
                else
                {
                    isFirst = false;
                }
                retval.Append(cmdarg);
            }
            retval.Append("] ");
            foreach (Argument arg in m_Arguments)
            {
                retval.Append(arg.Name);
                retval.Append(" ");
            }
            retval.Append("\n");
            retval.Append(HelpText);
            return retval.ToString();
        }

        /// <summary>
        /// Validates the Command Line Arguments to make sure each required argument was set
        /// </summary>
        public virtual void ValidateArguments()
        {
            foreach (Argument arg in m_Arguments)
            {
                if (arg.GetType() == typeof(RequiredArgument) && arg.IsSet == false)
                {
                    throw new ArgumentNullException(arg.Name);
                }
            }
        }

    }
}
