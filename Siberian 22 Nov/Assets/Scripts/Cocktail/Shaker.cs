using UnityEngine;
using System.Collections.Generic;
using System;
using FMODUnity;
using TMPro;

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
        [SerializeField] private TextMeshProUGUI _warningText;
        [SerializeField] private ParticleSystem[] _alcoholVFX;

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
                _warningText.text = "�������� ��������!";
                Debug.Log("�������� ��������!");
                return;
            }
            if (_iceAdded == false && _selectedIngridients.Count <= 0)
            {
                _warningText.text = "�� ������� ������������!";
                Debug.Log("�� ������� ������������!");
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
                    _warningText.text = "�������� ��";
                    if (_selectedAlcohol == null)
                        RuntimeManager.PlayOneShot(_icePutSound);
                    else
                        RuntimeManager.PlayOneShot(_icePutInWaterSound);
                    _iceAdded = true;
                    return;
                }
                else
                {
                    _warningText.text = "�� ��� �������� ��!";
                    return;
                }
            }

            if (_selectedAlcohol != null)
            {
                _warningText.text = "�� ��� ������ ��������!";
                return;
            }
            _selectedAlcohol = _additiveOnTable[index].Parameters;
            RuntimeManager.PlayOneShot(_additiveOnTable[index].Sound);
            ShowAlcoholVFX(_selectedAlcohol);
            _warningText.text = "������ ��������: " + _selectedAlcohol.name;
        }

        public void AddIngridient(CocktailParametersSO parameters)
        {
            _warningText.text = "������� ���������� � ��������";
            _selectedIngridients.Add(parameters);
        }

        private void ShowAlcoholVFX(CocktailAdditivesSO alcohol)
        {
            switch (alcohol.name)
            {
                case "����":
                    {
                        _alcoholVFX[0].Play();
                        _alcoholVFX[1].Play();
                        break;
                    }
                case "�����":
                    {
                        _alcoholVFX[2].Play();
                        break;
                    }
                case "������ ������":
                    {
                        _alcoholVFX[3].Play();
                        break;
                    }
                case "�������":
                    {
                        _alcoholVFX[4].Play();
                        break;
                    }
            }
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