using System;

namespace CLIUtility
{
    /// <summary>
    /// A Object representing a command line argument
    /// </summary>
    public class Argument
    {
        /// <summary>
        /// Name
        /// </summary>
        protected String m_Name = null;
        /// <summary>
        /// Value
        /// </summary>
        protected String m_Value = null;
        /// <summary>
        /// Default Value
        /// </summary>
        protected String m_DefaultValue = null;

        /// <summary>
        /// Returns the name of the argument
        /// </summary>
        public String Name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// Gets/Sets the value of the argument
        /// </summary>
        public virtual String Value
        {
            get { return m_Value == null ? m_DefaultValue : m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// Gets whether or not this argument has been set.
        /// </summary>
        public Boolean IsSet
        {
            get { return m_Value != null; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">The name of the argument</param>
        public Argument(String Name)
            : this(Name, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">The name of the argument</param>
        /// <param name="DefaultValue">The default value for the parameter</param>
        public Argument(String Name, String DefaultValue)
        {
            m_Name = Name;
            m_DefaultValue = DefaultValue;
        }

        /// <summary>
        /// Converts a string value to a given Enum Type
        /// </summary>
        /// <typeparam name="T">The enum type to return</typeparam>
        /// <param name="Value">The string representation of the enum value to return</param>
        /// <returns>The enum value represented by the string argument</returns>
        public static T GetEnumValue<T>(String Value)
        {
            T retval = default(T);
            try
            {
                retval = (T)Enum.Parse(typeof(T), Value);
            }
            catch (ArgumentException)
            {
            }
            catch (OverflowException)
            {
            }
            return retval;
        }

    }
}
