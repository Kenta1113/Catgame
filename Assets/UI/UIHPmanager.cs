using System.Collections.Generic;
using UnityEngine;

public class UIHPmanager : MonoBehaviour
{
    [SerializeField] GameObject _pawPrefab;
    [SerializeField] Transform _hpContainer;

    private List<GameObject> _paws = new List<GameObject>();

    /// <summary>
    /// ���݂̕\�����Ƃ̍��������A�E�[�ɒǉ��E�E�[����폜���܂�
    /// </summary>
    public void SetHp(int hp)
    {
        int current = _paws.Count;

        // HP�����F�E�[�ɒǉ�
        if (hp > current)
        {
            for (int i = 0; i < hp - current; i++)
            {
                GameObject newPaw = Instantiate(_pawPrefab, _hpContainer);
                // �����I�ɖ�����
                newPaw.transform.SetAsLastSibling();
                _paws.Add(newPaw);
            }
        }
        // HP�����F�E�[����폜
        else if (hp < current)
        {
            for (int i = 0; i < current - hp; i++)
            {
                int lastIndex = _paws.Count - 1;
                Destroy(_paws[lastIndex]);
                _paws.RemoveAt(lastIndex);
            }
        }
        // hp == current �͉������Ȃ�
    }
}
