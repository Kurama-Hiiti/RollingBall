using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    //•—‚Ì—Í
    [SerializeField]
    private float windForce;

    //•—‚Ì•ûŒü
    [SerializeField]
    private Vector3 windForceDir;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(windForceDir.normalized * windForce, ForceMode.Acceleration);
        }
    }


}
