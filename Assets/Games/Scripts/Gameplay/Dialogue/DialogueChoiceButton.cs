using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class DialogueChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI choiceText;

    Choice choice;
    DialogueManager _dm;
    DialogueManager dm
    {
        get
        {
            if (!_dm) _dm = ServiceLocator.Resolve<DialogueManager>();
            return _dm;
        }
    }

    public void SetChoice(Choice choice)
    {
        this.choice = choice;

        if (choice)
        {
            choiceText.text = choice.text;
        }
    }

    public void Interact()
    {
        dm.MadeChoice(choice);
    }
}
