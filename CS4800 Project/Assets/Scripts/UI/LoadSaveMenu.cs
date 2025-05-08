using TMPro;
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

    private SaveHandler _saveHandler;

    // Should populate the menu with saves
    private void OnEnable()
    {
        // Make sure the buttons are not activated
        _saveButton.SetActive(false);
        _loadButton.SetActive(false);
        _deleteButton.SetActive(false);

        // Create new save handler
        _saveHandler = new SaveHandler();

        UpdateSaveSlots();
    }

    // Update save slots text
    private void UpdateSaveSlots()
    {
        for (int i = 0; i < _saveSlots.Length; i++)
        {
            // Get text components of slot (make sure to check the texts are in this format)
            TextMeshProUGUI slotNumberText = _saveSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI levelNumberText = _saveSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI tasksText = _saveSlots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timeText = _saveSlots[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            // Fill in slot number level text
            slotNumberText.text = $"{i + 1}";

            // Get save data for each slot
            SaveData save = _saveHandler.GetSaveData(i + 1);

            // If the save does not exist, then fill the texts with - in every field
            if (save == null || save.username == "")
            {
                levelNumberText.text = "NONE";
                tasksText.text = "-";
                timeText.text = "-";
            }
            // If the save does exist, then populate the text fields with the right info
            else
            {
                levelNumberText.text = $"{save.levelNumber}";
                tasksText.text = $"{save.completedTasks} / {save.numberOfTasks}";
                timeText.text = $"{save.gameTime}";
            }
        }
    }

    // Select save slot
    public void SelectSlot(int slot)
    {
        _selectedSlot = slot;

        // Disables save button if the scene is in the main menu
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
            _saveButton.SetActive(false);
        // Enables save button if scene is a level scene
        else
            _saveButton.SetActive(true);

        // Disables load and delete button if save does not exist
        SaveData save = _saveHandler.GetSaveData(_selectedSlot);
        if (save == null || save.username == "")
        {
            _loadButton.SetActive(false);
            _deleteButton.SetActive(false);
        }
        // Enables load and delete button if save does exist
        else
        {
            _loadButton.SetActive(true);
            _deleteButton.SetActive(true);
        }
    }

    // BUTTONS
    // On-click behavior of the save button - save to selected slot
    public void SaveButton()
    {
        _saveHandler.WriteSaveData(_selectedSlot);

        // Update the UI
        UpdateSaveSlots();
        SelectSlot(_selectedSlot);
    }

    // On-click behavior of the delete button - delete the selected slot
    public void DeleteButton()
    {
        // Return if the selected slot does not have a save
        SaveData save = _saveHandler.GetSaveData(_selectedSlot);
        if (save == null || save.username == "")
            return;

        // Delete save
        _saveHandler.DeleteSaveData(_selectedSlot);

        // Update the UI
        UpdateSaveSlots();
        SelectSlot(_selectedSlot);
    }

    // On-click behavior of the load button - load the selected slot
    public void LoadButton()
    {
        // Return if the selected slot does not have a save
        SaveData save = _saveHandler.GetSaveData(_selectedSlot);
        if (save == null || save.username == "")
            return;

        // TODO: Load the save
    }
}
