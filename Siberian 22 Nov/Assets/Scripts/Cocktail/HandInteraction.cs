using UnityEngine;

namespace Cocktails
{
    public class HandInteraction : MonoBehaviour
    {
        private Shaker _shaker;
        private CocktailParametersSO _selectedIngridient;

        private void Awake()
        {
            FindObjectOfType<IngridientSelector>().IngridientSelected += TakeIngridient;
            _shaker = FindObjectOfType<Shaker>();
            _shaker.ShakerClicked += UseShaker;
        }

        private void TakeIngridient(CocktailParametersSO parameters)
        {
            Debug.Log("Selected " + parameters.name + " ingridient");
            _selectedIngridient = parameters;
        }

        private void UseShaker()
        {
            if(_selectedIngridient != null)
            {
                _shaker.AddIngridient(_selectedIngridient);
                _selectedIngridient = null;
                return;
            }
            _shaker.Shake();
        }
    }
}