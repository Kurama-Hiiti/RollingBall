using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoard : MonoBehaviour
{
    //踏むと指定された方向へ加速する板の処理

    //力を加える方向
    [SerializeField]
    private Vector3 forceDir;

    //加える力
    [SerializeField]
    private float addForcePower;

    //ボールの勢いを無くすかどうかの判定
    [SerializeField]
    private bool isStop;

    //SEを鳴らすか判定
    [SerializeField]
    private bool isSound = true;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (other.gameObject.CompareTag("Player"))
        {
            //既存の速度を消しダッシュボードの方向へ確実に進む(直角に曲がれるようにするため)
            if (isStop)
            {
                rb.velocity = Vector3.zero; // 一度速度をリセット
                rb.AddForce(forceDir.normalized * addForcePower, ForceMode.Impulse);

                if (isSound)
                {
                    SoundManager.instance.PlaySE(SoundManager.SoundType.Dash);
                }
            }
            else//既存の速度を保持し純粋な加速を行う
            {
                rb.AddForce(forceDir.normalized * addForcePower, ForceMode.Impulse);
                if (isSound)
                {
                    SoundManager.instance.PlaySE(SoundManager.SoundType.Dash);
                }
            }

        }
    }
}
