using TMPro;
using UnityEngine;
using UnityEngine.UI;

// A gameobject that handles an inventory slot of an inventory.
public class InventorySlot : MonoBehaviour
{
    private Item _item;
    private Image _image;
    private TextMeshProUGUI _itemText;

    // Selection alpha colors
    private const float _COLOR_ALPHA_SELECTED = 1f;
    private const float _COLOR_ALPHA_DESELECTED = 0.40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (GetComponentInParent<InventoryManager>() == null)
            Debug.LogError($"{this} is not under a parent InventoryManager!");

        _image = GetComponent<Image>();
        _itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Select()
    {
        ChangeImageAlpha(_COLOR_ALPHA_SELECTED);
    }

    public void Deselect()
    {
        ChangeImageAlpha(_COLOR_ALPHA_DESELECTED);
    }

    public void SetItem(Item item)
    {
        _item = item;

        if (_item == null)
            _itemText.text = "";
        else
            _itemText.text = item.itemName;
    }

    public Item GetItem()
    {
        return _item;
    }

    private void ChangeImageAlpha(float newAlpha)
    {
        Color newColor = _image.color;
        newColor.a = newAlpha;

        _image.color = newColor;
    }
}
