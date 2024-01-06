AddItemToCart = function (e) {

    //doc du lieu tu attribute
    var productId = $(e).data('productid');
    var productName = $(e).data('productname');
    var productDescription = $(e).data('productdescription');
    var productPrice = $(e).data('productprice');
    var productCreated = $(e).data('productcreated');
    var productInStock = $(e).data('productinstock');
    var homeFlag = $(e).data('homefla');
    var bestSeller = $(e).data('bestseller');
    var prImage1 = $(e).data('primage1');
    var categoryName = $(e).data('categoryname');

    var item = {
        ProductId: productId,
        ProductName: productName,
        ProductDescription: productDescription,
        ProductPrice: productPrice,
        ProductCreated: productCreated,
        ProductInStock: productInStock,
        HomeFlag: homeFlag,
        BestSeller: bestSeller,
        PrImage1: prImage1,
        CategoryName: categoryName,
        Quantity : 1
    }

    var store = GetCookie('MyShoppingCart');
    Add_UpdateShoppingCart(store, item);

    var index = store.findIndex(function (d) {
        return d.ProductId === item.ProductId;
    });

    var quantity = store[index].Quantity 

    //alert("Thêm vào giỏ thành công!" + quantity);

}

RemoveOneItemFromCart = function (e) {

    //doc du lieu tu attribute
    var productId = $(e).data('productid');
    var productName = $(e).data('productname');
    var productDescription = $(e).data('productdescription');
    var productPrice = $(e).data('productprice');
    var productCreated = $(e).data('productcreated');
    var productInStock = $(e).data('productinstock');
    var homeFlag = $(e).data('homefla');
    var bestSeller = $(e).data('bestseller');
    var prImage1 = $(e).data('primage1');
    var categoryName = $(e).data('categoryname');

    var item = {
        ProductId: productId,
        ProductName: productName,
        ProductDescription: productDescription,
        ProductPrice: productPrice,
        ProductCreated: productCreated,
        ProductInStock: productInStock,
        HomeFlag: homeFlag,
        BestSeller: bestSeller,
        PrImage1: prImage1,
        CategoryName: categoryName,
        Quantity: 1
    }

    var store = GetCookie('MyShoppingCart');
    RemoveOne_UpdateShoppingCart(store, item);

    var index = store.findIndex(function (d) {
        return d.ProductId === item.ProductId;
    });

    var quantity = store[index].Quantity

    //alert("Thêm vào giỏ thành công!" + quantity);

}

RemoveItemFromCart = function (e) {
    var result = confirm("Bạn có chắc chắn muốn xóa sản phẩm này ?");
    if (result) {
        var item = {
            ProductId: productId,
            ProductName: productName,
            ProductDescription: productDescription,
            ProductPrice: productPrice,
            ProductCreated: productCreated,
            ProductInStock: productInStock,
            HomeFlag: homeFlag,
            BestSeller: bestSeller,
            PrImage1: prImage1,
            CategoryName: categoryName,
        }

        var store = GetCookie('MyShoppingCart');
        store = RemoveItemFromShoppingCart(store, item);
        //UpdateShoppingCartView(store, 2);

        window.location.href = "/shoppingCart/Index";
    }
}

UpdateItemToCart = function (e) {
    var quantity = $("#txtQuantity").val();
    if (quantity == "" || quantity == null) { return; }

    var productNumber = parseInt(quantity, 10);
    if (productNumber <= 0) { return; }

    var item = {
        ProductID: $(e).data('productid'),
        ProductName: $(e).data('productname'),
        Quantity: parseInt(quantity, 10),
        Price: $(e).data('price')

    };

    var store = GetCookie('MyShoppingCart');

    var index = store.findIndex(function (d) {
        return d.ProductID == item.ProductID;
    });

    if (store.length == 0 || index == -1) {
        return;
    }

    store[index].Quantity = productNumber;
    SetCookie('MyShoppingCart', store);

    window.location.href = "/shoppingCart/Index";
}

Add_UpdateShoppingCart = function (store, item, quantity) {
    console.log(store);
    console.log(item);

    var index = store.findIndex(function (d) {
        return d.ProductId === item.ProductId;
    });

    if (store.length == 0 || index == -1) {
        store.push(item);
        SetCookie('MyShoppingCart', store);
        return store;
    }
    if (quantity != null && quantity != "undefined" && !isNaN(quantity) && parseInt(quantity) > 0) {
        store[index].Quantity = quantity;
    }
    else {
        store[index].Quantity = parseInt(store[index].Quantity) + 1;
    }
    SetCookie('MyShoppingCart', store);
    return store;
}

RemoveOne_UpdateShoppingCart = function (store, item, quantity) {
    console.log(store);
    console.log(item);

    var index = store.findIndex(function (d) {
        return d.ProductId === item.ProductId;
    });

    if (store.length == 0 || index == -1) {
        SetCookie('MyShoppingCart', store);
        return store;
    }

    store[index].Quantity--;

    if (store[index].Quantity === 0) {
        store.splice(index, 1);
    }

    SetCookie('MyShoppingCart', store);
    return store;
}

RemoveItemFromShoppingCart = function (store, item) {

    if (store.length > 0) {
        var index = store.findIndex(function (d) {
            return d.ProductID == item.ProductID;
        });

        store.splice(index, 1);
        SetCookie('MyShoppingCart', store);
        return store;
    }

}