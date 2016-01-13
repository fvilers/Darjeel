(function () {
	"use strict";

	/* @ngInject */
	function httpErrorLogger($q, $log) {
		function onResponse(response) {
            if (response && response.status === 202) {
                $.growl("Your submission has been accepted.", { type: "success" });
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
	    .factory("httpErrorLogger", ["$q", "$log", httpErrorLogger])
	;
})();