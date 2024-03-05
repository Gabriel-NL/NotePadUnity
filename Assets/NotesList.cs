using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NotesList : MonoBehaviour
{
    private string noteStorage = "SavedNotes";
    public int numberOfElements;
    public GameObject uiElementPrefab;
    public ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        CountTextFilesInFolder(noteStorage);
        PopulateScrollRect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Pressed p");
            CreateFile();
            CountTextFilesInFolder(noteStorage);
            PopulateScrollRect();
        }
    }

    void CreateFile()
    {
        string baseFileName = "test.txt";
        string filePath = Path.Combine(Application.dataPath, noteStorage, baseFileName);

        int fileIndex = 1;

        while (File.Exists(filePath))
        {
            // File already exists, modify the filename
            baseFileName = $"test({fileIndex}).txt";
            filePath = Path.Combine(Application.dataPath, noteStorage, baseFileName);
            fileIndex++;
        }

        // Create and write some content to the file
        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.WriteLine("Hello, this is a test file!");
            // You can add more content if needed
        }

        Debug.Log("File created at: " + filePath);
    }


    void CountTextFilesInFolder(string folderName)
    {
        string folderPath = Path.Combine(Application.dataPath, folderName);

        // Check if the folder exists
        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning("Folder not found: " + folderName);
            return;
        }

        // Get all text files in the folder
        string[] textFiles = Directory.GetFiles(folderPath, "*.txt");
        numberOfElements = textFiles.Length;
    }
    void PopulateScrollRect()
    {
        RectTransform contentTransform = scrollRect.content;

        for (int i = 0; i < numberOfElements; i++)
        {
            // Instantiate the UI element prefab
            GameObject uiElement = Instantiate(uiElementPrefab, contentTransform);

            // Set the position of the instantiated UI element
            RectTransform uiElementTransform = uiElement.GetComponent<RectTransform>();
            uiElementTransform.anchoredPosition = new Vector2(0f, -100f - i *200 );  // Adjust the spacing as needed

            // You can customize or set data for each instantiated UI element here
            // Example: uiElement.GetComponentInChildren<Text>().text = "Element " + i;
        }

        
    }
}
