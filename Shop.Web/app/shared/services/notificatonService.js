/// <reference path="~/Assets/admin/libs/angular/angular.js" />
(function (app) {

	app.factory('notificationService', notificationService);

	function notificationService() {
		toastr.options = {
			"closeButton": true,
			"debug": false,
			"newestOnTop": false,
			"progressBar": false,
			"positionClass": "toast-top-right",
			"preventDuplicates": false,
			"onclick": null,
			"showDuration": "300",
			"hideDuration": "1000",
			"timeOut": "5000",
			"extendedTimeOut": "1000",
			"showEasing": "swing",
			"hideEasing": "linear",
			"showMethod": "fadeIn",
			"hideMethod": "fadeOut"
		}

		return {
			displaySuccess: displaySuccess,
			displayError: displayError,
			displayWarning: displayWarning,
			displayInfo: displayInfo
		}

		function displaySuccess(message, title) {
			toastr.success(message, title);
		}

		function displayError(error, title) {
			if (Array.isArray(error)) {
				error.each(function (err) {
					toastr.error(err, title);
				});
			}
			else {
				toastr.error(error, title);
			}
		}

		function displayWarning(message, title) {
			toastr.warning(message, title);
		}

		function displayInfo(message, title) {
			toastr.info(message, title);
		}
	}

})(angular.module('shopapp.common'))