if (typeof $ != "undefined") {
    $(document).ready(function () {
        $(".panel-heading").unbind('click').click(function () {
            $(this).next(".panel-body").toggle("200");
        })

        //显示气泡
        //$("[data-toggle='popover']").popover({
        //    trigger: "hover"//使用click切换成点击显示
        //});
        $("[data-toggle='popover']").popover('destroy');

        try {
            //初始化日期控件
            if ($(".datepicker").length > 0) {
                $(".datepicker").datepicker({
                    language: "zh-CN",
                    autoclose: true,//选中之后自动隐藏日期选择框
                    clearBtn: true,//清除按钮
                    todayBtn: true,//今日按钮
                    format: "yyyy-mm-dd"//日期格式
                });
            }

        } catch (e) {
            //避免影响后面的js加载
        }
    });
}