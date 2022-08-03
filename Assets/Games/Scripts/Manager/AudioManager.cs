using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private float transitionTime;
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioLibrary> bgmSources;

    private bool onTransition;
    private string current_play;

    public static float Volume 
    {
        get { return volume; }
        set
        {
            volume = Mathf.Clamp01(value);
            Instance.UpdateVolume();
        }
    }

    public static float Multiplier
    {
        get { return multiplier; }
        set
        {
            multiplier = Mathf.Clamp01(value);
            Instance.UpdateVolume();
        }
    }

    private static float volume = 1f;
    private static float multiplier = 1f;

    private void Awake()
    {
        OnInitialize();
        source = GetComponent<AudioSource>();
    }

    public static void PlayBGM(string id_bgm)
    {
        if (Instance.onTransition) return;
        if (id_bgm.Equals(Instance.current_play)) return;

        Instance.StartCoroutine(Instance.DOPlayBGM(id_bgm));
    }

    private void UpdateVolume()
    {
        source.volume = volume * multiplier;
    }

    private IEnumerator DOPlayBGM(string id_bgm)
    {
        onTransition = true;
        current_play = id_bgm;
        Tween transition = DOVirtual.Float(1f, 0f, transitionTime, (x) =>
        {
            source.volume = volume * x;
        });
        source.Stop();
        yield return transition.WaitForCompletion();
        AudioLibrary audio = bgmSources.Find(x => x.id_clip.Equals(id_bgm));
        if (audio != null) source.clip = audio.clip;
        source.Play();
        transition = DOVirtual.Float(0f, 1f, transitionTime, (x) =>
        {
            source.volume = volume * x;
        });
        yield return transition.WaitForCompletion();
        onTransition = false;
    }

    [System.Serializable]
    public class AudioLibrary
    {
        public string id_clip;
        public AudioClip clip;
    }
}
