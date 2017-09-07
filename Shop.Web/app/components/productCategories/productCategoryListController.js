/// <reference path="~/Assets/admin/libs/angular/angular.js" />

(function (app) {
	app.controller('productCategoryListController', productCategoryListController);

	productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

	function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
		$scope.productCategories = [];
		$scope.page = 0;
		$scope.pagesCount = 0;
		$scope.keyword = "";
		$scope.getProductCagories = getProductCagories;
		$scope.search = search;
		$scope.deleteProductCategory = deleteProductCategory;

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
							listDeleteProductCategory: JSON.stringify(listItem)//stringify convert object to string
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
				angular.forEach($scope.productCategories, function (item) {
					item.Checked = true;
				})

				$scope.isSelectAll = true;
			}
			else {
				angular.forEach($scope.productCategories, function (item) {
					item.Checked = false;
				})
				$scope.isSelectAll = false;
			}
		}

		$scope.$watch("productCategories", function (newValue, oldValue) {
			var checked = $filter("filter")(newValue, { Checked: true });
			if (checked.length) {
				$scope.selected = checked;
				$('#btnDelete').removeAttr('disabled');
			}
			else {
				$('#btnDelete').attr('disabled', 'disabled');
			}
		}, true);

		function deleteProductCategory(id) {
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
			getProductCagories();
		}

		function getProductCagories(page) {
			page = page || 0;
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