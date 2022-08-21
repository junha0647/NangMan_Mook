using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BTNType
{
    Start,
    Exit,
    OtherCanvasOn,
    OtherCanvasOff,
    OptionOn,
    OptionOff,
    GoToTitle,
    Retry
}

public class BtnManager : MonoBehaviour
{
    [Header("��ư Ÿ�� ����")][SerializeField] private BTNType currentType;
    [Header("main Group ������Ʈ �ֱ�")][SerializeField] private CanvasGroup mainGroup;
    [Header("new Group ������Ʈ �ֱ�")][SerializeField] private CanvasGroup newGroup;

    [Header("LineDrawer ������Ʈ �ֱ�")][SerializeField] private LinesDrawer LD;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                SceneManager.LoadScene("Jun"); // ���� �� �����
                DataController.Instance.SaveReset();
                break;
            case BTNType.Exit:
                DataController.Instance.SaveReset();
                Application.Quit();
                break;
            case BTNType.OtherCanvasOn:
                CanvasGroupOn(newGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.OtherCanvasOff:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(newGroup);
                break;
            case BTNType.OptionOn:
                Time.timeScale = 0;
                UIManager.isPaused = true;
                CanvasGroupOn(newGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.OptionOff:
                Time.timeScale = 1;
                UIManager.isPaused = false;
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(newGroup);
                break;
            case BTNType.GoToTitle:
                SceneManager.LoadScene("01. TitleScene");
                DataController.Instance.SaveReset();
                break;
            case BTNType.Retry:
                SceneManager.LoadScene("Jun"); // static ������ Ȱ���Ͽ� üũ ����Ʈ �������� �ϱ�
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}