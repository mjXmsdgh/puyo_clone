using System.Collections;
using System.Collections.Generic;
using game_field;
using next_field;
using UnityEngine;
public class GameController : MonoBehaviour {

	//ゲーム
	private GameField m_field;

	//ゲームの描画
	public DrawGame m_DrawGame;

	// Use this for initialization
	void Start () {
		//ゲームの生成と初期化
		m_field = new GameField ();
		m_field.init ();

		//描画の初期化
		m_DrawGame.init (m_field.GetWidth (), m_field.GetHeight ());
	}

	// Update is called once per frame
	void Update () {
		//落下
		if (m_field.get_state () == 1) {
			Debug.Log ("fall");
			m_field.fall ();
		}

		//削除
		if (m_field.get_state () == 2) {
			Debug.Log ("delete");
			m_field.delete ();

			if (m_field.get_state () == 0) {
				m_field.next2temp ();
			}
		}

		//キー入力
		if (m_field.get_state () == 0) {
			ManageKey ();
		}

		//ゲームを描画
		m_DrawGame.draw (ref m_field);
	}
	void ManageKey () {
		//移動
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			//Debug.Log ("get ue");
			m_field.move (0, +1);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//Debug.Log ("get sita");
			bool ans = m_field.move (0, -1);

			if (ans == false) {
				m_field.fix ();
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			//Debug.Log ("get hidari");
			m_field.move (-1, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//Debug.Log ("get migi");
			m_field.move (+1, 0);
		}

		//回転
		if (Input.GetKeyDown (KeyCode.Z)) {
			m_field.rotate (true);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			m_field.rotate (false);
		}

		//nextから取ってくる
		if (Input.GetKeyDown (KeyCode.Space)) {
			//Debug.Log ("get");
			m_field.next2temp ();
		}
	}
}