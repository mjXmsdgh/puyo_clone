using System;
using point_space;

namespace puyopuyo_space {

	public class puyopuyo {
		private int[] puyo_color = new int[2];
		private Point[] puyo_pos = new Point[2];

		private bool m_isValid = false;

		public void init () {
			set_color (0, 0);
			set_color (1, 0);

			for (int i = 0; i < 2; i++) {
				puyo_pos[i] = new Point (0, 0);
			}

			m_isValid = true;
		}

		//---------
		//valid
		//---------

		public bool is_valid () {
			return m_isValid;
		}

		public void set_valid (bool ans) {
			m_isValid = ans;
		}

		//---------
		//color
		//---------
		public void set_color (int index, int color) {
			puyo_color[index] = color;
		}

		public int get_color (int index) {
			return puyo_color[index];
		}

		//---------
		//pos
		//---------
		public void set_position (int index, int pos_x, int pos_y) {
			puyo_pos[index].set (pos_x, pos_y);
		}

		public int get_position_x (int index) {
			return puyo_pos[index].get_x ();
		}

		public int get_position_y (int index) {
			return puyo_pos[index].get_y ();
		}

		public Point get_position (int index) {
			return puyo_pos[index];
		}

		//---------
		//copy
		//---------
		public void copy_color (puyopuyo data) {
			for (int i = 0; i < 2; i++) {
				set_color (i, data.get_color (i));
			}
		}

		public void copy_position (puyopuyo data) {
			for (int i = 0; i < 2; i++) {
				set_position (i, data.get_position_x (i), data.get_position_y (i));

			}
		}

		public void copy (puyopuyo data) {
			copy_color (data);
			copy_position (data);
		}

		//---------
		//move
		//---------
		public void move (Point move) {
			for (int i = 0; i < 2; i++) {
				int pos_x = get_position_x (i);
				int pos_y = get_position_y (i);

				set_position (i, pos_x + move.get_x (), pos_y + move.get_y ());
			}
		}

		//---------
		//rotate
		//---------
		public void rotate (bool isRight) {
			int rot = -1;

			if (isRight == true) {
				rot = 1;
			}

			//index1の位置を平行移動
			int pos_x = get_position_x (1) - get_position_x (0);
			int pos_y = get_position_y (1) - get_position_y (0);

			//回転
			int new_pos_x = (int) Math.Round (pos_x * Math.Cos (rot * Math.PI / 2) - pos_y * Math.Sin (rot * Math.PI / 2));
			int new_pos_y = (int) Math.Round (pos_x * Math.Sin (rot * Math.PI / 2) + pos_y * Math.Cos (rot * Math.PI / 2));

			set_position (1, new_pos_x + get_position_x (0), new_pos_y + get_position_y (0));
		}
	}
}