using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 관리용 네임스페이스

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    public AudioClip mainBgm;
    public AudioClip inGameBgm;
    public AudioClip jumpEffect;
    public AudioClip popEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        audioSource.clip = mainBgm;
        audioSource.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "IntroScene")
        {
            PlayBGM(mainBgm);
        }
        else if (scene.name == "InGameScene")
        {
            PlayBGM(inGameBgm);
        }
    }

    // BGM 전환용 공개 메서드
    public void PlayBGM(AudioClip clip)
    {
        if (audioSource.clip == clip) return;  // 중복 재생 방지

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    // 예: 점프 효과음 재생
    public void PlayJumpEffect()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(jumpEffect);
        audioSource.loop = true;
    }
    
    public void PlayPopEffect()
    {     
        audioSource.loop = false;
        audioSource.PlayOneShot(popEffect);
        audioSource.loop = true;
    }
}