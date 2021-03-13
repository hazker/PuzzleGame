using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{

    private static MiniGame instance;
    public static MiniGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MiniGame>();

            }
            return instance;
        }
    }

    public GameObject ghostGear;

    public Camera cam;

    public List<GearPos> gearsPlaces;
    public List<GameObject> gears;

    [HideInInspector]
    public Animator myAnim;
    [HideInInspector]
    public List<Gear> gearsInGame = new List<Gear>();
    [HideInInspector]
    public bool fading=false; 

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void StartTheMiniGame()
    {
        foreach (var item in gearsPlaces)
        {
            item.DefaultPos();
        }
    }

    public void SwapGears(Gear gear1, Gear gear2)
    {
        GearPos temp = gear1.myPos;
        gear1.myPos = gear2.myPos;
        gear2.myPos = temp;
        gear1.myPos.myGear = gear1;
        gear2.myPos.myGear = gear2;


        StartCoroutine(fadeOut(gear1.GetComponentInParent<SpriteRenderer>(), gear2.GetComponentInParent<SpriteRenderer>()));
        fading = true;

    }

    void ActuallySwap(Gear gear1, Gear gear2)
    {
        gear1.transform.position = gear1.myPos.transform.position;
        gear2.transform.position = gear2.myPos.transform.position;

        gear1.myPos.currentGear = gear1.gearType;
        gear2.myPos.currentGear = gear2.gearType;

        gear1.rotSide = gear1.myPos.side;
        gear2.rotSide = gear2.myPos.side;


        if (CheckVictoryConditions())
        {
            AudioManager.Instance.PlaySound("victory");
            StartCoroutine(Win());
        }
        fading = false;

    }

    IEnumerator fadeOut(SpriteRenderer sprite1, SpriteRenderer sprite2)
    {
        Color c = Color.white;
        for (float i = 1; i > -0.1; i-=0.05f)
        {
            c.a = i;
            sprite1.color = sprite2.color = c;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(fadeIn(sprite1, sprite2));
        ActuallySwap(sprite1.gameObject.GetComponent<Gear>(), sprite2.gameObject.GetComponent<Gear>());
    }

    IEnumerator fadeIn(SpriteRenderer sprite1, SpriteRenderer sprite2)
    {
        Color c = Color.white;
        for (float i = 0; i < 1.1; i += 0.05f)
        {
            c.a = i;
            sprite1.color = sprite2.color = c;
            yield return new WaitForFixedUpdate();
        }
        
    }

    bool CheckVictoryConditions()
    {
        foreach (var item in gearsPlaces)
        {
            if (item.myGear.gearType != item.winGear)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator Win()
    {
        foreach (var item in gearsInGame)
        {

            StartCoroutine(item.VictoryRotation());
        }
        yield return new WaitForSeconds(5f);
        AudioManager.Instance.PlaySound("close");
        myAnim.SetBool("TimeToPopUpOut", true);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }


}
