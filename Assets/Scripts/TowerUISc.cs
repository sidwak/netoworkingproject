using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerUISc : MonoBehaviour
{
    
    public TMP_InputField transmitPIn;
    public TMP_InputField minSIn;
    public TMP_InputField maxSIn;
    public TMP_Text avgCapa;
    public TMP_Text covOut;
    public TMP_Text powerCnOut;

    public GameObject container;

    public static TowerUISc Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SetValuesCalled() {
        TowerSc tower = InputManager.Instance.curTower;
        if (tower == null ) { return; }
        if (transmitPIn.text != "")
        {
            tower.transmitPower = float.Parse(transmitPIn.text);
        }
        if (minSIn.text != "") 
        {
            tower.minSpectrum = float.Parse(minSIn.text);
        }
        if (maxSIn.text != "")
        {
            tower.maxSpectrum = float.Parse(maxSIn.text);
        }
        tower.UpdateValues();
    }
}
