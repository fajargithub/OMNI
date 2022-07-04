class ParamconfirmationSwal {
    constructor(swalParam, yesFunc, noFunc, paramAjax) {
        this.swalParam = swalParam;
        this.yesFunc = yesFunc;
        this.noFunc = noFunc;
        this.paramAjax = paramAjax;
    }
}


class FormParam {
    constructor(form, questionText, submitUrl, returnUrl, modalId, tableId) {
        this.form = form;
        this.questionText = questionText;
        this.submitUrl = submitUrl;
        this.returnUrl = returnUrl;
        this.modalId = modalId;
        this.tableId = tableId;
    }
}


class SwalParam {
    constructor(title, text, type, confirmTextBtn) {
        this.title = title;
        this.text = text;
        this.type = type;
        this.confirmTextBtn = confirmTextBtn;
    }
}

class ParamAjax {
    constructor(isAjax, url, method, data) {
        this.isAjax = isAjax;
        this.url = url;
        this.method = method;
        this.data = data;
    }
}

class DataTableAjax {
    constructor( tableName, columnList ,filtration,controller, clsDataTableEndPoint ,method,setting,orderBy) {
        this.tableName = tableName;
        this.columnList = columnList;
        this.filtration = filtration;
        this.controller = controller;
        this.clsDataTableEndPoint = clsDataTableEndPoint;
        this.method = method;
        this.setting = setting;
        this.orderBy = orderBy;
    }
}

class DataTableAjaxColumns {
    constructor(name,data,renderFormat ) {
        this.name = name;
        this.data = data;
        this.renderFormat = renderFormat;
    }
}

class DataTableFiltration{
    constructor(attr, fieldName) {
        this.attr = attr;
        this.fieldName = fieldName;
    }
}

class DataTableEndPoint {
    constructor(endPointData, endPointEdit, endPointIsDelete, endPointIsActive, endPointAdditional) {
        this.endPointData = endPointData;
        this.endPointEdit = endPointEdit;
        this.endPointIsDelete = endPointIsDelete;
        this.endPointIsActive = endPointIsActive;
        this.endPointAdditional = endPointAdditional;
    }
}

class DataTableSetting {
    constructor(numberColumn, actionColumn , drawCallBack) {
        this.numberColumn = numberColumn;
        this.actionColumn = actionColumn;
        this.drawCallBack = drawCallBack;
    }
}


class SwalDelParam {
    constructor(text, successText, failedText, url, passedData, tableId) {
        this.text = text;
        this.successText = successText;
        this.failedText = failedText;
        this.url = url;
        this.passedData = passedData;
        this.tableId = tableId;
    }
}

class SwalErrorParam {
    constructor(text) {
        this.text = text;
    }
}