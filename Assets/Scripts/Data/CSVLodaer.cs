using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    public TextAsset csvFile; // 유니티 인스펙터에서 CSV 파일 연결
    public List<SwordData> swordDataList = new List<SwordData>();

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string[] data = csvFile.text.Split(new char[] { '\n' });

        // 첫 줄은 헤더이므로 i = 1부터
        for (int i = 1; i < data.Length; i++)
        {
            string line = data[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] row = line.Split(',');

            SwordData sword = new SwordData();
            sword.ID = int.Parse(row[0]);
            sword.Name = row[1];
            sword.UpgradeChance = float.Parse(row[2]);
            sword.DestroyChance = float.Parse(row[3]);
            sword.RequiredCoins = int.Parse(row[4]);
            // CSV 마지막 열(인덱스 5)에 Price(구매/판매 가격)가 있음
            if (row.Length > 5)
            {
                int price = 0;
                int.TryParse(row[5], out price);
                sword.Price = price;
            }

            swordDataList.Add(sword);
        }

        Debug.Log($"검 데이터 {swordDataList.Count}개 로드 완료!");
    }
}
