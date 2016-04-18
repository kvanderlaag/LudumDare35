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

    public float maxLineDistance = 0.5f;

    public float arcThickness = 0.001f;

    public float circleYOffset = 0.001f;

    private bool showArc = false;
    private bool showLine = false;
    private LineRenderer lineRenderer;
    public LineRenderer circleRenderer;
    private SteamVR_TrackedObject trackedObject;

    private float gravity = 0.00098f;

    private SteamVR_Controller.Device Controller;

    private Material arcMaterial;
    private Material circleMaterial;
    private Color startColor;
    public Color highlightColor = Color.green;

    public Transform TowerObject;
    public Transform holding;

    void Start()
    {
        arcMaterial = lineRenderer.material;
        startColor = arcMaterial.GetColor("_TintColor");
        
    }

	// Use this for initialization
	void OnEnable () {
        lineRenderer = GetComponent<LineRenderer>();
        circleMaterial = circleRenderer.material;

        SteamVR_TrackedController controller = transform.parent.GetComponent<SteamVR_TrackedController>();
        controller.PadTouched += PadTouched;
        controller.PadUntouched += PadUntouched;
        controller.PadClicked += PadClicked;
        controller.Gripped += GripClicked;
        controller.Ungripped += GripUnclicked;


        trackedObject = transform.parent.GetComponent<SteamVR_TrackedObject>();
        

	}

    void OnDisable()
    {
        SteamVR_TrackedController controller = transform.parent.GetComponent<SteamVR_TrackedController>();
        controller.Gripped -= GripClicked;
        controller.Ungripped -= GripUnclicked;
        controller.PadTouched -= PadTouched;
        controller.PadUntouched -= PadUntouched;
        controller.PadClicked -= PadClicked;
        
    }

    void PadTouched(object sender, ClickedEventArgs e)
    {
        if (holding)
            return;
        //Debug.Log("Touched pad " + trackedObject.index);
        showArc = true;
        lineRenderer.enabled = true;
        circleRenderer.enabled = true;
    }

    void PadUntouched(object sender, ClickedEventArgs e)
    {
        if (holding)
            return;
        //Debug.Log("Untouched pad " + trackedObject.index);
        showArc = false;
        lineRenderer.enabled = false;
        circleRenderer.enabled = false;
    }

    void PadClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log("Clicked touchpad " + trackedObject.index);
    }

    void GripClicked(object sender, ClickedEventArgs e)
    {
        if (!holding)
        { 
            //Debug.Log("Clicked grip " + trackedObject.index);
            holding = Instantiate(TowerObject, transform.position, Quaternion.Euler(new Vector3(-120f, 0f, 0f))) as Transform;
            holding.parent = transform;
            showLine = true;
            lineRenderer.enabled = true;
            circleRenderer.enabled = true;
        }
    }

    void GripUnclicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log("Released grip " + trackedObject.index);
        if (holding)
        {
            Ray r = new Ray(holding.position, Vector3.down);
            RaycastHit rch;
            if (Physics.Raycast(r, out rch, maxLineDistance))
            {
                if (rch.transform.gameObject.CompareTag("Buildable"))  // Add condition to check to make sure no tower is already built;
                {
                    if (rch.transform.gameObject.GetComponent<Buildable>().builtObject == null)
                        rch.transform.GetComponent<Buildable>().BuildTower(ETowerState.VAMPIRE);
                }
            }

            Destroy(holding.gameObject);
            holding = null;
            showLine = false;
            lineRenderer.enabled = false;
            circleRenderer.enabled = false;
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



        } else if (showLine)
        {
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, holding.position);

            Ray r = new Ray(holding.position, Vector3.down);
            RaycastHit rch;
            Vector3 centerPoint = new Vector3();
            if (Physics.Raycast(r, out rch, maxLineDistance) )
            {
                if (rch.transform.gameObject.CompareTag("Buildable"))
                {
                    lineRenderer.SetPosition(1, rch.point);
                    arcMaterial.SetColor("_TintColor", highlightColor);
                    circleMaterial.SetColor("_TintColor", highlightColor);
                    centerPoint = rch.point + new Vector3(0f, circleYOffset, 0f);
                } else
                {
                    lineRenderer.SetPosition(1, holding.position + (Vector3.down * maxLineDistance));
                    arcMaterial.SetColor("_TintColor", startColor);
                    circleMaterial.SetColor("_TintColor", startColor);
                    centerPoint = rch.point + holding.position + (Vector3.down * maxLineDistance) + new Vector3(0f, circleYOffset, 0f);
                }
            } else
            {
                lineRenderer.SetPosition(1, holding.position + (Vector3.down * maxLineDistance));
                arcMaterial.SetColor("_TintColor", startColor);
                circleMaterial.SetColor("_TintColor", startColor);
                centerPoint = rch.point + holding.position + (Vector3.down * maxLineDistance) + new Vector3(0f, circleYOffset, 0f);
            }

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



        }

    }
}
