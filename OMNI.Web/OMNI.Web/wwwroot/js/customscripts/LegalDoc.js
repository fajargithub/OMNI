const ParseValToJson = (formId) => {
    var object = {};
    formData = new FormData(document.getElementById(formId));
    formData.forEach((value, key) => {
        if (key == "__RequestVerificationToken")
            return;
        // Reflect.has in favor of: object.hasOwnProperty(key)
        if (!Reflect.has(object, key)) {
            object[key] = value;
            return;
        }
        if (!Array.isArray(object[key])) {
            object[key] = [object[key]];
        }
        object[key].push(value);
    });
    return JSON.stringify(object);
}

const populateVal = (formId, json) => {
    const form = document.forms[formId];
    //const event = new Event('change');
    const entries = (new URLSearchParams(json)).entries();
    for (const [key, val] of entries) {
        //http://javascript-coder.com/javascript-form/javascript-form-value.phtml
        const input = form.elements[key];
        switch (input.type) {
            case 'checkbox': input.checked = !!val; break;
            default: input.value = val; break;
        }

        if (val != "") {
            $(`span[id='${key}']`).text(val);
            $(`span[class='${key}']`).text(val);
        }
        //$(`input[name='${key}']`).trigger("change");

        //console.log(input);
        //$(input).change();
        // Dispatch it.
        //input.dispatchEvent(event);
    }
    M.updateTextFields();
}

const StoreInput = (formId) => {
    $.ajax(
        {
            method: "POST",
            //url: "?handler=CreateVal",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                Q: Qi,
                M: ParseValToJson(formId)
            },
            success: function (res) {
                if (res.status === "FAILED") {
                    alert("Gagal Save");
                } else {
                    Qi = res.response;
                }
            },
            error: function (res) {
                alert("Gagal Save");
            },
        });
}

const AutoInputEvent = (formId) => {
    $(`#${formId} input,#${formId} select,#${formId} textarea`)
        .on(`change keyup`, function (event)
            {
                const el = $(this);
                if (el.val() == "") {
                    $(`span[id='${el.attr("name")}']`).html(`<span class="empty-field"></span>`);
                } else {
                    $(`span[id='${el.attr("name")}']`).text(el.val());
                }
            }
        );
}
/**
 * This function to autohide some field
 * @param {{field:string,options:[{val:any,flag:any}]}} param
 */
const AutoHide = (param) => {
    console.log("TES");
    const el = $(`input[name="${param.field}"]:checked`);
    for (var i = 0; i < param.options.length; i++) {
        if (el.val() == param.options[i].val) {
            $(`${param.options[i].flag}`).show();
        } else {
            $(`${param.options[i].flag}`).hide();
        }
    }
}