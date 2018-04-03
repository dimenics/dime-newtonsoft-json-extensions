﻿using Newtonsoft.Json;

namespace Dime.Utilities
{
    /// <summary>
    /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class Copier
    {
        /// <summary>
        /// Perform a deep copy of the object, using JSON as a serialisation method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T DeepClone<T>(this T source)
        {
            // Initialize inner objects individually
            // For example in default constructor some list property initialized with some values, 
            // but in 'source' these items are cleaned - without ObjectCreationHandling.Replace default constructor values will be added to result
            JsonSerializerSettings deserializeSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return source.DeepClone(deserializeSettings);
        }

        /// <summary>
        /// Perform a deep copy of the object, using JSON as a serialisation method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <param name="settings">The serialization settings</param>
        /// <returns>The copied object.</returns>
        public static T DeepClone<T>(this T source, JsonSerializerSettings settings)
        {
            // Don't serialize a null object, simply return the default for that object
            return ReferenceEquals(source, null) 
                ? default(T) 
                : JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, settings), settings);
        }
    }
}