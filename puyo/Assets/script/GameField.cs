using System;
using System.Collections.Generic;
using next_field;
using point_space;
using puyopuyo_space;

namespace game_field {

	public partial class GameField {
		//フィールド
		private static int m_width = 6;
		private static int m_height = 12;

		//0:通常 1:落下状態 2:削除状態
		private int m_state = 0;

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

		//--------------------
		//grid
		//--------------------

		public int get_value (int input_i, int input_j) {
			return m_Grid[input_i, input_j];
		}
		public void set_value (int i, int j, int value) {
			m_Grid[i, j] = value;
		}

		public bool force_to_range (ref puyopuyo input_puyo) {
			bool isMove = false;

			while (true) {

				Point move_value = new Point (0, 0);

				for (int i = 0; i < 2; i++) {

					//移動量を取得
					Point temp_move_value = get_move_value (input_puyo.get_position (i));

					int prev_val = 0;
					int new_val = 0;

					prev_val = Math.Abs (move_value.get_x ());
					new_val = Math.Abs (temp_move_value.get_x ());

					if (prev_val < new_val) {
						move_value.set (temp_move_value.get_x (), move_value.get_y ());
						isMove = true;
					}

					prev_val = Math.Abs (move_value.get_y ());
					new_val = Math.Abs (temp_move_value.get_y ());

					if (prev_val < new_val) {
						move_value.set (move_value.get_y (), temp_move_value.get_y ());
						isMove = true;
					}
				}

				//移動
				input_puyo.move (move_value);

				if ((move_value.get_x () == 0) && (move_value.get_y () == 0)) {
					break;
				}

			}

			return isMove;
		}

		Point get_move_value (Point temp) {
			//Point temp = input_puyo.get_position (i);
			Point move = new Point (0, 0);

			//移動量を計算
			if (temp.get_x () < 0) {
				move.set (1, 0);
			} else if (temp.get_x () >= GetWidth ()) {
				move.set (-1, 0);
			} else if (temp.get_y () < 0) {
				move.set (0, +1);
			} else if (temp.get_y () >= GetHeight ()) {
				move.set (0, -1);
			}
			return move;
		}

		//--------------------
		//fix
		//--------------------
		public void fix () {
			/*
			for (int i = 0; i < 2; i++) {
				int color = m_temp_puyo.get_color (i);
				int pos_x = m_temp_puyo.get_position_x (i);
				int pos_y = m_temp_puyo.get_position_y (i);

				m_Grid[pos_x, pos_y] = color;
			}
			set_state (1);
			*/
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
				//落下しないので削除状態
				set_state (2);
			}
		}

		//--------------------
		//delete
		//--------------------
		public void delete () {

			//削除するところにフラグを付ける
			bool isDelete = check_delete ();

			if (isDelete == false) {
				//削除しないので通常状態
				set_state (0);
			} else {
				//フラグが付いたところを削除する
				_delete ();

				//削除したので落下状態
				set_state (1);
			}
		}
	}
}