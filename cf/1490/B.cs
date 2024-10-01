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
                int[] c = new int[3];
                foreach (var x in a) ++c[x % 3];
                int aver = (c[0] + c[1] + c[2]) / 3;
                for (int i = 0; i < 3; ++i) c[i] -= aver;
                if (c.All(x => x == 0)) {
                    output.AppendLine("0");
                } else if (c.Count(x => x > 0) == 1) {
                    for (int i = 0; i < 3; ++i) {
                        if (c[i] > 0) {
                            output.Append(-c[(i + 1) % 3] - c[(i + 2) % 3] * 2).AppendLine();
                        }
                    }
                } else {
                    for (int i = 0; i < 3; ++i) {
                        if (c[i] < 0) {
                            output.Append(c[(i + 2) % 3] + c[(i + 1) % 3] * 2).AppendLine();
                        }
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}