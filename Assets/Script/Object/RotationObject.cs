using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトがついているオブジェクトにRigidBodyを付ける
[RequireComponent(typeof(Rigidbody))]
public class RotationObject : MonoBehaviour
{
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

    //初期値
    //private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = true;

        //initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.state == GameManager.GameState.Playing && isRotation)
        {
            Quaternion deltaRotation = Quaternion.Euler(rotationAxis.normalized * rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isRotation)
            {
                isRotation = true;
            }
        }
    }

}
