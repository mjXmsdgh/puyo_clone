using System;
using point_space;

namespace puyopuyo_space {

	public class puyopuyo {
		private int[] puyo_color = new int[2];
		private Point[] puyo_pos = new Point[2];

		public void init () {
			set_color (0, 0);
			set_color (1, 0);

			for (int i = 0; i < 2; i++) {
				puyo_pos[i] = new Point (0, 0);
			}
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

		//---------
		//copy
		//---------
		public void copy (puyopuyo data) {
			for (int i = 0; i < 2; i++) {
				set_color (i, data.get_color (i));
			}
		}
	}
}