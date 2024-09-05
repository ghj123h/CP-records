using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace Template
{
    internal class SolutionB
    {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T : struct, IConvertible
        {
            char c;
            dynamic res = default(T);
            dynamic sign = 1;
            while (!sr.EndOfStream && char.IsWhiteSpace((char)sr.Peek())) sr.Read();
            if (!sr.EndOfStream && (char)sr.Peek() == '-')
            {
                sr.Read();
                sign = -1;
            }
            while (!sr.EndOfStream && char.IsDigit((char)sr.Peek()))
            {
                c = (char)sr.Read();
                res = res * 10 + c - '0';
            }
            return res * sign;
        }

        private T[] ReadArray<T>(int n)
            where T : struct, IConvertible
        {
            T[] arr = new T[n];
            for (int i = 0; i < n; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            sr.ReadLine();
            int[] hand = new int[2];
            int ans = 0;
            while (T-- > 0)
            {
                string line = sr.ReadLine();
                int key = int.Parse(line[0..^2]); char c = line[^1];
                int i = c == 'L' ? 0 : 1;
                if (hand[i] > 0) ans += Math.Abs(hand[i] - key);
                hand[i] = key;
            }
            output.Append(ans);
            Console.WriteLine(output.ToString());
        }
    }
}