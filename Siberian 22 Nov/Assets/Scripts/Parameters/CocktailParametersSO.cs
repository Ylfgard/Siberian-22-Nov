using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewCocktailParameter", menuName = "ScriptableObject/CocktailParameter")]
public class CocktailParametersSO : ScriptableObject, IRequiredParameters
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Parameter[] _parameters;

    public GameObject Prefab => _prefab;
    public Parameter[] Parameters => _parameters;

    public void Setup(List<ParameterSO> parameters)
    {
        List<Parameter> param = new List<Parameter>();
        foreach (ParameterSO p in parameters)
            param.Add(new Parameter(p.name, 0));
        _parameters = param.ToArray();
    }
}
