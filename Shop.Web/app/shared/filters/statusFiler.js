/// <reference path="d:\WORKING\STUDY\WEB\WebAPI_AngularJS\SHOP\shop\Shop.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
	app.filter('statusFilter', statusFilter);
	
	function statusFilter()
	{
		return function (input) {
			if (input == true)
				return 'Kích hoạt';
			else
				return 'Khóa';
		}
	}
})(angular.module('shopapp.common'));