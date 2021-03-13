using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGear : MonoBehaviour
{
    float curD;

    List<GearPos> gearsPos;
    GearPos closestPos;

    private void Awake()
    {
        gearsPos = MiniGame.Instance.gearsPlaces;
    }

    void Update()
    {
        float tempD = Mathf.Infinity;
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        foreach (var item in gearsPos)
        {
            curD = Vector3.Distance(new Vector3(MiniGame.Instance.cam.ScreenToWorldPoint(screenPosition).x, MiniGame.Instance.cam.ScreenToWorldPoint(screenPosition).y, transform.position.z), item.transform.position);
            if (tempD > curD && item.myGear.canBeSwappd)
            {
                tempD = curD;
                closestPos = item;
            }
        }
        transform.position = closestPos.transform.position;
    }
}
