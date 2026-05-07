using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SerializeField]
    private Transform warpPos;

    private bool copyIsWarp;

    private void OnTriggerEnter(Collider other)
    { 

        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            copyIsWarp = player.isWarp;

            if (player != null && player.isWarp && warpPos != null) 
            {
                player.Warp(warpPos);
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !copyIsWarp)
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.WarpFlagChange();
        }
    }
}
