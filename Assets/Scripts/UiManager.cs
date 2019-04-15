using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] Button _gridButton;
    [SerializeField] Button _shopButton;

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

}
