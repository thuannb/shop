(function (app) {
	app.controller('productCategoryListController', productCategoryListController);

	productCategoryListController.$inject = ['$scope', 'apiService','notificationService'];

	function productCategoryListController($scope, apiService, notificationService) {
		$scope.productCategories = [];
		$scope.page = 0;
		$scope.pagesCount = 0;
		$scope.keyword = "";
		$scope.getProductCagories = getProductCagories;
		$scope.search = search;

		function search(){
			getProductCagories();
		}

		function getProductCagories(page) {
			page= page || 0;
			//Create Para
			var config = {
				params: {
					keyword: $scope.keyword,
					page: page,
					pageSize: 20
				}
			}

			apiService.get('/api/productcategory/getall', config, function (result) {
				if (result.data.TotalCount === 0) {
					notificationService.displayWarning('Không tìm thấy dữ liệu bạn tìm!', 'Cảnh báo');
				}
				else {
					notificationService.displaySuccess('Dữ liệu đã được tìm thấy!', 'Thông báo');
				}
				$scope.productCategories = result.data.Items;
				$scope.page = result.data.Page;
				$scope.pagesCount = result.data.PagesCount;
				$scope.totalCount = result.data.TotalCount;
			}, function () {
				console.log('Load productcategory failed.');
			});
		}

		$scope.getProductCagories();
	}
})(angular.module('shopapp.productCategories'));