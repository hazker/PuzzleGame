using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearType
{
    Gear_n1,
    Gear_RightBigGear,
    Gear_s1,
    Gear_s2,
    Gear_s3
}

public class Gear : MonoBehaviour
{
    [Header("Gear Setup")]
    public bool gearCanBeMoved = true;
    public bool canBeSwappd = true;
    public GearType gearType;
    public float speedRotationMultiplier;

    [HideInInspector]
    public GearPos myPos;

    [HideInInspector]
    public RotationSide rotSide;

    bool victory = false;
    bool gearSelected = false;
    List<GearPos> gearsPos;

    GameObject gear;

    private void Awake()
    {
        gearsPos = MiniGame.Instance.gearsPlaces;
        gear = MiniGame.Instance.ghostGear;
    }

    void OnEnable()
    {
        victory = false;
        gearSelected = false;
    }

    private void OnMouseDown()
    {
        if (!victory && !MiniGame.Instance.fading)
        {
            gearSelected = true;
            AudioManager.Instance.PlaySound("grab");
        }
    }
    private void OnMouseUp()
    {
        if (gearSelected)
        {
            gear.GetComponent<SpriteRenderer>().sprite = null;
            AudioManager.Instance.PlaySound("set");
            gearSelected = false;

            float tempD = Mathf.Infinity;
            float curD;

            GearPos closestPos = myPos;

            foreach (var item in gearsPos)
            {
                curD = Vector3.Distance(transform.position, item.transform.position);
                if (tempD > curD && item.myGear.canBeSwappd)
                {
                    tempD = curD;
                    closestPos = item;
                }
            }
            if (closestPos != myPos && canBeSwappd && closestPos.myGear.canBeSwappd)
            {
                AudioManager.Instance.PlaySound("swap");

                MiniGame.Instance.SwapGears(this, closestPos.myGear);
            }

            else
                transform.position = myPos.transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (gearCanBeMoved && gearSelected)
        {
            gear.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            /*gear.transform.position=*/
            transform.position = new Vector3(MiniGame.Instance.cam.ScreenToWorldPoint(screenPosition).x, MiniGame.Instance.cam.ScreenToWorldPoint(screenPosition).y, transform.position.z);
        }

    }

    public IEnumerator VictoryRotation()
    {
        victory = true;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (rotSide == RotationSide.right)
                transform.Rotate(0, 0, -(speedRotationMultiplier) * Time.deltaTime*3);
            else
                transform.Rotate(0, 0, (speedRotationMultiplier) * Time.deltaTime*3);

        }

    }

}
