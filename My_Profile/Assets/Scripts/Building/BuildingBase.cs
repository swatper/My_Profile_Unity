using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class BuildingBase : MonoBehaviour
{
    [SerializeField] public GameObject infomaionUI;
    [SerializeField] public InfoUI uiScript;
    [SerializeField] public string buildingName;

    /// <summary>
    /// 충돌 감지
    /// </summary>
    /// <param name="collision"></param>
    public abstract void OnTriggerEnter2D(Collider2D collision);
}
