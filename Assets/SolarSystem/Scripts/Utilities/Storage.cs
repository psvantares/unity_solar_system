using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SolarSystem
{
    public class Storage
    {
        public static List<PlanetModel> PlanetModelData;

        public static IEnumerator LoadTextFieldAsync(string folderName, string dataName)
        {
            string realPath = null;
            string file = null;

#if UNITY_EDITOR || UNITY_IOS
            realPath = Application.streamingAssetsPath + "/" + folderName + "/" + dataName + ".json";
            file = File.ReadAllText(realPath);
#elif UNITY_WEBGL
            realPath = Application.streamingAssetsPath + "/" + folderName + "/" + dataName + ".json";
            if (realPath.Contains("://"))
            {
                WWW www = new WWW(realPath);
                yield return www;

                file = www.text;
            }
            else
            {
                file = File.ReadAllText(realPath);
            }
#elif UNITY_ANDROID
            realPath = string.Format("{0}/{1}", Application.persistentDataPath, dataName + ".json");
            string originalPath = "jar:file://" + Application.dataPath + "!/assets/" + folderName + "/" + dataName + ".json";

            var reader = new WWW(originalPath);
            yield return reader;

            File.WriteAllBytes(realPath, reader.bytes);
            file = File.ReadAllText(realPath);
#endif

            var data = JsonHelper.FromJson<PlanetModel>(file);
            PlanetModelData = data.ToList();

            yield return null;
        }
    }
}