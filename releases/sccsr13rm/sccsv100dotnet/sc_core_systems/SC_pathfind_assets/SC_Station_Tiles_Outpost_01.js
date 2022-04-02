using(console);


var arrayOfXMLScraps = [];
arrayOfXMLScraps.push("scrap_metal_000");
//arrayOfXMLScraps.push("scrap_metal_001");
//arrayOfXMLScraps.push("scrap_metal_002");
//arrayOfXMLScraps.push("scrap_metal_003");
//arrayOfXMLScraps.push("scrap_metal_004");
//arrayOfXMLScraps.push("scrap_metal_005");
//arrayOfXMLScraps.push("scrap_metal_006");
//arrayOfXMLScraps.push("scrap_metal_007");
//arrayOfXMLScraps.push("scrap_metal_008");

var SC_Station_Tiles_Outpost_01 = {

    buildTiles: function (theBase) {
        var widthLeft = 21;
        var widthRight = 21;

        var heightTop = 19;
        var heightBottom = 42;

        var arrayOfStationTiles = [];

        for (var x = -widthLeft; x <= widthRight; x++)
        {
            for (var y = -heightBottom; y <= heightTop; y++)
            {
                arrayOfStationTiles.push(0);
            }
        }

        var xx = 0;
        var yy = 0;

        for (var x = -widthLeft; x <= widthRight; x++) {
            for (var y = -heightBottom; y <= heightTop; y++) {
                xx = x;
                yy = y;

                if (x < 0) {
                    xx *= -1;
                    xx += (widthRight);
                }

                if (y < 0) {
                    yy *= -1;
                    yy += (heightTop);
                }

                if (x >= -21 && x <= -9 && y == -3) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= 9 && x < 22 && y == -3) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x >= -21 && x <= -9 && y == -21) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= 9 && x < 22 && y == -21) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                if (x >= -21 && x <= -18 && y == -2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                if (x <= 21 && x >= 18 && y == -2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                if (x >= -21 && x <= -18 && y == -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                if (x <= 21 && x >= 18 && y == -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }














                
                //TOP FILLING

                else if (x >= -17 && x <= -11 && y >= -2 && y <= 4) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 17 && x >= 11 && y >= -2 && y <= 4) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 16 && y >= 5 && y <= 7) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 15 && y >= 5 && y <= 9) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 14 && y >= 5 && y <= 11) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 13 && y >= 5 && y <= 12) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 12 && y >= 5 && y <= 13) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 11 && y >= 5 && y <= 14) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -16 && y >= 5 && y <= 7) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -15 && y >= 5 && y <= 9) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -14 && y >= 5 && y <= 11) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -13 && y >= 5 && y <= 12) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -12 && y >= 5 && y <= 13) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -11 && y >= 5 && y <= 14) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 10 && y >= -1 && y <= 15) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 9 && y >= 0 && y <= 16) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 8 && y >= 1 && y <= 16) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 7 && y >= 2 && y <= 16) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -10 && y >= -1 && y <= 15) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -9 && y >= 0 && y <= 16) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -8 && y >= 1 && y <= 16) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -7 && y >= 2 && y <= 16) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 6 && y >= 3 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -6 && y >= 3 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -5 && y >= 3 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 5 && y >= 3 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -4 && y >= 3 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 4 && y >= 3 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -3 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 3 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -2 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 2 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -1 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 1 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 0 && y >= 3 && y <= 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                //END OF TOP FILLING
                






                //TOP FINAL TOUCHES
                else if (x >= -5 && x <= 5 && y == -7) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -5 && y <= -7 && y > -9) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 5 && y <= -7 && y > -9) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -8 && y <= -3 && y > -6) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -9 && y <= -4 && y > -5) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 8 && y <= -3 && y > -6) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 9 && y <= -4 && y > -5) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }




                ///INSIDE THE DOCKING AREA TOP
                else if (x >= -5 && x <= 5 && y == 2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -6 && y >= 1 && y <= 2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -7 && y >= 0 && y <= 1) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -8 && y >= -1 && y <= 0) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -9 && y >= -2 && y <= -1) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -10 && y >= -3 && y <= -2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 6 && y >= 1 && y <= 2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 7 && y >= 0 && y <= 1) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 8 && y >= -1 && y <= 0) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 9 && y >= -2 && y <= -1) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 10 && y >= -3 && y <= -2) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                //END OF DOCKING AREA





















                //CONTAINER AREA
                //else if (x >= 9 && x <= 13 && y >= -18 && y <= -17)
                //{
                //    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                //}

                //UPPER LEFT QUARTER CIRCLE
                else if (x == -18 && y >= -2 && y <= 6) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -17 && y >= 5 && y <= 8) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -16 && y >= 8 && y <= 10) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -15 && y >= 10 && y <= 12) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -14 && y >= 12 && y <= 13) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -13 && y >= 13 && y <= 14) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -12 && y >= 14 && y <= 15) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -11 && y >= 15 && y <= 16) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -10 && y >= 16 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= -9 && x <= -7 && y == 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= -7 && x <= -4 && y == 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= -4 && x <= 0 && y == 19) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                //UPPER RIGHT QUARTER CIRCLE
                else if (x == 18 && y >= -2 && y <= 6) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 17 && y >= 5 && y <= 8) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 16 && y >= 8 && y <= 10) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 15 && y >= 10 && y <= 12) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 14 && y >= 12 && y <= 13) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 13 && y >= 13 && y <= 14) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 12 && y >= 14 && y <= 15) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 11 && y >= 15 && y <= 16) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 10 && y >= 16 && y <= 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 9 && x >= 7 && y == 17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 7 && x >= 4 && y == 18) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 4 && x >= 1 && y == 19) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }













                    
                //BOTTOM FILLING
                else if (x >= -17 && x <= -11 && y <= -22 && y >= -28) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 17 && x >= 11 && y <= -22 && y >= -28) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -17 && y == -29) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 17 && y == -29) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 16 && y <= -29 && y >= -31) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 15 && y <= -29 && y >= -33) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 14 && y <= -29 && y >= -35) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 13 && y <= -29 && y >= -36) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 12 && y <= -29 && y >= -37) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 11 && y <= -29 && y >= -38) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }


                else if (x == -16 && y <= -29 && y >= -31) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -15 && y <= -29 && y >= -33) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -14 && y <= -29 && y >= -35) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -13 && y <= -29 && y >= -36) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -12 && y <= -29 && y >= -37) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -11 && y <= -29 && y >= -38) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 10 && y <= -23 && y >= -39) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 9 && y <= -24 && y >= -40) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 8 && y <= -25 && y >= -40) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 7 && y <= -26 && y >= -40) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -10 && y <= -23 && y >= -39) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -9 && y <= -24 && y >= -40) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -8 && y <= -25 && y >= -40) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -7 && y <= -26 && y >= -40) {

                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }


                else if (x == 6 && y <= -26 && y >= -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -6 && y <= -26 && y >= -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -5 && y <= -26 && y >= -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 5 && y <= -26 && y >= -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -4 && y <= -27 && y >= -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 4 && y <= -27 && y >= -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -3 && y <= -26 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 3 && y <= -26 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -2 && y <= -27 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 2 && y <= -27 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -1 && y <= -27 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 1 && y <= -27 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 0 && y <= -27 && y >= -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                //END OF BOTTOM FILLING
                














































                //BOTTOm FINAL TOUCHES
                else if (x >= -5 && x <= 5 && y == -17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -5 && y <= -16 && y > -17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 5 && y <= -16 && y > -17) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -8 && y <= -19 && y > -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -9 && y <= -20 && y > -21) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 8 && y <= -19 && y > -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 9 && y <= -20 && y > -21) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }







                ///INSIDE THE DOCKING AREA BOTTOM
                else if (x >= -5 && x <= 5 && y == -26) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -6 && y <= -25 && y >= -26) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -7 && y <= -24 && y >= -25) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -8 && y <= -23 && y >= -24) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -9 && y <= -22 && y >= -23) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -10 && y <= -21 && y >= -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }


                else if (x == 6 && y <= -25 && y >= -26) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 7 && y <= -24 && y >= -25) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 8 && y <= -23 && y >= -24) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 9 && y <= -22 && y >= -23) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 10 && y <= -21 && y >= -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                //END OF DOCKING AREA






                //BOTTOM LEFT QUARTER CIRCLE
                else if (x == -18 && y >= -30 && y <= -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -17 && y >= -32 && y <= -30) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -16 && y >= -34 && y <= -32) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -15 && y >= -35 && y <= -34) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == -14 && y >= -36 && y <= -35) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -13 && y >= -37 && y <= -36) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -12 && y >= -38 && y <= -37) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -11 && y >= -39 && y <= -38) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == -10 && y >= -40 && y <= -39) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x >= -9 && x <= -7 && y == -40) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= -7 && x <= -4 && y == -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x >= -4 && x <= 0 && y == -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }




                //BOTTOM RIGHT QUARTER CIRCLE
                else if (x == 18 && y >= -30 && y <= -22) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 17 && y >= -32 && y <= -30) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 16 && y >= -34 && y <= -32) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 15 && y >= -35 && y <= -34) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x == 14 && y >= -36 && y <= -35) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 13 && y >= -37 && y <= -36) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 12 && y >= -38 && y <= -37) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 11 && y >= -39 && y <= -38) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x == 10 && y >= -40 && y <= -39) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

                else if (x <= 9 && x >= 7 && y == -40) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 7 && x >= 4 && y == -41) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }
                else if (x <= 4 && x >= 1 && y == -42) {
                    arrayOfStationTiles[xx + (widthLeft + widthRight + 1) * yy] = 1;
                }

            }
        }




        var getSomeIndex = theBase.base_xml.substring(11, theBase.base_xml.length); //outpost_01_0
        var parsedAngle = parseInt(getSomeIndex);

        var someExtraData = [];

        for (var i = 0; i < arrayOfStationTiles.length; i++) {
            someExtraData[i] = arrayOfStationTiles[i];
        }

        var stationData = { baseID: theBase.id, sys_id: theBase.sys_id, coord: theBase.coord, xml_id: theBase.base_xml, widthL: widthLeft, widthR: widthRight, heightT: heightTop, heightB: heightBottom, grid: arrayOfStationTiles, rot: parsedAngle, visualTiles: someExtraData };

        storage.SetGlobal("station_tiles" + theBase.id, stationData);










        /*var xx = 0;
        var yy = 0;
        
        for (var x = -widthLeft; x <= widthRight; x++) {
            for (var y = -heightBottom; y <= heightTop; y++) {
                xx = x;
                yy = y;

                if (x < 0) {
                    xx *= -1;
                    xx += (widthRight);
                }

                if (y < 0) {
                    yy *= -1;
                    yy += (heightTop);
                }
                if (arrayOfStationTiles[xx + (widthLeft + widthRight +1) * yy] == 1) {
                    var coordsToSpawnX = (theBase.coord.x) + x;
                    var coordsToSpawnY = (theBase.coord.y) + y;

                    var currentPos = { x: coordsToSpawnX, y: coordsToSpawnY };


                    //var getSomeIndex = theBase.base_xml.substring(11, theBase.base_xml.length); //outpost_01_0
                    //var index = indexOfStuff(getSomeIndex);
                    var parsedAngle = 0;//parseInt(getSomeIndex);

                    var newPoint = RotatePoint(currentPos, theBase.coord, parsedAngle);
                    var id00 = generator.AddContainer(theBase.sys_id, newPoint.x, newPoint.y, arrayOfXMLScraps[0], "droplist_empty");
                }
            }
        }*/
    }
};

//https://stackoverflow.com/questions/13695317/rotate-a-point-around-another-point
function RotatePoint(pointToRotate, centerPoint, angleInDegrees) {
    var angleInRadians = angleInDegrees * (Math.PI / 180);
    var cosTheta = Math.cos(angleInRadians);
    var sinTheta = Math.sin(angleInRadians);

    var newX = (cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x);
    var newY = (sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y);

    var newPos = { x: newX, y: newY };

    return newPos;
}


function indexOfStuff(someIndex) {
    if (someIndex == "0") {
        return 0;
    }
    if (someIndex == "45") {
        return 1;
    }
    else if (someIndex == "90") {
        return 2;
    }
    else if (someIndex == "135") {
        return 3;
    }
    else if (someIndex == "180") {
        return 4;
    }
    else if (someIndex == "225") {
        return 5;
    }
    else if (someIndex == "270") {
        return 6;
    }
    else if (someIndex == "315") {
        return 7;
    }
}

//var id1 = generator.AddSpecialObject(args.sys_id, args.bases[0].coord.x - 3.5, args.bases[0].coord.y - 23, "station_platform_refuel", 0);
//var id2 = generator.AddSpecialObject(args.sys_id, args.bases[0].coord.x + 3, args.bases[0].coord.y - 23, "station_platform_repair", 0);

//var idOfBase = args.bases[0].id;

//var id3 = generator.AddNPCShipToSystem("drone repair", "ai_repair_high", 1, "xml_repair_low", args.sys_id, args.bases[0].coord.x + 5.15, args.bases[0].coord.y - 21.5, { class: "stationDialog", someTag: "drone_repair", greeting: "terminal", stationID: idOfBase }); //, unique_id: "stationDialog"
//var id6 = generator.AddNPCShipToSystem("drone retriever", "ai_retriever_drone", 1, "xml_drone_retriever", args.sys_id, args.bases[0].coord.x + 13, args.bases[0].coord.y - 10, { class: "stationDialog", someTag: "drone_retriever", greeting: "terminal", stationID: idOfBase }); //, unique_id: "stationDialog"

