using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaskTransition : MonoBehaviour
{
    public bool isStarting;
    private RectTransform canvas;
    private float screenH = 0f;
    private float screenW = 0f;
    [SerializeField] private Image maskTransition;
    [SerializeField] private GameObject target;
    [SerializeField] private Player player;
    private float radius;

    private void Start()
    {
        GetCharacterPosition();
        StartCoroutine(EnterTransition());
    }

    void GetCharacterPosition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

        float characterScreenH = 0;
        float characterScreenW = 0;

        canvas = GetComponent<RectTransform>();
        screenH = Screen.height;
        screenW = Screen.width;

        if(screenW < screenH) //Portrait
        {
            maskTransition.rectTransform.sizeDelta = new Vector2(canvas.rect.height, canvas.rect.height);
            float newScreenPosX = screenPos.x + (screenH - screenW) / 2;

            characterScreenW = (newScreenPosX * 100) / screenH;
            characterScreenW /= 100;

            characterScreenH = (screenPos.y * 100) / screenH;
            characterScreenH /= 100;
        }
        else //Landscape
        {
            maskTransition.rectTransform.sizeDelta = new Vector2(canvas.rect.width, canvas.rect.width);
            float newScreenPosY = screenPos.y + (screenW - screenH) / 2;

            characterScreenW = (screenPos.x * 100) / screenW;
            characterScreenW /= 100;

            characterScreenH = (newScreenPosY * 100) / screenW;
            characterScreenH /= 100;
        }

        maskTransition.material.SetFloat("_CenterX", characterScreenW);
        maskTransition.material.SetFloat("_CenterY", characterScreenH);
    }

    public IEnumerator EnterTransition()
    {
        player.enabled = false;

        maskTransition.material.SetFloat("_Radius", 0);

        yield return new WaitForSeconds(0.5f);


        while(radius < 1)
        {
            radius += Time.deltaTime * 0.8f;
            maskTransition.material.SetFloat("_Radius", radius);
            yield return null;
        }

        maskTransition.material.SetFloat("_Radius", 1);

        yield return new WaitForSeconds(0.5f);
        maskTransition.gameObject.SetActive(false);
        player.enabled = true;
    }

    public IEnumerator ExitTransition()
    {
        maskTransition.gameObject.SetActive(true);
        maskTransition.material.SetFloat("_Radius", 1);

        yield return new WaitForSeconds(0.5f);

        while (radius > 0)
        {
            radius -= Time.deltaTime * 0.8f;
            maskTransition.material.SetFloat("_Radius", radius);
            yield return null;
        }

        maskTransition.material.SetFloat("_Radius", 0);

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    public void ResetGame()
    {
        StartCoroutine(ExitTransition());
    }
}
