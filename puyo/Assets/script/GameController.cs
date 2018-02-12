using game_field;
using game_manager;
using next_field;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour {

	//ゲーム
	private GameManager m_GameManager;

	//ゲームの描画
	public DrawGame m_DrawGame;

	AudioSource[] audiosource;
	public AudioClip audioClip_hit;
	public AudioClip audioClip_get;

	// Use this for initialization
	void Start () {
		m_GameManager = new GameManager ();
		m_GameManager.init ();

		m_DrawGame.init (m_GameManager.getWidth (), m_GameManager.getHeight ());

		audiosource = new AudioSource[2];

		audiosource[0] = gameObject.AddComponent<AudioSource> ();
		audiosource[1] = gameObject.AddComponent<AudioSource> ();

		audiosource[0].clip = audioClip_hit;
		audiosource[1].clip = audioClip_get;
	}

	// Update is called once per frame
	void Update () {

		//描画
		m_DrawGame.draw (m_GameManager.get_gamefield (), m_GameManager.get_temp_puyo (), m_GameManager.get_next_puyo (), m_GameManager.get_state ());

		//落下
		if (m_GameManager.get_state () == 1) {
			m_GameManager.fall ();
			return;
		}

		//削除フラグ
		if (m_GameManager.get_state () == 2) {

			m_GameManager.check_delete ();
			if (m_GameManager.get_state () == 0) {
				m_GameManager.next2temp ();
			}
			return;
		}

		//削除
		if (m_GameManager.get_state () == 3) {
			m_GameManager.delete ();
			audiosource[1].Play ();
			return;
		}

		//キー入力
		if (m_GameManager.get_state () == 0) {
			ManageKey ();
		}
	}

	void ManageKey () {
		//移動
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			audiosource[0].Play ();
			m_GameManager.move (0, +1);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			audiosource[0].Play ();
			bool ans = m_GameManager.move (0, -1);

			if (ans == true) {
				m_GameManager.fix ();
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			audiosource[0].Play ();
			m_GameManager.move (-1, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			audiosource[0].Play ();
			m_GameManager.move (+1, 0);
		}

		//回転
		if (Input.GetKeyDown (KeyCode.Z)) {
			audiosource[0].Play ();
			m_GameManager.rotate (true);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			audiosource[0].Play ();
			m_GameManager.rotate (false);
		}

		//nextから取ってくる
		if (Input.GetKeyDown (KeyCode.Space)) {
			m_GameManager.next2temp ();
		}
	}
}