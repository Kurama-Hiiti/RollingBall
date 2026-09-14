using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    //ゴール処理の呼び出しスクリプト

    //プレイヤーがゴールゾーンへ触れた時に処理を呼び出す

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnGoalReached();
        }
    }

}
