using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// 【背景のコントロール用クラス】
///     背景は3枚、カメラから見切れたら回り込む
/// </summary>
public class BackGroundController : MonoBehaviour
{
    [SerializeField] Transform[] backGrounds;
    [SerializeField] Transform player;

    [SerializeField] float width;
    [SerializeField] float moveScale = 0.2f;

    Vector3 savePlayerPos;

    private void Start()
    {
        savePlayerPos = player.position;
    }


    private void Update()
    {
        Vector3 delta = player.position - savePlayerPos;
        // 背景スクロール（全体を動かす）
        foreach (var bg in backGrounds)
        {
            bg.position += new Vector3(delta.x * moveScale, 0f, 0f);
        }

        for (int i = 0; i < backGrounds.Length; i++)
        {
            float distanceFromPlayer = backGrounds[i].position.x - player.position.x;
            if (distanceFromPlayer < -width * 1.5f)
            {
                backGrounds[i].position += new Vector3(width * backGrounds.Length, 0, 0);
            }
            else if (distanceFromPlayer > width * 1.5f)
            {
                backGrounds[i].position -= new Vector3(width * backGrounds.Length, 0, 0);
            }


        }
        savePlayerPos = player.position;
    }
}