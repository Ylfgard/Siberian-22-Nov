using GameControllers;
using System.Collections;
using UnityEngine;


public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private QuestionTrigger _questionTrigger;
    [SerializeField] private string _questionText;
    [SerializeField] private Parameter[] _allParamsScore;
    [SerializeField] private CocktailAdditivesSO _alcohol;
    [SerializeField] private QuestionManager _questionManager;

    private ScoreCounter _scoreCounter;

    public QuestionTrigger QuestionTrigger => _questionTrigger;
    public string QuestionText => _questionText;


    private void Awake()
    {
        _scoreCounter = FindObjectOfType<ScoreCounter>();
    }


    public void OnEventQuestionTriggered()
    {
        Debug.Log("event");
        _questionManager.ShowQuestion(QuestionText);
    }

    private void OnDisable()
    {
        QuestionTrigger.OnEventCharacterTriggered -= OnEventQuestionTriggered;
    }

    public void SetCharacterInfo(QuestionTrigger questionTrigger, string textQuestion, Parameter[] parameterScore, CocktailAdditivesSO alcohol)
    {
        _questionTrigger = questionTrigger;
        _questionText = textQuestion;
        _allParamsScore = parameterScore;
        _alcohol = alcohol;
        _scoreCounter.SetCharacterParameters(_allParamsScore, _alcohol);
        QuestionTrigger.Func(OnEventQuestionTriggered);
    }
}
