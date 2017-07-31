//-----------------BootstrapTable操作系列-----------------
var m_pagerow = 0;

//标准初始化BootstrapTable
function InitTable(tb, actionUrl, dbQueryParams, tbColumns) {
    $(tb).bootstrapTable({
        url: actionUrl,         //请求后台的URL（*）
        method: 'get',                      //请求方式（*）
        toolbar: '#toolbar',                //工具按钮用哪个容器
        striped: true,                      //是否显示行间隔色
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                   //是否显示分页（*）
        sortable: false,                     //是否启用排序
        sortOrder: "asc",                   //排序方式
        queryParams: dbQueryParams,//传递参数（*）
        sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: 10,                       //每页的记录行数（*）
        pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
        search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
        strictSearch: true,
        showColumns: true,                  //是否显示所有的列
        showRefresh: true,                  //是否显示刷新按钮
        //minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: true,                //是否启用点击选中行
        height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
        showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
        cardView: false,                    //是否显示详细视图
        detailView: false,                   //是否显示父子表
        columns: tbColumns,
        onPageChange: function (number, size) {
            m_pagerow = (number - 1) * size;
        }
    });
};

//自定义初始化BootstrapTable
function LvInitTable(tb, tbColumns, actionUrl, actionQueryParams, detailFormatterFun, methodName,
    toolbarName, uniqueIdName, sidePaginationType, pageSizeNumber, isCache, isSingleSelect,
    isShowColumns, isShowRefresh, isClickToSelect, isDetailView) {
    $(tb).bootstrapTable({
        url: actionUrl,                             //请求后台的URL（*）
        method: methodName,                             //请求方式（*）
        columns: tbColumns,
        toolbar: toolbarName,                      //工具按钮用哪个容器
        striped: true,                               //是否显示行间隔色
        cache: isCache,                           //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                          //是否显示分页（*）
        sortable: false,                              //是否启用排序
        sortOrder: "asc",                           //排序方式
        queryParamsType: '',
        queryParams: actionQueryParams,//传递参数（*）
        sidePagination: sidePaginationType,           //分页方式：client客户端分页，server服务端分页（*）
        detailFormatter: detailFormatterFun,
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: pageSizeNumber,       //每页的记录行数（*）
        pageList: [5, 10, 25],         //可供选择的每页的行数（*）
        search: false,                             //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
        showColumns: isShowColumns,                  //是否显示所有的列
        showRefresh: isShowRefresh,                  //是否显示刷新按钮
        minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: isClickToSelect,                //是否启用点击选中行
        singleSelect: isSingleSelect,
        //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: uniqueIdName,                     //每一行的唯一标识，一般为主键列
        cardView: false,                    //是否显示详细视图
        detailView: isDetailView,                   //是否显示父子表
        onClickRow: function (e, row, $element) {
            $('.success').removeClass('success');
            row.addClass('success');
        },
        onPageChange: function (number, size) {
            m_pagerow = (number - 1) * size;
        }
    });
};

//自定义初始化BootstrapTable，无分页
function LvInitTableNoPaging(tb, tbColumns, actionUrl, actionQueryParams, detailFormatterFun, methodName,
    toolbarName, uniqueIdName, sidePaginationType, pageSizeNumber, isCache, isSingleSelect,
    isShowColumns, isShowRefresh, isClickToSelect, isDetailView) {
    $(tb).bootstrapTable({
        url: actionUrl,                             //请求后台的URL（*）
        method: methodName,                             //请求方式（*）
        columns: tbColumns,
        toolbar: toolbarName,                      //工具按钮用哪个容器
        striped: true,                               //是否显示行间隔色
        cache: isCache,                           //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: false,                          //是否显示分页（*）
        sortable: false,                              //是否启用排序
        sortOrder: "asc",                           //排序方式
        queryParamsType: '',
        queryParams: actionQueryParams,//传递参数（*）
        sidePagination: sidePaginationType,           //分页方式：client客户端分页，server服务端分页（*）
        detailFormatter: detailFormatterFun,
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: pageSizeNumber,       //每页的记录行数（*）
        pageList: [5, 10, 25],         //可供选择的每页的行数（*）
        search: false,                             //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
        showColumns: isShowColumns,                  //是否显示所有的列
        showRefresh: isShowRefresh,                  //是否显示刷新按钮
        minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: isClickToSelect,                //是否启用点击选中行
        singleSelect: isSingleSelect,
        //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: uniqueIdName,                     //每一行的唯一标识，一般为主键列
        cardView: false,                    //是否显示详细视图
        detailView: isDetailView,                   //是否显示父子表
        onClickRow: function (e, row, $element) {
            $('.success').removeClass('success');
            row.addClass('success');
        },
        onPageChange: function (number, size) {
            m_pagerow = (number - 1) * size;
        }
    });
};

//自定义初始化BootstrapTable，置顶TableHeight
function LvInitTableWithHeight(tb, tbColumns, actionUrl, actionQueryParams, detailFormatterFun, methodName,
    toolbarName, uniqueIdName, sidePaginationType, pageSizeNumber, isCache, isSingleSelect,
    isShowColumns, isShowRefresh, isClickToSelect, isDetailView) {
    $(tb).bootstrapTable({
        url: actionUrl,                             //请求后台的URL（*）
        method: methodName,                             //请求方式（*）
        columns: tbColumns,
        toolbar: toolbarName,                      //工具按钮用哪个容器
        striped: true,                               //是否显示行间隔色
        cache: isCache,                           //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                          //是否显示分页（*）
        sortable: false,                              //是否启用排序
        sortOrder: "asc",                           //排序方式
        queryParamsType: '',
        queryParams: actionQueryParams,//传递参数（*）
        sidePagination: sidePaginationType,           //分页方式：client客户端分页，server服务端分页（*）
        detailFormatter: detailFormatterFun,
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: pageSizeNumber,       //每页的记录行数（*）
        pageList: [5, 10, 25],         //可供选择的每页的行数（*）
        search: false,                             //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
        showColumns: isShowColumns,                  //是否显示所有的列
        showRefresh: isShowRefresh,                  //是否显示刷新按钮
        minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: isClickToSelect,                //是否启用点击选中行
        singleSelect: isSingleSelect,
        height: 350,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: uniqueIdName,                     //每一行的唯一标识，一般为主键列
        cardView: false,                    //是否显示详细视图
        detailView: isDetailView,                   //是否显示父子表
        onClickRow: function (e, row, $element) {
            $('.success').removeClass('success');
            row.addClass('success');
        },
        onPageChange: function (number, size) {
            m_pagerow = (number - 1) * size;

        }
    });
};

//-----------------其他操作系列-----------------

//重置密码
function MultReset(table, actionUrl) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确定要将已选择用户的密码重置为初始密码“123456”吗？" }).on(function (e) {
        if (!e) {
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { "arrselections": JSON.stringify(arrselections) },
            success: function (result) {
                if (result.ResultType === 0) {
                    toastr.success(result.Message);
                    //$(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.Message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//-----------------Abp项目规范操作系列-----------------

//表格行多选删除
function MultDelete(table, actionUrl) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
        if (!e) {
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { "arrselections": JSON.stringify(arrselections) },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//获取表格选中行的id
function GetBootstrapTableRowId(table, theIdName) {
    var theRow = $(table).bootstrapTable('getSelections');
    var theId = "notFound";
    for (var i = 0; i < theRow.length; i++) {
        var prototype = theRow[i];
        for (var key in prototype)
            if (prototype.hasOwnProperty(key))
                if (key === theIdName) {
                    theId = prototype[key];
                    break;
                }
    }
    return theId;
}

//表格行单选删除
function SingleDeleteBackup(table, actionUrl, tableIdName) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
        if (!e) {
            return;
        }
        var theTableId = GetBootstrapTableRowId(table, tableIdName);
        if (theTableId === "notFound") {
            toastr.warning('请选择有效数据');
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选删除
function SingleDelete(table, actionUrl, tableIdName) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
        if (!e) {
            return;
        }
        var theTableId = GetBootstrapTableRowId(table, tableIdName);
        if (theTableId === "notFound") {
            toastr.warning('请选择有效数据');
            return;
        }
        $.ajax({
            type: "get",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选联合条件删除
function SingleDeleteWithUnionKey(table, actionUrl, key1Name, key2Name) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
        if (!e) {
            return;
        }
        var theTableId1 = GetBootstrapTableRowId(table, key1Name);
        var theTableId2 = GetBootstrapTableRowId(table, key2Name);
        if (theTableId1 === "notFound" || theTableId2 === "notFound") {
            toastr.warning('请选择有效数据');
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: {
                id1: theTableId1,
                id2: theTableId2
            },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选编辑、详细
function editRowForMutiPageForm(table, actionUrl, tableIdName) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('请选择有效数据');
        return;
    }
    window.location = actionUrl + "?id=" + theTableId;
}

//表格行单选提交
function submitStateForForMutiPageForm(table, actionUrl, tableIdName) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确认要提交数据吗？提交后将不能进行编辑、删除操作！" }).on(function (e) {
        if (!e) {
            return;
        }
        var theTableId = GetBootstrapTableRowId(table, tableIdName);
        if (theTableId === "notFound") {
            toastr.warning('请选择有效数据');
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//详细弹出模态窗体
function detailShowModal(table, actionUrl, tableIdName, modalId, successUpdateDivId) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('请选择有效数据');
        return;
    }
    $(modalId).modal('show');
    $.ajax({
        type: "post",
        url: actionUrl,
        contentType: "application/json",
        dataType: "html",
        data: JSON.stringify(
            {
                "id": theTableId
            }
        ),
        success: function (data) {
            $(successUpdateDivId).empty();
            $(successUpdateDivId).html(data);
        }
    });
}

//返回
function Back(message, actionUrl) {
    WinMsg.confirm({ message: message }).on(function (e) {
        if (!e) {
            return;
        }
        window.location = actionUrl;
    });
}

//表格行单选取消
function cancelStateForForMutiPageForm(table, actionUrl, tableIdName) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据');
        return;
    }
    WinMsg.confirm({ message: "确认要取消工单吗？取消后将不能执行！" }).on(function (e) {
        if (!e) {
            return;
        }
        var theTableId = GetBootstrapTableRowId(table, tableIdName);
        if (theTableId === "notFound") {
            toastr.warning('请选择有效数据');
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.ResultType === 0) {
                    toastr.success(result.Message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.Message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选有条件删除
function SingleDeleteWithCheckStateNo(table, actionUrl, tableIdName, stateValue, stateTip) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据！');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('指定数据未找到！');
        return;
    }
    //状态为stateValue的数据不能删除，弹出stateTip提示
    if (arrselections[0].State === stateValue) {
        toastr.warning(stateTip);
        return;
    }
    WinMsg.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
        if (!e) {
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.Message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.Message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选有条件新页面编辑
function SingleEditWithCheckStateNo(table, actionUrl, tableIdName, stateValue, stateTip) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据！');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('指定数据未找到！');
        return;
    }
    //状态为stateValue的数据不能编辑，弹出stateTip提示
    if (arrselections[0].State === stateValue) {
        toastr.warning(stateTip);
        return;
    }
    window.location = actionUrl + "?id=" + theTableId;
}

//表格行单选有条件提交
function SingleSubmitWithCheckStateNo(table, actionUrl, tableIdName, stateValue, stateTip) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据！');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('指定数据未找到！');
        return;
    }
    //状态为stateValue的数据不能提交，弹出stateTip提示
    if (arrselections[0].State === stateValue) {
        toastr.warning(stateTip);
        return;
    }
    WinMsg.confirm({ message: "确认要提交数据吗？提交后将不能进行编辑、删除操作！" }).on(function (e) {
        if (!e) {
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选有条件提交
function SingleSubmitWithCheckStateYes(table, actionUrl, tableIdName, stateValue, stateTip) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据！');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('指定数据未找到！');
        return;
    }
    //状态不为stateValue的数据不能提交，弹出stateTip提示
    if (arrselections[0].State !== stateValue) {
        toastr.warning(stateTip);
        return;
    }
    WinMsg.confirm({ message: "确认要提交数据吗？提交后将不能进行编辑、删除操作！" }).on(function (e) {
        if (!e) {
            return;
        }
        $.ajax({
            type: "post",
            url: actionUrl,
            data: { id: theTableId },
            success: function (result) {
                if (result.result.resultType === 0) {
                    toastr.success(result.result.message);
                    $(table).bootstrapTable('refresh');
                }
                else {
                    toastr.error(result.result.message);
                }
            },
            error: function () {
                toastr.error('网络错误，请重新提交！');
            }
        });
    });
}

//表格行单选详细
function SingleDetailWithShowBigModalDetail(table, actionUrl, tableIdName, title) {
    var arrselections = $(table).bootstrapTable('getSelections');
    if (arrselections.length <= 0) {
        toastr.warning('请选择有效数据！');
        return;
    }
    var theTableId = GetBootstrapTableRowId(table, tableIdName);
    if (theTableId === "notFound") {
        toastr.warning('指定数据未找到！');
        return;
    }
    var $modal = $("#modalDetail");
    //表单初始化
    $(".modal-title", $modal).html(title);
    $.ajax({
        type: "GET",
        url: actionUrl,
        data: {
            'id': theTableId
        },
        success: function (result) {
            $("#modalContent").empty();
            $("#modalContent").html(result);
            $("#modalDetail").modal('show');
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

//-----------------模态框操作js函数-----------------

//显示模态窗体表单
function ShowModal(actionUrl, param, title) {
    var $modal = $("#modal-form");
    //表单初始化
    $(".modal-title", $modal).html(title);
    $("#modal-content", $modal).attr("action", actionUrl);
    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#modal-content").html(result);
            $('#modal-form').modal('show');
            RegisterForm();//通过Ajax加载返回的页面原有MVC属性验证将失效，需要重新注册验证脚本。
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

//使用绑定函数显示模态窗体表单
function ShowModalWithBindingFunction(actionUrl, param, title, bindingFunction) {
    var $modal = $("#modal-form");
    //表单初始化
    $(".modal-title", $modal).html(title);
    $("#modal-content", $modal).attr("action", actionUrl);

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#modal-content").html(result);
            $('#modal-form').modal('show');
            RegisterForm();//通过Ajax加载返回的页面原有MVC属性验证将失效，需要重新注册验证脚本。
            bindingFunction();
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}

//保存模态窗体表单
function SaveModal(table) {
    var actionUrl = $("#modal-content").attr("action");
    var $form = $("#modal-content");
    if (!$form.valid()) {
        return;
    }
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: $form.serialize(),
        success: function (result) {
            if (result.result.resultType === 0) {
                toastr.success(result.result.message);//Abp封装
                $('#modal-form').modal('hide');
                $(table).bootstrapTable('refresh');
            }
            else {
                toastr.error(result.result.message);//Abp封装
            }
        },
        error: function () {
            toastr.error('网络错误，请重新提交！');
        }
    });
}

//注册验证脚本，通过Ajax返回的页面原有MVC属性验证将失效，需要重新注册验证脚本
function RegisterForm() {
    $('#modal-content').removeData('validator');
    $('#modal-content').removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse('#modal-content');
}

//-----------------自动补全函数-----------------

//标准自动补全
function Autocomplete(itemId, itemName, autoUrl, autoTime) {
    var cache = {};
    $(itemName)
        .autocomplete({
            minLength: 0,
            source: function (request, response) {
                var term = request.term;
                if (term in cache) {
                    data = cache[term];
                    response($.map(data,
                        function (item) {
                            return {
                                label: item.Name,
                                value: item.Id
                            };
                        }));
                } else {
                    $.ajax({
                        url: autoUrl,
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({
                            top: 10,
                            term: request.term,
                            queryPeriod: autoTime
                        }),
                        success: function (data) {
                            if (data.length) {
                                cache[term] = data;
                                response($.map(data,
                                    function (item) {
                                        return {
                                            label: item.Name,
                                            value: item.Id
                                        };
                                    }));
                            }
                        }
                    });
                }
            },
            focus: function (event, ui) {
                $(itemName).val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $(itemName).val(ui.item.label);
                $(itemId).val(ui.item.value);
                return false;
            }
        });
}

//无响应时间自动补全
function AutocompleteAlone(itemId, itemName, autoUrl) {
    var cache = {};
    $(itemName)
        .autocomplete({
            minLength: 0,
            source: function (request, response) {
                var term = request.term;
                if (term in cache) {
                    data = cache[term];
                    response($.map(data,
                        function (item) {
                            return {
                                label: item.Name,
                                value: item.Id
                            };
                        }));
                } else {
                    $.ajax({
                        url: autoUrl,
                        dataType: "json",
                        type: "Get",
                       contentType: "application/json; charset=utf-8",
                        data: {
                            term: request.term
                        },
                        success: function (data) {
                            if (data.length) {
                                cache[term] = data;
                                response($.map(data,
                                    function (item) {
                                        return {
                                            label: item.Name,
                                            value: item.Id
                                        };
                                    }));
                            }
                        }
                    });
                }
            },
            focus: function (event, ui) {
                $(itemName).val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $(itemName).val(ui.item.label);
                $(itemId).val(ui.item.value);
                return false;
            }
        });
}


