using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void LoadGame(int input){
        StateNameController.numberOfPuzzles = input;
        SceneManager.LoadScene("SampleScene");
    }
}
