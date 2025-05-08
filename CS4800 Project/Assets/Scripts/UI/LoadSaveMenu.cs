using UnityEngine;
using UnityEngine.UI;

public class LoadSaveMenu : MonoBehaviour
{
    private int _selectedSlot;
    [SerializeField] private GameObject[] _saveSlots;

    [Header("Buttons")]
    [SerializeField] private GameObject _saveButton;
    [SerializeField] private GameObject _loadButton;
    [SerializeField] private GameObject _deleteButton;

    // Should populate the menu with saves
    private void OnEnable()
    {
        // Make sure the buttons are not activated
        _saveButton.SetActive(false);
        _loadButton.SetActive(false);
        _deleteButton.SetActive(false);

        UpdateSaveSlots();
    }

    // Update save slots text
    private void UpdateSaveSlots()
    {
        for (int i = 0; i < _saveSlots.Length; i++)
        {
            // Get text components of slot

            // If the save does not exist, then fill the texts with - in every field
            // If the save does exist, then populate the text fields with the right info
        }
    }

    // Select save slot
    public void SelectSlot(int slot)
    {
        if (_selectedSlot == slot) {
            return;
        }

        _selectedSlot = slot;
    }

    // BUTTONS
    // On-click behavior of the save button - save to selected slot
    public void SaveSlot()
    {
        // Save to selected slot

        UpdateSaveSlots();
    }

    // On-click behavior of the delete button - delete the selected slot
    public void DeleteButton()
    {
        // Return if the selected slot does not have a save
        
        // Delete the save

        UpdateSaveSlots();
    }

    // On-click behavior of the load button - load the selected slot
    public void LoadButton()
    {
        // Return if the selected slot does not have a save

        // Load the save

        UpdateSaveSlots();
    }
}
