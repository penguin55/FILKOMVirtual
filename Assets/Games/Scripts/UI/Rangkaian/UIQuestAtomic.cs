using Sirenix.OdinInspector;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestAtomic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private Image checklist;
    [ReadOnly, SerializeField] private Quest quest;

    private UIRangkaian uiRangkaian;

    private ImageProvider imageProvider 
    { 
        get 
        {
            if (!_imageProvider) _imageProvider = ServiceLocator.Resolve<ImageProvider>();

            return _imageProvider;
        } 
    }
    private ImageProvider _imageProvider;

    public void Init(Quest quest, UIRangkaian uiRangkaian)
    {
        this.quest = quest;
        this.uiRangkaian = uiRangkaian;
        UpdateQuest();
    }

    public void UpdateQuest()
    {
        if (quest != null)
        {
            questTitle.text = quest.questData.questName;
            if (quest.questClear) checklist.sprite = imageProvider.RequestSprite("quest-clear");
            else checklist.sprite = imageProvider.RequestSprite("quest-unclear");
        } 
    }

    public void OpenDetail()
    {
        uiRangkaian.OpenDetailQuest(quest);
    }
}
