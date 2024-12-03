#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, k;
    cin >> n >> k;
    vector<int> a(n), b(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    multiset<int> front, rear;
    ll sum = 0;
    for (int i = 0; i < n; ++i) {
        cin >> b[i];
        front.insert(b[i]);
        sum += b[i];
    }
    while (front.size() >= k) {
        sum -= *front.rbegin();
        rear.insert(*front.rbegin());
        front.extract(*front.rbegin());
    }
    vector<int> idx(n);
    iota(idx.begin(), idx.end(), 0);
    ranges::sort(idx, {}, [&](int i){ return -a[i]; });
    int j = 0;
    ll ans = linf;
    while (rear.size() > 0) {
        int i = idx[j++];
        if (rear.contains(b[i])) {
            ans = min(ans, a[i] * (sum + b[i]));
            rear.extract(b[i]);
        } else {
            ans = min(ans, a[i] * (sum + *rear.begin()));
            sum += *rear.begin() - b[i];
            front.extract(b[i]);
            front.insert(*rear.begin());
            rear.erase(rear.begin());
        }
    }
    cout << ans << '\n';
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    // T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
