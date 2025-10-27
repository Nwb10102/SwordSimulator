using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 인스턴스

    public int playerCoins = 0;

    void Awake()
    {
        // 다른 GameManager가 이미 있으면 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동시 유지
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log("현재 코인: " + playerCoins);
    }
}
