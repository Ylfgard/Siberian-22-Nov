using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (fileName = "NewCocktailParameter", menuName = "ScriptableObject/CocktailParameter")]
public class CocktailParametersSO : ScriptableObject, IRequiredParameters
{
    [SerializeField] private List<Parameter> _parameters;

    public void Setup(List<ParameterSO> parameters)
    {
        _parameters = new List<Parameter>();
        foreach (ParameterSO param in parameters)
            _parameters.Add(new Parameter(param.name, 0));
    }
}
