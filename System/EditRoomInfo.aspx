<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRoomInfo.aspx.cs" Inherits="System.EditRoomInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>餐桌信息列表</title>
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
        <input name="Status" class="mini-hidden" value="edit" />
        <div style="padding-left: 11px; padding-bottom: 5px; margin-left: 20px;">
            <table style="table-layout: fixed;">
                <tr>
                    <td style="width: 80px;">客房编号：</td>
                    <td style="width: 150px;">
                        <input name="RoomId" class="mini-textbox" required="true" emptytext="请输入客房编号" readonly="readonly"/>
                    </td>
                    <td style="width: 80px;">客房名称：</td>
                    <td style="width: 150px;">
                        <input name="RoomName" class="mini-textbox" required="true" emptytext="请输入客房名称" />
                    </td>

                </tr>
                <tr>
                    <td>推荐指数：</td>
                    <td>
                        <select name="RoomRecommend" class="mini-radiobuttonlist" onvaluechanged="onRoomRecommandChanged" required="true">
                            <option value="0" >三颗星</option>
                            <option value="1">四颗星</option>
                            <option value="2">五颗星</option>
                        </select>
                    </td>
                    <td style="width: 80px;">客房类别：</td>
                    <td style="width: 150px;">
                        <input name="RoomType" class="mini-combobox" valuefield="id" textfield="text"
                            url="Data/roomtype.Json"
                            onvaluechanged="" required="true" 
                            emptytext="请选择客房类别" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">每日费用：</td>
                    <td style="width: 150px;">
                        <input name="RoomCost" class="mini-textbox" required="true" emptytext="请输入每日费用" />
                    </td>
                    <td style="width: 80px;">客房数量：</td>
                    <td style="width: 150px;">
                        <input name="RoomCount" class="mini-textbox" required="true" emptytext="请输入客房数量" />
                    </td>
                </tr>
                <tr>
                    <td>客房简介：</td>
                    <td colspan="3">
                        <input name="RoomSurplus" class="mini-textarea" style="width: 386px;" required="true" readonly="readonly"/>
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

        ////////////////////
        //标准方法接口定义
        function SetData(data) {
            if (data.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用
                data = mini.clone(data);

                $.ajax({
                    url: "RoomInfoService.ashx?action=GetRoomInfo&RoomId=" + data.id,
                    cache: false,
                    success: function (text) {
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);
                        mini.getByName("Status").setValue("edit");
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
                url: "RoomInfoService.ashx?action=SaveTableInfo",
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
        function onRoomRecommandChanged(e) {
            if (e.value == 0) {
                mini.getByName("RoomRecommand").setValue(0);
            } else if (e.value == 1) {
                mini.getByName("RoomRecommand").setValue(1);
            } else if (e.value == 2) {
                mini.getByName("RoomRecommand").setValue(2);
            }
        }


    </script>
</body>
</html>
