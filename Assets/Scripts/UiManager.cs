using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] Button _gridButton;
    [SerializeField] Button _shopButton;
    [SerializeField] GameObject _shop;
    bool _shopState = false;
    public Button shopButton
    {
        get
        {
            return _shopButton;
        }
    }

    public Button gridButton
    {
        get
        {
            return _gridButton;
        }
    }

    private void Start()
    {
        _shopState = _shop.activeSelf;
        _shopButton.onClick.AddListener(SetShopWindow);

    }

    public void SetShopWindow()
    {
        _shopState = !_shopState;
        _shop.SetActive(_shopState);
    }
}
