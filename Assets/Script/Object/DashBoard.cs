using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoard : MonoBehaviour
{
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
            if (isStop)
            {
                rb.velocity = Vector3.zero; // 一度速度をリセット
                rb.AddForce(forceDir.normalized * addForcePower, ForceMode.Impulse);

                if (isSound)
                {
                    SoundManager.instance.PlaySE(SoundManager.SoundType.Dash);
                }
            }
            else
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
