function GotoControllerAsync(url, type, data, includeAntiforgeToken, returnType, successCallback) {
	// type (e.g. post, get etc.)
	// returnType (e.g. json, html etc.)
	// data (json objects)

	const promiseAsync = new Promise((resolve, reject) => {
		resolve('resolve');
	});

	promiseAsync.then((test) => {

		ProcessAsync(url, type, data, includeAntiforgeToken, returnType)
			.then((response) => {

				if (typeof successCallback != 'undefined' && typeof successCallback == 'function') {
					successCallback(response);
				}
			});
	})
}

async function ProcessAsync(url, type, obj, includeAntiforgeToken, returnType) {

	return await $.ajax({
		type: type,
		url: url,
		contentType: includeAntiforgeToken ? 'application/json; charset=utf-8' : 'application/x-www-form-urlencoded; charset=UTF-8',
		data: includeAntiforgeToken ? JSON.stringify(obj) : obj,
		dataType: returnType,
		async: true,
		beforeSend: function (jqXHR, settings) {

			if (includeAntiforgeToken)
				jqXHR.setRequestHeader('RequestVerificationToken', $('.AntiForge' + ' input').val());
		}
	});
}

function loadGrid(gridContainerId
	, getUrl
	, readUrl
	, deleteUrl
	, updateUrl
	, pageSize
	, tableObj = { TableId: '', Columns: [{ FieldName: '', DataType: '' }] }) {

	/*
	 * e.g. FORMAT:
	 * tableObj = {
	 *      TableId: 'table_name', 
	 *      Columns: [
	 *          { FieldName: 'Id', DataType: 'int', IsKey: true },
	 *          { FieldName: 'Description', DataType: 'string', IsKey: false }
	 *      ]
	 *  }
	 *  
	 */

	let gridLayoutLink = '/Home/_TableView';

	// Assign values to the table object properties
	tableObj.GetUrl = getUrl;
	tableObj.ReadUrl = readUrl;
	tableObj.DeleteUrl = deleteUrl;
	tableObj.UpdateUrl = updateUrl;
	tableObj.PageSize = pageSize;

	GotoControllerAsync(gridLayoutLink, 'POST', tableObj, false, 'html', function (response) {
		$('#' + gridContainerId).html(response);
	});
}

function buildColumns(columnFields) {
    let dataColumns = [];

    columnFields.forEach((item) => {
        dataColumns.push({ data: item.CustomRowName == null || item.CustomRowName == '' ? item.FieldName : item.CustomRowName });
    });

    return dataColumns;
}

function buildColumnDefs(columnFields
    , primaryKey
    , tableId
    , readUrl
    , deleteUrl) {

    let dataColumns = [];

    let optionIndexes = columnFields.map((item, index) => {
        if (item.HasOption) {
            return index;
        }
    }).filter(item => item >= 0);

    let hiddenIndexes = columnFields.map((item, index) => {
        if (item.HideFromTable) {
            return index;
        }
    }).filter(item => item >= 0);

    /* add 3 dot options */
    if (optionIndexes.length > 0) {        
        dataColumns.push({
            targets: optionIndexes,
            data: primaryKey,
            render: function (data, type, row, meta) {
                let key_id = data.toString().replace(/ /g, "");

                //return "" +
                //    "<a href='#' style='cursor: pointer' onclick=\"onViewRow('" + key_id + "', '" + primaryKey + "', '" + readUrl + "', '" + tableId + "')\" id='btnView_" + key_id + "'><span>" + key_id + "</span></a>" +
                //        "<div class='btn-group' style='float:right;'>" +
                //        "<img src='/img/3dots.png' title='More Options' data-toggle='dropdown' />" +
                //        "<div class='dropdown-menu' role='menu'>" +
                //    "<a href='#' style='cursor: pointer' onclick=\"onEditRow('" + key_id + "', '" + primaryKey + "', '" + readUrl + "', '" + tableId + "')\" id='btnEdit_" + key_id + "'><span class='dropdown-item'>Edit</span></a>" +
                //    "<a href='#' style='cursor: pointer' onclick=\"onDeleteRow('" + key_id + "', '" + primaryKey + "', '" + deleteUrl + "', '" + tableId + "')\" id='btnDelete_" + key_id + "'><span class='dropdown-item'>Delete</span></a>" +
                //    "</div></div>";

                /* works on bootstrap 5 */
                return "" +
                    "<a href='#' style='cursor: pointer; text-decoration: none' onclick=\"onViewRow('" + row.Id + "', '" + primaryKey + "', '" + readUrl + "', '" + tableId + "')\" id='btnView_" + row.Id + "'>" + key_id + "</a>" +
                    "<span class='dropdown'>" +
                        "<div class='btn-group' style='float: right;' data-bs-toggle='dropdown' aria-expanded='false'>" +
                            "<img src='/img/3dots.png' title='More Options' data-toggle='dropdown' />" +
                        "</div>" +
                        "<ul class='dropdown-menu'>" +
                            "<li style='cursor: pointer'><a class='dropdown-item' onclick=\"onEditRow('" + row.Id + "', '" + primaryKey + "', '" + readUrl + "', '" + tableId + "')\" id='btnEdit_" + row.Id + "'>Edit</a></li>" +
                            "<li style='cursor: pointer'><a class='dropdown-item' onclick=\"onDeleteRow('" + row.Id + "', '" + primaryKey + "', '" + deleteUrl + "', '" + tableId + "')\" id='btnDelete_" + row.Id + "'>Delete</a></li>" +
                        "</ul>" +
                    "</span>";

            }
        });
    }

    /* set column visibility to hidden */
    if (hiddenIndexes.length > 0) {
        dataColumns.push({
            targets: hiddenIndexes,
            visible: false
        });
    }

    //TODO: it should be input from start (loadGrid function)
    dataColumns.push({
        targets: [3],
        render: function (data, type, row, meta) {
            return data === 'Claimed' ? putInBox(data, 'green') : putInBox(data, 'orange');
        }
    });

    return dataColumns;
}

function getkey(columnFields) {
    const result = columnFields.filter(item => item.IsKey == true);
    return result.length > 0 ? result[0].FieldName : null;
}

function navbuttonLogics(data, tableId) {
    let is_limit = false;
    let page_size = parseInt($('#pageSize_' + tableId).val());
    let page_number = parseInt($('#pageNumber_' + tableId).val());
    is_limit = data != null && data.Collection.length < page_size ? true : false;

    if (is_limit) {
        $('#btnNext_' + tableId).addClass('disabled-button');
    }
    else
        $('#btnNext_' + tableId).removeClass('disabled-button');

    if (page_number == 1) {
        $('#btnPrev_' + tableId).addClass('disabled-button');
    }
    else
        $('#btnPrev_' + tableId).removeClass('disabled-button');
}

function clearAllFields(columnFields) {
    columnFields.forEach((item) => {
        $('#input_' + item.FieldName.toLowerCase()).val('');
    });
}

function onViewRow(keyId, primaryKey, readUrl, tableId) {
    let parameter = '?' + primaryKey + '=' + keyId;

    GotoControllerAsync(readUrl + parameter, 'POST', null, false, 'json', function (response) {

        Object.keys(response).forEach(function eachKey(key) {
            $('#input_' + key.toLowerCase()).val(response[key]);
        });

        $('#modal_' + tableId).modal('show');
    });
}

function onEditRow(keyId, primaryKey, readUrl, tableId) {
    let parameter = '?' + primaryKey + '=' + keyId;
    
    GotoControllerAsync(readUrl + parameter, 'POST', null, false, 'json', function (response) {

        Object.keys(response).forEach(function eachKey(key) {
            $('#input_' + key.toLowerCase()).val(response[key]);
        });

        $('#' + tableId).DataTable().ajax.reload();
        $('#modal_' + tableId).modal('show');
    });
}

function onDeleteRow(keyId, primaryKey, deleteUrl, tableId) {
    let parameter = '?' + primaryKey + '=' + keyId;
    let isOk = confirm('Want to delete?');

    if (isOk) {
        GotoControllerAsync(deleteUrl + parameter, 'POST', null, false, 'json', function (response) {
            $('#' + tableId).DataTable().ajax.reload();
        });
    }
}

function onSaveRow(formId, callback, updateUrl) {
    let formData = $('#' + formId).serializeArray();
    let dataSource = {};

    $(formData).each(function (index, obj) {
        dataSource[obj.name] = obj.value;
    });

    GotoControllerAsync(updateUrl, 'POST', dataSource, false, 'json', function (response) {

        if (callback != null && typeof callback == 'function') {
            callback(response);
        }
    });
}

function putInBox(data, color) {
    return '<div style="padding: 6px; background-color: ' + color + '; color: white; width: 100px; border-radius: 5px; font-weight: 600">' + data + '</div>';
}