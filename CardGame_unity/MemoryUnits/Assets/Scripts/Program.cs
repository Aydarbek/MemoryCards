using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CardLibrary;
public class Program : MonoBehaviour, IPlayable {
    LogicMemory logic;
	// Use this for initialization
	void Start ()
    {
        logic = new LogicMemory(this);
        logic.CreateNewGame();   
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void NewGame()
    {
        for (int i = 0; i <= 15; i++)
        {
            HideCard(i);
        }
        logic.CreateNewGame();
        logic.Counter = 0;
        GameObject Score = GameObject.Find("Score");
        GameObject Winner = GameObject.Find("Winner");
        Score.GetComponent<Text>().text = "Score:\n0";
        Winner.GetComponent<Text>().text = "";

    }

    public void OnClick()
    {
        GameObject Go = EventSystem.current.currentSelectedGameObject;
        int nr = int.Parse(Go.name.Replace("Button", ""));
        logic.ClickPicture(nr);
        GameObject Score = GameObject.Find("Score");
        Score.GetComponent<Text>().text = "Score:\n"  + logic.Counter.ToString();

    }

    public void ShowCard(int nr, int card)
    {
        GameObject button = GameObject.Find("Button" + nr);
        GameObject picture = GameObject.Find("" + card);
        button.GetComponent<Image>().sprite = picture.GetComponent<SpriteRenderer>().sprite;
    }

    public void HideCard (int nr)
    {
        ShowCard(nr, 0);
    }

    public void ShowWinner()
    {
        GameObject Winner = GameObject.Find("Winner");
        GameObject Best = GameObject.Find("BestScore");
        if (logic.Counter < logic.BestScore)
        {
            logic.BestScore = logic.Counter;
            Winner.GetComponent<Text>().text = "You are winner! New best score!";
        }
        else
        {
            Winner.GetComponent<Text>().text = "You are winner!";
        }
        Best.GetComponent<Text>().text = "Best:\n" + logic.BestScore;


    }


}
