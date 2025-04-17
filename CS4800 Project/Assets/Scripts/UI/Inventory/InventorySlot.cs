using TMPro;
using UnityEngine;
using UnityEngine.UI;

// A gameobject that handles an inventory slot of an inventory.
public class InventorySlot : MonoBehaviour
{
    // Components
    private Item _item;
    private Image _image;
    private TextMeshProUGUI _itemText;

    // Selection alpha colors
    private const float _COLOR_ALPHA_SELECTED = 1f;
    private const float _COLOR_ALPHA_DESELECTED = 0.40f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Checks if the object is under an inventory manager.
        if (GetComponentInParent<InventoryManager>() == null)
            Debug.LogError($"{this} is not under a parent InventoryManager!");

        // Get components.
        _image = GetComponent<Image>();
        _itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Selects the slot.
    public void Select()
    {
        ChangeImageAlpha(_COLOR_ALPHA_SELECTED);
    }

    // Deselects the slot.
    public void Deselect()
    {
        ChangeImageAlpha(_COLOR_ALPHA_DESELECTED);
    }

    // Setter for item.
    public void SetItem(Item item)
    {
        _item = item;

        if (_item == null)
            _itemText.text = "";
        else
            _itemText.text = item.itemName;
    }

    // Getter for item.
    public Item GetItem()
    {
        return _item;
    }

    // Changes the hotbar sprite's alpha.
    // Used for selecting and deselecting hotbar slots.
    private void ChangeImageAlpha(float newAlpha)
    {
        Color newColor = _image.color;
        newColor.a = newAlpha;

        _image.color = newColor;
    }
}
