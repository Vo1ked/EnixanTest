using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    public SpawnObject item;
    public TextMeshProUGUI nameText;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.Instance.manager.placeHolder.SpawnItem(item);
        GameController.Instance.manager.uiManager.SetShopWindow();
    }

    void Start()
    {
        nameText.text = item.name;
    }



}
