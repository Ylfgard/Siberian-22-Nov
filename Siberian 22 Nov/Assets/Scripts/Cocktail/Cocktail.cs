using UnityEngine;
using GameControllers;
using FMODUnity;

namespace Cocktails
{
    public class Cocktail : MonoBehaviour, IResetable
    {
        //[SerializeField] private EventReference _cocktailFinish;
        [SerializeField] private Color _failCocktailMaterial;

        private ScoreCounter _scoreCounter;
        private CocktailParametersSO _curGlassParameters;
        private MeshRenderer _curDrinkMesh;
        private CocktailCombinationSO _curCocktailCombination;
        private CocktailAdditivesSO _curAlcohol;
        private CocktailParametersSO _curDecoration;

        public CocktailAdditivesSO CurAlcohol => _curAlcohol;

        private void Awake()
        {
            _scoreCounter = FindObjectOfType<ScoreCounter>();   
            Reset();
        }

        public void Reset()
        {
            if(_curDrinkMesh != null) _curDrinkMesh.enabled = false;
            _curDrinkMesh = null;
            _curAlcohol = null;
            _curCocktailCombination = null;
            _curGlassParameters = null;
            _curDecoration = null;
        }

        public bool SelectGlass(CocktailParametersSO glassParameters, MeshRenderer drinkMesh)
        {
            if (_curAlcohol == null)
            {
                Debug.Log("Selected " + glassParameters.name);
                _curGlassParameters = glassParameters;
                _curDrinkMesh = drinkMesh;
                return true;
            }
            return false;
        }

        public bool PourCocktail(CocktailCombinationSO combination, CocktailAdditivesSO alcohol)
        {
            if (_curGlassParameters == null) return false;
            if (combination == null)
            {
                _curDrinkMesh.material.color = _failCocktailMaterial;
                Debug.Log("Какая гадость!");
            }
            else
            {
                _curDrinkMesh.material.color = combination.Color;
                Debug.Log(combination.name + " " + alcohol.name);
            }
            _curDrinkMesh.enabled = true;
            _curCocktailCombination = combination;
            _curAlcohol = alcohol;
            return true;
        }

        public void SetDecoration(CocktailParametersSO decoration)
        {
            _curDecoration = decoration;
        }

        public bool GiveCocktail()
        {
            if(_curAlcohol == null)
            {
                Debug.Log("Вы не налили коктейль!");
                return false;
            }

            Parameter[] result = new Parameter[_curGlassParameters.Parameters.Length];
            for(int i = 0; i < result.Length; i++)
            {
                if (_curCocktailCombination == null)
                    result[i] = new Parameter(_curGlassParameters.Parameters[i].Name, 0);
                else if (_curDecoration == null)
                    result[i] = new Parameter(_curGlassParameters.Parameters[i].Name, _curGlassParameters.Parameters[i].Value +
                        _curCocktailCombination.Parameters.Parameters[i].Value + _curAlcohol.Parameters[i].Value);
                else
                    result[i] = new Parameter(_curGlassParameters.Parameters[i].Name, _curDecoration.Parameters[i].Value +
                        _curCocktailCombination.Parameters.Parameters[i].Value + _curGlassParameters.Parameters[i].Value
                        + _curAlcohol.Parameters[i].Value);
            }

            _scoreCounter.CountScore(result, _curAlcohol);
            //RuntimeManager.PlayOneShot(_cocktailFinish);
            Reset();
            return true;
        }
    }
}