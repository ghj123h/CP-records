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
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int l = 0;
                while (l < n) {
                    int mi = l;
                    int min = a[mi];
                    for (int i = mi + 1; i < n; ++i) if (min > a[i]) { min = a[i]; mi = i; }
                    for (int i = mi; i > l; --i) a[i] = a[i - 1];
                    a[l] = min;
                    if (l > 0 && a[l] < a[l - 1]) (a[l], a[l - 1]) = (a[l - 1], a[l]);
                    l = mi + 1;
                }
                output.AppendJoin(' ', a).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}