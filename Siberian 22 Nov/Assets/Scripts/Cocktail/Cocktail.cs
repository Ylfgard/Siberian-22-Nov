using UnityEngine;
using GameControllers;

namespace Cocktails
{
    public class Cocktail : MonoBehaviour, IResetable
    {
        private ScoreCounter _scoreCounter;
        private bool _glassIsEmpty;
        private CocktailParametersSO _curGlassParameters;
        private CocktailCombinationSO _curCocktailCombination;
        private CocktailAdditivesSO _curAlcohol;
        private CocktailParametersSO _curDecoration;

        private void Awake()
        {
            _scoreCounter = FindObjectOfType<ScoreCounter>();   
            Reset();
        }

        public void Reset()
        {
            _curAlcohol = null;
            _curCocktailCombination = null;
            _curGlassParameters = null;
            _curDecoration = null;
        }

        public bool SelectGlass(CocktailParametersSO glassParameters)
        {
            if (_curAlcohol == null)
            {
                Debug.Log("Selected " + glassParameters.name);
                _curGlassParameters = glassParameters;
                return true;
            }
            return false;
        }

        public bool PourCocktail(CocktailCombinationSO combination, CocktailAdditivesSO alcohol)
        {
            if (_curGlassParameters == null) return false;
            if (combination == null) Debug.Log("Какая гадость!");
            else Debug.Log(combination.name + " " + alcohol.name);

            _curCocktailCombination = combination;
            _curAlcohol = alcohol;
            return true;
        }

        public void SetDecoration(CocktailParametersSO decoration)
        {
            _curDecoration = decoration;
        }

        public void GiveCocktail()
        {
            if(_curAlcohol == null)
            {
                Debug.Log("Вы не налили коктейль!");
                return;
            }

            Parameter[] result = new Parameter[_curCocktailCombination.Parameters.Parameters.Length];

            _scoreCounter.CountScore(result, _curAlcohol);
        }
    }
}