using System;

namespace point_space {
	public class Point {

		//x座標
		int m_x;

		//y座標
		int m_y;

		//コンストラクタ
		public Point (int input_x, int input_y) {
			set (input_x, input_y);
		}

		//値を設定
		public void set (int input_x, int input_y) {
			m_x = input_x;
			m_y = input_y;
		}

		//x座標を取得
		public int get_x () {
			return m_x;
		}

		//y座標を取得
		public int get_y () {
			return m_y;
		}

		//足し算
		public static Point operator + (Point a, Point b) {
			return new Point (a.get_x () + b.get_x (), a.get_y () + b.get_y ());
		}
	}
}