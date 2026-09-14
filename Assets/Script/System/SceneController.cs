using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundManager;

public class SceneController : MonoBehaviour
{
    //シーンの遷移処理

    //シングルトン化
    public static SceneController Instance { get; private set; }

    [Header("フェード設定")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float durationWaitTime = 0.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    //タイトルシーン遷移
    public void LoadTitle()
    {
        SoundManager.instance.PlaySE(SoundType.Button);
        fadeImage.enabled = true;
        StartCoroutine(FadeAndLoadScene("TitleScene"));
    }

    //次ステージへ遷移
    public void LoadNextStage()
    {
        SoundManager.instance.PlaySE(SoundType.Button);
        fadeImage.enabled = true;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(FadeAndLoadScene(nextSceneIndex));
    }

    //リトライ
    public void RetryCurrentStage()
    {
        SoundManager.instance.PlaySE(SoundType.Button);
        fadeImage.enabled = true;
        StartCoroutine(FadeAndLoadScene(SceneManager.GetActiveScene().name));
    }

    //ステージセレクト画面からシーン遷移
    public void StageSelect(int sceneIndex)
    {
        fadeImage.enabled = true;

        StartCoroutine(FadeAndLoadScene(sceneIndex));
    }


    // フェードアウトしてからシーンを読み込む(シーン名でシーン遷移)
    private IEnumerator FadeAndLoadScene(string sceneName)
    {

        yield return StartCoroutine(Fade(1)); // フェードアウト
        yield return new WaitForSeconds(durationWaitTime); //フェードアウトしてから一定時間の後シーン遷移
        SceneManager.LoadScene(sceneName);
    }


    // フェードアウトしてからシーンを読み込む(シーン番号でシーン遷移)
    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        yield return StartCoroutine(Fade(1)); // フェードアウト
        yield return new WaitForSeconds(durationWaitTime); //フェードアウトしてから一定時間の後シーン遷移
        SceneManager.LoadScene(sceneIndex);
    }

    //画面のフェードアウト処理
    private IEnumerator Fade(float targetAlpha)
    {
        //フェードアウト用画像の初期アルファ値
        float startAlpha = fadeImage.color.a;

        //経過時間
        float time = 0f;

        //時間経過で徐々に目的のアルファ値へ遷移する
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        //最終的なアルファ値
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }


}
