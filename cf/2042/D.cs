using System;
using System.Collections.Generic;
using System.Linq;
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
            while (T-- > 0)
            {
                int n = Read<int>();
                (int l, int r)[] segs = new (int l, int r)[n];
                for (int i = 0; i < n; ++i) segs[i] = (Read<int>(), Read<int>());
                int[] L = segs.Select(s => s.l).ToArray(), R = segs.Select(s => s.r).ToArray();
                PriorityQueue<int, int> q = new();
                foreach (var i in Enumerable.Range(0, n).OrderBy(j => segs[j].r).ThenByDescending(j => segs[j].l)) {
                    bool rep = false;
                    while (q.Count > 0 && segs[q.Peek()].l >= segs[i].l) {
                        int j = q.Dequeue();
                        if (segs[j] == segs[i]) rep = true;
                        R[j] = segs[i].r;
                    }
                    if (!rep) q.Enqueue(i, -segs[i].l);
                }
                q.Clear();
                foreach (var i in Enumerable.Range(0, n).OrderByDescending(j => segs[j].l).ThenBy(j => segs[j].r)) {
                    bool rep = false;
                    while (q.Count > 0 && segs[q.Peek()].r <= segs[i].r) {
                        int j = q.Dequeue();
                        if (segs[j] == segs[i]) rep = true;
                        L[j] = segs[i].l;
                    }
                    if (!rep) q.Enqueue(i, segs[i].r);
                }
                for (int i = 0; i < n; ++i) output.Append(R[i] - L[i] - (segs[i].r - segs[i].l)).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}