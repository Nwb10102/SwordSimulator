using UnityEngine;


public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private CSVLoader csvLoader;

    public void TryUpgrade(int swordID)
    {
        SwordData sword = csvLoader.swordDataList.Find(x => x.ID == swordID);
        if (sword == null)
        {
            Debug.LogWarning("해당 ID의 검을 찾을 수 없습니다!");
            return;
        }

        Debug.Log($"[{sword.Name}] 강화 시도!");

        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= sword.UpgradeChance)
        {
            Debug.Log("강화 성공!");
        }
        else if (randomValue <= sword.UpgradeChance + sword.DestroyChance)
        {
            Debug.Log("파괴되었습니다...");
        }
        else
        {
            Debug.Log("강화 실패 (유지)");
        }
    }
}

