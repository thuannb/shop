(function (app) {
	app.controller('productEditController', productEditController);

	productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams']

	function productEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
		$scope.product = {};

		$scope.ckeditorOptions = {
			languague: 'vi',
			height: '200px'
		};

		$scope.UpdateProduct = UpdateProduct;
		$scope.moreImages = [];
		$scope.GetSeoTitle = GetSeoTitle;

		function GetSeoTitle() {
			$scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
		}

		//Tham số id phải giống với bên API Controller
		function LoadProductDetail() {
			apiService.get('api/product/getByID/' + $stateParams.id, null, function (result) {
				$scope.product = result.data;
				if ($scope.product.MoreImages != null)
					$scope.moreImages = JSON.parse($scope.product.MoreImages);
			}, function () {
				notificationService.displayError('Can not load data parent.', 'Error');
			});
		}

		function UpdateProduct() {
			$scope.product.MoreImages = JSON.stringify($scope.moreImages);
			apiService.put('api/product/update', $scope.product,
                function (result) {
                	notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                	$state.go('products');
                }, function (error) {
                	notificationService.displayError('Cập nhật không thành công.');
                });
		}

		function loadProductCategory() {
			apiService.get('api/productcategory/getallparent', null, function (result) {
				$scope.productCategories = result.data;
			}, function () {
				console.log('Cannot get list parent');
			});
		}

		$scope.ChooseImage = function () {
			var finder = new CKFinder();
			finder.selectActionFunction = function (fileUrl) {
				$scope.$apply(function () {
					$scope.product.Image = fileUrl;
				})
			}
			finder.popup();
		}

		$scope.ChooseMoreImage = function () {
			var finder = new CKFinder();
			finder.selectActionFunction = function (fileUrl) {
				$scope.$apply(function () {
					$scope.moreImages.push(fileUrl);
				});
			}
			finder.popup();
		}

		loadProductCategory();
		LoadProductDetail();
	}
})(angular.module('shopapp.products'));