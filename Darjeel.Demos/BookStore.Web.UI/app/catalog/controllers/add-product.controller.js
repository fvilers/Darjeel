(function () {
    "use strict";

    /* @ngInject */
    function addProductController(productService, $location) {
        var vm = this;

        function addProduct() {
            var product = {
                title: vm.title
            };

            return productService
                .add(product)
                .success(function() {
                    $location.path("#/products");
                })
            ;
        }

        vm.addProduct = addProduct;
    };

    angular
		.module("catalogModule")
		.controller("addProductController", ["productService", "$location", addProductController])
    ;
})();