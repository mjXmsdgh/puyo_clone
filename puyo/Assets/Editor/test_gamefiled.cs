using System.Collections;
using game_field;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_gamefiled {

	[Test]
	public void test_init () {
		GameField test_target = new GameField ();
		test_target.init ();

		//gridの初期化
		for (int i = 0; i < test_target.GetWidth (); i++) {
			for (int j = 0; j < test_target.GetHeight (); j++) {
				Assert.AreEqual (0, test_target.get_value (i, j));
			}
		}

		//現在のぷよ
		Assert.AreNotEqual (null, test_target.get_temp ());

		//nextのぷよ
		Assert.AreNotEqual (null, test_target.get_next ());
	}

	[Test]
	public void test_next2temp () {
		GameField test_target = new GameField ();
		test_target.init ();

		int color_zero = test_target.get_next ().get_color (0);
		int color_one = test_target.get_next ().get_color (1);

		test_target.next2temp ();

		//color
		Assert.AreEqual (color_zero, test_target.get_temp ().get_color (0));
		Assert.AreEqual (color_one, test_target.get_temp ().get_color (1));

		//position
		Assert.AreEqual (3, test_target.get_temp ().get_position_x (0));
		Assert.AreEqual (10, test_target.get_temp ().get_position_y (0));

		Assert.AreEqual (3, test_target.get_temp ().get_position_x (1));
		Assert.AreEqual (11, test_target.get_temp ().get_position_y (1));

		//update_next
	}

	[Test]
	public void test_GetWidth () {
		GameField test_target = new GameField ();
		Assert.AreEqual (6, test_target.GetWidth ());
	}

	[Test]
	public void test_GetHeight () {
		GameField test_target = new GameField ();
		Assert.AreEqual (12, test_target.GetHeight ());
	}

	[Test]
	public void test_getState () {
		GameField test_target = new GameField ();
		Assert.AreEqual (0, test_target.get_state ());
	}

	[Test]
	public void test_setgetvalue () {
		GameField test_target = new GameField ();

		for (int i = 0; i < 6; i++) {
			test_target.set_value (i, i + 1, i);
			Assert.AreEqual (i, test_target.get_value (i, i + 1));
		}
	}

	[Test]
	public void test_get_next () {
		GameField test_target = new GameField ();
		test_target.init ();
		Assert.AreNotEqual (null, test_target.get_next ());
	}

	[Test]
	public void test_get_temp () {
		GameField test_target = new GameField ();
		test_target.init ();

		Assert.AreNotEqual (null, test_target.get_temp ());
	}

	[Test]
	public void test_move () {
		GameField test_target = new GameField ();
		test_target.init ();

		//check range
		check_range (ref test_target);

		//don't move
		check_do_not_move (ref test_target);
	}

	void check_range (ref GameField test_target) {

		int[, ] test_data = new int[, ] { { 1, 0 }, { 0, 1 }, {-1, 0 }, { 0, -1 } };

		for (int i = 0; i < test_target.GetWidth (); i++) {
			for (int j = 0; j < test_target.GetHeight (); j++) {

				int[, ] base_pos = new int[, ] { { i, j }, { i + 1, j } };

				//基準座標,上下左右
				check_base_and_test (ref test_target, base_pos, test_data);
			}
		}
	}

	void check_base_and_test (ref GameField test_target, int[, ] base_pos, int[, ] test_data) {
		for (int k = 0; k < 4; k++) {
			int[] move_pos = new int[] { test_data[k, 0], test_data[k, 1] };
			int[] moved_pos = new int[] { move_pos[0], move_pos[1] };

			if (check_range (test_target.GetWidth (), 0, base_pos, move_pos) == false) {
				moved_pos[0] = 0;
			}

			if (check_range (test_target.GetHeight (), 1, base_pos, move_pos) == false) {
				moved_pos[1] = 0;
			}

			if (check_puyo (ref test_target, base_pos, move_pos) == false) {
				moved_pos[0] = 0;
				moved_pos[1] = 0;
			}
			
			//test 基準座標 移動量 実際に移動する量
			_test_move (ref test_target, base_pos, move_pos, moved_pos);
		}
	}

	bool check_range (int max, int index, int[, ] base_pos, int[] move_pos) {

		int min_value = base_pos[0, index] + move_pos[index];
		int max_value = base_pos[1, index] + move_pos[index];

		if ((min_value < 0) || (max_value >= max)) {
			return false;
		} else {
			return true;
		}
	}

	bool check_puyo (ref GameField test_target, int[, ] base_pos, int[] move_pos) {

		for (int index = 0; index < 2; index++) {

			if (test_target.get_value (base_pos[index, 0], base_pos[index, 1]) != 0) {
				return false;
			}

			int x = base_pos[index, 0] + move_pos[0];
			int y = base_pos[index, 1] + move_pos[1];

			if (test_target.get_value (x, y) != 0) {
				return false;
			}
		}
		return true;

	}

	void check_do_not_move (ref GameField test_target) {

		test_target.set_value (3, 3, 1);

		int[, ] test_data = new int[, ] { { 1, 0 }, { 0, 1 }, {-1, 0 }, { 0, -1 } };

		for (int i = 0; i < test_target.GetWidth (); i++) {
			for (int j = 0; j < test_target.GetHeight (); j++) {

				if ((i == 3) && (j == 3)) continue;
				if ((i + 1 == 3) && (j == 3)) continue;

				for (int k = 0; k < 4; k++) {
					int[, ] base_pos = new int[, ] { { i, j }, { i + 1, j } };

					check_base_and_test (ref test_target, base_pos, test_data);
				}
			}
		}
	}

	void _test_move (ref GameField test_target, int[, ] base_pos, int[] move_pos, int[] moved_pos) {

		//基準座標が範囲外の場合はテストしない
		for (int i = 0; i < 2; i++) {
			if ((base_pos[i, 0] < 0) || (base_pos[i, 0] >= test_target.GetWidth ())) {
				return;
			}

			if ((base_pos[i, 1] < 0) || (base_pos[i, 1] >= test_target.GetHeight ())) {
				return;
			}
		}

		//設定
		test_target.get_temp ().set_position (0, base_pos[0, 0], base_pos[0, 1]);
		test_target.get_temp ().set_position (1, base_pos[1, 0], base_pos[1, 1]);

		//移動
		test_target.move (move_pos[0], move_pos[1]);

		//テスト
		Assert.AreEqual (base_pos[0, 0] + moved_pos[0], test_target.get_temp ().get_position_x (0));
		Assert.AreEqual (base_pos[0, 1] + moved_pos[1], test_target.get_temp ().get_position_y (0));

		Assert.AreEqual (base_pos[1, 0] + moved_pos[0], test_target.get_temp ().get_position_x (1));
		Assert.AreEqual (base_pos[1, 1] + moved_pos[1], test_target.get_temp ().get_position_y (1));
	}

	[Test]
	public void test_rotate () { }

	[Test]
	public void test_fix () {

	}

	[Test]
	public void test_fall () {

	}

	[Test]
	public void test_delete () {

	}
}