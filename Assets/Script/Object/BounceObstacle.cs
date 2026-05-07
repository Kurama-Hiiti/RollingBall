using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : MonoBehaviour
{
    [Header("反発力")]
    [SerializeField]
    private float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null && collision.gameObject.CompareTag("Player"))
        {

            SoundManager.instance.PlaySE(SoundManager.SoundType.Bound);
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.velocity = Vector3.zero; // 一度速度をリセット
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);


        }

    }
}
