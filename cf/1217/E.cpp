#include <bits/stdc++.h>
using namespace std;

constexpr int inf = 1999999999;

template<class TValue, class TInfo>
class SimpleSegTree {
    vector<TInfo> tree;
    int n, j;
    TInfo e;
    void init(const TValue &value, TInfo &info) const;
    void merge(const TInfo &l, const TInfo &r, TInfo &res) const;
    void build(const vector<int> &vec, int v, int l, int r) {
        if (l == r) init(vec[l], tree[v]);
        else {
            int m = l + (r - l) / 2;
            build(vec, v * 2 + 1, l, m);
            build(vec, v * 2 + 2, m + 1, r);
            merge(tree[v * 2 + 1], tree[v * 2 + 2], tree[v]);
        }
    }
    void update(int v, int i, int l, int r, const TValue &value) {
        if (l == r && i == l) init(value, tree[v]);
        else {
            int m = l + (r - l) / 2;
            if (i <= m) update(v * 2 + 1, i, l, m, value);
            else update(v * 2 + 2, i, m + 1, r, value);
            merge(tree[v * 2 + 1], tree[v * 2 + 2], tree[v]);
        }
    }
    TInfo query(int v, int L, int R, int l, int r) const {
        if (L > R) return e;
        else if (l >= L && r <= R) return tree[v];
        int m = l + (r - l) / 2;
        auto left = query(v * 2 + 1, L, min(R, m), l, m);
        auto right = query(v * 2 + 2, max(L, m + 1), R, m + 1, r);
        TInfo res;
        merge(left, right, res);
        return res;
    }
public:
    SimpleSegTree(const vector<int> &vec, int j, const TInfo &e)
        : n(vec.size()), j(j), e(e), tree(vec.size() << 2) {
        build(vec, 0, 0, n - 1);
    }
    void update(int i, const TValue &value) { update(0, i, 0, n - 1, value); }
    TInfo query(int l, int r) const { return query(0, l, r, 0, n - 1); }
};

template<>
void SimpleSegTree<int, pair<int, int>>::init(const int &value, pair<int, int> &info) const {
    int u = value / j % 10; info = {u ? value : inf, inf};
}

template<>
void SimpleSegTree<int, pair<int, int>>::merge(const pair<int, int> &l, const pair<int, int> &r, pair<int, int> &res) const {
    res = l.first <= r.first ? make_pair(l.first, min(r.first, l.second)) : make_pair(r.first, min(l.first, r.second));
}

void solve() {
    int n, m;
    cin >> n >> m;
    vector<int> a(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    vector<SimpleSegTree<int, pair<int, int>>> segs;
    for (int i = 0, j = 1; i < 9; ++i, j *= 10) segs.emplace_back(a, j, make_pair(inf, inf));
    while (m--) {
        int u, v, w;
        cin >> u >> v >> w;
        if (u == 1) for (int i = 0; i < 9; ++i) segs[i].update(v - 1, w);
        else {
            int r = inf;
            for (int i = 0; i < 9; ++i) {
                auto [a, b] = segs[i].query(v - 1, w - 1);
                if (b < inf) r = min(r, a + b);
            }
            cout << (r == inf ? -1 : r) << '\n';
        }
    }
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    // cin >> T;
    T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
