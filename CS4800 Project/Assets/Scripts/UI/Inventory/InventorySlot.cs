using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// A gameobject that handles an inventory slot of an inventory.
public class InventorySlot : MonoBehaviour
{
    // Components
    private Item _item;
    private Image _image;
    private TextMeshProUGUI _itemText;
    private Image _itemSprite;

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
        _itemSprite = transform.GetChild(0).GetComponent<Image>();
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

    // Sets the item and assigns sprite and text for the hotbar slot. 
    public void SetItem(Item item)
    {
        _item = item;

        if (_item == null)
        {
            // Set text
            _itemText.text = "";

            // Set sprite
            _itemSprite.sprite = null;
            _itemSprite.gameObject.SetActive(false);
        }
        else
        {
            // Set text
            _itemText.text = item.itemName;

            // Set sprite (if there is a sprite)
            _itemSprite.gameObject.SetActive(item.sprite != null);
            _itemSprite.sprite = item.sprite;
        }
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
