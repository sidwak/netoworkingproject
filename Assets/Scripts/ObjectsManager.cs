using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public GameObject buildingPrefab;
    public List<GameObject> buildings = new List<GameObject>();

    public static ObjectsManager Instance;

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

    public void SpawnBuilding(Vector3 pos) 
    {
        GameObject newBuildling = Instantiate(buildingPrefab, gameObject.transform);
        newBuildling.transform.localPosition = pos;
        //PhoneSc phoneSc = newPhone.GetComponent<PhoneSc>();
        buildings.Add(newBuildling);
        //phoneSc.SetPhoneName("Phone" + phones.IndexOf(phoneSc).ToString());
    }
}
