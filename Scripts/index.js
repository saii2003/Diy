var app = angular.module('pcApp', []);

app.controller('pcControl', function ($scope) {
    $scope.data = {
        multipleSelect: []

    };


    //總計
    $scope.total = function () {

        var total = 0; //合計價格
        $scope.show_image = false;


        //單一商品總價
        var cpu_total = 0, mainboard_total = 0, memory_total = 0, hardware_total = 0, videocard_total = 0, power_total = 0, cases_total = 0, dvd_total = 0, monitor_total = 0;
        var src = "";

        //cpu價格
        if ($scope.cpu != "" && $scope.cpu != null) {
            cpu_total = $scope.cpu.split(',')[1] * $scope.cpu_amount; //小計
            $scope.cpu_price = $scope.cpu.split(',')[1]; //單價
            $scope.show_cpuimg = true;
            $scope.cpu_src = $scope.cpu.split('【')[0].trim() + ".jpg";
        }
        else {
            $scope.cpu_price = null;
            $scope.show_cpuimg = false;
        }

        //主機板總價
        if ($scope.mainboard != "" && $scope.mainboard != null) {
            mainboard_total = $scope.mainboard.split(',')[1] * $scope.mainboard_amount;
            $scope.mainboard_price = $scope.mainboard.split(',')[1];
            $scope.show_mainboardimg = true;
            $scope.mainboard_src = $scope.mainboard.split(',')[0].trim() + ".png";

        } else {
            $scope.mainboard_price = null;
            $scope.show_mainboardimg = false;
        }

        //記憶體總價
        if ($scope.memory != "" && $scope.memory != null) {
            memory_total = $scope.memory.split(',')[1] * $scope.memory_amount;
            $scope.memory_price = $scope.memory.split(',')[1];
            $scope.show_momeryimg = true;
            $scope.memory_src = $scope.memory.split(',')[0].trim() + ".jpg";

        } else {
            $scope.memory_price = null;
            $scope.show_momeryimg = false;
        }


        //硬碟總價
        if ($scope.hardware != "" && $scope.hardware != null) {
            hardware_total = $scope.hardware.split(',')[1] * $scope.hardware_amount;
            $scope.hardware_price = $scope.hardware.split(',')[1];
            $scope.show_hardwareimg = true;
            $scope.hardware_src = $scope.hardware.split(',')[0].trim() + ".jpg";
        } else {
            $scope.hardware_price = null;
            $scope.show_hardwareimg = false;
        }

        //顯示卡總價
        if ($scope.videocard != "" && $scope.videocard != null) {
            videocard_total = $scope.videocard.split(',')[1] * $scope.videocard_amount;
            $scope.videocard_price = $scope.videocard.split(',')[1];
            $scope.show_videocardimg = true;
            $scope.videocard_src = $scope.videocard.split(',')[0].trim() + ".png";

        } else {
            $scope.videocard_price = null;
            $scope.show_videocardimg = false;
        }

        //電源總價
        if ($scope.power != "" && $scope.power != null) {
            power_total = $scope.power.split(',')[1] * $scope.power_amount;
            $scope.power_price = $scope.power.split(',')[1];
            $scope.show_powerimg = true;
            $scope.power_src = $scope.power.split(',')[0].trim() + ".jpg";
        }
        else {
            $scope.power_price = null;
            $scope.show_powerimg = false;
        }

        //機殼總價
        if ($scope.cases != "" && $scope.cases != null) {
            cases_total = $scope.cases.split(',')[1] * $scope.cases_amount;
            $scope.cases_price = $scope.cases.split(',')[1];
            $scope.show_casesimg = true;
            $scope.cases_src = $scope.cases.split(',')[0].trim() + ".jpg";

        } else {
            $scope.cases_price = null;
            $scope.show_casesimg = false;
        }

        //光碟機總價
        if ($scope.dvd != "" && $scope.dvd != null) {
            dvd_total = $scope.dvd.split(',')[1] * $scope.dvd_amount;
            $scope.dvd_price = $scope.dvd.split(',')[1] * 1;
            $scope.show_dvdimg = true;
            $scope.dvd_src = $scope.dvd.split(',')[0].trim() + ".jpg";
        } else {
            $scope.dvd_price = null;
            $scope.show_dvdimg = false;
        }

        //螢幕總價
        if ($scope.monitor != "" && $scope.monitor != null) {
            monitor_total = $scope.monitor.split(',')[1] * $scope.monitor_amount;
            $scope.monitor_price = $scope.monitor.split(',')[1] * 1;
            $scope.show_monitorimg = true;
            $scope.monitor_src = $scope.monitor.split(',')[0].trim() + ".jpg";
        } else {
            $scope.monitor_price = null;
            $scope.show_monitorimg = false;
        }

        //合計
        total += cpu_total + mainboard_total + memory_total + hardware_total + videocard_total + power_total + cases_total + dvd_total + monitor_total;
        return total;

    };
    //耗電量
    $scope.powers = function () {
        var power_total = 0;
        var cpu = 0, mainboard = 0, memory = 0, hardware = 0, videocard = 0, dvd = 0;
        if ($scope.cpu != "" && $scope.cpu != null) {
            cpu = parseInt($scope.cpu.split('/')[1].split(',')[0].slice(0, -1));
        }
        if ($scope.mainboard != null && $scope.mainboard != "") {
            mainboard = 50;
        }
        if ($scope.mainboard != null && $scope.mainboard != "") {
            mainboard = 50;
        }
        if ($scope.memory != null && $scope.memory != "") {
            memory = 4;
        }
        if ($scope.hardware != null && $scope.hardware != "") {
            hardware = 20;
        }
        if ($scope.videocard != null && $scope.videocard != "") {
            videocard = parseInt($scope.videocard.split('/')[1].split(',')[0].slice(0, -1));
        }
        if ($scope.dvd != null && $scope.dvd != "") {
            dvd = 25;
        }

        power_total += cpu + mainboard + memory + hardware + videocard + dvd;
        return power_total;

    };
    //印估價單
    $scope.print = function () {

        var total = 0;
        var cpu = "", mainboard = "", memory = "", hardware = "", videocard = "", power = "", cases = "", dvd = "", monitor = "";
        var cpu_amount = 0, mainboard_amount = 0, memory_amount = 0, hardware_amount = 0, videocard_amount = 0, power_amount = 0, cases_amount = 0, dvd_amount = 0, monitor_amount = 0;
        var cpu_price = 0, mainboard_price = 0, memory_price = 0, hardware_price = 0, videocard_price = 0, power_price = 0, case_price = 0, dvd_price = 0, monitor_price = 0;
        var cpu_total = 0, mainboard_total = 0, memory_total = 0, hardware_total = 0, videocard_total = 0, power_total = 0, cases_total = 0, dvd_total = 0, monitor_total = 0;
        var product = new Array();

        if (($scope.cpu != null && $scope.cpu != "") || ($scope.mainboard != null && $scope.mainboard != "") || ($scope.memory != null && $scope.memory != "") || ($scope.hardware != null && $scope.hardware != "") || ($scope.videocard != null && $scope.videocard != "") || ($scope.power != null && $scope.power != "") || ($scope.cases != null && $scope.cases != "") || ($scope.dvd != null && $scope.dvd != "") || ($scope.monitor != null && $scope.monitor != "")) {
            if ($scope.cpu != null && $scope.cpu != "") {
                cpu = $scope.cpu.split(',')[0];
                cpu_amount = $scope.cpu_amount;
                cpu_price = $scope.cpu.split(',')[1];
                cpu_total = cpu_price * cpu_amount;
                product.push(cpu + "," + cpu_amount + "," + cpu_price + "," + cpu_total);
            }

            if ($scope.mainboard != null && $scope.mainboard != "") {
                mainboard = $scope.mainboard.split(',')[0];
                mainboard_amount = $scope.mainboard_amount;
                mainboard_price = $scope.mainboard.split(',')[1];
                mainboard_total = mainboard_price * mainboard_amount;
                product.push(mainboard + "," + mainboard_amount + "," + mainboard_price + "," + mainboard_total);
            }
            if ($scope.memory != null && $scope.memory != "") {
                memory = $scope.memory.split(',')[0];
                memory_amount = $scope.memory_amount;
                memory_price = $scope.memory.split(',')[1];
                memory_total = memory_price * memory_amount;
                product.push(memory + "," + memory_amount + "," + memory_price + "," + memory_total);
            }

            if ($scope.hardware != null && $scope.hardware != "") {
                hardware = $scope.hardware.split(',')[0];
                hardware_amount = $scope.hardware_amount;
                hardware_price = $scope.hardware.split(',')[1];
                hardware_total = hardware_price * hardware_amount;
                product.push(hardware + "," + hardware_amount + "," + hardware_price + "," + hardware_total);
            }

            if ($scope.videocard != null && $scope.videocard != "") {
                videocard = $scope.videocard.split(',')[0];
                videocard_amount = $scope.videocard_amount;
                videocard_price = $scope.videocard.split(',')[1];
                videocard_total = videocard_price * videocard_amount;
                product.push(videocard + "," + videocard_amount + "," + videocard_price + "," + videocard_total);
            }

            if ($scope.power != null && $scope.power != "") {
                power = $scope.power.split(',')[0];
                power_amount = $scope.power_amount;
                power_price = $scope.power.split(',')[1];
                power_total = power_price * power_amount;
                product.push(power + "," + power_amount + "," + power_price + "," + power_total);
            }
            if ($scope.cases != null && $scope.cases != "") {
                cases = $scope.cases.split(',')[0];
                cases_amount = $scope.cases_amount;
                cases_price = $scope.cases.split(',')[1];
                cases_total = cases_price * cases_amount;
                product.push(cases + "," + cases_amount + "," + cases_price + "," + cases_total);
            }

            if ($scope.dvd != null && $scope.dvd != "") {
                dvd = $scope.dvd.split(',')[0];
                dvd_amount = $scope.dvd_amount;
                dvd_price = $scope.dvd.split(',')[1];
                dvd_total = dvd_price * dvd_amount;
                product.push(dvd + "," + dvd_amount + "," + dvd_price + "," + dvd_total);
            }

            if ($scope.monitor != null && $scope.monitor != "") {
                monitor = $scope.monitor.split(',')[0];
                monitor_amount = $scope.monitor_amount;
                monitor_price = $scope.monitor.split(',')[1];
                monitor_total = monitor_price * monitor_amount;
                product.push(monitor + "," + monitor_amount + "," + monitor_price + "," + monitor_total);
            }


            total += cpu_total + mainboard_total + memory_total + hardware_total + videocard_total + power_total + cases_total + dvd_total + monitor_total;


            var data = "";
            var id = 1;
            for (var i = 0; i < product.length; i++) {
                data += "<tr>" +
                            "<td>" + id + "</td>" +
                            "<td>" + product[i].split(',')[0] + "</td>" +
                            "<td>" + product[i].split(',')[1] + "</td>" +
                            "<td>" + product[i].split(',')[2] + "</td>" +
                            "<td>" + product[i].split(',')[3] + "</td>" +
                            "</tr>";
                id++;

            }

            var table = "<table class=\"table table-bordered text-center\">" +

                               "<tr>" +
                               "<th class=\"text-center\"></th>" +
                               "<th class=\"text-center\">商品名稱</th>" +
                               "<th class=\"text-center\">數量</th>" +
                               "<th class=\"text-center\">單價</th>" +
                               "<th class=\"text-center\">合計</th>" +
                               "</tr>" +
                                    data +
                               "<tr>" +
                               "<td style=\"border:1px solid white;\"></td>" +
                               "<td style=\"border:1px solid white;\"></td>" +
                               "<td style=\"border:1px solid white;\"></td>" +
                               "<td style=\"border:1px solid white;\">總計</td>" +
                               "<td style=\"border:1px solid white;\">" + total + "</td>" +
                               "</tr>" +
                               "</table>";



            var print = window.open();
            print.document.open();
            print.document.write("<html><head><link href=\"../../Content/css/bootstrap.min.css\" rel=\"Stylesheet\" /></head><body onload='window.print();window.close()'><div class=\"text-center\"><h3>估價單</h3></div>");
            print.document.write("<div id=\"printdata\">" + table + "</div>");
            print.document.close("</body></html>");
        } else {
            alert("請選擇商品及數量");
        }

    };

    //重新估價
    $scope.reset = function reset() {
        $scope.cpu_amount = 1;
        $scope.mainboard_amount = 1;
        $scope.memory_amount = 1;
        $scope.hardware_amount = 1;
        $scope.videocard_amount = 1;
        $scope.power_amount = 1;
        $scope.cases_amount = 1;
        $scope.dvd_amount = 1;
        $scope.monitor_amount = 1;

        $scope.cpu = "";
        $scope.mainboard = "";
        $scope.memory = "";
        $scope.hardware = "";
        $scope.videocard = "";
        $scope.power = "";
        $scope.cases = "";
        $scope.dvd = "";
        $scope.monitor = "";
    }

    //商品加入購物車
    $scope.addtoCart = function () {
        var total = 0;
        var cpu = "", mainboard = "", memory = "", hardware = "", videocard = "", power = "", cases = "", dvd = "", monitor = "";
        var cpu_amount = 0, mainboard_amount = 0, memory_amount = 0, hardware_amount = 0, videocard_amount = 0, power_amount = 0, cases_amount = 0, dvd_amount = 0, monitor_amount = 0;
        var cpu_price = 0, mainboard_price = 0, memory_price = 0, hardware_price = 0, videocard_price = 0, power_price = 0, cases_price = 0, dvd_price = 0, monitor_price = 0;

        var product = new Array();

        if (($scope.cpu != null && $scope.cpu != "") || ($scope.mainboard != null && $scope.mainboard != "") || ($scope.memory != null && $scope.memory != "") || ($scope.hardware != null && $scope.hardware != "") || ($scope.videocard != null && $scope.videocard != "") || ($scope.power != null && $scope.power != "") || ($scope.cases != null && $scope.cases != "") || ($scope.dvd != null && $scope.dvd != "") || ($scope.monitor != null && $scope.monitor != "")) {
            if ($scope.cpu != null && $scope.cpu != "") {
                cpu = $scope.cpu.split(',')[0];
                cpu_amount = $scope.cpu_amount;
                cpu_price = $scope.cpu.split(',')[1];

                product.push("cpu=" + encodeURI(cpu) + "&cpu_amount=" + cpu_amount + "&cpu_price=" + cpu_price);
            }
            if ($scope.mainboard != null && $scope.mainboard != "") {
                mainboard = $scope.mainboard.split(',')[0];
                mainboard_amount = $scope.mainboard_amount;
                mainboard_price = $scope.mainboard.split(',')[1];

                product.push("mainboard=" + mainboard + "&mainboard_amount=" + mainboard_amount + "&mainboard_price=" + mainboard_price);
            }
            if ($scope.memory != null && $scope.memory != "") {
                memory = $scope.memory.split(',')[0];
                memory_amount = $scope.memory_amount;
                memory_price = $scope.memory.split(',')[1];

                product.push("memory=" + memory + "&memory_amount=" + memory_amount + "&memory_price=" + memory_price);
            }

            if ($scope.hardware != null && $scope.hardware != "") {
                hardware = $scope.hardware.split(',')[0];
                hardware_amount = $scope.hardware_amount;
                hardware_price = $scope.hardware.split(',')[1];

                product.push("hardware=" + hardware + "&hardware_amount=" + hardware_amount + "&hardware_price=" + hardware_price);
            }

            if ($scope.videocard != null && $scope.videocard != "") {
                videocard = $scope.videocard.split(',')[0];
                videocard_amount = $scope.videocard_amount;
                videocard_price = $scope.videocard.split(',')[1];

                product.push("videocard=" + videocard + "&videocard_amount=" + videocard_amount + "&videocard_price=" + videocard_price);
            }

            if ($scope.power != null && $scope.power != "") {
                power = $scope.power.split(',')[0];
                power_amount = $scope.power_amount;
                power_price = $scope.power.split(',')[1];

                product.push("power=" + power + "&power_amount=" + power_amount + "&power_price=" + power_price);
            }

            if ($scope.cases != null && $scope.cases != "") {
                cases = $scope.cases.split(',')[0];
                cases_amount = $scope.cases_amount;
                cases_price = $scope.cases.split(',')[1];

                product.push("cases=" + cases + "&cases_amount=" + cases_amount + "&cases_price=" + cases_price);
            }

            if ($scope.dvd != null && $scope.dvd != "") {
                dvd = $scope.dvd.split(',')[0];
                dvd_amount = $scope.dvd_amount;
                dvd_price = $scope.dvd.split(',')[1];

                product.push("dvd=" + dvd + "&dvd_amount=" + dvd_amount + "&dvd_price=" + dvd_price);
            }

            if ($scope.monitor != null && $scope.monitor != "") {
                monitor = $scope.monitor.split(',')[0];
                monitor_amount = $scope.monitor_amount;
                monitor_price = $scope.monitor.split(',')[1];
                monitor_total = monitor_price * monitor_amount;
                product.push("monitor=" + monitor + "&monitor_amount=" + monitor_amount + "&monitor_price=" + monitor_price);
            }
        }
        var urlparem = "";
        for (var i = 0; i < product.length; i++) {
            if (i != product.length - 1) {
                urlparem += product[i] + "&";
            }
            else {
                urlparem += product[i];
            }
        }
        location.href = "http://" + location.host + "/Pc/addtoCart?" + urlparem;
    }
});


//清除購物車所有商品
function clearCart() {
    $.ajax({
        type: 'POST',
        url: "http://" + location.host + "/Pc/clearCart",
        data: {}
    }).done(function (msg) {
        $('div#cart').html(msg);
    });
}
//清除購物單一商品
function removeCartItem(pname) {
    $.ajax({
        type: 'POST',
        url: "http://" + location.host + "/Pc/removeCartItem",
        data: { name: pname }
    }).done(function (msg) {
        $('div#cart').html(msg);
    });
}