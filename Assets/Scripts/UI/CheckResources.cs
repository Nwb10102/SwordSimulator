using UnityEngine;

public class CheckResources : MonoBehaviour
{
    void Start()
    {
        // Sword 폴더 안의 모든 Sprite 로드
        Sprite[] allSprites = Resources.LoadAll<Sprite>("Sword");

        Debug.Log($"[SpriteDebugger] 불러온 스프라이트 개수: {allSprites.Length}");

        foreach (var sprite in allSprites)
        {
            Debug.Log($"[SpriteDebugger] 불러온 스프라이트 이름: {sprite.name}");
        }

        if (allSprites.Length == 0)
        {
            Debug.LogWarning("[SpriteDebugger] ⚠️ 불러온 스프라이트가 없습니다! 폴더 이름 또는 위치를 확인하세요.");
        }
    }
}
