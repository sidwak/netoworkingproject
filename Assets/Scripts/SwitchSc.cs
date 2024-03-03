using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchSc : MonoBehaviour
{

    public List<TowerSc> towers = new List<TowerSc>();

    public static SwitchSc Instance;

    void Awake()
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

    public void receiveMsgFromTower(int towerId, int phnId, string msg) 
    {
        if (towerId > towers.Count - 1)
        {
            logMsg($"Tower ID invalid Id:{towerId}");
        }
        else {
            sendMsgToTower(towerId, phnId, msg);
        }
    }

    public void sendMsgToTower(int towerId, int phnId, string msg) 
    {
        towers[towerId].receiveMsgFromSwitch(phnId, msg);
    }

    private void logMsg(string msg)
    {
        Debug.Log("Switch Center: "+msg);
    }
}
