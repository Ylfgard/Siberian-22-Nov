using UnityEngine;
using System.Collections.Generic;
using System;
using FMODUnity;

namespace Cocktails
{
    public class Shaker : MonoBehaviour, IResetable
    {
        public event SendEvent ShakerClicked;

        [SerializeField] private EventReference _shakeSound;
        [SerializeField] private EventReference _icePutSound;
        [SerializeField] private EventReference _icePutInWaterSound;
        [SerializeField] private Additive[] _additiveOnTable;
        [SerializeField] private Additive _ice;

        private CocktailCombinator _combinator;
        private CocktailAdditivesSO _selectedAlcohol;
        private List<CocktailParametersSO> _selectedIngridients;
        private bool _iceAdded;

        public CocktailAdditivesSO SelectedAlcohol => _selectedAlcohol;

        private void Awake()
        {
            _combinator = FindObjectOfType<CocktailCombinator>();
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
            if (_selectedAlcohol == null)
            {
                Debug.Log("Добавьте алкоголь!");
                return;
            }
            if (_iceAdded == false && _selectedIngridients.Count <= 0)
            {
                Debug.Log("Не хватает ингридиентов!");
                return;
            }

            if (_combinator.MixCoctail(_iceAdded, _selectedIngridients, _selectedAlcohol) == false) return;
            RuntimeManager.PlayOneShot(_shakeSound);
            Reset();
        }

        private void SelectAdditive(int index)
        {
            if (index == -1)
            {
                if (_iceAdded == false)
                {
                    Debug.Log("Add ice");
                    if (_selectedAlcohol == null)
                        RuntimeManager.PlayOneShot(_icePutSound);
                    else
                        RuntimeManager.PlayOneShot(_icePutInWaterSound);
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
            RuntimeManager.PlayOneShot(_additiveOnTable[index].Sound);
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
        [SerializeField] private EventReference _sound;
        [SerializeField] private CocktailAdditivesSO _parameters;
        [SerializeField] private GameObject _selectedAdditive;

        public EventReference Sound => _sound;
        public CocktailAdditivesSO Parameters => _parameters;
        public GameObject SelectedAdditive => _selectedAdditive;
    }
}