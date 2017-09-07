/// <reference path="~/Assets/admin/libs/angular/angular.js" />
/// <reference path="notificatonService.js" />

(function (app) {
	app.factory('apiService', apiService);
	apiService.$inject = ['$http', 'notificationService'];

	function apiService($http, notificationService) {
		return {
			get: get,
			post: post,
			put: put,
			del: del
		}

		//Delete
		function del(url, data, success, failure) {
			$http.delete(url, data).then(function (result) {
				success(result);
			}, function (error) {
				if (error.status === 401) {
					notificationService.displayError('Authenticate is required.', 'Error');
				}
				else if (failure != null) {
					failure(error);
				}

			});
		}

		function post(url, data, success, failure) {
			$http.post(url, data).then(function (result) {
				success(result);
			}, function (error) {
				if (error.status === 401) {
					notificationService.displayError('Authenticate is required.', 'Error');
				}
				else if (failure != null) {
					failure(error);
				}

			});
		}

		//Update
		function put(url, data, success, failure) {
			$http.put(url, data).then(function (result) {
				success(result);
			}, function (error) {
				if (error.status === 401) {
					notificationService.displayError('Authenticate is required.');
				}
				else if (failure != null) {
					failure(error);
				}
			});
		}

		//Get
		function get(url, params, success, failure) {
			$http.get(url, params).then(function (result) {
				success(result);
			}, function (error) {
				failure(error);
			});
		}
	}
})(angular.module('shopapp.common'));