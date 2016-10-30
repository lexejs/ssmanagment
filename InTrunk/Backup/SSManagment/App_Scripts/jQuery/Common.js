// Global variables
var __passwordCloneValues;

//
// Function that will close jQuery UI dialog, clone it back to aspnet form and perform submit.
// Should be applied to the dialog.
//
// Arguments:
//		butOkId - id of the submit button
//
$.fn.extend({
	dialogCloseAndSubmit: function(butOkId)
	{
		if (!Page_IsValid)
			return false;

		__passwordCloneValues = new Array();
		$(":password", $(this)).each(function()
		{
			__passwordCloneValues.push($(this).val());
		});
		__passwordCloneValues = __passwordCloneValues.reverse();

		var dlg = $(this).clone();
		$(this).dialog("destroy").remove();

		dlg.css("display", "none");
		$("form:first").append(dlg);
		$(":password", dlg).each(function()
		{
			$(this).val(__passwordCloneValues.pop());
		});
		$("#" + butOkId, dlg).click();

		return true;
	}
});