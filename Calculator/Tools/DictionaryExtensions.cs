using System.Collections.Generic;


namespace Calculator.Tools
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key, 
        /// or default value if there is no the required key.
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dict, TKey key)
        {
            TValue result;
            return dict.TryGetValue(key, out result) 
                ? result 
                : default(TValue);
        }
    }
}
