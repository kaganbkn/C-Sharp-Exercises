using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Books.Legacy
{
    public class ComplicatedPageCalculator
    {
        /// <summary>
        /// Full CPU load for 5 seconds
        /// </summary>
        public int CalculateBookPages()
        {
            var watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                if (watch.ElapsedMilliseconds > 5000)
                {
                    break;
                }
            }

            return 42;
        }
    }
}