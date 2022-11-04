using UnityEngine;

namespace Cocktails
{
    public class Cocktail : MonoBehaviour, IResetable
    {
        private bool _glassIsEmpty;
        private CocktailParametersSO _curGlassParameters;

        private void Awake()
        {
            Reset();
        }

        public void Reset()
        {
            _glassIsEmpty = true;
            _curGlassParameters = null;
        }

        public bool SelectGlass(CocktailParametersSO glassParameters)
        {
            if (_glassIsEmpty)
            {
                Debug.Log("Selected " + glassParameters.name);
                _curGlassParameters = glassParameters;
                return true;
            }
            return false;
        }

        public bool PourCocktail(Color cocktaulColor)
        {
            if (_curGlassParameters == null) return false;
            _glassIsEmpty = false;
            return true;
        }
    }
}