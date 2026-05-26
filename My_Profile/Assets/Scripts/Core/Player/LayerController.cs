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
        //건물 위
        if (buildingCNT > 0) 
            pSpriteRender.sortingLayerName = frontLayer;
        //건물 위, 울타리 뒤
        else if (fenceCNT > 0)
            pSpriteRender.sortingLayerName = midLayer;
        //기본
        else
            pSpriteRender.sortingLayerName = backLayer;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Fence")
            fenceCNT++;
        else if (collision.tag == "Building")
            buildingCNT++;
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "Fence")
            fenceCNT--;
        else if (collision.tag == "Building")
            buildingCNT--;
    }
}
