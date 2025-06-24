using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    [SerializeField] float _volume = 1.0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource が見つかりません！");
        }
    }

    public void PlayClick()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound, _volume);
            Debug.Log("ボタン音再生");
        }
        else
        {
            Debug.LogWarning("AudioSource または clickSound が設定されていません！");
        }
    }
}
