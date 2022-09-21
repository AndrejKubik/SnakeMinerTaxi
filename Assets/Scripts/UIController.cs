using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Singleton
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    public static GameObject UpButton;
    public static GameObject DownButton;
    public static GameObject LeftButton;
    public static GameObject RightButton;

    private void Start()
    {
        UpButton = upButton;
        DownButton = downButton;
        LeftButton = leftButton;
        RightButton = rightButton;
    }

    public void HideButtons()
    {
        UpButton.SetActive(false);
        DownButton.SetActive(false);
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
    }
}
