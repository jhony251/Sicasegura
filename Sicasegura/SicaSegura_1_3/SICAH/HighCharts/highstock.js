﻿/*
Highstock JS v1.1.4 (2012-02-15)

(c) 2009-2011 Torstein H?nsi

License: www.highcharts.com/license
*/
(function() {
    function E(a, b) { var c; a || (a = {}); for (c in b) a[c] = b[c]; return a } function ja() { for (var a = 0, b = arguments, c = b.length, d = {}; a < c; a++) d[b[a++]] = b[a]; return d } function N(a, b) { return parseInt(a, b || 10) } function zb(a) { return typeof a === "string" } function rb(a) { return typeof a === "object" } function ic(a) { return Object.prototype.toString.call(a) === "[object Array]" } function Sb(a) { return typeof a === "number" } function Eb(a) { return sa.log(a) / sa.LN10 } function rc(a) { return sa.pow(10, a) } function Fb(a, b) {
        for (var c =
a.length; c--; ) if (a[c] === b) { a.splice(c, 1); break } 
    } function A(a) { return a !== G && a !== null } function R(a, b, c) { var d, e; if (zb(b)) A(c) ? a.setAttribute(b, c) : a && a.getAttribute && (e = a.getAttribute(b)); else if (A(b) && rb(b)) for (d in b) a.setAttribute(d, b[d]); return e } function sb(a) { return ic(a) ? a : [a] } function r() { var a = arguments, b, c, d = a.length; for (b = 0; b < d; b++) if (c = a[b], typeof c !== "undefined" && c !== null) return c } function J(a, b) { if (Tb && b && b.opacity !== G) b.filter = "alpha(opacity=" + b.opacity * 100 + ")"; E(a.style, b) } function na(a,
b, c, d, e) { a = S.createElement(a); b && E(a, b); e && J(a, { padding: 0, border: oa, margin: 0 }); c && J(a, c); d && d.appendChild(a); return a } function pa(a, b) { var c = function() { }; c.prototype = new a; E(c.prototype, b); return c } function Ub(a, b, c, d) { var e = X.lang, f = isNaN(b = Ba(b)) ? 2 : b, b = c === void 0 ? e.decimalPoint : c, d = d === void 0 ? e.thousandsSep : d, e = a < 0 ? "-" : "", c = String(N(a = Ba(+a || 0).toFixed(f))), g = c.length > 3 ? c.length % 3 : 0; return e + (g ? c.substr(0, g) + d : "") + c.substr(g).replace(/(\d{3})(?=\d)/g, "$1" + d) + (f ? b + Ba(a - c).toFixed(f).slice(2) : "") }
    function sc(a, b, c, d) { var e, c = r(c, 1); e = a / c; if (!b && (b = [1, 2, 2.5, 5, 10], d && (d.allowDecimals === !1 || d.type === "logarithmic"))) c === 1 ? b = [1, 2, 5, 10] : c <= 0.1 && (b = [1 / c]); for (d = 0; d < b.length; d++) if (a = b[d], e <= (b[d] + (b[d + 1] || b[d])) / 2) break; a *= c; return a } function tc(a, b) {
        var c = b || [[tb, [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], [kb, [1, 2, 5, 10, 15, 30]], [db, [1, 2, 5, 10, 15, 30]], [za, [1, 2, 3, 4, 6, 8, 12]], [ka, [1, 2]], [Ca, [1, 2]], [Ka, [1, 2, 3, 4, 6]], [La, null]], d = c[c.length - 1], e = D[d[0]], f = d[1], g; for (g = 0; g < c.length; g++) if (d = c[g], e = D[d[0]], f = d[1], c[g +
1] && a <= (e * f[f.length - 1] + D[c[g + 1][0]]) / 2) break; e === D[La] && a < 5 * e && (f = [1, 2, 5]); e === D[La] && a < 5 * e && (f = [1, 2, 5]); c = sc(a / e, f); return { unitRange: e, count: c, unitName: d[0]}
    } function Vb(a, b, c, d) {
        var e = [], f = {}, g = X.global.useUTC, h, j = new Date(b), b = a.unitRange, k = a.count; j.setMilliseconds(0); b >= D[kb] && j.setSeconds(b >= D[db] ? 0 : k * Na(j.getSeconds() / k)); if (b >= D[db]) j[uc](b >= D[za] ? 0 : k * Na(j[jc]() / k)); if (b >= D[za]) j[vc](b >= D[ka] ? 0 : k * Na(j[kc]() / k)); if (b >= D[ka]) j[lc](b >= D[Ka] ? 1 : k * Na(j[eb]() / k)); b >= D[Ka] && (j[wc](b >= D[La] ? 0 : k * Na(j[Wb]() /
k)), h = j[Xb]()); b >= D[La] && (h -= h % k, j[xc](h)); if (b === D[Ca]) j[lc](j[eb]() - j[mc]() + r(d, 1)); d = 1; h = j[Xb](); for (var i = j.getTime(), l = j[Wb](), j = j[eb](); i < c; ) e.push(i), b === D[La] ? i = Yb(h + d * k, 0) : b === D[Ka] ? i = Yb(h, l + d * k) : !g && (b === D[ka] || b === D[Ca]) ? i = Yb(h, l, j + d * k * (b === D[ka] ? 1 : 7)) : (i += b * k, b <= D[za] && i % D[ka] === 0 && (f[i] = ka)), d++; e.push(i); e.info = E(a, { higherRanks: f, totalRange: b * k }); return e
    } function yc() { this.symbol = this.color = 0 } function Nc(a, b, c, d, e, f, g, h, j) {
        var k = g.x, g = g.y, j = k + c + (j ? h : -a - h), i = g - b + d + 15, l; j < 7 && (j = c + k + h);
        j + a > c + e && (j -= j + a - (c + e), i = g - b + d - h, l = !0); i < d + 5 ? (i = d + 5, l && g >= i && g <= i + b && (i = g + d + h)) : i + b > d + f && (i = d + f - b - h); return { x: j, y: i}
    } function Oc(a, b) { var c = a.length, d, e; for (e = 0; e < c; e++) a[e].ss_i = e; a.sort(function(a, c) { d = b(a, c); return d === 0 ? a.ss_i - c.ss_i : d }); for (e = 0; e < c; e++) delete a[e].ss_i } function Gb(a) { for (var b = a.length, c = a[0]; b--; ) a[b] < c && (c = a[b]); return c } function Ab(a) { for (var b = a.length, c = a[0]; b--; ) a[b] > c && (c = a[b]); return c } function ub(a) { for (var b in a) a[b] && a[b].destroy && a[b].destroy(), delete a[b] } function Hb(a) {
        Zb ||
(Zb = na(vb)); a && Zb.appendChild(a); Zb.innerHTML = ""
    } function Ib(a, b) { Lb = r(a, b.animation) } function zc() { var a = X.global.useUTC, b = a ? "getUTC" : "get", c = a ? "setUTC" : "set"; Yb = a ? Date.UTC : function(a, b, c, g, h, j) { return (new Date(a, b, r(c, 1), r(g, 0), r(h, 0), r(j, 0))).getTime() }; jc = b + "Minutes"; kc = b + "Hours"; mc = b + "Day"; eb = b + "Date"; Wb = b + "Month"; Xb = b + "FullYear"; uc = c + "Minutes"; vc = c + "Hours"; lc = c + "Date"; wc = c + "Month"; xc = c + "FullYear" } function Bb() { } function $b(a, b) {
        function c(a) {
            function b(a, c) {
                this.pos = a; this.type = c || ""; this.isNew =
!0; c || this.addLabel()
            } function c(a) { if (a) this.options = a, this.id = a.id; return this } function d(a, b, c, e) { this.isNegative = b; this.options = a; this.x = c; this.stack = e; this.alignOptions = { align: a.align || (da ? b ? "left" : "right" : "center"), verticalAlign: a.verticalAlign || (da ? "middle" : b ? "bottom" : "top"), y: r(a.y, da ? 4 : b ? 14 : -6), x: r(a.x, da ? b ? -6 : 6 : 0) }; this.textAlign = a.textAlign || (da ? b ? "right" : "left" : "center") } function e() {
                var a = [], b = [], c; P = Va = null; n(F.series, function(e) {
                    if (e.visible || !v.ignoreHiddenSeries) {
                        var f = e.options,
g, h, i, j, k, l, o, s, p, Oa = f.threshold, t, n = [], u = 0; if (m) f = e.xData, f.length && (P = va(r(P, f[0]), Gb(f)), Va = T(r(Va, f[0]), Ab(f))); else {
                            var F, ac, x, ya = e.cropped, ca = e.xAxis.getExtremes(), w = !!e.modifyValue; g = f.stacking; $a = g === "percent"; if (g) k = f.stack, j = e.type + r(k, ""), l = "-" + j, e.stackKey = j, h = a[j] || [], a[j] = h, i = b[l] || [], b[l] = i; $a && (P = 0, Va = 99); f = e.processedXData; o = e.processedYData; t = o.length; for (c = 0; c < t; c++) if (s = f[c], p = o[c], p !== null && p !== G && (g ? (ac = (F = p < Oa) ? i : h, x = F ? l : j, p = ac[s] = A(ac[s]) ? ac[s] + p : p, q[x] || (q[x] = {}), q[x][s] ||
(q[x][s] = new d(z.stackLabels, F, s, k)), q[x][s].setTotal(p)) : w && (p = e.modifyValue(p)), ya || (f[c + 1] || s) >= ca.min && (f[c - 1] || s) <= ca.max)) if (s = p.length) for (; s--; ) p[s] !== null && (n[u++] = p[s]); else n[u++] = p; !$a && n.length && (P = va(r(P, n[0]), Gb(n)), Va = T(r(Va, n[0]), Ab(n))); e.useThreshold && Oa !== null && (P >= Oa ? (P = Oa, Ga = !0) : Va < Oa && (Va = Oa, yb = !0))
                        } 
                    } 
                })
            } function f(a) { var b; b = a; ma = r(ma, sa.pow(10, Na(sa.log(Da) / sa.LN10))); ma < 1 && (b = y(1 / ma) * 10, b = y(a * b) / b); return b } function g() {
                var a, b; a = f(Na(O / Da) * Da); var c = f(bc(U / Da) * Da); for (ea =
[]; a <= c; ) { ea.push(a); a = f(a + Da); if (a === b) break; b = a } 
            } function h() { var a, b = Va - P >= M, c, d, e, f, g, i; m && M === G && (A(z.min) || A(z.max) ? M = null : (n(F.series, function(a) { f = a.xData; for (d = g = a.xIncrement ? 1 : f.length - 1; d > 0; d--) if (e = f[d] - f[d - 1], c === G || e < c) c = e }), M = va(c * 5, Va - P))); U - O < M && (a = (M - U + O) / 2, a = [O - a, r(z.min, O - a)], b && (a[2] = P), O = Ab(a), i = [O + M, r(z.max, O + M)], b && (i[2] = Va), U = Gb(i), U - O < M && (a[0] = U - M, a[1] = r(z.min, U - M), O = Ab(a))) } function i(a) {
                var b, c, d = z.tickInterval, e = z.tickPixelInterval; Ua ? (c = o[m ? "xAxis" : "yAxis"][z.linkedTo],
b = c.getExtremes(), O = r(b.min, b.dataMin), U = r(b.max, b.dataMax)) : (O = r(Ya, z.min, P), U = r(Xa, z.max, Va)); w && (O = Eb(O), U = Eb(U)); Db && (Ya = O = T(O, U - Db), Xa = U, a && (Db = null)); h(); if (!Sa && !$a && !Ua && A(O) && A(U)) { b = U - O || 1; if (!A(z.min) && !A(Ya) && ja && (P < 0 || !Ga)) O -= b * ja; if (!A(z.max) && !A(Xa) && na && (Va > 0 || !yb)) U += b * na } Da = O === U || O === void 0 || U === void 0 ? 1 : Ua && !d && e === c.options.tickPixelInterval ? c.tickInterval : r(d, Sa ? 1 : (U - O) * e / (I || 1)); m && !a && n(F.series, function(a) { a.processData(O !== X || U !== ka) }); aa(); F.beforeSetTickPositions && F.beforeSetTickPositions();
                F.postProcessTickInterval && (Da = F.postProcessTickInterval(Da)); ya || (ma = sa.pow(10, Na(sa.log(Da) / sa.LN10)), A(z.tickInterval) || (Da = sc(Da, null, ma, z))); F.tickInterval = Da; ab = z.minorTickInterval === "auto" && Da ? Da / 5 : z.minorTickInterval; (ea = z.tickPositions || La && La.apply(F, [O, U])) || (ya ? ea = (F.getNonLinearTimeTicks || Vb)(tc(Da, z.units), O, U, z.startOfWeek, F.ordinalPositions, F.closestPointRange, !0) : g()); Y(F, "afterSetTickPositions", { tickPositions: ea }); if (!Ua && (a = ea[0], c = ea[ea.length - 1], z.startOnTick ? O = a : O > a && ea.shift(),
z.endOnTick ? U = c : U < c && ea.pop(), lb || (lb = { x: 0, y: 0 }), !ya && ea.length > lb[Aa] && z.alignTicks !== !1)) lb[Aa] = ea.length
            } function j(a) { a = (new c(a)).render(); fa.push(a); return a } function k() {
                var a = z.title, d = z.stackLabels, e = z.alternateGridColor, f = z.lineWidth, g, h, i = o.hasRendered && A(X) && !isNaN(X), l = (g = F.series.length && A(O) && A(U)) || r(z.showEmpty, !0); if (g || Ua) {
                    if (ab && !Sa) for (g = O + (ea[0] - O) % ab; g <= U; g += ab) ua[g] || (ua[g] = new b(g, "minor")), i && ua[g].isNew && ua[g].render(null, !0), ua[g].isActive = !0, ua[g].render(); n(ea, function(a,
c) { if (!Ua || a >= O && a <= U) Pa[a] || (Pa[a] = new b(a)), i && Pa[a].isNew && Pa[a].render(c, !0), Pa[a].isActive = !0, Pa[a].render(c) }); e && n(ea, function(a, b) { if (b % 2 === 0 && a < U) la[a] || (la[a] = new c), la[a].options = { from: a, to: ea[b + 1] !== G ? ea[b + 1] : U, color: e }, la[a].render(), la[a].isActive = !0 }); if (!F._addedPlotLB) n((z.plotLines || []).concat(z.plotBands || []), function(a) { j(a) }), F._addedPlotLB = !0
                } n([Pa, ua, la], function(a) { for (var b in a) a[b].isActive ? a[b].isActive = !1 : (a[b].destroy(), delete a[b]) }); f && (g = C + (p ? hb : 0) + ca, h = Ha - D - (p ? mb :
0) + ca, g = Z.crispLine([ta, s ? C : g, s ? h : pb, ga, s ? ha - Q : g, s ? h : Ha - D], f), Wa ? Wa.animate({ d: g }) : Wa = Z.path(g).attr({ stroke: z.lineColor, "stroke-width": f, zIndex: 7 }).add(), Wa[l ? "show" : "hide"]()); if (u && l) l = s ? C : pb, f = N(a.style.fontSize || 12), l = { low: l + (s ? 0 : I), middle: l + I / 2, high: l + (s ? I : 0)}[a.align], f = (s ? pb + mb : C) + (s ? 1 : -1) * (p ? -1 : 1) * Ka + (t === 2 ? f : 0), u[u.isNew ? "attr" : "animate"]({ x: s ? l : f + (p ? hb : 0) + ca + (a.x || 0), y: s ? f - (p ? mb : 0) + ca : l + (a.y || 0) }), u.isNew = !1; if (d && d.enabled) {
                    var m, Oa, d = F.stackTotalGroup; if (!d) F.stackTotalGroup = d = Z.g("stack-labels").attr({ visibility: bb,
                        zIndex: 6
                    }).translate($, V).add(); for (m in q) for (Oa in a = q[m], a) a[Oa].render(d)
                } F.isDirty = !1
            } function l(a) { for (var b = fa.length; b--; ) fa[b].id === a && fa[b].destroy() } var m = a.isX, p = a.opposite, s = da ? !m : m, t = s ? p ? 0 : 2 : p ? 1 : 3, q = {}, z = H(m ? cc : nc, [Pc, Qc, Ac, Rc][t], a), F = this, u, x = z.type, ya = x === "datetime", w = x === "logarithmic", ca = z.offset || 0, Aa = m ? "x" : "y", I = 0, Ma, K, Ra, B, C, pb, hb, mb, D, Q, Za, aa, J, S, R, Wa, P, Va, M = z.minRange || z.maxZoom, Db = z.range, Ya, Xa, ba, ia, U = null, O = null, X, ka, ja = z.minPadding, na = z.maxPadding, pa = 0, Ua = A(z.linkedTo),
Ga, yb, $a, x = z.events, Ca, fa = [], Da, ab, ma, ea, La = z.tickPositioner, Pa = {}, ua = {}, la = {}, oa, za, Ka, Sa = z.categories, Sc = z.labels.formatter || function() { var a = this.value, b = this.dateTimeLabelFormat; return b ? wb(b, a) : Da % 1E6 === 0 ? a / 1E6 + "M" : Da % 1E3 === 0 ? a / 1E3 + "k" : !Sa && a >= 1E3 ? Ub(a, 0) : a }, Ia = s && z.labels.staggerLines, qa = z.reversed, Qa = Sa && z.tickmarkPlacement === "between" ? 0.5 : 0; b.prototype = { addLabel: function() {
    var a = this.pos, b = z.labels, c = Sa && s && Sa.length && !b.step && !b.staggerLines && !b.rotation && wa / Sa.length || !s && wa / 2, d = a === ea[0],
f = a === ea[ea.length - 1], e = Sa && A(Sa[a]) ? Sa[a] : a, g = this.label, h = ea.info, i; ya && h && (i = z.dateTimeLabelFormats[h.higherRanks[a] || h.unitName]); this.isFirst = d; this.isLast = f; a = Sc.call({ axis: F, chart: o, isFirst: d, isLast: f, dateTimeLabelFormat: i, value: w ? rc(e) : e }); c = c && { width: T(1, y(c - 2 * (b.padding || 10))) + Ea }; c = E(c, b.style); A(g) ? g && g.attr({ text: a }).css(c) : this.label = A(a) && b.enabled ? Z.text(a, 0, 0, b.useHTML).attr({ align: b.align, rotation: b.rotation }).css(c).add(S) : null
}, getLabelSize: function() {
    var a = this.label; return a ?
(this.labelBBox = a.getBBox())[s ? "height" : "width"] : 0
}, render: function(a, b) {
    var c = this.type, d = this.label, f = this.pos, e = z.labels, g = this.gridLine, h = c ? c + "Grid" : "grid", i = c ? c + "Tick" : "tick", j = z[h + "LineWidth"], k = z[h + "LineColor"], l = z[h + "LineDashStyle"], m = z[i + "Length"], h = z[i + "Width"] || 0, o = z[i + "Color"], Oa = z[i + "Position"], i = this.mark, t = e.step, q = b && dc || Ha, n; n = s ? Za(f + Qa, null, null, b) + Ra : C + ca + (p ? (b && Ja || ha) - Q - C : 0); q = s ? q - D + ca - (p ? mb : 0) : q - Za(f + Qa, null, null, b) - Ra; if (j) {
        f = J(f + Qa, j, b); if (g === G) {
            g = { stroke: k, "stroke-width": j };
            if (l) g.dashstyle = l; if (!c) g.zIndex = 1; this.gridLine = g = j ? Z.path(f).attr(g).add(R) : null
        } !b && g && f && g.animate({ d: f })
    } if (h) Oa === "inside" && (m = -m), p && (m = -m), c = Z.crispLine([ta, n, q, ga, n + (s ? 0 : -m), q + (s ? m : 0)], h), i ? i.animate({ d: c }) : this.mark = Z.path(c).attr({ stroke: o, "stroke-width": h }).add(S); d && !isNaN(n) && (n = n + e.x - (Qa && s ? Qa * K * (qa ? -1 : 1) : 0), q = q + e.y - (Qa && !s ? Qa * K * (qa ? 1 : -1) : 0), A(e.y) || (q += N(d.styles.lineHeight) * 0.9 - d.getBBox().height / 2), Ia && (q += a / (t || 1) % Ia * 16), this.isFirst && !r(z.showFirstLabel, 1) || this.isLast && !r(z.showLastLabel,
1) ? d.hide() : d.show(), t && a % t && d.hide(), d[this.isNew ? "attr" : "animate"]({ x: n, y: q })); this.isNew = !1
}, destroy: function() { ub(this) } 
}; c.prototype = { render: function() {
    var a = this, b = (F.pointRange || 0) / 2, c = a.options, d = c.label, f = a.label, e = c.width, g = c.to, h = c.from, i = c.value, j, k = c.dashStyle, l = a.svgElem, m = [], z, o, p = c.color; o = c.zIndex; var Oa = c.events; w && (h = Eb(h), g = Eb(g), i = Eb(i)); if (e) { if (m = J(i, e), b = { stroke: p, "stroke-width": e }, k) b.dashstyle = k } else if (A(h) && A(g)) h = T(h, O - b), g = va(g, U + b), j = J(g), (m = J(h)) && j ? m.push(j[4], j[5],
j[1], j[2]) : m = null, b = { fill: p }; else return; if (A(o)) b.zIndex = o; if (l) m ? l.animate({ d: m }, null, l.onGetPath) : (l.hide(), l.onGetPath = function() { l.show() }); else if (m && m.length && (a.svgElem = l = Z.path(m).attr(b).add(), Oa)) for (z in k = function(b) { l.on(b, function(c) { Oa[b].apply(a, [c]) }) }, Oa) k(z); if (d && A(d.text) && m && m.length && hb > 0 && mb > 0) {
        d = H({ align: s && j && "center", x: s ? !j && 4 : 10, verticalAlign: !s && j && "middle", y: s ? j ? 16 : 10 : j ? 6 : -4, rotation: s && !j && 90 }, d); if (!f) a.label = f = Z.text(d.text, 0, 0).attr({ align: d.textAlign || d.align, rotation: d.rotation,
            zIndex: o
        }).css(d.style).add(); j = [m[1], m[4], r(m[6], m[1])]; m = [m[2], m[5], r(m[7], m[2])]; z = Gb(j); o = Gb(m); f.align(d, !1, { x: z, y: o, width: Ab(j) - z, height: Ab(m) - o }); f.show()
    } else f && f.hide(); return a
}, destroy: function() { ub(this); Fb(fa, this) } 
}; d.prototype = { destroy: function() { ub(this) }, setTotal: function(a) { this.cum = this.total = a }, render: function(a) {
    var b = this.options.formatter.call(this); this.label ? this.label.attr({ text: b, visibility: Ta }) : this.label = o.renderer.text(b, 0, 0).css(this.options.style).attr({ align: this.textAlign,
        rotation: this.options.rotation, visibility: Ta
    }).add(a)
}, setOffset: function(a, b) { var c = this.isNegative, d = F.translate(this.total), f = F.translate(0), f = Ba(d - f), e = o.xAxis[0].translate(this.x) + a, g = o.plotHeight, c = { x: da ? c ? d : d - f : e, y: da ? g - e - b : c ? g - d - f : g - d, width: da ? f : b, height: da ? b : f }; this.label && this.label.align(this.alignOptions, null, c).attr({ visibility: bb }) } 
}; Za = function(a, b, c, d, f) {
    var e = 1, g = 0, h = d ? B : K, d = d ? X : O, f = z.ordinal || w && f; h || (h = K); c && (e *= -1, g = I); qa && (e *= -1, g -= e * I); b ? (qa && (a = I - a), a = a / h + d, f && (a = F.lin2val(a))) :
(f && (a = F.val2lin(a)), a = e * (a - d) * h + g + e * pa); return a
}; J = function(a, b, c) { var d, f, e, a = Za(a, null, null, c), g = c && dc || Ha, h = c && Ja || ha, i, c = f = y(a + Ra); d = e = y(g - a - Ra); if (isNaN(a)) i = !0; else if (s) { if (d = pb, e = g - D, c < C || c > C + hb) i = !0 } else if (c = C, f = h - Q, d < pb || d > pb + mb) i = !0; return i ? null : Z.crispLine([ta, c, d, ga, f, e], b || 0) }; aa = function() {
    var a = U - O, b = 0, c, d; if (m) n(F.series, function(a) { b = T(b, a.pointRange); d = a.closestPointRange; !a.noSharedTooltip && A(d) && (c = A(c) ? va(c, d) : d) }), F.pointRange = b, F.closestPointRange = c; B = K; F.translationSlope =
K = I / (a + b || 1); Ra = s ? C : D; pa = K * (b / 2)
}; Fa.push(F); o[m ? "xAxis" : "yAxis"].push(F); da && m && qa === G && (qa = !0); E(F, { addPlotBand: j, addPlotLine: j, adjustTickAmount: function() { if (lb && lb[Aa] && !ya && !Sa && !Ua && z.alignTicks !== !1) { var a = oa, b = ea.length; oa = lb[Aa]; if (b < oa) { for (; ea.length < oa; ) ea.push(f(ea[ea.length - 1] + Da)); K *= (b - 1) / (oa - 1); U = ea[ea.length - 1] } if (A(a) && oa !== a) F.isDirty = !0 } }, categories: Sa, getExtremes: function() { return { min: O, max: U, dataMin: P, dataMax: Va, userMin: Ya, userMax: Xa} }, getPlotLinePath: J, getThreshold: function(a) {
    O >
a || a === null ? a = O : U < a && (a = U); return Za(a, 0, 1)
}, isXAxis: m, options: z, plotLinesAndBands: fa, getOffset: function() {
    var a = F.series.length && A(O) && A(U), c = a || r(z.showEmpty, !0), d = 0, f = 0, e = z.title, g = z.labels, h = [-1, 1, 1, -1][t], i; S || (S = Z.g("axis").attr({ zIndex: 7 }).add(), R = Z.g("grid").attr({ zIndex: z.gridZIndex || 1 }).add()); za = 0; if (a || Ua) n(ea, function(a) { Pa[a] ? Pa[a].addLabel() : Pa[a] = new b(a) }), n(ea, function(a) { if (t === 0 || t === 2 || { 1: "left", 3: "right"}[t] === g.align) za = T(Pa[a].getLabelSize(), za) }), Ia && (za += (Ia - 1) * 16); else for (i in Pa) Pa[i].destroy(),
delete Pa[i]; if (e && e.text) { if (!u) u = F.axisTitle = Z.text(e.text, 0, 0, e.useHTML).attr({ zIndex: 7, rotation: e.rotation || 0, align: e.textAlign || { low: "left", middle: "center", high: "right"}[e.align] }).css(e.style).add(), u.isNew = !0; c && (d = u.getBBox()[s ? "height" : "width"], f = r(e.margin, s ? 5 : 10)); u[c ? "show" : "hide"]() } ca = h * r(z.offset, L[t]); Ka = r(e.offset, za + f + (t !== 2 && za && h * z.labels[s ? "y" : "x"])); L[t] = T(L[t], Ka + d + h * ca)
}, render: k, setAxisSize: function() {
    var a = z.offsetLeft || 0, b = z.offsetRight || 0; C = r(z.left, $ + a); pb = r(z.top, V);
    hb = r(z.width, wa - a + b); mb = r(z.height, xa); D = Ha - mb - pb; Q = ha - hb - C; I = s ? hb : mb; F.left = C; F.top = pb; F.len = I
}, setAxisTranslation: aa, setCategories: function(b, c) { F.categories = a.categories = Sa = b; n(F.series, function(a) { a.translate(); a.setTooltipPoints(!0) }); F.isDirty = !0; r(c, !0) && o.redraw() }, setExtremes: function(a, b, c, d) { c = r(c, !0); Y(F, "setExtremes", { min: a, max: b }, function() { Ya = a; Xa = b; c && o.redraw(d) }) }, setScale: function() {
    var a, b, c; X = O; ka = U; Ma = I; I = s ? hb : mb; n(F.series, function(a) {
        if (a.isDirtyData || a.isDirty || a.xAxis.isDirty) c =
!0
    }); if (I !== Ma || c || Ua || Ya !== ba || Xa !== ia) { e(); i(); ba = Ya; ia = Xa; if (!m) for (a in q) for (b in q[a]) q[a][b].cum = q[a][b].total; if (!F.isDirty) F.isDirty = o.isDirtyBox || O !== X || U !== ka } 
}, setTickPositions: i, translate: Za, redraw: function() { xb.resetTracker && xb.resetTracker(); k(); n(fa, function(a) { a.render() }); n(F.series, function(a) { a.isDirty = !0 }) }, removePlotBand: l, removePlotLine: l, reversed: qa, series: [], stacks: q, destroy: function() {
    var a; ra(F); for (a in q) ub(q[a]), q[a] = null; if (F.stackTotalGroup) F.stackTotalGroup = F.stackTotalGroup.destroy();
    n([Pa, ua, la, fa], function(a) { ub(a) }); n([Wa, S, R, u], function(a) { a && a.destroy() }); Wa = S = R = u = null
} 
}); for (Ca in x) W(F, Ca, x[Ca]); if (w) F.val2lin = Eb, F.lin2val = rc
        } function d(a) {
            function b() { var a = this.points || sb(this), c = a[0].series, d; d = [c.tooltipHeaderFormatter(a[0].key)]; n(a, function(a) { c = a.series; d.push(c.tooltipFormatter && c.tooltipFormatter(a) || a.point.tooltipFormatter(c.tooltipOptions.pointFormat)) }); return d.join("") } function c(a, b) {
                l = m ? a : (2 * l + a) / 3; p = m ? b : (p + b) / 2; s.attr({ x: l, y: p }); cb = Ba(a - l) > 1 || Ba(b - p) >
1 ? function() { c(a, b) } : null
            } function d() { if (!m) { var a = o.hoverPoints; s.hide(); a && n(a, function(a) { a.setState() }); o.hoverPoints = null; m = !0 } } var f, e = a.borderWidth, g = a.crosshairs, h = [], i = a.style, j = a.shared, k = N(i.padding), m = !0, l = 0, p = 0; i.padding = 0; var s = Z.label("", 0, 0).attr({ padding: k, fill: a.backgroundColor, "stroke-width": e, r: a.borderRadius, zIndex: 8 }).css(i).hide().add().shadow(a.shadow); return { shared: j, refresh: function(e) {
                var i, k, l, p, q = {}, t = []; l = e.tooltipPos; i = a.formatter || b; q = o.hoverPoints; j && (!e.series ||
!e.series.noSharedTooltip) ? (p = 0, q && n(q, function(a) { a.setState() }), o.hoverPoints = e, n(e, function(a) { a.setState(fb); p += a.plotY; t.push(a.getLabelConfig()) }), k = e[0].plotX, p = y(p) / e.length, q = { x: e[0].category }, q.points = t, e = e[0]) : q = e.getLabelConfig(); q = i.call(q); f = e.series; k = r(k, e.plotX); p = r(p, e.plotY); i = y(l ? l[0] : da ? wa - p : k); k = y(l ? l[1] : da ? xa - k : p); l = j || !f.isCartesian || f.tooltipOutsidePlot || Qa(i, k); q === !1 || !l ? d() : (m && (s.show(), m = !1), s.attr({ text: q }), s.attr({ stroke: a.borderColor || e.color || f.color || "#606060" }),
k = Nc(s.width, s.height, $, V, wa, xa, { x: i, y: k }, r(a.distance, 12), da), c(y(k.x), y(k.y))); if (g) { g = sb(g); for (k = g.length; k--; ) if (l = e.series[k ? "yAxis" : "xAxis"], g[k] && l) if (l = l.getPlotLinePath(e[k ? "y" : "x"], 1), h[k]) h[k].attr({ d: l, visibility: bb }); else { i = { "stroke-width": g[k].width || 1, stroke: g[k].color || "#C0C0C0", zIndex: g[k].zIndex || 2 }; if (g[k].dashStyle) i.dashstyle = g[k].dashStyle; h[k] = Z.path(l).attr(i).add() } } 
            }, hide: d, hideCrosshairs: function() { n(h, function(a) { a && a.hide() }) }, destroy: function() {
                n(h, function(a) { a && a.destroy() });
                s && (s = s.destroy())
            } }
            } function e(a) {
                function b(a) { var c, d, e, a = a || fa.event; if (!a.target) a.target = a.srcElement; if (a.originalEvent) a = a.originalEvent; if (a.event) a = a.event; c = a.touches ? a.touches.item(0) : a; qb = Bc(C); d = qb.left; e = qb.top; Tb ? (d = a.x, c = a.y) : (d = c.pageX - d, c = c.pageY - e); return E(a, { chartX: y(d), chartY: y(c) }) } function c(a) { var b = { xAxis: [], yAxis: [] }; n(Fa, function(c) { var d = c.translate, e = c.isXAxis; b[e ? "xAxis" : "yAxis"].push({ axis: c, value: d((da ? !e : e) ? a.chartX - $ : xa - a.chartY + V, !0) }) }); return b } function e() {
                    var a =
o.hoverSeries, b = o.hoverPoint; if (b) b.onMouseOut(); if (a) a.onMouseOut(); ua && (ua.hide(), ua.hideCrosshairs()); db = null
                } function f() {
                    if (l) { var a = { xAxis: [], yAxis: [] }, b = l.getBBox(), c = b.x - $, d = b.y - V; k && (n(Fa, function(e) { if (e.options.zoomEnabled !== !1) { var f = e.translate, g = e.isXAxis, h = da ? !g : g, i = f(h ? c : xa - d - b.height, !0, 0, 0, 1), f = f(h ? c + b.width : xa - d, !0, 0, 0, 1); a[g ? "xAxis" : "yAxis"].push({ axis: e, min: va(i, f), max: T(i, f) }) } }), Y(o, "selection", a, ob)); l = l.destroy() } J(C, { cursor: "auto" }); o.mouseIsDown = za = k = !1; ra(S, Ga ? "touchend" :
"mouseup", f)
                } function g(a) { var b = A(a.pageX) ? a.pageX : a.page.x, a = A(a.pageX) ? a.pageY : a.page.y; qb && !Qa(b - qb.left - $, a - qb.top - V) && e() } function h() { e(); qb = null } var i, j, k, l, m = v.zoomType, p = /x/.test(m), s = /y/.test(m), q = p && !da || s && da, t = s && !da || p && da; Ua = function() { Xa ? (Xa.translate($, V), da && Xa.attr({ width: o.plotWidth, height: o.plotHeight }).invert()) : o.trackerGroup = Xa = Z.g("tracker").attr({ zIndex: 9 }).add() }; Ua(); if (a.enabled) o.tooltip = ua = d(a), jb = setInterval(function() { cb && cb() }, 32); (function() {
                    C.onmousedown = function(a) {
                        a =
b(a); !Ga && a.preventDefault && a.preventDefault(); o.mouseIsDown = za = !0; o.mouseDownX = i = a.chartX; j = a.chartY; W(S, Ga ? "touchend" : "mouseup", f)
                    }; var d = function(c) {
                        if (!c || !(c.touches && c.touches.length > 1)) {
                            c = b(c); if (!Ga) c.returnValue = !1; var d = c.chartX, e = c.chartY, f = !Qa(d - $, e - V); Ga && c.type === "touchstart" && (R(c.target, "isTracker") ? o.runTrackerClick || c.preventDefault() : !qa && !f && c.preventDefault()); f && (d < $ ? d = $ : d > $ + wa && (d = $ + wa), e < V ? e = V : e > V + xa && (e = V + xa)); if (za && c.type !== "touchstart") {
                                if (k = Math.sqrt(Math.pow(i - d, 2) + Math.pow(j -
e, 2)), k > 10) { var g = Qa(i - $, j - V); if (Ca && (p || s) && g) l || (l = Z.rect($, V, q ? 1 : wa, t ? 1 : xa, 0).attr({ fill: v.selectionMarkerFill || "rgba(69,114,167,0.25)", zIndex: 7 }).add()); l && q && (c = d - i, l.attr({ width: Ba(c), x: (c > 0 ? 0 : c) + i })); l && t && (e -= j, l.attr({ height: Ba(e), y: (e > 0 ? 0 : e) + j })); g && !l && v.panning && o.pan(d) } 
                            } else if (!f) {
                                var h, d = o.hoverPoint, e = o.hoverSeries, m, n, g = ha, u = da ? c.chartY : c.chartX - $; if (ua && a.shared && (!e || !e.noSharedTooltip)) {
                                    h = []; m = P.length; for (n = 0; n < m; n++) if (P[n].visible && P[n].options.enableMouseTracking !== !1 && !P[n].noSharedTooltip &&
P[n].tooltipPoints.length) c = P[n].tooltipPoints[u], c._dist = Ba(u - c.plotX), g = va(g, c._dist), h.push(c); for (m = h.length; m--; ) h[m]._dist > g && h.splice(m, 1); if (h.length && h[0].plotX !== db) ua.refresh(h), db = h[0].plotX
                                } if (e && e.tracker && (c = e.tooltipPoints[u]) && c !== d) c.onMouseOver()
                            } return f || !Ca
                        } 
                    }; C.onmousemove = d; W(C, "mouseleave", h); W(S, "mousemove", g); C.ontouchstart = function(a) { if (p || s) C.onmousedown(a); d(a) }; C.ontouchmove = d; C.ontouchend = function() { k && e() }; C.onclick = function(a) {
                        var d = o.hoverPoint, a = b(a); a.cancelBubble =
!0; if (!k) if (d && R(a.target, "isTracker")) { var e = d.plotX, f = d.plotY; E(d, { pageX: qb.left + $ + (da ? wa - f : e), pageY: qb.top + V + (da ? xa - e : f) }); Y(d.series, "click", E(a, { point: d })); d.firePointEvent("click", a) } else E(a, c(a)), Qa(a.chartX - $, a.chartY - V) && Y(o, "click", a); k = !1
                    } 
                })(); E(this, { zoomX: p, zoomY: s, resetTracker: e, normalizeMouseEvent: b, destroy: function() {
                    if (o.trackerGroup) o.trackerGroup = Xa = o.trackerGroup.destroy(); ra(C, "mouseleave", h); ra(S, "mousemove", g); C.onclick = C.onmousedown = C.onmousemove = C.ontouchstart = C.ontouchend =
C.ontouchmove = null
                } 
                })
            } function f(a) { var b = a.type || v.type || v.defaultSeriesType, c = ba[b], d = o.hasRendered; if (d) if (da && b === "column") c = ba.bar; else if (!da && b === "bar") c = ba.column; b = new c; b.init(o, a); !d && b.inverted && (da = !0); if (b.isCartesian) Ca = b.isCartesian; P.push(b); return b } function g() { v.alignTicks !== !1 && n(Fa, function(a) { a.adjustTickAmount() }); lb = null } function h(a) {
                var b = o.isDirtyLegend, c, d = o.isDirtyBox, e = P.length, f = e, h = o.clipRect; for (Ib(a, o); f--; ) if (a = P[f], a.isDirty && a.options.stacking) { c = !0; break } if (c) for (f =
e; f--; ) if (a = P[f], a.options.stacking) a.isDirty = !0; n(P, function(a) { a.isDirty && a.options.legendType === "point" && (b = !0) }); if (b && Ya.renderLegend) Ya.renderLegend(), o.isDirtyLegend = !1; Ca && (Ka || (lb = null, n(Fa, function(a) { a.setScale() })), g(), Ia(), n(Fa, function(a) { Y(a, "afterSetExtremes", a.getExtremes()); a.isDirty && a.redraw() })); d && (eb(), Ua(), h && (Jb(h), h.animate({ width: o.plotSizeX, height: o.plotSizeY + 1 }))); n(P, function(a) { a.isDirty && a.visible && (!a.isCartesian || a.xAxis) && a.redraw() }); xb && xb.resetTracker && xb.resetTracker();
                Y(o, "redraw")
            } function j() { var b = a.xAxis || {}, d = a.yAxis || {}, b = sb(b); n(b, function(a, b) { a.index = b; a.isX = !0 }); d = sb(d); n(d, function(a, b) { a.index = b }); b = b.concat(d); n(b, function(a) { new c(a) }); g() } function k() { var a = X.lang, b = v.resetZoomButton, c = b.relativeTo === "plot" && { x: $, y: V, width: wa, height: xa }; o.resetZoomButton = Z.button(a.resetZoom, null, null, tb, b.theme).attr({ align: b.position.align, title: a.resetZoomTitle }).add().align(b.position, !1, c) } function i(b, c) {
                Za = H(a.title, b); D = H(a.subtitle, c); n([["title", b, Za],
["subtitle", c, D]], function(a) { var b = a[0], c = o[b], d = a[1], a = a[2]; c && d && (c = c.destroy()); a && a.text && !c && (o[b] = Z.text(a.text, 0, 0, a.useHTML).attr({ align: a.align, "class": gb + b, zIndex: 1 }).css(a.style).add().align(a, !1, B)) })
            } function l() {
                Q = v.renderTo; pa = gb + oc++; zb(Q) && (Q = S.getElementById(Q)); Q.innerHTML = ""; Q.offsetWidth || (M = Q.cloneNode(0), J(M, { position: Cb, top: "-9999px", display: "" }), S.body.appendChild(M)); ka = (M || Q).offsetWidth; yb = (M || Q).offsetHeight; o.chartWidth = ha = v.width || ka || 600; o.chartHeight = Ha = v.height ||
(yb > 19 ? yb : 400); o.container = C = na(vb, { className: gb + "container" + (v.className ? " " + v.className : ""), id: pa }, E({ position: Cc, overflow: Ta, width: ha + Ea, height: Ha + Ea, textAlign: "left", lineHeight: "normal" }, v.style), M || Q); o.renderer = Z = v.forExport ? new Mb(C, ha, Ha, !0) : new Nb(C, ha, Ha); var a, b; Dc && C.getBoundingClientRect && (a = function() { J(C, { left: 0, top: 0 }); b = C.getBoundingClientRect(); J(C, { left: -(b.left - N(b.left)) + Ea, top: -(b.top - N(b.top)) + Ea }) }, a(), W(fa, "resize", a), W(o, "destroy", function() { ra(fa, "resize", a) }))
            } function m() {
                function a(c) {
                    var d =
v.width || Q.offsetWidth, e = v.height || Q.offsetHeight, c = c.target; if (d && e && (c === fa || c === S)) { if (d !== ka || e !== yb) clearTimeout(b), b = setTimeout(function() { nb(d, e, !1) }, 100); ka = d; yb = e } 
                } var b; W(fa, "resize", a); W(o, "destroy", function() { ra(fa, "resize", a) })
            } function p() { o && Y(o, "endResize", null, function() { Ka -= 1 }) } function t() { for (var b = da || v.inverted || v.type === "bar" || v.defaultSeriesType === "bar", c = a.series, d = c && c.length; !b && d--; ) c[d].type === "bar" && (b = !0); o.inverted = da = b } function x() {
                var b = a.labels, c = a.credits, d; i();
                Ya = o.legend = new Bb; n(Fa, function(a) { a.setScale() }); Ia(); n(Fa, function(a) { a.setTickPositions(!0) }); g(); Ia(); eb(); Ca && n(Fa, function(a) { a.render() }); if (!o.seriesGroup) o.seriesGroup = Z.g("series-group").attr({ zIndex: 3 }).add(); n(P, function(a) { a.translate(); a.setTooltipPoints(); a.render() }); b.items && n(b.items, function() { var a = E(b.style, this.style), c = N(a.left) + $, d = N(a.top) + V + 12; delete a.left; delete a.top; Z.text(this.html, c, d).attr({ zIndex: 2 }).css(a).add() }); if (c.enabled && !o.credits) d = c.href, o.credits = Z.text(c.text,
0, 0).on("click", function() { if (d) location.href = d }).attr({ align: c.position.align, zIndex: 8 }).css(c.style).add().align(c.position); Ua(); o.hasRendered = !0; M && (Q.appendChild(C), Hb(M))
            } function u() {
                if (!Ob && fa == fa.top && S.readyState !== "complete") S.attachEvent("onreadystatechange", function() { S.detachEvent("onreadystatechange", u); S.readyState === "complete" && u() }); else {
                    l(); Y(o, "init"); if (Highcharts.RangeSelector && a.rangeSelector.enabled) o.rangeSelector = new Highcharts.RangeSelector(o); ib(); kb(); t(); j(); n(a.series ||
[], function(a) { f(a) }); if (Highcharts.Scroller && (a.navigator.enabled || a.scrollbar.enabled)) o.scroller = new Highcharts.Scroller(o); o.render = x; o.tracker = xb = new e(a.tooltip); x(); b && b.apply(o, [o]); n(o.callbacks, function(a) { a.apply(o, [o]) }); Y(o, "load")
                } 
            } var w = a.series; a.series = null; a = H(X, a); a.series = w; var v = a.chart, w = v.margin, w = rb(w) ? w : [w, w, w, w], q = r(v.marginTop, w[0]), ca = r(v.marginRight, w[1]), ya = r(v.marginBottom, w[2]), I = r(v.marginLeft, w[3]), Ra = v.spacingTop, s = v.spacingRight, Aa = v.spacingBottom, K = v.spacingLeft,
B, Za, D, V, aa, Wa, $, L, Q, M, C, pa, ka, yb, ha, Ha, Ja, dc, ia, ja, ma, Kb, o = this, qa = (w = v.events) && !!w.click, La, Qa, ua, za, Ma, hb, Db, xa, wa, xb, Xa, Ua, Ya, $a, ab, qb, Ca = v.showAxes, Ka = 0, Fa = [], lb, P = [], da, Z, cb, jb, db, eb, Ia, ib, kb, nb, ob, tb, Bb = function() {
    function a(b, c) { var d = b.legendItem, e = b.legendLine, g = b.legendSymbol, h = p.color, i = c ? f.itemStyle.color : h, h = c ? b.color : h; d && d.css({ fill: i }); e && e.attr({ stroke: h }); g && g.attr({ stroke: h, fill: h }) } function b(a, c, d) {
        var e = a.legendItem, f = a.legendLine, g = a.legendSymbol, a = a.checkbox; e && e.attr({ x: c,
            y: d
        }); f && f.translate(c, d - 4); g && g.attr({ x: c + g.xOff, y: d + g.yOff }); if (a) a.x = c, a.y = d
    } function c() { n(j, function(a) { var b = a.checkbox, c = K.alignAttr; b && J(b, { left: c.translateX + a.legendItemWidth + b.x - 40 + Ea, top: c.translateY + b.y - 11 + Ea }) }) } function d(c) {
        var e, j, k, o, q = c.legendItem; o = c.series || c; var n = o.options, w = n && n.borderWidth || 0; if (!q) {
            o = /^(bar|pie|area|column)$/.test(o.type); c.legendItem = q = Z.text(f.labelFormatter.call(c), 0, 0).css(c.visible ? l : p).on("mouseover", function() { c.setState(fb); q.css(m) }).on("mouseout",
function() { q.css(c.visible ? l : p); c.setState() }).on("click", function() { var a = function() { c.setVisible() }; c.firePointEvent ? c.firePointEvent("legendItemClick", null, a) : Y(c, "legendItemClick", null, a) }).attr({ zIndex: 2 }).add(K); if (!o && n && n.lineWidth) { var Aa = { "stroke-width": n.lineWidth, zIndex: 2 }; if (n.dashStyle) Aa.dashstyle = n.dashStyle; c.legendLine = Z.path([ta, -h - i, 0, ga, -i, 0]).attr(Aa).add(K) } if (o) k = Z.rect(e = -h - i, j = -11, h, 12, 2).attr({ zIndex: 3 }).add(K); else if (n && n.marker && n.marker.enabled) k = n.marker.radius, k = Z.symbol(c.symbol,
e = -h / 2 - i - k, j = -4 - k, 2 * k, 2 * k).attr(c.pointAttr[la]).attr({ zIndex: 3 }).add(K); if (k) k.xOff = e + w % 2 / 2, k.yOff = j + w % 2 / 2; c.legendSymbol = k; a(c, c.visible); if (n && n.showCheckbox) c.checkbox = na("input", { type: "checkbox", checked: c.selected, defaultChecked: c.selected }, f.itemCheckboxStyle, C), W(c.checkbox, "click", function(a) { Y(c, "checkboxClick", { checked: a.target.checked }, function() { c.select() }) })
        } e = q.getBBox(); j = c.legendItemWidth = f.itemWidth || h + i + e.width + s; ya = e.height; if (g && u - t + j > (y || ha - 2 * s - t)) u = t, x += ca + ya + r; v = x + r; b(c, u,
x); g ? u += j : x += ca + ya + r; Ma = y || T(g ? u - t : j, Ma)
    } function e() {
        u = t; x = s + ca + q - 5; v = Ma = 0; K || (K = Z.g("legend").attr({ zIndex: 10 }).add()); j = []; n(A, function(a) { var b = a.options; b.showInLegend && (j = j.concat(a.legendItems || (b.legendType === "point" ? a.data : a))) }); Oc(j, function(a, b) { return (a.options.legendIndex || 0) - (b.options.legendIndex || 0) }); Ra && j.reverse(); n(j, d); $a = y || Ma; ab = v - q + ya; if (Aa || I) {
            $a += 2 * s; ab += 2 * s; if (w) { if ($a > 0 && ab > 0) w[w.isNew ? "attr" : "animate"](w.crisp(null, null, null, $a, ab)), w.isNew = !1 } else w = Z.rect(0, 0, $a, ab,
f.borderRadius, Aa || 0).attr({ stroke: f.borderColor, "stroke-width": Aa || 0, fill: I || oa }).add(K).shadow(f.shadow), w.isNew = !0; w[j.length ? "show" : "hide"]()
        } for (var a = ["left", "right", "top", "bottom"], b, g = 4; g--; ) b = a[g], k[b] && k[b] !== "auto" && (f[g < 2 ? "align" : "verticalAlign"] = b, f[g < 2 ? "x" : "y"] = N(k[b]) * (g % 2 ? -1 : 1)); j.length && K.align(E(f, { width: $a, height: ab }), !0, B); Ka || c()
    } var f = o.options.legend; if (f.enabled) {
        var g = f.layout === "horizontal", h = f.symbolWidth, i = f.symbolPadding, j, k = f.style, l = f.itemStyle, m = f.itemHoverStyle, p =
H(l, f.itemHiddenStyle), s = f.padding || N(k.padding), q = 18, t = 4 + s + h + i, u, x, v, ya = 0, ca = f.itemMarginTop || 0, r = f.itemMarginBottom || 0, w, Aa = f.borderWidth, I = f.backgroundColor, K, Ma, y = f.width, A = o.series, Ra = f.reversed; e(); W(o, "endResize", c); return { colorizeItem: a, destroyItem: function(a) { var b = a.checkbox; n(["legendItem", "legendLine", "legendSymbol"], function(b) { a[b] && a[b].destroy() }); b && Hb(a.checkbox) }, renderLegend: e, destroy: function() { w && (w = w.destroy()); K && (K = K.destroy()) } }
    } 
}; Qa = function(a, b) {
    return a >= 0 && a <= wa && b >=
0 && b <= xa
}; tb = function() { var a = o.resetZoomButton; Y(o, "selection", { resetSelection: !0 }, ob); if (a) o.resetZoomButton = a.destroy() }; ob = function(a) { var b = o.pointCount < 100, c; o.resetZoomEnabled !== !1 && !o.resetZoomButton && k(); !a || a.resetSelection ? n(Fa, function(a) { a.options.zoomEnabled !== !1 && (a.setExtremes(null, null, !1), c = !0) }) : n(a.xAxis.concat(a.yAxis), function(a) { var b = a.axis; if (o.tracker[b.isXAxis ? "zoomX" : "zoomY"]) b.setExtremes(a.min, a.max, !1), c = !0 }); c && h(!0, b) }; o.pan = function(a) {
    var b = o.xAxis[0], c = o.mouseDownX,
d = b.pointRange / 2, e = b.getExtremes(), f = b.translate(c - a, !0) + d, c = b.translate(c + wa - a, !0) - d; (d = o.hoverPoints) && n(d, function(a) { a.setState() }); f > va(e.dataMin, e.min) && c < T(e.dataMax, e.max) && b.setExtremes(f, c, !0, !1); o.mouseDownX = a; J(C, { cursor: "move" })
}; Ia = function() {
    var b = a.legend, c = r(b.margin, 10), d = b.x, e = b.y, f = b.align, g = b.verticalAlign, h; ib(); if ((o.title || o.subtitle) && !A(q)) (h = T(o.title && !Za.floating && !Za.verticalAlign && Za.y || 0, o.subtitle && !D.floating && !D.verticalAlign && D.y || 0)) && (V = T(V, h + r(Za.margin, 15) +
Ra)); b.enabled && !b.floating && (f === "right" ? A(ca) || (aa = T(aa, $a - d + c + s)) : f === "left" ? A(I) || ($ = T($, $a + d + c + K)) : g === "top" ? A(q) || (V = T(V, ab + e + c + Ra)) : g === "bottom" && (A(ya) || (Wa = T(Wa, ab - e + c + Aa)))); o.extraBottomMargin && (Wa += o.extraBottomMargin); o.extraTopMargin && (V += o.extraTopMargin); Ca && n(Fa, function(a) { a.getOffset() }); A(I) || ($ += L[3]); A(q) || (V += L[0]); A(ya) || (Wa += L[2]); A(ca) || (aa += L[1]); kb()
}; nb = function(a, b, c) {
    var d = o.title, e = o.subtitle; Ka += 1; Ib(c, o); dc = Ha; Ja = ha; if (A(a)) o.chartWidth = ha = y(a); if (A(b)) o.chartHeight =
Ha = y(b); J(C, { width: ha + Ea, height: Ha + Ea }); Z.setSize(ha, Ha, c); wa = ha - $ - aa; xa = Ha - V - Wa; lb = null; n(Fa, function(a) { a.isDirty = !0; a.setScale() }); n(P, function(a) { a.isDirty = !0 }); o.isDirtyLegend = !0; o.isDirtyBox = !0; Ia(); d && d.align(null, null, B); e && e.align(null, null, B); h(c); dc = null; Y(o, "resize"); Lb === !1 ? p() : setTimeout(p, Lb && Lb.duration || 500)
}; kb = function() {
    o.plotLeft = $ = y($); o.plotTop = V = y(V); o.plotWidth = wa = y(ha - $ - aa); o.plotHeight = xa = y(Ha - V - Wa); o.plotSizeX = da ? xa : wa; o.plotSizeY = da ? wa : xa; B = { x: K, y: Ra, width: ha - K - s, height: Ha -
Ra - Aa
    }; n(Fa, function(a) { a.setAxisSize(); a.setAxisTranslation() })
}; ib = function() { V = r(q, Ra); aa = r(ca, s); Wa = r(ya, Aa); $ = r(I, K); L = [0, 0, 0, 0] }; eb = function() {
    var a = v.borderWidth || 0, b = v.backgroundColor, c = v.plotBackgroundColor, d = v.plotBackgroundImage, e, f = { x: $, y: V, width: wa, height: xa }; e = a + (v.shadow ? 8 : 0); if (a || b) ia ? ia.animate(ia.crisp(null, null, null, ha - e, Ha - e)) : ia = Z.rect(e / 2, e / 2, ha - e, Ha - e, v.borderRadius, a).attr({ stroke: v.borderColor, "stroke-width": a, fill: b || oa }).add().shadow(v.shadow); c && (ja ? ja.animate(f) : ja = Z.rect($,
V, wa, xa, 0).attr({ fill: c }).add().shadow(v.plotShadow)); d && (ma ? ma.animate(f) : ma = Z.image(d, $, V, wa, xa).add()); v.plotBorderWidth && (Kb ? Kb.animate(Kb.crisp(null, $, V, wa, xa)) : Kb = Z.rect($, V, wa, xa, 0, v.plotBorderWidth).attr({ stroke: v.plotBorderColor, "stroke-width": v.plotBorderWidth, zIndex: 4 }).add()); o.isDirtyBox = !1
}; v.reflow !== !1 && W(o, "load", m); if (w) for (La in w) W(o, La, w[La]); o.options = a; o.series = P; o.xAxis = []; o.yAxis = []; o.addSeries = function(a, b, c) {
    var d; a && (Ib(c, o), b = r(b, !0), Y(o, "addSeries", { options: a }, function() {
        d =
f(a); d.isDirty = !0; o.isDirtyLegend = !0; b && o.redraw()
    })); return d
}; o.animation = r(v.animation, !0); o.Axis = c; o.destroy = function() {
    var b, c = C && C.parentNode; if (o !== null) {
        Y(o, "destroy"); ra(o); for (b = Fa.length; b--; ) Fa[b] = Fa[b].destroy(); for (b = P.length; b--; ) P[b] = P[b].destroy(); n("title,subtitle,seriesGroup,clipRect,credits,tracker,scroller,rangeSelector".split(","), function(a) { var b = o[a]; b && (o[a] = b.destroy()) }); n([ia, Kb, ja, Ya, ua, Z, xb], function(a) { a && a.destroy && a.destroy() }); ia = Kb = ja = Ya = ua = Z = xb = null; if (C) C.innerHTML =
"", ra(C), c && Hb(C), C = null; clearInterval(jb); for (b in o) delete o[b]; a = o = null
    } 
}; o.get = function(a) { var b, c, d; for (b = 0; b < Fa.length; b++) if (Fa[b].options.id === a) return Fa[b]; for (b = 0; b < P.length; b++) if (P[b].options.id === a) return P[b]; for (b = 0; b < P.length; b++) { d = P[b].points || []; for (c = 0; c < d.length; c++) if (d[c].id === a) return d[c] } return null }; o.getSelectedPoints = function() { var a = []; n(P, function(b) { a = a.concat(pc(b.points, function(a) { return a.selected })) }); return a }; o.getSelectedSeries = function() { return pc(P, function(a) { return a.selected }) };
            o.hideLoading = function() { Ma && ec(Ma, { opacity: 0 }, { duration: a.loading.hideDuration || 100, complete: function() { J(Ma, { display: oa }) } }); Db = !1 }; o.initSeries = f; o.isInsidePlot = Qa; o.redraw = h; o.setSize = nb; o.setTitle = i; o.showLoading = function(b) {
                var c = a.loading; Ma || (Ma = na(vb, { className: gb + "loading" }, E(c.style, { left: $ + Ea, top: V + Ea, width: wa + Ea, height: xa + Ea, zIndex: 10, display: oa }), C), hb = na("span", null, c.labelStyle, Ma)); hb.innerHTML = b || a.lang.loading; Db || (J(Ma, { opacity: 0, display: "" }), ec(Ma, { opacity: c.style.opacity }, { duration: c.showDuration ||
0
                }), Db = !0)
            }; o.pointCount = 0; o.counters = new yc; u()
        } var G, S = document, fa = window, sa = Math, y = sa.round, Na = sa.floor, bc = sa.ceil, T = sa.max, va = sa.min, Ba = sa.abs, ia = sa.cos, ma = sa.sin, Ia = sa.PI, Ec = Ia * 2 / 360, ib = navigator.userAgent, Tb = /msie/i.test(ib) && !fa.opera, Pb = S.documentMode === 8, Fc = /AppleWebKit/.test(ib), Dc = /Firefox/.test(ib), Ob = !!S.createElementNS && !!S.createElementNS("http://www.w3.org/2000/svg", "svg").createSVGRect, Tc = Dc && parseInt(ib.split("Firefox/")[1], 10) < 4, Nb, Ga = S.documentElement.ontouchstart !== G, Gc = {}, oc =
0, Zb, X, wb, Lb, Qb, D, vb = "div", Cb = "absolute", Cc = "relative", Ta = "hidden", gb = "highcharts-", bb = "visible", Ea = "px", oa = "none", ta = "M", ga = "L", Hc = "rgba(192,192,192," + (Ob ? 1.0E-6 : 0.0020) + ")", la = "", fb = "hover", tb = "millisecond", kb = "second", db = "minute", za = "hour", ka = "day", Ca = "week", Ka = "month", La = "year", Yb, jc, kc, mc, eb, Wb, Xb, uc, vc, lc, wc, xc, B = fa.HighchartsAdapter, Ja = B || {}, n = Ja.each, pc = Ja.grep, Bc = Ja.offset, nb = Ja.map, H = Ja.merge, W = Ja.addEvent, ra = Ja.removeEvent, Y = Ja.fireEvent, ec = Ja.animate, Jb = Ja.stop, ba = {}; fa.Highcharts = {};
        wb = function(a, b, c) {
            function d(a, b) { a = a.toString().replace(/^([0-9])$/, "0$1"); b === 3 && (a = a.toString().replace(/^([0-9]{2})$/, "0$1")); return a } if (!A(b) || isNaN(b)) return "Invalid date"; var a = r(a, "%Y-%m-%d %H:%M:%S"), e = new Date(b), f, g = e[kc](), h = e[mc](), j = e[eb](), k = e[Wb](), i = e[Xb](), l = X.lang, m = l.weekdays, b = { a: m[h].substr(0, 3), A: m[h], d: d(j), e: j, b: l.shortMonths[k], B: l.months[k], m: d(k + 1), y: i.toString().substr(2, 2), Y: i, H: d(g), I: d(g % 12 || 12), l: g % 12 || 12, M: d(e[jc]()), p: g < 12 ? "AM" : "PM", P: g < 12 ? "am" : "pm", S: d(e.getSeconds()),
                L: d(b % 1E3, 3)
            }; for (f in b) a = a.replace("%" + f, b[f]); return c ? a.substr(0, 1).toUpperCase() + a.substr(1) : a
        }; yc.prototype = { wrapColor: function(a) { if (this.color >= a) this.color = 0 }, wrapSymbol: function(a) { if (this.symbol >= a) this.symbol = 0 } }; D = ja(tb, 1, kb, 1E3, db, 6E4, za, 36E5, ka, 864E5, Ca, 6048E5, Ka, 2592E6, La, 31556952E3); Qb = { init: function(a, b, c) {
            var b = b || "", d = a.shift, e = b.indexOf("C") > -1, f = e ? 7 : 3, g, b = b.split(" "), c = [].concat(c), h, j, k = function(a) { for (g = a.length; g--; ) a[g] === ta && a.splice(g + 1, 0, a[g + 1], a[g + 2], a[g + 1], a[g + 2]) };
            e && (k(b), k(c)); a.isArea && (h = b.splice(b.length - 6, 6), j = c.splice(c.length - 6, 6)); d === 1 && (c = [].concat(c).splice(0, f).concat(c)); a.shift = 0; if (b.length) for (a = c.length; b.length < a; ) d = [].concat(b).splice(b.length - f, f), e && (d[f - 6] = d[f - 2], d[f - 5] = d[f - 1]), b = b.concat(d); h && (b = b.concat(h), c = c.concat(j)); return [b, c]
        }, step: function(a, b, c, d) { var e = [], f = a.length; if (c === 1) e = d; else if (f === b.length && c < 1) for (; f--; ) d = parseFloat(a[f]), e[f] = isNaN(d) ? a[f] : c * parseFloat(b[f] - d) + d; else e = b; return e } 
        }; B && B.init && B.init(Qb); if (!B &&
fa.jQuery) {
            var qa = jQuery, n = function(a, b) { for (var c = 0, d = a.length; c < d; c++) if (b.call(a[c], a[c], c, a) === !1) return c }, pc = qa.grep, nb = function(a, b) { for (var c = [], d = 0, e = a.length; d < e; d++) c[d] = b.call(a[d], a[d], d, a); return c }, H = function() { var a = arguments; return qa.extend(!0, null, a[0], a[1], a[2], a[3]) }, Bc = function(a) { return qa(a).offset() }, W = function(a, b, c) { qa(a).bind(b, c) }, ra = function(a, b, c) { var d = S.removeEventListener ? "removeEventListener" : "detachEvent"; S[d] && !a[d] && (a[d] = function() { }); qa(a).unbind(b, c) }, Y = function(a,
b, c, d) { var e = qa.Event(b), f = "detached" + b, g; E(e, c); a[b] && (a[f] = a[b], a[b] = null); n(["preventDefault", "stopPropagation"], function(a) { var b = e[a]; e[a] = function() { try { b.call(e) } catch (c) { a === "preventDefault" && (g = !0) } } }); qa(a).trigger(e); a[f] && (a[b] = a[f], a[f] = null); d && !e.isDefaultPrevented() && !g && d(e) }, ec = function(a, b, c) { var d = qa(a); if (b.d) a.toD = b.d, b.d = 1; d.stop(); d.animate(b, c) }, Jb = function(a) { qa(a).stop() }; qa.extend(qa.easing, { easeOutQuad: function(a, b, c, d, e) { return -d * (b /= e) * (b - 2) + c } }); var Ic = jQuery.fx, Jc =
Ic.step; n(["cur", "_default", "width", "height"], function(a, b) { var c = b ? Jc : Ic.prototype, d = c[a], e; d && (c[a] = function(a) { a = b ? a : this; e = a.elem; return e.attr ? e.attr(a.prop, a.now) : d.apply(this, arguments) }) }); Jc.d = function(a) { var b = a.elem; if (!a.started) { var c = Qb.init(b, b.d, b.toD); a.start = c[0]; a.end = c[1]; a.started = !0 } b.attr("d", Qb.step(a.start, a.end, a.pos, b.toD)) } 
        } B = { enabled: !0, align: "center", x: 0, y: 15, style: { color: "#666", fontSize: "11px", lineHeight: "14px"} }; X = { colors: "#4572A7,#AA4643,#89A54E,#80699B,#3D96AE,#DB843D,#92A8CD,#A47D7C,#B5CA92".split(","),
            symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: { loading: "Loading...", months: "January,February,March,April,May,June,July,August,September,October,November,December".split(","), shortMonths: "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(","), weekdays: "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday".split(","), decimalPoint: ".", resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: "," }, global: { useUTC: !0 }, chart: { borderColor: "#4572A7", borderRadius: 5,
                defaultSeriesType: "line", ignoreHiddenSeries: !0, spacingTop: 10, spacingRight: 10, spacingBottom: 15, spacingLeft: 10, style: { fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Verdana, Arial, Helvetica, sans-serif', fontSize: "12px" }, backgroundColor: "#FFFFFF", plotBorderColor: "#C0C0C0", resetZoomButton: { theme: { zIndex: 20 }, position: { align: "right", x: -10, y: 10 }, relativeTo: "plot"}
            }, title: { text: "Chart title", align: "center", y: 15, style: { color: "#3E576F", fontSize: "16px"} }, subtitle: { text: "", align: "center", y: 30, style: { color: "#6D869F"} },
            plotOptions: { line: { allowPointSelect: !1, showCheckbox: !1, animation: { duration: 1E3 }, events: {}, lineWidth: 2, shadow: !0, marker: { enabled: !0, lineWidth: 0, radius: 4, lineColor: "#FFFFFF", states: { hover: {}, select: { fillColor: "#FFFFFF", lineColor: "#000000", lineWidth: 2}} }, point: { events: {} }, dataLabels: H(B, { enabled: !1, y: -6, formatter: function() { return this.y } }), cropThreshold: 300, pointRange: 0, showInLegend: !0, states: { hover: { marker: {} }, select: { marker: {}} }, stickyTracking: !0} }, labels: { style: { position: Cb, color: "#3E576F"} }, legend: { enabled: !0,
                align: "center", layout: "horizontal", labelFormatter: function() { return this.name }, borderWidth: 1, borderColor: "#909090", borderRadius: 5, shadow: !1, style: { padding: "5px" }, itemStyle: { cursor: "pointer", color: "#3E576F" }, itemHoverStyle: { color: "#000000" }, itemHiddenStyle: { color: "#C0C0C0" }, itemCheckboxStyle: { position: Cb, width: "13px", height: "13px" }, symbolWidth: 16, symbolPadding: 5, verticalAlign: "bottom", x: 0, y: 0
            }, loading: { labelStyle: { fontWeight: "bold", position: Cc, top: "1em" }, style: { position: Cb, backgroundColor: "white",
                opacity: 0.5, textAlign: "center"}
            }, tooltip: { enabled: !0, backgroundColor: "rgba(255, 255, 255, .85)", borderWidth: 2, borderRadius: 5, headerFormat: '<span style="font-size: 10px">{point.key}</span><br/>', pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b><br/>', shadow: !0, snap: Ga ? 25 : 10, style: { color: "#333333", fontSize: "12px", padding: "5px", whiteSpace: "nowrap"} }, credits: { enabled: !0, text: "Highcharts.com", href: "http://www.highcharts.com", position: { align: "right", x: -10, verticalAlign: "bottom",
                y: -5
            }, style: { cursor: "pointer", color: "#909090", fontSize: "10px"}}
            }; var cc = { dateTimeLabelFormats: ja(tb, "%H:%M:%S.%L", kb, "%H:%M:%S", db, "%H:%M", za, "%H:%M", ka, "%e. %b", Ca, "%e. %b", Ka, "%b '%y", La, "%Y"), endOnTick: !1, gridLineColor: "#C0C0C0", labels: B, lineColor: "#C0D0E0", lineWidth: 1, max: null, min: null, minPadding: 0.01, maxPadding: 0.01, minorGridLineColor: "#E0E0E0", minorGridLineWidth: 1, minorTickColor: "#A0A0A0", minorTickLength: 2, minorTickPosition: "outside", startOfWeek: 1, startOnTick: !1, tickColor: "#C0D0E0", tickLength: 5,
                tickmarkPlacement: "between", tickPixelInterval: 100, tickPosition: "outside", tickWidth: 1, title: { align: "middle", style: { color: "#6D869F", fontWeight: "bold"} }, type: "linear"
            }, nc = H(cc, { endOnTick: !0, gridLineWidth: 1, tickPixelInterval: 72, showLastLabel: !0, labels: { align: "right", x: -8, y: 3 }, lineWidth: 0, maxPadding: 0.05, minPadding: 0.05, startOnTick: !0, tickWidth: 0, title: { rotation: 270, text: "Y-values" }, stackLabels: { enabled: !1, formatter: function() { return this.total }, style: B.style} }), Rc = { labels: { align: "right", x: -8, y: null },
                title: { rotation: 270}
            }, Qc = { labels: { align: "left", x: 8, y: null }, title: { rotation: 90} }, Ac = { labels: { align: "center", x: 0, y: 14 }, title: { rotation: 0} }, Pc = H(Ac, { labels: { y: -5} }), aa = X.plotOptions, B = aa.line; aa.spline = H(B); aa.scatter = H(B, { lineWidth: 0, states: { hover: { lineWidth: 0} }, tooltip: { headerFormat: '<span style="font-size: 10px; color:{series.color}">{series.name}</span><br/>', pointFormat: "x: <b>{point.x}</b><br/>y: <b>{point.y}</b><br/>"} }); aa.area = H(B, { threshold: 0 }); aa.areaspline = H(aa.area); aa.column = H(B, { borderColor: "#FFFFFF",
                borderWidth: 1, borderRadius: 0, groupPadding: 0.2, marker: null, pointPadding: 0.1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { brightness: 0.1, shadow: !1 }, select: { color: "#C0C0C0", borderColor: "#000000", shadow: !1} }, dataLabels: { y: null, verticalAlign: null }, threshold: 0
            }); aa.bar = H(aa.column, { dataLabels: { align: "left", x: 5, y: 0} }); aa.pie = H(B, { borderColor: "#FFFFFF", borderWidth: 1, center: ["50%", "50%"], colorByPoint: !0, dataLabels: { distance: 30, enabled: !0, formatter: function() { return this.point.name }, y: 5 },
                legendType: "point", marker: null, size: "75%", showInLegend: !1, slicedOffset: 10, states: { hover: { brightness: 0.1, shadow: !1}}
            }); zc(); var cb = function(a) {
                var b = [], c; (function(a) { (c = /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/.exec(a)) ? b = [N(c[1]), N(c[2]), N(c[3]), parseFloat(c[4], 10)] : (c = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(a)) && (b = [N(c[1], 16), N(c[2], 16), N(c[3], 16), 1]) })(a); return { get: function(c) {
                    return b && !isNaN(b[0]) ? c === "rgb" ? "rgb(" + b[0] + "," +
b[1] + "," + b[2] + ")" : c === "a" ? b[3] : "rgba(" + b.join(",") + ")" : a
                }, brighten: function(a) { if (Sb(a) && a !== 0) { var c; for (c = 0; c < 3; c++) b[c] += N(a * 255), b[c] < 0 && (b[c] = 0), b[c] > 255 && (b[c] = 255) } return this }, setOpacity: function(a) { b[3] = a; return this } }
                }; Bb.prototype = { init: function(a, b) { this.element = S.createElementNS("http://www.w3.org/2000/svg", b); this.renderer = a; this.attrSetters = {} }, animate: function(a, b, c) { b = r(b, Lb, !0); Jb(this); if (b) { b = H(b); if (c) b.complete = c; ec(this, a, b) } else this.attr(a), c && c() }, attr: function(a, b) {
                    var c,
d, e, f, g = this.element, h = g.nodeName, j = this.renderer, k, i = this.attrSetters, l = this.shadows, m = this.htmlNode, p, n = this; zb(a) && A(b) && (c = a, a = {}, a[c] = b); if (zb(a)) c = a, h === "circle" ? c = { x: "cx", y: "cy"}[c] || c : c === "strokeWidth" && (c = "stroke-width"), n = R(g, c) || this[c] || 0, c !== "d" && c !== "visibility" && (n = parseFloat(n)); else for (c in a) {
                        k = !1; d = a[c]; e = i[c] && i[c](d, c); if (e !== !1) {
                            e !== G && (d = e); if (c === "d") d && d.join && (d = d.join(" ")), /(NaN| {2}|^$)/.test(d) && (d = "M 0 0"), this.d = d; else if (c === "x" && h === "text") {
                                for (e = 0; e < g.childNodes.length; e++) f =
g.childNodes[e], R(f, "x") === R(g, "x") && R(f, "x", d); this.rotation && R(g, "transform", "rotate(" + this.rotation + " " + d + " " + N(a.y || R(g, "y")) + ")")
                            } else if (c === "fill") d = j.color(d, g, c); else if (h === "circle" && (c === "x" || c === "y")) c = { x: "cx", y: "cy"}[c] || c; else if (h === "rect" && c === "r") R(g, { rx: d, ry: d }), k = !0; else if (c === "translateX" || c === "translateY" || c === "rotation" || c === "verticalAlign") this[c] = d, this.updateTransform(), k = !0; else if (c === "stroke") d = j.color(d, g, c); else if (c === "dashstyle") if (c = "stroke-dasharray", d = d && d.toLowerCase(),
d === "solid") d = oa; else { if (d) { d = d.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (e = d.length; e--; ) d[e] = N(d[e]) * a["stroke-width"]; d = d.join(",") } } else c === "isTracker" ? this[c] = d : c === "width" ? d = N(d) : c === "align" ? (c = "text-anchor", d = { left: "start", center: "middle", right: "end"}[d]) : c === "title" && (e = S.createElementNS("http://www.w3.org/2000/svg",
"title"), e.appendChild(S.createTextNode(d)), g.appendChild(e)); c === "strokeWidth" && (c = "stroke-width"); Fc && c === "stroke-width" && d === 0 && (d = 1.0E-6); this.symbolName && /^(x|y|r|start|end|innerR|anchorX|anchorY)/.test(c) && (p || (this.symbolAttr(a), p = !0), k = !0); if (l && /^(width|height|visibility|x|y|d|transform)$/.test(c)) for (e = l.length; e--; ) R(l[e], c, d); if ((c === "width" || c === "height") && h === "rect" && d < 0) d = 0; c === "text" ? (this.textStr = d, this.added && j.buildText(this)) : k || R(g, c, d)
                        } if (m && (c === "x" || c === "y" || c === "translateX" ||
c === "translateY" || c === "visibility")) { e = m.length ? m : [this]; f = e.length; var x; for (x = 0; x < f; x++) m = e[x], k = m.getBBox(), m = m.htmlNode, J(m, E(this.styles, { left: k.x + (this.translateX || 0) + Ea, top: k.y + (this.translateY || 0) + Ea })), c === "visibility" && J(m, { visibility: d }) } 
                    } if (Fc && /Chrome\/(18|19)/.test(ib) && h === "text" && (a.x !== G || a.y !== G)) c = g.parentNode, d = g.nextSibling, c && (c.removeChild(g), d ? c.insertBefore(g, d) : c.appendChild(g)); return n
                }, symbolAttr: function(a) {
                    var b = this; n("x,y,r,start,end,width,height,innerR,anchorX,anchorY".split(","),
function(c) { b[c] = r(a[c], b[c]) }); b.attr({ d: b.renderer.symbols[b.symbolName](b.x, b.y, b.width, b.height, b) })
                }, clip: function(a) { return this.attr("clip-path", "url(" + this.renderer.url + "#" + a.id + ")") }, crisp: function(a, b, c, d, e) { var f, g = {}, h = {}, j, a = a || this.strokeWidth || this.attr && this.attr("stroke-width") || 0; j = y(a) % 2 / 2; h.x = Na(b || this.x || 0) + j; h.y = Na(c || this.y || 0) + j; h.width = Na((d || this.width || 0) - 2 * j); h.height = Na((e || this.height || 0) - 2 * j); h.strokeWidth = a; for (f in h) this[f] !== h[f] && (this[f] = g[f] = h[f]); return g },
                    css: function(a) { var b = this.element, b = a && a.width && b.nodeName === "text", c, d = "", e = function(a, b) { return "-" + b.toLowerCase() }; if (a && a.color) a.fill = a.color; this.styles = a = E(this.styles, a); if (Tb && !Ob) b && delete a.width, J(this.element, a); else { for (c in a) d += c.replace(/([A-Z])/g, e) + ":" + a[c] + ";"; this.attr({ style: d }) } b && this.added && this.renderer.buildText(this); return this }, on: function(a, b) { var c = b; Ga && a === "click" && (a = "touchstart", c = function(a) { a.preventDefault(); b() }); this.element["on" + a] = c; return this }, translate: function(a,
b) { return this.attr({ translateX: a, translateY: b }) }, invert: function() { this.inverted = !0; this.updateTransform(); return this }, updateTransform: function() { var a = this.translateX || 0, b = this.translateY || 0, c = this.inverted, d = this.rotation, e = []; c && (a += this.attr("width"), b += this.attr("height")); (a || b) && e.push("translate(" + a + "," + b + ")"); c ? e.push("rotate(90) scale(-1,1)") : d && e.push("rotate(" + d + " " + this.x + " " + this.y + ")"); e.length && R(this.element, "transform", e.join(" ")) }, toFront: function() {
    var a = this.element; a.parentNode.appendChild(a);
    return this
}, align: function(a, b, c) {
    a ? (this.alignOptions = a, this.alignByTranslate = b, c || this.renderer.alignedObjects.push(this)) : (a = this.alignOptions, b = this.alignByTranslate); var c = r(c, this.renderer), d = a.align, e = a.verticalAlign, f = (c.x || 0) + (a.x || 0), g = (c.y || 0) + (a.y || 0), h = {}; /^(right|center)$/.test(d) && (f += (c.width - (a.width || 0)) / { right: 1, center: 2}[d]); h[b ? "translateX" : "x"] = y(f); /^(bottom|middle)$/.test(e) && (g += (c.height - (a.height || 0)) / ({ bottom: 1, middle: 2}[e] || 1)); h[b ? "translateY" : "y"] = y(g); this[this.placed ?
"animate" : "attr"](h); this.placed = !0; this.alignAttr = h; return this
}, getBBox: function() { var a, b, c, d = this.rotation, e = d * Ec; try { a = E({}, this.element.getBBox()) } catch (f) { a = { width: 0, height: 0} } b = a.width; c = a.height; if (d) a.width = Ba(c * ma(e)) + Ba(b * ia(e)), a.height = Ba(c * ia(e)) + Ba(b * ma(e)); return a }, show: function() { return this.attr({ visibility: bb }) }, hide: function() { return this.attr({ visibility: Ta }) }, add: function(a) {
    var b = this.renderer, c = a || b, d = c.element || b.box, e = d.childNodes, f = this.element, g = R(f, "zIndex"), h; this.parentInverted =
a && a.inverted; this.textStr !== void 0 && b.buildText(this); if (a && this.htmlNode) { if (!a.htmlNode) a.htmlNode = []; a.htmlNode.push(this) } if (g) c.handleZ = !0, g = N(g); if (c.handleZ) for (c = 0; c < e.length; c++) if (a = e[c], b = R(a, "zIndex"), a !== f && (N(b) > g || !A(g) && A(b))) { d.insertBefore(f, a); h = !0; break } h || d.appendChild(f); this.added = !0; Y(this, "add"); return this
}, safeRemoveChild: function(a) { var b = a.parentNode; b && b.removeChild(a) }, destroy: function() {
    var a = this, b = a.element || {}, c = a.shadows, d = a.box, e, f; b.onclick = b.onmouseout = b.onmouseover =
b.onmousemove = null; Jb(a); if (a.clipPath) a.clipPath = a.clipPath.destroy(); if (a.stops) { for (f = 0; f < a.stops.length; f++) a.stops[f] = a.stops[f].destroy(); a.stops = null } a.safeRemoveChild(b); c && n(c, function(b) { a.safeRemoveChild(b) }); d && d.destroy(); Fb(a.renderer.alignedObjects, a); for (e in a) delete a[e]; return null
}, empty: function() { for (var a = this.element, b = a.childNodes, c = b.length; c--; ) a.removeChild(b[c]) }, shadow: function(a, b) {
    var c = [], d, e, f = this.element, g = this.parentInverted ? "(-1,-1)" : "(1,1)"; if (a) {
        for (d = 1; d <=
3; d++) e = f.cloneNode(0), R(e, { isShadow: "true", stroke: "rgb(0, 0, 0)", "stroke-opacity": 0.05 * d, "stroke-width": 7 - 2 * d, transform: "translate" + g, fill: oa }), b ? b.element.appendChild(e) : f.parentNode.insertBefore(e, f), c.push(e); this.shadows = c
    } return this
} 
                }; var Mb = function() { this.init.apply(this, arguments) }; Mb.prototype = { Element: Bb, init: function(a, b, c, d) {
                    var e = location, f; f = this.createElement("svg").attr({ xmlns: "http://www.w3.org/2000/svg", version: "1.1" }); a.appendChild(f.element); this.box = f.element; this.boxWrapper =
f; this.alignedObjects = []; this.url = Tb ? "" : e.href.replace(/#.*?$/, "").replace(/\(/g, "\\(").replace(/\)/g, "\\)"); this.defs = this.createElement("defs").add(); this.forExport = d; this.gradients = {}; this.setSize(b, c, !1)
                }, destroy: function() { var a = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy(); ub(this.gradients || {}); this.gradients = null; if (a) this.defs = a.destroy(); return this.alignedObjects = null }, createElement: function(a) { var b = new this.Element; b.init(this, a); return b }, buildText: function(a) {
                    for (var b =
a.element, c = r(a.textStr, "").toString().replace(/<(b|strong)>/g, '<span style="font-weight:bold">').replace(/<(i|em)>/g, '<span style="font-style:italic">').replace(/<a/g, "<span").replace(/<\/(b|strong|i|em|a)>/g, "</span>").split(/<br.*?>/g), d = b.childNodes, e = /style="([^"]+)"/, f = /href="([^"]+)"/, g = R(b, "x"), h = a.styles, j = h && a.useHTML && !this.forExport, k = a.htmlNode, i = h && N(h.width), l = h && h.lineHeight, m, p = d.length; p--; ) b.removeChild(d[p]); i && !a.added && this.box.appendChild(b); c[c.length - 1] === "" && c.pop(); n(c,
function(c, d) {
    var h, j = 0, k, c = c.replace(/<span/g, "|||<span").replace(/<\/span>/g, "</span>|||"); h = c.split("|||"); n(h, function(c) {
        if (c !== "" || h.length === 1) {
            var p = {}, n = S.createElementNS("http://www.w3.org/2000/svg", "tspan"); e.test(c) && R(n, "style", c.match(e)[1].replace(/(;| |^)color([ :])/, "$1fill$2")); f.test(c) && (R(n, "onclick", 'location.href="' + c.match(f)[1] + '"'), J(n, { cursor: "pointer" })); c = (c.replace(/<(.|\n)*?>/g, "") || " ").replace(/&lt;/g, "<").replace(/&gt;/g, ">"); n.appendChild(S.createTextNode(c)); j ?
p.dx = 3 : p.x = g; if (!j) { if (d) { !Ob && a.renderer.forExport && J(n, { display: "block" }); k = fa.getComputedStyle && N(fa.getComputedStyle(m, null).getPropertyValue("line-height")); if (!k || isNaN(k)) k = l || m.offsetHeight || 18; R(n, "dy", k) } m = n } R(n, p); b.appendChild(n); j++; if (i) for (var c = c.replace(/-/g, "- ").split(" "), t, r = []; c.length || r.length; ) t = a.getBBox().width, p = t > i, !p || c.length === 1 ? (c = r, r = [], c.length && (n = S.createElementNS("http://www.w3.org/2000/svg", "tspan"), R(n, { dy: l || 16, x: g }), b.appendChild(n), t > i && (i = t))) : (n.removeChild(n.firstChild),
r.unshift(c.pop())), c.length && n.appendChild(S.createTextNode(c.join(" ").replace(/- /g, "-")))
        } 
    })
}); if (j) { if (!k) k = a.htmlNode = na("span", null, E(h, { position: Cb, top: 0, left: 0 }), this.box.parentNode); k.innerHTML = a.textStr; for (p = d.length; p--; ) d[p].style.visibility = Ta } 
                }, button: function(a, b, c, d, e, f, g) {
                    var h = this.label(a, b, c), j = 0, k, i, l, m, p, a = { x1: 0, y1: 0, x2: 0, y2: 1 }, e = H(ja("stroke-width", 1, "stroke", "#999", "fill", ja("linearGradient", a, "stops", [[0, "#FFF"], [1, "#DDD"]]), "r", 3, "padding", 3, "style", ja("color", "black")),
e); l = e.style; delete e.style; f = H(e, ja("stroke", "#68A", "fill", ja("linearGradient", a, "stops", [[0, "#FFF"], [1, "#ACF"]])), f); m = f.style; delete f.style; g = H(e, ja("stroke", "#68A", "fill", ja("linearGradient", a, "stops", [[0, "#9BD"], [1, "#CDF"]])), g); p = g.style; delete g.style; W(h.element, "mouseenter", function() { h.attr(f).css(m) }); W(h.element, "mouseleave", function() { k = [e, f, g][j]; i = [l, m, p][j]; h.attr(k).css(i) }); h.setState = function(a) { (j = a) ? a === 2 && h.attr(g).css(p) : h.attr(e).css(l) }; return h.on("click", function() { d.call(h) }).attr(e).css(E({ cursor: "default" },
l))
                }, crispLine: function(a, b) { a[1] === a[4] && (a[1] = a[4] = y(a[1]) + b % 2 / 2); a[2] === a[5] && (a[2] = a[5] = y(a[2]) + b % 2 / 2); return a }, path: function(a) { return this.createElement("path").attr({ d: a, fill: oa }) }, circle: function(a, b, c) { a = rb(a) ? a : { x: a, y: b, r: c }; return this.createElement("circle").attr(a) }, arc: function(a, b, c, d, e, f) { if (rb(a)) b = a.y, c = a.r, d = a.innerR, e = a.start, f = a.end, a = a.x; return this.symbol("arc", a || 0, b || 0, c || 0, c || 0, { innerR: d || 0, start: e || 0, end: f || 0 }) }, rect: function(a, b, c, d, e, f) {
                    if (rb(a)) b = a.y, c = a.width, d = a.height,
e = a.r, f = a.strokeWidth, a = a.x; e = this.createElement("rect").attr({ rx: e, ry: e, fill: oa }); return e.attr(e.crisp(f, a, b, T(c, 0), T(d, 0)))
                }, setSize: function(a, b, c) { var d = this.alignedObjects, e = d.length; this.width = a; this.height = b; for (this.boxWrapper[r(c, !0) ? "animate" : "attr"]({ width: a, height: b }); e--; ) d[e].align() }, g: function(a) { var b = this.createElement("g"); return A(a) ? b.attr({ "class": gb + a }) : b }, image: function(a, b, c, d, e) {
                    var f = { preserveAspectRatio: oa }; arguments.length > 1 && E(f, { x: b, y: c, width: d, height: e }); f = this.createElement("image").attr(f);
                    f.element.setAttributeNS ? f.element.setAttributeNS("http://www.w3.org/1999/xlink", "href", a) : f.element.setAttribute("hc-svg-href", a); return f
                }, symbol: function(a, b, c, d, e, f) {
                    var g, h = this.symbols[a], h = h && h(y(b), y(c), d, e, f), j = /^url\((.*?)\)$/, k; if (h) g = this.path(h), E(g, { symbolName: a, x: b, y: c, width: d, height: e }), f && E(g, f); else if (j.test(a)) {
                        var i = function(a, b) { a.attr({ width: b[0], height: b[1] }).translate(-y(b[0] / 2), -y(b[1] / 2)) }; k = a.match(j)[1]; a = Gc[k]; g = this.image(k).attr({ x: b, y: c }); a ? i(g, a) : (g.attr({ width: 0,
                            height: 0
                        }), na("img", { onload: function() { i(g, Gc[k] = [this.width, this.height]) }, src: k }))
                    } return g
                }, symbols: { circle: function(a, b, c, d) { var e = 0.166 * c; return [ta, a + c / 2, b, "C", a + c + e, b, a + c + e, b + d, a + c / 2, b + d, "C", a - e, b + d, a - e, b, a + c / 2, b, "Z"] }, square: function(a, b, c, d) { return [ta, a, b, ga, a + c, b, a + c, b + d, a, b + d, "Z"] }, triangle: function(a, b, c, d) { return [ta, a + c / 2, b, ga, a + c, b + d, a, b + d, "Z"] }, "triangle-down": function(a, b, c, d) { return [ta, a, b, ga, a + c, b, a + c / 2, b + d, "Z"] }, diamond: function(a, b, c, d) {
                    return [ta, a + c / 2, b, ga, a + c, b + d / 2, a + c / 2, b + d,
a, b + d / 2, "Z"]
                }, arc: function(a, b, c, d, e) { var f = e.start, c = e.r || c || d, g = e.end - 1.0E-6, d = e.innerR, h = ia(f), j = ma(f), k = ia(g), g = ma(g), e = e.end - f < Ia ? 0 : 1; return [ta, a + c * h, b + c * j, "A", c, c, 0, e, 1, a + c * k, b + c * g, ga, a + d * k, b + d * g, "A", d, d, 0, e, 0, a + d * h, b + d * j, "Z"] } 
                }, clipRect: function(a, b, c, d) { var e = gb + oc++, f = this.createElement("clipPath").attr({ id: e }).add(this.defs), a = this.rect(a, b, c, d, 0).add(f); a.id = e; a.clipPath = f; return a }, color: function(a, b, c) {
                    var d, e = /^rgba/; if (a && a.linearGradient) {
                        var f = this, g = a.linearGradient, b = !ic(g), c = f.gradients,
h, j = g.x1 || g[0] || 0, k = g.y1 || g[1] || 0, i = g.x2 || g[2] || 0, l = g.y2 || g[3] || 0, m, p, t = [b, j, k, i, l, a.stops.join(",")].join(","); c[t] ? g = R(c[t].element, "id") : (g = gb + oc++, h = f.createElement("linearGradient").attr(E({ id: g, x1: j, y1: k, x2: i, y2: l }, b ? null : { gradientUnits: "userSpaceOnUse" })).add(f.defs), h.stops = [], n(a.stops, function(a) { e.test(a[1]) ? (d = cb(a[1]), m = d.get("rgb"), p = d.get("a")) : (m = a[1], p = 1); a = f.createElement("stop").attr({ offset: a[0], "stop-color": m, "stop-opacity": p }).add(h); h.stops.push(a) }), c[t] = h); return "url(" + this.url +
"#" + g + ")"
                    } else return e.test(a) ? (d = cb(a), R(b, c + "-opacity", d.get("a")), d.get("rgb")) : (b.removeAttribute(c + "-opacity"), a)
                }, text: function(a, b, c, d) { var e = X.chart.style, b = y(r(b, 0)), c = y(r(c, 0)), a = this.createElement("text").attr({ x: b, y: c, text: a }).css({ fontFamily: e.fontFamily, fontSize: e.fontSize }); a.x = b; a.y = c; a.useHTML = d; return a }, label: function(a, b, c, d, e, f) {
                    function g() {
                        var a = i.styles, a = a && a.textAlign, b = x, c = x + y(N(i.element.style.fontSize || 11) * 1.2); if (A(u) && (a === "center" || a === "right")) b += { center: 0.5, right: 1}[a] *
(u - p.width); (b !== l.x || c !== l.y) && l.attr({ x: b, y: c }); l.x = b; l.y = c
                    } function h(a, b) { m ? m.attr(a, b) : ya[a] = b } function j() { i.attr({ text: a, x: b, y: c, anchorX: e, anchorY: f }) } var k = this, i = k.g(), l = k.text().attr({ zIndex: 1 }).add(i), m, p, t = "left", x = 3, u, w, v, q, r = 0, ya = {}, I = i.attrSetters; W(i, "add", j); I.width = function(a) { u = a; return !1 }; I.height = function(a) { w = a; return !1 }; I.padding = function(a) { x = a; g(); return !1 }; I.align = function(a) { t = a; return !1 }; I.text = function(a, b) {
                        l.attr(b, a); p = (u === void 0 || w === void 0 || i.styles.textAlign) && l.getBBox(!0);
                        i.width = (u || p.width) + 2 * x; i.height = (w || p.height) + 2 * x; if (!m) i.box = m = d ? k.symbol(d, 0, 0, i.width, i.height) : k.rect(0, 0, i.width, i.height, 0, ya["stroke-width"]), m.add(i); m.attr(H({ width: i.width, height: i.height }, ya)); ya = null; g(); return !1
                    }; I["stroke-width"] = function(a, b) { r = a % 2 / 2; h(b, a); return !1 }; I.stroke = I.fill = I.r = function(a, b) { h(b, a); return !1 }; I.anchorX = function(a, b) { e = a; h(b, a + r - v); return !1 }; I.anchorY = function(a, b) { f = a; h(b, a - q); return !1 }; I.x = function(a) {
                        v = a; v -= { left: 0, center: 0.5, right: 1}[t] * ((u || p.width) + x);
                        i.attr("translateX", y(v)); return !1
                    }; I.y = function(a) { q = a; i.attr("translateY", y(a)); return !1 }; var Ra = i.css; return E(i, { css: function(a) { if (a) { var b = {}, a = H({}, a); n("fontSize,fontWeight,fontFamily,color,lineHeight,width".split(","), function(c) { a[c] !== G && (b[c] = a[c], delete a[c]) }); l.css(b) } return Ra.call(i, a) }, getBBox: function() { return m.getBBox() }, shadow: function(a) { m.shadow(a); return i }, destroy: function() { ra(i, "add", j); ra(i.element, "mouseenter"); ra(i.element, "mouseleave"); l && (l = l.destroy()); Bb.prototype.destroy.call(i) } })
                } 
                };
                Nb = Mb; var Rb; if (!Ob) B = pa(Bb, { init: function(a, b) { var c = ["<", b, ' filled="f" stroked="f"'], d = ["position: ", Cb, ";"]; (b === "shape" || b === vb) && d.push("left:0;top:0;width:10px;height:10px;"); Pb && d.push("visibility: ", b === vb ? Ta : bb); c.push(' style="', d.join(""), '"/>'); if (b) c = b === vb || b === "span" || b === "img" ? c.join("") : a.prepVML(c), this.element = na(c); this.renderer = a; this.attrSetters = {} }, add: function(a) {
                    var b = this.renderer, c = this.element, d = b.box, d = a ? a.element || a : d; a && a.inverted && b.invertChild(c, d); Pb && d.gVis === Ta &&
J(c, { visibility: Ta }); d.appendChild(c); this.added = !0; this.alignOnAdd && !this.deferUpdateTransform && this.updateTransform(); Y(this, "add"); return this
                }, toggleChildren: function(a, b) { for (var c = a.childNodes, d = c.length; d--; ) J(c[d], { visibility: b }), c[d].nodeName === "DIV" && this.toggleChildren(c[d], b) }, attr: function(a, b) {
                    var c, d, e, f = this.element || {}, g = f.style, h = f.nodeName, j = this.renderer, k = this.symbolName, i, l = this.shadows, m, p = this.attrSetters, n = this; zb(a) && A(b) && (c = a, a = {}, a[c] = b); if (zb(a)) c = a, n = c === "strokeWidth" ||
c === "stroke-width" ? this.strokeweight : this[c]; else for (c in a) if (d = a[c], m = !1, e = p[c] && p[c](d, c), e !== !1 && d !== null) {
                        e !== G && (d = e); if (k && /^(x|y|r|start|end|width|height|innerR|anchorX|anchorY)/.test(c)) i || (this.symbolAttr(a), i = !0), m = !0; else if (c === "d") { d = d || []; this.d = d.join(" "); e = d.length; for (m = []; e--; ) m[e] = Sb(d[e]) ? y(d[e] * 10) - 5 : d[e] === "Z" ? "x" : d[e]; d = m.join(" ") || "x"; f.path = d; if (l) for (e = l.length; e--; ) l[e].path = d; m = !0 } else if (c === "zIndex" || c === "visibility") {
                            if (Pb && c === "visibility" && h === "DIV") f.gVis = d, this.toggleChildren(f,
d), d === bb && (d = null); d && (g[c] = d); m = !0
                        } else if (c === "width" || c === "height") d = T(0, d), this[c] = d, this.updateClipping ? (this[c] = d, this.updateClipping()) : g[c] = d, m = !0; else if (/^(x|y)$/.test(c)) this[c] = d, f.tagName === "SPAN" ? this.updateTransform() : g[{ x: "left", y: "top"}[c]] = d; else if (c === "class") f.className = d; else if (c === "stroke") d = j.color(d, f, c), c = "strokecolor"; else if (c === "stroke-width" || c === "strokeWidth") f.stroked = d ? !0 : !1, c = "strokeweight", this[c] = d, Sb(d) && (d += Ea); else if (c === "dashstyle") (f.getElementsByTagName("stroke")[0] ||
na(j.prepVML(["<stroke/>"]), null, null, f))[c] = d || "solid", this.dashstyle = d, m = !0; else if (c === "fill") h === "SPAN" ? g.color = d : (f.filled = d !== oa ? !0 : !1, d = j.color(d, f, c), c = "fillcolor"); else if (c === "translateX" || c === "translateY" || c === "rotation" || c === "align") c === "align" && (c = "textAlign"), this[c] = d, this.updateTransform(), m = !0; else if (c === "text") this.bBox = null, f.innerHTML = d, m = !0; if (l && c === "visibility") for (e = l.length; e--; ) l[e].style[c] = d; m || (Pb ? f[c] = d : R(f, c, d))
                    } return n
                }, clip: function(a) {
                    var b = this, c = a.members; c.push(b);
                    b.destroyClip = function() { Fb(c, b) }; return b.css(a.getCSS(b.inverted))
                }, css: function(a) { var b = this.element; if (b = a && b.tagName === "SPAN" && a.width) delete a.width, this.textWidth = b, this.updateTransform(); this.styles = E(this.styles, a); J(this.element, a); return this }, safeRemoveChild: function(a) { a.parentNode && Hb(a) }, destroy: function() { this.destroyClip && this.destroyClip(); return Bb.prototype.destroy.apply(this) }, empty: function() { for (var a = this.element.childNodes, b = a.length, c; b--; ) c = a[b], c.parentNode.removeChild(c) },
                    getBBox: function(a) { var b = this.element, c = this.bBox; if (!c || a) { if (b.nodeName === "text") b.style.position = Cb; c = this.bBox = { x: b.offsetLeft, y: b.offsetTop, width: b.offsetWidth, height: b.offsetHeight} } return c }, on: function(a, b) { this.element["on" + a] = function() { var a = fa.event; a.target = a.srcElement; b(a) }; return this }, updateTransform: function() {
                        if (this.added) {
                            var a = this, b = a.element, c = a.translateX || 0, d = a.translateY || 0, e = a.x || 0, f = a.y || 0, g = a.textAlign || "left", h = { left: 0, center: 0.5, right: 1}[g], j = g && g !== "left", k = a.shadows;
                            if (c || d) J(b, { marginLeft: c, marginTop: d }), k && n(k, function(a) { J(a, { marginLeft: c + 1, marginTop: d + 1 }) }); a.inverted && n(b.childNodes, function(c) { a.renderer.invertChild(c, b) }); if (b.tagName === "SPAN") {
                                var i, l, k = a.rotation, m; i = 0; var p = 1, t = 0, x; m = N(a.textWidth); var u = a.xCorr || 0, w = a.yCorr || 0, v = [k, g, b.innerHTML, a.textWidth].join(","); if (v !== a.cTT) A(k) && (i = k * Ec, p = ia(i), t = ma(i), J(b, { filter: k ? ["progid:DXImageTransform.Microsoft.Matrix(M11=", p, ", M12=", -t, ", M21=", t, ", M22=", p, ", sizingMethod='auto expand')"].join("") :
oa
                                })), i = r(a.elemWidth, b.offsetWidth), l = r(a.elemHeight, b.offsetHeight), i > m && (J(b, { width: m + Ea, display: "block", whiteSpace: "normal" }), i = m), m = y((N(b.style.fontSize) || 12) * 1.2), u = p < 0 && -i, w = t < 0 && -l, x = p * t < 0, u += t * m * (x ? 1 - h : h), w -= p * m * (k ? x ? h : 1 - h : 1), j && (u -= i * h * (p < 0 ? -1 : 1), k && (w -= l * h * (t < 0 ? -1 : 1)), J(b, { textAlign: g })), a.xCorr = u, a.yCorr = w; J(b, { left: e + u, top: f + w }); a.cTT = v
                            } 
                        } else this.alignOnAdd = !0
                    }, shadow: function(a, b) {
                        var c = [], d, e = this.element, f = this.renderer, g, h = e.style, j, k = e.path; k && typeof k.value !== "string" && (k = "x");
                        if (a) { for (d = 1; d <= 3; d++) j = ['<shape isShadow="true" strokeweight="', 7 - 2 * d, '" filled="false" path="', k, '" coordsize="100,100" style="', e.style.cssText, '" />'], g = na(f.prepVML(j), null, { left: N(h.left) + 1, top: N(h.top) + 1 }), j = ['<stroke color="black" opacity="', 0.05 * d, '"/>'], na(f.prepVML(j), null, null, g), b ? b.element.appendChild(g) : e.parentNode.insertBefore(g, e), c.push(g); this.shadows = c } return this
                    } 
                }), Rb = function() { this.init.apply(this, arguments) }, Rb.prototype = H(Mb.prototype, { Element: B, isIE8: ib.indexOf("MSIE 8.0") >
-1, init: function(a, b, c) { var d; this.alignedObjects = []; d = this.createElement(vb); a.appendChild(d.element); this.box = d.element; this.boxWrapper = d; this.setSize(b, c, !1); if (!S.namespaces.hcv) S.namespaces.add("hcv", "urn:schemas-microsoft-com:vml"), S.createStyleSheet().cssText = "hcv\\:fill, hcv\\:path, hcv\\:shape, hcv\\:stroke{ behavior:url(#default#VML); display: inline-block; } " }, clipRect: function(a, b, c, d) {
    var e = this.createElement(); return E(e, { members: [], left: a, top: b, width: c, height: d, getCSS: function(a) {
        var b =
this.top, c = this.left, d = c + this.width, e = b + this.height, b = { clip: "rect(" + y(a ? c : b) + "px," + y(a ? e : d) + "px," + y(a ? d : e) + "px," + y(a ? b : c) + "px)" }; !a && Pb && E(b, { width: d + Ea, height: e + Ea }); return b
    }, updateClipping: function() { n(e.members, function(a) { a.css(e.getCSS(a.inverted)) }) } 
    })
}, color: function(a, b, c) {
    var d, e = /^rgba/; if (a && a.linearGradient) {
        var f, g, h = a.linearGradient, j = h.x1 || h[0] || 0, k = h.y1 || h[1] || 0, i = h.x2 || h[2] || 0, h = h.y2 || h[3] || 0, l, m, p, t; n(a.stops, function(a, b) {
            e.test(a[1]) ? (d = cb(a[1]), f = d.get("rgb"), g = d.get("a")) : (f =
a[1], g = 1); b ? (p = f, t = g) : (l = f, m = g)
        }); if (c === "fill") a = 90 - sa.atan((h - k) / (i - j)) * 180 / Ia, a = ['<fill colors="0% ', l, ",100% ", p, '" angle="', a, '" opacity="', t, '" o:opacity2="', m, '" type="gradient" focus="100%" method="any" />'], na(this.prepVML(a), null, null, b); else return f
    } else if (e.test(a) && b.tagName !== "IMG") return d = cb(a), a = ["<", c, ' opacity="', d.get("a"), '"/>'], na(this.prepVML(a), null, null, b), d.get("rgb"); else { b = b.getElementsByTagName(c); if (b.length) b[0].opacity = 1; return a } 
}, prepVML: function(a) {
    var b = this.isIE8,
a = a.join(""); b ? (a = a.replace("/>", ' xmlns="urn:schemas-microsoft-com:vml" />'), a = a.indexOf('style="') === -1 ? a.replace("/>", ' style="display:inline-block;behavior:url(#default#VML);" />') : a.replace('style="', 'style="display:inline-block;behavior:url(#default#VML);')) : a = a.replace("<", "<hcv:"); return a
}, text: function(a, b, c) { var d = X.chart.style; return this.createElement("span").attr({ text: a, x: y(b), y: y(c) }).css({ whiteSpace: "nowrap", fontFamily: d.fontFamily, fontSize: d.fontSize }) }, path: function(a) {
    return this.createElement("shape").attr({ coordsize: "100 100",
        d: a
    })
}, circle: function(a, b, c) { return this.symbol("circle").attr({ x: a, y: b, r: c }) }, g: function(a) { var b; a && (b = { className: gb + a, "class": gb + a }); return this.createElement(vb).attr(b) }, image: function(a, b, c, d, e) { var f = this.createElement("img").attr({ src: a }); arguments.length > 1 && f.css({ left: b, top: c, width: d, height: e }); return f }, rect: function(a, b, c, d, e, f) { if (rb(a)) b = a.y, c = a.width, d = a.height, f = a.strokeWidth, a = a.x; var g = this.symbol("rect"); g.r = e; return g.attr(g.crisp(f, a, b, T(c, 0), T(d, 0))) }, invertChild: function(a,
b) { var c = b.style; J(a, { flip: "x", left: N(c.width) - 10, top: N(c.height) - 10, rotation: -90 }) }, symbols: { arc: function(a, b, c, d, e) { var f = e.start, g = e.end, c = e.r || c || d, d = ia(f), h = ma(f), j = ia(g), k = ma(g), e = e.innerR, i = 0.07 / c, l = e && 0.1 / e || 0; if (g - f === 0) return ["x"]; else 2 * Ia - g + f < i ? j = -i : g - f < l && (j = ia(f + l)); return ["wa", a - c, b - c, a + c, b + c, a + c * d, b + c * h, a + c * j, b + c * k, "at", a - e, b - e, a + e, b + e, a + e * j, b + e * k, a + e * d, b + e * h, "x", "e"] }, circle: function(a, b, c, d) { return ["wa", a, b, a + c, b + d, a + c, b + d / 2, a + c, b + d / 2, "e"] }, rect: function(a, b, c, d, e) {
    if (!A(e)) return [];
    var f = a + c, g = b + d, c = va(e.r || 0, c, d); return [ta, a + c, b, ga, f - c, b, "wa", f - 2 * c, b, f, b + 2 * c, f - c, b, f, b + c, ga, f, g - c, "wa", f - 2 * c, g - 2 * c, f, g, f, g - c, f - c, g, ga, a + c, g, "wa", a, g - 2 * c, a + 2 * c, g, a + c, g, a, g - c, ga, a, b + c, "wa", a, b, a + 2 * c, b + 2 * c, a, b + c, a + c, b, "x", "e"]
} }
                }), Nb = Rb; $b.prototype.callbacks = []; var jb = function() { }; jb.prototype = { init: function(a, b, c) {
                    var d = a.chart.counters; this.series = a; this.applyOptions(b, c); this.pointAttr = {}; if (a.options.colorByPoint) {
                        b = a.chart.options.colors; if (!this.options) this.options = {}; this.color = this.options.color =
this.color || b[d.color++]; d.wrapColor(b.length)
                    } a.chart.pointCount++; return this
                }, applyOptions: function(a, b) { var c = this.series, d = typeof a; this.config = a; if (d === "number" || a === null) this.y = a; else if (typeof a[0] === "number") this.x = a[0], this.y = a[1]; else if (d === "object" && typeof a.length !== "number") E(this, a), this.options = a; else if (typeof a[0] === "string") this.name = a[0], this.y = a[1]; if (this.x === G) this.x = b === G ? c.autoIncrement() : b }, destroy: function() {
                    var a = this.series, b = a.chart.hoverPoints, c; a.chart.pointCount--;
                    b && (this.setState(), Fb(b, this)); if (this === a.chart.hoverPoint) this.onMouseOut(); a.chart.hoverPoints = null; if (this.graphic || this.dataLabel) ra(this), this.destroyElements(); this.legendItem && this.series.chart.legend.destroyItem(this); for (c in this) this[c] = null
                }, destroyElements: function() { for (var a = "graphic,tracker,dataLabel,group,connector,shadowGroup".split(","), b, c = 6; c--; ) b = a[c], this[b] && (this[b] = this[b].destroy()) }, getLabelConfig: function() {
                    return { x: this.category, y: this.y, key: this.name || this.category,
                        series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal}
                    }, select: function(a, b) { var c = this, d = c.series.chart, a = r(a, !c.selected); c.firePointEvent(a ? "select" : "unselect", { accumulate: b }, function() { c.selected = a; c.setState(a && "select"); b || n(d.getSelectedPoints(), function(a) { if (a.selected && a !== c) a.selected = !1, a.setState(la), a.firePointEvent("unselect") }) }) }, onMouseOver: function() {
                        var a = this.series, b = a.chart, c = b.tooltip, d = b.hoverPoint; if (d && d !== this) d.onMouseOut(); this.firePointEvent("mouseOver");
                        c && (!c.shared || a.noSharedTooltip) && c.refresh(this); this.setState(fb); b.hoverPoint = this
                    }, onMouseOut: function() { this.firePointEvent("mouseOut"); this.setState(); this.series.chart.hoverPoint = null }, tooltipFormatter: function(a) {
                        var b = this.series, c = b.tooltipOptions, d = String(this.y).split("."), d = d[1] ? d[1].length : 0, e = a.match(/\{(series|point)\.[a-zA-Z]+\}/g), f = /[\.}]/, g, h, j; for (j in e) h = e[j], zb(h) && h !== a && (g = h.indexOf("point") === 1 ? this : b, g = h === "{point.y}" ? (c.valuePrefix || c.yPrefix || "") + Ub(this.y, r(c.valueDecimals,
c.yDecimals, d)) + (c.valueSuffix || c.ySuffix || "") : g[e[j].split(f)[1]], a = a.replace(e[j], g)); return a
                    }, update: function(a, b, c) { var d = this, e = d.series, f = d.graphic, g, h = e.data, j = h.length, k = e.chart, b = r(b, !0); d.firePointEvent("update", { options: a }, function() { d.applyOptions(a); rb(a) && (e.getAttribs(), f && f.attr(d.pointAttr[e.state])); for (g = 0; g < j; g++) if (h[g] === d) { e.xData[g] = d.x; e.yData[g] = d.y; e.options.data[g] = a; break } e.isDirty = !0; e.isDirtyData = !0; b && k.redraw(c) }) }, remove: function(a, b) {
                        var c = this, d = c.series, e = d.chart,
f, g = d.data, h = g.length; Ib(b, e); a = r(a, !0); c.firePointEvent("remove", null, function() { for (f = 0; f < h; f++) if (g[f] === c) { g.splice(f, 1); d.options.data.splice(f, 1); d.xData.splice(f, 1); d.yData.splice(f, 1); break } c.destroy(); d.isDirty = !0; d.isDirtyData = !0; a && e.redraw() })
                    }, firePointEvent: function(a, b, c) {
                        var d = this, e = this.series.options; (e.point.events[a] || d.options && d.options.events && d.options.events[a]) && this.importEvents(); a === "click" && e.allowPointSelect && (c = function(a) { d.select(null, a.ctrlKey || a.metaKey || a.shiftKey) });
                        Y(this, a, b, c)
                    }, importEvents: function() { if (!this.hasImportedEvents) { var a = H(this.series.options.point, this.options).events, b; this.events = a; for (b in a) W(this, b, a[b]); this.hasImportedEvents = !0 } }, setState: function(a) {
                        var b = this.plotX, c = this.plotY, d = this.series, e = d.options.states, f = aa[d.type].marker && d.options.marker, g = f && !f.enabled, h = f && f.states[a], j = h && h.enabled === !1, k = d.stateMarkerGraphic, i = d.chart, l = this.pointAttr, a = a || la; if (!(a === this.state || this.selected && a !== "select" || e[a] && e[a].enabled === !1 || a &&
(j || g && !h.enabled))) { if (this.graphic) e = this.graphic.symbolName && l[a].r, this.graphic.attr(H(l[a], e ? { x: b - e, y: c - e, width: 2 * e, height: 2 * e} : {})); else { if (a) { if (!k) e = f.radius, d.stateMarkerGraphic = k = i.renderer.symbol(d.symbol, -e, -e, 2 * e, 2 * e).attr(l[a]).add(d.group); k.translate(b, c) } if (k) k[a ? "show" : "hide"]() } this.state = a } 
                    } 
                }; var M = function() { }; M.prototype = { isCartesian: !0, type: "line", pointClass: jb, pointAttrToOptions: { stroke: "lineColor", "stroke-width": "lineWidth", fill: "fillColor", r: "radius" }, init: function(a, b) {
                    var c,
d; d = a.series.length; this.chart = a; this.options = b = this.setOptions(b); this.bindAxes(); E(this, { index: d, name: b.name || "Series " + (d + 1), state: la, pointAttr: {}, visible: b.visible !== !1, selected: b.selected === !0 }); d = b.events; for (c in d) W(this, c, d[c]); if (d && d.click || b.point && b.point.events && b.point.events.click || b.allowPointSelect) a.runTrackerClick = !0; this.getColor(); this.getSymbol(); this.setData(b.data, !1)
                }, bindAxes: function() {
                    var a = this, b = a.options, c = a.chart, d; a.isCartesian && n(["xAxis", "yAxis"], function(e) {
                        n(c[e],
function(c) { d = c.options; if (b[e] === d.index || b[e] === G && d.index === 0) c.series.push(a), a[e] = c, c.isDirty = !0 })
                    })
                }, autoIncrement: function() { var a = this.options, b = this.xIncrement, b = r(b, a.pointStart, 0); this.pointInterval = r(this.pointInterval, a.pointInterval, 1); this.xIncrement = b + this.pointInterval; return b }, getSegments: function() {
                    var a = -1, b = [], c, d = this.points, e = d.length; if (e) if (this.options.connectNulls) { for (c = e; c--; ) d[c].y === null && d.splice(c, 1); b = [d] } else n(d, function(c, g) {
                        c.y === null ? (g > a + 1 && b.push(d.slice(a +
1, g)), a = g) : g === e - 1 && b.push(d.slice(a + 1, g + 1))
                    }); this.segments = b
                }, setOptions: function(a) { var b = this.chart.options, c = b.plotOptions, d = a.data; a.data = null; c = H(c[this.type], c.series, a); c.data = a.data = d; this.tooltipOptions = H(b.tooltip, c.tooltip); return c }, getColor: function() { var a = this.chart.options.colors, b = this.chart.counters; this.color = this.options.color || a[b.color++] || "#0000ff"; b.wrapColor(a.length) }, getSymbol: function() {
                    var a = this.options.marker, b = this.chart, c = b.options.symbols, b = b.counters; this.symbol =
a.symbol || c[b.symbol++]; if (/^url/.test(this.symbol)) a.radius = 0; b.wrapSymbol(c.length)
                }, addPoint: function(a, b, c, d) {
                    var e = this.data, f = this.graph, g = this.area, h = this.chart, j = this.xData, k = this.yData, i = f && f.shift || 0, l = this.options.data; Ib(d, h); if (f && c) f.shift = i + 1; if (g) g.shift = i + 1, g.isArea = !0; b = r(b, !0); d = { series: this }; this.pointClass.prototype.applyOptions.apply(d, [a]); j.push(d.x); k.push(this.valueCount === 4 ? [d.open, d.high, d.low, d.close] : d.y); l.push(a); c && (e[0] ? e[0].remove(!1) : (e.shift(), j.shift(), k.shift(),
l.shift())); this.getAttribs(); this.isDirtyData = this.isDirty = !0; b && h.redraw()
                }, setData: function(a, b) {
                    var c = this.points, d = this.options, e = this.initialColor, f = this.chart, g = null; this.xIncrement = null; this.pointRange = this.xAxis && this.xAxis.categories && 1 || d.pointRange; if (A(e)) f.counters.color = e; var h = [], j = [], k = a ? a.length : [], i = this.valueCount === 4; if (k > (d.turboThreshold || 1E3)) {
                        for (e = 0; g === null && e < k; ) g = a[e], e++; if (Sb(g)) {
                            g = r(d.pointStart, 0); d = r(d.pointInterval, 1); for (e = 0; e < k; e++) h[e] = g, j[e] = a[e], g += d; this.xIncrement =
g
                        } else if (ic(g)) if (i) for (e = 0; e < k; e++) d = a[e], h[e] = d[0], j[e] = d.slice(1, 5); else for (e = 0; e < k; e++) d = a[e], h[e] = d[0], j[e] = d[1]
                    } else for (e = 0; e < k; e++) d = { series: this }, this.pointClass.prototype.applyOptions.apply(d, [a[e]]), h[e] = d.x, j[e] = i ? [d.open, d.high, d.low, d.close] : d.y; this.data = []; this.options.data = a; this.xData = h; this.yData = j; for (e = c && c.length || 0; e--; ) c[e] && c[e].destroy && c[e].destroy(); this.isDirty = this.isDirtyData = f.isDirtyBox = !0; r(b, !0) && f.redraw(!1)
                }, remove: function(a, b) {
                    var c = this, d = c.chart, a = r(a, !0);
                    if (!c.isRemoving) c.isRemoving = !0, Y(c, "remove", null, function() { c.destroy(); d.isDirtyLegend = d.isDirtyBox = !0; a && d.redraw(b) }); c.isRemoving = !1
                }, processData: function(a) {
                    var b = this.xData, c = this.yData, d = b.length, e = 0, f = d, g, h, j = this.xAxis, k = this.options, i = k.cropThreshold; if (this.isCartesian && !this.isDirty && !j.isDirty && !this.yAxis.isDirty && !a) return !1; if (!i || d > i || this.forceCrop) if (a = j.getExtremes(), j = a.min, i = a.max, b[d - 1] < j || b[0] > i) b = [], c = []; else if (b[0] < j || b[d - 1] > i) {
                        for (a = 0; a < d; a++) if (b[a] >= j) { e = T(0, a - 1); break } for (; a <
d; a++) if (b[a] > i) { f = a + 1; break } b = b.slice(e, f); c = c.slice(e, f); g = !0
                    } for (a = b.length - 1; a > 0; a--) if (d = b[a] - b[a - 1], h === G || d < h) h = d; this.cropped = g; this.cropStart = e; this.processedXData = b; this.processedYData = c; if (k.pointRange === null) this.pointRange = h || 1; this.closestPointRange = h
                }, generatePoints: function() {
                    var a = this.options.data, b = this.data, c, d = this.processedXData, e = this.processedYData, f = this.pointClass, g = d.length, h = this.cropStart || 0, j, k = this.hasGroupedData, i, l = [], m; if (!b && !k) b = [], b.length = a.length, b = this.data =
b; for (m = 0; m < g; m++) j = h + m, k ? l[m] = (new f).init(this, [d[m]].concat(sb(e[m]))) : (b[j] ? i = b[j] : b[j] = i = (new f).init(this, a[j], d[m]), l[m] = i); if (b && (g !== (c = b.length) || k)) for (m = 0; m < c; m++) m === h && !k && (m += g), b[m] && b[m].destroyElements(); this.data = b; this.points = l
                }, translate: function() {
                    this.processedXData || this.processData(); this.generatePoints(); var a = this.chart, b = this.options, c = b.stacking, d = this.xAxis, e = d.categories, f = this.yAxis, g = this.points, h = g.length, j = !!this.modifyValue, k = this.index === f.series.length - 1, i; for (i =
0; i < h; i++) {
                        var l = g[i], m = l.x, p = l.y, n = l.low, r = f.stacks[(p < b.threshold ? "-" : "") + this.stackKey]; l.plotX = y(d.translate(m) * 10) / 10; if (c && this.visible && r && r[m]) { n = r[m]; m = n.total; n.cum = n = n.cum - p; p = n + p; if (k) n = b.threshold; c === "percent" && (n = m ? n * 100 / m : 0, p = m ? p * 100 / m : 0); l.percentage = m ? l.y * 100 / m : 0; l.stackTotal = m } if (A(n)) l.yBottom = f.translate(n, 0, 1, 0, 1); j && (p = this.modifyValue(p, l)); if (p !== null) l.plotY = y(f.translate(p, 0, 1, 0, 1) * 10) / 10; l.clientX = a.inverted ? a.plotHeight - l.plotX : l.plotX; l.category = e && e[l.x] !== G ? e[l.x] :
l.x
                    } this.getSegments()
                }, setTooltipPoints: function(a) { var b = this.chart, c = b.inverted, d = [], b = y((c ? b.plotTop : b.plotLeft) + b.plotSizeX), e, f; e = this.xAxis; var g, h, j = []; if (this.options.enableMouseTracking !== !1) { if (a) this.tooltipPoints = null; n(this.segments || this.points, function(a) { d = d.concat(a) }); e && e.reversed && (d = d.reverse()); a = d.length; for (h = 0; h < a; h++) { g = d[h]; e = d[h - 1] ? d[h - 1]._high + 1 : 0; for (f = g._high = d[h + 1] ? Na((g.plotX + (d[h + 1] ? d[h + 1].plotX : b)) / 2) : b; e <= f; ) j[c ? b - e++ : e++] = g } this.tooltipPoints = j } }, tooltipHeaderFormatter: function(a) {
                    var b =
this.tooltipOptions, c = b.xDateFormat || "%A, %b %e, %Y", d = this.xAxis; return b.headerFormat.replace("{point.key}", d && d.options.type === "datetime" ? wb(c, a) : a).replace("{series.name}", this.name).replace("{series.color}", this.color)
                }, onMouseOver: function() { var a = this.chart, b = a.hoverSeries; if (Ga || !a.mouseIsDown) { if (b && b !== this) b.onMouseOut(); this.options.events.mouseOver && Y(this, "mouseOver"); this.setState(fb); a.hoverSeries = this } }, onMouseOut: function() {
                    var a = this.options, b = this.chart, c = b.tooltip, d = b.hoverPoint;
                    if (d) d.onMouseOut(); this && a.events.mouseOut && Y(this, "mouseOut"); c && !a.stickyTracking && !c.shared && c.hide(); this.setState(); b.hoverSeries = null
                }, animate: function(a) { var b = this.chart, c = this.clipRect, d = this.options.animation; d && !rb(d) && (d = {}); if (a) { if (!c.isAnimating) c.attr("width", 0), c.isAnimating = !0 } else c.animate({ width: b.plotSizeX }, d), this.animate = null }, drawPoints: function() {
                    var a, b = this.points, c = this.chart, d, e, f, g, h, j, k, i; if (this.options.marker.enabled) for (f = b.length; f--; ) if (g = b[f], d = g.plotX, e = g.plotY,
i = g.graphic, e !== G && !isNaN(e)) if (a = g.pointAttr[g.selected ? "select" : la], h = a.r, j = r(g.marker && g.marker.symbol, this.symbol), k = j.indexOf("url") === 0, i) i.animate(E({ x: d - h, y: e - h }, i.symbolName ? { width: 2 * h, height: 2 * h} : {})); else if (h > 0 || k) g.graphic = c.renderer.symbol(j, d - h, e - h, 2 * h, 2 * h).attr(a).add(this.group)
                }, convertAttribs: function(a, b, c, d) { var e = this.pointAttrToOptions, f, g, h = {}, a = a || {}, b = b || {}, c = c || {}, d = d || {}; for (f in e) g = e[f], h[f] = r(a[g], b[f], c[f], d[f]); return h }, getAttribs: function() {
                    var a = this, b = aa[a.type].marker ?
a.options.marker : a.options, c = b.states, d = c[fb], e, f = a.color, g = { stroke: f, fill: f }, h = a.points, j = [], k, i = a.pointAttrToOptions, l; a.options.marker ? (d.radius = d.radius || b.radius + 2, d.lineWidth = d.lineWidth || b.lineWidth + 1) : d.color = d.color || cb(d.color || f).brighten(d.brightness).get(); j[la] = a.convertAttribs(b, g); n([fb, "select"], function(b) { j[b] = a.convertAttribs(c[b], j[la]) }); a.pointAttr = j; for (f = h.length; f--; ) {
                        g = h[f]; if ((b = g.options && g.options.marker || g.options) && b.enabled === !1) b.radius = 0; e = !1; if (g.options) for (l in i) A(b[i[l]]) &&
(e = !0); if (e) { k = []; c = b.states || {}; e = c[fb] = c[fb] || {}; if (!a.options.marker) e.color = cb(e.color || g.options.color).brighten(e.brightness || d.brightness).get(); k[la] = a.convertAttribs(b, j[la]); k[fb] = a.convertAttribs(c[fb], j[fb], k[la]); k.select = a.convertAttribs(c.select, j.select, k[la]) } else k = j; g.pointAttr = k
                    } 
                }, destroy: function() {
                    var a = this, b = a.chart, c = a.clipRect, d = /AppleWebKit\/533/.test(ib), e, f, g = a.data || [], h, j, k; Y(a, "destroy"); ra(a); n(["xAxis", "yAxis"], function(b) { if (k = a[b]) Fb(k.series, a), k.isDirty = !0 });
                    a.legendItem && a.chart.legend.destroyItem(a); for (f = g.length; f--; ) (h = g[f]) && h.destroy && h.destroy(); a.points = null; if (c && c !== b.clipRect) a.clipRect = c.destroy(); n(["area", "graph", "dataLabelsGroup", "group", "tracker"], function(b) { a[b] && (e = d && b === "group" ? "hide" : "destroy", a[b][e]()) }); if (b.hoverSeries === a) b.hoverSeries = null; Fb(b.series, a); for (j in a) delete a[j]
                }, drawDataLabels: function() {
                    if (this.options.dataLabels.enabled) {
                        var a = this, b, c, d = a.points, e = a.options, f = e.dataLabels, g, h, j, k = a.dataLabelsGroup, i = a.chart,
l = a.xAxis, l = l ? l.left : i.plotLeft, m = a.yAxis, m = m ? m.top : i.plotTop, p = i.renderer, t = i.inverted, x = a.type, u = e.stacking, w = x === "column" || x === "bar", v = f.verticalAlign === null, q = f.y === null, ca; w && (u ? (v && (f = H(f, { verticalAlign: "middle" })), q && (f = H(f, { y: { top: 14, middle: 4, bottom: -6}[f.verticalAlign] }))) : v && (f = H(f, { verticalAlign: "top" }))); k ? k.translate(l, m) : k = a.dataLabelsGroup = p.g("data-labels").attr({ visibility: a.visible ? bb : Ta, zIndex: 6 }).translate(l, m).add(); h = f; n(d, function(d) {
    ca = d.dataLabel; f = h; (g = d.options) && g.dataLabels &&
(f = H(f, g.dataLabels)); if (ca && a.isCartesian && !i.isInsidePlot(d.plotX, d.plotY)) d.dataLabel = ca.destroy(); else if (f.enabled) {
        j = f.formatter.call(d.getLabelConfig(), f); var l = d.barX, m = l && l + d.barW / 2 || d.plotX || -999, n = r(d.plotY, -999), v = f.align, K = q ? d.y >= 0 ? -6 : 12 : f.y; b = (t ? i.plotWidth - n : m) + f.x; c = (t ? i.plotHeight - m : n) + K; x === "column" && (b += { left: -1, right: 1}[v] * d.barW / 2 || 0); !u && t && d.y < 0 && (v = "right", b -= 10); f.style.color = r(f.color, f.style.color, a.color, "black"); if (ca) t && !f.y && (c = c + N(ca.styles.lineHeight) * 0.9 - ca.getBBox().height /
2), ca.attr({ text: j }).animate({ x: b, y: c }); else if (A(j)) ca = d.dataLabel = p.text(j, b, c).attr({ align: v, rotation: f.rotation, zIndex: 1 }).css(f.style).add(k), t && !f.y && ca.attr({ y: c + N(ca.styles.lineHeight) * 0.9 - ca.getBBox().height / 2 }); if (w && e.stacking && ca) m = d.barY, n = d.barW, d = d.barH, ca.align(f, null, { x: t ? i.plotWidth - m - d : l, y: t ? i.plotHeight - l - n : m, width: t ? d : n, height: t ? n : d })
    } 
})
                    } 
                }, drawGraph: function() {
                    var a = this, b = a.options, c = a.graph, d = [], e, f = a.area, g = a.group, h = b.lineColor || a.color, j = b.lineWidth, k = b.dashStyle, i, l = a.chart.renderer,
m = a.yAxis.getThreshold(b.threshold), p = /^area/.test(a.type), t = [], x = []; n(a.segments, function(c) {
    i = []; n(c, function(d, e) { a.getPointSpline ? i.push.apply(i, a.getPointSpline(c, d, e)) : (i.push(e ? ga : ta), e && b.step && i.push(d.plotX, c[e - 1].plotY), i.push(d.plotX, d.plotY)) }); c.length > 1 ? d = d.concat(i) : t.push(c[0]); if (p) {
        var e = [], f, g = i.length; for (f = 0; f < g; f++) e.push(i[f]); g === 3 && e.push(ga, i[1], i[2]); if (b.stacking && a.type !== "areaspline") for (f = c.length - 1; f >= 0; f--) f < c.length - 1 && b.step && e.push(c[f + 1].plotX, c[f].yBottom), e.push(c[f].plotX,
c[f].yBottom); else e.push(ga, c[c.length - 1].plotX, m, ga, c[0].plotX, m); x = x.concat(e)
    } 
}); a.graphPath = d; a.singlePoints = t; if (p) e = r(b.fillColor, cb(a.color).setOpacity(b.fillOpacity || 0.75).get()), f ? f.animate({ d: x }) : a.area = a.chart.renderer.path(x).attr({ fill: e }).add(g); if (c) Jb(c), c.animate({ d: d }); else if (j) { c = { stroke: h, "stroke-width": j }; if (k) c.dashstyle = k; a.graph = l.path(d).attr(c).add(g).shadow(b.shadow) } 
                }, render: function() {
                    var a = this, b = a.chart, c, d, e = a.options, f = e.clip !== !1, g = e.animation, h = g && a.animate, g =
h ? g && g.duration || 500 : 0, j = a.clipRect, k = b.renderer; if (!j && (j = a.clipRect = !b.hasRendered && b.clipRect ? b.clipRect : k.clipRect(0, 0, b.plotSizeX, b.plotSizeY + 1), !b.clipRect)) b.clipRect = j; if (!a.group) c = a.group = k.g("series"), b.inverted && (d = function() { c.attr({ width: b.plotWidth, height: b.plotHeight }).invert() }, d(), W(b, "resize", d), W(a, "destroy", function() { ra(b, "resize", d) })), f && c.clip(j), c.attr({ visibility: a.visible ? bb : Ta, zIndex: e.zIndex }).translate(a.xAxis.left, a.yAxis.top).add(b.seriesGroup); a.drawDataLabels();
                    h && a.animate(!0); a.getAttribs(); a.drawGraph && a.drawGraph(); a.drawPoints(); a.options.enableMouseTracking !== !1 && a.drawTracker(); h && a.animate(); setTimeout(function() { j.isAnimating = !1; if ((c = a.group) && j !== b.clipRect && j.renderer) { if (f) c.clip(a.clipRect = b.clipRect); j.destroy() } }, g); a.isDirty = a.isDirtyData = !1
                }, redraw: function() {
                    var a = this.chart, b = this.isDirtyData, c = this.group; c && (a.inverted && c.attr({ width: a.plotWidth, height: a.plotHeight }), c.animate({ translateX: this.xAxis.left, translateY: this.yAxis.top }));
                    this.translate(); this.setTooltipPoints(!0); this.render(); b && Y(this, "updatedData")
                }, setState: function(a) { var b = this.options, c = this.graph, d = b.states, b = b.lineWidth, a = a || la; if (this.state !== a) this.state = a, d[a] && d[a].enabled === !1 || (a && (b = d[a].lineWidth || b + 1), c && !c.dashstyle && c.attr({ "stroke-width": b }, a ? 0 : 500)) }, setVisible: function(a, b) {
                    var c = this.chart, d = this.legendItem, e = this.group, f = this.tracker, g = this.dataLabelsGroup, h, j = this.points, k = c.options.chart.ignoreHiddenSeries; h = this.visible; h = (this.visible =
a = a === G ? !h : a) ? "show" : "hide"; if (e) e[h](); if (f) f[h](); else if (j) for (e = j.length; e--; ) if (f = j[e], f.tracker) f.tracker[h](); if (g) g[h](); d && c.legend.colorizeItem(this, a); this.isDirty = !0; this.options.stacking && n(c.series, function(a) { if (a.options.stacking && a.visible) a.isDirty = !0 }); if (k) c.isDirtyBox = !0; b !== !1 && c.redraw(); Y(this, h)
                }, show: function() { this.setVisible(!0) }, hide: function() { this.setVisible(!1) }, select: function(a) {
                    this.selected = a = a === G ? !this.selected : a; if (this.checkbox) this.checkbox.checked = a; Y(this,
a ? "select" : "unselect")
                }, drawTracker: function() {
                    var a = this, b = a.options, c = [].concat(a.graphPath), d = c.length, e = a.chart, f = e.renderer, g = e.options.tooltip.snap, h = a.tracker, j = b.cursor, j = j && { cursor: j }, k = a.singlePoints, i; if (d) for (i = d + 1; i--; ) c[i] === ta && c.splice(i + 1, 0, c[i + 1] - g, c[i + 2], ga), (i && c[i] === ta || i === d) && c.splice(i, 0, ga, c[i - 2] + g, c[i - 1]); for (i = 0; i < k.length; i++) d = k[i], c.push(ta, d.plotX - g, d.plotY, ga, d.plotX + g, d.plotY); h ? h.attr({ d: c }) : (h = f.g().clip(e.clipRect).add(e.trackerGroup), a.tracker = f.path(c).attr({ isTracker: !0,
                        stroke: Hc, fill: oa, "stroke-linejoin": "bevel", "stroke-width": b.lineWidth + 2 * g, visibility: a.visible ? bb : Ta, zIndex: b.zIndex || 1
                    }).on(Ga ? "touchstart" : "mouseover", function() { if (e.hoverSeries !== a) a.onMouseOver() }).on("mouseout", function() { if (!b.stickyTracking) a.onMouseOut() }).css(j).add(h))
                } 
                }; B = pa(M); ba.line = B; B = pa(M, { type: "area", useThreshold: !0 }); ba.area = B; B = pa(M, { type: "spline", getPointSpline: function(a, b, c) {
                    var d = b.plotX, e = b.plotY, f = a[c - 1], g = a[c + 1], h, j, k, i; if (c && c < a.length - 1) {
                        a = f.plotY; k = g.plotX; var g = g.plotY,
l; h = (1.5 * d + f.plotX) / 2.5; j = (1.5 * e + a) / 2.5; k = (1.5 * d + k) / 2.5; i = (1.5 * e + g) / 2.5; l = (i - j) * (k - d) / (k - h) + e - i; j += l; i += l; j > a && j > e ? (j = T(a, e), i = 2 * e - j) : j < a && j < e && (j = va(a, e), i = 2 * e - j); i > g && i > e ? (i = T(g, e), j = 2 * e - i) : i < g && i < e && (i = va(g, e), j = 2 * e - i); b.rightContX = k; b.rightContY = i
                    } c ? (b = ["C", f.rightContX || f.plotX, f.rightContY || f.plotY, h || d, j || e, d, e], f.rightContX = f.rightContY = null) : b = [ta, d, e]; return b
                } 
                }); ba.spline = B; B = pa(B, { type: "areaspline", useThreshold: !0 }); ba.areaspline = B; var fc = pa(M, { type: "column", useThreshold: !0, tooltipOutsidePlot: !0,
                    pointAttrToOptions: { stroke: "borderColor", "stroke-width": "borderWidth", fill: "color", r: "borderRadius" }, init: function() { M.prototype.init.apply(this, arguments); var a = this, b = a.chart; b.hasRendered && n(b.series, function(b) { if (b.type === a.type) b.isDirty = !0 }) }, translate: function() {
                        var a = this, b = a.chart, c = a.options, d = c.stacking, e = c.borderWidth, f = 0, g = a.xAxis, h = g.reversed, j = {}, k, i; M.prototype.translate.apply(a); n(b.series, function(b) {
                            if (b.type === a.type && b.visible && a.options.group === b.options.group) b.options.stacking ?
(k = b.stackKey, j[k] === G && (j[k] = f++), i = j[k]) : i = f++, b.columnIndex = i
                        }); var l = a.points, g = Ba(g.translationSlope) * (g.ordinalSlope || g.closestPointRange || 1), m = g * c.groupPadding, p = (g - 2 * m) / f, t = c.pointWidth, x = A(t) ? (p - t) / 2 : p * c.pointPadding, u = bc(T(r(t, p - 2 * x), 1)), w = x + (m + ((h ? f - a.columnIndex : a.columnIndex) || 0) * p - g / 2) * (h ? -1 : 1), v = a.yAxis.getThreshold(c.threshold), q = r(c.minPointLength, 5); n(l, function(f) {
                            var g = f.plotY, h = f.yBottom || v, i = f.plotX + w, j = bc(va(g, h)), k = bc(T(g, h) - j), l = a.yAxis.stacks[(f.y < 0 ? "-" : "") + a.stackKey]; d &&
a.visible && l && l[f.x] && l[f.x].setOffset(w, u); Ba(k) < q && q && (k = q, j = Ba(j - v) > q ? h - q : v - (g <= v ? q : 0)); E(f, { barX: i, barY: j, barW: u, barH: k }); f.shapeType = "rect"; g = E(b.renderer.Element.prototype.crisp.apply({}, [e, i, j, u, k]), { r: c.borderRadius }); e % 2 && (g.y -= 1, g.height += 1); f.shapeArgs = g; f.trackerArgs = Ba(k) < 3 && H(f.shapeArgs, { height: 6, y: j - 3 })
                        })
                    }, getSymbol: function() { }, drawGraph: function() { }, drawPoints: function() {
                        var a = this, b = a.options, c = a.chart.renderer, d, e; n(a.points, function(f) {
                            var g = f.plotY; if (g !== G && !isNaN(g) && f.y !==
null) d = f.graphic, e = f.shapeArgs, d ? (Jb(d), d.animate(e)) : f.graphic = d = c[f.shapeType](e).attr(f.pointAttr[f.selected ? "select" : la]).add(a.group).shadow(b.shadow)
                        })
                    }, drawTracker: function() {
                        var a = this, b = a.chart, c = b.renderer, d, e, f = +new Date, g = a.options, h = g.cursor, j = h && { cursor: h }, k, i; a.isCartesian && (k = c.g().clip(b.clipRect).add(b.trackerGroup)); n(a.points, function(h) {
                            e = h.tracker; d = h.trackerArgs || h.shapeArgs; delete d.strokeWidth; if (h.y !== null) e ? e.attr(d) : h.tracker = c[h.shapeType](d).attr({ isTracker: f, fill: Hc,
                                visibility: a.visible ? bb : Ta, zIndex: g.zIndex || 1
                            }).on(Ga ? "touchstart" : "mouseover", function(c) { i = c.relatedTarget || c.fromElement; if (b.hoverSeries !== a && R(i, "isTracker") !== f) a.onMouseOver(); h.onMouseOver() }).on("mouseout", function(b) { if (!g.stickyTracking && (i = b.relatedTarget || b.toElement, R(i, "isTracker") !== f)) a.onMouseOut() }).css(j).add(h.group || k)
                        })
                    }, animate: function(a) {
                        var b = this, c = b.points; if (!a) n(c, function(a) {
                            var c = a.graphic, a = a.shapeArgs; c && (c.attr({ height: 0, y: b.yAxis.translate(0, 0, 1) }), c.animate({ height: a.height,
                                y: a.y
                            }, b.options.animation))
                        }), b.animate = null
                    }, remove: function() { var a = this, b = a.chart; b.hasRendered && n(b.series, function(b) { if (b.type === a.type) b.isDirty = !0 }); M.prototype.remove.apply(a, arguments) } 
                }); ba.column = fc; B = pa(fc, { type: "bar", init: function() { this.inverted = !0; fc.prototype.init.apply(this, arguments) } }); ba.bar = B; B = pa(M, { type: "scatter", translate: function() { var a = this; M.prototype.translate.apply(a); n(a.points, function(b) { b.shapeType = "circle"; b.shapeArgs = { x: b.plotX, y: b.plotY, r: a.chart.options.tooltip.snap} }) },
                    drawTracker: function() { for (var a = this, b = a.options.cursor, b = b && { cursor: b }, c = a.points, d = c.length, e; d--; ) if (e = c[d].graphic) e.element._index = d; a._hasTracking ? a._hasTracking = !0 : a.group.on(Ga ? "touchstart" : "mouseover", function(b) { a.onMouseOver(); c[b.target._index].onMouseOver() }).on("mouseout", function() { if (!a.options.stickyTracking) a.onMouseOut() }).css(b) } 
                }); ba.scatter = B; B = pa(jb, { init: function() {
                    jb.prototype.init.apply(this, arguments); var a = this, b; E(a, { visible: a.visible !== !1, name: r(a.name, "Slice") }); b =
function() { a.slice() }; W(a, "select", b); W(a, "unselect", b); return a
                }, setVisible: function(a) { var b = this.series.chart, c = this.tracker, d = this.dataLabel, e = this.connector, f = this.shadowGroup, g; g = (this.visible = a = a === G ? !this.visible : a) ? "show" : "hide"; this.group[g](); if (c) c[g](); if (d) d[g](); if (e) e[g](); if (f) f[g](); this.legendItem && b.legend.colorizeItem(this, a) }, slice: function(a, b, c) {
                    var d = this.series.chart, e = this.slicedTranslation; Ib(c, d); r(b, !0); a = this.sliced = A(a) ? a : !this.sliced; a = { translateX: a ? e[0] : d.plotLeft,
                        translateY: a ? e[1] : d.plotTop
                    }; this.group.animate(a); this.shadowGroup && this.shadowGroup.animate(a)
                } 
                }); B = pa(M, { type: "pie", isCartesian: !1, pointClass: B, pointAttrToOptions: { stroke: "borderColor", "stroke-width": "borderWidth", fill: "color" }, getColor: function() { this.initialColor = this.chart.counters.color }, animate: function() { var a = this; n(a.points, function(b) { var c = b.graphic, b = b.shapeArgs, d = -Ia / 2; c && (c.attr({ r: 0, start: d, end: d }), c.animate({ r: b.r, start: b.start, end: b.end }, a.options.animation)) }); a.animate = null },
                    setData: function() { M.prototype.setData.apply(this, arguments); this.processData(); this.generatePoints() }, translate: function() {
                        this.generatePoints(); var a = 0, b = -0.25, c = this.options, d = c.slicedOffset, e = d + c.borderWidth, f = c.center.concat([c.size, c.innerSize || 0]), g = this.chart, h = g.plotWidth, j = g.plotHeight, k, i, l, m = this.points, p = 2 * Ia, t, r = va(h, j), u, w, v, q = c.dataLabels.distance, f = nb(f, function(a, b) { return (u = /%$/.test(a)) ? [h, j, r, r][b] * N(a) / 100 : a }); this.getX = function(a, b) {
                            l = sa.asin((a - f[1]) / (f[2] / 2 + q)); return f[0] +
(b ? -1 : 1) * ia(l) * (f[2] / 2 + q)
                        }; this.center = f; n(m, function(b) { a += b.y }); n(m, function(c) {
                            t = a ? c.y / a : 0; k = y(b * p * 1E3) / 1E3; b += t; i = y(b * p * 1E3) / 1E3; c.shapeType = "arc"; c.shapeArgs = { x: f[0], y: f[1], r: f[2] / 2, innerR: f[3] / 2, start: k, end: i }; l = (i + k) / 2; c.slicedTranslation = nb([ia(l) * d + g.plotLeft, ma(l) * d + g.plotTop], y); w = ia(l) * f[2] / 2; v = ma(l) * f[2] / 2; c.tooltipPos = [f[0] + w * 0.7, f[1] + v * 0.7]; c.labelPos = [f[0] + w + ia(l) * q, f[1] + v + ma(l) * q, f[0] + w + ia(l) * e, f[1] + v + ma(l) * e, f[0] + w, f[1] + v, q < 0 ? "center" : l < p / 4 ? "left" : "right", l]; c.percentage = t * 100; c.total =
a
                        }); this.setTooltipPoints()
                    }, render: function() { this.getAttribs(); this.drawPoints(); this.options.enableMouseTracking !== !1 && this.drawTracker(); this.drawDataLabels(); this.options.animation && this.animate && this.animate(); this.isDirty = !1 }, drawPoints: function() {
                        var a = this.chart, b = a.renderer, c, d, e, f = this.options.shadow, g, h; n(this.points, function(j) {
                            d = j.graphic; h = j.shapeArgs; e = j.group; g = j.shadowGroup; if (f && !g) g = j.shadowGroup = b.g("shadow").attr({ zIndex: 4 }).add(); if (!e) e = j.group = b.g("point").attr({ zIndex: 5 }).add();
                            c = j.sliced ? j.slicedTranslation : [a.plotLeft, a.plotTop]; e.translate(c[0], c[1]); g && g.translate(c[0], c[1]); d ? d.animate(h) : j.graphic = b.arc(h).attr(E(j.pointAttr[la], { "stroke-linejoin": "round" })).add(j.group).shadow(f, g); j.visible === !1 && j.setVisible(!1)
                        })
                    }, drawDataLabels: function() {
                        var a = this.data, b, c = this.chart, d = this.options.dataLabels, e = r(d.connectorPadding, 10), f = r(d.connectorWidth, 1), g, h, j = r(d.softConnector, !0), k = d.distance, i = this.center, l = i[2] / 2, i = i[1], m = k > 0, p = [[], []], t, x, u, w, v = 2, q; if (d.enabled) {
                            M.prototype.drawDataLabels.apply(this);
                            n(a, function(a) { a.dataLabel && p[a.labelPos[7] < Ia / 2 ? 0 : 1].push(a) }); p[1].reverse(); w = function(a, b) { return b.y - a.y }; for (a = p[0][0] && p[0][0].dataLabel && N(p[0][0].dataLabel.styles.lineHeight); v--; ) {
                                var y = [], A = [], I = p[v], B = I.length, s; for (q = i - l - k; q <= i + l + k; q += a) y.push(q); u = y.length; if (B > u) { h = [].concat(I); h.sort(w); for (q = B; q--; ) h[q].rank = q; for (q = B; q--; ) I[q].rank >= u && I.splice(q, 1); B = I.length } for (q = 0; q < B; q++) {
                                    b = I[q]; h = b.labelPos; b = 9999; for (x = 0; x < u; x++) g = Ba(y[x] - h[1]), g < b && (b = g, s = x); if (s < q && y[q] !== null) s = q; else for (u <
B - q + s && y[q] !== null && (s = u - B + q); y[s] === null; ) s++; A.push({ i: s, y: y[s] }); y[s] = null
                                } A.sort(w); for (q = 0; q < B; q++) {
                                    b = I[q]; h = b.labelPos; g = b.dataLabel; x = A.pop(); t = h[1]; u = b.visible === !1 ? Ta : bb; s = x.i; x = x.y; if (t > x && y[s + 1] !== null || t < x && y[s - 1] !== null) x = t; t = this.getX(s === 0 || s === y.length - 1 ? t : x, v); g.attr({ visibility: u, align: h[6] })[g.moved ? "animate" : "attr"]({ x: t + d.x + ({ left: e, right: -e}[h[6]] || 0), y: x + d.y }); g.moved = !0; if (m && f) g = b.connector, h = j ? [ta, t + (h[6] === "left" ? 5 : -5), x, "C", t, x, 2 * h[2] - h[4], 2 * h[3] - h[5], h[2], h[3], ga, h[4],
h[5]] : [ta, t + (h[6] === "left" ? 5 : -5), x, ga, h[2], h[3], ga, h[4], h[5]], g ? (g.animate({ d: h }), g.attr("visibility", u)) : b.connector = g = this.chart.renderer.path(h).attr({ "stroke-width": f, stroke: d.connectorColor || b.color || "#606060", visibility: u, zIndex: 3 }).translate(c.plotLeft, c.plotTop).add()
                                } 
                            } 
                        } 
                    }, drawTracker: fc.prototype.drawTracker, getSymbol: function() { } 
                }); ba.pie = B; var Q = M.prototype, Uc = Q.processData, Vc = Q.generatePoints, Wc = Q.destroy, Xc = Q.tooltipHeaderFormatter, B = { approximation: "average", groupPixelWidth: 2, dateTimeLabelFormats: ja(tb,
["%A, %b %e, %H:%M:%S.%L", "%A, %b %e, %H:%M:%S.%L", "-%H:%M:%S.%L"], kb, ["%A, %b %e, %H:%M:%S", "%A, %b %e, %H:%M:%S", "-%H:%M:%S"], db, ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], za, ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], ka, ["%A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"], Ca, ["Week from %A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"], Ka, ["%B %Y", "%B", "-%B %Y"], La, ["%Y", "%Y", "-%Y"])
                }, Kc = [[tb, [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], [kb, [1, 2, 5, 10, 15, 30]], [db, [1, 2, 5, 10, 15, 30]], [za, [1, 2, 3, 4, 6, 8, 12]], [ka,
[1]], [Ca, [1]], [Ka, [1, 3, 6]], [La, null]], ob = { sum: function(a) { var b = a.length, c; if (!b && a.hasNulls) c = null; else if (b) for (c = 0; b--; ) c += a[b]; return c }, average: function(a) { var b = a.length, a = ob.sum(a); typeof a === "number" && b && (a /= b); return a }, open: function(a) { return a.length ? a[0] : a.hasNulls ? null : G }, high: function(a) { return a.length ? Ab(a) : a.hasNulls ? null : G }, low: function(a) { return a.length ? Gb(a) : a.hasNulls ? null : G }, close: function(a) { return a.length ? a[a.length - 1] : a.hasNulls ? null : G }, ohlc: function(a, b, c, d) {
    a = ob.open(a);
    b = ob.high(b); c = ob.low(c); d = ob.close(d); if (typeof a === "number" || typeof b === "number" || typeof c === "number" || typeof d === "number") return [a, b, c, d]
} 
}; Q.groupData = function(a, b, c, d) {
    var e = this.data, f = this.options.data, g = [], h = [], j = a.length, k, i, l = !!b; i = []; var m = [], n = [], t = [], r = typeof d === "function" ? d : ob[d], u; for (u = 0; u <= j; u++) {
        for (; c[1] !== G && a[u] >= c[1] || u === j; ) if (k = c.shift(), i = r(i, m, n, t), i !== G && (g.push(k), h.push(i)), i = [], m = [], n = [], t = [], u === j) break; if (u === j) break; k = l ? b[u] : null; if (d === "ohlc") {
            k = this.cropStart + u;
            var w = e && e[k] || this.pointClass.prototype.applyOptions.apply({}, [f[k]]); k = w.open; var v = w.high, q = w.low, w = w.close; if (typeof k === "number") i.push(k); else if (k === null) i.hasNulls = !0; if (typeof v === "number") m.push(v); else if (v === null) m.hasNulls = !0; if (typeof q === "number") n.push(q); else if (q === null) n.hasNulls = !0; if (typeof w === "number") t.push(w); else if (w === null) t.hasNulls = !0
        } else if (typeof k === "number") i.push(k); else if (k === null) i.hasNulls = !0
    } return [g, h]
}; Q.processData = function() {
    var a = this.options, b = a.dataGrouping,
c = b && b.enabled, d = this.groupedData, e; this.forceCrop = c; if (Uc.apply(this, arguments) !== !1 && c) {
        n(d || [], function(a, b) { a && (d[b] = a.destroy ? a.destroy() : null) }); var f; f = this.chart; var c = this.processedXData, g = this.processedYData, h = f.plotSizeX, j = this.xAxis, k = r(j.groupPixelWidth, b.groupPixelWidth), i = c.length, l = f.series, m = this.pointRange; if (!j.groupPixelWidth) { for (f = l.length; f--; ) l[f].xAxis === j && l[f].options.dataGrouping && (k = T(k, l[f].options.dataGrouping.groupPixelWidth)); j.groupPixelWidth = k } if (i > h / k || b.forced) {
            e =
!0; this.points = null; f = j.getExtremes(); i = f.min; l = f.max; f = j.getGroupIntervalFactor && j.getGroupIntervalFactor(i, l, c) || 1; h = k * (l - i) / h * f; j = (j.getNonLinearTimeTicks || Vb)(tc(h, b.units || Kc), i, l, null, c, this.closestPointRange); g = Q.groupData.apply(this, [c, g, j, b.approximation]); c = g[0]; g = g[1]; if (b.smoothed) { f = c.length - 1; for (c[f] = l; f-- && f > 0; ) c[f] += h / 2; c[0] = i } this.currentDataGrouping = j.info; if (a.pointRange === null) this.pointRange = j.info.totalRange; this.closestPointRange = j.info.totalRange; this.processedXData = c; this.processedYData =
g
        } else this.currentDataGrouping = null, this.pointRange = m; this.hasGroupedData = e
    } 
}; Q.generatePoints = function() { Vc.apply(this); this.groupedData = this.hasGroupedData ? this.points : null }; Q.tooltipHeaderFormatter = function(a) {
    var b = this.tooltipOptions, c = this.options.dataGrouping, d = b.xDateFormat, e, f = this.xAxis, g, h; if (f && f.options.type === "datetime" && c) {
        g = this.currentDataGrouping; c = c.dateTimeLabelFormats; if (g) f = c[g.unitName], g.count === 1 ? d = f[0] : (d = f[1], e = f[2]); else if (!d) for (h in D) if (D[h] >= f.closestPointRange) {
            d =
c[h][0]; break
        } d = wb(d, a); e && (d += wb(e, a + g.totalRange - 1)); a = b.headerFormat.replace("{point.key}", d)
    } else a = Xc.apply(this, [a]); return a
}; Q.destroy = function() { for (var a = this.groupedData || [], b = a.length; b--; ) a[b] && a[b].destroy(); Wc.apply(this) }; aa.line.dataGrouping = aa.spline.dataGrouping = aa.area.dataGrouping = aa.areaspline.dataGrouping = B; aa.column.dataGrouping = H(B, { approximation: "sum", groupPixelWidth: 10 }); aa.ohlc = H(aa.column, { lineWidth: 1, dataGrouping: { approximation: "ohlc", enabled: !0, groupPixelWidth: 5 }, states: { hover: { lineWidth: 3}} });
                var B = pa(jb, { applyOptions: function(a) { var b = this.series, c = 0; if (typeof a === "object" && typeof a.length !== "number") E(this, a), this.options = a; else if (a.length) { if (a.length === 5) { if (typeof a[0] === "string") this.name = a[0]; else if (typeof a[0] === "number") this.x = a[0]; c++ } this.open = a[c++]; this.high = a[c++]; this.low = a[c++]; this.close = a[c++] } this.y = this.high; if (this.x === G && b) this.x = b.autoIncrement(); return this }, tooltipFormatter: function() {
                    var a = this.series; return ['<span style="color:' + a.color + ';font-weight:bold">',
this.name || a.name, "</span><br/>Open: ", this.open, "<br/>High: ", this.high, "<br/>Low: ", this.low, "<br/>Close: ", this.close, "<br/>"].join("")
                } 
                }), qc = pa(ba.column, { type: "ohlc", valueCount: 4, pointClass: B, useThreshold: !1, pointAttrToOptions: { stroke: "color", "stroke-width": "lineWidth" }, translate: function() { var a = this.yAxis; ba.column.prototype.translate.apply(this); n(this.points, function(b) { if (b.open !== null) b.plotOpen = a.translate(b.open, 0, 1, 0, 1); if (b.close !== null) b.plotClose = a.translate(b.close, 0, 1, 0, 1) }) }, drawPoints: function() {
                    var a =
this, b = a.chart, c, d, e, f, g, h, j, k; n(a.points, function(i) { if (i.plotY !== G) j = i.graphic, c = i.pointAttr[i.selected ? "selected" : ""], f = c["stroke-width"] % 2 / 2, k = y(i.plotX) + f, g = y(i.barW / 2), h = ["M", k, y(i.yBottom), "L", k, y(i.plotY)], i.open !== null && (d = y(i.plotOpen) + f, h.push("M", k, d, "L", k - g, d)), i.close !== null && (e = y(i.plotClose) + f, h.push("M", k, e, "L", k + g, e)), j ? j.animate({ d: h }) : i.graphic = b.renderer.path(h).attr(c).add(a.group) })
                }, animate: null
                }); ba.ohlc = qc; aa.candlestick = H(aa.column, { dataGrouping: { approximation: "ohlc", enabled: !0 },
                    lineColor: "black", lineWidth: 1, upColor: "white", states: { hover: { lineWidth: 2}}
                }); B = pa(qc, { type: "candlestick", pointAttrToOptions: { fill: "color", stroke: "lineColor", "stroke-width": "lineWidth" }, getAttribs: function() { qc.prototype.getAttribs.apply(this, arguments); var a = this.options, b = a.states, a = a.upColor, c = H(this.pointAttr); c[""].fill = a; c.hover.fill = b.hover.upColor || a; c.select.fill = b.select.upColor || a; n(this.points, function(a) { if (a.open < a.close) a.pointAttr = c }) }, drawPoints: function() {
                    var a = this, b = a.chart, c, d,
e, f, g, h, j, k, i, l; n(a.points, function(m) { k = m.graphic; if (m.plotY !== G) c = m.pointAttr[m.selected ? "selected" : ""], h = c["stroke-width"] % 2 / 2, j = y(m.plotX) + h, d = y(m.plotOpen) + h, e = y(m.plotClose) + h, f = sa.min(d, e), g = sa.max(d, e), l = y(m.barW / 2), i = ["M", j - l, g, "L", j - l, f, "L", j + l, f, "L", j + l, g, "L", j - l, g, "M", j, g, "L", j, y(m.yBottom), "M", j, f, "L", j, y(m.plotY), "Z"], k ? k.animate({ d: i }) : m.graphic = b.renderer.path(i).attr(c).add(a.group) })
                } 
                }); ba.candlestick = B; var gc = Mb.prototype.symbols; aa.flags = H(aa.column, { dataGrouping: null, fillColor: "white",
                    lineWidth: 1, pointRange: 0, shape: "flag", stackDistance: 7, states: { hover: { lineColor: "black", fillColor: "#FCFFC5"} }, style: { fontSize: "11px", fontWeight: "bold", textAlign: "center" }, y: -30
                }); ba.flags = pa(ba.column, { type: "flags", noSharedTooltip: !0, useThreshold: !1, init: M.prototype.init, pointAttrToOptions: { fill: "fillColor", stroke: "color", "stroke-width": "lineWidth", r: "radius" }, translate: function() {
                    ba.column.prototype.translate.apply(this); var a = this.chart, b = this.points, c = b.length - 1, d, e, f, g = (d = this.options.onSeries) &&
a.get(d), h, j, k; if (g) { h = g.points; d = h.length; for (b.sort(function(a, b) { return a.x - b.x }); d-- && b[c]; ) if (e = b[c], j = h[d], j.x <= e.x && (e.plotY = j.plotY, j.x < e.x && (k = h[d + 1]) && (e.plotY += (e.x - j.x) / (k.x - j.x) * (k.plotY - j.plotY)), c--, d++, c < 0)) break } n(b, function(c, d) { if (!g) c.plotY = c.y === G ? a.plotHeight : c.plotY; if ((f = b[d - 1]) && f.plotX === c.plotX) { if (f.stackIndex === G) f.stackIndex = 0; c.stackIndex = f.stackIndex + 1 } })
                }, drawPoints: function() {
                    var a, b = this.points, c = this.chart.renderer, d, e, f = this.options, g = f.y, h = f.shape, j, k, i, l, m = f.lineWidth %
2 / 2, n; for (k = b.length; k--; ) if (i = b[k], d = i.plotX + m, a = i.stackIndex, e = i.plotY + g + m - (a !== G && a * f.stackDistance), isNaN(e) && (e = 0), j = a ? G : i.plotX + m, n = a ? G : i.plotY, l = i.graphic, e !== G) a = i.pointAttr[i.selected ? "select" : ""], l ? l.attr({ x: d, y: e, r: a.r, anchorX: j, anchorY: n }) : l = i.graphic = c.label(i.options.title || f.title || "A", d, e, h, j, n).css(H(f.style, i.style)).attr(a).attr({ align: h === "flag" ? "left" : "center", width: f.width, height: f.height }).add(this.group).shadow(f.shadow), j = l.box, a = j.getBBox(), i.shapeArgs = E(a, { x: d - (h === "flag" ?
0 : j.attr("width") / 2), y: e
})
                }, drawTracker: function() { ba.column.prototype.drawTracker.apply(this); n(this.points, function(a) { W(a.tracker.element, "mouseover", function() { a.graphic.toFront() }) }) }, tooltipFormatter: function(a) { return a.point.text }, animate: function() { } 
                }); gc.flag = function(a, b, c, d, e) { var f = e && e.anchorX || a, e = e && e.anchorY || b; return ["M", f, e, "L", a, b + d, a, b, a + c, b, a + c, b + d, a, b + d, "M", f, e, "Z"] }; n(["circle", "square"], function(a) {
                    gc[a + "pin"] = function(b, c, d, e, f) {
                        var g = f && f.anchorX, f = f && f.anchorY, b = gc[a](b,
c, d, e); g && f && b.push("M", g, c + e, "L", g, f); return b
                    } 
                }); Nb === Rb && n(["flag", "circlepin", "squarepin"], function(a) { Rb.prototype.symbols[a] = gc[a] }); var hc = Ga ? "touchstart" : "mousedown", Lc = Ga ? "touchmove" : "mousemove", Mc = Ga ? "touchend" : "mouseup", B = ja("linearGradient", { x1: 0, y1: 0, x2: 0, y2: 1 }, "stops", [[0, "#FFF"], [1, "#CCC"]]), Ja = [].concat(Kc); Ja[4] = [ka, [1, 2, 3, 4]]; Ja[5] = [Ca, [1, 2, 3]]; E(X, { navigator: { handles: { backgroundColor: "#FFF", borderColor: "#666" }, height: 40, margin: 10, maskFill: "rgba(255, 255, 255, 0.75)", outlineColor: "#444",
                    outlineWidth: 1, series: { type: "areaspline", color: "#4572A7", compare: null, fillOpacity: 0.4, dataGrouping: { approximation: "average", groupPixelWidth: 2, smoothed: !0, units: Ja }, dataLabels: { enabled: !1 }, id: gb + "navigator-series", lineColor: "#4572A7", lineWidth: 1, marker: { enabled: !1 }, pointRange: 0, shadow: !1 }, xAxis: { tickWidth: 0, lineWidth: 0, gridLineWidth: 1, tickPixelInterval: 200, labels: { align: "left", x: 3, y: -4} }, yAxis: { gridLineWidth: 0, startOnTick: !1, endOnTick: !1, minPadding: 0.1, maxPadding: 0.1, labels: { enabled: !1 }, title: { text: null },
                        tickWidth: 0}
                    }, scrollbar: { height: Ga ? 20 : 14, barBackgroundColor: B, barBorderRadius: 2, barBorderWidth: 1, barBorderColor: "#666", buttonArrowColor: "#666", buttonBackgroundColor: B, buttonBorderColor: "#666", buttonBorderRadius: 2, buttonBorderWidth: 1, rifleColor: "#666", trackBackgroundColor: ja("linearGradient", { x1: 0, y1: 0, x2: 0, y2: 1 }, "stops", [[0, "#EEE"], [1, "#FFF"]]), trackBorderColor: "#CCC", trackBorderWidth: 1}
                }); Highcharts.Scroller = function(a) {
                    function b(a, b) {
                        var c = { fill: Q.backgroundColor, stroke: Q.borderColor, "stroke-width": 1 },
d; fa || (ja[b] = j.g().css({ cursor: "e-resize" }).attr({ zIndex: 4 - b }).add(), d = j.rect(-4.5, 0, 9, 16, 3, 1).attr(c).add(ja[b]), oa.push(d), d = j.path(["M", -1.5, 4, "L", -1.5, 12, "M", 0.5, 4, "L", 0.5, 12]).attr(c).add(ja[b]), oa.push(d)); ja[b].translate(X + L + parseInt(a, 10), C + J / 2 - 8)
                    } function c(a) {
                        var b; fa || (ua[a] = j.g().add(o), b = j.rect(-0.5, -0.5, L + 1, L + 1, u.buttonBorderRadius, u.buttonBorderWidth).attr({ stroke: u.buttonBorderColor, "stroke-width": u.buttonBorderWidth, fill: u.buttonBackgroundColor }).add(ua[a]), oa.push(b), b = j.path(["M",
L / 2 + (a ? -1 : 1), L / 2 - 3, "L", L / 2 + (a ? -1 : 1), L / 2 + 3, L / 2 + (a ? 2 : -2), L / 2]).attr({ fill: u.buttonArrowColor }).add(ua[a]), oa.push(b)); a && ua[a].attr({ translateX: ha - L })
                    } function d(d, e, f, g) {
                        if (!isNaN(d)) {
                            var h = u.barBorderWidth; Y = C + R; m = r(s.left, a.plotLeft + L); p = r(s.len, a.plotWidth - 2 * L); X = m - L; ha = p + 2 * L; if (s.getExtremes) { var k = a.xAxis[0].getExtremes(), n = k.dataMin === null, q = s.getExtremes(), t = va(k.dataMin, q.dataMin), k = T(k.dataMax, q.dataMax); !n && (t !== q.min || k !== q.max) && s.setExtremes(t, k, !0, !1) } f = r(f, s.translate(d)); g = r(g, s.translate(e));
                            K = N(va(f, g)); E = N(T(f, g)); D = E - K; if (!fa && (l && (ia = j.rect().attr({ fill: i.maskFill, zIndex: 3 }).add(), la = j.rect().attr({ fill: i.maskFill, zIndex: 3 }).add(), ma = j.path().attr({ "stroke-width": $, stroke: i.outlineColor, zIndex: 3 }).add()), w)) o = j.g().add(), d = u.trackBorderWidth, na = j.rect().attr({ y: -d % 2 / 2, fill: u.trackBackgroundColor, stroke: u.trackBorderColor, "stroke-width": d, r: u.trackBorderRadius || 0, height: L }).add(o), pa = j.rect().attr({ y: -h % 2 / 2, height: L, fill: u.barBackgroundColor, stroke: u.barBorderColor, "stroke-width": h,
                                r: aa
                            }).add(o), qa = j.path().attr({ stroke: u.rifleColor, "stroke-width": 1 }).add(o); l && (ia.attr({ x: m, y: C, width: K, height: J }), la.attr({ x: m + E, y: C, width: p - E, height: J }), ma.attr({ d: [ta, X, Y, ga, m + K + R, Y, m + K + R, Y + S - L, ta, m + E - R, Y + S - L, ga, m + E - R, Y, X + ha, Y] }), b(K + R, 0), b(E + R, 1)); w && (c(0), c(1), o.translate(X, y(Y + J)), na.attr({ width: ha }), pa.attr({ x: y(L + K) + h % 2 / 2, width: D - h }), h = L + K + D / 2 - 0.5, qa.attr({ d: [ta, h - 3, L / 4, ga, h - 3, 2 * L / 3, ta, h, L / 4, ga, h, 2 * L / 3, ta, h + 3, L / 4, ga, h + 3, 2 * L / 3], visibility: D > 12 ? bb : Ta })); fa = !0
                        } 
                    } function e(b) {
                        var b = a.tracker.normalizeMouseEvent(b),
c = b.chartX, d = b.chartY, e = Ga ? 10 : 7; if (d > C && d < C + J + L) (d = !w || d < C + J) && sa.abs(c - K - m) < e ? (v = !0, B = E) : d && sa.abs(c - E - m) < e ? (q = !0, B = K) : c > m + K && c < m + E ? (A = c, V = M.cursor, M.cursor = "ew-resize", I = c - K) : c > X && c < X + ha && (c = d ? c - m - D / 2 : c < m ? K - va(10, D) : c > X + ha - L ? K + va(10, D) : c < m + K ? K - D : E, c < 0 ? c = 0 : c + D > p && (c = p - D), c !== K && a.xAxis[0].setExtremes(s.translate(c, !0), s.translate(c + D, !0), !0, !1)); b.preventDefault && b.preventDefault()
                    } function f(b) {
                        b = a.tracker.normalizeMouseEvent(b); b = b.chartX; b < m ? b = m : b > X + ha - L && (b = X + ha - L); v ? (G = !0, d(0, 0, b - m, B)) : q ? (G =
!0, d(0, 0, B, b - m)) : A && (G = !0, b < I ? b = I : b > p + I - D && (b = p + I - D), d(0, 0, b - I, b - I + D))
                    } function g() { G && a.xAxis[0].setExtremes(s.translate(K, !0), s.translate(E, !0), !0, !1); v = q = A = G = I = null; M.cursor = V } function h() {
                        var b = ba.xAxis, c = b.getExtremes(), e = c.min, f = c.max, g = c.dataMin, c = c.dataMax, h = f - e, j, i, k, l, m; j = t.xData; var n = !!b.setExtremes; i = f >= j[j.length - 1]; j = e <= g; if (!x) t.options.pointStart = ba.xData[0], t.setData(ba.options.data, !1), m = !0; j && (l = g, k = l + h); i && (k = c, j || (l = T(k - h, t.xData[0]))); n && (j || i) ? b.setExtremes(l, k, !0, !1) : (m &&
a.redraw(!1), d(T(e, g), va(f, c)))
                    } var j = a.renderer, k = a.options, i = k.navigator, l = i.enabled, m, p, t, x, u = k.scrollbar, w = u.enabled, v, q, A, B, I, G, s, Aa, K, E, D, M = document.body.style, V, Q = i.handles, J = l ? i.height : 0, $ = i.outlineWidth, L = w ? u.height : 0, S = J + L, aa = u.barBorderRadius, C, R = $ / 2, Y, X, ha, fa, ka = i.baseSeries, ba = a.series[ka] || typeof ka === "string" && a.get(ka) || a.series[0], ia, la, ma, ja = [], o, na, pa, qa, ua = [], oa = []; a.resetZoomEnabled = !1; (function() {
                        var b = a.xAxis.length, c = a.yAxis.length, d = a.setSize; a.extraBottomMargin = S + i.margin;
                        C = i.top || a.chartHeight - J - L - k.chart.spacingBottom; if (l) {
                            var j = ba.options, m = j.data, n = i.series; x = n.data; j.data = n.data = null; s = new a.Axis(H({ ordinal: ba.xAxis.options.ordinal }, i.xAxis, { isX: !0, type: "datetime", index: b, height: J, top: C, offset: 0, offsetLeft: L, offsetRight: -L, startOnTick: !1, endOnTick: !1, minPadding: 0, maxPadding: 0, zoomEnabled: !1 })); Aa = new a.Axis(H(i.yAxis, { alignTicks: !1, height: J, top: C, offset: 0, index: c, zoomEnabled: !1 })); b = H(ba.options, n, { threshold: null, clip: !1, enableMouseTracking: !1, group: "nav", padXAxis: !1,
                                xAxis: b, yAxis: c, name: "Navigator", showInLegend: !1, isInternal: !0, visible: !0
                            }); j.data = m; n.data = x; b.data = x || m; t = a.initSeries(b); W(ba, "updatedData", h)
                        } else s = { translate: function(b, c) { var d = ba.xAxis.getExtremes(), e = a.plotWidth - 2 * L, f = d.dataMin, d = d.dataMax - f; return c ? b * d / e + f : e * (b - f) / d } }; a.setSize = function(b, c, e) { s.options.top = Aa.options.top = C = i.top || c - J - L - k.chart.spacingBottom; d.call(a, b, c, e) }; W(a.container, hc, e); W(a.container, Lc, f); W(document, Mc, g)
                    })(); return { render: d, destroy: function() {
                        ra(a.container, hc,
e); ra(a.container, Lc, f); ra(document, Mc, g); l && ra(ba, "updatedData", h); n([s, Aa, ia, la, ma, na, pa, qa, o], function(a) { a && a.destroy && a.destroy() }); s = Aa = ia = la = ma = na = pa = qa = o = null; n([ua, ja, oa], function(a) { ub(a) })
                    } }
                    }; E(X, { rangeSelector: { buttonTheme: { width: 28, height: 16, padding: 1, r: 0, zIndex: 10}} }); X.lang = H(X.lang, { rangeSelectorZoom: "Zoom", rangeSelectorFrom: "From:", rangeSelectorTo: "To:" }); Highcharts.RangeSelector = function(a) {
                        function b(b, c, d) {
                            var e = a.xAxis[0], f = e && e.getExtremes(), g, h = f && f.dataMin, j = f && f.dataMax, i,
k = e && va(f.max, j), f = new Date(k); g = c.type; var c = c.count, l, m, n = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5 }; if (!(h === null || j === null || b === x)) n[g] ? (l = n[g] * c, i = T(k - l, h)) : g === "month" ? (f.setMonth(f.getMonth() - c), i = T(f.getTime(), h), l = 2592E6 * c) : g === "ytd" ? (f = new Date(0), g = new Date, m = g.getFullYear(), f.setFullYear(m), String(m) !== wb("%Y", f) && f.setFullYear(m - 1), i = m = T(h || 0, f.getTime()), g = g.getTime(), k = va(j || g, g)) : g === "year" ? (f.setFullYear(f.getFullYear() - c), i = T(h, f.getTime()), l = 31536E6 * c) :
g === "all" && e && (i = h, k = j), w[b] && w[b].setState(2), e ? setTimeout(function() { e.setExtremes(i, k, r(d, 1), 0); x = b }, 1) : (h = a.options.xAxis, h[0] = H(h[0], { range: l, min: m }), x = b)
                        } function c() { i && i.blur(); l && l.blur() } function d(a, b) { var c = a.hasFocus ? q.inputEditDateFormat || "%Y-%m-%d" : q.inputDateFormat || "%b %e, %Y"; if (b) a.HCTime = b; a.value = wb(c, a.HCTime) } function e(b) {
                            var c = b === "min", e; m[b] = na("span", { innerHTML: j[c ? "rangeSelectorFrom" : "rangeSelectorTo"] }, q.labelStyle, k); e = na("input", { name: b, className: gb + "range-selector",
                                type: "text"
                            }, E({ width: "80px", height: "16px", border: "1px solid silver", marginLeft: "5px", marginRight: c ? "5px" : "0", textAlign: "center" }, q.inputStyle), k); e.onfocus = e.onblur = function(a) { a = a || window.event; e.hasFocus = a.type === "focus"; d(e) }; e.onchange = function() { var b = e.value, d = Date.parse(b), f = a.xAxis[0].getExtremes(); isNaN(d) && (d = b.split("-"), d = Date.UTC(N(d[0]), N(d[1]) - 1, N(d[2]))); if (!isNaN(d) && (c && d >= f.dataMin && d <= l.HCTime || !c && d <= f.dataMax && d >= i.HCTime)) a.xAxis[0].setExtremes(c ? d : f.min, c ? f.max : d) }; return e
                        }
                        var f = a.renderer, g, h = a.container, j = X.lang, k, i, l, m = {}, p, t, x, u, w = [], v, q, y = [{ type: "month", count: 1, text: "1m" }, { type: "month", count: 3, text: "3m" }, { type: "month", count: 6, text: "6m" }, { type: "ytd", text: "YTD" }, { type: "year", count: 1, text: "1y" }, { type: "all", text: "All"}]; a.resetZoomEnabled = !1; (function() { a.extraTopMargin = 25; q = a.options.rangeSelector; v = q.buttons || y; var d = q.selected; W(h, hc, c); d !== G && v[d] && b(d, v[d], !1); W(a, "load", function() { W(a.xAxis[0], "afterSetExtremes", function() { w[x] && w[x].setState(0); x = null }) }) })();
                        return { render: function(c, m) {
                            var r = a.options.chart.style, s = q.buttonTheme, y = q.inputEnabled !== !1, A = s && s.states, B = a.plotLeft, D; g || (u = f.text(j.rangeSelectorZoom, B, a.plotTop - 10).css(q.labelStyle).add(), D = B + u.getBBox().width + 5, n(v, function(c, d) { w[d] = f.button(c.text, D, a.plotTop - 25, function() { b(d, c); this.isActive = !0 }, s, A && A.hover, A && A.select).css({ textAlign: "center" }).add(); D += w[d].width + (q.buttonSpacing || 0); x === d && w[d].setState(2) }), y && (t = k = na("div", null, { position: "relative", height: 0, fontFamily: r.fontFamily,
                                fontSize: r.fontSize, zIndex: 1
                            }), h.parentNode.insertBefore(k, h), p = k = na("div", null, E({ position: "absolute", top: a.plotTop - 25 + "px", right: a.chartWidth - a.plotLeft - a.plotWidth + "px" }, q.inputBoxStyle), k), i = e("min"), l = e("max"))); y && (d(i, c), d(l, m)); g = !0
                        }, destroy: function() { ra(h, hc, c); n([w], function(a) { ub(a) }); u && (u = u.destroy()); if (i) i.onfocus = i.onblur = i.onchange = null; if (l) l.onfocus = l.onblur = l.onchange = null; n([i, l, m.min, m.max, p, t], function(a) { Hb(a) }); i = l = m = k = p = t = null } }
                        }; $b.prototype.callbacks.push(function(a) {
                            function b() {
                                f =
a.xAxis[0].getExtremes(); g.render(T(f.min, f.dataMin), va(f.max, f.dataMax))
                            } function c() { f = a.xAxis[0].getExtremes(); h.render(f.min, f.max) } function d(a) { g.render(a.min, a.max) } function e(a) { h.render(a.min, a.max) } var f, g = a.scroller, h = a.rangeSelector; g && (W(a.xAxis[0], "afterSetExtremes", d), W(a, "resize", b), b()); h && (W(a.xAxis[0], "afterSetExtremes", e), W(a, "resize", c), c()); W(a, "destroy", function() {
                                g && (ra(a, "resize", b), ra(a.xAxis[0], "afterSetExtremes", d)); h && (ra(a, "resize", c), ra(a.xAxis[0], "afterSetExtremes",
e))
                            })
                        }); Highcharts.StockChart = function(a, b) {
                            var c = a.series, d, e = { marker: { enabled: !1, states: { hover: { enabled: !0, radius: 5}} }, shadow: !1, states: { hover: { lineWidth: 2} }, dataGrouping: { enabled: !0} }; a.xAxis = nb(sb(a.xAxis || {}), function(a) { return H({ minPadding: 0, maxPadding: 0, ordinal: !0, title: { text: null }, showLastLabel: !0 }, a, { type: "datetime", categories: null }) }); a.yAxis = nb(sb(a.yAxis || {}), function(a) { d = a.opposite; return H({ labels: { align: d ? "right" : "left", x: d ? -2 : 2, y: -2 }, showLastLabel: !1, title: { text: null} }, a) }); a.series =
null; a = H({ chart: { panning: !0 }, navigator: { enabled: !0 }, scrollbar: { enabled: !0 }, rangeSelector: { enabled: !0 }, title: { text: null }, tooltip: { shared: !0, crosshairs: !0 }, legend: { enabled: !1 }, plotOptions: { line: e, spline: e, area: e, areaspline: e, column: { shadow: !1, borderWidth: 0, dataGrouping: { enabled: !0}}} }, a, { chart: { inverted: !1} }); a.series = c; return new $b(a, b)
                        }; var Yc = Q.init, Zc = Q.processData, $c = jb.prototype.tooltipFormatter; Q.init = function() {
                            Yc.apply(this, arguments); var a = this.options.compare; if (a) this.modifyValue = function(b,
c) { var d = this.compareValue, b = a === "value" ? b - d : b = 100 * (b / d) - 100; if (c) c.change = b; return b } 
                        }; Q.processData = function() { Zc.apply(this, arguments); if (this.options.compare) for (var a = 0, b = this.processedXData, c = this.processedYData, d = c.length, e = this.xAxis.getExtremes().min; a < d; a++) if (typeof c[a] === "number" && b[a] >= e) { this.compareValue = c[a]; break } }; jb.prototype.tooltipFormatter = function(a) {
                            a = a.replace("{point.change}", (this.change > 0 ? "+" : "") + Ub(this.change, this.series.tooltipOptions.changeDecimals || 2)); return $c.apply(this,
[a])
                        }; (function() {
                            var a = Q.init, b = Q.getSegments; Q.init = function() {
                                var b, d; a.apply(this, arguments); b = this.chart; (d = this.xAxis) && d.options.ordinal && W(this, "updatedData", function() { delete d.ordinalIndex }); if (d && d.options.ordinal && !d.hasOrdinalExtension) {
                                    d.hasOrdinalExtension = !0; d.beforeSetTickPositions = function() {
                                        var a, b = [], c = !1, e, k = this.getExtremes(), i = k.min, k = k.max, l; if (this.options.ordinal) {
                                            n(this.series, function(c, d) {
                                                if (c.visible !== !1 && (b = b.concat(c.processedXData), a = b.length, d && a)) {
                                                    b.sort(function(a,
b) { return a - b }); for (d = a - 1; d--; ) b[d] === b[d + 1] && b.splice(d, 1)
                                                } 
                                            }); a = b.length; if (a > 2) { e = b[1] - b[0]; for (l = a - 1; l-- && !c; ) b[l + 1] - b[l] !== e && (c = !0) } c ? (this.ordinalPositions = b, c = d.val2lin(i, !0), e = d.val2lin(k, !0), this.ordinalSlope = k = (k - i) / (e - c), this.ordinalOffset = i - c * k) : this.ordinalPositions = this.ordinalSlope = this.ordinalOffset = G
                                        } 
                                    }; d.val2lin = function(a, b) {
                                        var c = this.ordinalPositions; if (c) {
                                            var d = c.length, e, i; for (e = d; e--; ) if (c[e] === a) { i = e; break } for (e = d - 1; e--; ) if (a > c[e] || e === 0) { c = (a - c[e]) / (c[e + 1] - c[e]); i = e + c; break } return b ?
i : this.ordinalSlope * (i || 0) + this.ordinalOffset
                                        } else return a
                                    }; d.lin2val = function(a, b) { var c = this.ordinalPositions; if (c) { var d = this.ordinalSlope, e = this.ordinalOffset, i = c.length - 1, l, m; if (b) a < 0 ? a = c[0] : a > i ? a = c[i] : (i = Na(a), m = a - i); else for (; i--; ) if (l = d * i + e, a >= l) { d = d * (i + 1) + e; m = (a - l) / (d - l); break } return m !== G && c[i] !== G ? c[i] + (m ? m * (c[i + 1] - c[i]) : 0) : a } else return a }; d.getExtendedPositions = function() {
                                        var a = d.series[0].currentDataGrouping, e = d.ordinalIndex, h = a ? a.count + a.unitName : "raw", j = d.getExtremes(), k, i; if (!e) e =
d.ordinalIndex = {}; if (!e[h]) k = { series: [], getExtremes: function() { return { min: j.dataMin, max: j.dataMax} }, options: { ordinal: !0} }, n(d.series, function(d) { i = { xAxis: k, xData: d.xData, chart: b }; i.options = { dataGrouping: a ? { enabled: !0, forced: !0, approximation: "open", units: [[a.unitName, [a.count]]]} : { enabled: !1} }; d.processData.apply(i); k.series.push(i) }), d.beforeSetTickPositions.apply(k), e[h] = k.ordinalPositions; return e[h]
                                    }; d.getGroupIntervalFactor = function(a, b, c) {
                                        for (var d = 0, e = c.length, i = []; d < e - 1; d++) i[d] = c[d + 1] - c[d];
                                        i.sort(function(a, b) { return a - b }); c = i[Na(e / 2)]; return e * c / (b - a)
                                    }; d.postProcessTickInterval = function(a) { var b = this.ordinalSlope; return b ? a / (b / d.closestPointRange) : a }; d.getNonLinearTimeTicks = function(a, b, c, d, e, i, l) {
                                        var m = 0, n = 0, r, x = {}, u, w, v, q = []; if (!e || b === G) return Vb(a, b, c, d); for (w = e.length; n < w; n++) { v = n && e[n - 1] > c; if (e[n] < b) m = n; else if (n === w - 1 || e[n + 1] - e[n] > i * 5 || v) r = Vb(a, e[m], e[n], d), q = q.concat(r), m = n + 1; if (v) break } a = r.info; if (l && a.unitRange <= D[za]) {
                                            n = q.length - 1; for (m = 1; m < n; m++) (new Date(q[m]))[eb]() !==
(new Date(q[m - 1]))[eb]() && (x[q[m]] = ka, u = !0); u && (x[q[0]] = ka); a.higherRanks = x
                                        } q.info = a; return q
                                    }; W(d, "afterSetTickPositions", function(a) { var b = d.options.tickPixelInterval, a = a.tickPositions; if (d.ordinalPositions && A(b)) for (var c = a.length, e, k, i, l = (e = a.info) ? e.higherRanks : []; c--; ) k = d.translate(a[c]), i && i - k < b * 0.6 ? (l[a[c]] && !l[a[c + 1]] ? (e = c + 1, i = k) : e = c, a.splice(e, 1)) : i = k }); var e = b.pan; b.pan = function(a) {
                                        var d = b.xAxis[0], h = !1; if (d.options.ordinal) {
                                            var j = b.mouseDownX, k = d.getExtremes(), i = k.dataMax, l = k.min, m = k.max,
p; p = b.hoverPoints; var r = d.closestPointRange, j = (j - a) / (d.translationSlope * (d.ordinalSlope || r)), x = { ordinalPositions: d.getExtendedPositions() }, u, r = d.lin2val, w = d.val2lin; if (x.ordinalPositions) { if (Ba(j) > 1) p && n(p, function(a) { a.setState() }), j < 0 ? (p = x, x = d.ordinalPositions ? d : x) : p = d.ordinalPositions ? d : x, u = x.ordinalPositions, i > u[u.length - 1] && u.push(i), p = r.apply(p, [w.apply(p, [l, !0]) + j, !0]), j = r.apply(x, [w.apply(x, [m, !0]) + j, !0]), p > va(k.dataMin, l) && j < T(i, m) && d.setExtremes(p, j, !0, !1), b.mouseDownX = a, J(b.container, { cursor: "move" }) } else h =
!0
                                        } else h = !0; h && e.apply(b, arguments)
                                    } 
                                } 
                            }; Q.getSegments = function() { var a = this, d, e = a.options.gapSize; b.apply(a); if (a.xAxis.options.ordinal && e) d = a.segments, n(d, function(b, g) { for (var h = b.length - 1; h--; ) b[h + 1].x - b[h].x > a.xAxis.closestPointRange * e && d.splice(g + 1, 0, b.splice(h + 1, b.length - h)) }) } 
                        })(); E(Highcharts, { Chart: $b, dateFormat: wb, pathAnim: Qb, getOptions: function() { return X }, hasRtlBug: Tc, numberFormat: Ub, Point: jb, Color: cb, Renderer: Nb, seriesTypes: ba, setOptions: function(a) {
                            cc = H(cc, a.xAxis); nc = H(nc, a.yAxis);
                            a.xAxis = a.yAxis = G; X = H(X, a); zc(); return X
                        }, Series: M, addEvent: W, removeEvent: ra, createElement: na, discardElement: Hb, css: J, each: n, extend: E, map: nb, merge: H, pick: r, splat: sb, extendClass: pa, product: "Highstock", version: "1.1.4"
                        })
                    })();