using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Speed
{
    public float spd;
}

[CreateAssetMenu(fileName = "Speed", menuName = "ScriptableObject/Player/Speed", order = 3)]
public class SpeedData : BaseUpgradeData<Speed>
{ }
