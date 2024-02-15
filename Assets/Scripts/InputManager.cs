using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    public TowerSc curTower;
    public PhoneSc curPhone;

    public static InputManager Instance;

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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            string[] layStrings = { "Tower", "Phone" };
            LayerMask layer = LayerMask.GetMask(layStrings);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 100f, layer);
            if (hit.collider != null)
            {
                // CAN ONLY USED FOR TOWER CHANGE LAYERMASK
                Debug.Log("Object below mouse: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Tower")) {
                    Debug.Log("Tower hit");
                    MainUISc.Instance.ShowTowerUI();
                    curTower = hit.collider.gameObject.GetComponentInParent<TowerSc>();
                    curTower.OnSelected();
                }
                else if (hit.collider.gameObject.CompareTag("Phone")) 
                {
                    if (curPhone != null) 
                    {
                        curPhone.isSelected = false;
                    }
                    MainUISc.Instance.ShowPhoneUI();
                    curPhone = hit.collider.gameObject.GetComponent<PhoneSc>();
                    curPhone.isSelected = true;
                    curPhone.OnSelected();
                    Debug.Log("Phone hit");
                }
            }
            else
            {
                Debug.Log("No object below mouse.");
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPos = new Vector3(mousePosition.x, mousePosition.y, 0f);
            TowerManager.Instance.SpawnTower(spawnPos);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPos = new Vector3(mousePosition.x, mousePosition.y, 0f);
            PhoneManager.Instance.SpawnPhone(spawnPos);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPos = new Vector3(mousePosition.x, mousePosition.y, 0f);
            ObjectsManager.Instance.SpawnBuilding(spawnPos);
        }
    }
}
