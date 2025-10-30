using UnityEngine;
using TMPro;

public class PercentUi : MonoBehaviour
{
    [SerializeField] private CSVLoader csvLoader;

    public GameObject TEXT_upgradePercent;
    public GameObject TEXT_destroyPercent;
    public GameObject TEXT_price;
    public GameObject TEXT_upgradeRequiredCoins;

    private void Update()
    {
        int swordID = GameManager.Instance != null ? GameManager.Instance.currentLevel : 0;

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

        TEXT_upgradePercent.GetComponent<TextMeshProUGUI>().text = $"강화 확률: {sword.UpgradeChance}%";
        TEXT_destroyPercent.GetComponent<TextMeshProUGUI>().text = $"파괴 확률: {sword.DestroyChance}%";
        TEXT_price.GetComponent<TextMeshProUGUI>().text = $"가격: {sword.Price}코인";
        TEXT_upgradeRequiredCoins.GetComponent<TextMeshProUGUI>().text = $"강화에 필요한 코인: {sword.RequiredCoins}코인";
    }
}
