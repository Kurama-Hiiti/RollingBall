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
    public static StageManager instance { get; private set; }

    //ステージクリア数
    [SerializeField]
    private int clearStageNum;

    [SerializeField]
    private Button[] stageSelectButtons;


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
            for (int i = 0; i < stageSelectButtons.Length; i++)
            {
                stageSelectButtons[i].interactable = false;
            }

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

        //クリックされたボタンを格納
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        Button clickButton = clickObject.GetComponent<Button>();

        int index = Array.IndexOf(stageSelectButtons, clickButton);

        SceneController.instance.StageSelect(index + 1);
        
    }


    //ステージをクリアした際にステージクリア数を更新する関数
    public void StageClearNumUpdate()
    {

        int nowStage = SceneManager.GetActiveScene().buildIndex;

        if (clearStageNum < nowStage)
        {
            clearStageNum++;

            PlayerPrefs.SetInt("ClearStage", clearStageNum);
        }

    }



}
