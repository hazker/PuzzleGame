using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundTouch : MonoBehaviour
{
    public GameObject miniGame;

    bool animPlayed = false;
    float animTime = 1.5f;

    private void OnMouseDown()
    {
        MiniGameShow(!miniGame.activeInHierarchy);
        //miniGame.SetActive(!miniGame.activeInHierarchy);       
    }

    void MiniGameShow(bool cond)
    {
        if (cond && !animPlayed)
        {
            AudioManager.Instance.PlaySound("open");
            StartCoroutine(WaitForEndAnim(animTime, cond));

        }
        else if (!animPlayed)
        {
            miniGame.GetComponent<MiniGame>().myAnim.SetBool("TimeToPopUpOut", true);

            AudioManager.Instance.PlaySound("close");
            StartCoroutine(WaitForEndAnim(animTime, cond));
        }
    }

    IEnumerator WaitForEndAnim(float time, bool cond)
    {
        animPlayed = true;
        if (cond)
        {
            miniGame.SetActive(cond);
            miniGame.GetComponent<MiniGame>().StartTheMiniGame();
        }
        yield return new WaitForSecondsRealtime(time);
        if (!cond)
        {
            miniGame.SetActive(cond);
        }
        animPlayed = false;
    }
}
