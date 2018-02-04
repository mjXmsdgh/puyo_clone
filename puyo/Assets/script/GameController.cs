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
			m_GameManager.fall ();
		}

		//削除
		if (m_GameManager.get_state () == 2) {
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
		m_DrawGame.draw (m_GameManager.get_gamefield (), m_GameManager.get_temp_puyo (), m_GameManager.get_next_puyo ());
	}

	void ManageKey () {
		//移動
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			m_GameManager.move (0, +1);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			bool ans = m_GameManager.move (0, -1);

			if (ans == true) {
				m_GameManager.fix ();
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			m_GameManager.move (-1, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
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
			m_GameManager.next2temp ();
		}
	}
}