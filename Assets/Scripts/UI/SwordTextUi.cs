using UnityEngine;
using TMPro;
using System;

public class SwordTextUi : MonoBehaviour
{
    [SerializeField] private CSVLoader csvLoader;
    public TextMeshProUGUI swordText;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            int currentLevel = GameManager.Instance.currentLevel;
            string swordName = csvLoader.swordDataList.Find(x => x.ID == currentLevel)?.Name ?? "Unknown";
            
            swordText.text = currentLevel + " - " + swordName;
        }
    }
}
