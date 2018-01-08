using System;
using System.Collections.Generic;
using next_field;
using point_space;
using puyopuyo_space;

namespace game_field {

	public class GameField {
		//フィールド
		private static int Width = 6;
		private static int Height = 12;

		//0:通常 1:落下状態 2:削除状態
		private int m_state = 0;

		//0:空っぽ 1: 2: 3: 4: 5: 6:
		private int[, ] Grid = new int[Width, Height];

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
			m_temp_puyo.copy (get_next ());
			m_temp_puyo.set_position (0, 3, 10);
			m_temp_puyo.set_position (1, 3, 11);

			m_next.update_next ();
		}

		//width
		public int GetWidth () {
			return Width;
		}

		//heigth
		public int GetHeight () {
			return Height;
		}
		//--------------------
		//state
		//--------------------
		public int get_state () {
			return m_state;
		}

		public void set_state (int input_state) {
			m_state = input_state;
		}

		//--------------------
		//grid
		//--------------------
		void init_grid () {
			for (int i = 0; i < GetWidth (); i++) {
				for (int j = 0; j < GetHeight (); j++) {
					Grid[i, j] = 0;
				}
			}
		}

		public int get_value (int input_i, int input_j) {
			return Grid[input_i, input_j];
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
		//check
		//--------------------

		bool isCheck (Point pos) {
			if ((isRange (pos) == true) && (isPuyo (pos) == true)) {
				return true;
			} else {
				return false;
			}
		}

		bool isRange (Point pos) {

			if ((pos.get_x () < 0) || (pos.get_x () >= GetWidth ())) {
				return false;
			}

			if ((pos.get_y () < 0) || (pos.get_y () >= GetHeight ())) {
				return false;
			}
			return true;
		}

		bool isPuyo (Point pos) {
			if (Grid[pos.get_x (), pos.get_y ()] == 0) {
				return true;
			} else {
				return false;
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

				Grid[pos_x, pos_y] = color;
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

		bool isFall () {
			int[] fall_place = new int[GetWidth ()];

			scan_field (ref fall_place);

			bool answer = false;

			for (int i = 0; i < GetWidth (); i++) {
				if (fall_place[i] != -1) {
					answer = true;
				}
			}
			return answer;
		}

		void _fall () {

			int[] fall_place = new int[GetWidth ()];
			scan_field (ref fall_place);

			for (int i = 0; i < GetWidth (); i++) {

				//落下しない列
				if (fall_place[i] == -1) {
					continue;
				}

				//落下する
				int temp_j = fall_place[i] + 1;
				while (true) {
					Grid[i, temp_j - 1] = Grid[i, temp_j];

					temp_j = temp_j + 1;

					if (temp_j >= GetHeight ()) {
						break;
					}
				}
			}
		}

		void scan_field (ref int[] fall_place) {

			for (int i = 0; i < GetWidth (); i++) {

				bool flag = false;
				fall_place[i] = -1;

				for (int j = GetHeight () - 1; j >= 0; j--) {

					if (Grid[i, j] != 0) {
						flag = true;
					} else if ((flag == true) && (Grid[i, j] == 0)) {
						fall_place[i] = j;
					}
				}
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

		bool check_delete () {
			return flood_fill ();
		}

		bool flood_fill () {

			bool delete_flag = false;

			for (int color = 0; color < 6; color++) {
				if (color_function (color + 1) == true) {
					delete_flag = true;
				}
			}
			return delete_flag;
		}

		bool color_function (int color_number) {
			//visit
			bool[, ] visit = new bool[GetWidth (), GetHeight ()];

			for (int i = 0; i < GetWidth (); i++) {
				for (int j = 0; j < GetHeight (); j++) {
					visit[i, j] = false;
				}
			}

			bool delete_flag = false;

			//flood_fill
			for (int i = 0; i < GetWidth (); i++) {
				for (int j = 0; j < GetHeight (); j++) {
					if (flood_fill_color (i, j, color_number, ref visit) == true) {
						delete_flag = true;
					}
				}
			}
			return delete_flag;
		}

		bool flood_fill_color (int i, int j, int color_number, ref bool[, ] visit) {
			//queue
			Queue<Point> ans_queue = new Queue<Point> ();

			search (new Point (i, j), color_number, ref visit, ref ans_queue);

			if (ans_queue.Count >= 4) {
				while (ans_queue.Count > 0) {
					Point pos = ans_queue.Dequeue ();
					Grid[pos.get_x (), pos.get_y ()] = -1;
				}
				return true;
			} else {
				return false;
			}
		}

		void search (Point target, int color_number, ref bool[, ] visit, ref Queue<Point> ans_queue) {

			Queue<Point> flood_queue = new Queue<Point> ();
			flood_queue.Enqueue (target);

			while (flood_queue.Count > 0) {

				Point temp_pos = flood_queue.Dequeue ();

				if (isTarget (temp_pos, color_number, ref visit) == false) {
					continue;
				}

				visit[temp_pos.get_x (), temp_pos.get_y ()] = true;
				ans_queue.Enqueue (temp_pos);

				for (int a = -1; a <= 1; a++) {
					for (int b = -1; b <= 1; b++) {

						if ((a == b) || (a * b != 0)) {
							continue;
						}

						Point new_pos = new Point (temp_pos.get_x () + a, temp_pos.get_y () + b);

						if (isTarget (new_pos, color_number, ref visit) == false) {
							continue;
						}

						flood_queue.Enqueue (new_pos);
					}
				}
			}
		}

		bool isTarget (Point pos, int color_number, ref bool[, ] visit) {

			if (isRange (pos) == false) {
				return false;
			}

			if (Grid[pos.get_x (), pos.get_y ()] != color_number) {
				return false;
			}

			if (visit[pos.get_x (), pos.get_y ()] == true) {
				return false;
			}
			return true;
		}

		void _delete () {

			for (int i = 0; i < GetWidth (); i++) {
				for (int j = 0; j < GetHeight (); j++) {
					if (Grid[i, j] < 0) {
						Grid[i, j] = 0;
					}
				}
			}
		}

	}
}