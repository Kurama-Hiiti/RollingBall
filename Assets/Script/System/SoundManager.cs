using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //static化
    public static SoundManager instance;

    //SEの名前設定(再生処理の際にわかりやすくする)
    public enum SoundType
    {
        GameClear, GameOver, Button, Bound, Dash, Warp,
    }

    //SEの配列
    [SerializeField]
    private AudioClip[] se;

    //SE用
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


    //音を鳴らす関数
    public void PlaySE(SoundType type)
    {
        //サウンドタイプを番号(int型)へ変更
        int index = (int)type;

        if (index >= 0 && index < se.Length)
        {
            audioSource.PlayOneShot(se[index]);
        }

    }

}
