using System.Collections;
using game_field;
using NUnit.Framework;
using puyopuyo_space;
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
	public void test_check_collision () {
		GameField test_target = new GameField ();
		test_target.init ();

		puyopuyo test_puyo = new puyopuyo ();
		test_puyo.init ();

		//ok
		test_puyo.set_position (0, 0, 0);
		test_puyo.set_position (1, 1, 0);

		Assert.AreEqual (false, test_target.check_collision (ref test_puyo));

		//range 0
		test_puyo.set_position (0, -1, 0);
		test_puyo.set_position (1, 0, 0);

		Assert.AreEqual (true, test_target.check_collision (ref test_puyo));

		//range 1
		test_puyo.set_position (0, 0, 0);
		test_puyo.set_position (1, -1, 0);

		Assert.AreEqual (true, test_target.check_collision (ref test_puyo));

		//puyo 0
		test_puyo.set_position (0, 0, 0);
		test_puyo.set_position (1, 1, 0);

		test_target.set_value (0, 0, 1);
		Assert.AreEqual (true, test_target.check_collision (ref test_puyo));

		//puyo 1
		test_puyo.set_position (0, 0, 0);
		test_puyo.set_position (1, 1, 0);

		test_target.set_value (1, 1, 1);
		Assert.AreEqual (true, test_target.check_collision (ref test_puyo));
	}

	[Test]
	public void test_fix () {
		GameField test_target = new GameField ();
		test_target.init ();

		puyopuyo test_puyo = new puyopuyo ();
		test_puyo.init ();

		test_puyo.set_position (0, 1, 0);
		test_puyo.set_position (1, 1, 1);
		test_puyo.set_color (0, 2);
		test_puyo.set_color (1, 3);

		test_target.fix (test_puyo);

		Assert.AreEqual (2, test_target.get_value (1, 0));
		Assert.AreEqual (3, test_target.get_value (1, 1));

		Assert.AreEqual (1, test_target.get_state ());
	}

	[Test]
	public void test_fall () {
		GameField test_target = new GameField ();
		test_target.init ();

		//落下しない
		test_target.fall ();
		Assert.AreEqual (2, test_target.get_state ());

		//落下する
		test_target.set_value (0, 1, 1);
		test_target.fall ();

		Assert.AreEqual (1, test_target.get_state ());
		Assert.AreEqual (1, test_target.get_value (0, 0));
		Assert.AreEqual (0, test_target.get_value (0, 1));
	}

	[Test]
	public void test_delete () {

	}
}