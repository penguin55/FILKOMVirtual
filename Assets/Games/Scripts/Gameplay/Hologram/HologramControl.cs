using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HologramControl : MonoBehaviour
{
    [SerializeField] private float targetScale = 1f;

    public Transform model;

    public float glitchChance = 0.1f;
    public Material hologramMaterial;

    WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);

    void Awake()
    {
        model.DOScaleY(0f, 0f);
        model.gameObject.SetActive(false);
        DOTween.Sequence()
            .AppendInterval(0.5f)
            .AppendCallback( ()=> model.gameObject.SetActive(true))
            .Append(model.DOScaleY(targetScale, (targetScale / 1f) * 0.5f).SetEase(Ease.Linear).OnComplete(()=>StartCoroutine(StartGlitch())));
    }

    // Start is called before the first frame update
    IEnumerator StartGlitch()
    {
        DOVirtual.DelayedCall(5f, () => SceneManagement.LoadScene("MENU"));
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance)
            {
                //Do Glitch
                float originalGlowIntensity = hologramMaterial.GetFloat("_GlowIntensity");
                hologramMaterial.SetFloat("_GlitchIntensity", Random.Range(0.07f, 0.1f));
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity * Random.Range(0.14f, 0.44f));
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                hologramMaterial.SetFloat("_GlitchIntensity", 0f);
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity);
            }

            yield return glitchLoopWait;
        }
    }
}