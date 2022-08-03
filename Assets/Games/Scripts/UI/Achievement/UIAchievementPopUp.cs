using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAchievementPopUp : MonoBehaviour
{
    [SerializeField] private GameObject achievementPanelParent;
    [SerializeField] private RectTransform achievementPanel;
    [SerializeField] private TextMeshProUGUI achievementTitle;
    [SerializeField] private float timeToAppear;
    [SerializeField] private Ease easeIn;
    [SerializeField] private Ease easeOut;
    [SerializeField] private float duration;

    private List<string> queueAchievementPopUp = new List<string>();

    private Tween mainTween;
    private bool showUp;

    public void QueuePopUpAchievement(string achievementName)
    {
        AddQueue(achievementName);
        
        if (showUp && queueAchievementPopUp.Count > 0 || (mainTween != null && mainTween.IsPlaying())) return;
        mainTween = DOVirtual.DelayedCall(0.5f, () => StartQueue()); 
    }

    private void AddQueue(string achievementName)
    {
        if (queueAchievementPopUp.Exists(x => x.Equals(achievementName))) return;
        queueAchievementPopUp.Add(achievementName);
    }

    private void StartQueue()
    {
        if (queueAchievementPopUp.Count == 0)
        {
            PopUp(false);
            return;
        }
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => {
            ResetPosition();
            achievementTitle.text = queueAchievementPopUp[0];
            queueAchievementPopUp.RemoveAt(0);
            PopUp(true);
        });
        sequence.Append(achievementPanel.DOAnchorPosX(353f, timeToAppear).SetEase(easeIn));
        sequence.AppendInterval(duration);
        sequence.Append(achievementPanel.DOAnchorPosX(-400f, timeToAppear).SetEase(easeOut));
        sequence.AppendInterval(0.5f);
        sequence.OnComplete(()=> CheckQueue());
    }

    private void CheckQueue()
    {
        if (queueAchievementPopUp.Count > 0)
        {
            StartQueue();
        } else
        {
            PopUp(false);
        }
    }

    private void PopUp(bool flag)
    {
        showUp = flag;
        achievementPanelParent.SetActive(showUp);
    }

    private void ResetPosition()
    {
        achievementPanel.DOAnchorPosX(-400f, 0f);
    }
}
