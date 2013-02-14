$(function () {
	if ($.fn.sisyphus) {
		$('#body_TabView1_tab01layer_menu_slh').append('<div class="sisyphusLoader" style="display:none;float:right;"><img title="loading" class="editorIcon" src="../umbraco_client/images/progressbar.gif" alt="loading"></div>');
		var $loader = $('.sisyphusLoader');
		$('#form1').sisyphus({
			timeout: 0,
			onSave: function () {
				if ($loader.is(':hidden')) {
					$loader.fadeIn('fast');
					setTimeout(function () {
						$loader.fadeOut('fast');
					}, 250);
				}
			},
			excludeFields: $('#__EVENTTARGET,#__EVENTARGUMENT,#__VIEWSTATE')
		});
	}
});