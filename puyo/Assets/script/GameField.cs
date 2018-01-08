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

		//next
		private next_puyo m_next = null;

		//temp
		private puyopuyo m_temp_puyo = null;

		public void init () {
			//gridの初期化
			init_grid ();

			//現在のぷよ生成
			m_temp_puyo = new puyopuyo ();
			m_temp_puyo.init ();

			//nextの生成と初期化
			m_next = new next_puyo ();
			m_next.init ();
		}

		public void next2temp () {
			m_temp_puyo.copy_color (get_next ());
			m_temp_puyo.set_position (0, 3, 10);
			m_temp_puyo.set_position (1, 3, 11);

			m_next.update_next ();
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

		//--------------------
		//next
		//--------------------
		public puyopuyo get_next () {
			return m_next.get ();
		}

		//--------------------
		//temp
		//--------------------
		public puyopuyo get_temp () {
			return m_temp_puyo;
		}

		//--------------------
		//move
		//--------------------
		public bool move (int move_x, int move_y) {

			Point[] new_pos = new Point[2];

			for (int i = 0; i < 2; i++) {

				//現在の位置
				Point tempPos = new Point (0, 0);
				tempPos.set (m_temp_puyo.get_position_x (i), m_temp_puyo.get_position_y (i));

				//移動量
				Point movePos = new Point (0, 0);
				movePos.set (move_x, move_y);

				//移動後の位置
				new_pos[i] = new Point (0, 0);
				new_pos[i] = tempPos + movePos;
			}

			//移動
			//if ((isRange (new_pos[0]) == true) && (isRange (new_pos[1]) == true)) {
			if ((isCheck (new_pos[0]) == true) && (isCheck (new_pos[1]) == true)) {
				for (int i = 0; i < 2; i++) {
					m_temp_puyo.set_position (i, new_pos[i].get_x (), new_pos[i].get_y ());
				}
				return true;
			}
			return false;
		}

		//--------------------
		//rotate
		//--------------------
		public void rotate (bool isRight) {

			Point[] new_pos = new Point[2];

			//回転方向
			int rot = -1;

			if (isRight == true) {
				rot = 1;
			}

			//回転
			//x=x*cos()-y*sin()
			//y=x*sin()+y*cos()

			//index1の位置を平行移動
			int pos_x = m_temp_puyo.get_position_x (1) - m_temp_puyo.get_position_x (0);
			int pos_y = m_temp_puyo.get_position_y (1) - m_temp_puyo.get_position_y (0);

			//回転
			int new_pos_x = (int) Math.Round (pos_x * Math.Cos (rot * Math.PI / 2) - pos_y * Math.Sin (rot * Math.PI / 2));
			int new_pos_y = (int) Math.Round (pos_x * Math.Sin (rot * Math.PI / 2) + pos_y * Math.Cos (rot * Math.PI / 2));

			//移動後の位置を設定
			new_pos[0] = new Point (0, 0);
			new_pos[0].set (m_temp_puyo.get_position_x (0), m_temp_puyo.get_position_y (0));

			new_pos[1] = new Point (0, 0);
			new_pos[1].set (new_pos_x + m_temp_puyo.get_position_x (0), new_pos_y + m_temp_puyo.get_position_y (0));

			//範囲内なら回転
			//if ((isRange (new_pos[0]) == true) && (isRange (new_pos[1]) == true)) {
			if ((isCheck (new_pos[0]) == true) && (isCheck (new_pos[1]) == true)) {
				for (int i = 0; i < 2; i++) {
					m_temp_puyo.set_position (i, new_pos[i].get_x (), new_pos[i].get_y ());
				}
			}
		}
		//--------------------
		//fix
		//--------------------
		public void fix () {
			for (int i = 0; i < 2; i++) {
				int color = m_temp_puyo.get_color (i);
				int pos_x = m_temp_puyo.get_position_x (i);
				int pos_y = m_temp_puyo.get_position_y (i);

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