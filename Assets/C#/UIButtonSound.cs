using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    [SerializeField] float _volume = 1.0f;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource が見つかりません。UIButtonSound に AudioSource を追加してください。");
        }
        if (clickSound == null)
        {
            Debug.LogWarning("clickSound が設定されていません。AudioClip をアタッチしてください。");
        }
    }

    public void PlayClick()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource が null です。");
            return;
        }
        if (clickSound == null)
        {
            Debug.LogWarning("clickSound が null です。");
            return;
        }

        Debug.Log("PlayClick() が呼ばれました");
        audioSource.PlayOneShot(clickSound, _volume);
    }
}
