﻿!function(e){"function"==typeof define&&define.amd?define(["jquery"],e):"object"==typeof exports?module.exports=e(require("jquery")):e(jQuery)}(function(a){function o(e,o){var a,t=document.createElement("canvas");e.appendChild(t),"object"==typeof G_vmlCanvasManager&&G_vmlCanvasManager.initElement(t);var i=t.getContext("2d");t.width=t.height=o.size;var n=1;1<window.devicePixelRatio&&(n=window.devicePixelRatio,t.style.width=t.style.height=[o.size,"px"].join(""),t.width=t.height=o.size*n,i.scale(n,n)),i.translate(o.size/2,o.size/2),i.rotate((o.rotate/180-.5)*Math.PI);var r=(o.size-o.lineWidth)/2;function s(e,t,a){var n=(a=Math.min(Math.max(-1,a||0),1))<=0;i.beginPath(),i.arc(0,0,r,0,2*Math.PI*a,n),i.strokeStyle=e,i.lineWidth=t,i.stroke()}function d(){o.scaleColor&&function(){var e,t;i.lineWidth=1,i.fillStyle=o.scaleColor,i.save();for(var a=24;0<a;--a)e=a%6==0?(t=o.scaleLength,0):(t=.6*o.scaleLength,o.scaleLength-t),i.fillRect(-o.size/2+e,0,t,1),i.rotate(Math.PI/12);i.restore()}(),o.trackColor&&s(o.trackColor,o.trackWidth||o.lineWidth,1)}o.scaleColor&&o.scaleLength&&(r-=o.scaleLength+2),Date.now=Date.now||function(){return+new Date};var l=window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||function(e){window.setTimeout(e,1e3/60)};this.getCanvas=function(){return t},this.getCtx=function(){return i},this.clear=function(){i.clearRect(o.size/-2,o.size/-2,o.size,o.size)},this.draw=function(e){var t;o.scaleColor||o.trackColor?i.getImageData&&i.putImageData?a?i.putImageData(a,0,0):(d(),a=i.getImageData(0,0,o.size*n,o.size*n)):(this.clear(),d()):this.clear(),i.lineCap=o.lineCap,t="function"==typeof o.barColor?o.barColor(e):o.barColor,s(t,o.lineWidth,e/100)}.bind(this),this.animate=function(a,n){var i=Date.now();o.onStart(a,n);var r=function(){var e=Math.min(Date.now()-i,o.animate.duration),t=o.easing(this,e,a,n-a,o.animate.duration);this.draw(t),o.onStep(a,n,t),e>=o.animate.duration?o.onStop(a,n):l(r)}.bind(this);l(r)}.bind(this)}function n(t,a){var n={barColor:"#ef1e25",trackColor:"#f9f9f9",scaleColor:"#dfe0e0",scaleLength:5,lineCap:"round",lineWidth:3,trackWidth:void 0,size:110,rotate:0,animate:{duration:1e3,enabled:!0},easing:function(e,t,a,n,i){return(t/=i/2)<1?n/2*t*t+a:-n/2*(--t*(t-2)-1)+a},onStart:function(e,t){},onStep:function(e,t,a){},onStop:function(e,t){}};if(void 0!==o)n.renderer=o;else{if("undefined"==typeof SVGRenderer)throw new Error("Please load either the SVG- or the CanvasRenderer");n.renderer=SVGRenderer}var i={},r=0,e=function(){for(var e in this.el=t,this.options=i,n)n.hasOwnProperty(e)&&(i[e]=a&&void 0!==a[e]?a[e]:n[e],"function"==typeof i[e]&&(i[e]=i[e].bind(this)));"string"==typeof i.easing&&"undefined"!=typeof jQuery&&jQuery.isFunction(jQuery.easing[i.easing])?i.easing=jQuery.easing[i.easing]:i.easing=n.easing,"number"==typeof i.animate&&(i.animate={duration:i.animate,enabled:!0}),"boolean"!=typeof i.animate||i.animate||(i.animate={duration:1e3,enabled:i.animate}),this.renderer=new i.renderer(t,i),this.renderer.draw(r),t.dataset&&t.dataset.percent?this.update(parseFloat(t.dataset.percent)):t.getAttribute&&t.getAttribute("data-percent")&&this.update(parseFloat(t.getAttribute("data-percent")))}.bind(this);this.update=function(e){return e=parseFloat(e),i.animate.enabled?this.renderer.animate(r,e):this.renderer.draw(e),r=e,this}.bind(this),this.disableAnimation=function(){return i.animate.enabled=!1,this},this.enableAnimation=function(){return i.animate.enabled=!0,this},e()}a.fn.easyPieChart=function(t){return this.each(function(){var e;a.data(this,"easyPieChart")||(e=a.extend({},t,a(this).data()),a.data(this,"easyPieChart",new n(this,e)))})}}),document.addEventListener("DOMContentLoaded",function(){$(".js-easy-pie-chart").each(function(){var e=$(this),t=e.css("color")||color.primary._700,a=e.data("trackcolor")||"rgba(0,0,0,0.04)",n=parseInt(e.data("piesize"))||50,i=e.data("scalecolor")||e.css("color"),r=parseInt(e.data("scalelength"))||0,o=parseInt(e.data("linewidth"))||parseInt(n/8.5),s=e.data("linecap")||"butt";e.easyPieChart({size:n,barColor:t,trackColor:a,scaleColor:i,scaleLength:r,lineCap:s,lineWidth:o,animate:{duration:1500,enabled:!0},onStep:function(e,t,a){$(this.el).find(".js-percent").text(Math.round(a))}}),e=null})});