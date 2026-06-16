using UnityEngine;

public class TerrainScaler : BaseUpgradeModel<Vector2>
{
    [SerializeField] Transform terrianSize;
    protected TerrainStageData terrainData => SOData as TerrainStageData;
    protected Vector2 terrainStat;

    protected void Awake(){
        InitTerrainSize();
    }

    void InitTerrainSize(){
        terrianSize.localScale = terrainData.levelTables[0];
    }

    public override string GetDescription(){
        Debug.Log("지형 정보 넘겨주기");
        float upgradeSize = terrainData.levelTables[currentLevel].x - terrianSize.localScale.x;
        string desc = $"    Terrain += {upgradeSize}; \n";
        return desc + "}";
    }

    public override void Upgrade()
    {
        base.Upgrade();
        terrianSize.localScale = terrainData.levelTables[currentLevel - 1];
    }

    public override Sprite GetIcon(){
        Debug.Log("지형 아이콘 넘겨주기");
        return terrainData.TerrainIcon;
    }
}
