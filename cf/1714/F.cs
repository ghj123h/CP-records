using System;
using System.Collections.Generic;
using System.Linq;
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
                int n = Read<int>(), a = Read<int>(), b = Read<int>(), c = Read<int>();
                int u = 2, v = 3;
                int s = 4;
                if (a > c) {
                    (a, c) = (c, a); // a: 1->u b: u->v c: 1->v
                    (u, v) = (v, u);
                }
                if (a + b == c) {
                    output.AppendLine("Yes");
                    Construct(1, u, a);
                    Construct(u, v, b);
                    Remain();
                } else {
                    int t = a + b + c;
                    if (t % 2 != 0 || (t /= 2) >= n) {
                        output.AppendLine("No");
                    } else {
                        int ap = t - b, bp = t - c, cp = t - a; // ap: 1->4 bp: 4->u cp: 4->v
                        if (ap < 0 || bp <= 0 || cp <= 0) {
                            output.AppendLine("No");
                        } else if (ap == 0) {
                            output.AppendLine("Yes");
                            Construct(1, u, bp);
                            Construct(1, v, cp);
                            Remain();
                        } else {
                            s = 5;
                            output.AppendLine("Yes");
                            Construct(1, 4, ap);
                            Construct(4, u, bp);
                            Construct(4, v, cp);
                            Remain();
                        }
                    }
                }

                void Construct(int u, int v, int d) {
                    if (d == 1) output.AppendFormat("{0} {1}\n", u, v);
                    else {
                        output.AppendFormat("{0} {1}\n", u, s);
                        for (int i = 1; i < d - 1; ++i, ++s) output.AppendFormat("{0} {1}\n", s, s + 1);
                        output.AppendFormat("{0} {1}\n", s++, v);
                    }
                }

                void Remain() {
                    while (s <= n) output.AppendFormat("{0} {1}\n", s++, 1);
                }
            }
            Console.Write(output.ToString());
        }
    }
}