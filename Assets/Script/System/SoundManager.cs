using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //staticâª
    public static SoundManager instance;

    //SEÇÃñºëOê›íË
    public enum SoundType
    {
        GameClear, GameOver, Button, Bound, Dash, Warp,
    }

    //SEÇÃîzóÒ
    [SerializeField]
    private AudioClip[] se;

    //SEóp
    private AudioSource audioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }


    //âπÇñ¬ÇÁÇ∑ä÷êî
    public void PlaySE(SoundType type)
    {
        int index = (int)type;

        if (index >= 0 && index < se.Length)
        {
            audioSource.PlayOneShot(se[index]);
        }

    }

}
