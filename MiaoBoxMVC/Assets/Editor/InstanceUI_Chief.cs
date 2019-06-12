using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class InstanceUI_Chief : Editor
{
    [UnityEditor.MenuItem("Assets/Create/InstanceUIScripts",false,81)]
    public static void CreateInstanceUIScripts()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
        GetSelectedPathOrFallback() + "/NewInstanceUIContorler.cs",
        null,
       "Assets/Editor/ScriptTemplates/81-C# Script-NewUIInstanceControler.cs.txt");

    }

    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }


}


class MyDoCreateScriptAsset : EndNameEditAction
{


    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(o);
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
    {
        string fullPath = Path.GetFullPath(pathName);
        StreamReader streamReader = new StreamReader(resourceFile);
        string text = streamReader.ReadToEnd();
        streamReader.Close();
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
        text = Regex.Replace(text, "#SCRIPTNAME#", fileNameWithoutExtension);
        //string text2 = Regex.Replace(fileNameWithoutExtension, " ", string.Empty);
        //text = Regex.Replace(text, "#SCRIPTNAME#", text2);
        //if (char.IsUpper(text2, 0))
        //{
        //    text2 = char.ToLower(text2[0]) + text2.Substring(1);
        //    text = Regex.Replace(text, "#SCRIPTNAME_LOWER#", text2);
        //}
        //else
        //{
        //    text2 = "my" + char.ToUpper(text2[0]) + text2.Substring(1);
        //    text = Regex.Replace(text, "#SCRIPTNAME_LOWER#", text2);
        //}
        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(text);
        streamWriter.Close();
        AssetDatabase.ImportAsset(pathName);
        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }

}





