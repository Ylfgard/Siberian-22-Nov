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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _glassIsEmpty = !_glassIsEmpty;
        }
    }
}