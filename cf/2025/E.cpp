#include <bits/stdc++.h>
using namespace std;

using ll = long long;
// constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

constexpr int N = 500 * 500 * 2 + 10, P = 998244353;

// from tourist
ll Inv(ll a) {
    ll u = 0, v = 1, m = P;
    while (a > 0) {
        ll t = m / a;
        m -= t * a; swap(a, m);
        u -= t * v; swap(u, v);
    }
    return (u % P + P) % P;
}


// from https://oi-wiki.org/math/poly/ntt/
ll qpow(ll x, int y) {
  ll res(1);
  while (y) {
    if (y & 1) res = res * x % P;
    x = x * x % P;
    y >>= 1;
  }
  return res;
}

ll r[N];

void ntt(ll *x, int lim, int opt) {
    int i, j, k, m;
    for (i = 0; i < lim; ++i)
        if (r[i] < i) swap(x[i], x[r[i]]);
    for (m = 2; m <= lim; m <<= 1) {
        k = m >> 1;
        ll gn = qpow(3, (P - 1) / m);
        for (i = 0; i < lim; i += m) {
            ll g = 1;
            for (j = 0; j < k; ++j, g = g * gn % P) {
                ll tmp = x[i + j + k] * g % P;
                x[i + j + k] = (x[i + j] - tmp + P) % P;
                x[i + j] = (x[i + j] + tmp) % P;
            }
        }
    }
    if (opt == -1) {
        reverse(x + 1, x + lim);
        ll inv = Inv(lim);
        for (i = 0; i < lim; ++i) x[i] = x[i] * inv % P;
    }
}

ll A[N], B[N], fact[N], inv_fact[N];
ll C(int n, int k){ return fact[n] * inv_fact[k] % P * inv_fact[n-k] % P; }

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int n, m, lim = 1;
    fact[0] = inv_fact[0] = 1;
    for (int i = 1; i < N; ++i) {
        fact[i] = fact[i-1] * i % P;
        inv_fact[i] = Inv(fact[i]);
    }
    cin >> n >> m;
    m >>= 1;
    while (lim < ((m * n) << 1)) lim <<= 1;
    for (int i = 0; i < lim; ++i) r[i] = (i & 1) * (lim >> 1) + (r[i >> 1] >> 1);
    for (int i = 0; i <= m; ++i) {
        A[i] = B[i] = C(2 * m, m + i) * (2 * i + 1) % P * Inv(m + i + 1) % P;
    }
    ntt(A, lim, 1);
    for (int i = 0; i < lim; ++i) {
        A[i] = qpow(A[i], n - 1);
    }
    ntt(A, lim, -1);
    ll ans = 0;
    for (int i = 0; i <= m; ++i) {
        ans += A[i] * B[i] % P;
        ans %= P;
    }
    cout << ans << '\n';
    return 0;
}
