using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] private List<Character> _listCharacters;
    [SerializeField] private Parameter[] _allParamsScore;

    private void Start()
    {
        Character character = _listCharacters[UnityEngine.Random.Range(0, _listCharacters.Count)];

        Preset preset = character.GetRandomPreset();
        Object object1 = character.GetRandomObjects();
        Object object2 = character.GetRandomObjects();
        Question question = character.GetRandomQuestion();

        if (preset != null)
        {
            Instantiate(preset.GO);
        }

        if (object1 != null)
        {
            Instantiate(object1.GO);
        }

        if (object2 != null)
        {
            Instantiate(object2.GO);
        }

        _allParamsScore = CountParameterScore(preset, object1, object2);
        Debug.Log(question.Description);
    }

    public Parameter[] CountParameterScore(Preset preset, Object object1, Object object2)
    {
        Parameter[] result = new Parameter[preset.PresetParameter.Parameters.Length];

        for (int i = 0; i < result.Length; i++)
        {
            result[i]  = new Parameter(preset.PresetParameter.Parameters[i].Name, preset.PresetParameter.Parameters[i].Value + object1.ObjectParameter.Parameters[i].Value);
        }

        return result;
    }
}

[Serializable]
public class Character
{
    [SerializeField] private string _name;
    [SerializeField] private List<Preset> _presets;
    [SerializeField] private List<Object> _objects;
    [SerializeField] private List<Question> _questions;

    public Preset GetRandomPreset()
    {
        if (_presets.Count == 0) return null;

        return _presets[UnityEngine.Random.Range(0, _presets.Count)];
    }

    public Object GetRandomObjects()
    {
        if (_objects.Count == 0) return null;

        return _objects[UnityEngine.Random.Range(0, _objects.Count)];
    }

    public Question GetRandomQuestion()
    {
        if (_questions.Count == 0) return null;

        return _questions[UnityEngine.Random.Range(0, _questions.Count)];
    }
}



