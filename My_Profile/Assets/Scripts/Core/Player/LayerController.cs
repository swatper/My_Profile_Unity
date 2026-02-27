using UnityEngine;

public class LayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer pSpriteRender;
    [SerializeField] string frontLayer = "Player_Front";
    [SerializeField] string midLayer = "Player_Mid";
    [SerializeField] string backLayer = "Player_Back";
    [SerializeField] int fenceCNT = 0;
    [SerializeField] int buildingCNT = 0;

    /// <summary>
    /// 레이어 상태를 실시간으로 확인
    /// </summary>
    private void LateUpdate()
    {
        if (buildingCNT > 0) 
            pSpriteRender.sortingLayerName = frontLayer;
        else if (fenceCNT > 0)
            pSpriteRender.sortingLayerName = midLayer;
        else
            pSpriteRender.sortingLayerName = backLayer;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Fence")
            fenceCNT++;
        else 
            buildingCNT++;
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "Fence")
            fenceCNT--;
        else
            buildingCNT--;
    }
}
