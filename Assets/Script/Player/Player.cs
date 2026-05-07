using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    //プレイヤーの操作クラス
    PlayerController controller = new PlayerController();

    private Rigidbody rb;

    private Camera cam;

    //移動のために加える力
    [SerializeField]
    private float moveForce;

    //接地判定
    [SerializeField]
    private bool isGround;

    //仮想重力値
    [SerializeField]
    private float extraGravityForce;

    //ワープ可能判定
    public bool isWarp { get; private set; }


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;

        cam = Camera.main;

        isWarp = true;

    }

    private void FixedUpdate()
    {
        //仮想重力
        rb.AddForce(Vector3.down * extraGravityForce, ForceMode.Acceleration);

        //地面に接地している場合且つゴールしていない時移動可能
        if (isGround)
        {
            //プレイヤーの移動関数
            controller.PlayerMove(rb, cam, moveForce);

        }
        else
        {
            //プレイヤーの移動関数 空中では少しだけ動ける
            controller.PlayerMove(rb, cam, moveForce / 10);
        }


    }


    void Update()
    {


    }


    //接地判定
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }

    //風の中でも移動しやすくする
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }


    //ワープ関数
    public void Warp(Transform warpPos)
    {
        transform.position = warpPos.position;
        isWarp = false;

        SoundManager.instance.PlaySE(SoundManager.SoundType.Warp);

    }

    //ワープフラグ変更関数
    public void WarpFlagChange()
    {
        isWarp = true;
    }

}
