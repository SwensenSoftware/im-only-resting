using System;
using System.Collections.Generic;

namespace Swensen.Utils {
    public class HistoryList<T> : IEnumerable<T> {
        List<T> list;

        /// <summary>
        /// Init with initial max history
        /// </summary>
        /// <param name="maxHistory">initial max history</param>
        public HistoryList(int maxHistory) {
            this.maxHistory = maxHistory;
            this.list = new List<T>(maxHistory);
        }


        int maxHistory;
        /// <summary>
        /// The movable max history
        /// </summary>
        public int MaxHistory {
            get { return maxHistory; }
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MaxHistory must be greater than or equal to 0");

                maxHistory = value;
                ensureMaxHistory();
            }
        }

        /// <summary>
        /// Remove any element from the tail of the list that are outside of maxHistory
        /// </summary>
        private void ensureMaxHistory() {
            if (list.Count > maxHistory) {
                //maxHistory = 10 and list.Count = 11, then list.RemoveRange(10, 11 - 10 == 1)
                list.RemoveRange(maxHistory, list.Count - maxHistory);
            }
        }

        /// <summary>
        /// Prepend a history item
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item) {
            list.Insert(0, item);
            ensureMaxHistory();
        }

        public void Clear() {
            list.Clear();
        }

        public int Count { get { return list.Count; } }

        public IEnumerator<T> GetEnumerator() {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
