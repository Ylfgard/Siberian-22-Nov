using UnityEngine;
using System;

[Serializable]
public class ClothCharacteristics : MonoBehaviour
{
    [SerializeField] private int _index;

    public int Index => _index;
}


