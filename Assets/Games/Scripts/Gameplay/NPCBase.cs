using System.Collections;
using Ink.Runtime;
using TMPro;
using TomGustin.GameDesignPattern;
using TomGustin.Interface;
using UnityEngine;
using UnityEngine.Events;

public class NPCBase : MonoBehaviour, IInteractable
{
    [SerializeField] protected PenugasanDataValidator dataValidator;
    //[SerializeField] protected PointofInterest poi;
    [SerializeField] protected string npcName;
    [SerializeField] protected string npcNickname;
    [SerializeField] protected string npcRole;
    [SerializeField] protected TextAsset inkAsset;
    [SerializeField] protected TextMeshPro nameText;
    [SerializeField] protected UnityEvent onNotifyStory;
    [SerializeField] protected UnityEvent onCompleteStory;

    Story story;

    private DialogueManager dialogue;

    private void Awake()
    {
        dialogue = ServiceLocator.Resolve<DialogueManager>();

        story = new Story(inkAsset.text);
        
        story.BindExternalFunction("Notify", (string notifyName) =>
        {
            Notify(notifyName);
        });
        story.BindExternalFunction("GetNPCName", () =>
        {
            return npcName;
        });
        story.BindExternalFunction("GetNPCNickname", () =>
        {
            return npcNickname;
        });
        story.BindExternalFunction("GetNPCRole", () =>
        {
            return npcRole;
        });
        story.BindExternalFunction("GetRangkaian", () =>
        {
            switch (RangkaianManager.ActiveRangkaian)
            {
                case "rangkaian_1":
                    return "1";
                case "rangkaian_2":
                    return "2";
                case "rangkaian_3":
                    return "3";
                default:
                    return "NONE";
            }
        });
        story.BindExternalFunction("GetPlayerName", () =>
        {
            return AccountManager.Instance.GetActiveAccount().user_name;
        });
        story.BindExternalFunction("GetQuestStatus", () =>
        {
            return (dataValidator.Active ? 1 : 0) + (dataValidator.Cleared ? 1 : 0);
        });

        nameText.text = $"{npcRole}";
    }

    void IInteractable.Interact(PlayerController controller)
    {
        StartCoroutine(Dialog());
    }

    private IEnumerator Dialog()
    {
        story.ResetState();
        dialogue.PlayDialogue(story);

        yield return new WaitWhile(()=> dialogue.IsDialogueActive());

        onCompleteStory?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITriggerCollider>(out ITriggerCollider triggerCollider))
        {
            triggerCollider.OnEnter(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ITriggerCollider>(out ITriggerCollider triggerCollider))
        {
            triggerCollider.OnExit(gameObject);
        }
    }

    private void Notify(string notifyName)
    {
        //poi?.OnNotifyMessage(notifyName);
        onNotifyStory?.Invoke();
        if (notifyName.ToLower().Equals("clear")) dataValidator.Clear();
    }

    InteractableType IInteractable.GetInteractType()
    {
        return InteractableType.Chat;
    }
}
