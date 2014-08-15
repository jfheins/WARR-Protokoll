﻿function ReplaceRow(list, rowId, data) {
	$('#' + list + '_' + rowId).replaceWith(data);
	RefreshTables(list);
	enableDatePicker();
}

function RemoveRow(list, rowId) {
	$('#' + list + '_' + rowId).remove();
	RefreshTables(list);
}

function AddRow(list, data) {
	$('#' + list + '_tbody').append(data);
	$('#' + list + '_tbody + tfoot form')[0].reset();
	RefreshTables(list);
	$('#' + list + '_form input[type=date]').each(function() {
		 reformatDateField(this);
	});
}

function RefreshTables(list) {
	var selector = 'table.table-sortable';
	if (list) {
		selector = '#' + list + '_table';
	}
	$(selector).trigger('update');
	$("table.table-sortable time[rel=timeago]").timeago();
}