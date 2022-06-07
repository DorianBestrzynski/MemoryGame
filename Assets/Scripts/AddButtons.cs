using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject button;

    public GameObject puzzleF;

    // Start is called before the first frame update
    void Awake()
    {
        int numOfPuzzles = StateNameController.numberOfPuzzles;
        var getGridLayout = puzzleF.GetComponent<GridLayoutGroup>();
        Debug.Log(getGridLayout);
        getGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        if (numOfPuzzles == 4) {
            getGridLayout.constraintCount = 2;
        }
         else {
             getGridLayout.constraintCount = 4;
         }
  
        for(int i = 0 ; i < numOfPuzzles ; i++){
            GameObject btn = Instantiate(button);
            btn.name = "" + i;
            btn.transform.SetParent(puzzleField, false);
        }
    }
}
