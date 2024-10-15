using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TemplateD;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace TemplateD
{
    internal class SolutionD
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
            while (T-- > 0)
            {
                string t = sr.ReadLine();
                int n = Read<int>(), m = t.Length;
                string[] s = new string[n];
                sr.ReadLine();
                for (int i = 0; i < n; ++i) s[i] = sr.ReadLine();
                int[] mat = Enumerable.Range(1, m).ToArray();
                int[] cor = new int[m];
                for (int i = 0; i < n; ++i) {
                    var ss = s[i];
                    int j = -1;
                    while ((j = t.IndexOf(ss, j + 1)) != -1) {
                        int k = j + ss.Length - 1;
                        if (mat[k] > j) {
                            mat[k] = j;
                            cor[k] = i + 1;
                        }
                    }
                }
                int[] d = new int[m + 1], pre = Enumerable.Range(0, m + 1).ToArray();
                Array.Fill(d, 0x3f3f3f3f); d[0] = 0;
                for (int i = 0; i < m; ++i) {
                    for (int j = mat[i]; j <= i; ++j) {
                        if (d[i + 1] > d[j] + 1) {
                            d[i + 1] = d[j] + 1;
                            pre[i + 1] = j;
                        }
                    }
                }
                if (d[m] == 0x3f3f3f3f) output.AppendLine("-1");
                else {
                    output.Append(d[m]).AppendLine();
                    for (int i = m; i > 0; i = pre[i]) {
                        output.AppendFormat("{0} {1}\n", cor[i - 1], mat[i - 1] + 1);
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}