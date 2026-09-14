using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //シングルトン化
    public static GameManager Instance { get; private set; }

    //ゲームの状態を格納
    public enum GameState
    {
        Playing,
        GameClear,
        GameOver,
        GamePaused,
        Title,
    }

    //状態設定関数
    public GameState state;

    //UI管理スクリプト
    [SerializeField]
    private UIManager uiManager;

    //メニュー画面管理フラグ
    private bool isMenu;

    //メニュー画面表示時速度と方向を保存する変数
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //ゲーム開始時状態設定
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            state = GameState.Title;
        }
        else
        {
            state = GameState.Playing;
            isMenu = false;
        }

        MouseCursorToggle();

    }

    private void Update()
    {

        //タイトル画面、ゲームクリア時もしくはゲームオーバー時はリターン
        if (state == GameState.GameClear || state == GameState.GameOver || state == GameState.Title)
        {
            return;
        }

        //メニュー画面表示
        if (Input.GetKeyDown(KeyCode.Tab) && !isMenu)
        {
            //メニュー画面表示フラグ
            isMenu = true;

            //メニュー画面表示非表示実行関数
            uiManager.ToggleMenuPanel(isMenu);

            //プレイヤーのRifidBody変更
            RigidbodyModeToggle();

            //マウスカーソル表示非表示関数
            MouseCursorToggle();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isMenu)
        {
            //メニュー画面表示フラグ
            isMenu = false;

            //メニュー画面表示非表示実行関数
            uiManager.ToggleMenuPanel(isMenu);

            //プレイヤーのRifidBody変更
            RigidbodyModeToggle();

            //マウスカーソル表示非表示関数
            MouseCursorToggle();
        }

    }

    //マウスカーソル表示非表示判定関数
    private void MouseCursorToggle()
    {
        if (state == GameState.Playing)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }


    //ゴール判定関数
    public void OnGoalReached()
    {
        if (state == GameState.GameClear) return; // 多重防止
        state = GameState.GameClear;

        //プレイヤーのRigidbodyのモード変更(移動しないようにするため)
        StartCoroutine(RigidbodyModeToggle(0.3f));

        //ゲームクリアパネル表示
        uiManager.ShowGameClearPanel();

        //マウスカーソル表示
        MouseCursorToggle();

        //ステージクリア数更新
        StageManager.Instance.StageClearNumUpdate();

    }

    //ゲームオーバー判定
    public void GameOver()
    {
        if(state == GameState.GameOver) return; // 多重防止
        state = GameState.GameOver;

        //プレイヤーのRigidbodyのモード変更(移動しないようにするため)
        StartCoroutine(RigidbodyModeToggle(0.3f));

        //ゲームオーバーパネル表示
        uiManager.ShowGameOverPanel();

        //マウスカーソル表示
        MouseCursorToggle();

    }



    //速度を保存してプレイヤーのRigidbodyのモード変更関数
    private void RigidbodyModeToggle()
    {
        GameObject player = GameObject.FindWithTag("Player");

        Rigidbody rb = player.GetComponent<Rigidbody>();


        //ポーズされた瞬間
        if (!rb.isKinematic)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
        }
        else if (rb.isKinematic) //ポーズ解除
        {
            rb.isKinematic = false;
            rb.velocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }

    }

    //速度を保存してプレイヤーのRigidbodyのモード変更関数(コルーチン)
    private IEnumerator RigidbodyModeToggle(float time)
    {
        //指定時間待つ
        yield return new WaitForSeconds(time);

        GameObject player = GameObject.FindWithTag("Player");

        Rigidbody rb = player.GetComponent<Rigidbody>();


        //ポーズされた瞬間
        if (!rb.isKinematic)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
        }
        else if (rb.isKinematic) //ポーズ解除
        {
            rb.isKinematic = false;
            rb.velocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
    }
}

