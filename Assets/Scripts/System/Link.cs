using UnityEngine;

public class Link : MonoBehaviour
{
    public string URL;
    public void OnClick_OpenURL(){
        Application.OpenURL(URL);
    }
}
