//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace ExpressionEngine
{
    public interface IVariables
    {
        /// <summary>
        /// Returns true, if a variable name is a protected constant that is read only
        /// </summary>
        /// <param name="variableName">variable name</param>
        /// <returns>True, if the variable can only be readed</returns>
        bool IsConstant(string variableName);

        /// <summary>
        /// Gets or sets a variable value.
        /// If variable not exists on read: throw an exception
        /// If variable is a constant and it's trying to be set: throw exception
        /// If variable not exists on write: create var with value
        /// </summary>
        /// <param name="variable">Variable name</param>
        /// <returns>variable value</returns>
        INumber this[string variable] { get; set; }

        /// <summary>
        /// Cheks if a variable or constant is defined or not
        /// </summary>
        /// <param name="variable">Variable to check</param>
        /// <returns>true, if variable or constant defined</returns>
        bool IsDefined(string variable);

        /// <summary>
        /// Exception safe alternative for setting a varaible
        /// </summary>
        /// <param name="variable">variable name</param>
        /// <param name="value">variable value</param>
        /// <returns>true, if variable was set succesfully</returns>
        bool TrySetValue(string variable, INumber value);

        /// <summary>
        /// Get a variable value
        /// </summary>
        /// <param name="variable">variable name</param>
        /// <returns>variable value</returns>
        INumber GetValue(string variable) => this[variable];

        /// <summary>
        /// Clear all previously set variables
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns the number of set variables
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the currently available variables
        /// </summary>
        IEnumerable<string> VariableNames { get; }
    }
}
