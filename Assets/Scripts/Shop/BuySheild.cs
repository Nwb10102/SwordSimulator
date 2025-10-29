using UnityEngine;

public class BuySheild : MonoBehaviour
{
    public int price;
    public int shieldAmount = 1;

    public void BuyShield()
    {
        if (GameManager.Instance.playerCoins >= price)
        {
            GameManager.Instance.playerCoins -= price;
            GameManager.Instance.failProtection += shieldAmount;
            Debug.Log($"방패 구매 완료! 남은 코인: {GameManager.Instance.playerCoins}, 방패 수량: {GameManager.Instance.failProtection}");
        }
        else
        {
            Debug.Log("코인이 부족합니다. 방패를 구매할 수 없습니다.");
        }
    }
}
