using UnityEngine;
using TMPro;

public class Top3Best : MonoBehaviour
{
    public Score scoreScript;
    public TextMeshProUGUI bestScoresText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Savescore()
    {
        int currentscore = scoreScript.score;

        Debug.Log("Saving score: " + currentscore);



        int best1 = PlayerPrefs.GetInt("BestScore1", 0);
        int best2 = PlayerPrefs.GetInt("BestScore2", 0);
        int best3 = PlayerPrefs.GetInt("BestScore3", 0);

        if (currentscore > best1)
        {
            best3 = best2;
            best2 = best1;
            best1 = currentscore;
        }
        else if (currentscore > best2)
        {
            best3 = best2;
            best2 = currentscore;
            
        }
        else if(currentscore > best3)
        {
            best3 = currentscore;
        }
        
        PlayerPrefs.SetInt("BestScore1", best1);
        PlayerPrefs.SetInt("BestScore2", best2);
        PlayerPrefs.SetInt("BestScore3", best3);

        PlayerPrefs.Save();

        UpdateDisplay();
    }


    void UpdateDisplay()
    {
        int best1 = PlayerPrefs.GetInt("BestScore1", 0);
        int best2 = PlayerPrefs.GetInt("BestScore2", 0);
        int best3 = PlayerPrefs.GetInt("BestScore3", 0);


        bestScoresText.text = "Best Runs: \n" + "1. " + best1 + "\n" + "2. " + best2 + "\n" +  "3. " + best3;

        // if (best1 != best1 || best2 != best2 || best3 != best3){

        //     bestScoresText.text = "Best Runs: \n " + "1. " + best1 + "\n" + "2. " + best2 + "\n" +  "3. " + best3;
        // }

    }
}

