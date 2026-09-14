using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトがついているオブジェクトにRigidBodyを付ける
[RequireComponent(typeof(Rigidbody))]
public class RotationObject : MonoBehaviour
{
    //回転する物体の処理

    [Header("回転軸（X, Y, Z で調整）")]
    [SerializeField]
    private Vector3 rotationAxis;

    [Header("回転スピード（度/秒）")]
    [SerializeField]
    private float rotationSpeed;

    private Rigidbody rb;

    //回転するかの判定
    [SerializeField]
    private bool isRotation = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = true;

    }

    void FixedUpdate()
    {
        if (GameManager.Instance.state == GameManager.GameState.Playing && isRotation)
        {
            //回転量
            Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime * rotationAxis.normalized);
            //回転処理
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤーが触れた際に回転開始する
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isRotation)
            {
                isRotation = true;
            }
        }
    }

}
