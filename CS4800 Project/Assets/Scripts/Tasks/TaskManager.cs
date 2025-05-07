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
    [SerializeField] private GameObject _finishLevelMenu;

    // Task list
    private List<Task> _taskList = new List<Task>();
    int tracker = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void AddTask(string description, TaskTypes type, int goalNumber)
    {
        // Initialize the task
        Task task = new Task();
        GameObject go = Instantiate(_taskPrefab, _taskContainer.transform);
        task.InitializeTask(description, type, goalNumber, go);

        // Add task to list
        _taskList.Add(task);

        // Update task to reflect the assigned components
        task.UpdateTask();
    }

    // Remove task
    public void RemoveTask(Task task)
    {
        _taskList.Remove(task);
        Destroy(task.go);
    }

    // Removes all tasks
    public void Clear()
    {
        foreach (Task task in _taskList)
        {
            RemoveTask(task);
        }
    }

    // Triggers whenever a task parameter gets called and increments tasks that are under that
    public virtual void IncrementTask(TaskTypes type)
    {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
                task.IncrementTask();
        }

        if (IsLevelComplete())
            CompleteLevel();
    }

    public bool FinishedTask(TaskTypes type) {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
                return task.IsFinished();
        }
        return false;
    }

    // Checks if the level is completed (checking if all tasks are completed)
    public bool IsLevelComplete()
    {
        foreach (Task task in _taskList)
        {
            if (!task.IsFinished())
                return false;
        }

        return true;
    }

    // Increments the failure of a task
    public void FailedTask(TaskTypes type) 
    {
        
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
                task.IncrementFailure();
        }
    }

    // Displays amount of failure for a task
    public void DisplayFail(TaskTypes type)
    {
        foreach (Task task in _taskList)
        {
            if (task.taskType == type)
                tracker = task.taskfail;
                Debug.Log($"Amount of fails: " + tracker);
        }
    }

    // Completes the level
    private void CompleteLevel()
    {
        _finishLevelMenu.SetActive(true);
        Debug.Log("Completed level");
    }
}

