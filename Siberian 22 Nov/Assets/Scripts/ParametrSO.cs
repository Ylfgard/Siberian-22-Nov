using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewParameter", menuName = "ScriptableObject/Parameter")]
public class ParametrSO : ScriptableObject
{
    [SerializeField] public int Value;
}
