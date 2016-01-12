(function () {
    "use strict";

    /* @ngInject */
    function productController(productService) {
        var vm = this;

        function mapProduct(product) {
            var mapped = {
                title: product.title
            };

            return mapped;
        }

        function findProducts() {
            return productService
				.find()
				.success(function (products) {
				    vm.products = products.map(mapProduct);
				    return products;
				})
				.error(function () {
				})
            ;
        }

        function load() {
            return findProducts().then(function () {
            });
        }

        load();
    };

    angular
		.module("catalogModule")
		.controller("productController", ["productService", productController])
    ;
})();