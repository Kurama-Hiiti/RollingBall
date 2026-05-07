using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class CameraMnager : MonoBehaviour
{
    //追従すべきオブジェクトの位置
    [SerializeField]
    private Transform target;

    //カメラとターゲットとの距離
    [SerializeField]
    private float distance = 12.0f;

    //カメラのx方向への移動速度
    [SerializeField]
    private float xSpeed = 250.0f;

    //カメラのy方向への移動速度
    [SerializeField] 
    private float ySpeed = 120.0f;

    //カメラのy方向の限界
    [SerializeField]
    private float yMinLimit = -45f;

    //カメラのy方向の限界
    [SerializeField]
    private float yMaxLimit = 85f;


    //カメラの角度移動のための変数
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        //基準となる軸を設定
        Vector3 angls = transform.eulerAngles;
        x = angls.y;
        y = angls.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && GameManager.instance.state == GameState.Playing)
        {
            //マウスカーソルの位置を判定して角度を替える
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            //ｙ軸方向に一回転しないように制限を儲ける
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            //カメラの位置と回転を定義する
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            //変更した値を反映する
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
