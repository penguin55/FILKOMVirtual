using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("References Choice")]
    [SerializeField] private GameObject choiceParent;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;

    private DialogUI dialogUI;
    private PlayerController player;
    private Story inkStory;

    private bool inChoice;
    private bool dialogueActive;

    private void Awake()
    {
        dialogUI = ServiceLocator.Resolve<DialogUI>();
        player = ServiceLocator.Resolve<PlayerController>();
    }

    public void PlayDialogue(Story story)
    {
        dialogueActive = true;
        inkStory = story;
        player.Pause(true);

        dialogUI.OpenDialoguePanel(true);

        NextLine();
    }

    public void NextLine()
    {
        if (inChoice) return;
        NextDialogueLine();
    }

    private void NextDialogueLine()
    {
        if (inkStory.canContinue)
        {
            string rawText = inkStory.Continue();

            string[] splittedText = rawText.Split(':');

            dialogUI.SetText(splittedText[0], splittedText[1]);

            if (inkStory.currentChoices.Count > 0)
            {
                RenderChoice(true);
            }
        }
        else
        {
            dialogUI.OpenDialoguePanel(false);
            player.Pause(false);
            dialogueActive = false;
        }
    }

    public void MadeChoice(Choice choice)
    {
        inkStory.ChooseChoiceIndex(choice.index);
        NextDialogueLine();
        RenderChoice(false);
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    private void RenderChoice(bool render)
    {
        choiceParent.SetActive(render);
        inChoice = render;
        if (render)
        {
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < inkStory.currentChoices.Count)
                {
                    choiceButtons[i].gameObject.SetActive(true);
                    choiceButtons[i].SetChoice(inkStory.currentChoices[i]);
                }
                else
                {
                    choiceButtons[i].SetChoice(null);
                    choiceButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
