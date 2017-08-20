/// <reference path="D:\WORKING\STUDY\WEB\WebAPI_AngularJS\SHOP\shop\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
	angular.module('shopapp', [
		'shopapp.products',
		'shopapp.productCategories',
		'shopapp.common']).config(config);
	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('home', {
			url: "/admin",
			templateUrl: "/app/components/home/homeView.html",
			controller: "homeController"
		});
		$urlRouterProvider.otherwise('/admin');
	}
})();