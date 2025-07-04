using System.Collections.Generic;
using UnityEngine;

public class UIHPmanager : MonoBehaviour
{
    [SerializeField] GameObject _pawPrefab;
    [SerializeField] Transform _hpContainer;

    private List<GameObject> _paws = new List<GameObject>();

    /// <summary>
    /// 現在の表示数との差分だけ、右端に追加・右端から削除します
    /// </summary>
    public void SetHp(int hp)
    {
        int current = _paws.Count;

        // HP増加：右端に追加
        if (hp > current)
        {
            for (int i = 0; i < hp - current; i++)
            {
                GameObject newPaw = Instantiate(_pawPrefab, _hpContainer);
                // 明示的に末尾へ
                newPaw.transform.SetAsLastSibling();
                _paws.Add(newPaw);
            }
        }
        // HP減少：右端から削除
        else if (hp < current)
        {
            for (int i = 0; i < current - hp; i++)
            {
                int lastIndex = _paws.Count - 1;
                Destroy(_paws[lastIndex]);
                _paws.RemoveAt(lastIndex);
            }
        }
        // hp == current は何もしない
    }
}
