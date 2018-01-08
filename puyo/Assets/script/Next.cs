using System;
using puyopuyo_space;

namespace next_field {
	public class next_puyo {

		private puyopuyo m_puyopuyo;
		private System.Random m_random = new System.Random ();

		public void init () {
			m_puyopuyo = new puyopuyo ();
			update_next ();
		}

		public void update_next () {
			m_puyopuyo.set_color (0, getRandom ());
			m_puyopuyo.set_color (1, getRandom ());
		}

		int getRandom () {
			return m_random.Next (2, 6);
		}

		public puyopuyo get () {
			return m_puyopuyo;
		}
	}
}