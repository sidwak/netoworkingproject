using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneUISc : MonoBehaviour
{
    public TMP_Text conTwTx;
    public TMP_Text sigStTx;
    public TMP_Text pacLtTx;
    public TMP_Text speedTx;
    public TMP_Text numToTx;
    public TMP_InputField phnIpIn;
    public TMP_InputField msgIn;
    public TMP_InputField msgOut;

    public PhoneSc curPhone = null;

    public GameObject container;

    public static PhoneUISc Instance;

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

    public void SetValues() 
    {
        curPhone = InputManager.Instance.curPhone;
        if (curPhone == null) { return; }
        if (curPhone.connectedTower == null) { return; }
        conTwTx.text = "Connected Tower: " + curPhone.connectedTower.gameObject.name;
        sigStTx.text = "Signal Strength: " + curPhone.signalStrength.ToString() + "dB";
        pacLtTx.text = "Packet Loss: "+ curPhone.packetLoss.ToString() + "%";
        speedTx.text = "Average Speed: " + curPhone.avgSpeed.ToString() + " Mbps";
        numToTx.text = "Tower in Range: " + curPhone.inRangeTowers.Count.ToString();
        msgOut.text = curPhone.recvdMsg;
    }

    public void OnMsgBtnClicked() {
        curPhone = InputManager.Instance.curPhone;
        if (curPhone == null) { return; }
        if (curPhone.connectedTower == null) { return; }
        string []arr = phnIpIn.text.Split('.');
        int towerId = int.Parse(arr[0]);
        int phnId = int.Parse(arr[1]);
        string msg = msgIn.text;
        curPhone.sentMessage(towerId, phnId, msg);
    }
}
