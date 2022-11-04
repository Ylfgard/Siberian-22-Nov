using UnityEngine;
using System;

namespace Cocktails
{
    public delegate void SendCocktailParameter(CocktailParametersSO Parameter);

    public class IngridientSelector : MonoBehaviour
    {
        public event SendCocktailParameter IngridientSelected;

        [SerializeField] private Ingridient[] _ingridients;

        private void Awake()
        {
            for (int i = 0; i < _ingridients.Length; i++)
            {
                var clickable = _ingridients[i].IngridientOnTable.AddComponent<ClickableObject>();
                clickable.InitializeClick(i, SelectIngridient);
            }
        }

        private void SelectIngridient(int index)
        {
            IngridientSelected?.Invoke(_ingridients[index].Parameters);
        }
    }

    [Serializable]
    public class Ingridient
    {
        [SerializeField] private CocktailParametersSO _parameters;
        [SerializeField] private GameObject _ingridientOnTable;

        public CocktailParametersSO Parameters => _parameters;
        public GameObject IngridientOnTable => _ingridientOnTable;
    }
}