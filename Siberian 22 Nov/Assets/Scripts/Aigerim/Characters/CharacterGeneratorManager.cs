using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneratorManager : MonoBehaviour
{
    [SerializeField] private CharacterInfo _characterInfo;
    [SerializeField] private QuestionManager _questionManager;

    private void Awake()
    {
        _characterInfo = GetComponent<CharacterInfo>();
    }

    private void Start()
    {
        Debug.Log(_characterInfo.QuestionTrigger);
        _characterInfo.QuestionTrigger.OnEventCharacterTriggered += OnEventQuestionTriggered;
    }

    public void OnEventQuestionTriggered()
    {
        _questionManager.ShowQuestion(_characterInfo.QuestionText);

    }

    private void OnDisable()
    {
        _characterInfo.QuestionTrigger.OnEventCharacterTriggered -= OnEventQuestionTriggered;
    }
}
