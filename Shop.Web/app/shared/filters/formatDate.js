/// <reference path="~/Assets/admin/libs/angular/angular.js" />

(function (app) {

	app.filter('formatDate', formatDate);

	function formatDate() {
		return function (input) {
			var date = new Date(input);
			return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
		}
	}

})(angular.module('shopapp.common'));