using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    GameObject selectTower, twoTower;
    [SerializeField] Transform selectObject;
        
    void Start()
    {
        
    }
    void Update()
    {
        if(Controll.Instance._state == "Game")
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Tower" && hit.collider.gameObject.GetComponent<Tower>().towerType == Tower.Character.player)
                    {                        
                        hit.collider.gameObject.GetComponent<Tower>().Selected();
                        selectTower = hit.collider.gameObject;

                        selectObject.transform.position = selectTower.transform.GetChild(0).transform.position;
                        selectObject.GetComponent<LineRenderer>().SetPosition(1, selectTower.transform.GetChild(0).position);                        
                        selectObject.gameObject.SetActive(true);
                    }
                }
            }
            if (Input.GetMouseButton(0) && selectTower != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Tower" && (hit.collider.gameObject.GetComponent<Tower>().towerType == Tower.Character.neutral || hit.collider.gameObject.GetComponent<Tower>().towerType == Tower.Character.enemy))
                    {
                        twoTower = hit.collider.gameObject;
                        selectObject.position = new Vector3(hit.collider.gameObject.transform.position.x, selectObject.position.y, hit.collider.gameObject.transform.position.z);
                    }
                    if(hit.collider.gameObject.tag == "Ground")
                    {
                        Vector3 pos = hit.point;
                        selectObject.position = new Vector3(pos.x, selectObject.position.y, pos.z);
                    }
                }
                selectObject.GetComponent<LineRenderer>().SetPosition(0, selectObject.position);
            }
            if (Input.GetMouseButtonUp(0) && selectTower != null)
            {
                selectTower.GetComponent<Tower>().DropSelected();               
                selectObject.gameObject.SetActive(false);
               
                if (twoTower != null)
                {
                    selectTower.GetComponent<Tower>().AddTarget(twoTower);
                    twoTower.GetComponent<Tower>().SetType(Tower.Character.player);
                }

                selectTower = null;
                twoTower = null;
            }
        }        
    }
}
