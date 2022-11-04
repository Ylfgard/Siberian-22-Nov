using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCloth", menuName = "ScriptableObject/Cloth")]
public class ClothParametersSO : ScriptableObject
{
    [SerializeField] private List<ParametrSO> _parametrs;
}
