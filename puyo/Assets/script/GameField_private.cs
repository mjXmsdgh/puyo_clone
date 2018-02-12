using System;
using next_field;
using point_space;
using puyopuyo_space;
using System.Collections.Generic;

namespace game_field {

	//privateメソッドを集める
	public partial class GameField {
		//--------------------
		//state
		//--------------------

		void set_state (int input_state) {
			m_state = input_state;
		}

		//--------------------
		//grid
		//--------------------
		void init_grid () {
			for (int i = 0; i < GetWidth (); i++) {
				for (int j = 0; j < GetHeight (); j++) {
					set_value (i, j, 0);
				}
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
			if (m_Grid[pos.get_x (), pos.get_y ()] == 0) {
				return true;
			} else {
				return false;
			}
		}

		//--------------------
		//fall
		//--------------------
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
					m_Grid[i, temp_j - 1] = m_Grid[i, temp_j];

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

					if (m_Grid[i, j] != 0) {
						flag = true;
					} else if ((flag == true) && (m_Grid[i, j] == 0)) {
						fall_place[i] = j;
					}
				}
			}
		}

		//--------------------
		//delete
		//--------------------
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
					m_Grid[pos.get_x (), pos.get_y ()] = -1;
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

			if (m_Grid[pos.get_x (), pos.get_y ()] != color_number) {
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
					if (m_Grid[i, j] < 0) {
						m_Grid[i, j] = 0;
					}
				}
			}
		}
	}
}