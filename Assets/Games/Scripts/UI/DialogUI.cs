using System.Collections;
using System.Collections.Generic;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private DialogueManager dialogue;

    private void Awake()
    {
        dialogue = ServiceLocator.Resolve<DialogueManager>();
    }

    public void OpenDialoguePanel(bool open)
    {
        dialoguePanel.SetActive(open);
    }

    public void SetText(string name, string dialogueLine)
    {
        nameText.text = name.Replace('_', ' ');
        dialogueText.text = dialogueLine;
    }

    public void NextIterator()
    {
        dialogue.NextLine();
    }
}
