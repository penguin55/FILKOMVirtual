using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : Singleton<SceneManagement>
{
    public float minimumLoadTime = 1f;
    public float transitionTime;
    public CanvasGroup canvas;
    public Image loading;

    private static bool onLoad = false;
    private static bool readyToLoad = true;

    void Awake()
    {
        OnInitialize();
    }

    public static void LoadScene(string scene_name)
    {
        if (onLoad) return;
        Instance.StartCoroutine(Instance.DoLoadScene(scene_name));
    }

    public static void ReadyToLoad()
    {
        readyToLoad = true;
    }

    public static bool CompleteLoading()
    {
        return !onLoad;
    }

    private IEnumerator DoLoadScene(string scene_name)
    {
        onLoad = true;
        readyToLoad = false;
        loading.fillAmount = 0f;
        Tween transition = canvas.DOFade(1f, transitionTime).SetEase(Ease.Linear);
        DOVirtual.Float(1f, 0.4f, transitionTime, x => AudioManager.Multiplier = x).SetEase(Ease.Linear);

        yield return transition.WaitForCompletion();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene_name);
        asyncOperation.allowSceneActivation = false;

        
        DOVirtual.Float(0f, 0.9f, minimumLoadTime, (x) => loading.fillAmount = x).SetEase(Ease.Linear);
        yield return new WaitForSeconds(minimumLoadTime);
        asyncOperation.allowSceneActivation = true;
        yield return new WaitUntil(()=> asyncOperation.isDone);
        yield return new WaitUntil(() => readyToLoad);
        loading.fillAmount = 1f;
        canvas.DOFade(0f, transitionTime).SetEase(Ease.Linear);
        DOVirtual.Float(0.4f, 1f, transitionTime, x => AudioManager.Multiplier = x).SetEase(Ease.Linear);
        onLoad = false;
    }
}
