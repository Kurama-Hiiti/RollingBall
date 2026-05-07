using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public void PlayerMove(Rigidbody rb, Camera cam, float moveForce)
    {
        float x = Input.GetAxis("Horizontal"); // A/D または ←/→
        float z = Input.GetAxis("Vertical");   // W/S または ↑/↓

        //カメラの向いている方向のX方向とZ方向の単位ベクトルを取得
        Vector3 camForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;

        Vector3 force = camForward * z + camRight * x;


        rb.AddForce(force * moveForce);
    }

}
