using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class ACMiniatureManager : MonoBehaviour {

    public float DrawDistance = 2f;
    public Text AngleText;
    public GameObject MiniaturePrefab;

    private Vector3 gazeOrigin;
    private Vector3 gazeDirection;
    private Vector3 gazeTargetPosition;

    private GameObject Miniature;
    private GazeStabilizer gazeStabilizer;

    // Use this for initialization
    void Start () {
        gazeStabilizer = GetComponent<GazeStabilizer>();

        // Initialize the miniature figure
        if (MiniaturePrefab == null)
        {
            Miniature = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Miniature.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            Miniature = Instantiate(MiniaturePrefab) as GameObject;
        }
        Miniature.transform.parent = gameObject.transform;
        Miniature.name = "Miniature";
        Miniature.transform.position = new Vector3(0, 0, DrawDistance);
    }

    // Update is called once per 
    void Update()
    {
        gazeOrigin = Camera.main.transform.position;
        gazeDirection = Camera.main.transform.forward;
        gazeTargetPosition = gazeOrigin + gazeDirection * DrawDistance;

        gazeStabilizer.UpdateStability(gazeTargetPosition, Camera.main.transform.rotation);
        gazeDirection = gazeStabilizer.StableRotation.eulerAngles;
        gazeTargetPosition = gazeStabilizer.StablePosition;

        Miniature.transform.position = new Vector3(0f, gazeTargetPosition.y, gazeTargetPosition.z);
        Miniature.transform.rotation = Camera.main.transform.rotation;

        if (AngleText != null)
        {
            AngleText.transform.position = Camera.main.WorldToScreenPoint(Miniature.transform.position);
            
            AngleText.text = string.Format("{0}", GetCurrentAngle());
        }
    }

    private float GetCurrentAngle()
    {
        float angle = Mathf.RoundToInt(Miniature.transform.rotation.eulerAngles.x);

        if (angle > 0 && angle <= 180)
        {
            return -angle;
        } else if (angle > 180 && angle <= 360)
        {
            return 360 - angle;
        }

        return angle;
    }
}
