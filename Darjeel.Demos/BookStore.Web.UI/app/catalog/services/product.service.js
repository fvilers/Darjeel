(function () {
    "use strict";

    /* @ngInject */
    function productService($http) {
        var url = "/api/catalog/products";

        function find() {
            return $http.get(url);
        }

        var service = {
            find: find
        };

        return service;
    };

    angular
		.module("catalogModule")
	    .factory("productService", ["$http", productService])
    ;
})();