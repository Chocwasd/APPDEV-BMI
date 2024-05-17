using System.Collections.Generic;

namespace BMIapp.Common
{
    public class JavaObjectWrapper
    {
        private Dictionary<string, Java.Lang.Object> _javaObject;
        private IDictionary<string, object> dictionary;

        public JavaObjectWrapper(Dictionary<string, Java.Lang.Object> javaObject)
        {
            _javaObject = javaObject;
        }

        public JavaObjectWrapper(IDictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }

        // Provide methods to access properties
        public Java.Lang.Object GetValue(string key)
        {
            if (_javaObject.ContainsKey(key))
            {
                return _javaObject[key];
            }
            return null; // or throw an exception if key not found
        }
    }
}
