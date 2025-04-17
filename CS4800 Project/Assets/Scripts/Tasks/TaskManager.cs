using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Singleton
    public static TaskManager instance;

    [Header("References")]
    [SerializeField] private GameObject _taskContainer;
    [SerializeField] private GameObject _taskPrefab;

    // Task list
    private List<Task> _taskList = new List<Task>();

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        AddTask("Get tools!", TaskTypes.GetTools, 4);
    }

    public void AddTask(string description, TaskTypes type, int goalNumber)
    {
        // Instantiate the task with its references
        Task task = new Task();
        task.go = Instantiate(_taskPrefab, _taskContainer.transform);
        task.taskText = task.go.GetComponentInChildren<TextMeshProUGUI>();

        // Assign the components of the task
        task.taskDescription = description;
        task.taskType = type;
        task.taskGoalNumber = goalNumber;

        // Add task to list
        _taskList.Add(task);

        // Update task to reflect the assigned components
        task.UpdateTask();
    }


    public void FinishTask(Task task)
    {
        
    }

    public void UpdateTask(TaskTypes type)
    {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
                task.IncrementTask();
        }
    }
}
