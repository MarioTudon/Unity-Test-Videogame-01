using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIAnimation uIAnimation;
    [SerializeField] private AudioSource clickAudio;

    public void DeactivatePlayerScript()
    {
        player.enabled = false;
    }
    
    public void ActivatePlayerScript()
    {
        player.enabled = true;
    }

    public void HelpButton()
    {
        uIAnimation.UITitleExit();
        clickAudio.Play();
    }

    public void BackButton()
    {
        uIAnimation.UIHelpExit();
        clickAudio.Play();
    }
}
