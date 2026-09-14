using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundManager;

public class StageManager : MonoBehaviour
{
    //インスタンス化
    public static StageManager Instance { get; private set; }

    //ステージクリア数
    [SerializeField]
    private int clearStageNum;

    [SerializeField]
    private Button[] stageSelectButtons;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //現在クリアしている数
        clearStageNum = PlayerPrefs.GetInt("ClearStage", 0);

        //挑戦可能なステージのボタンを有効化
        if (stageSelectButtons == null || stageSelectButtons.Length == 0)
        {
            return;
        }
        else
        {
            //一度全てのステージ選択ボタンを無効にする
            for (int i = 0; i < stageSelectButtons.Length; i++)
            {
                stageSelectButtons[i].interactable = false;
            }

            //クリア状況を考慮してステージ選択ボタンを有効にする
            for (int i = 0; i < clearStageNum + 1; i++)
            {
                if (i < stageSelectButtons.Length)
                {
                    stageSelectButtons[i].interactable = true;
                }
                
            }
        }

    }


    //ステージへの遷移関数
    public void TransitionStage()
    {
        SoundManager.instance.PlaySE(SoundType.Button);

        //クリックされたオブジェクトを格納
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        //クリックされたオブジェクトからボタンを格納
        Button clickButton = clickObject.GetComponent<Button>();

        //クリックされたボタンがステージ選択ボタンのリストの中の何番目の物か検索して格納
        int index = Array.IndexOf(stageSelectButtons, clickButton);

        //シーン遷移
        SceneController.Instance.StageSelect(index + 1);
        
    }


    //ステージをクリアした際にステージクリア数を更新する関数
    public void StageClearNumUpdate()
    {
        //クリアしたステージ数格納
        int nowStage = SceneManager.GetActiveScene().buildIndex;

        //クリアしたステージ数が攻略済みのステージ数を超えている場合は攻略ステージ数を更新
        if (clearStageNum < nowStage)
        {
            clearStageNum++;

            PlayerPrefs.SetInt("ClearStage", clearStageNum);
        }

    }



}
