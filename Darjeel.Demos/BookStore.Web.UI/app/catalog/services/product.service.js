(function () {
    "use strict";

    /* @ngInject */
    function productService($http) {
        var url = "/api/catalog/products";

        function find() {
            return $http.get(url);
        }

        function add(product) {
            return $http.post(url, product);
        }

        var service = {
            find: find,
            add: add
        };

        return service;
    };

    angular
		.module("catalogModule")
	    .factory("productService", ["$http", productService])
    ;
})();