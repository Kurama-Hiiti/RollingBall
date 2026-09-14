using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    //瞬間移動処理

    //移動先
    [SerializeField]
    private Transform warpPos;

    //プレイヤーに設定されているワープ可能かのフラグをコピーするためのブール値
    private bool copyIsWarp;

    private void OnTriggerEnter(Collider other)
    { 

        if (other.gameObject.CompareTag("Player"))
        {
            //プレイヤーのスクリプト取得
            Player player = other.gameObject.GetComponent<Player>();

            //プレイヤーのワープ可能フラグをコピー
            copyIsWarp = player.IsWarp;

            if (player != null && player.IsWarp && warpPos != null) 
            {
                //ワープ処理
                player.Warp(warpPos);
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //プレイヤーが離れる際の処理
        if (other.gameObject.CompareTag("Player") && !copyIsWarp)
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.WarpFlagChange();
        }
    }
}
