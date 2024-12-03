#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

struct KKK {
    multiset<int> front, mid, rear;
    int k;
    KKK(int k) : k(k) {}
    int size() { return front.size() + mid.size() + rear.size(); }
    void add(int v) {
        if (front.size() < k) front.insert(v);
        else if (rear.size() < k) {
            if (v > *front.rbegin()) rear.insert(v);
            else {
                auto p = prev(front.end());
                rear.insert(*p);
                front.erase(p);
                front.insert(v);
            }
        } else {
            if (v < *front.rbegin()) {
                auto p = prev(front.end());
                mid.insert(*p);
                front.erase(p);
                front.insert(v);
            } else if (v > *rear.begin()) {
                auto p = rear.begin();
                mid.insert(*p);
                rear.erase(p);
                rear.insert(v);
            } else mid.insert(v);
        }
    }
    void remove(int v) {
        if (mid.size() > 0) {
            if (v < *mid.begin()) {
                front.extract(v);
                front.insert(*mid.begin());
                mid.erase(mid.begin());
            } else if (v > *mid.rbegin()) {
                rear.extract(v);
                rear.insert(*mid.rbegin());
                mid.erase(prev(mid.end()));
            } else mid.extract(v);
        } else if (rear.size() > 0) {
            if (v < *rear.begin()) front.extract(v);
            else rear.extract(v);
        } else front.extract(v);
    }
};

void solve() {
    int n;
    cin >> n;
    vector<int> x(n), y(n);
    for (int i = 0; i < n; ++i) cin >> x[i] >> y[i];
    vector<int> idx(n);
    ranges::iota(idx, 0);
    ranges::sort(idx, {}, [&](const int &i){ return make_pair(-x[i], y[i]); });

    auto check = [&](int k) -> tuple<int, int, bool> {
        if (k) {
            KKK left(k), right(k);
            int j = 0;
            for (int i = 0; i < n; ++i) {
                if (x[idx[i]] >= x[idx[2*k-1]]) right.add(y[idx[i]]), j = i;
                else left.add(y[idx[i]]);
            }
            while (left.size() >= 2 * k) {
                int ly1 = *left.front.rbegin(), ly2 = *left.rear.begin(); // (ly1, ly2]
                int ry1 = *right.front.rbegin(), ry2 = *right.rear.begin(); // (ry1, ry2]
                if (ry1 < ry2 && ly1 < ly2 && ry1 < ly2 && ly1 < ry2) return {x[idx[j]], min(ly2, ry2), true};
                for (int i = ++j; i < n && x[idx[i]] == x[idx[j]]; ++i)
                    right.add(y[idx[i]]), left.remove(y[idx[i]]), j = i;
            }
        }
        return {0, 0, false};
    };

    int L = 1, R = n / 4 + 1;
    while (L < R) {
        int mid = L + (R - L) / 2;
        auto [_, _, suc] = check(mid);
        if (suc) L = mid + 1;
        else R = mid;
    }
    auto [X, Y, _] = check(--L);
    cout << L << '\n';
    cout << X << ' ' << Y << '\n';
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    while (T--) {
        solve();
    }
    return 0;
}

