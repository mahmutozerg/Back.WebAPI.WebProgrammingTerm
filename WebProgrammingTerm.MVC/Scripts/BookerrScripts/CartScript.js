
function getCartItems() {
    var cartItems = JSON.parse(localStorage.getItem('cart')) || [];

    console.log('Retrieved cart items:', cartItems);

    return cartItems
}
function addToCart(productId) {

    var cartItems = JSON.parse(localStorage.getItem('cart')) || [];

    cartItems.push(productId);

    localStorage.setItem('cart', JSON.stringify(cartItems));
    
    console.log('Product added to cart:', productId);
}

function SenRequestForAddToCart(token)
{
    if (token === "")
    {
        window.location.href = "http://localhost:5000/signin";
        return;
    }
    
    let cartItems =getCartItems()
    if (cartItems.length >0)
    {
        $.ajax({
            type: 'POST',
            url: '/Cart/add',
            data: { productIds: cartItems },
            success: function (data) {

                console.log('Cart cleared');
                removeCartFromLocal()
            },
            error: function (error) {
                console.error('Error adding product to cart on the server.');
            }
        });

    }


}

function  removeCartFromLocal()
{
    localStorage.removeItem('cart');

}

function addToFavorites(productId,token) {


    if (token === "")
    {
        window.location.href = "http://localhost:5000/signin";
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/Favorites',
        data: { productId: productId},
        success: function (data) {
            console.log('Product added to cart on the server.');

            localStorage.removeItem('cart');
            console.log('Cart cleared');
            
            console.log('Cart cleared after adding the product to the server.');
        },
        error: function (error) {
            console.error('Error adding product to cart on the server.');
        }
    });

}