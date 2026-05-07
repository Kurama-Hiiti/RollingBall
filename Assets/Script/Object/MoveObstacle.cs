using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveObstacle : MonoBehaviour
{
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
        rb.isKinematic = true; // 自動制御用
    }



    private void Start()
    {
        originePos = transform.position;

        TargetPositionCalculation();

        isMovingToPlus = true;

    }
    void FixedUpdate()
    {
        if (isMove && GameManager.instance.state == GameManager.GameState.Playing) 
        {
            float step = speed * Time.fixedDeltaTime;
            Vector3 target = isMovingToPlus ? plusTargetPos : minusTargetPos;
            rb.MovePosition(Vector3.MoveTowards(transform.position, target, step));

            if (transform.position == target)
            {
                //移動床の場合は方向転換部分で一時停止する
                if(isBoad)
                {
                    isMove = false;
                    StartCoroutine(ReverseWaitTime(1.0f));
                }
                else
                {
                    isMovingToPlus = !isMovingToPlus;
                }

            }
        }

    }


    //void Update()
    //{
    //    float step = speed * Time.deltaTime;
    //    if (isMovingToPlus)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, plusTargetPos, step);

    //        if (transform.position == plusTargetPos)
    //        {
    //            isMovingToPlus = false;
    //        }
    //    }
    //    else
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, minusTargetPos, step);

    //        if (transform.position == minusTargetPos)
    //        {
    //            isMovingToPlus = true;
    //        }
    //    }
        
    //}



    //移動関数
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
