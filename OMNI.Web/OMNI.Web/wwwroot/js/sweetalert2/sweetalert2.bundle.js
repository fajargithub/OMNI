! function (t) {
    var e, n = t.Promise,
        o = n && "resolve" in n && "reject" in n && "all" in n && "race" in n && (new n(function (t) {
            e = t
        }), "function" == typeof e);
    "undefined" != typeof exports && exports ? (exports.Promise = o ? n : x, exports.Polyfill = x) : "function" == typeof define && define.amd ? define(function () {
        return o ? n : x
    }) : o || (t.Promise = x);

    function i() { }
    var r = "pending",
        a = "sealed",
        s = "fulfilled",
        u = "rejected";

    function c(t) {
        return "[object Array]" === Object.prototype.toString.call(t)
    }
    var l, d = "undefined" != typeof setImmediate ? setImmediate : setTimeout,
        f = [];

    function p() {
        for (var t = 0; t < f.length; t++) f[t][0](f[t][1]);
        l = !(f = [])
    }

    function m(t, e) {
        f.push([t, e]), l || (l = !0, d(p, 0))
    }

    function h(t) {
        var e = t.owner,
            n = e.state_,
            o = e.data_,
            i = t[n],
            r = t.then;
        if ("function" == typeof i) {
            n = s;
            try {
                o = i(o)
            } catch (t) {
                b(r, t)
            }
        }
        g(r, o) || (n === s && v(r, o), n === u && b(r, o))
    }

    function g(e, n) {
        var o;
        try {
            if (e === n) throw new TypeError("A promises callback cannot return that same promise.");
            if (n && ("function" == typeof n || "object" == typeof n)) {
                var t = n.then;
                if ("function" == typeof t) return t.call(n, function (t) {
                    o || (o = !0, n !== t ? v(e, t) : y(e, t))
                }, function (t) {
                    o || (o = !0, b(e, t))
                }), !0
            }
        } catch (t) {
            return o || b(e, t), !0
        }
        return !1
    }

    function v(t, e) {
        t !== e && g(t, e) || y(t, e)
    }

    function y(t, e) {
        t.state_ === r && (t.state_ = a, t.data_ = e, m(C, t))
    }

    function b(t, e) {
        t.state_ === r && (t.state_ = a, t.data_ = e, m(k, t))
    }

    function w(t) {
        var e = t.then_;
        t.then_ = void 0;
        for (var n = 0; n < e.length; n++) h(e[n])
    }

    function C(t) {
        t.state_ = s, w(t)
    }

    function k(t) {
        t.state_ = u, w(t)
    }

    function x(t) {
        if ("function" != typeof t) throw new TypeError("Promise constructor takes a function argument");
        if (this instanceof x == !1) throw new TypeError("Failed to construct 'Promise': Please use the 'new' operator, this object constructor cannot be called as a function.");
        this.then_ = [],
            function (t, e) {
                function n(t) {
                    b(e, t)
                }
                try {
                    t(function (t) {
                        v(e, t)
                    }, n)
                } catch (t) {
                    n(t)
                }
            }(t, this)
    }
    x.prototype = {
        constructor: x,
        state_: r,
        then_: null,
        data_: void 0,
        then: function (t, e) {
            var n = {
                owner: this,
                then: new this.constructor(i),
                fulfilled: t,
                rejected: e
            };
            return this.state_ === s || this.state_ === u ? m(h, n) : this.then_.push(n), n.then
        },
        catch: function (t) {
            return this.then(null, t)
        }
    }, x.all = function (s) {
        if (!c(s)) throw new TypeError("You must pass an array to Promise.all().");
        return new this(function (n, t) {
            var o = [],
                i = 0;

            function e(e) {
                return i++ ,
                    function (t) {
                        o[e] = t, --i || n(o)
                    }
            }
            for (var r, a = 0; a < s.length; a++)(r = s[a]) && "function" == typeof r.then ? r.then(e(a), t) : o[a] = r;
            i || n(o)
        })
    }, x.race = function (i) {
        if (!c(i)) throw new TypeError("You must pass an array to Promise.race().");
        return new this(function (t, e) {
            for (var n, o = 0; o < i.length; o++)(n = i[o]) && "function" == typeof n.then ? n.then(t, e) : t(n)
        })
    }, x.resolve = function (e) {
        return e && "object" == typeof e && e.constructor === this ? e : new this(function (t) {
            t(e)
        })
    }, x.reject = function (n) {
        return new this(function (t, e) {
            e(n)
        })
    }
}("undefined" != typeof window ? window : "undefined" != typeof global ? global : "undefined" != typeof self ? self : this),
    function (t, e) {
        "object" == typeof exports && "undefined" != typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define(e) : t.Sweetalert2 = e()
    }(this, function () {
        "use strict";

        function r(t) {
            return (r = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (t) {
                return typeof t
            } : function (t) {
                return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t
            })(t)
        }

        function o(t, e) {
            if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function")
        }

        function i(t, e) {
            for (var n = 0; n < e.length; n++) {
                var o = e[n];
                o.enumerable = o.enumerable || !1, o.configurable = !0, "value" in o && (o.writable = !0), Object.defineProperty(t, o.key, o)
            }
        }

        function a(t, e, n) {
            return e && i(t.prototype, e), n && i(t, n), t
        }

        function s() {
            return (s = Object.assign || function (t) {
                for (var e = 1; e < arguments.length; e++) {
                    var n = arguments[e];
                    for (var o in n) Object.prototype.hasOwnProperty.call(n, o) && (t[o] = n[o])
                }
                return t
            }).apply(this, arguments)
        }

        function u(t) {
            return (u = Object.setPrototypeOf ? Object.getPrototypeOf : function (t) {
                return t.__proto__ || Object.getPrototypeOf(t)
            })(t)
        }

        function c(t, e) {
            return (c = Object.setPrototypeOf || function (t, e) {
                return t.__proto__ = e, t
            })(t, e)
        }

        function l(t, e, n) {
            return (l = function () {
                if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
                if (Reflect.construct.sham) return !1;
                if ("function" == typeof Proxy) return !0;
                try {
                    return Date.prototype.toString.call(Reflect.construct(Date, [], function () { })), !0
                } catch (t) {
                    return !1
                }
            }() ? Reflect.construct : function (t, e, n) {
                var o = [null];
                o.push.apply(o, e);
                var i = new (Function.bind.apply(t, o));
                return n && c(i, n.prototype), i
            }).apply(null, arguments)
        }

        function d(t, e) {
            return !e || "object" != typeof e && "function" != typeof e ? function (t) {
                if (void 0 === t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                return t
            }(t) : e
        }

        function f(t, e, n) {
            return (f = "undefined" != typeof Reflect && Reflect.get ? Reflect.get : function (t, e, n) {
                var o = function (t, e) {
                    for (; !Object.prototype.hasOwnProperty.call(t, e) && null !== (t = u(t)););
                    return t
                }(t, e);
                if (o) {
                    var i = Object.getOwnPropertyDescriptor(o, e);
                    return i.get ? i.get.call(n) : i.value
                }
            })(t, e, n || t)
        }

        function p(e) {
            return Object.keys(e).map(function (t) {
                return e[t]
            })
        }

        function m(t) {
            return Array.prototype.slice.call(t)
        }

        function h(t) {
            console.error("".concat(e, " ").concat(t))
        }

        function g(t, e) {
            ! function (t) {
                -1 === n.indexOf(t) && (n.push(t), w(t))
            }('"'.concat(t, '" is deprecated and will be removed in the next major release. Please use "').concat(e, '" instead.'))
        }

        function v(t) {
            return t && Promise.resolve(t) === t
        }

        function t(t) {
            var e = {};
            for (var n in t) e[t[n]] = "swal2-" + t[n];
            return e
        }

        function y(t, e) {
            return t.classList.contains(e)
        }

        function b(e, t, n) {
            m(e.classList).forEach(function (t) {
                -1 === p(x).indexOf(t) && -1 === p(P).indexOf(t) && e.classList.remove(t)
            }), t && t[n] && rt(e, t[n])
        }
        var e = "SweetAlert2:",
            w = function (t) {
                console.warn("".concat(e, " ").concat(t))
            },
            n = [],
            C = function (t) {
                return "function" == typeof t ? t() : t
            },
            k = Object.freeze({
                cancel: "cancel",
                backdrop: "backdrop",
                close: "close",
                esc: "esc",
                timer: "timer"
            }),
            x = t(["container", "shown", "height-auto", "iosfix", "popup", "modal", "no-backdrop", "toast", "toast-shown", "toast-column", "fade", "show", "hide", "noanimation", "close", "title", "header", "content", "actions", "confirm", "cancel", "footer", "icon", "image", "input", "file", "range", "select", "radio", "checkbox", "label", "textarea", "inputerror", "validation-message", "progress-steps", "active-progress-step", "progress-step", "progress-step-line", "loading", "styled", "top", "top-start", "top-end", "top-left", "top-right", "center", "center-start", "center-end", "center-left", "center-right", "bottom", "bottom-start", "bottom-end", "bottom-left", "bottom-right", "grow-row", "grow-column", "grow-fullscreen", "rtl"]),
            P = t(["success", "warning", "info", "question", "error"]),
            S = {
                previousBodyPadding: null
            };

        function B(t, e) {
            if (!e) return null;
            switch (e) {
                case "select":
                case "textarea":
                case "file":
                    return st(t, x[e]);
                case "checkbox":
                    return t.querySelector(".".concat(x.checkbox, " input"));
                case "radio":
                    return t.querySelector(".".concat(x.radio, " input:checked")) || t.querySelector(".".concat(x.radio, " input:first-child"));
                case "range":
                    return t.querySelector(".".concat(x.range, " input"));
                default:
                    return st(t, x.input)
            }
        }

        function A(t) {
            if (t.focus(), "file" !== t.type) {
                var e = t.value;
                t.value = "", t.value = e
            }
        }

        function E(t, e, n) {
            t && e && ("string" == typeof e && (e = e.split(/\s+/).filter(Boolean)), e.forEach(function (e) {
                t.forEach ? t.forEach(function (t) {
                    n ? t.classList.add(e) : t.classList.remove(e)
                }) : n ? t.classList.add(e) : t.classList.remove(e)
            }))
        }

        function T(t, e, n) {
            n || 0 === parseInt(n) ? t.style[e] = "number" == typeof n ? n + "px" : n : t.style.removeProperty(e)
        }

        function O(t, e) {
            var n = 1 < arguments.length && void 0 !== e ? e : "flex";
            t.style.opacity = "", t.style.display = n
        }

        function L(t) {
            t.style.opacity = "", t.style.display = "none"
        }

        function M(t, e, n) {
            e ? O(t, n) : L(t)
        }

        function j(t) {
            return !(!t || !(t.offsetWidth || t.offsetHeight || t.getClientRects().length))
        }

        function V(t) {
            var e = window.getComputedStyle(t),
                n = parseFloat(e.getPropertyValue("animation-duration") || "0"),
                o = parseFloat(e.getPropertyValue("transition-duration") || "0");
            return 0 < n || 0 < o
        }

        function H() {
            return document.body.querySelector("." + x.container)
        }

        function I(t) {
            var e = H();
            return e ? e.querySelector(t) : null
        }

        function _(t) {
            return I("." + t)
        }

        function q() {
            return _(x.popup)
        }

        function R() {
            var t = q();
            return m(t.querySelectorAll("." + x.icon))
        }

        function D() {
            var t = R().filter(function (t) {
                return j(t)
            });
            return t.length ? t[0] : null
        }

        function N() {
            return _(x.title)
        }

        function U() {
            return _(x.content)
        }

        function F() {
            return _(x.image)
        }

        function z() {
            return _(x["progress-steps"])
        }

        function W() {
            return _(x["validation-message"])
        }

        function K() {
            return I("." + x.actions + " ." + x.confirm)
        }

        function Y() {
            return I("." + x.actions + " ." + x.cancel)
        }

        function Z() {
            return _(x.actions)
        }

        function Q() {
            return _(x.header)
        }

        function $() {
            return _(x.footer)
        }

        function J() {
            return _(x.close)
        }

        function X() {
            var t = m(q().querySelectorAll('[tabindex]:not([tabindex="-1"]):not([tabindex="0"])')).sort(function (t, e) {
                return t = parseInt(t.getAttribute("tabindex")), (e = parseInt(e.getAttribute("tabindex"))) < t ? 1 : t < e ? -1 : 0
            }),
                e = m(q().querySelectorAll('\n  a[href],\n  area[href],\n  input:not([disabled]),\n  select:not([disabled]),\n  textarea:not([disabled]),\n  button:not([disabled]),\n  iframe,\n  object,\n  embed,\n  [tabindex="0"],\n  [contenteditable],\n  audio[controls],\n  video[controls],\n  summary\n')).filter(function (t) {
                    return "-1" !== t.getAttribute("tabindex")
                });
            return function (t) {
                for (var e = [], n = 0; n < t.length; n++) - 1 === e.indexOf(t[n]) && e.push(t[n]);
                return e
            }(t.concat(e)).filter(function (t) {
                return j(t)
            })
        }

        function G() {
            return !ut() && !document.body.classList.contains(x["no-backdrop"])
        }

        function tt() {
            return "undefined" == typeof window || "undefined" == typeof document
        }

        function et(t) {
            Ue.isVisible() && it !== t.target.value && Ue.resetValidationMessage(), it = t.target.value
        }

        function nt(t, e) {
            t instanceof HTMLElement ? e.appendChild(t) : "object" === r(t) ? dt(e, t) : t && (e.innerHTML = t)
        }

        function ot(t, e) {
            var n = Z(),
                o = K(),
                i = Y();
            e.showConfirmButton || e.showCancelButton || L(n), b(n, e.customClass, "actions"), pt(o, "confirm", e), pt(i, "cancel", e), e.buttonsStyling ? function (t, e, n) {
                rt([t, e], x.styled), n.confirmButtonColor && (t.style.backgroundColor = n.confirmButtonColor);
                n.cancelButtonColor && (e.style.backgroundColor = n.cancelButtonColor);
                var o = window.getComputedStyle(t).getPropertyValue("background-color");
                t.style.borderLeftColor = o, t.style.borderRightColor = o
            }(o, i, e) : (at([o, i], x.styled), o.style.backgroundColor = o.style.borderLeftColor = o.style.borderRightColor = "", i.style.backgroundColor = i.style.borderLeftColor = i.style.borderRightColor = ""), e.reverseButtons && o.parentNode.insertBefore(i, o)
        }
        var it, rt = function (t, e) {
            E(t, e, !0)
        },
            at = function (t, e) {
                E(t, e, !1)
            },
            st = function (t, e) {
                for (var n = 0; n < t.childNodes.length; n++)
                    if (y(t.childNodes[n], e)) return t.childNodes[n]
            },
            ut = function () {
                return document.body.classList.contains(x["toast-shown"])
            },
            ct = '\n <div aria-labelledby="'.concat(x.title, '" aria-describedby="').concat(x.content, '" class="').concat(x.popup, '" tabindex="-1">\n   <div class="').concat(x.header, '">\n     <ul class="').concat(x["progress-steps"], '"></ul>\n     <div class="').concat(x.icon, " ").concat(P.error, '">\n       <span class="swal2-x-mark"><span class="swal2-x-mark-line-left"></span><span class="swal2-x-mark-line-right"></span></span>\n     </div>\n     <div class="').concat(x.icon, " ").concat(P.question, '"></div>\n     <div class="').concat(x.icon, " ").concat(P.warning, '"></div>\n     <div class="').concat(x.icon, " ").concat(P.info, '"></div>\n     <div class="').concat(x.icon, " ").concat(P.success, '">\n       <div class="swal2-success-circular-line-left"></div>\n       <span class="swal2-success-line-tip"></span> <span class="swal2-success-line-long"></span>\n       <div class="swal2-success-ring"></div> <div class="swal2-success-fix"></div>\n       <div class="swal2-success-circular-line-right"></div>\n     </div>\n     <img class="').concat(x.image, '" />\n     <h2 class="').concat(x.title, '" id="').concat(x.title, '"></h2>\n     <button type="button" class="').concat(x.close, '"></button>\n   </div>\n   <div class="').concat(x.content, '">\n     <div id="').concat(x.content, '"></div>\n     <input class="').concat(x.input, '" />\n     <input type="file" class="').concat(x.file, '" />\n     <div class="').concat(x.range, '">\n       <input type="range" />\n       <output></output>\n     </div>\n     <select class="').concat(x.select, '"></select>\n     <div class="').concat(x.radio, '"></div>\n     <label for="').concat(x.checkbox, '" class="').concat(x.checkbox, '">\n       <input type="checkbox" />\n       <span class="').concat(x.label, '"></span>\n     </label>\n     <textarea class="').concat(x.textarea, '"></textarea>\n     <div class="').concat(x["validation-message"], '" id="').concat(x["validation-message"], '"></div>\n   </div>\n   <div class="').concat(x.actions, '">\n     <button type="button" class="').concat(x.confirm, '">OK</button>\n     <button type="button" class="').concat(x.cancel, '">Cancel</button>\n   </div>\n   <div class="').concat(x.footer, '">\n   </div>\n </div>\n').replace(/(^|\n)\s*/g, ""),
            lt = function (t) {
                if (function () {
                    var t = H();
                    t && (t.parentNode.removeChild(t), at([document.documentElement, document.body], [x["no-backdrop"], x["toast-shown"], x["has-column"]]))
                }(), tt()) h("SweetAlert2 requires document to initialize");
                else {
                    var e = document.createElement("div");
                    e.className = x.container, e.innerHTML = ct;
                    var n = function (t) {
                        return "string" == typeof t ? document.querySelector(t) : t
                    }(t.target);
                    n.appendChild(e),
                        function (t) {
                            var e = q();
                            e.setAttribute("role", t.toast ? "alert" : "dialog"), e.setAttribute("aria-live", t.toast ? "polite" : "assertive"), t.toast || e.setAttribute("aria-modal", "true")
                        }(t),
                        function (t) {
                            "rtl" === window.getComputedStyle(t).direction && rt(H(), x.rtl)
                        }(n),
                        function () {
                            var t = U(),
                                e = st(t, x.input),
                                n = st(t, x.file),
                                o = t.querySelector(".".concat(x.range, " input")),
                                i = t.querySelector(".".concat(x.range, " output")),
                                r = st(t, x.select),
                                a = t.querySelector(".".concat(x.checkbox, " input")),
                                s = st(t, x.textarea);
                            e.oninput = et, n.onchange = et, r.onchange = et, a.onchange = et, s.oninput = et, o.oninput = function (t) {
                                et(t), i.value = o.value
                            }, o.onchange = function (t) {
                                et(t), o.nextSibling.value = o.value
                            }
                        }()
                }
            },
            dt = function (t, e) {
                if (t.innerHTML = "", 0 in e)
                    for (var n = 0; n in e; n++) t.appendChild(e[n].cloneNode(!0));
                else t.appendChild(e.cloneNode(!0))
            },
            ft = function () {
                if (tt()) return !1;
                var t = document.createElement("div"),
                    e = {
                        WebkitAnimation: "webkitAnimationEnd",
                        OAnimation: "oAnimationEnd oanimationend",
                        animation: "animationend"
                    };
                for (var n in e)
                    if (Object.prototype.hasOwnProperty.call(e, n) && void 0 !== t.style[n]) return e[n];
                return !1
            }();

        function pt(t, e, n) {
            M(t, n["showC" + e.substring(1) + "Button"], "inline-block"), t.innerHTML = n[e + "ButtonText"], t.setAttribute("aria-label", n[e + "ButtonAriaLabel"]), t.className = x[e], b(t, n.customClass, e + "Button"), rt(t, n[e + "ButtonClass"])
        }

        function mt(t, e) {
            var n = H();
            n && (function (t, e) {
                "string" == typeof e ? t.style.background = e : e || rt([document.documentElement, document.body], x["no-backdrop"])
            }(n, e.backdrop), !e.backdrop && e.allowOutsideClick && w('"allowOutsideClick" parameter requires `backdrop` parameter to be set to `true`'), function (t, e) {
                e in x ? rt(t, x[e]) : (w('The "position" parameter is not valid, defaulting to "center"'), rt(t, x.center))
            }(n, e.position), function (t, e) {
                if (e && "string" == typeof e) {
                    var n = "grow-" + e;
                    n in x && rt(t, x[n])
                }
            }(n, e.grow), b(n, e.customClass, "container"), e.customContainerClass && rt(n, e.customContainerClass))
        }

        function ht(t, e) {
            t.placeholder && !e.inputPlaceholder || (t.placeholder = e.inputPlaceholder)
        }
        var gt = {
            promise: new WeakMap,
            innerParams: new WeakMap,
            domCache: new WeakMap
        },
            vt = ["input", "file", "range", "select", "radio", "checkbox", "textarea"],
            yt = function (t) {
                if (!Ct[t.input]) return h('Unexpected type of input! Expected "text", "email", "password", "number", "tel", "select", "radio", "checkbox", "textarea", "file" or "url", got "'.concat(t.input, '"'));
                var e = Ct[t.input](t);
                O(e), setTimeout(function () {
                    A(e)
                })
            },
            bt = function (t, e) {
                var n = B(U(), t);
                if (n)
                    for (var o in function (t) {
                        for (var e = 0; e < t.attributes.length; e++) {
                            var n = t.attributes[e].name; - 1 === ["type", "value", "style"].indexOf(n) && t.removeAttribute(n)
                        }
                    }(n), e) "range" === t && "placeholder" === o || n.setAttribute(o, e[o])
            },
            wt = function (t, e, n) {
                t.className = e, n.inputClass && rt(t, n.inputClass), n.customClass && rt(t, n.customClass.input)
            },
            Ct = {};
        Ct.text = Ct.email = Ct.password = Ct.number = Ct.tel = Ct.url = function (t) {
            var e = st(U(), x.input);
            return "string" == typeof t.inputValue || "number" == typeof t.inputValue ? e.value = t.inputValue : v(t.inputValue) || w('Unexpected type of inputValue! Expected "string", "number" or "Promise", got "'.concat(r(t.inputValue), '"')), ht(e, t), e.type = t.input, e
        }, Ct.file = function (t) {
            var e = st(U(), x.file);
            return ht(e, t), e.type = t.input, e
        }, Ct.range = function (t) {
            var e = st(U(), x.range),
                n = e.querySelector("input"),
                o = e.querySelector("output");
            return n.value = t.inputValue, n.type = t.input, o.value = t.inputValue, e
        }, Ct.select = function (t) {
            var e = st(U(), x.select);
            if (e.innerHTML = "", t.inputPlaceholder) {
                var n = document.createElement("option");
                n.innerHTML = t.inputPlaceholder, n.value = "", n.disabled = !0, n.selected = !0, e.appendChild(n)
            }
            return e
        }, Ct.radio = function () {
            var t = st(U(), x.radio);
            return t.innerHTML = "", t
        }, Ct.checkbox = function (t) {
            var e = st(U(), x.checkbox),
                n = B(U(), "checkbox");
            return n.type = "checkbox", n.value = 1, n.id = x.checkbox, n.checked = Boolean(t.inputValue), e.querySelector("span").innerHTML = t.inputPlaceholder, e
        }, Ct.textarea = function (t) {
            var e = st(U(), x.textarea);
            if (e.value = t.inputValue, ht(e, t), "MutationObserver" in window) {
                var n = parseInt(window.getComputedStyle(q()).width),
                    o = parseInt(window.getComputedStyle(q()).paddingLeft) + parseInt(window.getComputedStyle(q()).paddingRight);
                new MutationObserver(function () {
                    var t = e.offsetWidth + o;
                    q().style.width = n < t ? t + "px" : null
                }).observe(e, {
                    attributes: !0,
                    attributeFilter: ["style"]
                })
            }
            return e
        };

        function kt(t, e) {
            var n = U().querySelector("#" + x.content);
            e.html ? (nt(e.html, n), O(n, "block")) : e.text ? (n.textContent = e.text, O(n, "block")) : L(n),
                function (t, o) {
                    var i = U(),
                        e = gt.innerParams.get(t),
                        r = !e || o.input !== e.input;
                    vt.forEach(function (t) {
                        var e = x[t],
                            n = st(i, e);
                        bt(t, o.inputAttributes), wt(n, e, o), r && L(n)
                    }), o.input && r && yt(o)
                }(t, e), b(U(), e.customClass, "content")
        }

        function xt(t, i) {
            var r = z();
            if (!i.progressSteps || 0 === i.progressSteps.length) return L(r);
            O(r), r.innerHTML = "";
            var a = parseInt(null === i.currentProgressStep ? Ue.getQueueStep() : i.currentProgressStep);
            a >= i.progressSteps.length && w("Invalid currentProgressStep parameter, it should be less than progressSteps.length (currentProgressStep like JS arrays starts from 0)"), i.progressSteps.forEach(function (t, e) {
                var n = function (t) {
                    var e = document.createElement("li");
                    return rt(e, x["progress-step"]), e.innerHTML = t, e
                }(t);
                if (r.appendChild(n), e === a && rt(n, x["active-progress-step"]), e !== i.progressSteps.length - 1) {
                    var o = function (t) {
                        var e = document.createElement("li");
                        return rt(e, x["progress-step-line"]), t.progressStepsDistance && (e.style.width = t.progressStepsDistance), e
                    }(t);
                    r.appendChild(o)
                }
            })
        }

        function Pt(t, e) {
            var n = Q();
            b(n, e.customClass, "header"), xt(0, e),
                function (t, e) {
                    var n = gt.innerParams.get(t);
                    if (n && e.type === n.type && D()) b(D(), e.customClass, "icon");
                    else if (At(), e.type)
                        if (Et(), -1 !== Object.keys(P).indexOf(e.type)) {
                            var o = I(".".concat(x.icon, ".").concat(P[e.type]));
                            O(o), b(o, e.customClass, "icon"), E(o, "swal2-animate-".concat(e.type, "-icon"), e.animation)
                        } else h('Unknown type! Expected "success", "error", "warning", "info" or "question", got "'.concat(e.type, '"'))
                }(t, e),
                function (t, e) {
                    var n = F();
                    if (!e.imageUrl) return L(n);
                    O(n), n.setAttribute("src", e.imageUrl), n.setAttribute("alt", e.imageAlt), T(n, "width", e.imageWidth), T(n, "height", e.imageHeight), n.className = x.image, b(n, e.customClass, "image"), e.imageClass && rt(n, e.imageClass)
                }(0, e),
                function (t, e) {
                    var n = N();
                    M(n, e.title || e.titleText), e.title && nt(e.title, n), e.titleText && (n.innerText = e.titleText), b(n, e.customClass, "title")
                }(0, e),
                function (t, e) {
                    var n = J();
                    n.innerHTML = e.closeButtonHtml, b(n, e.customClass, "closeButton"), M(n, e.showCloseButton), n.setAttribute("aria-label", e.closeButtonAriaLabel)
                }(0, e)
        }

        function St(t, e) {
            ! function (t, e) {
                var n = q();
                T(n, "width", e.width), T(n, "padding", e.padding), e.background && (n.style.background = e.background), n.className = x.popup, e.toast ? (rt([document.documentElement, document.body], x["toast-shown"]), rt(n, x.toast)) : rt(n, x.modal), b(n, e.customClass, "popup"), "string" == typeof e.customClass && rt(n, e.customClass), E(n, x.noanimation, !e.animation)
            }(0, e), mt(0, e), Pt(t, e), kt(t, e), ot(0, e),
                function (t, e) {
                    var n = $();
                    M(n, e.footer), e.footer && nt(e.footer, n), b(n, e.customClass, "footer")
                }(0, e)
        }

        function Bt() {
            return K() && K().click()
        }
        var At = function () {
            for (var t = R(), e = 0; e < t.length; e++) L(t[e])
        },
            Et = function () {
                for (var t = q(), e = window.getComputedStyle(t).getPropertyValue("background-color"), n = t.querySelectorAll("[class^=swal2-success-circular-line], .swal2-success-fix"), o = 0; o < n.length; o++) n[o].style.backgroundColor = e
            };

        function Tt() {
            var t = q();
            t || Ue.fire(""), t = q();
            var e = Z(),
                n = K(),
                o = Y();
            O(e), O(n), rt([t, e], x.loading), n.disabled = !0, o.disabled = !0, t.setAttribute("data-loading", !0), t.setAttribute("aria-busy", !0), t.focus()
        }

        function Ot() {
            return new Promise(function (t) {
                var e = window.scrollX,
                    n = window.scrollY;
                Vt.restoreFocusTimeout = setTimeout(function () {
                    Vt.previousActiveElement && Vt.previousActiveElement.focus ? (Vt.previousActiveElement.focus(), Vt.previousActiveElement = null) : document.body && document.body.focus(), t()
                }, 100), void 0 !== e && void 0 !== n && window.scrollTo(e, n)
            })
        }

        function Lt(t) {
            return Object.prototype.hasOwnProperty.call(Ht, t)
        }

        function Mt(t) {
            return _t[t]
        }
        var jt = [],
            Vt = {},
            Ht = {
                title: "",
                titleText: "",
                text: "",
                html: "",
                footer: "",
                type: null,
                toast: !1,
                customClass: "",
                customContainerClass: "",
                target: "body",
                backdrop: !0,
                animation: !0,
                heightAuto: !0,
                allowOutsideClick: !0,
                allowEscapeKey: !0,
                allowEnterKey: !0,
                stopKeydownPropagation: !0,
                keydownListenerCapture: !1,
                showConfirmButton: !0,
                showCancelButton: !1,
                preConfirm: null,
                confirmButtonText: "OK",
                confirmButtonAriaLabel: "",
                confirmButtonColor: null,
                confirmButtonClass: "",
                cancelButtonText: "Cancel",
                cancelButtonAriaLabel: "",
                cancelButtonColor: null,
                cancelButtonClass: "",
                buttonsStyling: !0,
                reverseButtons: !1,
                focusConfirm: !0,
                focusCancel: !1,
                showCloseButton: !1,
                closeButtonHtml: "&times;",
                closeButtonAriaLabel: "Close this dialog",
                showLoaderOnConfirm: !1,
                imageUrl: null,
                imageWidth: null,
                imageHeight: null,
                imageAlt: "",
                imageClass: "",
                timer: null,
                width: null,
                padding: null,
                background: null,
                input: null,
                inputPlaceholder: "",
                inputValue: "",
                inputOptions: {},
                inputAutoTrim: !0,
                inputClass: "",
                inputAttributes: {},
                inputValidator: null,
                validationMessage: null,
                grow: !1,
                position: "center",
                progressSteps: [],
                currentProgressStep: null,
                progressStepsDistance: null,
                onBeforeOpen: null,
                onAfterClose: null,
                onOpen: null,
                onClose: null,
                scrollbarPadding: !0
            },
            It = ["title", "titleText", "text", "html", "type", "customClass", "showConfirmButton", "showCancelButton", "confirmButtonText", "confirmButtonAriaLabel", "confirmButtonColor", "confirmButtonClass", "cancelButtonText", "cancelButtonAriaLabel", "cancelButtonColor", "cancelButtonClass", "buttonsStyling", "reverseButtons", "imageUrl", "imageWidth", "imageHeigth", "imageAlt", "imageClass", "progressSteps", "currentProgressStep"],
            _t = {
                customContainerClass: "customClass",
                confirmButtonClass: "customClass",
                cancelButtonClass: "customClass",
                imageClass: "customClass",
                inputClass: "customClass"
            },
            qt = ["allowOutsideClick", "allowEnterKey", "backdrop", "focusConfirm", "focusCancel", "heightAuto", "keydownListenerCapture"],
            Rt = Object.freeze({
                isValidParameter: Lt,
                isUpdatableParameter: function (t) {
                    return -1 !== It.indexOf(t)
                },
                isDeprecatedParameter: Mt,
                argsToParams: function (n) {
                    var o = {};
                    switch (r(n[0])) {
                        case "object":
                            s(o, n[0]);
                            break;
                        default:
                            ["title", "html", "type"].forEach(function (t, e) {
                                switch (r(n[e])) {
                                    case "string":
                                        o[t] = n[e];
                                        break;
                                    case "undefined":
                                        break;
                                    default:
                                        h("Unexpected type of ".concat(t, '! Expected "string", got ').concat(r(n[e])))
                                }
                            })
                    }
                    return o
                },
                isVisible: function () {
                    return j(q())
                },
                clickConfirm: Bt,
                clickCancel: function () {
                    return Y() && Y().click()
                },
                getContainer: H,
                getPopup: q,
                getTitle: N,
                getContent: U,
                getImage: F,
                getIcon: D,
                getIcons: R,
                getCloseButton: J,
                getActions: Z,
                getConfirmButton: K,
                getCancelButton: Y,
                getHeader: Q,
                getFooter: $,
                getFocusableElements: X,
                getValidationMessage: W,
                isLoading: function () {
                    return q().hasAttribute("data-loading")
                },
                fire: function () {
                    for (var t = arguments.length, e = new Array(t), n = 0; n < t; n++) e[n] = arguments[n];
                    return l(this, e)
                },
                mixin: function (n) {
                    return function (t) {
                        function e() {
                            return o(this, e), d(this, u(e).apply(this, arguments))
                        }
                        return function (t, e) {
                            if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function");
                            t.prototype = Object.create(e && e.prototype, {
                                constructor: {
                                    value: t,
                                    writable: !0,
                                    configurable: !0
                                }
                            }), e && c(t, e)
                        }(e, t), a(e, [{
                            key: "_main",
                            value: function (t) {
                                return f(u(e.prototype), "_main", this).call(this, s({}, n, t))
                            }
                        }]), e
                    }(this)
                },
                queue: function (t) {
                    var r = this;
                    jt = t;

                    function a(t, e) {
                        jt = [], document.body.removeAttribute("data-swal2-queue-step"), t(e)
                    }
                    var s = [];
                    return new Promise(function (i) {
                        ! function e(n, o) {
                            n < jt.length ? (document.body.setAttribute("data-swal2-queue-step", n), r.fire(jt[n]).then(function (t) {
                                void 0 !== t.value ? (s.push(t.value), e(n + 1, o)) : a(i, {
                                    dismiss: t.dismiss
                                })
                            })) : a(i, {
                                value: s
                            })
                        }(0)
                    })
                },
                getQueueStep: function () {
                    return document.body.getAttribute("data-swal2-queue-step")
                },
                insertQueueStep: function (t, e) {
                    return e && e < jt.length ? jt.splice(e, 0, t) : jt.push(t)
                },
                deleteQueueStep: function (t) {
                    void 0 !== jt[t] && jt.splice(t, 1)
                },
                showLoading: Tt,
                enableLoading: Tt,
                getTimerLeft: function () {
                    return Vt.timeout && Vt.timeout.getTimerLeft()
                },
                stopTimer: function () {
                    return Vt.timeout && Vt.timeout.stop()
                },
                resumeTimer: function () {
                    return Vt.timeout && Vt.timeout.start()
                },
                toggleTimer: function () {
                    var t = Vt.timeout;
                    return t && (t.running ? t.stop() : t.start())
                },
                increaseTimer: function (t) {
                    return Vt.timeout && Vt.timeout.increase(t)
                },
                isTimerRunning: function () {
                    return Vt.timeout && Vt.timeout.isRunning()
                }
            });

        function Dt() {
            var t = gt.innerParams.get(this),
                e = gt.domCache.get(this);
            t.showConfirmButton || (L(e.confirmButton), t.showCancelButton || L(e.actions)), at([e.popup, e.actions], x.loading), e.popup.removeAttribute("aria-busy"), e.popup.removeAttribute("data-loading"), e.confirmButton.disabled = !1, e.cancelButton.disabled = !1
        }

        function Nt() {
            null === S.previousBodyPadding && document.body.scrollHeight > window.innerHeight && (S.previousBodyPadding = parseInt(window.getComputedStyle(document.body).getPropertyValue("padding-right")), document.body.style.paddingRight = S.previousBodyPadding + function () {
                if ("ontouchstart" in window || navigator.msMaxTouchPoints) return 0;
                var t = document.createElement("div");
                t.style.width = "50px", t.style.height = "50px", t.style.overflow = "scroll", document.body.appendChild(t);
                var e = t.offsetWidth - t.clientWidth;
                return document.body.removeChild(t), e
            }() + "px")
        }

        function Ut() {
            return !!window.MSInputMethodContext && !!document.documentMode
        }

        function Ft() {
            var t = H(),
                e = q();
            t.style.removeProperty("align-items"), e.offsetTop < 0 && (t.style.alignItems = "flex-start")
        }
        var zt = function () {
            var e, n = H();
            n.ontouchstart = function (t) {
                e = t.target === n || ! function (t) {
                    return !!(t.scrollHeight > t.clientHeight)
                }(n) && "INPUT" !== t.target.tagName
            }, n.ontouchmove = function (t) {
                e && (t.preventDefault(), t.stopPropagation())
            }
        },
            Wt = {
                swalPromiseResolve: new WeakMap
            };

        function Kt(t, e, n, o) {
            n ? $t(t, o) : (Ot().then(function () {
                return $t(t, o)
            }), Vt.keydownTarget.removeEventListener("keydown", Vt.keydownHandler, {
                capture: Vt.keydownListenerCapture
            }), Vt.keydownHandlerAdded = !1), e.parentNode && e.parentNode.removeChild(e), G() && (null !== S.previousBodyPadding && (document.body.style.paddingRight = S.previousBodyPadding + "px", S.previousBodyPadding = null), function () {
                if (y(document.body, x.iosfix)) {
                    var t = parseInt(document.body.style.top, 10);
                    at(document.body, x.iosfix), document.body.style.top = "", document.body.scrollTop = -1 * t
                }
            }(), "undefined" != typeof window && Ut() && window.removeEventListener("resize", Ft), m(document.body.children).forEach(function (t) {
                t.hasAttribute("data-previous-aria-hidden") ? (t.setAttribute("aria-hidden", t.getAttribute("data-previous-aria-hidden")), t.removeAttribute("data-previous-aria-hidden")) : t.removeAttribute("aria-hidden")
            })), at([document.documentElement, document.body], [x.shown, x["height-auto"], x["no-backdrop"], x["toast-shown"], x["toast-column"]])
        }

        function Yt(t) {
            var e = q();
            if (e && !y(e, x.hide)) {
                var n = gt.innerParams.get(this);
                if (n) {
                    var o = Wt.swalPromiseResolve.get(this);
                    at(e, x.show), rt(e, x.hide),
                        function (t, e, n) {
                            var o = H(),
                                i = ft && V(e),
                                r = n.onClose,
                                a = n.onAfterClose;
                            if (r !== null && typeof r === "function") {
                                r(e)
                            }
                            if (i) {
                                Qt(t, e, o, a)
                            } else {
                                Kt(t, o, ut(), a)
                            }
                        }(this, e, n), o(t || {})
                }
            }
        }

        function Zt(t) {
            for (var e in t) t[e] = new WeakMap
        }
        var Qt = function (t, e, n, o) {
            Vt.swalCloseEventFinishedCallback = Kt.bind(null, t, n, ut(), o), e.addEventListener(ft, function (t) {
                t.target === e && (Vt.swalCloseEventFinishedCallback(), delete Vt.swalCloseEventFinishedCallback)
            })
        },
            $t = function (t, e) {
                setTimeout(function () {
                    null !== e && "function" == typeof e && e(), q() || function (t) {
                        delete t.params, delete Vt.keydownHandler, delete Vt.keydownTarget, Zt(gt), Zt(Wt)
                    }(t)
                })
            };

        function Jt(t, e, n) {
            var o = gt.domCache.get(t);
            e.forEach(function (t) {
                o[t].disabled = n
            })
        }

        function Xt(t, e) {
            if (!t) return !1;
            if ("radio" === t.type)
                for (var n = t.parentNode.parentNode.querySelectorAll("input"), o = 0; o < n.length; o++) n[o].disabled = e;
            else t.disabled = e
        }
        var Gt = function () {
            function n(t, e) {
                o(this, n), this.callback = t, this.remaining = e, this.running = !1, this.start()
            }
            return a(n, [{
                key: "start",
                value: function () {
                    return this.running || (this.running = !0, this.started = new Date, this.id = setTimeout(this.callback, this.remaining)), this.remaining
                }
            }, {
                key: "stop",
                value: function () {
                    return this.running && (this.running = !1, clearTimeout(this.id), this.remaining -= new Date - this.started), this.remaining
                }
            }, {
                key: "increase",
                value: function (t) {
                    var e = this.running;
                    return e && this.stop(), this.remaining += t, e && this.start(), this.remaining
                }
            }, {
                key: "getTimerLeft",
                value: function () {
                    return this.running && (this.stop(), this.start()), this.remaining
                }
            }, {
                key: "isRunning",
                value: function () {
                    return this.running
                }
            }]), n
        }(),
            te = {
                email: function (t, e) {
                    return /^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9.-]+\.[a-zA-Z0-9-]{2,24}$/.test(t) ? Promise.resolve() : Promise.resolve(e || "Invalid email address")
                },
                url: function (t, e) {
                    return /^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._+~#=]{2,256}\.[a-z]{2,63}\b([-a-zA-Z0-9@:%_+.~#?&/=]*)$/.test(t) ? Promise.resolve() : Promise.resolve(e || "Invalid URL")
                }
            };

        function ee(t) {
            ! function (e) {
                e.inputValidator || Object.keys(te).forEach(function (t) {
                    e.input === t && (e.inputValidator = te[t])
                })
            }(t), t.showLoaderOnConfirm && !t.preConfirm && w("showLoaderOnConfirm is set to true, but preConfirm is not defined.\nshowLoaderOnConfirm should be used together with preConfirm, see usage example:\nhttps://sweetalert2.github.io/#ajax-request"), t.animation = C(t.animation),
                function (t) {
                    t.target && ("string" != typeof t.target || document.querySelector(t.target)) && ("string" == typeof t.target || t.target.appendChild) || (w('Target parameter is not valid, defaulting to "body"'), t.target = "body")
                }(t), "string" == typeof t.title && (t.title = t.title.split("\n").join("<br />")), lt(t)
        }

        function ne(t, e) {
            t.removeEventListener(ft, ne), e.style.overflowY = "auto"
        }

        function oe(t) {
            var e = H(),
                n = q();
            "function" == typeof t.onBeforeOpen && t.onBeforeOpen(n), pe(e, n, t), de(e, n), G() && fe(e, t.scrollbarPadding), ut() || Vt.previousActiveElement || (Vt.previousActiveElement = document.activeElement), "function" == typeof t.onOpen && setTimeout(function () {
                return t.onOpen(n)
            })
        }

        function ie(t, e) {
            "select" === e.input || "radio" === e.input ? me(t, e) : -1 !== ["text", "email", "number", "tel", "textarea"].indexOf(e.input) && v(e.inputValue) && he(t, e)
        }

        function re(t, e) {
            t.disableButtons(), e.input ? ye(t, e) : be(t, e, !0)
        }

        function ae(t, e) {
            t.disableButtons(), e(k.cancel)
        }

        function se(t, e) {
            t.closePopup({
                value: e
            })
        }

        function ue(e, t, n, o) {
            t.keydownTarget && t.keydownHandlerAdded && (t.keydownTarget.removeEventListener("keydown", t.keydownHandler, {
                capture: t.keydownListenerCapture
            }), t.keydownHandlerAdded = !1), n.toast || (t.keydownHandler = function (t) {
                return Be(e, t, n, o)
            }, t.keydownTarget = n.keydownListenerCapture ? window : q(), t.keydownListenerCapture = n.keydownListenerCapture, t.keydownTarget.addEventListener("keydown", t.keydownHandler, {
                capture: t.keydownListenerCapture
            }), t.keydownHandlerAdded = !0)
        }

        function ce(t, e, n) {
            for (var o = X(t.focusCancel), i = 0; i < o.length; i++) return (e += n) === o.length ? e = 0 : -1 === e && (e = o.length - 1), o[e].focus();
            q().focus()
        }

        function le(t, e, n) {
            e.toast ? Le(t, e, n) : (je(t), Ve(t), He(t, e, n))
        }
        var de = function (t, e) {
            ft && V(e) ? (t.style.overflowY = "hidden", e.addEventListener(ft, ne.bind(null, e, t))) : t.style.overflowY = "auto"
        },
            fe = function (t, e) {
                ! function () {
                    if (/iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream && !y(document.body, x.iosfix)) {
                        var t = document.body.scrollTop;
                        document.body.style.top = -1 * t + "px", rt(document.body, x.iosfix), zt()
                    }
                }(), "undefined" != typeof window && Ut() && (Ft(), window.addEventListener("resize", Ft)), m(document.body.children).forEach(function (t) {
                    t === H() || function (t, e) {
                        if ("function" == typeof t.contains) return t.contains(e)
                    }(t, H()) || (t.hasAttribute("aria-hidden") && t.setAttribute("data-previous-aria-hidden", t.getAttribute("aria-hidden")), t.setAttribute("aria-hidden", "true"))
                }), e && Nt(), setTimeout(function () {
                    t.scrollTop = 0
                })
            },
            pe = function (t, e, n) {
                n.animation && (rt(e, x.show), rt(t, x.fade)), O(e), rt([document.documentElement, document.body, t], x.shown), n.heightAuto && n.backdrop && !n.toast && rt([document.documentElement, document.body], x["height-auto"])
            },
            me = function (e, n) {
                function o(t) {
                    return ge[n.input](i, ve(t), n)
                }
                var i = U();
                v(n.inputOptions) ? (Tt(), n.inputOptions.then(function (t) {
                    e.hideLoading(), o(t)
                })) : "object" === r(n.inputOptions) ? o(n.inputOptions) : h("Unexpected type of inputOptions! Expected object, Map or Promise, got ".concat(r(n.inputOptions)))
            },
            he = function (e, n) {
                var o = e.getInput();
                L(o), n.inputValue.then(function (t) {
                    o.value = "number" === n.input ? parseFloat(t) || 0 : t + "", O(o), o.focus(), e.hideLoading()
                }).catch(function (t) {
                    h("Error in inputValue promise: " + t), o.value = "", O(o), o.focus(), e.hideLoading()
                })
            },
            ge = {
                select: function (t, e, i) {
                    var r = st(t, x.select);
                    e.forEach(function (t) {
                        var e = t[0],
                            n = t[1],
                            o = document.createElement("option");
                        o.value = e, o.innerHTML = n, i.inputValue.toString() === e.toString() && (o.selected = !0), r.appendChild(o)
                    }), r.focus()
                },
                radio: function (t, e, a) {
                    var s = st(t, x.radio);
                    e.forEach(function (t) {
                        var e = t[0],
                            n = t[1],
                            o = document.createElement("input"),
                            i = document.createElement("label");
                        o.type = "radio", o.name = x.radio, o.value = e, a.inputValue.toString() === e.toString() && (o.checked = !0);
                        var r = document.createElement("span");
                        r.innerHTML = n, r.className = x.label, i.appendChild(o), i.appendChild(r), s.appendChild(i)
                    });
                    var n = s.querySelectorAll("input");
                    n.length && n[0].focus()
                }
            },
            ve = function (e) {
                var n = [];
                return "undefined" != typeof Map && e instanceof Map ? e.forEach(function (t, e) {
                    n.push([e, t])
                }) : Object.keys(e).forEach(function (t) {
                    n.push([t, e[t]])
                }), n
            },
            ye = function (e, n) {
                var o = we(e, n);
                n.inputValidator ? (e.disableInput(), Promise.resolve().then(function () {
                    return n.inputValidator(o, n.validationMessage)
                }).then(function (t) {
                    e.enableButtons(), e.enableInput(), t ? e.showValidationMessage(t) : be(e, n, o)
                })) : e.getInput().checkValidity() ? be(e, n, o) : (e.enableButtons(), e.showValidationMessage(n.validationMessage))
            },
            be = function (e, t, n) {
                (t.showLoaderOnConfirm && Tt(), t.preConfirm) ? (e.resetValidationMessage(), Promise.resolve().then(function () {
                    return t.preConfirm(n, t.validationMessage)
                }).then(function (t) {
                    j(W()) || !1 === t ? e.hideLoading() : se(e, void 0 === t ? n : t)
                })) : se(e, n)
            },
            we = function (t, e) {
                var n = t.getInput();
                if (!n) return null;
                switch (e.input) {
                    case "checkbox":
                        return Ce(n);
                    case "radio":
                        return ke(n);
                    case "file":
                        return xe(n);
                    default:
                        return e.inputAutoTrim ? n.value.trim() : n.value
                }
            },
            Ce = function (t) {
                return t.checked ? 1 : 0
            },
            ke = function (t) {
                return t.checked ? t.value : null
            },
            xe = function (t) {
                return t.files.length ? t.files[0] : null
            },
            Pe = ["ArrowLeft", "ArrowRight", "ArrowUp", "ArrowDown", "Left", "Right", "Up", "Down"],
            Se = ["Escape", "Esc"],
            Be = function (t, e, n, o) {
                n.stopKeydownPropagation && e.stopPropagation(), "Enter" === e.key ? Ae(t, e, n) : "Tab" === e.key ? Ee(e, n) : -1 !== Pe.indexOf(e.key) ? Te() : -1 !== Se.indexOf(e.key) && Oe(e, n, o)
            },
            Ae = function (t, e, n) {
                if (!e.isComposing && e.target && t.getInput() && e.target.outerHTML === t.getInput().outerHTML) {
                    if (-1 !== ["textarea", "file"].indexOf(n.input)) return;
                    Bt(), e.preventDefault()
                }
            },
            Ee = function (t, e) {
                for (var n = t.target, o = X(e.focusCancel), i = -1, r = 0; r < o.length; r++)
                    if (n === o[r]) {
                        i = r;
                        break
                    } t.shiftKey ? ce(e, i, -1) : ce(e, i, 1), t.stopPropagation(), t.preventDefault()
            },
            Te = function () {
                var t = K(),
                    e = Y();
                document.activeElement === t && j(e) ? e.focus() : document.activeElement === e && j(t) && t.focus()
            },
            Oe = function (t, e, n) {
                C(e.allowEscapeKey) && (t.preventDefault(), n(k.esc))
            },
            Le = function (t, e, n) {
                t.popup.onclick = function () {
                    e.showConfirmButton || e.showCancelButton || e.showCloseButton || e.input || n(k.close)
                }
            },
            Me = !1,
            je = function (e) {
                e.popup.onmousedown = function () {
                    e.container.onmouseup = function (t) {
                        e.container.onmouseup = void 0, t.target === e.container && (Me = !0)
                    }
                }
            },
            Ve = function (e) {
                e.container.onmousedown = function () {
                    e.popup.onmouseup = function (t) {
                        e.popup.onmouseup = void 0, t.target !== e.popup && !e.popup.contains(t.target) || (Me = !0)
                    }
                }
            },
            He = function (e, n, o) {
                e.container.onclick = function (t) {
                    Me ? Me = !1 : t.target === e.container && C(n.allowOutsideClick) && o(k.backdrop)
                }
            };
        var Ie = function (t, e, n) {
            e.timer && (t.timeout = new Gt(function () {
                n("timer"), delete t.timeout
            }, e.timer))
        },
            _e = function (t, e) {
                if (!e.toast) return C(e.allowEnterKey) ? e.focusCancel && j(t.cancelButton) ? t.cancelButton.focus() : e.focusConfirm && j(t.confirmButton) ? t.confirmButton.focus() : void ce(e, -1, 1) : qe()
            },
            qe = function () {
                document.activeElement && "function" == typeof document.activeElement.blur && document.activeElement.blur()
            };
        var Re, De = Object.freeze({
            hideLoading: Dt,
            disableLoading: Dt,
            getInput: function (t) {
                var e = gt.innerParams.get(t || this),
                    n = gt.domCache.get(t || this);
                return n ? B(n.content, e.input) : null
            },
            close: Yt,
            closePopup: Yt,
            closeModal: Yt,
            closeToast: Yt,
            enableButtons: function () {
                Jt(this, ["confirmButton", "cancelButton"], !1)
            },
            disableButtons: function () {
                Jt(this, ["confirmButton", "cancelButton"], !0)
            },
            enableConfirmButton: function () {
                g("Swal.disableConfirmButton()", "Swal.getConfirmButton().removeAttribute('disabled')"), Jt(this, ["confirmButton"], !1)
            },
            disableConfirmButton: function () {
                g("Swal.enableConfirmButton()", "Swal.getConfirmButton().setAttribute('disabled', '')"), Jt(this, ["confirmButton"], !0)
            },
            enableInput: function () {
                return Xt(this.getInput(), !1)
            },
            disableInput: function () {
                return Xt(this.getInput(), !0)
            },
            showValidationMessage: function (t) {
                var e = gt.domCache.get(this);
                e.validationMessage.innerHTML = t;
                var n = window.getComputedStyle(e.popup);
                e.validationMessage.style.marginLeft = "-".concat(n.getPropertyValue("padding-left")), e.validationMessage.style.marginRight = "-".concat(n.getPropertyValue("padding-right")), O(e.validationMessage);
                var o = this.getInput();
                o && (o.setAttribute("aria-invalid", !0), o.setAttribute("aria-describedBy", x["validation-message"]), A(o), rt(o, x.inputerror))
            },
            resetValidationMessage: function () {
                var t = gt.domCache.get(this);
                t.validationMessage && L(t.validationMessage);
                var e = this.getInput();
                e && (e.removeAttribute("aria-invalid"), e.removeAttribute("aria-describedBy"), at(e, x.inputerror))
            },
            getProgressSteps: function () {
                return g("Swal.getProgressSteps()", "const swalInstance = Swal.fire({progressSteps: ['1', '2', '3']}); const progressSteps = swalInstance.params.progressSteps"), gt.innerParams.get(this).progressSteps
            },
            setProgressSteps: function (t) {
                g("Swal.setProgressSteps()", "Swal.update()");
                var e = s({}, gt.innerParams.get(this), {
                    progressSteps: t
                });
                xt(0, e), gt.innerParams.set(this, e)
            },
            showProgressSteps: function () {
                var t = gt.domCache.get(this);
                O(t.progressSteps)
            },
            hideProgressSteps: function () {
                var t = gt.domCache.get(this);
                L(t.progressSteps)
            },
            _main: function (t) {
                ! function (t) {
                    for (var e in t) Lt(i = e) || w('Unknown parameter "'.concat(i, '"')), t.toast && (o = e, -1 !== qt.indexOf(o) && w('The parameter "'.concat(o, '" is incompatible with toasts'))), Mt(n = void 0) && g(n, Mt(n));
                    var n, o, i
                }(t), q() && Vt.swalCloseEventFinishedCallback && (Vt.swalCloseEventFinishedCallback(), delete Vt.swalCloseEventFinishedCallback), Vt.deferDisposalTimer && (clearTimeout(Vt.deferDisposalTimer), delete Vt.deferDisposalTimer);
                var e = s({}, Ht, t);
                ee(e), Object.freeze(e), Vt.timeout && (Vt.timeout.stop(), delete Vt.timeout), clearTimeout(Vt.restoreFocusTimeout);
                var n = function (t) {
                    var e = {
                        popup: q(),
                        container: H(),
                        content: U(),
                        actions: Z(),
                        confirmButton: K(),
                        cancelButton: Y(),
                        closeButton: J(),
                        validationMessage: W(),
                        progressSteps: z()
                    };
                    return gt.domCache.set(t, e), e
                }(this);
                return St(this, e), gt.innerParams.set(this, e),
                    function (n, o, i) {
                        return new Promise(function (t) {
                            var e = function t(e) {
                                n.closePopup({
                                    dismiss: e
                                })
                            };
                            Wt.swalPromiseResolve.set(n, t);
                            Ie(Vt, i, e);
                            o.confirmButton.onclick = function () {
                                return re(n, i)
                            };
                            o.cancelButton.onclick = function () {
                                return ae(n, e)
                            };
                            o.closeButton.onclick = function () {
                                return e(k.close)
                            };
                            le(o, i, e);
                            ue(n, Vt, i, e);
                            if (i.toast && (i.input || i.footer || i.showCloseButton)) {
                                rt(document.body, x["toast-column"])
                            } else {
                                at(document.body, x["toast-column"])
                            }
                            ie(n, i);
                            oe(i);
                            _e(o, i);
                            o.container.scrollTop = 0
                        })
                    }(this, n, e)
            },
            update: function (e) {
                var n = {};
                Object.keys(e).forEach(function (t) {
                    Ue.isUpdatableParameter(t) ? n[t] = e[t] : w('Invalid parameter to update: "'.concat(t, '". Updatable params are listed here: https://github.com/sweetalert2/sweetalert2/blob/master/src/utils/params.js'))
                });
                var t = s({}, gt.innerParams.get(this), n);
                St(this, t), gt.innerParams.set(this, t), Object.defineProperties(this, {
                    params: {
                        value: s({}, this.params, e),
                        writable: !1,
                        enumerable: !0
                    }
                })
            }
        });

        function Ne() {
            if ("undefined" != typeof window) {
                "undefined" == typeof Promise && h("This package requires a Promise library, please include a shim to enable it in this browser (See: https://github.com/sweetalert2/sweetalert2/wiki/Migration-from-SweetAlert-to-SweetAlert2#1-ie-support)"), Re = this;
                for (var t = arguments.length, e = new Array(t), n = 0; n < t; n++) e[n] = arguments[n];
                var o = Object.freeze(this.constructor.argsToParams(e));
                Object.defineProperties(this, {
                    params: {
                        value: o,
                        writable: !1,
                        enumerable: !0,
                        configurable: !0
                    }
                });
                var i = this._main(this.params);
                gt.promise.set(this, i)
            }
        }
        Ne.prototype.then = function (t) {
            return gt.promise.get(this).then(t)
        }, Ne.prototype.finally = function (t) {
            return gt.promise.get(this).finally(t)
        }, s(Ne.prototype, De), s(Ne, Rt), Object.keys(De).forEach(function (e) {
            Ne[e] = function () {
                var t;
                if (Re) return (t = Re)[e].apply(t, arguments)
            }
        }), Ne.DismissReason = k, Ne.version = "8.16.3";
        var Ue = Ne;
        return Ue.default = Ue
    }), void 0 !== this && this.Sweetalert2 && (this.swal = this.sweetAlert = this.Swal = this.SweetAlert = this.Sweetalert2);