using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VesselController : MonoBehaviour
{
    public bool startedLevel;
    [Header("Movement Parameters")] 
    public float sinkSpeed;
    public float sideSpeed;
    public float thrustSpeed;

    [Header("Meter Parameters")] public float oxygenConsumeSpeed;
    public float powerConsumeSpeed;
    public float hullDamageAmount;

    [Header("DeptCalculation")]
    public float depthFloat;
    
    private float originalSpeed, originalSideSpeed, originalY;

    [Header("UiReferences")]
    public SlicedFilledImage oxygenMeter;
    public SlicedFilledImage powerMeter;
    public SlicedFilledImage damageMeter;
    public TextMeshProUGUI depthLabel;
    
    void Start()
    {
        originalSpeed = sinkSpeed;
        originalSideSpeed = sideSpeed;
        sideSpeed = 0;
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startedLevel = !startedLevel;
        }
        if (startedLevel)
        {
            Movement();
            ConsumeOxygen();
            CalculateDepth();
        }
    }

    private void CalculateDepth()
    {
        float distanceTravelled = -(transform.position.y - originalY)/depthFloat;
        depthLabel.text = "Depth: "+ ((int)distanceTravelled).ToString() + " M";
    }

    private void ConsumeOxygen()
    {
        oxygenMeter.fillAmount -= oxygenConsumeSpeed*Time.deltaTime;
    }

    private void Movement()
    {
        Debug.Log(originalSpeed);
        transform.position = new Vector3(transform.position.x + sideSpeed, transform.position.y - sinkSpeed, transform.position.z);
        if (Input.GetKey(KeyCode.Space))
        {
            sinkSpeed = thrustSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            sinkSpeed = originalSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sideSpeed = -originalSideSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sideSpeed = originalSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            sinkSpeed = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            sideSpeed = 0;
        }
    }
}
