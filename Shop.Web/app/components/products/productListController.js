(function (app) {
	app.controller('productListController', productListController);

	productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

	function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
		$scope.products = [];
		$scope.page = 0;
		$scope.pagesCount = 0;
		$scope.keyword = "";
		$scope.getProducts = getProducts;
		$scope.search = search;
		$scope.deleteProduct = deleteProduct;

		$scope.selectAll = selectAll;
		$scope.isSelectAll = false;

		$scope.deleteAll = deleteAll;

		function deleteAll() {
			$ngBootbox.confirm({ message: "Bạn có chắc chắn xóa không các dòng đang chọn không?", title: 'Confirm' })
				.then(function () {

					var listItem = [];
					$.each($scope.selected, function (i, item) {
						listItem.push(item.ID);
					})

					var config = {
						params: {
							listDeleteProduct: JSON.stringify(listItem)//stringify convert object to string
						}
					}

					apiService.del('api/productcategory/deletemulti', config, function (result) {
						notificationService.displaySuccess('Xóa dữ liệu thành công ' + result.data + ' bản ghi!', 'Success');
						$scope.getProductCagories();

					}, function () {
						notificationService.displayError('Xóa dữ liệu không thành công', 'Error');
					});
				});
		}

		function selectAll() {
			if ($scope.isSelectAll === false) {
				angular.forEach($scope.products, function (item) {
					item.Checked = true;
				})

				$scope.isSelectAll = true;
			}
			else {
				angular.forEach($scope.products, function (item) {
					item.Checked = false;
				})
				$scope.isSelectAll = false;
			}
		}

		$scope.$watch("products", function (newValue, oldValue) {
			var checked = $filter("filter")(newValue, { Checked: true });
			if (checked.length) {
				$scope.selected = checked;
				$('#btnDelete').removeAttr('disabled');
			}
			else {
				$('#btnDelete').attr('disabled', 'disabled');
			}
		}, true);

		function deleteProduct(id) {
			$ngBootbox.confirm({ message: "Bạn có chắc chắn xóa không?", title: 'Confirm' })
				.then(function () {
					var config = {
						params: {
							id: id
						}
					}

					apiService.del('api/productcategory/delete', config, function () {
						notificationService.displaySuccess('Xóa dữ liệu thành công', 'Success');

						$scope.getProductCagories();

					}, function () {
						notificationService.displayError('Xóa dữ liệu không thành công', 'Error');
					});
				});
		}

		function search() {
			getProducts();
		}

		function getProducts(page) {
			page = page || 0;
			//Create Para
			var config = {
				params: {
					keyword: $scope.keyword,
					page: page,
					pageSize: 20
				}
			}

			apiService.get('/api/product/getall', config, function (result) {
				if (result.data.TotalCount === 0) {
					notificationService.displayWarning('Không tìm thấy dữ liệu bạn tìm!', 'Cảnh báo');
				}

				$scope.products = result.data.Items;
				$scope.page = result.data.Page;
				$scope.pagesCount = result.data.PagesCount;
				$scope.totalCount = result.data.TotalCount;
			}, function () {
				console.log('Load product failed.');
			});
		}

		$scope.getProducts();
	}
})(angular.module('shopapp.products'));