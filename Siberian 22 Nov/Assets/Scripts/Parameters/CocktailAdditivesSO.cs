using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "NewAdditive", menuName = "ScriptableObject/CocktailAdditive")]
public class CocktailAdditivesSO : ScriptableObject, IRequiredParameters
{
    [SerializeField] private List<Parameter> _parameters;

    public List<Parameter> Parameters => _parameters;

    public void Setup(List<ParameterSO> parameters)
    {
        _parameters = new List<Parameter>();
        foreach (ParameterSO param in parameters)
            _parameters.Add(new Parameter(param.name, 0));
    }
}
