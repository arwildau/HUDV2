using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextualIndicator : MonoBehaviour {

    public float DrawDistance = 2f;
    public GameObject HorizonLinePrefab;

    private GameObject HorizonLine;

	// Use this for initialization
	void Start () {
	    if (HorizonLinePrefab != null)
        {
            HorizonLine = Instantiate(HorizonLinePrefab) as GameObject;
            HorizonLine.transform.parent = gameObject.transform;
            HorizonLine.name = "HorizonLine";
            HorizonLine.transform.position = new Vector3(0f, 0f, DrawDistance);
        }
	}
}
