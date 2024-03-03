using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneSc : MonoBehaviour
{
    public int id = 0;
    public int globalId = 0;
    public bool isSelected = false;
    public float movSpeed = 1f;

    public float avgSpeed;
    public float packetLoss;
    public float signalStrength;
    public float disToTower;
    public string recvdMsg = "";

    public GameObject line;

    public TextMeshPro nameText;

    public TowerSc connectedTower;
    public List<TowerSc> inRangeTowers = new List<TowerSc>();

    void Start()
    {
        
    }

    void Update()
    {
        if (isSelected == true)
        {
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.W))
            {
                newPosition.y += movSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newPosition.y -= movSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newPosition.x -= movSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newPosition.x += movSpeed * Time.deltaTime;
            }
            transform.position = newPosition;
            if (connectedTower != null)
            {
                disToTower = Vector2.Distance(gameObject.transform.position, connectedTower.gameObject.transform.position);
                line.transform.localScale = new Vector3(disToTower, 0.08f, 1f);
                Vector2 dir = connectedTower.transform.position - transform.position;
                line.transform.right = dir.normalized;
                UpdateValues();
            }
            else { }
        }
        if (connectedTower == null) 
        {
            line.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    public void SelectClosestTower() 
    {
        float minDis = 9999999999f;
        foreach (TowerSc cTower in inRangeTowers) 
        {
            float disBet = Vector2.Distance(gameObject.transform.position, cTower.gameObject.transform.position);
            if (disBet < minDis)
            {
                minDis = disBet;
                disToTower = disBet;
                connectedTower = cTower;
            }
        }
        connectedTower.connDev += 1;
        connectedTower.phones.Add(this);
        id = connectedTower.phones.IndexOf(this);
        SetNewPhoneName(connectedTower.id.ToString());
        UpdateValues();
    }

    public void UpdateValues() 
    {
        avgSpeed = CalculateTransferSpeed();
        avgSpeed = CastRayAndDetect();
        signalStrength = CalculateSignalStrength();
        packetLoss = CalculatePacketLoss();
        if (isSelected == true) 
        {
            PhoneUISc.Instance.SetValues();
        }
    }
    public void ResetValues()
    {
        avgSpeed = 0f;
        signalStrength = 0f;
        packetLoss = 0f;
        UpdateGUIValues();
    }

    public float CastRayAndDetect() 
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (connectedTower.transform.position - transform.position).normalized, disToTower);
        int numColliders = 0;
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit.collider.gameObject.CompareTag("Phone"))
            {
                numColliders++;
            }
            else if (hit.collider.gameObject.CompareTag("Tower")) {
                break;
            }
        }
        //Debug.Log(numColliders);
        float transferSpeed = avgSpeed / ((numColliders + 1)*0.50f);
        if (numColliders <= 2) 
        {
            transferSpeed = avgSpeed;
        }
        return transferSpeed;
    }

    public void UpdateGUIValues() 
    {
        if (PhoneUISc.Instance.container.activeInHierarchy == true) 
        {
            PhoneUISc.Instance.SetValues();
        }
    }

    public void OnSelected() 
    {
        UpdateGUIValues();
    }

    public float CalculateTransferSpeed()
    {
        float distance = disToTower;
        float transferSpeed = ((connectedTower.transmitPower * connectedTower.spectrumWidth) / (distance + 1)) * 10f - ((connectedTower.connDev+1) * 0.055f);

        return transferSpeed;
    }

    public float CalculateSignalStrength()
    {
        float distance = disToTower;
        float signalStrength = (connectedTower.transmitPower / (distance * distance)) * 10f;
        return signalStrength;
    }

    public float CalculatePacketLoss()
    {
        return Random.Range(0f, 0.05f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BaseStation")) 
        {
            //Debug.Log("Collider Enter");
            TowerSc rTower = collision.gameObject.GetComponent<TowerSc>();
            if (inRangeTowers.Contains(rTower) == false) 
            {
                inRangeTowers.Add(rTower);
            }
            SelectClosestTower();
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BaseStation"))
        {
            //Debug.Log("Collider Exit");
            TowerSc rTower = collision.gameObject.GetComponent<TowerSc>();
            if (inRangeTowers.Contains(rTower) == true)
            {
                inRangeTowers.Remove(rTower);
                if (rTower == connectedTower)
                {
                    ResetValues();
                    PhoneUISc.Instance.conTwTx.text = "Connected Tower: None";
                    connectedTower.connDev -= 1;
                    connectedTower.phones.Remove(this);
                    connectedTower = null;
                    id = globalId;
                    SetNewPhoneName("n/a");
                    if (inRangeTowers.Count > 0)
                    {
                        SelectClosestTower();
                    }
                }
            }
        }
    }

    public void SetPhoneName(string name) 
    {
        gameObject.name = name;
        nameText.text = name;
    }
    public void SetNewPhoneName(string towerId) 
    {
        string name = "Phone "+towerId+"." + id.ToString();
        gameObject.name = name;
        nameText.text = name;
    }

    public void sentMessage(int towerId, int recvPhnId, string msg) 
    {
        string from = $"From {nameText.text}: "+msg;
        connectedTower.sendMsgToSwitch(towerId, recvPhnId, from);    
    }

    public void receiveMessage(string msg) 
    {
        recvdMsg += msg;
        recvdMsg += "\n";
        UpdateGUIValues();
    }
}
