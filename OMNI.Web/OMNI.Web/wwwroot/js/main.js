
var Main = {
    init: function () {
        this.setPartnerList();
        this.changePartnerList();
        //this.getLocalStatusByCode();
        this.getPartnerUserKey();
        //this.checkSession();
        //this.objectifyForm();
        //        this.numberWithCommas();
        //        this.numberLostCommas();
        //        this.autoChangeToNumberWithComma();
        //        this.datatables();
        //        this.afterLoadAjax();
        //        this.link();
        //this.btnLink();
        //this.select2();
        this.modalDialog();
        //this.setDataAll();
        //        this.imagesPreview();
        //        this.checkSession();
        //this.autoLogout();
        //this.requestlist();
        //this.downloadTemplate();
        //this.draggable();
        //this.choosen();
        this.datetimepicker();
        this.onlymonthyearpicker();
        this.onlyyearpicker();
        this.onlydatepicker();
        this.onlytimepicker();
        this.loaderAjax();
        //        this.resizedataURL();
        //        this.imageExists();
        //        this.dropzoneInit();
        //        this.swallWarningForm();
        //this.onlyNumberInput();
        //this.convertDateASP();
        //this.timeAgo(now,date);
        //this.autoSetValue();
        //this.convertSecondtoString();
    },
    //checkSession: function () {
    //    $(document).on('click', 'a', function () {
    //        var url = window.location.href;
    //        if (!url.includes("Login")) {
    //            console.log('on a href click!');
    //            $.ajax({
    //                type: 'GET',
    //                url: base_url + 'Session/CheckSessionWithJSON',
    //                success: function (result) {
    //                    if (result === null) {
    //                        window.location.href = base_url + 'Account/Login?ReturnUrl=%2F';
    //                    }
    //                }
    //            });
    //        }
    //    });

    //    $(document).on('click', 'button', function () {
    //        console.log('on button click!');
    //        var url = window.location.href;
    //        if (!url.includes("Login")) {
    //            $.ajax({
    //                type: 'GET',
    //                url: base_url + 'Session/CheckSessionWithJSON',
    //                success: function (result) {
    //                    if (result === null) {
    //                        window.location.href = base_url + 'Account/Login?ReturnUrl=%2F';
    //                    }
    //                }
    //            });
    //        }
    //    });

    //    $(document).on('click', '.swal2-styled', function () {
    //        console.log('on swal click!');
    //        var url = window.location.href;
    //        if (!url.includes("Login")) {
    //            $.ajax({
    //                type: 'GET',
    //                url: base_url + 'Session/CheckSessionWithJSON',
    //                success: function (result) {
    //                    if (result === null) {
    //                        window.location.href = base_url + 'Account/Login?ReturnUrl=%2F';
    //                    }
    //                }
    //            });
    //        }
    //    });
    //},
    setDataAll: function (userId) {
        if (!userId) {
            window.location.href = base_url + 'identity/account/logout';
        }

        //async: false,
        if (!localStorage.getItem("data")) {
            $.when(
                //2. get 
                $.get(base_api + "UserExtraType/GetList"),
                $.post(base_api + "User/GetList", { id: userId }),
            ).done(function (status, usertype, josub, jomain, user) {
                function setData() {
                    var data = {};
                    data.status = status[0].data; // get all
                    data.usertype = usertype[0].data; // get all
                    data.josub = josub[0].data; // get all
                    data.jomain = jomain[0].data; // get all
                    data.user = user[0].data[0]; // hanya 1 row
                    localStorage.setItem("data", JSON.stringify(data));

                    // Set Partner
                    localStorage.setItem("selectedPartner", Main.getPartnerUserKey("Id").join(","));
                }

                $.when(
                    setData()
                ).done(function () {
                    window.location.href = base_url.slice(0, -1) + Main.getUrlParameter("ReturnUrl");

                });


            });
        }
    },
    unescapeHTML: function (escapedHTML) {
        return escapedHTML.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, '&');
    },
    getLocalStatusByCode: function (variable) {
        var data = localStorage.getItem("data");
        var result = {};
        if (data) {
            data = JSON.parse(data);
            $.each(data[variable], function (i, value) {
                result[value.code] = value;
            });
        }
        return result;
    },

    getLocalUser: function () {
        var data = localStorage.getItem("data");
        var result = {};
        if (data) {
            result = JSON.parse(data).user;
        }
        return result;
    },
    getPartnerUserKey: function (variable) {
        var data = localStorage.getItem("data");
        var result = [];
        if (data) {
            data = JSON.parse(Main.getLocalUser().partnerId);
            $.each(data, function (i, value) {
                result.push(value[variable]);
            });
        }
        return result;
    },
    changePartnerList: function () {
        $(document).on('click', '.login-partner .partner-select', function () {
            var partnerId = $('.login-partner select[name="partner_id[]"]').val();
            if (partnerId.length > 0) {
                localStorage.setItem("selectedPartner", partnerId.join(","));
            } else {
                localStorage.setItem("selectedPartner", '0');
            }
            location.reload();
        });
        $(document).on('click', '.login-partner .partner-all', function () {
            $('.login-partner select[name="partner_id[]"]').val(Main.getPartnerUserKey("Id")).trigger("change");
        });
        $(document).on('click', '.login-partner .partner-reset', function () {
            $('.login-partner select[name="partner_id[]"]').val([]).trigger("change");
        });
    },
    setPartnerList: function () {

        var result = [];
        if ($('.login-partner select[name="partner_id[]"]').length) {
            var data = localStorage.getItem("data");
            if (data) {
                data = JSON.parse(Main.getLocalUser().partnerId);
                $.each(data, function (i, value) {
                    $('.login-partner select[name="partner_id[]"]').append($('<option>').text(value.Name).attr('value', value.Id));
                });
                $('.login-partner select[name="partner_id[]"]').val(localStorage.getItem("selectedPartner").split(','));
                $('.login-partner select[name="partner_id[]"]').select2({
                    /* Sort data using localeCompare */
                    sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
                }).trigger('change');
            }
        }
        return result;
    },
    objectifyForm: function (formArray) {
        var returnArray = {};
        for (var i = 0; i < formArray.length; i++) {
            returnArray[formArray[i]['name']] = formArray[i]['value'];
        }
        return returnArray;
    },
    onlyNumberInput: function () {
        $(document).on('keyup', '.onlyNumberInput', function () {
            $(this).val($(this).val().replace(/[^0-9]/g, ''))
        });
    },
    swallWarningForm: function () {
        swal({
            title: 'Must Be Filled?',
            text: "Form is something wrong.",
            type: 'warning'
        });
    },
    dropzoneInit: function (data) {

        $(function () {


            var zone = JSON.parse(data);
            Dropzone.autoDiscover = false;
            var foto_upload = new Dropzone(zone.dropzone, {
                url: zone.create,
                maxFilesize: 2,
                method: "post",
                acceptedFiles: "image/*,application/pdf",
                paramName: "attach",
                dictInvalidFileType: "this file is not allowed",
                addRemoveLinks: true,
                //            thumbnailWidth: 300,
                //            thumbnailHeight: 300,
                init: function () {

                    thisDropzone = this;

                    var attach = $(zone.input).val();
                    var arr_new = [];
                    attach = attach.split(',').filter(Boolean);
                    attach.forEach(function (a, b) {
                        var src = zone.read + a;
                        console.log(src)
                        if (Main.imageExists(src)) {
                            arr_new.push(a)
                            var mockFile = { name: a, size: 12345 };
                            thisDropzone.emit("addedfile", mockFile);

                            thisDropzone.createThumbnailFromUrl(mockFile, src);

                            // Make sure that there is no progress bar, etc...
                            thisDropzone.emit("complete", mockFile);
                            //                    console.log(thisDropzone.removeFile)
                        }
                    })
                    $(zone.input).val(arr_new)

                    setTimeout(function () {
                        $('.dz-preview').each(function (a, b) {
                            var alt = $(this).find('.dz-filename span').text();
                            console.log(alt)
                            $(this).find('.dz-remove').attr('id', alt);
                            if (!zone.removefile) {
                                $(this).find('.dz-remove').hide();
                                //                                    .addClass('btn-dz-preview')
                                //                                    .attr({
                                //                                        title:'PREVIEW',
                                //                                        href:'',
                                //                                        src:zone.read + alt,
                                //                                    })
                                //                                    .text('preview')
                                //                                    .removeClass('dz-remove');
                            }

                        })
                    }, 1000);



                    // attach callback to the `success` event
                    this.on("success", function (file, result) {
                        result = JSON.parse(result);
                        if (result.status === 1) {
                            var attach = $(zone.input).val();
                            var nn = attach.split(',');
                            console.log(result.data.action)
                            if (result.data.action === 'create') {
                                nn.push(result.data.name);
                                file._removeLink.id = result.data.name;
                            }
                            $(zone.input).val(nn.filter(Boolean).join());
                        }

                    });

                    this.on("complete", function (file, result) {
                        //                    console.log($(this).find('a.dz-remove').html())
                        //                    console.log(result)
                        //                    console.log(this.getUploadingFiles())
                        //
                        //                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                        //                        doSomething();
                        //                    }
                    });

                    //Event ketika Memulai mengupload
                    this.on("sending", function (a, b, c) {
                        a.token = Math.random();
                        c.append("token_foto", a.token); //Menmpersiapkan token untuk masing masing foto
                    });

                    //Event ketika Memulai mengupload
                    this.on("uploadprogress", function (a, b, c) {

                        console.log(a.upload.progress)
                        console.log(b)
                        console.log(c)
                    });

                    //Event ketika foto dihapus
                    this.on("removedfile", function (a) {

                        var token = a.token;
                        $.ajax({
                            type: "post",
                            data: { token: token, id: a._removeLink.id },
                            url: zone.delete,
                            cache: false,
                            //                        dataType: 'json',
                            success: function (result) {
                                result = JSON.parse(result);
                                if (result.status === 1) {
                                    var attach = $(zone.input).val();
                                    var nn = attach.split(',').filter(Boolean);
                                    var index = nn.indexOf(result.data.name);
                                    if (index > -1) {
                                        nn.splice(index, 1);
                                    }
                                    $(zone.input).val(nn.filter(Boolean).join());
                                }

                                if ($('.dz-image-preview').html() === undefined) {
                                    $('.dz-message').show();
                                } else {
                                    $('.dz-message').hide();
                                }
                            },
                            error: function () {
                                console.log("Error");

                            }
                        });
                    });

                }
            });
        });

    },
    loaderAjax: function () {
        $(document).ajaxStart(function () {
            $('#page-loader').removeClass('hide');
        }).ajaxStop(function () {
            $('#page-loader').addClass('hide');
        }).ajaxError(function () {
            $('#page-loader').addClass('hide');
        });
    },
    onlydatepicker: function () {
        if ($('.onlydatepicker').length) {
            //$(".onlydatepicker").datetimepicker({ format: 'dd/mm/yyyy hh:ii' });
            $('.onlydatepicker').datetimepicker({
                format: 'DD/MM/YYYY',
                useCurrent: false
                //useCurrent: 'day',
                //autoclose: true
            }).attr("tabindex", 0).find("input").css("pointer-events", "none");
        }
    },
    onlymonthyearpicker: function () {
        if ($('.onlymonthyearpicker').length) {
            //$(".onlydatepicker").datetimepicker({ format: 'dd/mm/yyyy hh:ii' });
            $('.onlymonthyearpicker').datetimepicker({
                viewMode: 'years',
                format: 'MM/YYYY',
                useCurrent: false
                //useCurrent: 'day',
                //autoclose: true
            }).attr("tabindex", 0).find("input").css("pointer-events", "none");

            $(".onlymonthyearpicker").on("dp.show", function (e) {
                $(e.target).data("DateTimePicker").viewMode("months");
            })
        }
    },
    onlyyearpicker: function () {
        if ($('.onlyyearpicker').length) {
            $('.onlyyearpicker').datetimepicker({
                viewMode: 'years',
                format: 'YYYY',
                useCurrent: false
                //useCurrent: 'day',
                //autoclose: true
            }).attr("tabindex", 0).find("input").css("pointer-events", "none");

            $(".onlyyearpicker").on("dp.show", function (e) {
                $(e.target).data("DateTimePicker").viewMode("years");
            })
        }
    },
    onlytimepicker: function () {
        if ($('.onlytimepicker').length) {
            $('.onlytimepicker').datetimepicker({
                format: 'HH:mm',
                //useCurrent: 'day',
                useCurrent: false
            }).attr("tabindex", 0).find("input").css("pointer-events", "none");
        }
    },
    datetimepicker: function () {
        if ($('.datetimepicker').length) {
            $('.datetimepicker').datetimepicker({
                format: 'DD/MM/YYYY HH:mm:00',
                //useCurrent: 'day',
                useCurrent: false,
            }).attr("tabindex", 0).find("input").css("pointer-events", "none");
        }
    },
    clockPicker: function () {
        $('.clockpicker').clockpicker();
    },
    modalDialog: function () {


        $(document).on('click', '.btn-datatable-doc', function (e) {
            e.preventDefault();
            var src = $(this).attr('src');
            var title = 'Document Priview';
            var zoom = 'modal-lg';

            var modal = $('#myModal3');
            modal.modal('show');
            modal.find('.modal-title').html(title);
            modal.find('.modal-dialog').removeClass('modal-xl modal-lg modal-md modal-sm modal-xs');
            modal.find('.modal-dialog').addClass(zoom);
            modal.find('.modal-body').html('<i class=\"fa fa-circle-o-notch fa-spin\"></i>');
            modal.find('.modal-body').html('<embed name="plugin" src="' + src + '" type="application/pdf" style="height:550px;width:100%;">');
        });

      
        $(document).on('click', '.btn-delete', function (e) {

            });

    
    },
    afterLoadAjax: function () {
        $(document)
            .ajaxStart(function () {
            })
            .ajaxStop(function () {

            });
    },
    autoChangeToNumberWithComma: function () {
        $('table td').each(function () {
            if (!isNaN($(this).html())) {
                $(this).text(ConvertFormat.numberWithCommas($(this).html()));
            }
        });
    },
    numberWithCommas: function (x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    },
    numberLostCommas: function (x) {
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/[^\d.]/g, '');
        return parts.join(".");
    },
    numberLostChar: function (x) {
        return x.replace(/[^0-9]/g, '');
    },
    tableCreateSingle: function (data) {

        var tableBody = "<table class=\"table\">";

        for (var key in data) {
            if (data.hasOwnProperty(key)) {
                tableBody += "<tr>";
                tableBody += "<td>" + this.ucWord(key.replace(/_/g, ' ')) + "</td>";
                tableBody += "<td>" + data[key] + "</td>";
                tableBody += "</tr>";
            }
        }
        tableBody += "</table>";
        return tableBody;
    },
    removeObjectEmpty: function(obj) {
        for (var propName in obj) {
            if (!obj[propName]) {
                delete obj[propName];
            }
        }
        return obj;
    },
    exportToExcel: function (params = {}) {

        var img = base_url + '/img/logo-ptk-print.png';

        var title = '<img src="' + img + '"></img><br><br>\n\
                    <b> PT.PERTAMINA TRANS KONTINENTAL<b> <br> \n\
                    <b>' + params.header + '<b> <br> \n\
                    <b>Periode : ' + (params.periode ? params.periode : '== All ==') + ' <b> <br> \n\
                    <b>Location : ' + (params.location ? params.location : '== All ==') + ' <b> <br> \n\
                    ';

        var tab_text = '<html xmlns:x="urn:schemas-microsoft-com:office:excel">';
        tab_text = tab_text + '<head><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>';
        tab_text = tab_text + '<x:Name>Sheet1</x:Name>';
        tab_text = tab_text + '<x:WorksheetOptions><x:Panes></x:Panes></x:WorksheetOptions></x:ExcelWorksheet>';
        tab_text = tab_text + '</x:ExcelWorksheets></x:ExcelWorkbook></xml></head><body>';

        tab_text = tab_text + title;

        tab_text = tab_text + "<table border='1px'>";

        var exportTable = $('#' + params.table).clone();
        exportTable.find('.mark').each(function (index, elem) { $(elem).remove(); });

        tab_text = tab_text + exportTable.html();
        tab_text = tab_text + '</table></body></html>';
        var data_type = 'data:application/vnd.ms-excel';
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        var fileName = params.header + '.xls';
        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
            if (window.navigator.msSaveBlob) {
                var blob = new Blob([tab_text], {
                    type: "application/csv;charset=utf-8;"
                });
                navigator.msSaveBlob(blob, fileName);
            }
        } else {
            var blob2 = new Blob([tab_text], {
                type: "application/csv;charset=utf-8;"
            });
            var filename = fileName;
            var elem = window.document.createElement('a');
            elem.href = window.URL.createObjectURL(blob2);
            elem.download = filename;
            document.body.appendChild(elem);
            elem.click();
            document.body.removeChild(elem);
        }
    },
    exportToWord: function (header, table, name) {
        var head = "<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
            "xmlns:w='urn:schemas-microsoft-com:office:word' " +
            "xmlns='http://www.w3.org/TR/REC-html40'>" +
            "<head><meta charset='utf-8'><title>Export HTML to Word Document with JavaScript</title></head><body>";
        var footer = "</body></html>";

        var title = '<b>PT. PERTAMINA TRANS KONTINENTAL<b> <br> <b>Periode : <b> <br> <b>'+header+'<b> <br>';


        mydiv = document.createElement('div');
        mydiv.innerHTML = document.getElementById(table).innerHTML;
        $(mydiv).find('#mytable').attr('cellspacing', 0);
        $(mydiv).find('#mytable').attr('cellpadding', 0);

        $(mydiv).find('#mytable th').attr('style',"border: 1px solid #dddddd;");
        $(mydiv).find('#mytable td').attr('style',"border: 1px solid #dddddd;");


        console.log($(mydiv).html());


        var sourceHTML = head + title + $(mydiv).html() + footer;

        var source = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(sourceHTML);
        var fileDownload = document.createElement("a");
        document.body.appendChild(fileDownload);
        fileDownload.href = source;
        fileDownload.download = name + '.doc';
        fileDownload.click();
        document.body.removeChild(fileDownload);
    },
    exportToPdf: function (header, table, name, pageOrientation = 'portrait', periode = '') {
        var body = [];
        var widths = [];
        $("#" + table + " tr").each(function (i) {
            if ($(this).parent()[0].localName === 'thead') {
                var th = [];
                $(this).children().each(function () {
                    th.push($(this).text());
                });
                body.push(th);
            } else {
                var td = [];
                $(this).children().each(function () {
                    td.push($(this).text());
                });
                body.push(td);
            }
        });

        $.each(body[0], function () {
            widths.push('*');
        });

        var docDefinition = {
            content: [
                { text: "PT. PERTAMINA TRANS KONTINENTAL", style: 'header'},
                { text: "Periode : " + periode, style: 'header'},
                { text: header, style: 'header'},
                {
                    style: 'tableExample',
                    widths: widths,
                    table: {
                        headerRows: 1,
                        body: body
                    },
                    layout: {
                        hLineWidth: function (i, node) {
                            return i === 0 || i === node.table.body.length ? 0.1 : 0.1;
                        },
                        vLineWidth: function (i, node) {
                            return i === 0 || i === node.table.widths.length ? 0.1 : 0.1;
                        }
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 8,
                    bold: true
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 5
                }
            },
            pageOrientation: pageOrientation
        };

        pdfMake.createPdf(docDefinition).download(name + '.pdf');
    },
    pad: function (num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    },
    intToTime: function (int) {
        int = parseFloat(int).toFixed(2);
        return parseInt(int) + ':' + (int.split('.')[1] > 0 ? (parseInt(int.split('.')[1]) * 60).toString().substr(0, 2) : '00');
    },
    rangeDurationHour: function (from,to) {
        var ende = moment(from, 'DD/MM/YYYY HH:mm'); //todays date
        var nowe = moment(to, 'DD-MM-YYYY HH:mm'); // another date
        var duratione = moment.duration(nowe.diff(ende));
        return duratione.asHours();
    }
};
$(document).ready(function () {
    Main.init();
});