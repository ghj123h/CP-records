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
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                SortedSet<int> s = new();
                for (int i = 0; i < n; ++i) s.Add(Read<int>());
                while (m-->0)
                {
                    sr.ReadLine();
                    char c = (char)sr.Read();
                    int u = c == '-' ? -1 : 1;
                    int l = Read<int>(), r = Read<int>();
                    var view = s.GetViewBetween(l, r);
                    if (view.Max > 0)
                    {
                        var max = view.Max;
                        s.Remove(max);
                        s.Add(max + u);
                    }
                    output.AppendFormat("{0} ", s.Max);
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}