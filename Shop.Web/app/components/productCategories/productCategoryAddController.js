/// <reference path="../../shared/services/notificatonService.js" />
/// <reference path="../../shared/services/apiService.js" />
/// <reference path="../../Assets/admin/libs/angular/angular.js" />

(function (app) {

	app.controller('productCategoryAddController', productCategoryAddController);

	productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']

	function productCategoryAddController($scope, apiService, notificationService, $state) {
		$scope.productCategory = {
			CreatedDate: new Date(),
			Status: true
		}

		$scope.addProductCategory = addProductCategory;

		$scope.GetSeoTitle = GetSeoTitle;

		function GetSeoTitle() {
			$scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
		}

		function addProductCategory() {
			apiService.post('api/productcategory/create', $scope.productCategory,
			  function (result) {
			  	notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.', 'Info');
			  	$state.go('productCategories');
			  }, function (error) {
			  	notificationService.displayError('Thêm mới không thành công.', 'Error');
			  });
		}

		function loadParentCategories() {
			apiService.get('api/productcategory/getallparent', null, function (result) {
				$scope.parentCategories = result.data;
			}, function () {
				notificationService.displayError('Can not load data parent.', 'Error');
			});
		}

		loadParentCategories();
	}
})(angular.module('shopapp.productCategories'));