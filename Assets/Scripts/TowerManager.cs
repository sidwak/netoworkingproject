using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject TowerPrefab;
    public List<TowerSc> towers = new List<TowerSc>();
    public static TowerManager Instance;

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

    public void SpawnTower(Vector3 pos) 
    {
        GameObject newTower = Instantiate(TowerPrefab, gameObject.transform);
        newTower.transform.localPosition = pos;
        TowerSc towerSc = newTower.GetComponent<TowerSc>();
        towers.Add(towerSc);
        towerSc.SetTowerName("Tower " + towers.IndexOf(towerSc).ToString());
    }
}
