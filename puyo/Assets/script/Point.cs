using System;

namespace point_space {
	public class Point {
		int m_x;
		int m_y;

		public Point (int input_x, int input_y) {
			set (input_x, input_y);
		}

		public void set (int input_x, int input_y) {
			m_x = input_x;
			m_y = input_y;
		}

		public int get_x () {
			return m_x;
		}

		public int get_y () {
			return m_y;
		}

		public static Point operator + (Point a, Point b) {
			return new Point (a.get_x () + b.get_x (), a.get_y () + b.get_y ());

		}

	}
}