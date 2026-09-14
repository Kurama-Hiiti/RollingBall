using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleManager : MonoBehaviour
{
    //風のエフェクト表示処理


    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //ゲームプレイ中のみエフェクト再生
        if (GameManager.Instance.state != GameManager.GameState.Playing)
        {
            particle.Pause();           
        }
        else if(particle.isPaused)
        {
            particle.Play();
        }
    }
}
