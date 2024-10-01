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
                int n = Read<int>();
                string[] A = new string[n], B = new string[n];
                sr.ReadLine();
                for (int i = 0; i < n; ++i) A[i] = sr.ReadLine();
                sr.ReadLine();
                for (int i = 0; i < n; ++i) B[i] = sr.ReadLine();
                int[] col = A[0].Zip(B[0], (a, b) => a == b ? 0 : 1).ToArray();
                bool suc = true;
                for (int i = 1; i < n && suc; ++i) {
                    int v = A[i][0] == B[i][0] ? 0 : 1;
                    v ^= col[0];
                    for (int j = 1; j < n && suc; ++j) {
                        int u = A[i][j] == B[i][j] ? 0 : 1;
                        suc = (col[j] ^ v) == u;
                    }
                }
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}