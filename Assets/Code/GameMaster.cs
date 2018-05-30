using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
	[Header("Title UI")]
	public UnityEngine.UI.Text Title_Title;
	public UnityEngine.UI.Text Title_Highscore;
	public UnityEngine.UI.Text Title_Instructions;

	[Header("Death UI")]
	public UnityEngine.UI.Text Death_Title;
	public UnityEngine.UI.Text Death_Score;
	public UnityEngine.UI.Text Death_Highscore;
	public UnityEngine.UI.Text Death_Instructions;

	[Header("Game UI")]
	public UnityEngine.UI.Text Game_Score;

	[Header("Game Vars")]
	public GameObject Platform;
	public GameObject Ball;
	public string Game_State = "Title";
	int Score = 0;
	float Delay = 2.2f;
	float Start_Time;

	// Use this for initialization
	void Start () {
		Title_Highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
		GameObject.Find("Platform").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
	}

	// Update is called once per frame
	void Update () {
		Game_Score.text = "Score: " + Score;
		if (Input.GetMouseButtonDown(0) && Game_State != "Game") {
			Game_State = "Game";
			StopAllCoroutines();
			StartCoroutine(Game_Start());
			if (Game_State == "Death") {
				Instantiate(Platform, new Vector3(0, -1.468625f, 0), new Quaternion(0, 0, 0, 0), null);
			}else {
				GameObject.Find("Platform").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			}
			Balls();
		}else if (Game_State == "Game") {
			Score = Mathf.FloorToInt((Time.realtimeSinceStartup - Start_Time) / 2);
		}
		if (Game_State == "End") {
			StopAllCoroutines();
			StartCoroutine(Game_End());
			Game_State = "Death";
		}
	}

	void Balls () { //Crashes app???
		/*while (Game_State == "Game") {
			if (Score > 30) {
				int i = Random.Range(Score, 100000);
				if (i == Score) {
					Instantiate(Ball, new Vector3(Random.Range(-2.5f, 2.5f), 6, 0), new Quaternion(0, 0, 0, 0), null);
				}
			}
		}*/
	}

	IEnumerator Game_Start () {
		StartCoroutine(Fade(Title_Title, Delay * -1));
		StartCoroutine(Fade(Title_Highscore, Delay * -1));
		StartCoroutine(Fade(Title_Instructions, Delay * -1));
		StartCoroutine(Fade(Death_Title, Delay * -1));
		StartCoroutine(Fade(Death_Score, Delay * -1));
		StartCoroutine(Fade(Death_Highscore, Delay * -1));
		StartCoroutine(Fade(Death_Instructions, Delay * -1));
		Start_Time = Time.realtimeSinceStartup;
		yield return new WaitForSecondsRealtime(2.123f);
		StartCoroutine(Fade(Game_Score, Delay * 1));
		yield break;
	}

	IEnumerator Game_End () {
		Debug.Log("Test test");
		if (Score > PlayerPrefs.GetInt("Highscore")) {
			PlayerPrefs.SetInt("Highscore", Score);
		}
		StartCoroutine(Fade(Death_Title, Delay * 1));
		StartCoroutine(Fade(Death_Score, Delay * 1));
		StartCoroutine(Fade(Death_Highscore, Delay * 1));
		StartCoroutine(Fade(Game_Score, Delay * -1));
		yield return new WaitForSecondsRealtime(2.123f);
		Death_Score.text = "Score: " + Score;
		Death_Highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
		StartCoroutine(Fade(Death_Instructions, Delay * 1));
		yield break;
	}

	IEnumerator Fade (UnityEngine.UI.Text Text, float Type) { //If type is positive then fade in. Vice versa is vice versa.
		for (int i = 255; i > 0; i += Mathf.RoundToInt(Type)) {
			Text.color = new Color(Text.color.r, Text.color.b, Text.color.b, Text.color.a + Type/255);
			yield return new WaitForSecondsRealtime(.01f);
		}
		Text.color = new Color(Text.color.r, Text.color.b, Text.color.b, Type/Delay);
		yield return null;
	}
}