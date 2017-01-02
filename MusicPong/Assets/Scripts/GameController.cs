using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using TouchScript;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{



	//References to needed Unity Objects


	public GameObject musicNote;
	public GameObject mViolin;
	public GameObject mGuitar;
	public GameObject mPiano;
	public GameObject mTrumpet;
	public GameObject mSax;

	//Variables for calculations and management
	private float vertMiddle;
	private float rightEdge;
	private List<GameObject> mNotes;
	private List<GameObject> mInstruments;
	private List<GameObject> toDelete;

	//Variables to control speeds (difficulty) of game
	public float noteSpeed;
	public float timeToWait;


	private Dictionary<GameObject,System.DateTime> mFadeOut;
	public float fadeDegree;


	PressGesture pressG;
	LongPressGesture lPressG;
	private bool isPressing = false;
	private Vector2 pressPos;


	/*Unity handler Methods
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * */
	void Start ()
	{
		
		GetVertMiddle ();
		GetRightEdge ();

		mNotes = new List<GameObject> ();
		mInstruments = new List<GameObject> ();
		toDelete = new List<GameObject> ();

	
		mFadeOut = new Dictionary<GameObject,System.DateTime> ();
	}

	private void OnEnable ()
	{

		pressG = GetComponent<PressGesture> ();
		pressG.Pressed += PressHandler;

		lPressG = GetComponent<LongPressGesture> ();
		lPressG.LongPressed += LongPressHandler;



	}


	private void OnDisable ()
	{

		pressG.Pressed -= PressHandler;
		lPressG.LongPressed -= LongPressHandler;


	}


	void FixedUpdate ()
	{

		StartCoroutine (PressKoordinator ());
//		PressKoord();
		StartCoroutine (NoteMoverCoR ());
		StartCoroutine (FaderCor ());
		GOCleaner ();
	}




	/*
	 * Methods for calculations and gameobject managment.
	 * 
	 * 
	 * 
	 * 
	 * 
	 * */
	private void GetVertMiddle ()
	{

		var temp = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0f));
		vertMiddle = temp.x;
	}

	private void GetRightEdge ()
	{
		var temp = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0.5f, 0f));
		rightEdge = temp.x;
	}





	private void GOCleaner ()
	{
		toDelete = new List<GameObject> ();
		if (mNotes.Count > 0) {
			foreach (GameObject go in mNotes) {
				if (go != null) {
					if (go.transform.position.x >= rightEdge) {
						toDelete.Add (go);
					}
			
				}
			}
		}



		if (toDelete.Count > 0) {
			foreach (GameObject go in toDelete) {
				if (go != null) {
					if (go.tag.Equals ("MusicNote")) {
						mNotes.Remove (go);
						Destroy (go);
					} 

				}
			}
		}


	}




	private bool IsInLeft (Vector2 v2)
	{
		float vm = vertMiddle;
		if (v2.x < vm)
			return true;
		else
			return false;
	}

	private bool IsInRight (Vector2 v2)
	{

		float vm = vertMiddle;
		if (v2.x > vm)
			return true;
		else
			return false;
	}


	IEnumerator NoteMoverCoR ()
	{
		if (mNotes.Count > 0) {


			foreach (GameObject go in mNotes) {
				if (go != null) {

					Vector2 v2 = new Vector2 (go.transform.position.x + noteSpeed * Time.deltaTime, go.transform.position.y);
					go.transform.position = v2;
				}
			}
		}
		yield return null;
	}



	IEnumerator FaderCor ()
	{
		if (mInstruments.Count > 0) {
			foreach (GameObject go in mInstruments) {
				if (go != null) {
					SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
					InstrumentInfo info = go.GetComponent<InstrumentInfo> ();
					if (!(sr.color.a >= 1) && !info.isColliding) {
						Color c = sr.color;
						c.a += fadeDegree * Time.deltaTime / 255f;
						sr.color = c;
					} else {
						mFadeOut.Add (go, System.DateTime.Now);
											}
				}
			}
		}

		if (mFadeOut.Count > 0) {

			foreach (GameObject go in mFadeOut.Keys) {
				if (go != null) {
					if (mInstruments.Contains (go)) {
						mInstruments.Remove (go);
					}
					System.TimeSpan diff = System.DateTime.Now - mFadeOut [go];
					if (diff.Seconds >= timeToWait) {
						SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
						if (!(sr.color.a <= 0)) {
							Color c = sr.color;
							c.a -= fadeDegree * Time.deltaTime / 255f;
							sr.color = c;
						} else {
							//						mInstruments.Remove (go);
							Destroy (go);
						}
					}
				}
			}


		}
		yield return null;
	}



	void PressHandler (object sender, EventArgs e)
	{
		isPressing = true;
		pressPos = Camera.main.ScreenToWorldPoint (new Vector2 (pressG.ScreenPosition.x, pressG.ScreenPosition.y));
	
	}

	void LongPressHandler(object sender, EventArgs e){
		int x = SceneManager.GetActiveScene ().buildIndex;
		if (x == SceneManager.sceneCountInBuildSettings - 1)
			x=0;
		else
			x++;

		SceneManager.LoadScene (x);
	}



	IEnumerator PressKoordinator ()
	{
		if (isPressing) {
	
			if (IsInLeft (pressPos)) {
	
	
				GameObject tO = Instantiate (musicNote, pressPos, Quaternion.identity) as GameObject;
				mNotes.Add (tO);
	
	
	
			}
	
	
			if (IsInRight (pressPos)) {
	
	
				int rand = UnityEngine.Random.Range (0, 5);
	
				GameObject tO = null;
				switch (rand) {
				case 0:
					tO = Instantiate (mGuitar, pressPos, Quaternion.identity) as GameObject;
					break;
				case 1:
					tO = Instantiate (mViolin, pressPos, Quaternion.identity) as GameObject;
					break;
				case 2:
					tO = Instantiate (mPiano, pressPos, Quaternion.identity) as GameObject;
					break;
				case 3:
					tO = Instantiate (mTrumpet, pressPos, Quaternion.identity) as GameObject;
					break;
				case 4:
					tO = Instantiate (mSax, pressPos, Quaternion.identity) as GameObject;
					break;
				}
	
	
	
				mInstruments.Add (tO);
			}
	
			cleanPress ();
		}
		yield return null;
	}



	private void cleanPress ()
	{
		isPressing = false;
	}






}
