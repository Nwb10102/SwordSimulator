using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private CSVLoader csvLoader;

    public AudioSource audioSource;
    public GameObject effectTextTarget; // 여기에 Text 컴포넌트가 있어야 함

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

            if (effectTextTarget != null)
            {
                Text txt = effectTextTarget.GetComponent<Text>();
                if (txt != null)
                {
                    StopAllCoroutines(); // 혹시 이전 효과가 남아있다면 멈춤
                    StartCoroutine(FadeTextColor(txt, Color.green, Color.white, 0.5f));
                }
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

            if (effectTextTarget != null)
            {
                Text txt = effectTextTarget.GetComponent<Text>();
                if (txt != null)
                {
                    StopAllCoroutines(); // 혹시 이전 효과가 남아있다면 멈춤
                    StartCoroutine(FadeTextColor(txt, Color.black, Color.white, 2.0f));
                }
            }
        }
        else
        {
            Debug.Log("강화 실패 (유지)");

            // 🔴 강화 실패 시 텍스트 색상 변경 효과 실행
            if (effectTextTarget != null)
            {
                Text txt = effectTextTarget.GetComponent<Text>();
                if (txt != null)
                {
                    StopAllCoroutines(); // 혹시 이전 효과가 남아있다면 멈춤
                    StartCoroutine(FadeTextColor(txt, Color.red, Color.white, 0.5f));
                }
            }
        }
    }

    // 🔹 텍스트 색상을 서서히 바꾸는 코루틴
    private IEnumerator FadeTextColor(Text text, Color startColor, Color endColor, float duration)
    {
        text.color = startColor;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        text.color = endColor;
    }
}
