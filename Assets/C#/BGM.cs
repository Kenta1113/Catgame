using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    [SerializeField] float _BGMVolume = 0.5f;
    private AudioSource audioSource;

    void Awake()
    {
        // �V�[�����܂����ł��j������Ȃ��悤�ɂ���i�C�Ӂj
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = _BGMVolume;
        audioSource.Play();
    }
}
