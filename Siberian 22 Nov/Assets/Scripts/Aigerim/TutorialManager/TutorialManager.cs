using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Book;

public class TutorialManager : MonoBehaviour
{
    [TextArea(7, 15)]
    [SerializeField] private List<string> _tutorialText;

    [SerializeField] private Book.Book _book;

    private void OnEnable()
    {
        _book.OnEventOpenedBook += ShowFirstSentence;
    }


    public void ShowFirstSentence()
    {

    }

}
