using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    
    public List<Button> btns = new List<Button>();

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    private bool firstGuess, secondGuess;

    private int countGuesses;

    private int countCorrectGuesses;

    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    public TextMeshProUGUI score;

    public int scoreCount;

    private readonly int minimalAcceptableScore = -10;

    


    void Awake(){
        puzzles = Resources.LoadAll<Sprite>("Sprites/Cards");
    }

    void Start()
    {
        SetDefaultText();
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void SetDefaultText(){
        score.text = $"Score: 0" ;
    }

    void EndGameScreen(){
        SceneManager.LoadScene("EndGame");
    }

    public void MainScreen(){
        SceneManager.LoadScene("Menu");
    }
 
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles(){
        int numberOfPuzzles = btns.Count;
        int index = 0;

        for (int i = 0; i< numberOfPuzzles; i++){
            if (index == numberOfPuzzles / 2){
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index ++; 
        }
    }
    void AddListeners(){
        foreach(Button btn in btns){
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle(){
    
        if (!firstGuess){
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

            btns[firstGuessIndex].enabled = false;

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
        }
        else if (!secondGuess){
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            btns[secondGuessIndex].enabled = false;

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            countGuesses++;
            StartCoroutine(CheckIfPuzzlesMatch());
        }
    }
    IEnumerator CheckIfPuzzlesMatch(){
        yield return new WaitForSeconds(.5f);

        if (firstGuessPuzzle == secondGuessPuzzle){
            scoreCount += 10;
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.enabled = false;
            btns[secondGuessIndex].image.enabled = false;
            CheckIfGameIsFinished();
        }
        else{
            scoreCount -= 2;
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].enabled = true;
            btns[secondGuessIndex].enabled = true;
            btns[firstGuessIndex].image.sprite = bgImage; 
            btns[secondGuessIndex].image.sprite = bgImage; 
        }

        
        score.text = $"Score: {scoreCount}" ;
        firstGuess = secondGuess = false;
        if(scoreCount <= minimalAcceptableScore){
            for(int i = 0 ; i < btns.Count;i++){
                btns[i].interactable = false;
                btns[i].image.enabled = false;
               
            }
            StateNameController.hasUserWon = false;
            StateNameController.userTotalScore = scoreCount;
            EndGameScreen();
        }
        

    }

    void CheckIfGameIsFinished(){
        countCorrectGuesses ++;;

        if(countCorrectGuesses == gameGuesses){
            StateNameController.hasUserWon = true;
            StateNameController.userTotalScore = scoreCount;

            EndGameScreen();
        }
    }

    void Shuffle(List<Sprite> list) {
        for(int i = 0 ; i < list.Count ; i++){
            Sprite temp = list[i];
            int randomIndex = Random.Range(0,list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;

        }


        
    }
}
