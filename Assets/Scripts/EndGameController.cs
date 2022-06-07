using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class EndGameController : MonoBehaviour
{
    // Start is called before the first frame update
   public TextMeshProUGUI resultText;

   public TextMeshProUGUI scoreText;


    void Start()
    {
        DisplayResultText();
    }

    public void MainScreen(){
        SceneManager.LoadScene("Menu");
    }

    void DisplayResultText(){

        if (StateNameController.hasUserWon){
            resultText.color = new Color32(0,255,0,255);
            resultText.text = "Congratulations, you have won !!";
    }
        else {
            resultText.color = new Color32(255,0,100,255);
            resultText.text = "You lost :( Better luck next time !!";
        }
        scoreText.color = new Color32(255,255,255,255);
        scoreText.text = $"Your score: {StateNameController.userTotalScore}";
    }
}
