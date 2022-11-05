using UnityEngine;
using System;
using System.Collections.Generic;
using FMODUnity;

namespace Cocktails
{
    public class HandInteraction : MonoBehaviour, IResetable
    {
        [SerializeField] private DecorationHolder[] _decorationHolders;

        private Cocktail _cocktail;
        private Shaker _shaker;
        private CocktailParametersSO _selectedIngridient;
        private GameObject _selectedIngridientPrefab;
        private EventReference _selectedIngridientSound;
        private GameObject _curDecoration;

        private void Awake()
        {
            _cocktail = FindObjectOfType<Cocktail>();
            FindObjectOfType<GlassSelector>().GlassChanged += RemoveDecoration;
            FindObjectOfType<IngridientSelector>().IngridientSelected += TakeIngridient;
            _shaker = FindObjectOfType<Shaker>();
            _shaker.ShakerClicked += UseShaker;
            for (int i = 0; i < _decorationHolders.Length; i++)
            {
                var clckable = _decorationHolders[i].Glass.AddComponent<ClickableObject>();
                clckable.InitializeClick(i, UseGlass);
            }

            Reset();
        }

        public void Reset()
        {
            if(_curDecoration != null)
            {
                _curDecoration.SetActive(false);
                _curDecoration = null;
            }
            _selectedIngridient = null;
            _selectedIngridientPrefab = null;
        }

        private void TakeIngridient(CocktailParametersSO parameters, GameObject prefab, EventReference reference)
        {
            Debug.Log("Selected " + parameters.name + " ingridient");
            _selectedIngridient = parameters;
            _selectedIngridientPrefab = prefab;
            _selectedIngridientSound = reference;
        }

        private void UseShaker()
        {
            if(_selectedIngridient != null)
            {
                _shaker.AddIngridient(_selectedIngridient);
                RuntimeManager.PlayOneShot(_selectedIngridientSound);
                _selectedIngridient = null;
                _selectedIngridientPrefab = null;
                return;
            }
            _shaker.Shake();
        }

        private void UseGlass(int index)
        {
            if (_selectedIngridient == null)
            {
                if(_cocktail.GiveCocktail())
                    Reset();
            }
            else
                AddDecoration(_decorationHolders[index]);
        }

        private void AddDecoration(DecorationHolder holder)
        {
            GameObject decoration;
            if (_curDecoration != null) _curDecoration.SetActive(false);
            if (holder.Decorations.ContainsKey(_selectedIngridient.name))
            {
                holder.Decorations.TryGetValue(_selectedIngridient.name, out decoration);
                decoration.SetActive(true);
            }
            else
            {
                decoration = Instantiate(_selectedIngridientPrefab, holder.DecorationPosition, Quaternion.identity);
                holder.Decorations.Add(_selectedIngridient.name, decoration);
            }
            _curDecoration = decoration;
            _selectedIngridient = null;
        }

        private void RemoveDecoration()
        {
            if (_curDecoration == null) return;
            _cocktail.SetDecoration(null);
            _curDecoration.SetActive(false);
            _curDecoration = null;
        }
    }

    [Serializable]
    public class DecorationHolder
    {
        [SerializeField] private GameObject _glassOnTable;
        [SerializeField] private Transform _decorationPoint;
        [SerializeField] private Dictionary<string, GameObject> _decorations = new Dictionary<string, GameObject>();

        public GameObject Glass => _glassOnTable;
        public Vector3 DecorationPosition => _decorationPoint.position;
        public Dictionary<string, GameObject> Decorations => _decorations;
    }
}