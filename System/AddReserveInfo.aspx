<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReserveInfo.aspx.cs" Inherits="System.AddReserveInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>预订信息列表</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />

    <script src="Content/scripts/boot.js" type="text/javascript"></script>


    <style type="text/css">
        html, body {
            padding: 0;
            margin: 0;
            border: 0;
            height: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" method="post">
        <input name="Status" class="mini-hidden" value="added" />
        <div style="padding-left: 11px; padding-bottom: 5px; margin-left: 20px;">
            <table style="table-layout: fixed;">
                <tr>
                    <td style="width: 80px;">预订编号：</td>
                    <td style="width: 150px;">
                        <input name="ReserveNo" class="mini-textbox" required="true" emptytext="预订编号" readonly="readonly"/>
                    </td>
                    <td style="width: 80px;">客房名称：</td>
                    <td style="width: 150px;">
                        <input name="ReserveName" class="mini-combobox" valuefield="RoomName" textfield="RoomName"
                            url="ReserveInfoService.ashx?action=ShowRoomName"
                            onvaluechanged="" required="true"
                            emptytext="请选择客房名称" />
                    </td>

                </tr>
                <tr>
                    <td>预定人：</td>
                    <td>
                        <input name="ReservePerson" class="mini-textbox" required="true" emptytext="请输入预定人" />
                    </td>
                    <td style="width: 80px;">联系方式：</td>
                    <td style="width: 150px;">
                        <input name="ReservePhone" class="mini-textbox" required="true" emptytext="请输入联系方式" vtype="rangeLength:11,11"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">入住日期：</td>
                    <td style="width: 150px;">
                        <input name="StartTime" class="mini-datepicker" required="true" emptytext="请选择入住日期" renderer="onTimeRenderer"  format="yyyy-MM-dd HH:mm:ss" showtime="true" minDate="<%=DateTime.Now.AddDays(-1) %>"/>

                    </td>
                    <td style="width: 80px;">离开日期：</td>
                    <td style="width: 150px;">
                        <input name="EndTime" class="mini-datepicker" required="true" emptytext="请选择离开日期" renderer="onTimeRenderer"  format="yyyy-MM-dd HH:mm:ss" showtime="true" onvaluechanged="onEndTimeChanged"/>

                    </td>
                </tr>
            </table>
        </div>

        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
            <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
        </div>
    </form>
    <script type="text/javascript">
        mini.parse();


        var form = new mini.Form("form1");
        $.ajax({
            url: "ReserveInfoService.ashx?action=ShowReserveNo",
            type: "post",
            success: function (text) {
                mini.getbyName("ReserveNo").setValue(text);

            }
        });
        ////////////////////
        //标准方法接口定义
        function SetData(data) {
            if (data.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用
                data = mini.clone(data);

                $.ajax({
                    url: "AjaxService.aspx?method=GetBook&id=" + data.id,
                    cache: false,
                    success: function (text) {
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);
                    }
                });
            }
        }

        function GetData() {
            var o = form.getData();
            return o;
        }
        function CloseWindow(action) {
            if (action == "close" && form.isChanged()) {
                if (mini.confirm("数据被修改了，是否先保存？")) {
                    return false;
                }
            }
            if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
            else window.close();
        }
        function onOk(e) {
            var form = new mini.Form("form1");
            var data = form.getData();
            var json = mini.encode(data);
            $.ajax({
                url: "ReserveInfoService.ashx?action=SaveReserveInfo",
                type: "post",
                data: { data: json },
                success: function (text) {
                    if (mini.confirm("提交成功，返回结果" + text)) {
                        CloseWindow("save");
                    }
                    else {
                        return false;
                    }

                }
            });
        }
        function onCancel(e) {
            CloseWindow("cancel");
        }
        function onEndTimeChanged() {
            var startTime = mini.getbyName("StartTime").getValue();
            var endTime = mini.getbyName("EndTime").getValue();
            if (startTime >= endTime) {
                var endTime = mini.getbyName("EndTime").setValue("");
                mini.alert("输入错误！离开时间不能在入住时间之前！");
            }
        }



    </script>
</body>
</html>

