using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class HUDManager : MonoBehaviour {

    private float Speed;
    private float Altitude;

    public Text SpeedLabel;
    public Text AltitudeLabel;

    public void Update()
    {
        if (SpeedLabel != null)
        {
            SpeedLabel.text = string.Format("{0}", Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(Camera.main.velocity.x, 2)
                + Mathf.Pow(Camera.main.velocity.y, 2) + 
                Mathf.Pow(Camera.main.velocity.z, 2))));
        }

        if (AltitudeLabel != null)
        {
            AltitudeLabel.text = string.Format("{0}", Mathf.RoundToInt(Camera.main.transform.position.y));
        }
    }
}
