﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
	angular.module('shopapp.productCategories', ['shopapp.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {

		$stateProvider.state('productCategories', {
			url: "/productCategories",
			templateUrl: "/app/components/productCategories/productCategoryListView.html",
			controller: "productCategoryListController"
		});
	}
})();