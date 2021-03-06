using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Transform instructionBox;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void OpenSite()
    {
        Application.OpenURL("https://www.lukebriggs.dev");
    }

    public void ToggleInstructions()
    {
        instructionBox.gameObject.SetActive(!instructionBox.gameObject.activeSelf);
    }
}
