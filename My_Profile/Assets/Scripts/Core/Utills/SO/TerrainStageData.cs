using Core.Define;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain", menuName = "ScriptableObject/TerrainStagData", order = 6)]
public class TerrainStageData : BaseUpgradeData<Vector2>
{
    public Sprite TerrainIcon;
    public UpgradeType type;
}
