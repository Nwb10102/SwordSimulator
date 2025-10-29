using System;
using UnityEngine;

public class ToggleUi : MonoBehaviour
{
    public GameObject targetUi;
    Boolean isUiActive = false;

    public void Start()
    {
        targetUi.SetActive(false);
    }

    // Update is called once per frame
    public void OnoffUi()
    {
        if (isUiActive)
        {
            targetUi.SetActive(true);
            isUiActive = false;
        }
        else
        {
            targetUi.SetActive(false);
            isUiActive = true;
        }
    }
}
