using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundManager;

public class UIManager : MonoBehaviour
{
    //ゲームクリアパネル
    [SerializeField]
    private GameObject gameClearPanel;

    //ゲームオーバーパネル
    [SerializeField]
    private GameObject gameOverPanel;

    //メニュー画面パネル
    [SerializeField]
    private GameObject menuPanel;

    //メニュー画面の現在のステージ数テキスト
    [SerializeField]
    private TextMeshProUGUI nowStageText;


    //タイトル画面用変数

    //タイトルパネル
    [SerializeField]
    private GameObject titlePanel;

    //ステージ選択パネル
    [SerializeField]
    private GameObject stageSelectPanel;





    private void Start()
    {
        //テキストが格納されている時だけテキスト更新
        if (nowStageText != null)
        {
            nowStageText.text = "Stage:" + SceneManager.GetActiveScene().buildIndex;
        }

    }


    //ゲームクリアパネル表示
    public void ShowGameClearPanel()
    {
        SoundManager.instance.PlaySE(SoundType.GameClear);
        gameClearPanel.SetActive(true);
    }

    //ゲームオーバーパネル表示
    public void ShowGameOverPanel() 
    {
        SoundManager.instance.PlaySE(SoundType.GameOver);
        gameOverPanel.SetActive(true);
    }

    //メニュー画面表示
    public void ToggleMenuPanel(bool isVisible)
    {
        SoundManager.instance.PlaySE(SoundType.Button);

        menuPanel.SetActive(isVisible);

        if (isVisible)
        {
            GameManager.instance.state = GameManager.GameState.GamePaused;
        }
        else
        {
            GameManager.instance.state = GameManager.GameState.Playing;
        }
    }



    //タイトル画面用関数

    //ステージ選択画面表示関数
    public void ShowStageSelectPanel()
    {
        SoundManager.instance.PlaySE(SoundType.Button);
        titlePanel.SetActive(false);
        stageSelectPanel.SetActive(true);
    }


    //ステージ選択画面からタイトル画面に戻る関数
    public void BackTitlePanel()
    {
        SoundManager.instance.PlaySE(SoundType.Button);
        titlePanel.SetActive(true);
        stageSelectPanel.SetActive(false);
    }

}
