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