using UnityEngine;

public class SellScript : MonoBehaviour
{
	[SerializeField] private CSVLoader csvLoader;

	// 버튼에서 호출하세요 (Inspector -> OnClick)
	public void SellCurrentSword()
	{
		if (GameManager.Instance == null)
		{
			Debug.LogWarning("GameManager 인스턴스가 없습니다. 판매할 수 없습니다.");
			return;
		}

		if (csvLoader == null)
		{
			Debug.LogWarning("CSVLoader가 할당되어 있지 않습니다. 판매 가격을 읽을 수 없습니다.");
			return;
		}

		int currentID = GameManager.Instance.currentLevel;

		SwordData sword = csvLoader.swordDataList.Find(x => x.ID == currentID);
		if (sword == null)
		{
			Debug.LogWarning($"현재 레벨({currentID})에 해당하는 검 데이터를 찾을 수 없습니다.");
			return;
		}

		int sellPrice = sword.Price; // CSV의 Price 필드를 판매가로 사용

		// 코인 추가
		GameManager.Instance.AddCoins(sellPrice);

		// currentLevel을 0으로 리셋
		GameManager.Instance.currentLevel = 0;

		Debug.Log($"{sword.Name} (ID: {sword.ID}) 판매 완료 - 획득 코인: {sellPrice}. currentLevel -> 0");
	}
}