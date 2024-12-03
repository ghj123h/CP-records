using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
                int n = Read<int>(), k = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                int[] pre = new int[n];
                pre[0] = s[0] == '0' ? -1 : 1;
                for (int i = 1; i < n; ++i) pre[i] = pre[i - 1] + (s[i] == '0' ? -1 : 1);
                // m*pre[n]-pre[i1]-pre[i2]-pre[i3]-....
                int sum = pre[^1];
                long v = 0;
                Array.Sort(pre);
                bool suc = true;
                for (int i = 0; i < n; ++i) {
                    if (pre[i] >= sum) {
                        suc = false;
                        break;
                    }
                    v += sum - pre[i];
                    if (v >= k) {
                        output.Append(i + 2).AppendLine();
                        break;
                    }
                }
                if (!suc) output.AppendLine("-1");
            }
            Console.Write(output.ToString());
        }
    }
}