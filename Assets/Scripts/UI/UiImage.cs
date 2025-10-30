using UnityEngine;
using UnityEngine.UI;

public class UiImage : MonoBehaviour
{
    public Image targetImage;

    private int lastId = -1;

    private void Update()
    {
        int id = GameManager.Instance != null ? GameManager.Instance.currentLevel : 0;

        if (targetImage == null) return;

        if (id != lastId)
        {
            lastId = id;

            string folderPath = $"Sword/Id_{id}";
            string spriteName = $"Id_{id}_0";

            Debug.Log($"[UiImage] Trying to load from folder: {folderPath}, sprite name: {spriteName}");

            // 폴더 안 모든 스프라이트 불러오기
            Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);

            if (sprites.Length == 0)
            {
                Debug.LogWarning($"[UiImage] ❌ No sprites found in folder: {folderPath}");
                return;
            }

            // 해당 이름의 스프라이트 찾기
            Sprite found = null;
            foreach (var s in sprites)
            {
                if (s.name == spriteName)
                {
                    found = s;
                    break;
                }
            }

            if (found != null)
            {
                targetImage.sprite = found;
                Debug.Log($"[UiImage] ✅ Loaded successfully: {found.name}");
            }
            else
            {
                Debug.LogWarning($"[UiImage] ⚠️ Folder found, but sprite name not matched: {spriteName}");
            }
        }
    }
}
