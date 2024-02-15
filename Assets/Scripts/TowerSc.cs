using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerSc : MonoBehaviour
{
    public int maxDeviceCapacity;
    public int connDev = 0;
    public float minSpectrum = 0.6f;
    public float maxSpectrum = 0.8f;
    public float spectrumWidth = 0.2f;
    public float transmitPower = 10f;
    public float coverageRadius;
    public float powerConsumption;

    public GameObject coverageCircle;
    private CircleCollider2D circleCol;

    public TextMeshPro towerName;
   
    void Start()
    {
        circleCol = GetComponent<CircleCollider2D>();
        UpdateValues();
        UpdateRange();
    }

    void Update()
    {
        
    }

    public void UpdateValues() 
    {
        spectrumWidth = maxSpectrum - minSpectrum;
        maxDeviceCapacity = CalculateDeviceCapacity(transmitPower, spectrumWidth);
        coverageRadius = CalculateCoverage(transmitPower, spectrumWidth);
        powerConsumption = CalculatePowerConsumption(transmitPower, spectrumWidth);
        UpdateGUIValues();
        UpdateRange();
    }

    public void UpdateGUIValues() 
    {
        if (TowerUISc.Instance.container.activeInHierarchy == false) { return; }
        TowerUISc.Instance.avgCapa.text = maxDeviceCapacity.ToString();
        TowerUISc.Instance.covOut.text = coverageRadius.ToString();
        TowerUISc.Instance.powerCnOut.text = powerConsumption.ToString();
    }

    public void OnSelected() 
    {
        UpdateGUIValues();
    }

    public void UpdateRange() 
    {
        circleCol.radius = coverageRadius/2f;
        coverageCircle.transform.localScale = new Vector3(coverageRadius, coverageRadius, 1f);
    }

    public void SetTowerName(string newName) 
    {
        towerName.text = newName;
        gameObject.name = "BaseStation" + newName;
    }

    int CalculateDeviceCapacity(float towerPower, float spectrum)
    {
        float deviceCapacity = towerPower * spectrum * 10f;
        return (int)deviceCapacity * 10;
    }

    float CalculateCoverage(float towerPower, float spectrum)
    {
        float coverage = (towerPower * spectrum / 5f * 10f) * Random.Range(0.1f, 0.12f);
        return coverage * 10f;
    }

    float CalculatePowerConsumption(float towerPower, float spectrum)
    {
        float powerConsumption = towerPower * spectrum / 2.5f;
        return powerConsumption * 10f;
    }
}
