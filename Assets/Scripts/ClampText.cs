using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampText : MonoBehaviour {

    public Text Label;
    private Vector3 textPosition;

	// Use this for initialization
	void Start () {
        textPosition = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        Label.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
	}
}
