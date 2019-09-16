<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomInfoList.aspx.cs" Inherits="System.RoomInfoList" %>

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
    <h1>客房信息</h1>

    <div style="width: 1100px;">
        <div class="mini-toolbar" style="border-bottom: 0; padding: 0px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <a class="mini-button" iconcls="icon-add" onclick="add()">增加</a>
                        <a class="mini-button" iconcls="icon-remove" onclick="remove()">删除</a>
                    </td>
                    <td style="white-space: nowrap;">
                        <input name="SearchRoomName" class="mini-textbox" valuefield="id" textfield="name" url="" emptytext="请输入客房名称" />
                        <input name="SearchRoomType" class="mini-combobox" valuefield="id" textfield="text"
                            url="Data/roomtype.Json"
                            onvaluechanged="" required="true"
                            emptytext="请选择客房类别" />
                        <a class="mini-button" onclick="search()">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="datagrid1" class="mini-datagrid" style="width: 1100px; height: 350px;" allowresize="true"
        url="RoomInfoService.ashx?action=SearchAllRoomInfo" idfield="id" multiselect="true">
        <div property="columns">
            <div type="indexcolumn"></div>
            <div type="checkcolumn"></div>
            <div field="RoomId" width="120" headeralign="center">客房编号</div>
            <div field="RoomName" width="120" headeralign="center">客房名称</div>
            <div field="RoomRecommend" width="120" headeralign="center" renderer="onRoomRecommendRenderer">推荐指数</div>
            <div field="RoomType" width="120" headeralign="center" renderer="onRoomTypeRenderer">客房类别</div>
            <div field="RoomCost" width="120" headeralign="center">每日费用</div>
            <div field="RoomCount" width="120" headeralign="center">客房数量</div>
            <div field="RoomSurplus" width="120" headeralign="center" renderer="onRoomSurplusRenderer">剩余数量</div>
            <div field="Operation" width="120" headeralign="center" renderer="onActionRenderer">操作</div>

        </div>
    </div>


    <script type="text/javascript">
        mini.parse();

        var grid = mini.get("datagrid1");
        grid.load();


        function add() {

            mini.open({
                targetWindow: window,

                url: "AddRoomInfo.aspx",
                title: "新增客房", width: 600, height: 400,
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

        function edit() {

            var row = grid.getSelected();
            if (row) {
                mini.open({
                    targetWindow: window,
                    url: "EditRoomInfo.aspx",
                    title: "编辑客房信息", width: 600, height: 400,
                    onload: function () {
                        var iframe = this.getIFrameEl();
                        var data = { action: "edit", id: row.RoomId };
                        iframe.contentWindow.SetData(data);
                    },
                    ondestroy: function (action) {
                        //var iframe = this.getIFrameEl();
                        grid.reload();
                    }
                });

            } else {
                alert("请选中一条记录");
            }

        }
        function remove() {

            var rows = grid.getSelecteds();
            if (rows.length > 0) {
                if (confirm("确定删除选中记录？")) {
                    var ids = [];
                    for (var i = 0, l = rows.length; i < l; i++) {
                        var r = rows[i];
                        ids.push(r.RoomId);
                    }
                    var id = ids.join(',');
                    grid.loading("操作中，请稍后......");
                    $.ajax({
                        url: "RoomInfoService.ashx?action=RemoveRoomInfo&RoomId=" + id,
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
        function onRoomTypeRenderer(e) {
            if (e.value == 0) {
                return "豪华单人间";
            } else if (e.value == 1) {
                return "标准双人间";
            } else if (e.value == 2) {
                return "标准三人间";
            }
        }
        function onRoomRecommendRenderer(e) {
            if (e.value == 0) {
                return "三颗星";
            }
            else if (e.value == 1) {
                return "四颗星";
            }
            else if (e.value == 2) {
                return "五颗星"
            }

        }
        function onRoomSurplusRenderer(e) {
            if (e.row.RoomCount == e.row.RoomSurplus) {
                e.cellStyle = 'background:yellow';
                return e.value;
            } else {
                return e.value;
            }
        }
        function search() {
            var SearchRoomName = mini.getByName("SearchRoomName").getValue();
            var SearchRoomType = mini.getByName("SearchRoomType").getValue()-1;
            grid.load({ SearchRoomName: SearchRoomName, SearchRoomType: SearchRoomType });
        }
        function onActionRenderer(e) {
            var grid = e.sender;
            var record = e.record;
            var uid = record._uid;
            var rowIndex = e.rowIndex;

            var s = '<a class="Edit_Button" href="javascript:edit(\'' + uid + '\')">修改</a> '
                +'<a class="Delete_Button" href="javascript:remove(\'' + uid + '\')">删除</a> ';
            return s;
        }
    </script>
</body>
</html>
