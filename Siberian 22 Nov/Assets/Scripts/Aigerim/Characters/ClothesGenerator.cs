using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesGenerator : MonoBehaviour
{
    [SerializeField] private RandomCloth[] _listRandomCloth;

    private void Start()
    {
        foreach (RandomCloth randomCloth in _listRandomCloth)
        {
            ClothCharacteristics cloth = randomCloth.GetRandomCloth();
            if (cloth != null)
            {
                Instantiate(cloth, cloth.transform.position, cloth.transform.rotation, transform);
            }
        }
    }

    [Serializable]
    public class RandomCloth
    {
        [SerializeField]
        private ClothCharacteristics[] _prefabs;

        public ClothCharacteristics GetRandomCloth()
        {
            if (_prefabs.Length == 0) return null;

            return _prefabs[UnityEngine.Random.Range(0, _prefabs.Length)];
        }

    }

}
