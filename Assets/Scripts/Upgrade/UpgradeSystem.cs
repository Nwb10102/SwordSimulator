using UnityEngine;


public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private CSVLoader csvLoader;
    // TryUpgrade(): 기본적으로 GameManager.Instance.currentLevel을 '검 ID'로 사용합니다.
    // 기존의 호출(인자로 ID 전달)도 지원하기 위해 오버로드를 제공합니다.
    // 동작:
    // - 성공: GameManager.Instance.currentLevel += 1
    // - 실패(유지): 아무 변경 없음
    // - 파괴: GameManager.Instance.currentLevel = 0
    public void TryUpgrade()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager 인스턴스가 없습니다. currentLevel을 읽을 수 없습니다.");
            return;
        }

        // 현재 레벨의 다음 레벨(업그레이드 대상)을 시도하도록 변경
        int targetID = GameManager.Instance.currentLevel + 1;
        Debug.Log($"자동 업그레이드 대상 ID: {targetID} (currentLevel: {GameManager.Instance.currentLevel})");
        TryUpgrade(targetID);
    }

    // 기존 시그니처를 유지하면서도 GameManager의 currentLevel을 갱신합니다.
    public void TryUpgrade(int swordID)
    {
        if (csvLoader == null)
        {
            Debug.LogWarning("CSVLoader가 할당되어 있지 않습니다.");
            return;
        }

        SwordData sword = csvLoader.swordDataList.Find(x => x.ID == swordID);
        if (sword == null)
        {
            Debug.LogWarning($"해당 ID({swordID})의 검을 찾을 수 없습니다!");
            return;
        }

        Debug.Log($"[{sword.Name}] 강화 시도! (ID: {swordID})");

        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= sword.UpgradeChance)
        {
            Debug.Log("강화 성공!");
            // 성공 시 currentLevel을 증가
            if (GameManager.Instance != null)
            {
                GameManager.Instance.currentLevel += 1;
                Debug.Log($"GameManager.currentLevel 증가: {GameManager.Instance.currentLevel}");
            }
        }
        else if (randomValue <= sword.UpgradeChance + sword.DestroyChance)
        {
            Debug.Log("파괴되었습니다...");
            // 파괴 시 currentLevel을 0으로 리셋
            if (GameManager.Instance != null)
            {
                GameManager.Instance.currentLevel = 0;
                Debug.Log("GameManager.currentLevel 이 0으로 초기화되었습니다.");
            }
        }
        else
        {
            Debug.Log("강화 실패 (유지)");
            // 실패(유지) : 아무 변경 없음
        }
    }
}

