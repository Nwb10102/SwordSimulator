using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerCoins = 0;
    public int failProtection = 0;
    public int currentLevel = 0;

    [Space]
    [Header("Text UI Elements")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI failProtectionText;
    public TextMeshProUGUI swordText;

    void Awake()
    {
        // 다른 GameManager가 이미 있으면 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UseFailProtection()
    {
        if (failProtection > 0)
        {
            failProtection--;
            Debug.Log("실패 보호 사용! 남은 보호 횟수: " + failProtection);
        }
        else
        {
            Debug.Log("실패 보호가 없습니다!");
        }
    }
    
    public void AddFailProtection(int amount)
    {
        failProtection += amount;
        Debug.Log("실패 보호 추가! 현재 보호 횟수: " + failProtection);
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log("현재 코인: " + playerCoins);
    }

    void Update()
    {
        coinText.text = "Coins : " + playerCoins;
        failProtectionText.text = "Fail Protection : " + failProtection;
    }
}
