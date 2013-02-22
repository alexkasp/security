/**
 * A JS for half/full pie chart drawing

 * @author Mohamed Samir <mohamedsamir216@gmail.com>
 * @Date Started June 24 2012
 */

//Raphael Paper
var paper;


function drawHalfPie(targetDiv,percentages){
    paper = Raphael(targetDiv, 122,65);
    paper.pieChart(65,63,45,percentages,Array("#e04d2d","#b23419","#4097b4","#095e79"),"half");
}

/*
Main drawing function pass parameters to pieChart, after initializing Raphael chart
*/
function drawPie(targetDiv,x,y,chartWidth,chartHeight,radious,percentages,colors,type){
    paper = Raphael(targetDiv, chartWidth,chartHeight);
    paper.pieChart(x,y,radious,percentages,colors,type);
}


/*
    Draw half pie chart using center point and giving the percentages of inner volumes 
    and radious of the half circle.

    using inner method sector to draw sectors using the given percentages then adding 
    the animation to show given percentage of each sector with animation style.

*/
Raphael.fn.pieChart = function (cx, cy, r, values, labels, type) {
    var paper = this,
        rad = Math.PI / 180,
        chart = this.set();
    var stroke="white";
    var radSize = 180;
    if(type.toLowerCase() =="full")radSize=360;
    
    /*
        Draw a sector from center point with specific radious [r] to rotate clockwise from start angle [startAngle in Radian]
        to end angle [endAngle in radian] with ability to add styling/animation in the params array
    */
    function sector(cx, cy, r, startAngle, endAngle, params) {
        var x1 = cx + r * Math.cos(-startAngle * rad),
            x2 = cx + r * Math.cos(-endAngle * rad),
            y1 = cy + r * Math.sin(-startAngle * rad),
            y2 = cy + r * Math.sin(-endAngle * rad);
        return paper.path(["M", cx, cy, "L", x1, y1, "A", r, r, 0, +(endAngle - startAngle > 180), 0, x2, y2, "z"]).attr(params);
    }
    var angle = 0,
        total = 0,
        start = 0,
        process = function (j) {
            var value = values[j],
                angleplus = radSize * value / total,
                popangle = angle + (angleplus / 2),
                ms = 500,
                delta = 30,
                bcolor =labels[j*2],
                color=labels[j*2+1],
              	p;
               var txt;
		if (j==0) {
			if (values[0]>values[1]) {p = sector(cx-10, cy, r+10, angle, angle + angleplus, {fill: "90-" + bcolor + "-" + color, stroke: stroke, "stroke-width":0.01});}
			else if (values[0]==values[1]) {p = sector(cx-7, cy, r+10, angle, angle + angleplus, {fill: "90-" + bcolor + "-" + color, stroke: stroke, "stroke-width":0.01});}
			else {p = sector(cx-7, cy, r, angle, angle + angleplus, {fill: "90-" + bcolor + "-" + color, stroke: stroke, "stroke-width":0.01});}
		} else {
			if (values[0]<=values[1]) {p = sector(cx-7, cy, r+10, angle, angle + angleplus, {fill: "90-" + bcolor + "-" + color, stroke: stroke, "stroke-width":0.01});}
			else {p = sector(cx-10, cy, r, angle, angle + angleplus, {fill: "90-" + bcolor + "-" + color, stroke: stroke, "stroke-width":0.01});}
		}
		if (values[0]>values[1]) {
			if (values[1]==0) {paper.text(cx-8, cy-10, values[0]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});}
			else {
		                paper.text(cx+24, cy-10, values[0]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});
				paper.text(cx-36, cy-10, values[1]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});
				}
		} else if (values[0]<values[1]) {
			if (values[0]==0) {paper.text(cx-8, cy-10, values[1]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});}
			else {
		                paper.text(cx+22, cy-10, values[0]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});
		                paper.text(cx-38, cy-10, values[1]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});
				}
		} else {
	                paper.text(cx+19, cy-10, values[0]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});
	                paper.text(cx-33, cy-10, values[1]).attr({"font-size": 12,"font-weight":"bold", "fill":"#ffffff"});
		}
                angle += angleplus;
                chart.push(p);
                start += .1;
                };
    for (var i = 0, ii = values.length; i < ii; i++) {
        total += values[i];
    }
    for (i = 0; i < ii; i++) {
        process(i);
    }
    return chart;
};
