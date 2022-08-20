using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Player 오브젝트 넣기")][SerializeField] private GameObject Player;

    [Header("GameOver 오브젝트 넣기")][SerializeField] private Text GameOverText;
    [Header("FadeIn 오브젝트 넣기")][SerializeField] private Image FadeInImg;

    [Header("main Group 오브젝트 넣기")][SerializeField] private CanvasGroup mainGroup;
    [Header("new Group 오브젝트 넣기")][SerializeField] private CanvasGroup newGroup;

    [Header("LineDrawer 오브젝트 넣기")][SerializeField] private LinesDrawer LD;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        DrawMode();
    }

    public static bool isDraw = false;
    public static bool isPaused = false;
    private void DrawMode()
    {
        if(!isDraw && !isPaused && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.G)))
        {
            //Time.timeScale = 0;
            isDraw = true;
            //LD.isPaused = true;
            Player.layer = 7;
            CanvasGroupOn(newGroup);
            CanvasGroupOff(mainGroup);
        }
        else if(isDraw && !isPaused && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.G)))
        {
            //Time.timeScale = 1;
            isDraw = false;
            Player.layer = 8;
            CanvasGroupOn(mainGroup);
            CanvasGroupOff(newGroup);
            
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverScene());
    }

    private float time = 0f;
    private float F_time = 1f;
    private IEnumerator GameOverScene()
    {
        GameOverText.gameObject.SetActive(true);
        Color alpha = GameOverText.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            GameOverText.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        time = 0f;
        F_time = 1f;
        FadeInImg.gameObject.SetActive(true);
        alpha = FadeInImg.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            FadeInImg.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("03. GameOverScene");
    }

    private void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}