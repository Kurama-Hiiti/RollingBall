using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveObstacle : MonoBehaviour
{
    //移動する障害物、移動床の処理

    private Vector3 originePos;

    [Header("移動距離指定")]
    [SerializeField]
    private float edgePosX, reverseEdgePosX, edgePosY, reverseEdgePosY, edgePosZ, reverseEdgePosZ;

    //目的位置
    private Vector3 plusTargetPos, minusTargetPos;

    //速度
    [Header("速度")]
    [SerializeField]
    private float speed;

    //反転フラグ
    private bool isMovingToPlus;


    private Rigidbody rb;

    //移動するのか判定するフラグ
    [SerializeField]
    private bool isMove = true;

    //移動する床であるかの判定
    [SerializeField]
    private bool isBoad = false;

    //プレイヤーが板に触れてから動き出すまでの時間
    [SerializeField]
    private float boadMoveWaitTime = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }



    private void Start()
    {
        originePos = transform.position;

        //移動位置計算
        TargetPositionCalculation();

        isMovingToPlus = true;

    }
    void FixedUpdate()
    {
        if (isMove && GameManager.Instance.state == GameManager.GameState.Playing) 
        {
            //移動量
            float step = speed * Time.fixedDeltaTime;

            //目的位置の設定
            Vector3 target = isMovingToPlus ? plusTargetPos : minusTargetPos;

            //移動処理
            rb.MovePosition(Vector3.MoveTowards(transform.position, target, step));

            //目的位置へ到着したとき
            if (transform.position == target)
            {
                //移動床の場合は方向転換部分で一時停止する
                if(isBoad)
                {
                    isMove = false;
                    StartCoroutine(ReverseWaitTime(1.0f));
                }
                else//障害物の場合は即座に反転
                {
                    isMovingToPlus = !isMovingToPlus;
                }

            }
        }

    }

    //移動位置計算関数
    private void TargetPositionCalculation()
    {
        plusTargetPos = originePos + new Vector3(edgePosX, edgePosY, edgePosZ);
        minusTargetPos = originePos - new Vector3(reverseEdgePosX, reverseEdgePosY, reverseEdgePosZ);

    }


    //プレイヤーが乗ったら動き出す
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!isMove)
            {
                StartCoroutine(IsMoveTrue(boadMoveWaitTime));
            }
        }
    }


    //プレイヤーが乗ってから動き出すまでの時間猶予
    private IEnumerator IsMoveTrue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMove = true;
    }

    //方向転換するときに一時停止
    private IEnumerator ReverseWaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMovingToPlus = !isMovingToPlus;
        isMove = true;

    }
}
