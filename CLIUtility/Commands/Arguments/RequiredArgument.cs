using System;

namespace CLIUtility
{
	/// <summary>
	/// An object representing an required command line argument
	/// </summary>
    public class RequiredArgument : Argument
	{

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">The name of the argument</param>
        public RequiredArgument(String Name) : base(Name)
        {
            
        }
        /// <summary>
        /// Gets/Sets the value of the argument
        /// </summary>
        public override String Value
        {
            get { 
                if (m_Value == null)
                {
                    throw new Exception("This is a required argument (" + Name + ")");
                }
                return  m_Value;
            }
            set { m_Value = value; }
        }
	}
}
