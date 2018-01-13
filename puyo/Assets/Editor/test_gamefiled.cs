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

		test_target.get_temp ().set_position (0, 3, 3);
		test_target.get_temp ().set_position (1, 4, 3);

		//move
		move_ok (ref test_target, 3, 3, 4, 3);

		//don't move
		move_ng (ref test_target, 3, 3, 4, 3);
	}

	void move_ok (ref GameField test_target, int zero_x, int zero_y, int one_x, int one_y) {
		//up
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (0, 1);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 0, 1);

		//down
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (0, -1);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 0, -1);

		//left
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (-1, 0);
		check (ref test_target, zero_x, zero_y, one_x, one_y, -1, 0);

		//rigth
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (1, 0);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 1, 0);
	}

	void move_ng (ref GameField test_target, int zero_x, int zero_y, int one_x, int one_y) {

		//ぷよ
		test_target.set_value (zero_x, zero_y - 1, 1);
		test_target.set_value (one_y + 1, one_y, 1);
		test_target.set_value (zero_x, zero_y + 1, 1);
		test_target.set_value (zero_x - 1, zero_y, 1);

		//up
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (0, 1);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 0, 0);

		//down
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (0, -1);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 0, 0);

		//left
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (-1, 0);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 0, 0);

		//rigth
		init_pos (ref test_target, zero_x, zero_y, one_x, one_y);
		test_target.move (1, 0);
		check (ref test_target, zero_x, zero_y, one_x, one_y, 0, 0);
	}

	void init_pos (ref GameField test_target, int zero_x, int zero_y, int one_x, int one_y) {
		test_target.get_temp ().set_position (0, zero_x, zero_y);
		test_target.get_temp ().set_position (1, one_x, one_y);
	}

	void check (ref GameField test_target, int zero_x, int zero_y, int one_x, int one_y, int move_x, int move_y) {
		Assert.AreEqual (zero_x + move_x, test_target.get_temp ().get_position_x (0));
		Assert.AreEqual (zero_y + move_y, test_target.get_temp ().get_position_y (0));

		Assert.AreEqual (one_x + move_x, test_target.get_temp ().get_position_x (1));
		Assert.AreEqual (one_y + move_y, test_target.get_temp ().get_position_y (1));
	}

	[Test]
	public void test_rotate () {

	}

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