using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiscAdEarth
{
		class Alarm: System.Object {
			public Alarm() {
				timer = 0;
				is_set = false;
				name = "temp";
			}

			public Alarm(int time, bool on, string newName) {
				timer = time;
				is_set = on;
				name = newName;
			}
			bool is_set;
			string name;
			int timer;

			public void add(int time) {
				timer += time;
			}

			public bool ring() {
				return timer == 0 && is_set;
			}

			public void set(int time) {
				timer = time;
			}

            public void set(bool setting)
            {
                is_set = setting;
            }

            public void set(int time, bool setting)
            {
                timer = time;
                is_set = setting;
            }

			public void update() {
				if (is_set) timer--;
			}
		}

}

