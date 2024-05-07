using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class AddNewNote : MonoBehaviour
{
    
    private string noteStorage = "SavedNotes";
    public int numberOfElements;
    public GameObject uiElementPrefab;
    public ScrollRect scrollRect;

    private void Start()
    {
        CountTextFilesInFolder(noteStorage);
        PopulateScrollRect();
    }



    void Update()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Perform the raycast
                if (Physics.Raycast(ray, out hit))
                {
                // Check if the collider hit is the one we want
                    if (hit.collider == GetComponent<Collider>())
                    {
                        CreateFile();
                        CountTextFilesInFolder(noteStorage);
                        PopulateScrollRect();
                    }
                }
            }
        }
 
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

            // Get the file name from the file path
            string fileName = Path.GetFileNameWithoutExtension(textFiles[i]);

            // Set the position of the instantiated UI element
            RectTransform uiElementTransform = uiElement.GetComponent<RectTransform>();
            uiElementTransform.anchoredPosition = new Vector2(0f, -100f - i * 220);  // Adjust the spacing as needed

            // You can customize or set data for each instantiated UI element here
            // Example: uiElement.GetComponentInChildren<Text>().text = "Element " + i;
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
}
