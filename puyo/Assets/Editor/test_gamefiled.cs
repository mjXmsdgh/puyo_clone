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

				if (i >= test_target.GetWidth () - 1) continue;

				for (int k = 0; k < 4; k++) {
					int[, ] base_pos = new int[, ] { { i, j }, { i + 1, j } };
					int[] move_pos = new int[] { test_data[k, 0], test_data[k, 1] };
					int[] moved_pos = new int[] { move_pos[0], move_pos[1] };

					int min_x = base_pos[0, 0] + move_pos[0];
					int max_x = base_pos[1, 0] + move_pos[0];

					int min_y = base_pos[0, 1] + move_pos[1];
					int max_y = base_pos[0, 1] + move_pos[1];

					if ((min_x < 0) || (max_x >= test_target.GetWidth ())) {
						moved_pos[0] = 0;
					}

					if ((min_y < 0) || (max_y >= test_target.GetHeight ())) {
						moved_pos[1] = 0;
					}

					_test_move (ref test_target, base_pos, move_pos, moved_pos);
				}
			}
		}
	}

	void check_do_not_move (ref GameField test_target) {

		test_target.set_value (3, 3, 1);

		int[, ] test_data = new int[, ] { { 1, 0 }, { 0, 1 }, {-1, 0 }, { 0, -1 } };

		for (int i = 0; i < test_target.GetWidth (); i++) {
			for (int j = 0; j < test_target.GetHeight (); j++) {
				for (int k = 0; k < 4; k++) {
					int[, ] base_pos = new int[, ] { { i, j }, { i + 1, j } };
					int[] move_pos = new int[] { test_data[k, 0], test_data[k, 1] };
					int[] moved_pos = new int[] { move_pos[0], move_pos[1] };

					int min_x = base_pos[0, 0] + move_pos[0];
					int max_x = base_pos[1, 0] + move_pos[0];

					int min_y = base_pos[0, 1] + move_pos[1];
					int max_y = base_pos[0, 1] + move_pos[1];

					if ((min_x == 3) || (max_x == 3)) {
						moved_pos[0] = 0;
						moved_pos[1] = 0;
						_test_move (ref test_target, base_pos, moved_pos, moved_pos);
					}

					if ((min_y == 3) || (max_y == 3)) {
						moved_pos[0] = 0;
						moved_pos[1] = 0;
						_test_move (ref test_target, base_pos, moved_pos, moved_pos);
					}
				}
			}
		}

	}

	void _test_move (ref GameField test_target, int[, ] base_pos, int[] move_pos, int[] moved_pos) {
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