#include <bits/stdc++.h>
using namespace std;

using ll = long long;

void gcd(ll a, ll b, ll x) { 
    cout << a << ' ' << b << '\n';
    if (x > a || !b) {
        cout << "No\n";
        return;
    }
    if ((a - x) % b == 0) {
        cout << "Yes\n";
        return;
    }
    gcd(b, a % b, x);
}

void solve() {
    int a, b, x;
    cin >> a >> b >> x;
    if (a < b) swap(a, b);
    gcd(a, b, x);
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T;
    cin >> T;
    while (T--) solve();
    return 0;
}
