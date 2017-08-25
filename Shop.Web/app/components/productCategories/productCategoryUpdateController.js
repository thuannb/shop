/// <reference path="../../shared/services/notificatonService.js" />
/// <reference path="../../shared/services/apiService.js" />
/// <reference path="../../Assets/admin/libs/angular/angular.js" />
/// <reference path="../../shared/services/commonService.js" />

(function (app) {

	app.controller('productCategoryUpdateController', productCategoryUpdateController);

	productCategoryUpdateController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

	function productCategoryUpdateController($scope, apiService, notificationService, $state, $stateParams, commonService) {
		$scope.productCategory = {
			CreatedDate: new Date(),
			Status: true
		}

		$scope.UpdateProductCategory = UpdateProductCategory;
		//$scope.LoadProductCategoryDetail = LoadProductCategoryDetail;
		$scope.GetSeoTitle = GetSeoTitle;

		function GetSeoTitle() {
			$scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
		}

		//Tham số id phải giống với bên API Controller
		function LoadProductCategoryDetail() {
			apiService.get('api/productcategory/getByID/' + $stateParams.id, null, function (result) {
				$scope.productCategory = result.data;
			}, function () {
				notificationService.displayError('Can not load data parent.', 'Error');
			});
		}

		function UpdateProductCategory() {
			apiService.put('api/productcategory/update', $scope.productCategory,
			   function (result) {
			   	notificationService.displaySuccess(result.data.Name + ' cập nhật thành công.', 'Info');
			   	$state.go('productCategories');
			   }, function (error) {
			   	notificationService.displayError('Cập nhật không thành công.', 'Error');
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
		LoadProductCategoryDetail();
	}
})(angular.module('shopapp.productCategories'));