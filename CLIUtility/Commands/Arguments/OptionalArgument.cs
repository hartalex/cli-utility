using System;

namespace CLIUtility
{
    /// <summary>
    /// An object representing an optional command line argument
    /// </summary>
    public class OptionalArgument : Argument
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">The name of the argument</param>
        public OptionalArgument(String Name)
            : this(Name, null )
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">The name of the argument</param>
        /// <param name="DefaultValue">The default value of the argument</param>
        public OptionalArgument(String Name,String DefaultValue)
            : base("[" + Name + "]",DefaultValue)
        {

        }

    }
}
