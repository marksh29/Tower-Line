using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum Character
    {
        neutral, player, enemy
    }

    public Character towerType;
    [SerializeField] Material[] typeMaterial;
    [SerializeField] GameObject linePrefab;
    [SerializeField] List<GameObject> targets;
    [SerializeField] List<LineRenderer> lines;

    bool select;
    GameObject selectObject;

    private void Awake()
    {
        selectObject = transform.GetChild(0).gameObject;
        selectObject.SetActive(false);
    }
    void Start()
    {
        SetColor();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveTarget(targets[1]);
        }
    }

    public void SetType(Character tp)
    {
        towerType = tp;
        SetColor();
    }
    void SetColor()
    {
        switch (towerType)
        {
            case (Character.neutral):
                GetComponent<MeshRenderer>().sharedMaterial = typeMaterial[0];
                break;
            case (Character.player):
                GetComponent<MeshRenderer>().sharedMaterial = typeMaterial[1];
                break;
            case (Character.enemy):
                GetComponent<MeshRenderer>().sharedMaterial = typeMaterial[2];
                break;
        }
    }
    public void Selected()
    {
        select = true;
        selectObject.SetActive(select);
    }
    public void DropSelected()
    {
        select = false;
        selectObject.SetActive(select);
    }

    public void AddTarget(GameObject obj)
    {
        if (!targets.Contains(obj))
        {
            targets.Add(obj);
            GameObject objt = Instantiate(linePrefab, transform) as GameObject;

            lines.Add(objt.GetComponent<LineRenderer>());
            objt.GetComponent<LineRenderer>().SetPosition(0, transform.GetChild(0).position);
            objt.GetComponent<LineRenderer>().SetPosition(1, obj.transform.GetChild(0).position);            
        }
    }
    public void RemoveTarget(GameObject obj)
    {
        int ct = targets.IndexOf(obj);
        Destroy(lines[ct].gameObject, 0.1f);
        lines.Remove(lines[ct]);
        targets.Remove(obj);
    }
}
