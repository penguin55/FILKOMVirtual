using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class UIRangkaian : BasePanel
{
    [SerializeField] private Button rangkaianButton;
    [SerializeField] private Button cancelArea;
    [SerializeField] private Button questDetailBackButton;

    [Header("Rangkaian Properties")]
    [SerializeField] private GameObject questPrefabs;
    [SerializeField] private TextMeshProUGUI rangkaianTitle;
    [SerializeField] private RectTransform questsScroll;
    [SerializeField] private RectTransform questsListParent;

    [Header("Quest Detail")]
    [SerializeField] private RectTransform questsDetailParent;
    [SerializeField] private Image questChecklist;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;

    [Header("Dev Message")]
    [SerializeField] private GameObject panelMessage;
    [SerializeField] private TextMeshProUGUI rangkaianTitleMessage;
    [SerializeField] private TextMeshProUGUI rangkaianDescMessage;

    private PenugasanSystem rangkaian;
    private ImageProvider imageProvider;
    private bool busy;

    private void Start()
    {
        Init();

        rangkaian = ServiceLocator.Resolve<PenugasanSystem>();
        imageProvider = ServiceLocator.Resolve<ImageProvider>();
    }

    private void OnDisable()
    {
        DestroyInit();
    }

    protected override void Init()
    {
        base.Init();
        rangkaianButton.onClick.AddListener(() =>
        {
            rangkaianButton.gameObject.SetActive(false);
            OpenRangkaianPanel(true);
        });

        questDetailBackButton.onClick.AddListener(()=>
        {
            OpenDetailPanel(false);
        });


    }

    protected override void DestroyInit()
    {
        base.DestroyInit();
        rangkaianButton.onClick.RemoveAllListeners();
        questDetailBackButton.onClick.RemoveAllListeners();
    }

    private void OpenRangkaianPanel(bool active)
    {
        InvokeCancelArea(active);
        ActivatePanel(active);
        if (this.active) FillContentPanel();
    }

    private void FillContentPanel()
    {
        UIQuestAtomic[] atomicQuestChild = questsListParent.GetComponentsInChildren<UIQuestAtomic>();
        List<Quest> activeQuest = rangkaian.GetRangkaian().GetActiveQuest();

        int maxValue = atomicQuestChild.Length > activeQuest.Count ? atomicQuestChild.Length : activeQuest.Count;

        for (int i = 0; i < maxValue; i++)
        {
            if (i < atomicQuestChild.Length)
            {
                if (i < activeQuest.Count) atomicQuestChild[i].Init(activeQuest[i], this);
                else Destroy(atomicQuestChild[i].gameObject);
            } else
            {
                UIQuestAtomic atomicQuestUI = Instantiate(questPrefabs, questsListParent).GetComponent<UIQuestAtomic>();
                atomicQuestUI.Init(activeQuest[i], this);
            }
        }

        Vector2 size = questsListParent.sizeDelta;
        size.y = (30f * activeQuest.Count) + (3f * (activeQuest.Count - 1));

        size.y = Mathf.Max(306f, size.y);

        questsListParent.sizeDelta = size;
        questsListParent.anchoredPosition = new Vector2(0f, 306f - size.y);
    }

    private void InvokeCancelArea(bool invoke)
    {
        cancelArea.gameObject.SetActive(invoke);

        if (invoke)
        {
            cancelArea.onClick.AddListener(() =>
            {
                rangkaianButton.gameObject.SetActive(true);
                OpenRangkaianPanel(false);
                OpenDetailPanel(false);
            });
        } else
        {
            cancelArea.onClick.RemoveAllListeners();
        }
    }

    private void OpenDetailPanel(bool open)
    {
        if (busy) return;
        busy = true;
        if (open)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(questsDetailParent.DOLocalMoveY(0f, 0.3f).SetEase(Ease.InOutBack));
            sequence.Join(questsScroll.DOLocalMoveY(-306f, 0.3f).SetEase(Ease.InOutBack));
            sequence.OnComplete(() => busy = false);
        } else
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(questsDetailParent.DOLocalMoveY(306f, 0.3f).SetEase(Ease.InOutBack));
            sequence.Join(questsScroll.DOLocalMoveY(0f, 0.3f).SetEase(Ease.InOutBack));
            sequence.OnComplete(() => busy = false);
        }
    }

    public void OpenDetailQuest(Quest quest)
    {
        if (busy) return;

        questChecklist.sprite = imageProvider.RequestSprite(quest.questClear ? "quest-clear" : "quest-unclear");
        questName.text = quest.questData.questName;
        questDescription.text = quest.questData.questDescription;

        OpenDetailPanel(true);
    }

    public void ChangeActiveRangkaian(string title)
    {
        rangkaianTitle.text = title;
    }

    public void CloseDevMessage()
    {
        panelMessage.SetActive(false);
    }

    public void ShowDevMessage(string rangkaianName, string rangkaianDescription)
    {
        panelMessage.SetActive(true);
        rangkaianTitleMessage.text = rangkaianName;
        rangkaianDescMessage.text = rangkaianDescription;
    }
}
