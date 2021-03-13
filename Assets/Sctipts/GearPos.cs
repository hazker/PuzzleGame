using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationSide
{
    left,
    right
}

public class GearPos : MonoBehaviour
{
    [Header("Gear Position Setup")]
    public GearType defaultGear;
    
    public GearType winGear;

    public RotationSide side;

    [Header("Debug/Hint")]
    public GearType currentGear;

    [HideInInspector]
    public Gear myGear;
    Gear myDefaultGear;


    public void DefaultPos()
    {
        if (myGear == null)
        {
            myGear = Instantiate(MiniGame.Instance.gears[(int)defaultGear], MiniGame.Instance.transform).GetComponent<Gear>();
            myDefaultGear = myGear;

            MiniGame.Instance.gearsInGame.Add(myGear);
        }
        else
        {
            myGear = myDefaultGear;
        }

        myDefaultGear.transform.position = transform.position;
        myDefaultGear.myPos = this;
        myGear.rotSide = side;
        currentGear = defaultGear;
    }

}
