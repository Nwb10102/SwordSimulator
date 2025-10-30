using UnityEngine;


public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private CSVLoader csvLoader;

    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TryUpgrade()
    {
        int swordID = GameManager.Instance != null ? GameManager.Instance.currentLevel : 0;
        int destroyDefenceCoin = GameManager.Instance != null ? GameManager.Instance.failProtection : 0;
        int currentCoins = GameManager.Instance != null ? GameManager.Instance.playerCoins : 0;
        Debug.Log($"현재 검 ID: {swordID}");

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
        if (currentCoins < sword.RequiredCoins)
        {
            Debug.Log("코인이 부족하여 강화를 시도할 수 없습니다.");
            return;
        }

        GameManager.Instance.AddCoins(-sword.RequiredCoins); // 강화 비용 차감

        if (randomValue <= sword.UpgradeChance)
        {
            Debug.Log("강화 성공!");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.currentLevel += 1;
                Debug.Log($"GameManager.currentLevel 증가: {GameManager.Instance.currentLevel}");
            }
        }
        else if (randomValue <= sword.DestroyChance)
        {
            if (destroyDefenceCoin > 0)
            {
                GameManager.Instance.UseFailProtection();
                Debug.Log("파괴 보호 코인 사용으로 검이 파괴되지 않았습니다.");
                return;
            }

            Debug.Log("파괴되었습니다...");
            audioSource.Play();


            if (GameManager.Instance != null)
            {
                GameManager.Instance.currentLevel = 0;
                Debug.Log("GameManager.currentLevel 이 0으로 초기화되었습니다.");
            }
        }
        else
        {
            Debug.Log("강화 실패 (유지)");
        }
    }
}

