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
            Debug.LogWarning("AudioSource ��������܂���I");
        }
    }

    public void PlayClick()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound, _volume);
            Debug.Log("�{�^�����Đ�");
        }
        else
        {
            Debug.LogWarning("AudioSource �܂��� clickSound ���ݒ肳��Ă��܂���I");
        }
    }
}
