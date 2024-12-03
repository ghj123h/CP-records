using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
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
            int[] mp = new int[200010];
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int u = n - 2;
                mp[a[0]] = 1;
                for (int i = 1; i < n; ++i) {
                    if (u % a[i] == 0 && mp[u / a[i]] > 0) {
                        output.AppendFormat("{0} {1}\n", a[i], u / a[i]);
                        break;
                    }
                    ++mp[a[i]];
                }
                foreach (var v in a) mp[v] = 0;
            }
            Console.Write(output.ToString());
        }
    }
}