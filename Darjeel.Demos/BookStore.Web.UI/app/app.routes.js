(function () {
    "use strict";

    /* @ngInject */
    function config($routeProvider) {
        $routeProvider
			.otherwise({
			    redirectTo: "/products"
			})
        ;
    }

    angular
		.module("app")
		.config(["$routeProvider", config])
    ;
})();