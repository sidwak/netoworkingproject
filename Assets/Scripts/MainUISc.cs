using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUISc : MonoBehaviour
{

    public GameObject phoneUICnt;
    public GameObject towerUICnt;

    public static MainUISc Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }


    void Start()
    {
        phoneUICnt = PhoneUISc.Instance.container;
        towerUICnt = TowerUISc.Instance.container;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            HideAllUIs();
        }
    }

    public void HideAllUIs() 
    {
        phoneUICnt.SetActive(false);
        towerUICnt.SetActive(false);
    }

    public void ShowPhoneUI() 
    {
        towerUICnt.SetActive(false);
        phoneUICnt.SetActive(true);
    }

    public void ShowTowerUI() 
    {
        phoneUICnt.SetActive(false);
        towerUICnt.SetActive(true);
    }
}

