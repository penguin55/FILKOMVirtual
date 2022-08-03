using Sirenix.OdinInspector;
using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class UIAchievementAtomic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementTitle;
    [SerializeField] private Image achievementImage;
    [SerializeField, ReadOnly] private AchievementSystem.Achievement achievement;

    private UIAchievement uiAchievement;

    private ImageProvider imageProvider
    {
        get
        {
            if (!_imageProvider) _imageProvider = ServiceLocator.Resolve<ImageProvider>();
            return _imageProvider;
        }
    }
    private ImageProvider _imageProvider;

    public void Init(AchievementSystem.Achievement achievement, UIAchievement uiAchievement)
    {
        this.achievement = achievement;
        this.uiAchievement = uiAchievement;

        UpdateAchievement();
    }

    public void UpdateAchievement()
    {
        if (achievement != null)
        {
            achievementTitle.text = achievement.achievementData.achievementName;
            achievementImage.color = achievement.isAchieve ? uiAchievement.GetColor().achieve : uiAchievement.GetColor().unachieve;
        }
    }

    public void OpenDetail()
    {
        uiAchievement.OpenDetailAchievement(achievement);
    }
}
