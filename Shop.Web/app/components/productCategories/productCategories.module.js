/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
	angular.module('shopapp.productCategories', ['shopapp.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {

		$stateProvider.state('productCategories', {
			url: "/productCategories",
			templateUrl: "/app/components/productCategories/productCategoryListView.html",
			controller: "productCategoryListController"
		})
			.state('productCategoryAdd', {
			url: "/productCategoryAdd",
			templateUrl: "/app/components/productCategories/productCategoryAddView.html",
			controller: "productCategoryAddController"
			})
			.state('productCategoryUpdate', {
				url: "/productCategoryUpdate/:id",
				templateUrl: "/app/components/productCategories/productCategoryUpdateView.html",
				controller: "productCategoryUpdateController"
			});
	}
})();