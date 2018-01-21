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

		//--------------------
		//move
		//--------------------
		public bool move (int move_x, int move_y) {
			/*
			//移動前のpuyoを保存
			puyopuyo prev_puyo = new puyopuyo ();
			prev_puyo.init ();
			prev_puyo.copy (m_temp_puyo);

			//移動
			m_temp_puyo.move (new Point (move_x, move_y));

			if ((isCheck (m_temp_puyo.get_position (0)) == true) && (isCheck (m_temp_puyo.get_position (1)) == true)) {
				//範囲内ならなにもしない
				return true;
			} else {
				//範囲外ならもとに戻す
				m_temp_puyo.copy (prev_puyo);
					return false;
				}*/
			return false;
		}

		public bool force_to_range (ref puyopuyo input_puyo) {

			bool[] check = new bool[2] { false, false };
			bool isMove = false;

			while ((check[0] == false) || (check[1] == false)) {

				for (int i = 0; i < 2; i++) {
					Point temp = input_puyo.get_position (i);
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

					//移動
					input_puyo.move (move);

					//フラグ
					if (isRange (temp) == true) {
						check[i] = true;
						isMove = true;
					} else {
						check[i] = false;
					}
				}
			}
			return isMove;
		}

		//--------------------
		//rotate
		//--------------------
		public void rotate (bool isRight) {
			/*
						//移動前のpuyoを保存
						puyopuyo prev_puyo = new puyopuyo ();
						prev_puyo.init ();
						prev_puyo.copy (m_temp_puyo);

						//回転
						m_temp_puyo.rotate (isRight);

						if ((isCheck (m_temp_puyo.get_position (0)) == true) && (isCheck (m_temp_puyo.get_position (1)) == true)) {
							//範囲内ならなにもしない
							return;
						} else {
							//範囲外ならもとに戻す
							m_temp_puyo.copy (prev_puyo);
							return;
						}*/
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