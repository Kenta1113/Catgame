using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;

    [Header("各シーン用BGM")]
    [SerializeField] private AudioClip defaultBGM;       // 通常ゲーム中
    [SerializeField] private AudioClip titleBGM;         // タイトル
    [SerializeField] private AudioClip gameOverBGM;      // ゲームオーバー
    [SerializeField] private AudioClip gameClearBGM;     // クリア
    [SerializeField] private float _volume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = _volume;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーン名に応じてBGMを切り替え
        switch (scene.name)
        {
            case "TitleScene":
                PlayBGM(titleBGM);
                break;
            case "Stage1":
                PlayBGM(defaultBGM);
                break;
            default:
                StopBGM(); // 不明なシーンでは無音に
                break;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    // ゲームオーバー用
    public void PlayGameOverBGM()
    {
        PlayBGM(gameOverBGM);
    }

    // ゲームクリア用
    public void PlayGameClearBGM()
    {
        PlayBGM(gameClearBGM);
    }
}
