using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSpaceGame.Utils {
    /// <summary>
    /// A global abstract resource class to contain game assets while keeping them easily accessable.
    /// 
    /// Usage: Resource<Texture2D>.Set(textureVariable, "Hello"); Resource<Texture2D>.Get("Hello");
    /// </summary>
    /// <typeparam name="T">
    /// The type of the resource (eg. Texture2D, SpriteSheet)
    /// </typeparam>
    public abstract class Resource<T> {
        // The internal map holding all the resources of type T mapped to a string name.
        private static Dictionary<string, T> _Map = new Dictionary<string, T>();

        // Wrapper function that sets a value in the map.
        public static void Set(string name, T t) {
            _Map.Add(name, t);
        }

        // Removes a resource from the map.
        public static void Remove(string name) {
            _Map.Remove(name);
        }

        // Tries to get the resource of type T with name name.
        // If it failes, it prints an error and returns the default value of T.
        public static T Get(string name) {
            T ret;
            bool result = _Map.TryGetValue(name, out ret);

            if (!result) {
                Console.Error.WriteLine("Failed to get resource \"" + name + "\" of type \"" + ret.ToString() + "\"!");
                return default(T);
            }

            return ret;
        }
    }
}
