<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReserveInfoList.aspx.cs" Inherits="System.ReserveInfoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>客房预订管理系统</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <%--<link href="Content/css/demo.css" rel="stylesheet" type="text/css" />--%>

    <script src="Content/scripts/boot.js" type="text/javascript"></script>
    <%--  <script src="Content/js/ColumnsMenu.js" type="text/javascript"></script>--%>
</head>
<body>
    <h1>预订信息</h1>

    <div style="width: 1100px;">
        <div class="mini-toolbar" style="border-bottom: 0; padding: 0px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <a class="mini-button" iconcls="icon-add" onclick="add()">增加</a>
                        <a class="mini-button" iconcls="icon-remove" onclick="remove()">删除</a>
                    </td>
                  
                </tr>
            </table>
        </div>
    </div>
    <div id="datagrid1" class="mini-datagrid" style="width: 1100px; height: 350px;" allowresize="true"
        url="ReserveInfoService.ashx?action=SearchAllReserveInfo" idfield="id" multiselect="true">
        <div property="columns">
            <div type="indexcolumn"></div>
            <div type="checkcolumn"></div>
            <div field="ReserveNo" width="120" headeralign="center">预订编号</div>
            <div field="ReserveName" width="120" headeralign="center" renderer="">客房名称</div>
            <div field="ReservePerson" width="120" headeralign="center" renderer="">预定人</div>
            <div field="ReservePhone" width="120" headeralign="center">联系方式</div>
            <div field="ReserveNum" width="120" headeralign="center" allowsort="true">预定天数</div>
            <div field="StartTime" width="120" headeralign="center" allowsort="true" renderer="onTimeRenderer">入住时间</div>
            <div field="EndTime" width="120" headeralign="center" renderer="onTimeRenderer">离开时间</div>
            <div field="ReserveCost" width="120" headeralign="center">房费</div>

        </div>
    </div>


    <script type="text/javascript">
        mini.parse();

        var grid = mini.get("datagrid1");
        grid.load();


        function add() {

            mini.open({
                targetWindow: window,

                url: "AddReserveInfo.aspx",
                title: "新增预订信息", width: 600, height: 400,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { action: "added" };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    grid.reload();
                }
            });
        }
        function remove() {

            var rows = grid.getSelecteds();
            if (rows.length > 0) {
                if (confirm("确定删除选中记录？")) {
                    var ids = [];
                    for (var i = 0, l = rows.length; i < l; i++) {
                        var r = rows[i];
                        ids.push(r.ReserveNo);
                    }
                    var id = ids.join(',');
                    grid.loading("操作中，请稍后......");
                    $.ajax({
                        url: "ReserveInfoService.ashx?action=RemoveReserveInfo&ReserveNo=" + id,
                        success: function (text) {
                            mini.alert("提交成功！" + text);
                            grid.reload();
                        },
                        error: function () {
                        }
                    });
                }
            } else {
                alert("请选中一条记录");
            }
        }
        function onTimeRenderer(e) {
            var value = e.value;
            if (value) return mini.formatDate(value, 'yyyy-MM-dd');
        }
        function onHoldNumRenderer(e) {
            if (e.value == 2) {
                return "二人桌";
            }
            else if (e.value ==4) {
                return "四人桌";
            }
            else if (e.value == 6) {
                return "六人桌"
            }

        }

        function search() {
            var SearchHoldNum = mini.getByName("SearchHoldNum").getValue();
            var SearchIsUse = mini.getByName("SearchIsUse").getValue();
            grid.load({ SearchHoldNum: SearchHoldNum, SearchIsUse: SearchIsUse });
        }
    </script>
</body>
</html>