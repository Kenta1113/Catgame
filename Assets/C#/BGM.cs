using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;

    [Header("�e�V�[���pBGM")]
    [SerializeField] private AudioClip defaultBGM;       // �ʏ�Q�[����
    [SerializeField] private AudioClip titleBGM;         // �^�C�g��
    [SerializeField] private AudioClip gameOverBGM;      // �Q�[���I�[�o�[
    [SerializeField] private AudioClip gameClearBGM;     // �N���A
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
        // �V�[�����ɉ�����BGM��؂�ւ�
        switch (scene.name)
        {
            case "TitleScene":
                PlayBGM(titleBGM);
                break;
            case "Stage1":
                PlayBGM(defaultBGM);
                break;
            default:
                StopBGM(); // �s���ȃV�[���ł͖�����
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

    // �Q�[���I�[�o�[�p
    public void PlayGameOverBGM()
    {
        PlayBGM(gameOverBGM);
    }

    // �Q�[���N���A�p
    public void PlayGameClearBGM()
    {
        PlayBGM(gameClearBGM);
    }
}
