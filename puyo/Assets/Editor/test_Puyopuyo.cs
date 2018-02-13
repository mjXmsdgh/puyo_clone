using NUnit.Framework;
using puyopuyo_space;
using System.Collections;
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
			Assert.AreEqual (0, test_target.get_position (i).get_x ());
			Assert.AreEqual (0, test_target.get_position (i).get_y ());
		}
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