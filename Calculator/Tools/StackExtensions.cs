using System.Collections.Generic;


namespace Calculator.Tools
{
    public static class StackExtensions
    {
        /// <summary>
        /// Removes and returns the object at the top of the stack 
        /// or the specified value if the stack is empty.
        /// </summary>
        public static T PopOrValue<T>(this Stack<T> stack, T value)
        {
            return stack.Count > 0 ? stack.Pop() : value;
        }
    }
}
