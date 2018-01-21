using System.Collections;
using System.Collections.Generic;
using game_field;
using game_manager;
using next_field;
using UnityEngine;
public class GameController : MonoBehaviour {

	//ゲーム
	private GameManager m_GameManager;

	//ゲームの描画
	public DrawGame m_DrawGame;

	// Use this for initialization
	void Start () {

		m_GameManager = new GameManager ();
		m_GameManager.init ();

		m_DrawGame.init (m_GameManager.getWidth (), m_GameManager.getHeight ());
	}

	// Update is called once per frame
	void Update () {

		//落下
		if (m_GameManager.get_state () == 1) {
			Debug.Log ("fall");
			m_GameManager.fall ();
		}

		//削除
		if (m_GameManager.get_state () == 2) {
			Debug.Log ("delete");
			m_GameManager.delete ();

			if (m_GameManager.get_state () == 0) {
				m_GameManager.next2temp ();
			}
		}

		//キー入力
		if (m_GameManager.get_state () == 0) {
			ManageKey ();
		}

		//ゲームを描画
		//m_DrawGame.draw (ref m_field);
		m_DrawGame.draw (m_GameManager.get_gamefield (), m_GameManager.get_temp_puyo (), m_GameManager.get_next_puyo ());
	}
	void ManageKey () {
		//移動
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			//Debug.Log ("get ue");
			m_GameManager.move (0, +1);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//Debug.Log ("get sita");
			bool ans = m_GameManager.move (0, -1);

			if (ans == true) {
				Debug.Log ("fix");
				m_GameManager.fix ();
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			//Debug.Log ("get hidari");
			m_GameManager.move (-1, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//Debug.Log ("get migi");
			m_GameManager.move (+1, 0);
		}

		//回転
		if (Input.GetKeyDown (KeyCode.Z)) {
			m_GameManager.rotate (true);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			m_GameManager.rotate (false);
		}

		//nextから取ってくる
		if (Input.GetKeyDown (KeyCode.Space)) {
			//Debug.Log ("get");
			//m_field.next2temp ();
			m_GameManager.next2temp ();
		}
	}
}