using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;
using static AchievementSystem;

public class UIAchievement : BasePanel
{
    [Header("Properties")]
    [SerializeField] private Button achievementButton;
    [SerializeField] private Button cancelArea;
    [SerializeField] private Button achievementDetailBackButton;

    [Header("Achievement Properties")]
    [SerializeField] private GameObject achievementPrefabs;
    [SerializeField] private RectTransform achievementScroll;
    [SerializeField] private RectTransform achievementListParent;
    [SerializeField] private Color achieve;
    [SerializeField] private Color unachieve;

    [Header("Achievement Detail")]
    [SerializeField] private RectTransform achievementDetailParent;
    [SerializeField] private Image achievementChecklist;
    [SerializeField] private TextMeshProUGUI achievementName;
    [SerializeField] private TextMeshProUGUI achievementDescription;

    private AchievementSystem achievementSystem;
    private ImageProvider imageProvider;
    private bool busy;

    private void Start()
    {
        Init();

        achievementSystem = ServiceLocator.Resolve<AchievementSystem>();
        imageProvider = ServiceLocator.Resolve<ImageProvider>();
    }

    private void OnDisable()
    {
        DestroyInit();
    }

    protected override void Init()
    {
        base.Init();
        achievementButton.onClick.AddListener(() =>
        {
            achievementButton.gameObject.SetActive(false);
            OpenAchievementPanel(true);
        });

        achievementDetailBackButton.onClick.AddListener(() =>
        {
            OpenDetailPanel(false);
        });
    }

    protected override void DestroyInit()
    {
        base.DestroyInit();
        achievementButton.onClick.RemoveAllListeners();
        achievementDetailBackButton.onClick.RemoveAllListeners();
    }

    private void OpenAchievementPanel(bool active)
    {
        InvokeCancelArea(active);
        ActivatePanel(active);
        if (this.active) FillContentPanel();
    }

    private void FillContentPanel()
    {
        UIAchievementAtomic[] atomicAchievementChilds = achievementListParent.GetComponentsInChildren<UIAchievementAtomic>();
        List<Achievement> activeAchievement = achievementSystem.GetAchievements();

        int maxValue = atomicAchievementChilds.Length > activeAchievement.Count ? atomicAchievementChilds.Length : activeAchievement.Count;

        for (int i = 0; i < maxValue; i++)
        {
            if (i < atomicAchievementChilds.Length)
            {
                if (i < activeAchievement.Count) atomicAchievementChilds[i].Init(activeAchievement[i], this);
                else Destroy(atomicAchievementChilds[i].gameObject);
            }
            else
            {
                UIAchievementAtomic atomicQuestUI = Instantiate(achievementPrefabs, achievementListParent).GetComponent<UIAchievementAtomic>();
                atomicQuestUI.Init(activeAchievement[i], this);
            }
        }

        Vector2 size = achievementListParent.sizeDelta;
        size.y = (55f * activeAchievement.Count) + (3f * (activeAchievement.Count - 1));

        size.y = Mathf.Max(306f, size.y);

        achievementListParent.sizeDelta = size;
        achievementListParent.anchoredPosition = new Vector2(0f, 306f - size.y);
    }

    private void InvokeCancelArea(bool invoke)
    {
        cancelArea.gameObject.SetActive(invoke);

        if (invoke)
        {
            cancelArea.onClick.AddListener(() =>
            {
                achievementButton.gameObject.SetActive(true);
                OpenAchievementPanel(false);
                OpenDetailPanel(false);
            });
        }
        else
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
            sequence.Append(achievementDetailParent.DOLocalMoveY(0f, 0.3f).SetEase(Ease.InOutBack));
            sequence.Join(achievementScroll.DOLocalMoveY(-306f, 0.3f).SetEase(Ease.InOutBack));
            sequence.OnComplete(() => busy = false);
        }
        else
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(achievementDetailParent.DOLocalMoveY(306f, 0.3f).SetEase(Ease.InOutBack));
            sequence.Join(achievementScroll.DOLocalMoveY(0f, 0.3f).SetEase(Ease.InOutBack));
            sequence.OnComplete(() => busy = false);
        }
    }

    public void OpenDetailAchievement(Achievement achievement)
    {
        if (busy) return;

        achievementChecklist.color = achievement.isAchieve ? achieve : unachieve;
        achievementName.text = achievement.achievementData.achievementName;
        achievementDescription.text = achievement.isAchieve ? achievement.achievementData.achievementDescription : "<color=\"red\">[ACHIEVEMENT BELUM DIDAPATKAN]</color>";

        OpenDetailPanel(true);
    }

    public (Color achieve, Color unachieve) GetColor()
    {
        return (achieve, unachieve);
    }
}
