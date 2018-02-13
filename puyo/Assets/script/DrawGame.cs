using game_field;
using next_field;
using puyopuyo_space;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawGame : MonoBehaviour {

	private int m_width = 0;
	private int m_height = 0;

	private GameObject[] m_PrefabPuyo = new GameObject[6];

	private GameObject[, ] m_displayGrid = null;
	private GameObject[] m_displayNext = null;
	private GameObject[] m_dislayTemp = null;

	public void init (int width, int height) {
		//幅と高さを設定
		set_width (width);
		set_height (height);

		//リソース読み込み
		load_resorce ();

		//グリッド生成と初期化
		m_displayGrid = new GameObject[get_width (), get_height ()];
		init_grid ();

		//temp初期化
		m_dislayTemp = new GameObject[2];
		init_temp ();

		//next初期化
		m_displayNext = new GameObject[2];
		init_next ();
	}
	//---------------------------------
	//描画
	//---------------------------------
	public void draw (GameField gamefield, puyopuyo temp_puyo, puyopuyo next_puyo, int state) {
		update_temp (temp_puyo);
		update_grid (gamefield);
		update_next (next_puyo);
	}
	//---------------------------------
	//field関係
	//---------------------------------
	void set_width (int input) {
		m_width = input;
	}

	int get_width () {
		return m_width;
	}

	void set_height (int input) {
		m_height = input;
	}

	int get_height () {
		return m_height;
	}

	//---------------------------------
	//リソースの読み込み
	//---------------------------------
	public void load_resorce () {
		for (int i = 1; i <= 6; i++) {
			_load_color (i);
		}
	}
	void _load_color (int color_num) {
		string name = getColorName (color_num);
		string resorce_name = "puyoObj/" + name;
		m_PrefabPuyo[color_num - 1] = (GameObject) Resources.Load (resorce_name);
	}

	//---------------------------------
	//grid関係
	//---------------------------------
	void init_grid () {
		for (int i = 0; i < get_width (); i++) {
			for (int j = 0; j < get_height (); j++)
				m_displayGrid[i, j] = null;
		}
	}

	void delete_grid () {
		for (int i = 0; i < get_width (); i++) {
			for (int j = 0; j < get_height (); j++)
				Destroy (m_displayGrid[i, j]);
		}
	}

	void update_grid (GameField input_gamefield) {

		//オブジェクトを初期化
		delete_grid ();

		//オブジェクト生成
		for (int i = 0; i < get_width (); i++) {
			for (int j = 0; j < get_height (); j++) {

				int value = input_gamefield.get_value (i, j);

				if (value == 0) {
					continue;
				} else if (value < 0) {
					m_displayGrid[i, j] = Instantiate (m_PrefabPuyo[0], new Vector3 (i, j), new Quaternion (0, 0, 0, 0));
				} else {
					m_displayGrid[i, j] = Instantiate (m_PrefabPuyo[value - 1], new Vector3 (i, j), new Quaternion (0, 0, 0, 0));
				}
			}
		}
	}

	//---------------------------------
	//temp関係
	//---------------------------------
	void init_temp () {
		for (int i = 0; i < 2; i++) {
			m_dislayTemp[i] = null;
		}
	}

	void delete_temp () {
		for (int i = 0; i < 2; i++) {
			if (m_dislayTemp[i]) {
				DestroyImmediate (m_dislayTemp[i]);
				m_dislayTemp[i] = null;
			}
		}
	}

	void update_temp (puyopuyo temp_puyo) {

		if (temp_puyo.is_valid () == false) {
			return;
		}

		delete_temp ();

		for (int i = 0; i < 2; i++) {
			int color = temp_puyo.get_color (i);
			int pos_x = temp_puyo.get_position_x (i);
			int pos_y = temp_puyo.get_position_y (i);

			if (color == 0) {
				continue;
			}
			m_dislayTemp[i] = Instantiate (m_PrefabPuyo[color - 1], new Vector3 (pos_x, pos_y, 0), new Quaternion (0, 0, 0, 0));

		}
	}

	//---------------------------------
	//next関係
	//---------------------------------
	void init_next () {
		for (int i = 0; i < 2; i++) {
			m_displayNext[i] = null;
		}
	}

	void delete_next () {
		for (int i = 0; i < 2; i++) {
			Destroy (m_displayNext[i]);
		}
	}

	void update_next (puyopuyo next_puyo) {
		delete_next ();
		for (int i = 0; i < 2; i++) {

			int color = next_puyo.get_color (i);
			int pos_x = 8;
			int pos_y = 9 + i;

			if (color == 0) {
				continue;
			}
			m_displayNext[i] = Instantiate (m_PrefabPuyo[color - 1], new Vector3 (pos_x, pos_y, 0), new Quaternion (0, 0, 0, 0));
		}
	}

	//---------------------------------
	//その他
	//---------------------------------

	//色の番号から名前
	string getColorName (int input_number) {
		switch (input_number) {
			case 0:
				return "blank";
			case 1:
				return "Black";
			case 2:
				return "Brue";
			case 3:
				return "Green";
			case 4:
				return "Purple";
			case 5:
				return "Red";
			case 6:
				return "Yellow";
		}
		return "";
	}
}