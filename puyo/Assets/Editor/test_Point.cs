using System.Collections;
using NUnit.Framework;
using point_space;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Point {

	[Test] //コンストラクタのテスト
	public void test_constructor () {
		Point test_target = new Point (3, 7);

		int x = test_target.get_x ();
		int y = test_target.get_y ();

		Assert.AreEqual (3, x);
		Assert.AreEqual (7, y);
	}

	[Test] //setのテスト
	public void test_set () {
		Point test_target = new Point (0, 0);
		test_target.set (11, 13);

		int x = test_target.get_x ();
		int y = test_target.get_y ();

		Assert.AreEqual (11, x);
		Assert.AreEqual (13, y);
	}

	[Test] //getのテスト
	public void test_get () {
		Point test_target = new Point (17, 19);

		int x = test_target.get_x ();
		int y = test_target.get_y ();

		Assert.AreEqual (17, x);
		Assert.AreEqual (19, y);
	}

	[Test] //足し算のテスト
	public void test_operator () {
		Point a = new Point (1, 2);
		Point b = new Point (3, 5);
		Point c = a + b;

		int x = c.get_x ();
		int y = c.get_y ();

		Assert.AreEqual (x, 4);
		Assert.AreEqual (y, 7);
	}
}