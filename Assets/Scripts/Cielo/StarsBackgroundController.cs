using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StarsBackgroundController : MonoBehaviour
{
    [FormerlySerializedAs("sky")] public GameObject skyCenter;
    public float distance;
    private GameObject skyLeft;
    private GameObject skyRight;
    
    void Start()
    {
        GenerateBase();
    }

    private void GenerateBase()
    {
        skyLeft =
            Instantiate(skyCenter, new Vector3(skyCenter.transform.position.x - distance, 0, 0), Quaternion.identity);
        skyRight =
            Instantiate(skyCenter, new Vector3(skyCenter.transform.position.x + distance, 0, 0), Quaternion.identity);
    }

    public void ChangePositionSky(DirectionMoveSkyStar directionMoveSkyStar)
    {
        
        print("ChangePositionSky");
        // switch (directionMoveSkyStar)
        // {
        //     case DirectionMoveSkyStar.Left:
        //         // skyRight.transform.position = new Vector3(skyLeft.transform.position.x - distance,
        //         //     skyRight.transform.position.y, skyLeft.transform.position.z);
        //         break;
        //     case DirectionMoveSkyStar.Right:
        //         skyCenter.transform.position = new Vector3(skyCenter.transform.position.x + distance, 0, 0);
        //         break;
        // }
    }
}