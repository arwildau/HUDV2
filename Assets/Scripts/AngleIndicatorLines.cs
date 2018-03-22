using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleIndicatorLines : MonoBehaviour {

    // Public classes
    [System.Serializable]
    public class StepSize
    {
        public float Big = 10f;
        public float Medium = 5f;
        public float Small = 2.5f;
    }

    [System.Serializable]
    public class DegreeIndicatorSize
    {
        public float Big = 0.5f;
        public float Medium = 0.3f;
        public float Small = 0.2f;
    }

    [System.Serializable]
    public class LineMaterials
    {
        public Material PositivePitchLineMaterial;
        public Material NegativePitchLineMaterial;
        public Material HorizonLineMaterial;
    }

    // Public variables
    [Tooltip("DrawDistance to draw the HUD position indicator")]
    public float DrawDistance = 2f;

    public GameObject LinePrefab;


    public StepSize stepSize = new StepSize();
    public DegreeIndicatorSize degreeIndicatorSize = new DegreeIndicatorSize();
    public LineMaterials lineMaterials = new LineMaterials();

    [SerializeField]
    List<GameObject> AttitudeLines = new List<GameObject>();

    // Private variables
    private GameObject AttitudeLinesContainer;

    public void Start()
    {
        // Create containers for the angle indication lines
        AttitudeLinesContainer = new GameObject();
        AttitudeLinesContainer.transform.parent = gameObject.transform;
        AttitudeLinesContainer.name = "AttitudeLines";

        GameObject upLinesContainer = new GameObject();
        upLinesContainer.name = "UpLines";

        GameObject downLinesContainer = new GameObject();
        downLinesContainer.name = "DownLines";

        upLinesContainer.transform.parent = AttitudeLinesContainer.transform;
        downLinesContainer.transform.parent = AttitudeLinesContainer.transform;

        // Initialize the angle indicator lines (attitude lines)
        InitializeAttitudeLines(upLinesContainer, downLinesContainer);
    }

    private void InitializeAttitudeLines(GameObject upContainer, GameObject downContainer)
    {

        float lineSize = 0.1f;
        for (float i = 0; i < 45; i += 0.5f)
        {
            // Only initialize multiples of the step sizes. Skip 0 degree line.
            if ((i % stepSize.Small == 0 || i % stepSize.Medium == 0 || i % stepSize.Big == 0) && i != 0)
            {
                // Set the correct length of the indicator line.
                if (i % stepSize.Big == 0)
                {
                    lineSize = degreeIndicatorSize.Big;
                }
                else if (i % stepSize.Medium == 0)
                {
                    lineSize = degreeIndicatorSize.Medium;
                }
                else if (i % stepSize.Small == 0)
                {
                    lineSize = degreeIndicatorSize.Small;
                }

                GameObject degreeIndicatorUp = Instantiate(LinePrefab) as GameObject;
                degreeIndicatorUp.name = string.Format("AttitudeIndicator {0}", i);
                degreeIndicatorUp.transform.parent = upContainer.transform;

                if (lineMaterials.PositivePitchLineMaterial != null)
                {
                    Renderer degreeIndicatorUpRenderer = degreeIndicatorUp.GetComponent<Renderer>();
                    degreeIndicatorUpRenderer.material = lineMaterials.PositivePitchLineMaterial;
                }

                GameObject degreeIndicatorDown = Instantiate(LinePrefab) as GameObject;
                degreeIndicatorDown.name = string.Format("AttitudeIndicator {0}", -i);
                degreeIndicatorDown.transform.parent = downContainer.transform;

                if (lineMaterials.NegativePitchLineMaterial != null)
                {
                    Renderer degreeIndicatorDownRenderer = degreeIndicatorDown.GetComponent<Renderer>();
                    degreeIndicatorDownRenderer.material = lineMaterials.NegativePitchLineMaterial;
                }

                float h = DrawDistance * Mathf.Tan(i * Mathf.Deg2Rad);

                degreeIndicatorUp.transform.position = new Vector3(0, h, DrawDistance);
                degreeIndicatorUp.transform.localScale = new Vector3(degreeIndicatorUp.transform.localScale.x,
                    degreeIndicatorUp.transform.localScale.y * lineSize,
                    degreeIndicatorUp.transform.localScale.z);
                AttitudeLines.Add(degreeIndicatorUp);

                degreeIndicatorDown.transform.position = new Vector3(0, -h, DrawDistance);
                degreeIndicatorDown.transform.localScale = new Vector3(degreeIndicatorDown.transform.localScale.x,
                    degreeIndicatorDown.transform.localScale.y * lineSize,
                    degreeIndicatorDown.transform.localScale.z);
                AttitudeLines.Add(degreeIndicatorDown);
            }
        }
    }
}
