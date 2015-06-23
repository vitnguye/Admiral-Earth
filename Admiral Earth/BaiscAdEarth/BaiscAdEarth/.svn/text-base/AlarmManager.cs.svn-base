using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiscAdEarth
{
		class AlarmManager: System.Object {
			public AlarmManager() {
				size = 0;
			}

			AlarmManager() {
				LinkedList<string> result = new LinkedList<string>();
				foreach (KeyValuePair<string, Alarm> itor in AlarmMap)
				{
						if (itor.Value.ring())
								result.AddFirst(itor.Key);
				}
				return result;
			}
			int size;

			public void add(string name, int time, bool on) {
				Alarm temp = new Alarm(time, on, name);
				AlarmMap.Add(name, temp);
				size++;
			}

			public void update() {
				foreach (KeyValuePair<string, Alarm> itor in AlarmMap)
				{
						itor.Value.update();
				}
			}
		}
		}


