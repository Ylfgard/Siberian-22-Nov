using System;
using UnityEngine;


public class CharacterContainer : MonoBehaviour
{

}

[Serializable]
public class Preset
{
    [SerializeField] private GameObject _preset;
    [SerializeField] private PresetsParametersSO _parameter;

    public GameObject GO => _preset;
    public PresetsParametersSO PresetParameter => _parameter;
}

[Serializable]
public class Object
{
    [SerializeField] private GameObject _object;
    [SerializeField] private ObjectParametersSO _parameter;

    public GameObject GO => _object;

    public ObjectParametersSO ObjectParameter => _parameter;
}

[Serializable]
public class Question
{
    [SerializeField][TextArea(5, 10)] private string _question;

    public string Description => _question;
}

