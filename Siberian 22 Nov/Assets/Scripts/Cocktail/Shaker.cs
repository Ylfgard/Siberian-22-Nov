using UnityEngine;
using System.Collections.Generic;
using System;

namespace Cocktails
{
    public class Shaker : MonoBehaviour, IResetable
    {
        public event SendEvent ShakerClicked;

        [SerializeField] private Additive[] _additiveOnTable;
        [SerializeField] private Additive _ice;

        private Cocktail _cocktail;
        private CocktailAdditivesSO _selectedAlcohol;
        private List<CocktailParametersSO> _selectedIngridients;
        private bool _iceAdded;

        private void Awake()
        {
            _cocktail = FindObjectOfType<Cocktail>();
            for (int i = 0; i < _additiveOnTable.Length; i++)
            {
                var clickable = _additiveOnTable[i].SelectedAdditive.AddComponent<ClickableObject>();
                clickable.InitializeClick(i, SelectAdditive);
            }
            var ice = _ice.SelectedAdditive.AddComponent<ClickableObject>();
            ice.InitializeClick(-1, SelectAdditive);
            Reset();
        }

        public void Reset()
        {
            _selectedAlcohol = null;
            _selectedIngridients = new List<CocktailParametersSO>();
            _iceAdded = false;
        }

        public void Shake()
        {
            if (_selectedAlcohol == null && _iceAdded == false &&
                _selectedIngridients.Count <= 0)
            {
                Debug.Log("Шейкер пуст!");
                return;
            }

            

            if(_cocktail.PourCocktail(Color.black) == false)
            {
                Debug.Log("Выберите ёмкость для коктейля!");
                return;
            }
            Reset();
        }

        private void SelectAdditive(int index)
        {
            if (index == -1)
            {
                if (_iceAdded == false)
                {
                    Debug.Log("Add ice");
                    _iceAdded = true;
                    return;
                }
                else
                {
                    Debug.Log("Вы уже добавили лёд!");
                    return;
                } 
            }
            
            if(_selectedAlcohol != null)
            {
                Debug.Log("Вы уже налили алкоголь!");
                return;
            }
            _selectedAlcohol = _additiveOnTable[index].Parameters;
            Debug.Log("Add " + _selectedAlcohol.name);
        }

        public void AddIngridient(CocktailParametersSO parameters)
        {
            Debug.Log("Add " + parameters.name + " ingridient");
            _selectedIngridients.Add(parameters);
        }

        private void OnMouseDown()
        {
            ShakerClicked?.Invoke();
        }
    }

    [Serializable]
    public class Additive
    {
        [SerializeField] private CocktailAdditivesSO _parameters;
        [SerializeField] private GameObject _selectedAdditive;

        public CocktailAdditivesSO Parameters => _parameters;
        public GameObject SelectedAdditive => _selectedAdditive;
    }
}