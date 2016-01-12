(function () {
	"use strict";

	/* @ngInject */
	function httpErrorLogger($q, $log, $window) {
		function onResponse(response) {
            if (response && response.status === 202) {
                $window.alert("Your submission has been accepted.");
            }

			return response;
		}

		function onResponseError(rejection) {
			$log.error("HTTP error (" + rejection.status + "): " + rejection.statusText + ".");

			return $q.reject(rejection);
		}

		var interceptor = {
			response: onResponse,
			responseError: onResponseError
		};

		return interceptor;
	};

	function config($httpProvider) {
		$httpProvider.interceptors.push("httpErrorLogger");
	}

	angular
		.module("app")
		.config(["$httpProvider", config])
	    .factory("httpErrorLogger", ["$q", "$log", "$window", httpErrorLogger])
	;
})();