using UnityEngine;
using Cocktails;
using TMPro;

namespace GameControllers
{
    public class ScoreCounter : MonoBehaviour
    {
        public event SendEvent ScoreCounted;

        [SerializeField] private TextMeshProUGUI _scoreText;
        private Parameter[] _characterParameters;
        private CocktailAdditivesSO _characterAlcohol;
        private float _score;


        private void Awake()
        {
            _score = 0;
        }

        public void SetCharacterParameters(Parameter[] parameters, CocktailAdditivesSO alcohol)
        {
            _characterParameters = parameters;
            _characterAlcohol = alcohol;
        }

        public void CountScore(Parameter[] parameters, CocktailAdditivesSO alcohol)
        {
            int result = 0;
            for (int i = 0; i < _characterParameters.Length; i++)
            {
                if (_characterParameters[i].Value <= parameters[i].Value)
                    result += _characterParameters[i].Value - (parameters[i].Value - _characterParameters[i].Value);
                else
                    result += parameters[i].Value - (_characterParameters[i].Value - parameters[i].Value);
                Debug.Log(parameters[i].Name + " " + _characterParameters[i].Value + " " + parameters[i].Value + " = " + result);
            }

            if (_characterAlcohol == alcohol)
            {
                if (result > 0)
                    result *= 2;
                else
                    result = 0;
            }
            _score += result * 10;
            if (_score < 0) 
                _score = 0;
            Debug.Log(_score + " Alcohol: " + (_characterAlcohol == alcohol).ToString());
            _scoreText.text = _score.ToString();
            ScoreCounted?.Invoke();
        }

        public void ChangeScore(int Value)
        {
            _score += Value;
            _scoreText.text = _score.ToString();
            if (_score < 0) _score = 0;
        }
    }
}