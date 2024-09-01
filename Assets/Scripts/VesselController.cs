using System;
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
    public float pressureGainSpeed;

    private float maxO2, maxPow, maxPSI;
    [Header("DeptCalculation")]
    public float depthFloat;
    
    private float originalSpeed, originalSideSpeed, originalY;
    private bool reach200, reach300;
    private int uniqueFishCount;
    public QuestHandler _questController;
    [Header("UiReferences")]
    public TextMeshProUGUI oxygenMeter;
    public TextMeshProUGUI powerMeter;
    public TextMeshProUGUI pressureMeter;
    public TextMeshProUGUI depthLabel;
    
    void Start()
    {
        originalSpeed = sinkSpeed;
        originalSideSpeed = sideSpeed;
        sideSpeed = 0;
        originalY = transform.position.y;
        maxO2 = 100;
        maxPow = 100;
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
            ConsumeMeters();
            CalculateDepth();
            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (maxO2 == 0 || maxPow == 0 || maxPSI == 0)
        {
            Initiate.Fade("Game Over Scene",Color.black, 4.0f);
        }
    }

    private void CalculateDepth()
    {
        float distanceTravelled = -(transform.position.y - originalY)/depthFloat;
        depthLabel.text = ((int)distanceTravelled).ToString() + " M";
        if (distanceTravelled > 300)
        {
            Initiate.Fade("Level Complete Scene",Color.black, 4.0f);
        }
  
    }

    private void ConsumeMeters()
    {
        float a = Mathf.Clamp(maxO2 - oxygenConsumeSpeed * Time.deltaTime, 0, 1);
        maxO2 = Mathf.Clamp(maxO2 - oxygenConsumeSpeed * Time.deltaTime, 0, 100);
        maxPow = Mathf.Clamp(maxPow - powerConsumeSpeed * Time.deltaTime, 0, 100);
        maxPSI = Mathf.Clamp(maxPSI + pressureGainSpeed * Time.deltaTime, 0, 100);
        oxygenMeter.text = ((int)maxO2).ToString()+"%";
        powerMeter.text = ((int)maxPow).ToString()+"%";
        pressureMeter.text = ((int)maxPSI).ToString()+"%";
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
            sideSpeed = originalSideSpeed;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Depth200")
        {
            _questController.OnDepthReach(200);
        }
        else if (other.tag == "Depth300")
        {
            _questController.OnDepthReach(300);
        }
        else if (other.tag == "Fish")
        {
            Debug.Log("FishY TOUCHY");
            uniqueFishCount++;
            if (uniqueFishCount == 5)
            {
                _questController.OnFishCapture();
            }
        }
    }
}
