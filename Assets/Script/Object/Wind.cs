using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    //プレイヤーに風の力（外力）を加える処理

    //風の力
    [SerializeField]
    private float windForce;

    //風の方向
    [SerializeField]
    private Vector3 windForceDir;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        //プレイヤーがコライダーの空間内にいる時に力を加え続ける
        if (other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(windForceDir.normalized * windForce, ForceMode.Acceleration);
        }
    }


}
