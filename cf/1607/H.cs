using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateH;

#if !PROBLEM
SolutionH a = new();
a.Solve();
#endif

namespace TemplateH
{
    internal class SolutionH
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
            int inf = 0x3f3f3f3f;
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] x = new int[n], y = new int[n], m = new int[n];
                (int l, int r)[] vals = new (int l, int r)[n];
                Dictionary<int, SortedSet<(int r, int l, int i)>> mp = new();
                for (int i = 0; i < n; ++i) {
                    x[i] = Read<int>(); y[i] = Read<int>();
                    m[i] = Read<int>();
                    (int l, int r) val;
                    if (x[i] >= m[i] && y[i] >= m[i]) val = (-m[i], m[i]);
                    else if (x[i] >= m[i]) val = (m[i] - 2 * y[i], m[i]);
                    else if (y[i] >= m[i]) val = (-m[i], 2 * x[i] - m[i]);
                    else val = (m[i] - 2 * y[i], 2 * x[i] - m[i]);
                    val = (x[i] - y[i] - val.r, x[i] - y[i] - val.l);
                    if (mp.TryGetValue(x[i] + y[i] - m[i], out var st)) {
                        st.Add((val.r, val.l, i));
                    } else {
                        st = new SortedSet<(int, int, int)> { (val.r, val.l, i) };
                        mp.Add(x[i] + y[i] - m[i], st);
                    }
                    vals[i] = val;
                }
                int[] d = new int[n];
                int ans = 0;
                foreach (var (_, st) in mp) {
                    int cur = int.MinValue;
                    foreach (var (r, l, i) in st) {
                        if (l > cur) {
                            cur = r;
                            ++ans;
                        }
                        d[i] = cur;
                    }
                }
                output.Append(ans).AppendLine();
                for (int i = 0; i < n; ++i) output.AppendFormat("{0} {1}\n", x[i] - (x[i] + y[i] - m[i] + d[i]) / 2, y[i] - (x[i] + y[i] - m[i] - d[i]) / 2);
            }
            Console.Write(output.ToString());
        }
    }
}