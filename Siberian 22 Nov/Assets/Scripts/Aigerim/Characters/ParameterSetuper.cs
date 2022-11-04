using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ParameterSetuper : MonoBehaviour
{
    [SerializeField] private ParameterRequest[] _parameterRequestsCopy;
    [SerializeField] private List<ParametrSO> _parameters;





#if UNITY_EDITOR
    [MenuItem("Edit/Find Parameters")]
#endif
    private static List<ParametrSO> FindParametrs()
    {
        string[] parametrsGUID = AssetDatabase.FindAssets("t:ParametrSO", new[] { "Assets/ScriptableObjects/Parameters" });
        Debug.Log("found");
        List<string> spellPaths = new List<string>();
        foreach (string GUID in parametrsGUID)
            spellPaths.Add(AssetDatabase.GUIDToAssetPath(GUID));
        var parametrs = new List<ParametrSO>();
        foreach (string path in spellPaths)
            parametrs.Add(AssetDatabase.LoadAssetAtPath(path, typeof(ParametrSO)) as ParametrSO);
        return parametrs;
    }
}

[Serializable]
public class ParameterRequest
{
    [SerializeField] private string _type;
    [SerializeField] private string _path;
}



public interface IRequiredParameters
{
    List<ParametrSO> Parametrs { get; set; }
}


