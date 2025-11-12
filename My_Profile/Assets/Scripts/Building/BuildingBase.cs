using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class BuildingBase : MonoBehaviour
{
    [SerializeField] public GameObject infomaionObject;
    [SerializeField] public string buildingName;

    /// <summary>
    /// 충돌 감지
    /// </summary>
    /// <param name="collision"></param>
    public abstract void OnTriggerEnter2D(Collider2D collision);

    /// <summary>
    /// 빌딩 정보(포폴 정보) 보여주기
    /// </summary>
    public abstract void ShowInfomattion();
}
