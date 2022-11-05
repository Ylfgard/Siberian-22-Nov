using UnityEngine;
using System;

namespace Cocktails
{
    public class GlassSelector : MonoBehaviour, IResetable
    {
        public event SendEvent GlassChanged;

        [SerializeField] private Glass[] _glasses;
        
        private Cocktail _cocktail;
        private GameObject _selectedGlass;

        private void Awake()
        {
            _cocktail = GetComponent<Cocktail>();

            for (int i = 0; i < _glasses.Length; i++)
            {
                var glass = _glasses[i].Selectable;
                var clickable = glass.AddComponent<ClickableObject>();
                clickable.InitializeClick(i, SelectGlass);
            }
            Reset();
        }

        public void Reset()
        {
            _selectedGlass = null;
            foreach (var glass in _glasses)
                glass.OnTable.SetActive(false);
        }

        private void SelectGlass(int index)
        {
            var glass = _glasses[index];
            if (_cocktail.SelectGlass(glass.Parameters))
            {
                if(_selectedGlass != null)
                {
                    GlassChanged?.Invoke();
                    _selectedGlass.SetActive(false);
                }
                glass.OnTable.SetActive(true);
                _selectedGlass = glass.OnTable;
            }
            else Debug.Log("Опустошите текущую ёмкость!");
        }
    }

    [Serializable]
    public class Glass
    {
        [SerializeField] private CocktailParametersSO _parameters;
        [SerializeField] private GameObject _glassOnTable;
        [SerializeField] private GameObject _selectableGlasses;

        public CocktailParametersSO Parameters => _parameters;
        public GameObject OnTable => _glassOnTable;
        public GameObject Selectable => _selectableGlasses;
    }
}