using System;
using next_field;
using point_space;
using puyopuyo_space;
using System.Collections.Generic;

namespace game_field {

	public partial class GameField {
		//フィールド
		private static int m_width = 6;
		private static int m_height = 12;

		//0:通常 1:落下 2:削除フラグをつける 3:削除
		private int m_state = 0;

		//連鎖数
		private int m_rensa = 0;

		//0:空っぽ 1: 2: 3: 4: 5: 6:
		private int[, ] m_Grid = new int[m_width, m_height];

		public void init () {
			//gridの初期化
			init_grid ();
		}

		//width
		public int GetWidth () {
			return m_width;
		}

		//heigth
		public int GetHeight () {
			return m_height;
		}
		//--------------------
		//state
		//--------------------
		public int get_state () {
			return m_state;
		}

		public int get_rensa () {
			return m_rensa;
		}

		//--------------------
		//grid
		//--------------------

		public int get_value (int input_i, int input_j) {

			if (isRange (new Point (input_i, input_j)) == false) {
				return -99;
			}

			return m_Grid[input_i, input_j];
		}
		public bool set_value (int i, int j, int value) {

			if (isRange (new Point (i, j)) == false) {
				return false;
			}

			m_Grid[i, j] = value;
			return true;
		}

		public bool check_collision (ref puyopuyo input_puyo) {

			//range
			bool ans1 = isRange (input_puyo.get_position (0));
			bool ans2 = isRange (input_puyo.get_position (1));
			if ((ans1 == false) || (ans2 == false)) {
				return true;
			}

			//puyo
			ans1 = isPuyo (input_puyo.get_position (0));
			ans2 = isPuyo (input_puyo.get_position (1));
			if ((ans1 == false) || (ans2 == false)) {
				return true;
			}

			return false;
		}

		//--------------------
		//fix
		//--------------------
		public void fix (puyopuyo input_puyo) {

			for (int i = 0; i < 2; i++) {
				int color = input_puyo.get_color (i);
				int pos_x = input_puyo.get_position_x (i);
				int pos_y = input_puyo.get_position_y (i);

				m_Grid[pos_x, pos_y] = color;
			}
			set_state (1);
		}

		//--------------------
		//fall
		//--------------------
		public void fall () {

			bool answer = isFall ();

			if (answer == true) {
				//落下する
				_fall ();

				//引き続き落下状態
				set_state (1);
			} else {
				//落下しないので削除フラグをつける
				set_state (2);
			}
		}

		//--------------------
		//delete
		//--------------------
		public bool check_delete () {

			bool ans = flood_fill ();

			if (ans == true) {
				//削除表示
				set_state (3);
			} else {
				//通常
				set_state (0);
				m_rensa = 0;
			}

			return ans;
		}

		public void delete () {
			_delete ();
			//削除したので落下
			set_state (1);
		}
	}
}