using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;

[RequireComponent (typeof(LineRenderer))]
public class HandController : MonoBehaviour {

    public static bool casting = false;
    private bool thisCasting = false;

    public static int screenshotnum = 0;

    public GameState gameState;

    public float grabDistance;

    public Transform pickup;

    private Vector3 target;

    public Material powerMoonMaterial, powerSunMaterial, powerRainMaterial;

    private PlayerPowers playerPowers;

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
    public Renderer powerRenderer;
    private SteamVR_TrackedObject trackedObject;

    private HandSounds sounds;

    public Transform hmdCamera;

    private float gravity = 0.002f;

    private SteamVR_Controller.Device Controller;

    private Material arcMaterial;
    private Material circleMaterial;
    private Color startColor;
    public Color highlightColor = Color.green;

    private SteamVR_TrackedController controller;

    public Transform TowerObject;
    public Transform holding;

    void Start()
    {
        
        arcMaterial = lineRenderer.material;
        startColor = arcMaterial.GetColor("_TintColor");
        
    }

	// Use this for initialization
	void OnEnable () {
        gameState = GameObject.Find("Map").GetComponent<GameState>();
        sounds = GetComponent<HandSounds>();
        lineRenderer = GetComponent<LineRenderer>();
        circleMaterial = circleRenderer.material;
        playerPowers = transform.parent.parent.GetComponent<PlayerPowers>();

        SteamVR_TrackedController controller = transform.parent.GetComponent<SteamVR_TrackedController>();
        controller.PadTouched += PadTouched;
        controller.PadUntouched += PadUntouched;
        controller.TriggerClicked += PadClicked;
        controller.Gripped += GripClicked;
        controller.Ungripped += GripUnclicked;
        controller.PadClicked += TriggerClicked;
        controller.MenuButtonClicked += MenuClicked;
     


        trackedObject = transform.parent.GetComponent<SteamVR_TrackedObject>();
        

	}

    void OnDisable()
    {
        controller = transform.parent.GetComponent<SteamVR_TrackedController>();
        controller.Gripped -= GripClicked;
        controller.Ungripped -= GripUnclicked;
        controller.PadTouched -= PadTouched;
        controller.PadUntouched -= PadUntouched;
        controller.TriggerClicked -= PadClicked;
        controller.PadClicked -= TriggerClicked;
        controller.MenuButtonClicked -= MenuClicked;
     

    }

    void MenuClicked(object sender, ClickedEventArgs e)
    {
        if (gameState.phaseState == EPhaseState.NONE)
        {
            gameState.StartGame();
        }
    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (!pickup)
            return;

        if (pickup.GetComponent<Tower>())
        {
            
            var device = SteamVR_Controller.Input((int)trackedObject.index);
            //Debug.Log("Clicked touchpad " + trackedObject.index);
            Vector2 touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            //Debug.Log("Touchpad x: " + touchpad.x + ", y: " + touchpad.y);
            float angle = Mathf.Rad2Deg * Mathf.Atan2(touchpad.y, touchpad.x);
            //Debug.Log("Angle: " + angle);
            while (angle < 0)
            {
                angle += 360f;
            }
            while (angle > 360)
            {
                angle -= 360f;
            }
            if (angle <= 360f / 3f)
            {
                pickup.GetComponent<Tower>().SwitchTower(ETowerState.WEREWOLF);
            }
            else if (angle <= 360f / 3f * 2f)
            {
                pickup.GetComponent<Tower>().SwitchTower(ETowerState.VAMPIRE);
            }
            else
            {
                pickup.GetComponent<Tower>().SwitchTower(ETowerState.ALIEN);
            }
            device.TriggerHapticPulse(500);
        }
    }

    void TriggerUnclicked(object sender, ClickedEventArgs e)
    {

    }

    void PadTouched(object sender, ClickedEventArgs e)
    {
        if (holding || casting)
            return;


        //Debug.Log("Touched pad " + trackedObject.index);
        casting = true;
        thisCasting = true;
        showArc = true;
        lineRenderer.enabled = true;
        circleRenderer.enabled = true;
        powerRenderer.enabled = true;
    }

    void PadUntouched(object sender, ClickedEventArgs e)
    {
        if (holding)
            return;

        if (thisCasting == true)
        {
            casting = false;
            thisCasting = false;
        }
        //Debug.Log("Untouched pad " + trackedObject.index);
        showArc = false;
        lineRenderer.enabled = false;
        circleRenderer.enabled = false;
        powerRenderer.enabled = false;
        target = new Vector3();
    }

    void PadClicked(object sender, ClickedEventArgs e)
    {
        if (!target.Equals(new Vector3()))
        {
            if (playerPowers.AttemptExecutePower(target))
            {
                var device = SteamVR_Controller.Input((int)trackedObject.index);
                device.TriggerHapticPulse(1000);
                sounds.Spell();
            }
        }
    }

    void GripClicked(object sender, ClickedEventArgs e)
    {
        if (gameState.phaseState == EPhaseState.ENEMYWAVE || gameState.phaseState == EPhaseState.NONE)
            return;
        if (!holding)
        {
            if (pickup)
            {
                if (pickup.gameObject.CompareTag("Tower"))
                {
                    gameState.curTowers--;
                    Destroy(pickup.gameObject);
                }
            }

            if (gameState.curTowers < gameState.maxTowers)
            {
                //Debug.Log("Clicked grip " + trackedObject.index);
                holding = Instantiate(TowerObject, transform.position, Quaternion.Euler(new Vector3(-120f, 0f, 0f))) as Transform;
                holding.parent = transform;
                sounds.Pickup();
                showLine = true;
                lineRenderer.enabled = true;
                circleRenderer.enabled = true;
            }
        }
    }

    void GripUnclicked(object sender, ClickedEventArgs e)
    {
        if (gameState.phaseState == EPhaseState.ENEMYWAVE)
        {
            if (holding)
            {
                Destroy(holding.gameObject);
                holding = null;
                showLine = false;
                lineRenderer.enabled = false;
                circleRenderer.enabled = false;
            }
            return;
        }
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
            powerRenderer.transform.LookAt(Camera.main.transform);
            powerRenderer.transform.Rotate(Vector3.up, 180f);
            target = new Vector3();
            var device = SteamVR_Controller.Input((int)trackedObject.index);
            //Debug.Log("Clicked touchpad " + trackedObject.index);
            Vector2 touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            //Debug.Log("Touchpad x: " + touchpad.x + ", y: " + touchpad.y);
            float angle = Mathf.Rad2Deg * Mathf.Atan2(touchpad.y, touchpad.x);
            //Debug.Log("Angle: " + angle);
            while (angle < 0)
            {
                angle += 360f;
            }
            while (angle > 360)
            {
                angle -= 360f;
            }
            if (angle <= 360f / 3f)
            {
                playerPowers.currentPowerIdx = 0;
                powerRenderer.material = powerMoonMaterial;
            }
            else if (angle <= 360f / 3f * 2f)
            {
                playerPowers.currentPowerIdx = 1;
                powerRenderer.material = powerSunMaterial;
            }
            else
            {
                playerPowers.currentPowerIdx = 2;
                powerRenderer.material = powerRainMaterial;
            }


            float amount = playerPowers.powers[playerPowers.currentPowerIdx].cooldownTimerCur / playerPowers.powers[playerPowers.currentPowerIdx].cooldown;
            Color lerpColor = Color.Lerp(Color.white, Color.red, amount);
            powerRenderer.material.SetColor("_Color", lerpColor);

            if (pickup)
            {
                lineRenderer.enabled = false;
                circleRenderer.enabled = false;
                return;
            }

            lineRenderer.enabled = true;
            circleRenderer.enabled = true;

            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(transform.position);

            arcMaterial.SetColor("_TintColor", startColor);
            circleMaterial.SetColor("_TintColor", startColor);
            bool hit = false;
            {
                int i = 1;
                while (Vector3.Distance(transform.position, linePoints[linePoints.Count - 1]) < maxArcDistance)
                {
                    Ray r = new Ray(linePoints[i - 1], (transform.forward * arcResolution) + (Vector3.down * i * gravity * arcResolution));
                    RaycastHit rch;
                    if (Physics.Raycast(r, out rch, Vector3.Distance(linePoints[i - 1], linePoints[i - 1] + (transform.forward * arcResolution) + (Vector3.down * i * gravity * arcResolution))))
                    {
                        linePoints.Add(rch.point);
                        arcMaterial.SetColor("_TintColor", highlightColor);
                        circleMaterial.SetColor("_TintColor", highlightColor);
                        hit = true;
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
            if (hit)
                centerPoint.y = Mathf.Max(centerPoint.y, 0.707338f + circleYOffset + (2 * arcThickness));
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

            target = centerPoint;



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
                    centerPoint = rch.point + new Vector3(0f, circleYOffset, 0f);
                }
            } else
            {
                lineRenderer.SetPosition(1, holding.position + (Vector3.down * maxLineDistance));
                arcMaterial.SetColor("_TintColor", startColor);
                circleMaterial.SetColor("_TintColor", startColor);
                centerPoint = holding.position + (Vector3.down * maxLineDistance) + new Vector3(0f, circleYOffset, 0f);
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

    public void OnTriggerEnter(Collider other)
    {
        if (!pickup)
        {
            pickup = other.transform;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (pickup == other.transform)
        {
            pickup = null;
        }
    }
}
