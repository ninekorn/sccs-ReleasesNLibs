using(npc);
using(console);

var SC_Utilities =
{
    //Sebastian Lague Youtube Pathfind Tutorial START
    npcCheckDistance: function (nodeA, nodeB) {
        var dstX = Math.abs((nodeA.x) - (nodeB.x));
        var dstZ = Math.abs((nodeA.y) - (nodeB.y));

        if (dstX > dstZ)
            return 14 * dstZ + 10 * (dstX - dstZ);
        return 14 * dstX + 10 * (dstZ - dstX);
    },
    //Sebastian Lague Youtube Pathfind Tutorial END

    Dot: function (aX, aY, bX, bY) {
        return (aX * bX) + (aY * bY);
    },

    GetDistancePow: function (x1, y1, x2, y2) {
        return Math.sqrt(Math.pow((x2 - x1), 2) + Math.pow((y2 - y1), 2));
    },

    GetDistance: function (a,b) {
        return Math.sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    },


    crossProd: function (a, b) {
        var cross = a.x * b.y - b.x * a.y;
        return cross;
    },

    NSEW: function (a, b, c) {
        return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)) > 0;
    },

    NSDIST: function (a, b, c) {
        return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x));
    },

    EWDIST: function (a, b, c) {
        return ((b.y - a.y) * (c.x - a.x) - (b.x - a.x) * (c.y - a.y));
    },

    RotatePoint: function (pointToRotate, centerPoint, angleInDegrees) {
        var angleInRadians = angleInDegrees * (Math.PI / 180);
        var cosTheta = Math.cos(angleInRadians);
        var sinTheta = Math.sin(angleInRadians);

        var newX = (cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x);
        var newY = (sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y);

        var newPos = { x: newX, y: newY };

        return newPos;
    },

    finder: function(cmp, arr, getter) {
        var val = getter(arr[0]);
        for (var i = 1; i < arr.length; i++) {
            val = cmp(val, getter(arr[i]));
        }
        return val;
    },

    //Got that from the internet from gamedev.stackOverflow if I remember correctly.
    contains: function (a, obj)
    {
        for (var i = 0; i < a.length; i++)
        {
            //console.PrintError(a[i].indexOf(obj));
            if (JSON.stringify(a[i]) === JSON.stringify(obj)) //a[i].valueOf() === obj.valueOf() //a[i].indexOf(obj) >= 0
            {
                return true;
            }
        }

        return false;
    },


    isAlphaOnly: function (a)
    {
        var b = '';
        for (var i = 0; i < a.length; i++)
        {
            if (a[i] >= 'A' && a[i] <= 'z')
            {

            }
            else
            {
                return 0;
            }
        }
        return 1;
    },





    //Got that from the internet from gamedev.stackOverflow if I remember correctly.
    alphaOnly: function(a) {
        var b = '';
        for (var i = 0; i < a.length; i++)
        {
            if (a[i] >= 'A' && a[i] <= 'z')
            {
                b += a[i];
            }         
        }
        return b;
    },

    //Got that from the internet from gamedev.stackOverflow if I remember correctly.
    numberOnly: function(a) {
        var b = '';
        for (var i = 0; i < a.length; i++) {
            if (a[i] >= '0' && a[i] <= '9') b += a[i];
        }
        return b;
    },

    setBehavior: function (id, isEvading)
    {
        if (isEvading == 1) {
            npc.SetBehavior(id, "avoid_asteroids", true);
            npc.SetBehavior(id, "avoid_ships", true);
            npc.SetBehavior(id, "avoid_bases", true);
            npc.SetBehavior(id, "avoid_debris", true);
        }
        else {
            npc.SetBehavior(id, "avoid_asteroids", false);
            npc.SetBehavior(id, "avoid_ships", false);
            npc.SetBehavior(id, "avoid_bases", false);
            npc.SetBehavior(id, "avoid_debris", false);
        }
    }
};















































/*//https://stackoverflow.com/questions/13695317/rotate-a-point-around-another-point
function RotatePoint(pointToRotate, centerPoint, angleInDegrees)
{
    var angleInRadians = angleInDegrees * (Math.PI / 180);
    var cosTheta = Math.cos(angleInRadians);
    var sinTheta = Math.sin(angleInRadians);

    var newX = (cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x);
    var newY = (sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y);

    var newPos = { x: newX, y: newY };

    return newPos;
}




function Dot(aX, aY, bX, bY) {
    return (aX * bX) + (aY * bY);
}

function finder(cmp, arr, getter) {
    var val = getter(arr[0]);
    for (var i = 1; i < arr.length; i++) {
        val = cmp(val, getter(arr[i]));
    }
    return val;
}

function GetDistance(x1, y1, x2, y2) {
    return Math.sqrt(Math.pow((x2 - x1), 2) + Math.pow((y2 - y1), 2));
}

function contains(a, obj) {
    for (var i = 0; i < a.length; i++) {
        if (a[i] === obj) {
            return true;
        }
    }
    return false;
}

function alphaOnly(a) {
    var b = '';
    for (var i = 0; i < a.length; i++) {
        if (a[i] >= 'A' && a[i] <= 'z') b += a[i];
    }
    return b;
}

function numberOnly(a) {
    var b = '';
    for (var i = 0; i < a.length; i++) {
        if (a[i] >= '0' && a[i] <= '9') b += a[i];
    }
    return b;
}





function RotatePoint(pointToRotate, centerPoint, angleInDegrees) {
    var angleInRadians = angleInDegrees * (Math.PI / 180);
    var cosTheta = Math.cos(angleInRadians);
    var sinTheta = Math.sin(angleInRadians);

    var newX = (cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x);
    var newY = (sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y);

    var newPos = { x: newX, y: newY };

    return newPos;
}
function setBehavior(id, isEvading) {
    if (isEvading == 1) {
        npc.SetBehavior(id, "avoid_asteroids", true);
        npc.SetBehavior(id, "avoid_ships", true);
        npc.SetBehavior(id, "avoid_bases", true);
        npc.SetBehavior(id, "avoid_debris", true);
    }
    else {
        npc.SetBehavior(id, "avoid_asteroids", false);
        npc.SetBehavior(id, "avoid_ships", false);
        npc.SetBehavior(id, "avoid_bases", false);
        npc.SetBehavior(id, "avoid_debris", false);
    }
}


//https://stackoverflow.com/questions/2333292/cross-product-of-2-2d-vectors
function crossProd(a, b) {
    var cross = a.x * b.y - b.x * a.y;
    return cross;
}


function NSEW(a, b, c) {
    return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)) > 0;
}

function NSDIST(a, b, c) {
    return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x));
}

function EWDIST(a, b, c) {
    return ((b.y - a.y) * (c.x - a.x) - (b.x - a.x) * (c.y - a.y));
}*/




