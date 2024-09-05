using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
                int n = Read<int>(), q = Read<int>();
                long[] a = ReadArray<long>(n);
                long[] pre = new long[2 * n + 1];
                for (int i = 0; i < n; ++i) pre[i + 1] = pre[i] + a[i];
                for (int i = n; i < 2 * n; ++i) pre[i + 1] = pre[i] + a[i - n];
                while (q-->0)
                {
                    long l = Read<long>(), r = Read<long>();
                    --l; --r;
                    long cl = l / n, cr = r / n;
                    long il = l % n + cl, ir = r % n + cr;
                    il %= n; ir = ir % n + n;
                    if (cl == cr) output.Append(pre[il + r - l + 1] - pre[il]).AppendLine();
                    else
                    {
                        long ans = (cr - cl - 1) * pre[n];
                        ans += pre[il + n - l % n] - pre[il];
                        ans += pre[ir + 1] - pre[ir - r % n];
                        output.Append(ans).AppendLine();
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}