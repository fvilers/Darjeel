(function () {
    "use strict";

    /* @ngInject */
    function config($routeProvider) {
        $routeProvider
			.when("/products", {
			    name: "products",
			    templateUrl: "app/catalog/partials/products.html",
			    controller: "productController",
			    controllerAs: "vm"
			})
            .when("/products/add", {
                name: "add-product",
                templateUrl: "app/catalog/partials/add-product.html",
                controller: "addProductController",
                controllerAs: "vm"
            })
        ;
    }

    angular
		.module("catalogModule")
		.config(["$routeProvider", config])
    ;
})();