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
            Debug.LogWarning("AudioSource ��������܂���BUIButtonSound �� AudioSource ��ǉ����Ă��������B");
        }
        if (clickSound == null)
        {
            Debug.LogWarning("clickSound ���ݒ肳��Ă��܂���BAudioClip ���A�^�b�`���Ă��������B");
        }
    }

    public void PlayClick()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource �� null �ł��B");
            return;
        }
        if (clickSound == null)
        {
            Debug.LogWarning("clickSound �� null �ł��B");
            return;
        }

        Debug.Log("PlayClick() ���Ă΂�܂���");
        audioSource.PlayOneShot(clickSound, _volume);
    }
}
