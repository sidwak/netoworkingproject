using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public GameObject phonePrefab;
    public List<PhoneSc> phones = new List<PhoneSc>();
    public static PhoneManager Instance;

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

    public void SpawnPhone(Vector3 pos)
    {
        GameObject newPhone = Instantiate(phonePrefab, gameObject.transform);
        newPhone.transform.localPosition = pos;
        PhoneSc phoneSc = newPhone.GetComponent<PhoneSc>();
        phones.Add(phoneSc);
        phoneSc.id = phones.IndexOf(phoneSc);
        phoneSc.globalId = phones.IndexOf(phoneSc);
        phoneSc.SetPhoneName("Phone n/a." + phones.IndexOf(phoneSc).ToString());
    }
}
