using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;

[RequireComponent (typeof(LineRenderer))]
public class HandController : MonoBehaviour {

    public float arcResolution;
    public int circleResolution = 20;
    public float circleRadius = 0.1f;
    public float maxArcDistance;

    public float arcThickness = 0.001f;

    public float circleYOffset = 0.001f;

    private bool showArc = false;
    private LineRenderer lineRenderer;
    public LineRenderer circleRenderer;
    private SteamVR_TrackedObject trackedObject;

    private float gravity = 0.00098f;

    private SteamVR_Controller.Device Controller;

    private Material arcMaterial;
    private Material circleMaterial;
    private Color startColor;
    public Color highlightColor = Color.green;

	// Use this for initialization
	void Awake () {
        lineRenderer = GetComponent<LineRenderer>();
        
        trackedObject = transform.parent.GetComponent<SteamVR_TrackedObject>();
        arcMaterial = lineRenderer.material;
        startColor = arcMaterial.GetColor("_TintColor");
        circleMaterial = circleRenderer.material;   

	}

    void FixedUpdate()
    {
        Controller = SteamVR_Controller.Input((int)trackedObject.index);
        if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            showArc = true;
        }
        else
        {
            showArc = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (showArc == true)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(transform.position);

            arcMaterial.SetColor("_TintColor", startColor);
            circleMaterial.SetColor("_TintColor", startColor);
            {
                int i = 1;
                while (Vector3.Distance(transform.position, linePoints[linePoints.Count - 1]) < maxArcDistance)
                {
                    Ray r = new Ray(linePoints[i - 1], (transform.forward * arcResolution) + (Vector3.down * i * gravity * arcResolution));
                    RaycastHit rch;
                    if (Physics.Raycast(r, out rch, Vector3.Distance(linePoints[i - 1], linePoints[i - 1] + (transform.forward * 0.01f) + (Vector3.down * i * gravity * arcResolution))))
                    {
                        linePoints.Add(rch.point);
                        arcMaterial.SetColor("_TintColor", highlightColor);
                        circleMaterial.SetColor("_TintColor", highlightColor);
                        break;
                    }
                    else {
                        linePoints.Add(linePoints[i - 1] + (transform.forward * arcResolution) + (Vector3.down * i * gravity * arcResolution));
                    }
                    i++;
                }
                lineRenderer.SetVertexCount(linePoints.Count);
                lineRenderer.SetPositions(linePoints.ToArray());
                lineRenderer.SetWidth(arcThickness, arcThickness);
                lineRenderer.enabled = true;
            }

            
            Vector3 centerPoint = linePoints[linePoints.Count - 1] + new Vector3(0f, circleYOffset, 0f);
            List<Vector3> circlePoints = new List<Vector3>();
            circlePoints.Add(centerPoint + (Vector3.forward * circleRadius));
            for (int i = 1; i <= circleResolution; ++i)
            {
                float x = Mathf.Sin(i * (360f / circleResolution) * Mathf.Deg2Rad) * circleRadius;
                float z = Mathf.Cos(i * (360f / circleResolution) * Mathf.Deg2Rad) * circleRadius;
                circlePoints.Add(centerPoint + new Vector3(x, 0f, z));
            }
            circleRenderer.SetVertexCount(circleResolution + 1);
            circleRenderer.SetPositions(circlePoints.ToArray());
            circleRenderer.SetWidth(arcThickness, arcThickness);
            circleRenderer.enabled = true;
                    

        } else
        {
            lineRenderer.enabled = false;
            circleRenderer.enabled = false;
        }

    }

    void ShowLineRenderer()
    {
        showArc = true;
        //Debug.Log("Touched Controller " + (int)trackedObject.index);
    }

    void HideLineRenderer()
    {
        showArc = false;
        //Debug.Log("Released Controller " + (int)trackedObject.index);
    }
}
