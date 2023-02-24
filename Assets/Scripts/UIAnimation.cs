using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject highScore;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject blurPanel;
    [SerializeField] private GameObject blockMousePanel;

    private void Start()
    {
        instructions.transform.localScale = Vector3.zero;
        blurPanel.transform.localScale = Vector3.zero;
    }

    public void UIStartGame()
    {
        LeanTween.move(help.GetComponent<RectTransform>(), new Vector3(-80, 300, 0), 0.8f).setEase(LeanTweenType.easeInElastic);
        LeanTween.move(highScore.GetComponent<RectTransform>(), new Vector3(0, 150, 0), 0.8f).setEase(LeanTweenType.easeInElastic);
        LeanTween.move(logo.GetComponent<RectTransform>(), new Vector3(-2000, 0, 0), 0.5f).setDelay(0.9f).setEase(LeanTweenType.easeInBack);
    }

    public void UITitleEnter()
    {
        LeanTween.move(help.GetComponent<RectTransform>(), new Vector3(-80, -60, 0), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.move(highScore.GetComponent<RectTransform>(), new Vector3(0, -100, 0), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.move(logo.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.8f).setDelay(1.1f).setEase(LeanTweenType.easeOutBack).setOnComplete(EnableMouse);
    }

    public void UITitleExit()
    {
        blockMousePanel.SetActive(true);
        LeanTween.move(help.GetComponent<RectTransform>(), new Vector3(-80, 300, 0), 1f).setEase(LeanTweenType.easeInElastic);
        LeanTween.move(highScore.GetComponent<RectTransform>(), new Vector3(0, 150, 0), 1f).setEase(LeanTweenType.easeInElastic);
        LeanTween.move(logo.GetComponent<RectTransform>(), new Vector3(-2000, 0, 0), 0.8f).setDelay(1.1f).setEase(LeanTweenType.easeInBack).setOnComplete(UIHelpEnter);
    }

    public void UIHelpEnter()
    {
        LeanTween.scale(blurPanel.GetComponent<RectTransform>(), Vector3.one * 3.39f, 2f);
        LeanTween.move(back.GetComponent<RectTransform>(), new Vector3(-80, -60, 0), 1.3f).setDelay(0.8f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(instructions.GetComponent<RectTransform>(), Vector3.one * 3.39f, 2f).setDelay(1.9f).setEase(LeanTweenType.easeOutElastic);
    }

    public void UIHelpExit()
    {
        LeanTween.move(back.GetComponent<RectTransform>(), new Vector3(-50, 300, 0), 1.3f).setEase(LeanTweenType.easeInElastic);
        LeanTween.scale(instructions.GetComponent<RectTransform>(), Vector3.zero, 1.5f).setDelay(1f).setEase(LeanTweenType.easeInElastic);
        LeanTween.scale(blurPanel.GetComponent<RectTransform>(), Vector3.zero, 2f).setDelay(1.2f).setOnComplete(UITitleEnter);
    }

    void EnableMouse()
    {
        blockMousePanel.SetActive(false);
    }
}
