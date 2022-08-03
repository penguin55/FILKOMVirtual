using DG.Tweening;
using UnityEngine;

public class ParalaxMainMenu : MonoBehaviour
{
    [SerializeField] private float paralaxTime;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    Tween paralaxTween;

    Vector3 baseAngle;

    private void OnEnable()
    {
        baseAngle = transform.eulerAngles;
        paralaxTween?.Complete();

        paralaxTween = Rotate();
    }

    private void OnDisable()
    {
        if (paralaxTween != null)
        {
            paralaxTween.Complete();
            paralaxTween = null;
        }
    }

    private Tween Rotate()
    {
        return DOTween.Sequence()
            .AppendInterval(2f)
            .Append(transform.DOLocalRotate(new Vector3(baseAngle.x, maxAngle, baseAngle.z), paralaxTime).SetEase(Ease.Linear))
            .AppendInterval(2f)
            .Append(transform.DOLocalRotate(new Vector3(baseAngle.x, minAngle, baseAngle.z), paralaxTime).SetEase(Ease.Linear))
            .SetLoops(-1);
    }
}
