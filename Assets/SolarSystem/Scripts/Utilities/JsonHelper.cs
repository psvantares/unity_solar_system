using System;

namespace SolarSystem
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            var editJson = "{ \"Items\": " + json + "}";
            var wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(editJson);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array, bool prettyPrint = true)
        {
            var wrapper = new Wrapper<T> { Items = array };
            return UnityEngine.JsonUtility.ToJson(wrapper);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}