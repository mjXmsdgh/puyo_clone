using System.Collections;
using NUnit.Framework;
using puyopuyo_space;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Puyopuyo {
	[Test] //initのテスト
	public void test_init () {
		puyopuyo test_target = new puyopuyo ();
		test_target.init ();

		//color
		for (int i = 0; i < 2; i++) {
			int color = test_target.get_color (i);
			Assert.AreEqual (color, 0);
		}

		//pos
		for (int i = 0; i < 2; i++) {
			int x = test_target.get_position_x (i);
			int y = test_target.get_position_y (i);

			Assert.AreEqual (x, 0);
			Assert.AreEqual (y, 0);
		}
	}

	[Test] //colorのset/getのテスト
	public void test_color_setget () {
		puyopuyo test_target = new puyopuyo ();
		test_target.init ();

		int color = -1;

		//index[0]
		test_target.set_color (0, 3);
		color = test_target.get_color (0);
		Assert.AreEqual (color, 3);

		//index[1]
		test_target.set_color (1, 5);
		color = test_target.get_color (1);
		Assert.AreEqual (color, 5);
	}

	[Test] //posのset/getのテスト
	public void test_position_setget () {
		puyopuyo test_target = new puyopuyo ();
		test_target.init ();

		test_target.set_position (0, 3, 5);
		test_target.set_position (1, 7, 11);

		int x = 0;
		int y = 0;

		x = test_target.get_position_x (0);
		Assert.AreEqual (x, 3);

		x = test_target.get_position_x (1);
		Assert.AreEqual (x, 7);

		y = test_target.get_position_y (0);
		Assert.AreEqual (y, 5);

		y = test_target.get_position_y (1);
		Assert.AreEqual (y, 11);
	}

	[Test] //copyのテスト
	public void test_copy_color () {
		puyopuyo test_target = new puyopuyo ();
		test_target.init ();

		test_target.set_color (0, 1);
		test_target.set_color (1, 2);

		puyopuyo copy_obj = new puyopuyo ();

		copy_obj.copy_color (test_target);

		for (int i = 0; i < 2; i++) {
			int color = copy_obj.get_color (i);
			Assert.AreEqual (color, i + 1);
		}
	}

	[Test]
	public void test_copy_position () {

	}

	[Test]
	public void test_copy () {

	}

	[Test]
	public void test_move () {

	}

	[Test]
	public void test_rotate () {

	}
}