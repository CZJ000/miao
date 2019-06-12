using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AnimsImporter : ScriptableWizard
{
    public GameObject doll;
    public TextAsset animFile;
    private ModelImporter modelImporter;

    [MenuItem("Editor/Models/Import Doll Anims")]
    static void Import()
    {
        ScriptableWizard.DisplayWizard(
            "Import anims", typeof(AnimsImporter),
            "Apply  Close");
    }
    void OnWizardCreate()
    {
        apply();
    }
    void apply()
    {
        Renderer[] renders = doll.GetComponentsInChildren<Renderer>();
        foreach(var render in renders)
        {
            if (render.sharedMaterials != null)
            {
                foreach (var material in render.sharedMaterials)
                {
                    if (material)
                    {
                        material.shader = Shader.Find("Unlit/UnlitAlphaWithFade");
                        material.SetColor("_Color", Color.white);
                    }
                }
            }
        }
        string path = AssetDatabase.GetAssetPath(doll);
        modelImporter = AssetImporter.GetAtPath(path) as ModelImporter;
        modelImporter.animationType = ModelImporterAnimationType.Legacy;
        loadAnimationsFromText(animFile.text);
        AssetDatabase.ImportAsset(path);

        GameObject catObj = PrefabUtility.CreatePrefab("Assets/Resources/Characters/" + doll.name + ".prefab", doll);
        catObj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        CharacterController catCtl = catObj.AddComponent<CharacterController>();
        catCtl.slopeLimit = 0;
        catCtl.stepOffset = 0;
        catCtl.skinWidth = 0.01f;
        catCtl.minMoveDistance = 0.01f;
        catCtl.center = new Vector3(0, 0.56f, 0);
        catCtl.radius = 0.4f;
        catCtl.height = 1.09f;
    }
    public void loadAnimationsFromText(string animationsDescriptorFile)
    {
        char[] delimiters = ";".ToCharArray();
        string[] lines = animationsDescriptorFile.Split(delimiters);
        ModelImporterClipAnimation[] animations = new ModelImporterClipAnimation[lines.Length];
        
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            animations[i] = parse(line);
        }
        modelImporter.clipAnimations = animations;
    }

    public ModelImporterClipAnimation parse(string textEncoding)
    {
        ModelImporterClipAnimation newClip = new ModelImporterClipAnimation();

        char[] delimiters = " =-".ToCharArray();

        //--- Fix the tokens (two spaces makes a blank token, not what we want)
        List<string> tokens = new List<string>();
        string[] badTokens = textEncoding.Split(delimiters);
        foreach (string token in badTokens)
        {
            if (!token.Equals(string.Empty))
                tokens.Add(token.ToString());
        }

        newClip.name = tokens[0];
        newClip.firstFrame = int.Parse(tokens[1]);
        newClip.lastFrame = int.Parse(tokens[2]);
        newClip.loop = false;
        newClip.wrapMode = WrapMode.Once;

        return newClip;
    }
}