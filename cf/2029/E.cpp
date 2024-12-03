#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

constexpr int V = 400010, maxn = 100010;
int a[maxn], comp[V];
int prime[V], tot;

bool check(int pr, int n, int f) { // 检查 pr 能否生成 f 的倍数
    if (pr == f) return true;
    int nr = (pr + f - 1) / f * f * 2;
    return nr <= n;
}

void solve() {
    int pr = 0, ans = 0;
    int n;
    cin >> n;
    for (int i = 0; i < n; ++i) {
        cin >> a[i];
        if (!comp[a[i]]) {
            if (!pr) pr = a[i];
            else ans = -1;
        }
    }
    if (!pr) ans = 2;
    if (!ans) {
        bool ssuc = true;
        for (int i = 0; i < n; ++i) {
            int u = a[i];
            bool suc = false;
            for (int j = 0; comp[u]; ++j) {
                if (u % prime[j] == 0) {
                    suc = suc || check(pr, a[i], prime[j]);
                    do {
                        u /= prime[j];
                    } while (u % prime[j] == 0);
                }
            }
            if (u > 1) suc = suc || check(pr, a[i], u);
            if (!suc) {
                ssuc = false;
                break;
            }
        }
        ans = ssuc ? pr : -1;
    }
    cout << ans << '\n';
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    for (ll i = 2; i < V; ++i) {
        if (!comp[i]) {
            prime[tot++] = i;
            for (ll j = i * i; j < V; j += i) comp[j] = true;
        }
    }
    while (T--) {
        solve();
    }
    return 0;
}
