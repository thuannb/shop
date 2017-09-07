/// <reference path="~/Assets/admin/libs/angular/angular.js" />
/// <reference path="~/Assets/admin/libs/angular-ui-router/release/angular-ui-router.js" />
(function () {
	angular.module('shopapp.products', ['shopapp.common']).config(config);
	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('products', {
			url: "/products",
			templateUrl: "/app/components/products/productListView.html",
			controller: "productListController"
		})
			.state('productsAdd', {
			url: "/productsAdd",
			templateUrl: "/app/components/products/productAddView.html",
			controller: "productAddController"
		})
			.state('productsEdit', {
			url: "/productsEdit/:id",
			templateUrl: "/app/components/products/productEditView.html",
			controller: "productEditController"
		});
	}
})();
