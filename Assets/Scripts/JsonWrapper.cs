using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class JsonWrapper
{
    [Serializable]
    private class JsonData<T>
    {
        public List<T> userDatas;
    }

    public static void SaveList<T>(List<T> datas, string path)
    {
        JsonData<T> jsonData = new JsonData<T>();
        jsonData.userDatas = datas;
        string dataJson = JsonUtility.ToJson(jsonData);
        if (!path.StartsWith('/'))
            path = "/" + path;
        File.WriteAllText(Application.dataPath + path, dataJson);
        AssetDatabase.Refresh();
    }
    public static List<T> LoadList<T>(string path)
    {
        if(!path.Equals("/")) path = "/" + path;
        string dataJson = File.ReadAllText(Application.dataPath + path);
        JsonData<T> jsonData = JsonUtility.FromJson<JsonData<T>>(dataJson); 
        return jsonData.userDatas;
    }

}
