using System.Collections;
using next_field;
using NUnit.Framework;
using puyopuyo_space;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Next {

	[Test] //
	public void test_init () {
		next_puyo test_target = new next_puyo ();
		test_target.init ();
	}

	[Test]
	public void test_get () {
		next_puyo test_target = new next_puyo ();
		test_target.init ();

		puyopuyo ret_puyo = test_target.get ();

		Assert.AreNotEqual (ret_puyo, null);

		//puyo color
		for (int i = 0; i < 2; i++) {
			int color = ret_puyo.get_color (i);
			Assert.AreEqual (3, color, 3);
		}

		//puyo pos
		for (int i = 0; i < 2; i++) {
			int pos_x = ret_puyo.get_position_x (i);
			int pos_y = ret_puyo.get_position_y (i);
			Assert.AreEqual (0, pos_x);
			Assert.AreEqual (0, pos_y);
		}

	}
}