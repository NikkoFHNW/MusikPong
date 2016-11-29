using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using TouchScript;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{



	//References to needed Unity Objects
	//	public Text rightTxt;
	//	public Text leftTxt;
	TapGesture tg;
	public GameObject musicNote;
	public GameObject mViolin;
	public GameObject mGuitar;
	public GameObject mPiano;

	//Variables for calculations and management
	private bool isTap;
	private Vector2 touchedPos;
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

		isTap = false;
		mNotes = new List<GameObject> ();
		mInstruments = new List<GameObject> ();
		toDelete = new List<GameObject> ();

	
		mFadeOut = new Dictionary<GameObject,System.DateTime> ();
	}

	private void OnEnable ()
	{
		tg = GetComponent<TapGesture> ();
		tg.Tapped += TouchBeganHandler;


	}


	private void OnDisable ()
	{
		tg.Tapped -= TouchBeganHandler;


	}


	void FixedUpdate ()
	{
		TapKoordinator ();
		NoteMover ();
		Fader ();
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

	private void TouchBeganHandler (object sender, EventArgs e)
	{
		var gesture = sender as TapGesture;

		isTap = true;

		touchedPos = Camera.main.ScreenToWorldPoint (new Vector2 (gesture.ScreenPosition.x, gesture.ScreenPosition.y));

		Touch[] t = Input.touches;
		int x = t.Length;

	}



	private void NoteMover ()
	{
		if (mNotes.Count > 0) {
			

			foreach (GameObject go in mNotes) {
				if (go != null) {
					
					Vector2 v2 = new Vector2 (go.transform.position.x + noteSpeed * Time.deltaTime, go.transform.position.y);
					go.transform.position = v2;
				}
			}
		}
	}



	private void GOCleaner ()
	{
		toDelete=new List<GameObject> ();
		if (mNotes.Count > 0) {
//			toDelete = new List<GameObject> ();
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
//					else if (go.tag.Equals ("Guitar") || go.tag.Equals ("Violin") || go.tag.Equals ("Piano")) {
//
//
//					}

				}
			}
		}


	}

	private void TapKoordinator ()
	{

		if (isTap) {

			if (IsInLeft (touchedPos)) {


				GameObject tO = Instantiate (musicNote, touchedPos, Quaternion.identity) as GameObject;
				mNotes.Add (tO);


			}


			if (IsInRight (touchedPos)) {


				int rand = UnityEngine.Random.Range (0, 3);

				GameObject tO = null;
				switch (rand) {
				case 0:
					tO = Instantiate (mGuitar, touchedPos, Quaternion.identity) as GameObject;
					break;
				case 1:
					tO = Instantiate (mViolin, touchedPos, Quaternion.identity) as GameObject;
					break;
				case 2:
					tO = Instantiate (mPiano, touchedPos, Quaternion.identity) as GameObject;
					break;
				}



				mInstruments.Add (tO);
			}
			isTap = false;
		}
	}





	private void Fader ()
	{
		if (mInstruments.Count > 0) {
			foreach (GameObject go in mInstruments) {
				if (go != null) {
					SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
					if (!(sr.color.a >=1)) {
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
					System.TimeSpan diff = System.DateTime.Now-mFadeOut [go];
					if (diff.Seconds>=timeToWait) {
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
}
