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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Updating");
            
        }
    }

    


    
}
